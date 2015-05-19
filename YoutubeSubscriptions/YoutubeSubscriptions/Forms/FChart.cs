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
    public partial class FChart : Form
    {
        private static List<string> MenuButtonCaptions = new List<string>(new string[] { "Back", "CLOSE" });
        private List<PictureBoxButton> MenuButtons;

        private FMain MainForm;

        private Form previousForm;
        private MyChartItemList myChartItems;
        public MyChart MyChartCh;

        public FChart(FMain MainForm)
        {
            InitializeComponent();
            this.MainForm = MainForm;
            MyGUIs.InitializeAndFormatFormComponents(this);
        }

        private void FChart_Load(object sender, EventArgs e)
        {
            MenuButtons = MyGUIs.CreateMenuButtons(MenuP, MenuButtonCaptions, true, MenuButton_Click);
            this.MyChartCh = new MyChart(ChartP, new Rectangle(0, 0, ChartP.Width, ChartP.Height), null, null);
            this.MyChartCh.Title.FontFormatting = new FontFormatting(MyGUIs.GetFont("Segoe UI", 16, true), Color.Orange);
            this.MyChartCh.Subtitle.FontFormatting = new FontFormatting(MyGUIs.GetFont("Segoe UI", 13, false), Color.WhiteSmoke);
            this.MyChartCh.YAxis.FontFormatting.Color = Color.DarkGray;
            this.MyChartCh.XAxis.FontFormatting.Color = Color.DarkGray;
            MyChartAux.BackgroundColor = this.BackColor;
            MyChartAux.AxisPen = new Pen(Color.WhiteSmoke, 4);
            MyChartAux.AxisParalelPen = new Pen(ColorTranslator.FromHtml("#3D3D3D"), 1);
        }

        public void RefreshInfo(Form previousForm, MyChartItemList myChartItems)
        {
            this.previousForm = previousForm;
            this.myChartItems = myChartItems;
            this.MyChartCh.RedrawChart(this.myChartItems);
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            int r = MenuButtonCaptions.IndexOf(((PictureBoxButton) sender).Caption);
            switch (r)
            {
                case 0: // back
                    this.MainForm.ShowAndFocusFormAndHideTheRest(this.previousForm);
                    break;
                case 1: // close
                    this.MainForm.ShowAndFocusFormAndHideTheRest(null);
                    break;
            }
        }

        private void FChart_VisibleChanged(object sender, EventArgs e)
        {
            
        }
    }
}
