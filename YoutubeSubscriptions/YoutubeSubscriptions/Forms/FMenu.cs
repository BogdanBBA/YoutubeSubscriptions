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
    public partial class FMenu : Form
    {
        private FMain MainForm;

        private List<string> MenuButtonCaptions;
        private List<PictureBoxButton> MenuButtons;
        private BorderPictureBox BorderPB;

        public FMenu(FMain mainForm)
        {
            InitializeComponent();
            this.MainForm = mainForm;
            MyGUIs.InitializeAndFormatFormComponents(this);
            //
            MenuButtonCaptions = new List<string>();
            MenuButtons = new List<PictureBoxButton>();
            BorderPB = new BorderPictureBox(this);
        }

        private void FMenu_Load(object sender, EventArgs e)
        {
        }

        public void RefreshMenuForm(List<string> captions, EventHandler menuButton_Click_Event, Point location)
        {
            MenuButtonCaptions.Clear();
            MenuButtonCaptions.AddRange(captions);
            MenuButtonCaptions.Add("CLOSE");
            //
            this.Location = location;
            this.Size = new Size(300 + 2 * BorderPB.BorderWidth, MenuButtonCaptions.Count * 45 + 2 * BorderPB.BorderWidth);
            BorderPB.SetBounds(0, 0, this.Width, this.Height);
            BorderPB.RedrawBorder();
            //
            for (int i = MenuButtonCaptions.Count; i < MenuButtons.Count; i++)
                MenuButtons[i].Hide();
            //
            for (int i = 0; i < MenuButtonCaptions.Count; i++)
            {
                if (i >= MenuButtons.Count)
                {
                    PictureBoxButton butt = new PictureBoxButton("null", null);
                    butt.Parent = this;
                    butt.SetBounds(BorderPB.BorderWidth, BorderPB.BorderWidth + i * 45, 300, 45);
                    MenuButtons.Add(butt);
                }
                MenuButtons[i].SetOnClickEventHandler(menuButton_Click_Event);
                MenuButtons[i].Caption = MenuButtonCaptions[i];
                PictureBoxButton.DrawPictureBoxButton(MenuButtons[i], false);
                MenuButtons[i].Show();
            }
            BorderPB.SendToBack();
            //
            this.Show();
        }

        //
        //
        //

        public void MainFormMenu_More_Click(object sender, EventArgs e)
        {
            int r = MenuButtonCaptions.IndexOf((sender as PictureBoxButton).Caption);
            this.MainForm.ShowAndFocusFormAndHideTheRest(null);
            if (r != MenuButtonCaptions.Count - 1)
                switch (r)
                {
                    case 0: // about
                        this.MainForm.ShowAndFocusFormAndHideTheRest(this.MainForm.AboutForm);
                        break;
                    case 1: // open workspace
                        System.Diagnostics.Process.Start(Paths.ProgramFilesFolder);
                        break;
                    case 2: // edit subs
                        this.MainForm.ShowAndFocusFormAndHideTheRest(this.MainForm.SubEditorForm);
                        break;
                    case 3: // settings/about 
                        this.MainForm.ShowAndFocusFormAndHideTheRest(this.MainForm.SettingsForm);
                        break;
                    default:
                        MessageBox.Show("Invalid menu button caption :\\", "Weird", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
        }
    }
}
