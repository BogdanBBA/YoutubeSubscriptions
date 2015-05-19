using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeSubscriptions.Classes;

namespace YoutubeSubscriptions.Forms
{
    public partial class FYoutuber : Form
    {
        private static List<string> MenuButtonCaptions = new List<string>(new string[] { "Profile »", "Videos »", "View history", "CLOSE" });
        private List<PictureBoxButton> MenuButtons;

        private FMain MainForm;

        private TYoutuber Youtuber;

        public FYoutuber(FMain MainForm)
        {
            InitializeComponent();
            this.MainForm = MainForm;
            MyGUIs.InitializeAndFormatFormComponents(this);
        }

        private void FYoutuber_Load(object sender, EventArgs e)
        {
            MenuButtons = MyGUIs.CreateMenuButtons(MenuP, MenuButtonCaptions, true, MenuButton_Click);
        }

        public void RefreshInfo(TYoutuber Youtuber)
        {
            this.Youtuber = Youtuber;
            try { ThumbnailPB.Image = Utils.ScaleImage(new Bitmap(Youtuber.GetImagePath()), ThumbnailPB.Width, ThumbnailPB.Height, true); }
            catch (Exception E) { ThumbnailPB.Image = null; }
            ytNameL.Text = Youtuber.Name;
            ytIDL.Text = Youtuber.ID;
            ytInfoLUL.Text = string.Format("Info last updated {0} ({1})",
                Utils.FormatDateTime(Youtuber.InfoLastUpdated, Utils.DateTimeFormatA), Utils.FormatElapsedTime(DateTime.Now.Subtract(Youtuber.InfoLastUpdated)));
            joinedL.Text = string.Format("{0}",
                Utils.FormatDateTime(Youtuber.Joined, Utils.DateTimeFormatD)/*, Utils.FormatElapsedTime(DateTime.Now.Subtract(Youtuber.Joined))*/);
            subsL.Text = Utils.FormatNumber(Youtuber.Subscribers);

            recVidL.Text = Utils.FormatNumber(Youtuber.Videos.Count);
            recViewsL.Text = Utils.FormatNumber(Youtuber.CurrentVideoViews());
            recEarningsL.Text = Utils.FormatMinMaxEarnings(Youtuber.CurrentVideoViews());
            recAvgViewsL.Text = Utils.FormatNumber(Youtuber.Videos.Count > 0 ? Youtuber.CurrentVideoViews() / Youtuber.Videos.Count : 0);
            recAvgEarningsL.Text = Utils.FormatMinMaxEarnings(Youtuber.Videos.Count > 0 ? Youtuber.CurrentVideoViews() / Youtuber.Videos.Count : 0);

            totVidL.Text = Utils.FormatNumber(Youtuber.TotalVideos);
            totViewsL.Text = Utils.FormatNumber(Youtuber.TotalViews);
            totEarningsL.Text = Utils.FormatMinMaxEarnings(Youtuber.TotalViews);
            totAvgViewsL.Text = Utils.FormatNumber(Youtuber.TotalVideos > 0 ? Youtuber.TotalViews / Youtuber.TotalVideos : 0);
            totAvgEarningsL.Text = Utils.FormatMinMaxEarnings(Youtuber.TotalVideos > 0 ? Youtuber.TotalViews / Youtuber.TotalVideos : 0);

        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            int r = MenuButtonCaptions.IndexOf(((PictureBoxButton) sender).Caption);
            switch (r)
            {
                case 0: // open profile
                    System.Diagnostics.Process.Start(this.Youtuber.GetProfileAddress());
                    break;
                case 1: // open videos
                    System.Diagnostics.Process.Start(this.Youtuber.GetVideosAddress());
                    break;
                case 2: // view history
                    if (this.Youtuber.Videos.Count > 0)
                    {
                        MainForm.ShowAndFocusFormAndHideTheRest(MainForm.ChartForm);
                        MyChartItemList items = new MyChartItemList();
                        foreach (TVideo video in this.Youtuber.Videos)
                            items.Add(new MyChartItem(items.Count, video.Views, video));
                        MainForm.ChartForm.MyChartCh.XAxis.IntervalCount = this.Youtuber.Videos.Count;
                        MainForm.ChartForm.MyChartCh.XAxis.NumberFormat = MyChart.FormatDate;
                        MainForm.ChartForm.MyChartCh.YAxis.NumberFormat = MyChart.FormatViews;
                        MainForm.ChartForm.MyChartCh.Title.Caption = this.Youtuber.Name + " / video view history";
                        MainForm.ChartForm.MyChartCh.Subtitle.Caption = string.Format("{0} / {1} between {2} - {3}",
                            Utils.RegularPlural("video", this.Youtuber.Videos.Count, true),
                            Utils.RegularPlural("view", this.Youtuber.CurrentVideoViews(), true),
                            Utils.FormatDateTime(this.Youtuber.Videos.First().Uploaded, Utils.DateTimeFormatD),
                            Utils.FormatDateTime(this.Youtuber.Videos.Last().Uploaded, Utils.DateTimeFormatD));
                        MainForm.ChartForm.RefreshInfo(this, items);
                    }
                    else
                        MessageBox.Show("There's nothing to show!\n\nThe youtuber hasn't got any videos.", "No videos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 3: // close
                    MainForm.ShowAndFocusFormAndHideTheRest(null);
                    break;
            }
            this.Hide();
        }

        private void ThumbnailPB_Click(object sender, EventArgs e)
        {
            if (File.Exists(Youtuber.GetImagePath()))
                System.Diagnostics.Process.Start(Youtuber.GetImagePath());
            else
                MessageBox.Show("The image at \"" + Youtuber.GetImagePath() + "\" does not exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.Hide();
        }

        private void FYoutuber_VisibleChanged(object sender, EventArgs e)
        {
            /*if (!this.Visible && ThumbnailPB.Image != null)
                ThumbnailPB.Image.Dispose();*/
        }
    }
}
