using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubeSubscriptions.Classes
{
    public class GeneralVideoManager
    {
        private Panel Container;
        private EventHandler Click;
        private EventHandler VideoBoxMouseEnter;
        public List<YoutuberVideo> Videos;
        public List<VideoBox> VideoBoxes;

        public GeneralVideoManager(Panel Container, EventHandler Click, EventHandler VideoBoxMouseEnter, List<YoutuberVideo> Videos, TSettings InitialSettings)
        {
            this.Container = Container;
            this.Click += Click;
            this.VideoBoxMouseEnter += VideoBoxMouseEnter;
            this.Videos = Videos;
            this.VideoBoxes = new List<VideoBox>();
            this.RecreateVideoBoxes(InitialSettings);
        }

        public void RecreateVideoBoxes(TSettings Settings)
        {
            Container.VerticalScroll.Value = 0;

            // If Youtuber is null, clear and escape
            if (Videos == null)
            {
                foreach (VideoBox videoBox in VideoBoxes)
                    videoBox.Hide();
                return;
            }

            // Make sure the existing video box count covers the maximum displayable at this point
            for (int i = VideoBoxes.Count; i < Videos.Count; i++)
                VideoBoxes.Add(new VideoBox(null, Container, new Rectangle(0, 0, 1, 1), Click, VideoBoxMouseEnter));

            // Get box size
            int vbWidth = (Container.Width - (Settings.VideoColumns - 1) * FMain.ControlPadding - 20) / Settings.VideoColumns;
            int vbHeight = (int) Math.Round((double) (vbWidth * 9) / 16);
            VideoBox.titleHeight = Utils.GetTitleHeight(Settings.VideoColumns, VideoBox.titleLineHeight);
            vbHeight = vbHeight + (Settings.ShowVideoUploader ? VideoBox.uploaderHeight : 0) + (Settings.ShowVideoTitle ? VideoBox.titleHeight : 0) + (Settings.ShowVideoStats ? VideoBox.statsHeight : 0);
            VideoBox.videoBoxSize = new Size(vbWidth, vbHeight);
            VideoBox.thumbnailMaxSize = new Size(VideoBox.videoBoxSize.Width, (int) Math.Round((double) (VideoBox.videoBoxSize.Width * 9) / 16) + 1);
            FMain mainForm = Application.OpenForms[0] as FMain;
            int starW = VideoBox.thumbnailMaxSize.Height / 2;
            mainForm.Database.SizedStarImage = Utils.ScaleImage(mainForm.Database.StarImage, starW, starW, false);

            // Iterate through
            for (int i = 0, lastX = 0, lastY = 0; i < VideoBoxes.Count; i++)
            {
                if (i < Videos.Count)
                {
                    VideoBoxes[i].SetBounds(lastX, lastY, VideoBox.videoBoxSize.Width, VideoBox.videoBoxSize.Height);
                    VideoBoxes[i].YoutuberVideo = Videos[i];
                    VideoBoxes[i].YoutuberVideo.Video.LoadThumbImageIfExists();
                    VideoBox.DrawVideoBox(VideoBoxes[i], false);
                    lastX += VideoBox.videoBoxSize.Width + FMain.ControlPadding;
                    if (lastX + VideoBox.videoBoxSize.Width + 19 > Container.Width)
                    {
                        lastX = 0;
                        lastY += VideoBox.videoBoxSize.Height + FMain.ControlPadding;
                    }
                }
                VideoBoxes[i].Visible = i < Videos.Count;
            }
        }
    }

    public class VideoBox : PictureBox
    {
        public static Size videoBoxSize = new Size(100, 100);
        public static Size thumbnailMaxSize = new Size(100, 100);
        public const int uploaderHeight = 24;
        public const int titleLineHeight = 18;
        public static int titleHeight = titleLineHeight;
        public const int statsHeight = 24;
        public const int textPadding = 2;

        private static Brush bgBrush = new SolidBrush(MyGUIs.ButtonC);
        private static Brush bgBrushMO = new SolidBrush(MyGUIs.SelectedButtonC);
        private static Brush titleBrush = new SolidBrush(MyGUIs.FontC);
        private static Brush titleBrushMO = new SolidBrush(Color.Orange);
        private static Brush statsBrush = new SolidBrush(Color.LightGray);
        private static Brush statsBrushMO = new SolidBrush(Color.WhiteSmoke);
        private static Brush durationBgBrush = new SolidBrush(Color.FromArgb(191, Color.Black));

        private static Font uploaderFont = MyGUIs.GetFont("Segoe UI", 11, true);
        private static Font titleFont = MyGUIs.GetFont("Segoe UI", 10, true);
        private static Font durationFont = MyGUIs.GetFont("Andika", 11, false);
        private static Font statsFont = MyGUIs.GetFont("Segoe UI", 10, false);

        public YoutuberVideo YoutuberVideo;

        public VideoBox(YoutuberVideo YoutuberVideo, Panel container, Rectangle bounds, EventHandler Click, EventHandler MouseEnter)
            : base()
        {
            this.YoutuberVideo = YoutuberVideo;
            this.Parent = container;
            this.SetBounds(bounds.Left, bounds.Top, bounds.Width, bounds.Height);
            this.Cursor = Cursors.Hand;
            this.Click += Click;
            this.MouseEnter += MouseEnter;
            this.MouseEnter += new EventHandler(OnMouseEnter);
            this.MouseLeave += new EventHandler(OnMouseLeave);
            DrawVideoBox(this, false);
        }

        public static void OnMouseEnter(object sender, EventArgs e)
        { DrawVideoBox((VideoBox) sender, true); }

        public static void OnMouseLeave(object sender, EventArgs e)
        { DrawVideoBox((VideoBox) sender, false); }

        public static void DrawVideoBox(VideoBox pic, bool selected)
        {
            if (pic == null)
                return;
            if (pic.Image != null)
                pic.Image.Dispose();

            // init
            Bitmap bmp = new Bitmap(pic.Width, pic.Height);
            Graphics g = Graphics.FromImage(bmp);
            TSettings sett = ((FMain) Application.OpenForms[0]).Settings;
            Brush brush = selected ? bgBrushMO : bgBrush;
            Brush titleBr = selected ? titleBrushMO : titleBrush, statsBr = selected ? statsBrushMO : statsBrush;
            Size size;
            Point location = Point.Empty;
            string text;

            // clear
            g.FillRectangle(brush, 0, 0, pic.Width, pic.Height);
            if (pic.YoutuberVideo == null)
                return;

            int uploaderOffset = sett.ShowVideoUploader ? uploaderHeight : 0;

            // thumb
            if (pic.YoutuberVideo.Video.Thumb != null)
                try
                {
                    location = new Point(thumbnailMaxSize.Width / 2 - pic.YoutuberVideo.Video.Thumb.Width / 2,
                        uploaderOffset + thumbnailMaxSize.Height / 2 - pic.YoutuberVideo.Video.Thumb.Height / 2);
                    g.DrawImage(pic.YoutuberVideo.Video.Thumb, location);
                }
                catch (Exception E) { }

            // uploader
            if (sett.ShowVideoUploader)
                switch (sett.UploaderInformationTypeIndex) // check with TSettings.UploaderInformationTypes
                {
                    case 0:
                        text = pic.YoutuberVideo.Youtuber.Name;
                        size = g.MeasureString(text, durationFont).ToSize();
                        location = new Point(textPadding, uploaderHeight / 2 - size.Height / 2);
                        g.DrawString(text, uploaderFont, Brushes.OrangeRed, location);
                        break;
                    case 1:
                        if (pic.YoutuberVideo.Youtuber.Thumb != null)
                            try
                            {
                                int side = Math.Min((int) (pic.Width * 0.2), SubscriptionBox.expandedBoxHeight);
                                location = new Point((int) (pic.Width * 0.04), 1);
                                Image tempThumb = Utils.ScaleImage(pic.YoutuberVideo.Youtuber.Thumb, side, side, false);
                                g.DrawImage(tempThumb, location);
                            }
                            catch (Exception E) { }
                        break;
                    case 2:
                        text = pic.YoutuberVideo.Youtuber.Name;
                        size = g.MeasureString(text, durationFont).ToSize();
                        location = new Point(pic.Width - size.Width - 1, uploaderHeight / 2 - size.Height / 2);
                        g.DrawString(text, uploaderFont, Brushes.OrangeRed, location);
                        if (pic.YoutuberVideo.Youtuber.Thumb != null)
                            try
                            {
                                int side = Math.Min((int) (pic.Width * 0.2), SubscriptionBox.expandedBoxHeight);
                                location = new Point((int) (pic.Width * 0.04), 1);
                                Image tempThumb = Utils.ScaleImage(pic.YoutuberVideo.Youtuber.Thumb, side, side, false);
                                g.DrawImage(tempThumb, location);
                            }
                            catch (Exception E) { }
                        break;
                }

            // bookmark sign
            FMain mainForm = Application.OpenForms[0] as FMain;
            location.Offset(5, VideoBox.thumbnailMaxSize.Height - mainForm.Database.SizedStarImage.Height - 8);
            if (mainForm.Database.BookmarkedVideoIndexByVideoID(pic.YoutuberVideo.Video.ID) != -1)
                g.DrawImage(mainForm.Database.SizedStarImage, location);

            // video length
            if (sett.ShowVideoLengthOnThumb)
            {
                text = Utils.FormatDuration(pic.YoutuberVideo.Video.Duration);
                size = g.MeasureString(text, durationFont).ToSize();
                location = new Point(thumbnailMaxSize.Width - size.Width - 4, uploaderOffset + thumbnailMaxSize.Height - size.Height - 3);
                Rectangle rectangle = new Rectangle(location, size);
                g.FillRectangle(durationBgBrush, rectangle);
                g.DrawString(text, durationFont, Brushes.White, location);
            }

            // video title
            if (sett.ShowVideoTitle)
            {
                Rectangle rect = new Rectangle(textPadding, uploaderOffset + thumbnailMaxSize.Height + textPadding, thumbnailMaxSize.Width - 2 * textPadding, titleHeight);
                g.DrawString(pic.YoutuberVideo.Video.Title, titleFont, titleBr, rect);
            }

            // video stats
            if (sett.ShowVideoStats)
            {
                int top = uploaderOffset + thumbnailMaxSize.Height + (sett.ShowVideoTitle ? titleHeight : 0) + textPadding;

                text = Utils.FormatDateTimeByFormatIndex(pic.YoutuberVideo.Video.Uploaded, sett.UploadedFormatIndex);
                location = new Point(textPadding, top);
                g.DrawString(text, statsFont, statsBr, location);

                text = Utils.FormatNumber(pic.YoutuberVideo.Video.Views) + " views";
                location = new Point(thumbnailMaxSize.Width - g.MeasureString(text, statsFont).ToSize().Width - textPadding, top);
                g.DrawString(text, statsFont, statsBr, location);
            }

            pic.Image = bmp;
        }
    }
}
