using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Xml;
using System.Threading;

namespace YoutubeSubscriptions.Classes
{
    public class SubscriptionManager
    {
        public TDatabase Database;
        private Panel Container;
        private EventHandler Click;
        private EventHandler VideoBoxMouseEnter;
        public List<SubscriptionBox> SubscriptionBoxes;
        public SubscriptionBox SelectedSubBox;

        public SubscriptionManager(TDatabase Database, Panel Container, EventHandler Click, EventHandler VideoBoxMouseEnter)
        {
            this.Database = Database;
            this.Container = Container;
            this.Click = Click;
            this.VideoBoxMouseEnter = VideoBoxMouseEnter;
            this.SubscriptionBoxes = new List<SubscriptionBox>();
            this.RecreateSubscriptionBoxes();
            this.SelectedSubBox = this.SubscriptionBoxes.Count > 0 ? this.SubscriptionBoxes[0] : null;
            if (this.SelectedSubBox != null)
                this.SelectedSubBox.Checked = true;
            SubscriptionBox.DrawSubscriptionBox(this.SelectedSubBox, false, null);
        }

        public void RecreateSubscriptionBoxes()
        {
            FMain mainForm = Application.OpenForms[0] as FMain;
            SubscriptionBox.nameFont = MyGUIs.GetFont("Andika", mainForm.Settings.CompactSubscriptionBox ? SubscriptionBox.compactNameFontSize : SubscriptionBox.expandedNameFontSize, !mainForm.Settings.CompactSubscriptionBox);
            SubscriptionBox.boxHeight = mainForm.Settings.CompactSubscriptionBox ? SubscriptionBox.compactBoxHeight : SubscriptionBox.expandedBoxHeight;
            SubscriptionBox.imagePadding = mainForm.Settings.CompactSubscriptionBox ? SubscriptionBox.compactImagePadding : SubscriptionBox.expandedImagePadding;

            // Make sure the existing sub box count covers the maximum displayable at this point
            Container.VerticalScroll.Value = 0;
            for (int i = SubscriptionBoxes.Count; i < Database.Youtubers.Count; i++)
                SubscriptionBoxes.Add(new SubscriptionBox(null, Container, new Rectangle(0, i * SubscriptionBox.boxHeight, Container.Width - 20, SubscriptionBox.boxHeight), Click, VideoBoxMouseEnter));
            TYoutuber selBoxSubBeforeRefresh = this.SelectedSubBox != null ? this.SelectedSubBox.Youtuber : null;

            // Filter
            List<TYoutuber> filteredYoutubers = Database.GetFilteredYoutubers(mainForm.SubSearchTB.Text);

            // Refresh accordingly
            for (int i = 0; i < SubscriptionBoxes.Count; i++)
            {
                SubscriptionBoxes[i].Visible = i < filteredYoutubers.Count;
                if (SubscriptionBoxes[i].Visible)
                {
                    SubscriptionBoxes[i].Top = i * SubscriptionBox.boxHeight;
                    SubscriptionBoxes[i].Height = SubscriptionBox.boxHeight;
                    SubscriptionBoxes[i].Youtuber = filteredYoutubers[i];
                    SubscriptionBoxes[i].Youtuber.LoadThumbImageIfExists();
                    SubscriptionBox.DrawSubscriptionBox(SubscriptionBoxes[i], false, null);
                }
            }
            mainForm.FilteredSubsCountL.Text = string.Format("Filtered subscriptions ({0}/{1})", filteredYoutubers.Count, Database.Youtubers.Count);
            mainForm.FilteredSubsCountL.ForeColor = filteredYoutubers.Count == Database.Youtubers.Count ? MyGUIs.FontC : MyGUIs.SelectedFontC;

            // Display selection stats
            long videos = 0, subs = 0, views = 0;
            foreach (TYoutuber youtuber in filteredYoutubers)
            {
                videos += youtuber.Videos.Count;
                subs += youtuber.Subscribers;
                views += youtuber.CurrentVideoViews();
            }
            mainForm.FilteredSubsInfoL.Text = string.Format("Filtered: {0} ({1} in total) / {2} and {3} for them",
                Utils.RegularPlural("subscription", filteredYoutubers.Count, true), Utils.RegularPlural("subscriber", subs, true),
                Utils.RegularPlural("recent video", videos, true), Utils.RegularPlural("view", views, true));

            /*  Housekeeping: if the filtered subs list changes structure (caused by a filter modification, as opposed to a simple refresh),
             *  check that the newly displayed list shows what is selected/checked correctly. That is, if the previous sub is also present in
             *  this list, keep it selected (as it may be in a different sub box). Otherwise, clear all the things and show a message.
             */

            // Uncheck everything that thinks of itself as being checked (there can be multiple checked items depending on the structures and sizes of the previous lists)
            foreach (SubscriptionBox subBox in this.SubscriptionBoxes)
                if (subBox.Checked)
                {
                    subBox.Checked = false;
                    SubscriptionBox.DrawSubscriptionBox(subBox, false, null);
                }

            // If the previously selected sub still is in the list, check it in its new correct position
            if (filteredYoutubers.Contains(selBoxSubBeforeRefresh))
            {
                foreach (SubscriptionBox subBox in this.SubscriptionBoxes)
                    if (subBox.Youtuber.Equals(selBoxSubBeforeRefresh))
                    {
                        this.SelectedSubBox = subBox;
                        this.SelectedSubBox.Checked = true;
                        SubscriptionBox.DrawSubscriptionBox(subBox, false, null);
                        this.Container.ScrollControlIntoView(this.SelectedSubBox);
                    }
            }
            // If not, you don't want the videos to still be displayed, so clear them
            else
            {
                this.SelectedSubBox = null;
                if (mainForm.VideoManager != null)
                {
                    mainForm.VideoManager.Videos = new List<YoutuberVideo>();
                    mainForm.VideoManager.RecreateVideoBoxes(mainForm.Settings);
                    mainForm.StatusBar.UpdateStatus("", "Click a subscription from the subscription list on the left in order to list its videos", "", 100);
                }
            }
        }

