using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubeSubscriptions.Classes
{
    public static class MyGUIs
    {
        public static Color FormBackgroundC = ColorTranslator.FromHtml("#100E10");
        public static Color ButtonC = ColorTranslator.FromHtml("#100E10");
        public static Color SelectedButtonC = ColorTranslator.FromHtml("#181618");
        public static Color FontC = ColorTranslator.FromHtml("#FFFFFF");
        public static Color SelectedFontC = Color.Orange;

        public static Font GetFont(string name, int size, bool bold)
        {
            return new Font(name, size, bold ? FontStyle.Bold : FontStyle.Regular);
        }

        public static void InitializeAndFormatFormComponents(Form form)
        {
            form.BackColor = FormBackgroundC;
            foreach (Control control in form.Controls)
                InitializeAndFormatControlComponents(control);
        }

        private static void InitializeAndFormatControlComponents(Control control)
        {
            if (control is Label || control is PictureBox || control is Panel)
                control.BackColor = FormBackgroundC;
            foreach (Control subControl in control.Controls)
                if (subControl is Label)
                    (subControl as Label).BackColor = control.BackColor;
                else if (subControl is PictureBox)
                    (subControl as PictureBox).BackColor = control.BackColor;
                else if (subControl is Panel)
                    InitializeAndFormatControlComponents(subControl);
        }

        public static List<PictureBoxButton> CreateMenuButtons(Panel container, List<string> captions, bool horizontal, EventHandler click)
        {
            List<PictureBoxButton> result = new List<PictureBoxButton>();
            for (int i = 0, n = captions.Count, dim = horizontal ? container.Width / n : container.Height / n; i < n; i++)
            {
                PictureBoxButton pic = new PictureBoxButton(captions[i], click);
                pic.Parent = container;
                pic.Cursor = Cursors.Hand;
                if (horizontal)
                    pic.SetBounds(i * dim, 0, dim, container.Height);
                else
                    pic.SetBounds(0, i * dim, container.Width, dim);
                PictureBoxButton.OnMouseLeave(pic, null);
                result.Add(pic);
            }
            return result;
        }

        public static List<SimpleCheckableButton> CreateSimpleCheckableButtons(Panel container, List<string> captions, bool horizontal, EventHandler click)
        {
            List<SimpleCheckableButton> result = new List<SimpleCheckableButton>();
            for (int i = 0, n = captions.Count, dim = horizontal ? container.Width / n : container.Height / n; i < n; i++)
            {
                SimpleCheckableButton pic = new SimpleCheckableButton(captions[i], false, click);
                pic.Parent = container;
                pic.Cursor = Cursors.Hand;
                if (horizontal)
                    pic.SetBounds(i * dim, 0, dim, container.Height);
                else
                    pic.SetBounds(0, i * dim, container.Width, dim);
                SimpleCheckableButton.OnMouseLeave(pic, null);
                result.Add(pic);
            }
            return result;
        }

        public static void DrawLikesDislikes(PictureBox pic, long likes, long dislikes)
        {
            if (pic.Image != null)
                pic.Image.Dispose();
            Bitmap bmp = new Bitmap(pic.Width, pic.Height);
            Graphics g = Graphics.FromImage(bmp);
            Brush brush = new SolidBrush(FormBackgroundC);
            g.FillRectangle(brush, 0, 0, pic.Width, pic.Height);
            if (likes > 0)
            {
                brush = new SolidBrush(ColorTranslator.FromHtml("#4FC418"));
                long width = (long) (double) (pic.Width * likes) / (likes + dislikes);
                g.FillRectangle(brush, 0, 0, width, pic.Height);
            }
            if (dislikes > 0)
            {
                brush = new SolidBrush(ColorTranslator.FromHtml("#B5122C"));
                long width = (long) (double) (pic.Width * dislikes) / (likes + dislikes);
                g.FillRectangle(brush, pic.Width - width - 1, 0, width, pic.Height);
            }
            pic.Image = bmp;
        }

        public static void DrawViewsSubscribersPercentage(PictureBox pic, long views, long subscribers)
        {
            if (pic.Image != null)
                pic.Image.Dispose();
            Bitmap bmp = new Bitmap(pic.Width, pic.Height);
            Graphics g = Graphics.FromImage(bmp);
            Brush brush = new SolidBrush(ColorTranslator.FromHtml("#2B343B"));
            g.FillRectangle(brush, 0, 0, pic.Width, pic.Height);
            brush = new SolidBrush(ColorTranslator.FromHtml("#066FBA"));
            long width = views >= subscribers ? pic.Width : (long) (double) (pic.Width * views) / subscribers;
            g.FillRectangle(brush, 0, 0, width, pic.Height);
            pic.Image = bmp;
        }
    }

    /// 
    /// 
    /// 
    public class PictureBoxButton : PictureBox
    {
        protected const int ButtonBarHeight = 4;
        protected static readonly Brush BarBr = new SolidBrush(MyGUIs.SelectedButtonC);
        protected static readonly Brush BarMOBr = new SolidBrush(MyGUIs.SelectedFontC);

        public string Caption { get; set; }
        private EventHandler ClickEH;

        public PictureBoxButton(string Information, EventHandler Click)
            : this(Information, Click, OnMouseEnter, OnMouseLeave)
        {
        }

        public PictureBoxButton(string Information, EventHandler Click, EventHandler MouseEnter, EventHandler MouseLeave)
            : base()
        {
            this.Caption = Information;
            this.Click += Click;
            this.MouseEnter += new EventHandler(OnMouseEnter);
            this.MouseLeave += new EventHandler(OnMouseLeave);
            DrawPictureBoxButton(this, false);
        }

        public void SetOnClickEventHandler(EventHandler click)
        {
            ClearOnClickEventHandler();
            this.ClickEH = click;
            this.Click += click;
        }

        public void ClearOnClickEventHandler()
        {
            this.Click -= ClickEH;
        }

        public static void OnMouseEnter(object sender, EventArgs e)
        {
            DrawPictureBoxButton((PictureBoxButton) sender, true);
        }

        public static void OnMouseLeave(object sender, EventArgs e)
        {
            DrawPictureBoxButton((PictureBoxButton) sender, false);
        }

        public static void DrawPictureBoxButton(PictureBoxButton pic, bool selected)
        {
            if (pic.Image != null)
                pic.Image.Dispose();
            Bitmap bmp = new Bitmap(pic.Width, pic.Height);
            Graphics g = Graphics.FromImage(bmp);
            Brush bgBrush = selected ? new SolidBrush(MyGUIs.SelectedButtonC) : new SolidBrush(MyGUIs.ButtonC);
            Brush textBrush = selected ? new SolidBrush(MyGUIs.SelectedFontC) : new SolidBrush(MyGUIs.FontC);
            Font font = MyGUIs.GetFont("Segoe UI", 17, true);
            g.FillRectangle(bgBrush, 0, 0, pic.Width, pic.Height);
            //
            int w = pic.Width, h = pic.Height, h3 = (int) (h * 0.7), h4 = (int) (h * 0.9);
            /*Point[] points = new Point[3] { new Point(1, 2 * pic.Height / 3), new Point(pic.Width / 3, pic.Height), new Point(1, pic.Height) };
            GraphicsPath gp = new GraphicsPath();
            for (int i = 0; i < points.Length; i++)
                gp.AddLine(points[i], points[i < points.Length - 1 ? i + 1 : 0]);
            g.FillPath(selected ? BarMOBr : BarBr, gp);*/
            g.FillRectangle(selected ? BarMOBr : BarBr, new Rectangle(1, pic.Height - ButtonBarHeight, pic.Width, ButtonBarHeight));
            g.FillRectangle(selected ? BarMOBr : BarBr, new Rectangle(pic.Width - ButtonBarHeight, (int) (pic.Height * 0.7), ButtonBarHeight, pic.Height));
            //
            Size size = g.MeasureString(pic.Caption, font).ToSize();
            g.DrawString(pic.Caption, font, textBrush, new Point(pic.Width / 2 - size.Width / 2, pic.Height / 2 - size.Height / 2));
            pic.Image = bmp;
        }
    }

    public class SimpleCheckableButton : PictureBox
    {
        protected const int SCButtonBarHeight = 6;
        protected static readonly Brush BarBr = new SolidBrush(MyGUIs.SelectedButtonC);
        protected static readonly Brush BarMOBr = new SolidBrush(MyGUIs.SelectedFontC);

        public string Information;
        public bool Checked;

        public SimpleCheckableButton(string Information, bool Checked, EventHandler Click)
            : this(Information, Checked, Click, OnMouseEnter, OnMouseLeave)
        {
        }

        public SimpleCheckableButton(string Information, bool Checked, EventHandler Click, EventHandler MouseEnter, EventHandler MouseLeave)
            : base()
        {
            this.Information = Information;
            this.Click += Click;
            this.MouseEnter += MouseEnter;
            this.MouseLeave += MouseLeave;
            SetCheckedAndRedraw(Checked);
        }

        public void SetCheckedAndRedraw(bool chk)
        {
            this.Checked = chk;
            DrawSimpleCheckableButton(this, false);
        }

        public static void OnMouseEnter(object sender, EventArgs e)
        {
            DrawSimpleCheckableButton((SimpleCheckableButton) sender, true);
        }

        public static void OnMouseLeave(object sender, EventArgs e)
        {
            DrawSimpleCheckableButton((SimpleCheckableButton) sender, false);
        }

        public static void DrawSimpleCheckableButton(SimpleCheckableButton pic, bool selected)
        {
            if (pic.Image != null)
                pic.Image.Dispose();
            Bitmap bmp = new Bitmap(pic.Width, pic.Height);
            Graphics g = Graphics.FromImage(bmp);
            Brush bgBrush = selected ? new SolidBrush(MyGUIs.SelectedButtonC) : new SolidBrush(MyGUIs.ButtonC);
            Brush textBrush = selected ? new SolidBrush(MyGUIs.SelectedFontC) : new SolidBrush(MyGUIs.FontC);
            Font font = MyGUIs.GetFont("Segoe UI", 15, pic.Checked);
            g.FillRectangle(bgBrush, 0, 0, pic.Width, pic.Height);
            if (pic.Checked)
                g.FillRectangle(BarMOBr, 0, pic.Height - SCButtonBarHeight, pic.Width, SCButtonBarHeight);
            Size size = g.MeasureString(pic.Information, font).ToSize();
            g.DrawString(pic.Information, font, textBrush, new Point(pic.Width / 2 - size.Width / 2, pic.Height / 2 - size.Height / 2));
            pic.Image = bmp;
        }
    }

    public class BorderPictureBox : PictureBox
    {
        public const int DefaultBorderWidth = 4;
        private static readonly Brush BgBr = new SolidBrush(MyGUIs.FormBackgroundC);
        private static readonly Brush BorderBr = new SolidBrush(ColorTranslator.FromHtml("#AAAAAA"));

        public int BorderWidth;

        public BorderPictureBox(Form form)
            : this(form, DefaultBorderWidth)
        {
        }

        public BorderPictureBox(Form form, int borderWidth)
            : base()
        {
            this.BorderWidth = borderWidth;
            this.Parent = form;
            this.StretchOut_SendToBack_Redraw();
        }

        public void StretchOut_SendToBack_Redraw()
        {
            this.SetBounds(0, 0, this.Parent.Width, this.Parent.Height);
            this.SendToBack();
            this.RedrawBorder();
        }

        public void RedrawBorder()
        {
            if (this.Image != null)
                this.Image.Dispose();

            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(BorderBr, new Rectangle(0, 0, this.Width, this.Height));
            g.FillRectangle(BgBr, new Rectangle(BorderWidth, BorderWidth, this.Width - BorderWidth * 2, this.Height - BorderWidth * 2));
            this.Image = bmp;
        }
    }

    /// 
    /// 
    /// 

    public class PictureBoxStatusBar : PictureBox
    {
        private static Brush bgBrush = new SolidBrush(MyGUIs.FormBackgroundC);
        private static Brush progressPassedBrush = new SolidBrush(ColorTranslator.FromHtml("#FFA500"));
        private static Brush progressRemainingBrush = new SolidBrush(ColorTranslator.FromHtml("#5C4F37"));
        private static Brush progressFinishedBrush = new SolidBrush(ColorTranslator.FromHtml("#3D871F"));
        private static Font info1F = MyGUIs.GetFont("Segoe UI", 13, true);
        private static Font info2F = MyGUIs.GetFont("Segoe UI", 13, true);
        private static Font info3F = MyGUIs.GetFont("Segoe UI", 12, false);
        private const int barPadding = 8;

        public string Info1, Info2, Info3;
        public int Percentage;

        public PictureBoxStatusBar(Panel container, string info1, string info2, string info3, int Percentage)
            : base()
        {
            this.Info1 = info1;
            this.Info2 = info2;
            this.Info3 = info3;
            this.Percentage = Percentage;
            this.Parent = container;
            this.SetBounds(0, 0, container.Width, container.Height);
            RefreshStatusBar();
        }

        public void UpdateStatus(string info1, string info2, string info3, int Percentage)
        {
            this.Info1 = info1;
            this.Info2 = info2;
            this.Info3 = info3;
            this.Percentage = Percentage;
            this.RefreshStatusBar();
        }

        private void RefreshStatusBar()
        {
            if (this.Image != null)
                this.Image.Dispose();
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(bgBrush, 0, 0, this.Width, this.Height);
            g.FillRectangle(progressRemainingBrush, new Rectangle(0, barPadding, 320, this.Height - 2 * barPadding));
            g.FillRectangle(this.Percentage < 100 ? progressPassedBrush : progressFinishedBrush, new Rectangle(0, barPadding, (int) (double) (320 * this.Percentage) / 100, this.Height - 2 * barPadding));

            Size size = g.MeasureString(this.Info1, info1F).ToSize();
            g.DrawString(this.Info1, info1F, Brushes.OrangeRed, new Point(328, this.Height / 2 - size.Height / 2));

            g.DrawString(this.Info2, info2F, Brushes.LightGray, new Point(328 + size.Width, this.Height / 2 - size.Height / 2));

            size = g.MeasureString(this.Info3, info3F).ToSize();
            g.DrawString(this.Info3, info3F, Brushes.Gray, new Point(this.Width - size.Width, this.Height / 2 - size.Height / 2));

            this.Image = bmp;
        }
    }
}
