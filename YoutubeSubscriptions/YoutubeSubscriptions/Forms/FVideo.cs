using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeSubscriptions.Classes;

namespace YoutubeSubscriptions.Forms
{
    public partial class FVideo : Form
    {
        private static List<string> MenuButtonCaptions = new List<string>(new string[] { "Play!", "Webpage »", "Video »", "CLOSE" });
        private List<PictureBoxButton> MenuButtons;

        private FMain MainForm;
        private FPlayer PlayerForm;

        private YoutuberVideo YoutuberVideo;

        public FVideo(FMain MainForm, FPlayer PlayerForm)
        {
            InitializeComponent();
            this.MainForm = MainForm;
            this.PlayerForm = PlayerForm;
            MyGUIs.InitializeAndFormatFormComponents(this);
        }

        private void FVideo_Load(object sender, EventArgs e)
        {
            MenuButtons = MyGUIs.CreateMenuButtons(MenuP, MenuButtonCaptions, true, MenuButton_Click);
        }

        public void RefreshInfo(YoutuberVideo YoutuberVideo)
        {
            this.YoutuberVideo = YoutuberVideo;
            try { ThumbnailPB.Image = Utils.ScaleImage(new Bitmap(YoutuberVideo.Video.GetImagePath()), ThumbnailPB.Width, ThumbnailPB.Height, true); }
            catch (Exception E) { ThumbnailPB.Image = null; }
            youtuberL.Text = YoutuberVideo.Youtuber.Name;
            videoTitleL.Text = YoutuberVideo.Video.Title;
            uploadedL.Text = string.Format("Uploaded {0} ({1})", Utils.FormatDateTime(YoutuberVideo.Video.Uploaded, Utils.DateTimeFormatA2), Utils.FormatElapsedTime(DateTime.Now.Subtract(YoutuberVideo.Video.Uploaded)));
            durationL.Text = Utils.FormatDuration(YoutuberVideo.Video.Duration);
            ratingL.Text = Utils.FormatNumber(YoutuberVideo.Video.Likes) + " / " + Utils.FormatNumber(YoutuberVideo.Video.Dislikes);
            MyGUIs.DrawLikesDislikes(likesDislikesPB, YoutuberVideo.Video.Likes, YoutuberVideo.Video.Dislikes);
            commentsL.Text = Utils.FormatNumber(YoutuberVideo.Video.Comments);
            viewsL.Text = string.Format("{0} ({1}%)", Utils.FormatNumber(YoutuberVideo.Video.Views), ((double) (YoutuberVideo.Video.Views * 100) / YoutuberVideo.Youtuber.Subscribers).ToString("0.0"));
            MyGUIs.DrawViewsSubscribersPercentage(viewsSubsPB, YoutuberVideo.Video.Views, YoutuberVideo.Youtuber.Subscribers);
            earningsL.Text = Utils.FormatMinMaxEarnings(YoutuberVideo.Video.Views);
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            int r = MenuButtonCaptions.IndexOf(((PictureBoxButton) sender).Caption);
            switch (r)
            {
                case 0: // play
                    this.MainForm.ShowAndFocusFormAndHideTheRest(this.PlayerForm);
                    this.PlayerForm.InitializeWithVideo(this.YoutuberVideo);
                    break;
                case 1: // open webpage
                    System.Diagnostics.Process.Start(YoutuberVideo.Video.GetWebpageVideoURL());
                    break;
                case 2: // open video
                    System.Diagnostics.Process.Start(YoutuberVideo.Video.GetVideoURL());
                    break;
                case 3: // close
                    MainForm.ShowAndFocusFormAndHideTheRest(null);
                    break;
            }
            this.Hide();
        }

        private void ThumbnailPB_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(YoutuberVideo.Video.GetVideoMaxResDefaultThumb());
            this.Hide();
        }

        private void youtuberL_Click(object sender, EventArgs e)
        {
            MainForm.ShowAndFocusFormAndHideTheRest(MainForm.YoutuberForm);
            MainForm.YoutuberForm.RefreshInfo(this.YoutuberVideo.Youtuber);
        }

        private void FVideo_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible && ThumbnailPB.Image != null)
                ThumbnailPB.Image.Dispose();
        }
    }
}
