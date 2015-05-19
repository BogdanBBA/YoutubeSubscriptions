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
using YoutubeSubscriptions.Forms;

namespace YoutubeSubscriptions
{
    public partial class FMain : Form
    {
        public const int ControlPadding = 8;
        private static List<string> MenuButtonCaptions = new List<string>() { "UPDATE ALL", "UPDATE CURRENT", "MORE", "EXIT" };
        private static List<string> MoreMenuButtonCaptions = new List<string>() { "About the app", "Open workspace", "Edit subscriptions", "Edit settings" };
        private static List<string> ModeButtonCaptions = new List<string>() { "Sub list", "Bookmarks", "24H feed" };

        private List<PictureBoxButton> MenuButtons;
        private List<SimpleCheckableButton> ModeButtons;
        public PictureBoxStatusBar StatusBar;

        public FSettings SettingsForm;
        public FAbout AboutForm;
        public FMenu MenuForm;
        public FSubEditor SubEditorForm;
        public FVideo VideoForm;
        public FYoutuber YoutuberForm;
        public FPlayer PlayerForm;
        public FChart ChartForm;

        public TDatabase Database;
        public TSettings Settings;
        public SubscriptionManager SubManager;
        public GeneralVideoManager VideoManager;

        public enum AppModes { Subs, Bookmarks, Feed };
        public AppModes CurrentMode = AppModes.Subs;

        public enum FocusablePanel { SubListP, VideoListP };
        public FocusablePanel FocusedPanel = FocusablePanel.SubListP;

        public FMain()
        {
            InitializeComponent();
            MyGUIs.InitializeAndFormatFormComponents(this);
            this.MouseWheel += new MouseEventHandler(SubscriptionListPanel_Scroll);
            SubListP.MouseEnter += new EventHandler(SubscriptionListPanel_MouseEnter);
            VideoListP.MouseEnter += new EventHandler(VideoListPanel_MouseEnter);
        }