        public int GetThreadsWithStatus(int status)
        {
            int n = 0;
            foreach (SubscriptionBox sb in SubscriptionBoxes)
                if (sb.CurrentStatus == status)
                    n++;
            return n;
        }

        public int GetWorkingThreads()
        {
            return GetThreadsWithStatus(SubscriptionBox.StatusPreparing) + GetThreadsWithStatus(SubscriptionBox.StatusUpdating);
        }

        public void UpdateAllSubscriptions()
        {
            foreach (SubscriptionBox sb in SubscriptionBoxes)
                if (sb.Visible && sb.CurrentStatus != SubscriptionBox.StatusUpdating && sb.CurrentStatus != SubscriptionBox.StatusPreparing)
                {
                    sb.CurrentStatus = SubscriptionBox.StatusPreparing;
                    SubscriptionBox.DrawSubscriptionBox(sb, false, "Updating...");
                    sb.UpdateThread.RunWorkerAsync();
                }
        }
    }

    public class SubscriptionBox : PictureBox
    {
        public const int StatusPreparing = 0, StatusUpdating = 1, StatusDoneOK = 2, StatusDoneFAIL = 3;

        public const int checkedBarHeight = 2;
        public const int compactBoxHeight = 28, expandedBoxHeight = 60, compactImagePadding = 1, expandedImagePadding = 2, compactNameFontSize = 12, expandedNameFontSize = 17;
        private static Brush bgBrush = new SolidBrush(MyGUIs.ButtonC);
        private static Brush bgBrushMO = new SolidBrush(MyGUIs.SelectedButtonC);
        private static Brush checkedBrush = new SolidBrush(Color.Orange);
        private static Brush titleBrush = new SolidBrush(ColorTranslator.FromHtml("#FFFFFF"));
        private static Brush statusBrush = new SolidBrush(ColorTranslator.FromHtml("#999999"));

        public static int boxHeight = expandedBoxHeight, imagePadding = expandedImagePadding;
        public static Font nameFont;
        private static Font statusFont = MyGUIs.GetFont("Andika", 12, false);

