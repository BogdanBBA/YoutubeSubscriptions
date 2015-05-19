using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace YoutubeSubscriptions.Classes
{
    public class TDatabase
    {
        public List<TYoutuber> Youtubers;
        public List<VideoWithYoutuberID> BookmarkedVideos;
        [XmlIgnore]
        public Image StarImage, SizedStarImage;

        public TYoutuber GetYoutuberByID(string ID)
        {
            foreach (TYoutuber youtuber in Youtubers)
                if (youtuber.ID.Equals(ID))
                    return youtuber;
            return null;
        }

        public static string OpenDatabase(out TDatabase Database)
        {
            string phase = "reading data from XML: ";
            try
            {
                phase += "initializing the serializer; ";
                XmlSerializer serializer = new XmlSerializer(typeof(TDatabase));
                phase += "initializing the file stream; ";
                FileStream fs = new FileStream(Paths.DatabaseFile, FileMode.Open);
                phase += "attempting deserialization";
                Database = (TDatabase) serializer.Deserialize(fs);
                fs.Close();
                phase = "actual database loaded successfully, now loading 'star.png'";
                Database.StarImage = new Bitmap(Paths.StarImage);
            }
            catch (Exception E)
            {
                Database = null;
                return "TDatabase.OpenProject() ERROR: Failed to open database at phase '" + phase + "'.\n\n" + E.ToString();
            }
            return "";
        }

        public static string SaveDatabase(TDatabase Database)
        {
            string phase = "saving data to XML: ";
            try
            {
                phase += "initializing the serializer; ";
                XmlSerializer serializer = new XmlSerializer(typeof(TDatabase));
                phase = "initializing the file stream; ";
                TextWriter writer = new StreamWriter(Paths.DatabaseFile);
                phase = "attempting serialization; ";
                serializer.Serialize(writer, Database);
                writer.Close();
            }
            catch (Exception E)
            {
                return "TDatabase.SaveDatabase() ERROR: Failed to save database at phase '" + phase + "'.\n\n" + E.ToString();
            }
            return "";
        }

        public List<TYoutuber> GetFilteredYoutubers(string filter)
        {
            List<TYoutuber> result = new List<TYoutuber>();
            filter = filter.ToLower();
            foreach (TYoutuber youtuber in this.Youtubers)
                if (youtuber.ID.ToLower().IndexOf(filter) != -1 || youtuber.Name.ToLower().IndexOf(filter) != -1)
                    result.Add(youtuber);
            return result;
        }

        public List<YoutuberVideo> Get24HourFeed(out long nYoutubers, out long nViews)
        {
            List<YoutuberVideo> result = new List<YoutuberVideo>();
            List<TYoutuber> youtubers = new List<TYoutuber>();
            nViews = 0;
            long minTicks = DateTime.Now.AddDays(-1).Ticks;
            //
            foreach (TYoutuber youtuber in Youtubers)
                foreach (TVideo video in youtuber.Videos)
                    if (video.Uploaded.Ticks >= minTicks)
                    {
                        if (!youtubers.Contains(youtuber))
                            youtubers.Add(youtuber);
                        nViews += video.Views;
                        result.Add(new YoutuberVideo(youtuber, video));
                    }
            //
            nYoutubers = youtubers.Count;
            TDatabase.SortYoutuberVideoList(result);
            return result;
        }

        public List<YoutuberVideo> GetBookmarkedVideosAsYoutuberVideos()
        {
            List<YoutuberVideo> result = new List<YoutuberVideo>();
            for (int i = 0; i < BookmarkedVideos.Count; i++)
            {
                TYoutuber youtuber = GetYoutuberByID(BookmarkedVideos[i].YoutuberID);
                if (youtuber != null)
                    result.Add(new YoutuberVideo(youtuber, BookmarkedVideos[i].Video));
                else
                {
                    BookmarkedVideos.RemoveAt(i);
                    i--;
                }
            }
            TDatabase.SortYoutuberVideoList(result);
            return result;
        }

        public int BookmarkedVideoIndexByVideoID(string id)
        {
            for (int i = 0; i < BookmarkedVideos.Count; i++)
                if (BookmarkedVideos[i].Video.ID.Equals(id))
                    return i;
            return -1;
        }

        public static void SortVideoList(List<TVideo> videos)
        {
            if (videos.Count >= 2)
                for (int i = 0; i < videos.Count - 1; i++)
                    for (int j = i + 1; j < videos.Count; j++)
                        if (videos[i].Uploaded.CompareTo(videos[j].Uploaded) < 0)
                        {
                            TVideo aux = videos[i];
                            videos[i] = videos[j];
                            videos[j] = aux;
                        }
        }

        public static void SortYoutuberVideoList(List<YoutuberVideo> videos)
        {
            if (videos.Count >= 2)
                for (int i = 0; i < videos.Count - 1; i++)
                    for (int j = i + 1; j < videos.Count; j++)
                        if (videos[i].Video.Uploaded.CompareTo(videos[j].Video.Uploaded) < 0)
                        {
                            YoutuberVideo aux = videos[i];
                            videos[i] = videos[j];
                            videos[j] = aux;
                        }
        }
    }

    public class TYoutuber
    {
        public string ID;
        public string Name;
        public DateTime InfoLastUpdated;
        public DateTime Joined;
        public long Subscribers;
        public long TotalVideos;
        public long TotalViews;
        public List<TVideo> Videos;
        [XmlIgnore]
        public Image Thumb;

        public TYoutuber()
        {
            this.Thumb = null;
        }

        public string GetYoutuberJsonAddress()
        {
            return string.Format("http://gdata.youtube.com/feeds/api/users/{0}?v=2", this.ID);
        }

        public string GetVideosJsonAddress(int start, int count)
        {
            return string.Format("http://gdata.youtube.com/feeds/api/users/{0}/uploads?v=2&start-index={1}&max-results={2}", this.ID, start, count);
        }

        public string GetImagePath()
        {
            return string.Format(@"{0}\{1}.jpg", Paths.ThumbnailFolder, this.ID);
        }

        public string GetProfileAddress()
        {
            return string.Format("https://www.youtube.com/user/{0}/about", this.ID);
        }

        public string GetVideosAddress()
        {
            return string.Format("https://www.youtube.com/user/{0}/videos", this.ID);
        }

        public long CurrentVideoViews()
        {
            long total = 0;
            foreach (TVideo video in Videos)
                total += video.Views;
            return total;
        }

        public long CurrentVideoTotalDuration()
        {
            long total = 0;
            foreach (TVideo video in Videos)
                total += video.Duration;
            return total;
        }

        public void LoadThumbImageIfExists()
        {
            if (File.Exists(this.GetImagePath()))
            {
                int imgWidth = SubscriptionBox.boxHeight - 2 * SubscriptionBox.imagePadding;
                this.Thumb = Utils.ScaleImage(new Bitmap(this.GetImagePath()), imgWidth, imgWidth, true);
            }
            else
                this.Thumb = null;
        }

        public List<YoutuberVideo> GetVideosAsYoutuberVideos()
        {
            List<YoutuberVideo> result = new List<YoutuberVideo>();
            foreach (TVideo video in Videos)
                result.Add(new YoutuberVideo(this, video));
            return result;
        }
    }

    public class TVideo
    {
        public string ID;
        public DateTime Uploaded;
        public string Title;
        public long Duration;
        public long Views;
        public long Likes;
        public long Dislikes;
        public double AvgRating;
        public long Comments;
        [XmlIgnore]
        public Image Thumb;

        public TVideo()
        {
            this.Thumb = null;
        }

        public string GetImagePath()
        {
            return string.Format(@"{0}\{1}.jpg", Paths.ThumbnailFolder, this.ID);
        }

        public string GetWebpageVideoURL()
        {
            return string.Format(@"https://www.youtube.com/watch?v={0}", this.ID);
        }

        public string GetVideoURL()
        {
            return string.Format(@"https://www.youtube.com/v/{0}", this.ID);
        }

        public string GetVideoMaxResDefaultThumb()
        {
            return string.Format(@"http://i.ytimg.com/vi/{0}/maxresdefault.jpg", this.ID);
        }

        public void LoadThumbImageIfExists()
        {
            if (File.Exists(this.GetImagePath()))
            {
                Size maxSize = VideoBox.thumbnailMaxSize;
                this.Thumb = Utils.ScaleImage(new Bitmap(this.GetImagePath()), maxSize.Width, maxSize.Height, true);
            }
            else
                this.Thumb = null;
        }
    }

    public class YoutuberVideo
    {
        public TYoutuber Youtuber;
        public TVideo Video;

        public YoutuberVideo(TYoutuber Youtuber, TVideo Video)
        {
            this.Youtuber = Youtuber;
            this.Video = Video;
        }
    }

    public class VideoWithYoutuberID
    {
        public string YoutuberID;
        public TVideo Video;
    }

    ///
    ///
    ///

    public class TSettings
    {
        public const string TimePassedFormatString = "%TP";
        public static string[] UploaderInformationTypes = { "Uploader name", "Uploader thumb", "Uploader name and thumb" };
        public static string[] UploadedFormats = { TimePassedFormatString, TimePassedFormatString + " (ddd, d/MMM)", TimePassedFormatString + " (d/MMM/yyyy)", "dd.MM.yy", "d-MM-yyyy", "d/MMM/yyyy", "d MMMM yyyy", "ddd, d MMMM yyyy", "dddd, d MMMM yyyy" };
        public static string[] SubBoxSubtitleCaptions = { "Last updated", "Joined Youtube", "Subscribers", "Average new subscribers per day", "Recent videos / views", "Recent videos / earnings", "Recent views / earnings", "Recent views per subscribers", "Recent earnings per hour of content" };
        public static string[] PlaybackQualities = { "small", "medium", "large", "hd720", "hd1080", "highres", "default" };
        public static string[] PlaybackQualityCaptions = { "240p", "360p", "480p", "720p", "1080p", "+1080p", "Default (best fit)" };

        public int VideoDaysGoBack;
        public int VideoColumns;
        public int UploaderInformationTypeIndex;
        public int UploadedFormatIndex;
        public int SubscriptionBoxSubtitleIndex;
        public int DefaultQualityIndex;
        public int DefaultVolume;
        public int SkipSeconds;
        public double DefaultPlaybackRate;
        public bool ShowVideoTitle;
        public bool ShowVideoStats;
        public bool DownloadThumbnailsAnyway;
        public bool ShowVideoLengthOnThumb;
        public bool ShowVideoUploader;
        public bool CompactSubscriptionBox;
        public bool StartFullWindow;

        public void ReadFromFile()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Paths.SettingsFile);
                XmlNode node = doc.SelectSingleNode("/SETTINGS");

                try { VideoDaysGoBack = Int32.Parse(node["VideoDaysGoBack"].Attributes["value"].Value); }
                catch (Exception E) { VideoDaysGoBack = 31; }

                try { VideoColumns = Int32.Parse(node["VideoColumns"].Attributes["value"].Value); }
                catch (Exception E) { VideoColumns = 4; }

                try { UploaderInformationTypeIndex = Int32.Parse(node["UploaderInformationTypeIndex"].Attributes["value"].Value); }
                catch (Exception E) { UploaderInformationTypeIndex = 0; }

                try { UploadedFormatIndex = Int32.Parse(node["UploadedFormatIndex"].Attributes["value"].Value); }
                catch (Exception E) { UploadedFormatIndex = 0; }

                try { SubscriptionBoxSubtitleIndex = Int32.Parse(node["SubscriptionBoxSubtitleIndex"].Attributes["value"].Value); }
                catch (Exception E) { SubscriptionBoxSubtitleIndex = 0; }

                try { DefaultQualityIndex = Int32.Parse(node["DefaultQualityIndex"].Attributes["value"].Value); }
                catch (Exception E) { DefaultQualityIndex = 0; }

                try { DefaultVolume = Int32.Parse(node["DefaultVolume"].Attributes["value"].Value); }
                catch (Exception E) { DefaultVolume = 50; }

                try { SkipSeconds = Int32.Parse(node["SkipSeconds"].Attributes["value"].Value); }
                catch (Exception E) { SkipSeconds = 0; }

                try { DefaultPlaybackRate = Double.Parse(node["DefaultPlaybackRate"].Attributes["value"].Value); }
                catch (Exception E) { DefaultPlaybackRate = 1.0; }

                try { ShowVideoTitle = Boolean.Parse(node["ShowVideoTitle"].Attributes["value"].Value); }
                catch (Exception E) { ShowVideoTitle = true; }

                try { ShowVideoStats = Boolean.Parse(node["ShowVideoStats"].Attributes["value"].Value); }
                catch (Exception E) { ShowVideoStats = true; }

                try { DownloadThumbnailsAnyway = Boolean.Parse(node["DownloadThumbnailsAnyway"].Attributes["value"].Value); }
                catch (Exception E) { DownloadThumbnailsAnyway = false; }

                try { ShowVideoLengthOnThumb = Boolean.Parse(node["ShowVideoLengthOnThumb"].Attributes["value"].Value); }
                catch (Exception E) { ShowVideoLengthOnThumb = true; }

                try { ShowVideoUploader = Boolean.Parse(node["ShowVideoUploader"].Attributes["value"].Value); }
                catch (Exception E) { ShowVideoUploader = true; }

                try { CompactSubscriptionBox = Boolean.Parse(node["CompactSubscriptionBox"].Attributes["value"].Value); }
                catch (Exception E) { CompactSubscriptionBox = false; }

                try { StartFullWindow = Boolean.Parse(node["StartFullWindow"].Attributes["value"].Value); }
                catch (Exception E) { StartFullWindow = false; }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString(), "TSettings.ReadFromFile() ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SaveToFile()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlNode root, node;

                root = doc.AppendChild(doc.CreateElement("SETTINGS"));
                root.Attributes.Append(Utils.GetNewAttribute(doc, "lastSavedCirca", DateTime.Now.ToString("ddd, d MMMM yyyy, HH:mm")));

                node = root.AppendChild(doc.CreateElement("VideoDaysGoBack"));
                node.Attributes.Append(Utils.GetNewAttribute(doc, "value", VideoDaysGoBack.ToString()));

                node = root.AppendChild(doc.CreateElement("VideoColumns"));
                node.Attributes.Append(Utils.GetNewAttribute(doc, "value", VideoColumns.ToString()));

                node = root.AppendChild(doc.CreateElement("UploaderInformationTypeIndex"));
                node.Attributes.Append(Utils.GetNewAttribute(doc, "value", UploaderInformationTypeIndex.ToString()));

                node = root.AppendChild(doc.CreateElement("SubscriptionBoxSubtitleIndex"));
                node.Attributes.Append(Utils.GetNewAttribute(doc, "value", SubscriptionBoxSubtitleIndex.ToString()));

                node = root.AppendChild(doc.CreateElement("DefaultQualityIndex"));
                node.Attributes.Append(Utils.GetNewAttribute(doc, "value", DefaultQualityIndex.ToString()));

                node = root.AppendChild(doc.CreateElement("DefaultVolume"));
                node.Attributes.Append(Utils.GetNewAttribute(doc, "value", DefaultVolume.ToString()));

                node = root.AppendChild(doc.CreateElement("SkipSeconds"));
                node.Attributes.Append(Utils.GetNewAttribute(doc, "value", SkipSeconds.ToString()));

                node = root.AppendChild(doc.CreateElement("DefaultPlaybackRate"));
                node.Attributes.Append(Utils.GetNewAttribute(doc, "value", DefaultPlaybackRate.ToString()));

                node = root.AppendChild(doc.CreateElement("UploadedFormatIndex"));
                node.Attributes.Append(Utils.GetNewAttribute(doc, "value", UploadedFormatIndex.ToString()));

                node = root.AppendChild(doc.CreateElement("ShowVideoTitle"));
                node.Attributes.Append(Utils.GetNewAttribute(doc, "value", ShowVideoTitle.ToString()));

                node = root.AppendChild(doc.CreateElement("ShowVideoStats"));
                node.Attributes.Append(Utils.GetNewAttribute(doc, "value", ShowVideoStats.ToString()));

                node = root.AppendChild(doc.CreateElement("DownloadThumbnailsAnyway"));
                node.Attributes.Append(Utils.GetNewAttribute(doc, "value", DownloadThumbnailsAnyway.ToString()));

                node = root.AppendChild(doc.CreateElement("ShowVideoLengthOnThumb"));
                node.Attributes.Append(Utils.GetNewAttribute(doc, "value", ShowVideoLengthOnThumb.ToString()));

                node = root.AppendChild(doc.CreateElement("ShowVideoUploader"));
                node.Attributes.Append(Utils.GetNewAttribute(doc, "value", ShowVideoUploader.ToString()));

                node = root.AppendChild(doc.CreateElement("CompactSubscriptionBox"));
                node.Attributes.Append(Utils.GetNewAttribute(doc, "value", CompactSubscriptionBox.ToString()));

                node = root.AppendChild(doc.CreateElement("StartFullWindow"));
                node.Attributes.Append(Utils.GetNewAttribute(doc, "value", StartFullWindow.ToString()));

                doc.Save(Paths.SettingsFile);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString(), "TSettings.SaveToFile() ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
