using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubeSubscriptions.Classes
{
    public class VideoTitlePB : PictureBox
    {
        private static Font ytFont = MyGUIs.GetFont("Segoe UI", 20, true);
        private static Font titleFont = MyGUIs.GetFont("Segoe UI Light", 20, false);
        private static Font durationFont = MyGUIs.GetFont("Segoe UI", 20, true);

        public VideoTitlePB(Panel parent, Point location, Size size)
            : base()
        {
            this.Parent = parent;
            this.Location = location;
            this.Size = size;
        }

        public VideoTitlePB(Panel parent)
            : this(parent, Point.Empty, parent.Size)
        {
        }

        public void RefreshForYoutuberVideo(YoutuberVideo yVideo)
        {
            if (this.Image != null)
                this.Image.Dispose();
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(MyGUIs.FormBackgroundC);
            int bottom = this.Height, lastLeft = 0;

            string text = yVideo.Youtuber.Name;
            Size size = g.MeasureString(text, ytFont).ToSize();
            g.DrawString(text, ytFont, Brushes.OrangeRed, new Point(lastLeft, bottom - size.Height));
            lastLeft += size.Width;

            text = " /  ";
            size = g.MeasureString(text, durationFont).ToSize();
            g.DrawString(text, durationFont, Brushes.WhiteSmoke, new Point(lastLeft, bottom - size.Height));
            lastLeft += size.Width;

            text = yVideo.Video.Title;
            size = g.MeasureString(text, titleFont).ToSize();
            g.DrawString(text, titleFont, Brushes.Wheat, new Point(lastLeft, bottom - size.Height));

            text = Utils.FormatDuration(yVideo.Video.Duration);
            size = g.MeasureString(text, durationFont).ToSize();
            g.DrawString(text, durationFont, Brushes.WhiteSmoke, new Point(this.Width - size.Width, bottom - size.Height));

            this.Image = bmp;
        }
    }
}