        public void ShowAndFocusFormAndHideTheRest(Form form)
        {
            if (SettingsForm != form)
                SettingsForm.Hide();
            if (AboutForm != form)
                AboutForm.Hide();
            if (MenuForm != form)
                MenuForm.Hide();
            if (SubEditorForm != form)
                SubEditorForm.Hide();
            if (VideoForm != form)
                VideoForm.Hide();
            if (YoutuberForm != form)
                YoutuberForm.Hide();
            if (PlayerForm != form)
                PlayerForm.Hide();
            if (ChartForm != form)
                ChartForm.Hide();
            if (form != null)
            {
                form.Show();
                form.Focus();
            }
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            try
            {
                string assetsCheckResult = Paths.CheckFolders(true);
                if (!assetsCheckResult.Equals(""))
                    throw new ApplicationException(assetsCheckResult);
            }
            catch (Exception E)
            {
                MessageBox.Show("An error occured while verifying (and creating where possible) the folders and other assets.\n\n" + E.ToString(), "Initialization ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            //

            try
            {
                SettingsForm = new FSettings(this);
                AboutForm = new FAbout(this);
                MenuForm = new FMenu(this);
                SubEditorForm = new FSubEditor(this);
                PlayerForm = new FPlayer(this);
                VideoForm = new FVideo(this, PlayerForm);
                YoutuberForm = new FYoutuber(this);
                ChartForm = new FChart(this);
            }
            catch (Exception E)
            {
                MessageBox.Show("An error occured while initializing other forms (in FMain_Load).\n\n" + E.ToString(), "Initialization ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            //

            const int SmallPanelHeight = 24, SubscriptionPanelWidth = 320;

            MenuP.SetBounds(ControlPadding, ControlPadding, this.Width - 2 * ControlPadding, 60);

            StatusP.SetBounds(ControlPadding, MenuP.Bottom + ControlPadding, MenuP.Width, SmallPanelHeight);

            LeftP.SetBounds(ControlPadding, StatusP.Bottom + ControlPadding, SubscriptionPanelWidth, this.Height - MenuP.Height - 2 * SmallPanelHeight - 5 * ControlPadding);
            ModeP.SetBounds(0, 0, LeftP.Width, 45);
            SubSearchP.SetBounds(0, ModeP.Bottom + ControlPadding, LeftP.Width, SubSearchP.Height);
            SubSearchTB.Width = SubSearchP.Width - 2 * SubSearchTB.Left;
            SubListP.SetBounds(0, SubSearchP.Bottom, LeftP.Width, LeftP.Height - SubSearchP.Height - ModeP.Height - ControlPadding);

            VideoListP.SetBounds(LeftP.Right + ControlPadding, StatusP.Bottom + ControlPadding, this.Width - LeftP.Right - 2 * ControlPadding, LeftP.Height);

            GeneralInfoP.SetBounds(ControlPadding, LeftP.Bottom + ControlPadding, StatusP.Width, SmallPanelHeight);
            FilteredSubsInfoL.SetBounds(0, GeneralInfoP.Height / 2 - FilteredSubsInfoL.Height / 2, 2 * GeneralInfoP.Width / 3, FilteredSubsInfoL.Height);
            CurrentDateL.SetBounds(2 * GeneralInfoP.Width / 3, GeneralInfoP.Height / 2 - CurrentDateL.Height / 2, GeneralInfoP.Width / 3, CurrentDateL.Height);
            CurrentDateT.Enabled = true;

            MenuButtons = MyGUIs.CreateMenuButtons(MenuP, MenuButtonCaptions, true, MenuButton_Click);
            ModeButtons = MyGUIs.CreateSimpleCheckableButtons(ModeP, ModeButtonCaptions, true, ModeButton_Click);
            StatusBar = new PictureBoxStatusBar(StatusP, "", "Feel free to browse, update or edit subscriptions, or to play around with the settings!", "", 100);
        }

        private void FMain_Shown(object sender, EventArgs e)
        {
            string loadResult = TDatabase.OpenDatabase(out Database);
            if (!loadResult.Equals(""))
            {
                MessageBox.Show(loadResult, "Database open ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                Settings = new TSettings();
                Settings.ReadFromFile();

                LeftP.Show();
                GeneralInfoP.Show();

                ModeButton_Click(ModeButtons[0], null);
            }
        }

        private void CurrentDateT_Tick(object sender, EventArgs e)
        {
            CurrentDateL.Text = "Current time: " + Utils.FormatDateTime(DateTime.Now, Utils.DateTimeFormatT);
        }

        private void SubSearchTB_TextChanged(object sender, EventArgs e)
        {
            FindSubsT.Enabled = false;
            FindSubsT.Enabled = true;
        }

        private void FindSubsT_Tick(object sender, EventArgs e)
        {
            FindSubsT.Enabled = false;
            SubManager.RecreateSubscriptionBoxes();
            SubManager.SelectedSubBox = SubManager.SubscriptionBoxes.Count > 0 ? SubManager.SubscriptionBoxes[0] : null;
            SubscriptionBox_Click(SubManager.SelectedSubBox, null);
        }

        private void ModeButton_Click(object sender, EventArgs e)
        {
            foreach (SimpleCheckableButton scb in ModeButtons)
                if (!scb.Information.Equals((sender as SimpleCheckableButton).Information))
                    scb.SetCheckedAndRedraw(false);
                else
                {
                    scb.SetCheckedAndRedraw(true);
                    SimpleCheckableButton.DrawSimpleCheckableButton(scb, e != null);
                }
            CurrentMode = ModeButtons[0].Checked ? AppModes.Subs : (ModeButtons[1].Checked ? AppModes.Bookmarks : AppModes.Feed);
            SubSearchP.Visible = CurrentMode == AppModes.Subs;
            SubListP.Visible = SubSearchP.Visible;

            if (VideoManager == null)
                VideoManager = new GeneralVideoManager(VideoListP, VideoBox_Click, VideoListPanel_MouseEnter, new List<YoutuberVideo>(), this.Settings);

            switch (CurrentMode)
            {
                case AppModes.Subs: // subscription list
                    if (SubManager == null)
                        SubManager = new SubscriptionManager(Database, SubListP, SubscriptionBox_Click, SubscriptionListPanel_MouseEnter);
                    SubSearchTB.Text = "";
                    SubSearchTB_TextChanged(null, null); // will trigger the FindSubsT timer, which on tick will call SubManager.RecreateSubscriptionBoxes()
                    break;
                case AppModes.Bookmarks: // bookmarks
                    VideoManager.Videos = Database.GetBookmarkedVideosAsYoutuberVideos();
                    VideoManager.RecreateVideoBoxes(this.Settings);
                    StatusBar.UpdateStatus("Bookmarks:", Utils.RegularPlural("video", VideoManager.Videos.Count, true), "", 100);
                    FilteredSubsInfoL.Text = "";
                    break;
                case AppModes.Feed: // 24h feed
                    long nYoutubers, nViews;
                    VideoManager.Videos = Database.Get24HourFeed(out nYoutubers, out nViews);
                    VideoManager.RecreateVideoBoxes(this.Settings);
                    StatusBar.UpdateStatus("24-hour video feed:", Utils.RegularPlural("video", VideoManager.Videos.Count, true),
                        string.Format("{0} with {1}", Utils.RegularPlural("subscription", nYoutubers, true), Utils.RegularPlural("view", nViews, true)), 100);
                    FilteredSubsInfoL.Text = "";
                    break;
            }
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            int r = MenuButtonCaptions.IndexOf(((PictureBoxButton) sender).Caption);
            switch (r)
            {
                case 0: // update all
                    ShowAndFocusFormAndHideTheRest(null);
                    if (SubManager.GetWorkingThreads() > 0)
                    {
                        MessageBox.Show("Some threads (" + SubManager.GetWorkingThreads() + ") are still working! Please wait until they finish to exit.", "Have patience", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    int nVisible = 0;
                    foreach (SubscriptionBox sb in SubManager.SubscriptionBoxes)
                        if (sb.Visible)
                            nVisible++;
                    if (nVisible < Database.Youtubers.Count)
                    {
                        MessageBox.Show("Please have all your subscriptions visible when updating! This is a buggy situation that may or may not be fixed soon.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    StatusBar.UpdateStatus("Updating all subscriptions...", "", "", 0);
                    SubManager.UpdateAllSubscriptions();
                    UpdateAllTimer.Enabled = true;
                    break;
                case 1: // update current					
                    if (CurrentMode != AppModes.Subs)
                    {
                        MessageBox.Show("Please switch to \"Sub list\" mode in order to update your subscription.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    ShowAndFocusFormAndHideTheRest(null);
                    if (SubManager.SelectedSubBox == null)
                    {
                        MessageBox.Show("You do not have a subscription selected!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (SubManager.SelectedSubBox.CurrentStatus == SubscriptionBox.StatusPreparing || SubManager.SelectedSubBox.CurrentStatus == SubscriptionBox.StatusUpdating)
                        return;
                    StatusBar.UpdateStatus(SubManager.SelectedSubBox.Youtuber.Name + ":", "Updating subscription...", "", 0);
                    SubscriptionBox.DrawSubscriptionBox(SubManager.SelectedSubBox, false, "Updating...");
                    SubManager.SelectedSubBox.UpdateThread.RunWorkerAsync();
                    UpdateCurrentTimer.Enabled = true;
                    break;
                case 2: // more
                    MenuForm.RefreshMenuForm(MoreMenuButtonCaptions, MenuForm.MainFormMenu_More_Click, Cursor.Position);
                    break;
                case 3: // exit	
                    ShowAndFocusFormAndHideTheRest(null);
                    if (SubManager.GetWorkingThreads() > 0)
                    {
                        MessageBox.Show("Some threads (" + SubManager.GetWorkingThreads() + ") are still working! Please wait until they finish to exit.", "Have patience", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    SettingsForm.Dispose();
                    SubEditorForm.Dispose();
                    VideoForm.Dispose();
                    YoutuberForm.Dispose();
                    PlayerForm.Dispose();
                    Application.Exit();
                    break;
            }
        }

        private void UpdateAllTimer_Tick(object sender, EventArgs e)
        {
            int total = SubManager.Database.Youtubers.Count, nDone = total - SubManager.GetWorkingThreads();
            if (nDone < total)
                StatusBar.UpdateStatus("Updating subscriptions:", string.Format("{0}/{1} completed...", nDone, total), "", (nDone * 100) / total);
            else
            {
                UpdateAllTimer.Enabled = false;
                if (SubManager.GetThreadsWithStatus(SubscriptionBox.StatusDoneFAIL) == 0)
                    StatusBar.UpdateStatus("", string.Format("All {0} subscriptions have been updated!", total), "", 100);
                else
                    StatusBar.UpdateStatus("", string.Format("Only {0} of {1} subscriptions have been updated.", SubManager.GetThreadsWithStatus(SubscriptionBox.StatusDoneOK), total), "", 100);
                SubManager.RecreateSubscriptionBoxes();
                SubscriptionBox_Click(SubManager.SelectedSubBox, null);
                SubscriptionBox.DrawSubscriptionBox(SubManager.SelectedSubBox, false, null);
                //
                string saveResult = TDatabase.SaveDatabase(Database);
                if (!saveResult.Equals(""))
                    MessageBox.Show(saveResult, "Database save ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCurrentTimer_Tick(object sender, EventArgs e)
        {
            if (SubManager.SelectedSubBox.CurrentStatus == SubscriptionBox.StatusPreparing || SubManager.SelectedSubBox.CurrentStatus == SubscriptionBox.StatusUpdating)
                StatusBar.UpdateStatus(SubManager.SelectedSubBox.Youtuber.Name + ":", "Updating subscription...", "", 50);
            else
            {
                UpdateCurrentTimer.Enabled = false;
                if (SubManager.SelectedSubBox.CurrentStatus == SubscriptionBox.StatusDoneFAIL)
                    MessageBox.Show("Failed to update subscription to " + SubManager.SelectedSubBox.Youtuber.Name + " :( .", "Update ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                string saveResult = TDatabase.SaveDatabase(Database);
                if (!saveResult.Equals(""))
                    MessageBox.Show(saveResult, "Database save ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SubscriptionBox_Click(object sender, EventArgs e)
        {
            if (SubManager.GetWorkingThreads() > 0)
                return;
            SubscriptionBox clickedSB = sender as SubscriptionBox;
            if (e == null || ((MouseEventArgs) e).Button == MouseButtons.Left)
            {
                SubManager.SelectedSubBox = clickedSB;
                foreach (SubscriptionBox sb in SubManager.SubscriptionBoxes)
                    if (sb.Visible)
                    {
                        sb.Checked = sb.Youtuber.Equals(clickedSB.Youtuber);
                        SubscriptionBox.DrawSubscriptionBox(sb, sb.Checked && e != null, null);
                    }
                VideoManager.Videos = clickedSB.Youtuber.GetVideosAsYoutuberVideos();
                if (VideoManager.Videos.Count > 0)
                    StatusBar.UpdateStatus(clickedSB.Youtuber.Name + ":",
                        string.Format("info last updated {0}", Utils.FormatElapsedTime(DateTime.Now.Subtract(clickedSB.Youtuber.InfoLastUpdated))),
                        string.Format("{0} with {1} and {2}",
                            Utils.RegularPlural("recent video", clickedSB.Youtuber.Videos.Count, true),
                            Utils.RegularPlural("view", clickedSB.Youtuber.CurrentVideoViews(), true),
                            Utils.RegularPlural("subscriber", clickedSB.Youtuber.Subscribers, true)), 100);
                else
                    StatusBar.UpdateStatus(clickedSB.Youtuber.Name + ":",
                        string.Format("info last updated {0}", Utils.FormatElapsedTime(DateTime.Now.Subtract(clickedSB.Youtuber.InfoLastUpdated))),
                        string.Format("no recent videos / {0}", Utils.RegularPlural("subscriber", clickedSB.Youtuber.Subscribers, true)), 100);
                VideoManager.RecreateVideoBoxes(this.Settings);
            }
            else
            {
                ShowAndFocusFormAndHideTheRest(YoutuberForm);
                YoutuberForm.RefreshInfo(clickedSB.Youtuber);
            }
        }

        private void VideoBox_Click(object sender, EventArgs e)
        {
            if (SubManager.GetWorkingThreads() > 0)
                return;
            VideoBox vidBox = sender as VideoBox;
            if ((e as MouseEventArgs).Button == MouseButtons.Left) // left click, open video form
            {
                ShowAndFocusFormAndHideTheRest(VideoForm);
                VideoForm.RefreshInfo(vidBox.YoutuberVideo);
            }
            else // right click, (un-)bookmark video
            {
                YoutuberVideo ytVid = vidBox.YoutuberVideo;
                int poz = Database.BookmarkedVideoIndexByVideoID(ytVid.Video.ID);
                if (poz != -1)
                    Database.BookmarkedVideos.RemoveAt(poz);
                else
                {
                    VideoWithYoutuberID vID = new VideoWithYoutuberID();
                    vID.YoutuberID = ytVid.Youtuber.ID;
                    vID.Video = ytVid.Video;
                    Database.BookmarkedVideos.Add(vID);
                }
                VideoBox.DrawVideoBox(vidBox, true);
                string saveResult = TDatabase.SaveDatabase(Database);
                if (!saveResult.Equals(""))
                    MessageBox.Show(saveResult, "Database save ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SubscriptionListPanel_MouseEnter(object sender, EventArgs e)
        {
            FocusedPanel = FocusablePanel.SubListP;
        }

        private void VideoListPanel_MouseEnter(object sender, EventArgs e)
        {
            FocusedPanel = FocusablePanel.VideoListP;
        }

        private static void Panel_MouseScroll(Panel panel, int amount)
        {
            using (Control c = new Control() { Parent = panel, Height = 1, Top = (amount > 0 ? panel.ClientSize.Height : 0) + amount })
            {
                panel.ScrollControlIntoView(c);
            }
        }

        private void SubscriptionListPanel_Scroll(object sender, MouseEventArgs e)
        {
            int extraAmount = Math.Sign(e.Delta) == -1 ? -1 : 0;
            if (FocusedPanel == FocusablePanel.SubListP)
            {
                if (!(e.Delta < 0 && SubListP.VerticalScroll.Value + SubListP.Height >= SubListP.VerticalScroll.Maximum))
                    Panel_MouseScroll(SubListP, -(e.Delta / 120) * SubscriptionBox.boxHeight + extraAmount);
            }
            else
            {
                if (!(e.Delta < 0 && VideoListP.VerticalScroll.Value + VideoListP.Height >= VideoListP.VerticalScroll.Maximum))
                    Panel_MouseScroll(VideoListP, -(e.Delta / 120) * (VideoBox.videoBoxSize.Height / 2 + FMain.ControlPadding / 2) + extraAmount);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                SubSearchTB.Focus();
                SubSearchTB.SelectAll();
                return true;
            }
            return false;
        }
    }
}