        public TYoutuber Youtuber;
        public bool Checked;

        public BackgroundWorker UpdateThread;
        public int CurrentStatus;
        public string FailureReason;

        public SubscriptionBox(TYoutuber Youtuber, Panel container, Rectangle bounds, EventHandler Click, EventHandler MouseEnter)
            : base()
        {
            this.Youtuber = Youtuber;
            this.Checked = false;
            this.UpdateThread = new BackgroundWorker();
            this.UpdateThread.DoWork += new DoWorkEventHandler(UpdateSubscription);
            this.UpdateThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(UpdateYoutuberFinished);
            this.CurrentStatus = StatusDoneOK;
            this.FailureReason = "(you shouldn't be seeing this message)";
            this.Parent = container;
            this.SetBounds(bounds.Left, bounds.Top, bounds.Width, bounds.Height);
            this.Cursor = Cursors.Hand;
            this.Click += Click;
            this.MouseEnter += MouseEnter;
            this.MouseEnter += new EventHandler(OnMouseEnter);
            this.MouseLeave += new EventHandler(OnMouseLeave);
            DrawSubscriptionBox(this, false, null);
        }

        private void UpdateSubscription(object sender, DoWorkEventArgs e)
        {
            this.CurrentStatus = StatusUpdating;
            this.FailureReason = "";

            string phase = "preparing";
            TSettings Settings = (Application.OpenForms[0] as FMain).Settings;
            XmlDocument doc = new XmlDocument();

            // Youtuber info

            // download youtuber info
            string destFile = Path.Combine(Paths.GDataXmlFolder, this.Youtuber.ID + ".xml");
            if (!DownloadFromTheHolyInternet(this.Youtuber.GetYoutuberJsonAddress(), destFile))
            {
                this.FailureReason = "Failed to download youtuber JSON from\n" + this.Youtuber.GetYoutuberJsonAddress();
                return;
            }
            // decode youtuber info
            try
            {
                phase = "loading as XML";
                Utils.EncodeColonsInFile(destFile);
                doc.Load(destFile);
                foreach (XmlNode iNode in doc.ChildNodes[1].ChildNodes)
                    if (iNode.Name.Equals(Utils.EncodeColons("gd:feedLink")))
                    {
                        phase = "decoding XML (upload count)";
                        if (iNode.Attributes["rel"] != null && iNode.Attributes["rel"].Value.IndexOf("user.uploads") != -1)
                            this.Youtuber.TotalVideos = long.Parse(iNode.Attributes["countHint"].Value);
                    }
                    else if (iNode.Name.Equals(Utils.EncodeColons("yt:statistics")))
                    {
                        phase = "decoding XML (subs): ";
                        this.Youtuber.Subscribers = long.Parse(iNode.Attributes["subscriberCount"].Value);
                        phase = "decoding XML (total views)";
                        this.Youtuber.TotalViews = long.Parse(iNode.Attributes["totalUploadViews"].Value);
                    }
                    else if (iNode.Name.Equals("published"))
                    {
                        phase = "decoding XML (joined)";
                        this.Youtuber.Joined = Utils.DecodeYoutubeApiDate(iNode.InnerText);
                    }
                    else if (iNode.Name.Equals(Utils.EncodeColons("media:thumbnail")))
                    {
                        phase = "downloading youtuber profile pic";
                        string src = Utils.DecodeColons(iNode.Attributes["url"].Value).Replace("s88-c-k-no/photo.jpg", "");
                        //System.Diagnostics.Process.Start(src);
                        string dest = Path.Combine(Paths.ThumbnailFolder, this.Youtuber.ID + ".jpg");
                        bool skipDld = File.Exists(dest) && !Settings.DownloadThumbnailsAnyway;
                        if (!skipDld)
                            DownloadFromTheHolyInternet(src, dest);
                    }
            }
            catch (Exception E)
            { this.FailureReason = "Failed to load/decode the downloaded XML youtuber information from \"" + destFile + "\".\n\nPhase: " + phase + "\n\n" + E.ToString(); }

            // Video info

            // reset videos
            this.Youtuber.Videos.Clear();
            int downloadStartIndex = 1, downloadCount = 50, iCurr = 0;
            DateTime earliestUploadDate = DateTime.Now.Date.AddDays(-((FMain) Application.OpenForms[0]).Settings.VideoDaysGoBack);
            bool itsTimeToQuit = false;

            while (true)
            {
                // download video info
                destFile = Path.Combine(Paths.GDataXmlFolder, this.Youtuber.ID + " videos " + downloadStartIndex + "-" + (downloadStartIndex + downloadCount - 1) + ".xml");
                if (!DownloadFromTheHolyInternet(this.Youtuber.GetVideosJsonAddress(downloadStartIndex, downloadCount), destFile))
                {
                    this.FailureReason = "Failed to download video uploads JSON from\n" + this.Youtuber.GetVideosJsonAddress(downloadStartIndex, downloadCount);
                    return;
                }

                // decode video info
                try
                {
                    phase = "loading as XML";
                    Utils.EncodeColonsInFile(destFile);
                    doc.Load(destFile);
                    phase = "parsing XML";
                    foreach (XmlNode iNode in doc.ChildNodes[1].ChildNodes)
                        if (iNode.Name.Equals("entry"))
                        {
                            TVideo video = new TVideo();
                            foreach (XmlNode jNode in iNode.ChildNodes)
                                if (jNode.Name.Equals("id"))
                                {
                                    phase = "decoding id";
                                    video.ID = jNode.InnerText.Substring(jNode.InnerText.IndexOf(Utils.EncodeColons(":video:")) + Utils.EncodeColons(":video:").Length);
                                }
                                else if (jNode.Name.Equals("title"))
                                {
                                    phase = "decoding title";
                                    video.Title = Utils.DecodeColons(jNode.InnerText.Replace("  ", " ").Trim());
                                }
                                else if (jNode.Name.Equals(Utils.EncodeColons("gd:comments")))
                                {
                                    phase = "decoding comment count";
                                    foreach (XmlNode kNode in jNode.ChildNodes)
                                        if (kNode.Name.Equals(Utils.EncodeColons("gd:feedLink")))
                                            video.Comments = long.Parse(kNode.Attributes["countHint"].Value);
                                }
                                else if (jNode.Name.Equals(Utils.EncodeColons("media:group")))
                                {
                                    phase = "decoding screen url";
                                    foreach (XmlNode kNode in jNode.ChildNodes)
                                        if (kNode.Name.Equals(Utils.EncodeColons("media:thumbnail")) && kNode.Attributes["url"] != null && kNode.Attributes["url"].Value.IndexOf("mqdefault.jpg") != -1)
                                        {
                                            phase = "downloading video screenshot";
                                            string src = Utils.DecodeColons(kNode.Attributes["url"].Value);
                                            //string src = "http" + "://i.ytimg.com/vi/" + video.ID + "/maxresdefault.jpg";
                                            string dest = Path.Combine(Paths.ThumbnailFolder, video.ID + ".jpg");
                                            bool skipDld = File.Exists(dest) && !Settings.DownloadThumbnailsAnyway;
                                            if (!skipDld)
                                                DownloadFromTheHolyInternet(src, dest);
                                        }
                                        else if (kNode.Name.Equals(Utils.EncodeColons("yt:duration")))
                                        {
                                            phase = "decoding duration";
                                            video.Duration = long.Parse(kNode.Attributes["seconds"].Value);
                                        }
                                        else if (kNode.Name.Equals(Utils.EncodeColons("yt:uploaded")))
                                        {
                                            phase = "decoding uploaded date";
                                            video.Uploaded = Utils.DecodeYoutubeApiDate(kNode.InnerText);
                                        }
                                }
                                else if (jNode.Name.Equals(Utils.EncodeColons("gd:rating")))
                                {
                                    phase = "decoding rating";
                                    video.AvgRating = Double.Parse(jNode.Attributes["average"].Value);
                                }
                                else if (jNode.Name.Equals(Utils.EncodeColons("yt:statistics")))
                                {
                                    phase = "decoding views";
                                    video.Views = long.Parse(jNode.Attributes["viewCount"].Value);
                                }
                                else if (jNode.Name.Equals(Utils.EncodeColons("yt:rating")))
                                {
                                    phase = "decoding likes/dislikes";
                                    video.Likes = long.Parse(jNode.Attributes["numLikes"].Value);
                                    video.Dislikes = long.Parse(jNode.Attributes["numDislikes"].Value);
                                }
                            //
                            //MessageBox.Show("here " + video.Uploaded.CompareTo(earliestUploadDate));
                            //if (video.Uploaded.CompareTo(earliestUploadDate) >= 0)
                                this.Youtuber.Videos.Add(video);
                            //
                            iCurr++;
                            //MessageBox.Show(string.Format("{4}\nid={0}, date={1}/{5}\nicurr={2}/total={3}", video.ID, video.Uploaded.ToString("dd-MMMM-yyyy"), iCurr, this.Youtuber.TotalVideos, this.Youtuber.Name, earliestUploadDate.ToString("dd-MMMM-yyyy")));
                            if (iCurr == this.Youtuber.TotalVideos || video.Uploaded.CompareTo(earliestUploadDate) < 0)
                                itsTimeToQuit = true;
                            if (itsTimeToQuit)
                                break;
                        }

                    if (itsTimeToQuit)
                        break;
                }
                catch (Exception E)
                { this.FailureReason = "Failed to load/decode the downloaded XML video list information from \"" + destFile + "\", range " + downloadStartIndex + "+" + downloadCount + ".\n\nPhase: " + phase + "\n\n" + E.ToString(); }

                downloadStartIndex += downloadCount;
            }

            if (this.FailureReason != null && !this.FailureReason.Equals(""))
                MessageBox.Show(this.FailureReason);

            // sort videos
            TDatabase.SortVideoList(this.Youtuber.Videos);
            this.Youtuber.InfoLastUpdated = DateTime.Now;
        }

