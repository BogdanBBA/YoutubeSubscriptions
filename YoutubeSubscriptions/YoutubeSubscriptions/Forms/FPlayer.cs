using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using YoutubeSubscriptions.Classes;

namespace YoutubeSubscriptions.Forms
{
    public partial class FPlayer : Form
    {
        private const int ControlPadding = 8, StatusBarHeight = 60;

        private static List<string> MenuButtonCaptions = new List<string>(new string[] { "Play/pause", "Fill window", "Stop and close" });
        private List<PictureBoxButton> MenuButtons;
        private VideoTitlePB VideoTitle;

        private FMain MainForm;

        private bool youtubePlayerReady;
        private bool currentlyPlaying;
        private bool seekedAhead;
        private bool currentlyFullscreen = false;

        public FPlayer(FMain MainForm)
        {
            InitializeComponent();
            this.MainForm = MainForm;
            this.VideoTitle = new VideoTitlePB(TitleP);
            MyGUIs.InitializeAndFormatFormComponents(this);
        }

        private void FPlayer_Load(object sender, EventArgs e)
        {
            MenuButtons = MyGUIs.CreateMenuButtons(MenuP, MenuButtonCaptions, true, MenuButton_Click);
            this.youtubePlayerReady = false;
            this.PlayerSF.FlashCall += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEventHandler(this.PlayerSF_FlashCall);
        }

        private void FPlayer_Resize(object sender, EventArgs e)
        {
            TitleP.SetBounds(ControlPadding, ControlPadding, this.Width - 2 * ControlPadding, 40);
            MenuP.SetBounds(this.Width / 2 - (MenuButtonCaptions.Count * 300) / 2, this.Height - ControlPadding - 60 - 30, MenuButtonCaptions.Count * 300, 60);
            StatusP.SetBounds(TitleP.Left, MenuP.Top - ControlPadding - StatusBarHeight, TitleP.Width, StatusBarHeight);
            ResizePlayerSF();
        }

        private void ResizePlayerSF()
        {
            PlayerSF.SetBounds(TitleP.Left, TitleP.Bottom + ControlPadding, TitleP.Width, StatusP.Top - TitleP.Height - 3 * ControlPadding);
        }

        public void InitializeWithVideo(YoutuberVideo yVideo)
        {
            this.VideoTitle.SetBounds(0, 0, TitleP.Width, TitleP.Height);
            this.VideoTitle.RefreshForYoutuberVideo(yVideo);
            PlayerSF.Movie = yVideo.Video.GetVideoURL() + @"?version=3&enablejsapi=1";
            this.PlayerSF.BringToFront();
            this.MenuP.BringToFront();
        }

        private void PlayerSF_FlashCall(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent e)
        {
            // message is in xml format so we need to parse it
            XmlDocument document = new XmlDocument();
            document.LoadXml(e.request);
            // get attributes to see which command flash is trying to call
            XmlAttributeCollection attributes = document.FirstChild.Attributes;
            String command = attributes.Item(0).InnerText;
            // get parameters
            XmlNodeList list = document.GetElementsByTagName("arguments");
            List<string> listS = new List<string>();
            foreach (XmlNode l in list)
                listS.Add(l.InnerText);
            // Interpret command
            //UpdateStatus("PlayerSF_FlashCall command: " + command);
            switch (command)
            {
                case "onYouTubePlayerReady":
                    YTready(listS[0]);
                    break;
                case "YTStateChange":
                    YTStateChange(listS[0]);
                    break;
                case "YTError":
                    YTStateError(listS[0]);
                    break;
            }
        }

