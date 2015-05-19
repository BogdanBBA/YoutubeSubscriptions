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
    public partial class FAbout : Form
    {
        private List<PictureBoxButton> MenuButtons;
        private FMain MainForm;

        public FAbout(FMain MainForm)
        {
            InitializeComponent();
            this.MainForm = MainForm;
            MyGUIs.InitializeAndFormatFormComponents(this);
        }

        private void FAbout_Load(object sender, EventArgs e)
        {
            MenuButtons = MyGUIs.CreateMenuButtons(MenuP, new List<string>() { "Close" }, true, MenuButton_Click);
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            this.MainForm.ShowAndFocusFormAndHideTheRest(null);
        }
    }
}