        private static bool DownloadFromTheHolyInternet(string url, string destFile)
        {
            try { new WebClient().DownloadFile(url, destFile); }
            catch (Exception E) { return false; }
            return true;
        }

        private void UpdateYoutuberFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            //MessageBox.Show(this.Youtuber.Name + ": " + this.Youtuber.Videos.Count);
            this.CurrentStatus = this.FailureReason.Equals("") ? StatusDoneOK : StatusDoneFAIL;
            if (this.CurrentStatus == StatusDoneFAIL)
                MessageBox.Show(this.Youtuber.ID + " failed!\n\nReason: " + FailureReason);
            foreach (SubscriptionBox sb in ((FMain) Application.OpenForms[0]).SubManager.SubscriptionBoxes)
                if (sb.Visible && sb.Youtuber.ID.Equals(this.Youtuber.ID))
                {
                    sb.Youtuber.LoadThumbImageIfExists();
                    if (sb.Checked)
                        sb.InvokeOnClick(sb, null);
                    SubscriptionBox.DrawSubscriptionBox(sb, false, null);
                    break;
                }
        }

        public static void OnMouseEnter(object sender, EventArgs e)
        { if (!((SubscriptionBox) sender).UpdateThread.IsBusy) DrawSubscriptionBox((SubscriptionBox) sender, true, null); }

