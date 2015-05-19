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
    public partial class FSettings : Form
    {
        private static List<string> MenuButtonCaptions = new List<string>(new string[1] { "Save and close" });

        private List<PictureBoxButton> MenuButtons;
        private FMain MainForm;

        public FSettings(FMain MainForm)
        {
            InitializeComponent();
            this.MainForm = MainForm;
            MyGUIs.InitializeAndFormatFormComponents(this);
        }

        private void FSettings_Load(object sender, EventArgs e)
        {
            MenuButtons = MyGUIs.CreateMenuButtons(MenuP, MenuButtonCaptions, true, MenuButton_Click);
            DateTime exampleDateTime = DateTime.Now.AddSeconds(-5);
            uploaderInfoTypeCB.Items.AddRange(TSettings.UploaderInformationTypes);
            for (int i = 0; i < TSettings.UploadedFormats.Length; i++)
                dateFormat.Items.Add(Utils.FormatDateTimeByFormatIndex(exampleDateTime, i));
            subBoxSubtitleCB.Items.AddRange(TSettings.SubBoxSubtitleCaptions);
            qualityCB.Items.AddRange(TSettings.PlaybackQualityCaptions);
            RefreshSettings();
        }

        private void RefreshSettings()
        {
            TSettings sett = MainForm.Settings;

            compactSubBoxChB.Checked = sett.CompactSubscriptionBox;
            subBoxSubtitleCB.SelectedIndex = sett.SubscriptionBoxSubtitleIndex;

            videoColsNUD.Value = sett.VideoColumns;
            uploaderInfoTypeCB.SelectedIndex = sett.UploaderInformationTypeIndex;
            dateFormat.SelectedIndex = sett.UploadedFormatIndex;
            showVideoUploaderChB.Checked = sett.ShowVideoUploader;
            showLengthOnThumbChB.Checked = sett.ShowVideoLengthOnThumb;
            showVideoTitleChB.Checked = sett.ShowVideoTitle;
            showVideoStatsChB.Checked = sett.ShowVideoStats;

            daysNUD.Value = sett.VideoDaysGoBack;
            downloadThumbnailsAnywayChB.Checked = sett.DownloadThumbnailsAnyway;

            volumeTrB.Value = sett.DefaultVolume;
            startFullWindowChB.Checked = sett.StartFullWindow;
            qualityCB.SelectedIndex = sett.DefaultQualityIndex;
            playbackRateNUD.Value = (decimal) sett.DefaultPlaybackRate;
            skipSecondsNUD.Value = sett.SkipSeconds;

            CheckBox_CheckedChanged(showVideoTitleChB, null);
            CheckBox_CheckedChanged(showVideoStatsChB, null);
            CheckBox_CheckedChanged(downloadThumbnailsAnywayChB, null);
            CheckBox_CheckedChanged(showLengthOnThumbChB, null);
            CheckBox_CheckedChanged(showVideoUploaderChB, null);
            CheckBox_CheckedChanged(compactSubBoxChB, null);
            CheckBox_CheckedChanged(startFullWindowChB, null);
            volumeTrB_Scroll(null, null);
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            int r = MenuButtonCaptions.IndexOf(((PictureBoxButton) sender).Caption);
            switch (r)
            {
                case 0: // save and close
                    // get settings
                    TSettings sett = MainForm.Settings;

                    sett.CompactSubscriptionBox = compactSubBoxChB.Checked;
                    sett.SubscriptionBoxSubtitleIndex = subBoxSubtitleCB.SelectedIndex;

                    sett.VideoColumns = (int) videoColsNUD.Value;
                    sett.UploaderInformationTypeIndex = uploaderInfoTypeCB.SelectedIndex;
                    sett.UploadedFormatIndex = dateFormat.SelectedIndex;
                    sett.ShowVideoUploader = showVideoUploaderChB.Checked;
                    sett.ShowVideoLengthOnThumb = showLengthOnThumbChB.Checked;
                    sett.ShowVideoTitle = showVideoTitleChB.Checked;
                    sett.ShowVideoStats = showVideoStatsChB.Checked;

                    sett.VideoDaysGoBack = (int) daysNUD.Value;
                    sett.DownloadThumbnailsAnyway = downloadThumbnailsAnywayChB.Checked;

                    sett.DefaultVolume = volumeTrB.Value;
                    sett.StartFullWindow = startFullWindowChB.Checked;
                    sett.DefaultQualityIndex = qualityCB.SelectedIndex;
                    sett.DefaultPlaybackRate = (double) playbackRateNUD.Value;
                    sett.SkipSeconds = (int) skipSecondsNUD.Value;

                    // actual save and close
                    this.MainForm.ShowAndFocusFormAndHideTheRest(null);
                    sett.SaveToFile();

                    if (MainForm.CurrentMode == FMain.AppModes.Subs)
                        MainForm.SubManager.RecreateSubscriptionBoxes();
                    MainForm.VideoManager.RecreateVideoBoxes(MainForm.Settings);
                    MainForm.Focus();
                    break;
                default: // something else
                    MessageBox.Show("Not implemented yet, WTF?!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void daysNUD_ValueChanged(object sender, EventArgs e)
        {
            if (daysNUD.Value > daysNUD.Maximum)
                daysNUD.Value = daysNUD.Maximum;
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            (sender as CheckBox).Text = (sender as CheckBox).Checked ? "Yes" : "No";
        }

        private void volumeTrB_Scroll(object sender, EventArgs e)
        {
            volumeL.Text = "Default volume (" + volumeTrB.Value + "%)";
        }
    }
}