        private string PlayerSF_CallFlash(string ytFunction)
        {
            string flashXMLrequest = "";
            string flashFunction = "";
            List<string> flashFunctionArgs = new List<string>();

            Regex func2xml = new Regex(@"([a-z][a-z0-9]*)(\(([^)]*)\))?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Match fmatch = func2xml.Match(ytFunction);

            if (fmatch.Captures.Count != 1)
                return "";

            flashFunction = fmatch.Groups[1].Value.ToString();
            flashXMLrequest = "<invoke name=\"" + flashFunction + "\" returntype=\"xml\">";
            if (fmatch.Groups[3].Value.Length > 0)
            {
                flashFunctionArgs = Utils.ParseDelimitedString(fmatch.Groups[3].Value);
                if (flashFunctionArgs.Count > 0)
                {
                    flashXMLrequest += "<arguments><string>";
                    flashXMLrequest += string.Join("</string><string>", flashFunctionArgs);
                    flashXMLrequest += "</string></arguments>";
                }
            }
            flashXMLrequest += "</invoke>";

            try { return PlayerSF.CallFunction(flashXMLrequest); }
            catch { return ""; }
        }

        private void YTready(string playerID)
        {
            youtubePlayerReady = true;
            //start eventHandlers
            PlayerSF_CallFlash("addEventListener(\"onStateChange\",\"YTStateChange\")");
            PlayerSF_CallFlash("addEventListener(\"onError\",\"YTError\")");
        }

        private void YTStateChange(string yPlayState)
        {
            //UpdateStatus("YTStateChange yPlayState: " + yPlayState);
            /*
             * https://developers.google.com/youtube/js_api_reference?csw=1#Playback_controls
             * player.playVideo():Void
             * player.seekTo(seconds:Number, allowSeekAhead:Boolean):Void
             * player.setVolume(volume:Number):Void
             * player.setPlaybackRate(suggestedRate:Number):Void
             * player.getCurrentTime():Number
             * player.setPlaybackQuality(suggestedQuality:String):Void
             */
            TSettings sett = (Application.OpenForms[0] as FMain).Settings;
            switch (int.Parse(yPlayState))
            {
                case -1: // not started yet; this is where we tell it to start
                    currentlyPlaying = false;
                    seekedAhead = false;
                    PlayerSF_CallFlash("setPlaybackQuality(" + TSettings.PlaybackQualities[sett.DefaultQualityIndex] + ")");
                    PlayerSF_CallFlash("setPlaybackRate(" + sett.DefaultPlaybackRate + ")");
                    PlayerSF_CallFlash("playVideo()");
                    break;
                case 0: // ended
                    currentlyPlaying = false;
                    UpdateStatus("Video has ended");
                    // if (!loopFile) mediaNext(); else PlayerSF_CallFlash("seekTo(0)");
                    break;
                case 1: // playing
                    currentlyPlaying = true;
                    UpdateStatus("Playing video...");
                    if (sett.StartFullWindow && !this.currentlyFullscreen)
                        MenuButton_Click(MenuButtons[1], null); // check with MenuButton_Click 
                    if (!seekedAhead)
                    {
                        PlayerSF_CallFlash("setVolume(" + sett.DefaultVolume + ")");
                        PlayerSF_CallFlash("seekTo(" + sett.SkipSeconds + ", true)");
                        seekedAhead = true;
                    }
                    break;
                case 2: // paused
                    currentlyPlaying = false;
                    UpdateStatus("Video paused");
                    break;
                case 3: // buffering
                    break;
            }
        }
        private void YTStateError(string error)
        {
            MessageBox.Show("YTStateError error:\n\n" + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            int r = MenuButtonCaptions.IndexOf(((PictureBoxButton) sender).Caption);
            switch (r)
            {
                case 0: // play/pause
                    PlayerSF_CallFlash(this.currentlyPlaying ? "pauseVideo()" : "playVideo()");
                    break;
                case 1: // fullscreen; also check with YTStateChange in case -1
                    if (!this.currentlyFullscreen)
                        this.PlayerSF.SetBounds(0, 0, this.Width, this.Height);
                    else
                        ResizePlayerSF();
                    this.currentlyFullscreen = !this.currentlyFullscreen;
                    break;
                case 2: // close
                    PlayerSF_CallFlash("pauseVideo()");
                    this.MainForm.ShowAndFocusFormAndHideTheRest(null);
                    break;
            }
        }

        private void UpdateStatus(string text)
        {
            StatusL.Text = text;
        }
    }
}