        public static void OnMouseLeave(object sender, EventArgs e)
        { if (!((SubscriptionBox) sender).UpdateThread.IsBusy) DrawSubscriptionBox((SubscriptionBox) sender, false, null); }

        public static void DrawSubscriptionBox(SubscriptionBox pic, bool selected, string status)
        {
            if (pic.Image != null)
                pic.Image.Dispose();
            Bitmap bmp = new Bitmap(pic.Width, pic.Height);
            Graphics g = Graphics.FromImage(bmp);
            Brush brush = selected ? bgBrushMO : bgBrush;
            g.FillRectangle(brush, 0, 0, pic.Width, pic.Height);
            if (pic.Youtuber == null)
                return;
            if (pic.Youtuber.Thumb != null)
                try
                {
                    int imgWidth = pic.Height - 2 * imagePadding;
                    g.DrawImage(pic.Youtuber.Thumb, new Point(imagePadding, imagePadding));
                }
                catch (Exception E) { }
            Size size = g.MeasureString(pic.Youtuber.Name, nameFont).ToSize();
            g.DrawString(pic.Youtuber.Name, nameFont, selected ? checkedBrush : titleBrush, new Point(pic.Height + expandedImagePadding, 0));
            if (pic.Height == expandedBoxHeight)
            {
                string text = status;
                if (text == null)
                {
                    TSettings sett = (Application.OpenForms[0] as FMain).Settings;
                    // see TSettings.SubBoxSubtitleCaptions
                    switch (sett.SubscriptionBoxSubtitleIndex)
                    {
                        case 0: // Last updated
                            text = string.Format("Last updated {0}", Utils.FormatElapsedTime(DateTime.Now.Subtract(pic.Youtuber.InfoLastUpdated)));
                            break;
                        case 1: // Joined Youtube
                            text = string.Format("Joined {0}", Utils.FormatDateTime(pic.Youtuber.Joined, "d MMMM yyyy"));
                            break;
                        case 2: // Subscribers
                            text = string.Format("{0} subscribers", Utils.FormatNumberOrder(pic.Youtuber.Subscribers));
                            break;
                        case 3: // Average new subscribers per day
                            int days = DateTime.Now.Subtract(pic.Youtuber.Joined).Days;
                            text = string.Format("{0} avg. new subs per day", Utils.FormatNumberOrder(pic.Youtuber.Subscribers / days));
                            break;
                        case 4: // Recent videos / views
                            text = string.Format("{0} / {1} {2}",
                                Utils.RegularPlural("video", pic.Youtuber.Videos.Count, true),
                                Utils.FormatNumberOrder(pic.Youtuber.CurrentVideoViews()),
                                Utils.RegularPlural("view", pic.Youtuber.CurrentVideoViews(), false));
                            break;
                        case 5: // Recent videos / earnings
                            text = pic.Youtuber.Videos.Count == 0 ? "no content, no nothing" :
                                string.Format("{0} / min {1}",
                                    Utils.RegularPlural("video", pic.Youtuber.Videos.Count, true),
                                    Utils.FormatEarnings(Utils.CalculateMinimumEarnings(pic.Youtuber.CurrentVideoViews())));
                            break;
                        case 6: // Recent views / earnings
                            text = pic.Youtuber.Videos.Count == 0 ? "no content, no nothing" :
                                string.Format("{0} {1} / min {2}",
                                    Utils.FormatNumberOrder(pic.Youtuber.CurrentVideoViews()),
                                    Utils.RegularPlural("view", pic.Youtuber.CurrentVideoViews(), false),
                                    Utils.FormatEarnings(Utils.CalculateMinimumEarnings(pic.Youtuber.CurrentVideoViews())));
                            break;
                        case 7: // Recent views per subscribers
                            text = pic.Youtuber.Videos.Count == 0 || pic.Youtuber.Subscribers == 0 ? "no content or subscribers :\\" :
                                string.Format("{0}% views per subs",
                                    ((double) (100 * (pic.Youtuber.CurrentVideoViews() / pic.Youtuber.Videos.Count)) / pic.Youtuber.Subscribers).ToString("n1"));
                            break;
                        case 8: // Earnings per hour of video
                            text = pic.Youtuber.Videos.Count == 0 ? "no content, no earnings" :
                                string.Format("{0} per hour of content",
                                    Utils.FormatEarnings(Utils.CalculateMinimumEarnings(pic.Youtuber.CurrentVideoViews()) / (pic.Youtuber.CurrentVideoTotalDuration() / 3600.0)));
                            break;
                    }
                }
                g.DrawString(text, statusFont, statusBrush, new Point(pic.Height + expandedImagePadding, pic.Height - g.MeasureString(text, statusFont).ToSize().Height - checkedBarHeight - 2));
            }
            if (pic.Checked)
                g.FillRectangle(checkedBrush, 0, pic.Height - checkedBarHeight - 1, pic.Width, pic.Height - 1);
            pic.Image = bmp;
        }
    }
}
