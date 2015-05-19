using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubeSubscriptions.Classes
{
    /// <summary>
    /// Defines a chart image with formatting and visual style settings for two-dimensional number array sets. OPTIMIZED FOR YOUTUBESUBSCRIPTIONS.
    /// </summary>
    public class MyChart : PictureBox
    {
        private MyLabel title;
        private MyLabel subtitle;
        private Axis xAxis;
        private Axis yAxis;

        /// <summary>Constructs a MyChart object, placed in the given Control object and sized as specified.</summary>
        /// <param name="parent">the Control object that is to be the parent of the MyChart object</param>
        /// <param name="bounds">the locationa and size of the MyChart object</param>
        public MyChart(Control parent, Rectangle bounds, AxisFormatNumber_Delegate xAxisNumberFormat, AxisFormatNumber_Delegate yAxisNumberFormat)
            : base()
        {
            this.Parent = parent;
            this.Bounds = bounds;

            this.title = new MyLabel("MyChart title", new FontFormatting(MyGUIs.GetFont("Segoe UI", 20, true), Color.Black));
            this.subtitle = new MyLabel("MyChart subtitle", new FontFormatting(MyGUIs.GetFont("Segoe UI", 16, false), Color.Black));
            this.xAxis = new Axis("X-axis", xAxisNumberFormat);
            this.yAxis = new Axis("Y-axis", yAxisNumberFormat);
        }

        /// <summary>Gets the title MyLabel object of the current MyChart object.</summary>
        public MyLabel Title
        {
            get { return this.title; }
        }

        /// <summary>Gets the subtitle MyLabel object of the current MyChart object.</summary>
        public MyLabel Subtitle
        {
            get { return this.subtitle; }
        }

        /// <summary>Gets the X-axis Axis object of the current MyChart object.</summary>
        public Axis XAxis
        {
            get { return this.xAxis; }
        }

        /// <summary>Gets the Y-axis Axis object of the current MyChart object.</summary>
        public Axis YAxis
        {
            get { return this.yAxis; }
        }

        /// <summary>Redraws the current MyChart object with the given item list, using the current settings.</summary>
        /// <param name="items">the list of MyChartItem objects</param>
        public void RedrawChart(MyChartItemList items)
        {
            if (this.Image != null)
                this.Image.Dispose();
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.Clear(MyChartAux.BackgroundColor);

            // Prepare

            items.SortList(MyChartItemList.SortingOptions.ByXAxisValues, false);

            // Draw the title and subtitle, and track vertical usage
            int lastTop = MyChartAux.VerticalPadding;
            string text = this.title.Caption;
            Size size = g.MeasureString(text, this.title.FontFormatting.Font).ToSize();
            Point location = new Point(this.Width / 2 - size.Width / 2, lastTop);
            g.DrawString(text, this.title.FontFormatting.Font, this.title.FontFormatting.SolidBrush, location);
            lastTop += size.Height;

            text = this.subtitle.Caption;
            size = g.MeasureString(text, this.subtitle.FontFormatting.Font).ToSize();
            location = new Point(this.Width / 2 - size.Width / 2, lastTop);
            g.DrawString(text, this.subtitle.FontFormatting.Font, this.subtitle.FontFormatting.SolidBrush, location);
            lastTop += size.Height;

            // Calculate sizes and X value range

            double minXVal = Double.MaxValue, maxXVal = Double.MinValue;
            int yAxisW = 10;
            foreach (MyChartItem item in items)
            {
                if (minXVal > item.XValue)
                    minXVal = item.XValue;
                if (maxXVal < item.XValue)
                    maxXVal = item.XValue;

                int w = (int) g.MeasureString(this.yAxis.NumberFormat(item.YValue), this.yAxis.FontFormatting.Font).Width;
                if (yAxisW < w)
                    yAxisW = w;
            }
            double xValPadd = (maxXVal - minXVal) * 0.1;
            minXVal -= xValPadd;
            maxXVal += xValPadd;
            yAxisW = (int) (yAxisW * 1.2);

            Rectangle fullChart = new Rectangle(MyChartAux.HorizontalPadding, lastTop + MyChartAux.VerticalPadding,
                this.Width - 2 * MyChartAux.HorizontalPadding, this.Height - lastTop - 2 * MyChartAux.VerticalPadding);
            Size yAxisLabels = new Size(yAxisW + MyChartAux.HorizontalPadding, fullChart.Height - MyChartAux.XAxisHeight);
            Size xAxisLabels = new Size(fullChart.Width - yAxisLabels.Width, MyChartAux.XAxisHeight);
            Size graphArea = new Size(fullChart.Width - yAxisLabels.Width, fullChart.Height - xAxisLabels.Height);

            // Get Y-axis value range

            double maxYVal;
            if (this.yAxis.ValueRange.Auto)
            {
                maxYVal = Double.MinValue;
                foreach (MyChartItem item in items)
                    if (maxYVal < item.YValue)
                        maxYVal = item.YValue;
            }
            else
            {
                maxYVal = this.yAxis.ValueRange.Max;
            }
            maxYVal *= 1.2;

            // Draw Y-axis and Y-axis parallels, and interval values

            int lastX = fullChart.Left, lastY = fullChart.Top;
            double val = maxYVal;
            g.DrawLine(MyChartAux.AxisPen, lastX + yAxisLabels.Width, lastY, lastX + yAxisLabels.Width, lastY + yAxisLabels.Height);
            for (int i = 0, iY = lastY; i < this.yAxis.IntervalCount + 1; i++, iY += yAxisLabels.Height / this.yAxis.IntervalCount)
            {
                if (i < this.yAxis.IntervalCount)
                    g.DrawLine(MyChartAux.AxisParalelPen, lastX + yAxisLabels.Width + MyChartAux.AxisPen.Width / 2, iY, lastX + yAxisLabels.Width + graphArea.Width - 1, iY);

                text = this.yAxis.NumberFormat(val);
                size = g.MeasureString(text, this.yAxis.FontFormatting.Font).ToSize();
                location = new Point(fullChart.Left + yAxisLabels.Width - size.Width - 2 * (int) MyChartAux.AxisPen.Width, iY - size.Height / 2);
                g.DrawString(text, this.yAxis.FontFormatting.Font, this.yAxis.FontFormatting.SolidBrush, location);
                val -= maxYVal / (double) this.yAxis.IntervalCount;
            }

            // Get minimum X-axis value interval and calculate bar width

            double minIntv;
            int barW;
            if (items.Count > 1)
            {
                minIntv = Double.MaxValue;
                for (int i = 0; i < items.Count - 1; i++)
                {
                    double intv = items[i + 1].XValue - items[i].XValue;
                    if (minIntv > intv)
                        minIntv = intv;
                }
                //barW = (int) ((minIntv / 2) / (maxXVal - minXVal) * graphArea.Width);
                barW = graphArea.Width / items.Count / 3;
                if (barW < MyChartAux.MinBarWidth)
                    barW = MyChartAux.MinBarWidth;
            }
            else
            {
                minIntv = graphArea.Width / 2;
                barW = graphArea.Width / 2;
            }

            // Draw X-axis and X-axis parallels, and bars

            lastX = fullChart.Left + yAxisLabels.Width;
            lastY = fullChart.Top + yAxisLabels.Height;
            g.DrawLine(MyChartAux.AxisPen, lastX - MyChartAux.AxisPen.Width / 2, lastY, lastX + xAxisLabels.Width, lastY);
            for (int i = 0, iX = fullChart.Left + fullChart.Width - 1; i < this.xAxis.IntervalCount; i++, iX -= xAxisLabels.Width / this.xAxis.IntervalCount)
                g.DrawLine(MyChartAux.AxisParalelPen, iX, fullChart.Top, iX, lastY - MyChartAux.AxisPen.Width / 2 - 1);
            foreach (MyChartItem item in items)
            {
                int x = (int) (graphArea.Width - (item.XValue - minXVal) / (maxXVal - minXVal) * graphArea.Width + 1);
                int h = (int) (item.YValue / maxYVal * graphArea.Height);
                h = h < MyChartAux.MinBarHeight ? MyChartAux.MinBarHeight : h;
                location = new Point(fullChart.Left + yAxisLabels.Width + x - barW / 2, fullChart.Top + graphArea.Height - h - (int) (MyChartAux.AxisPen.Width / 2));
                size = new Size(barW, h);
                Rectangle rect = new Rectangle(location, size);
                g.FillRectangle(MyChartAux.BarBrush, rect);

                text = this.yAxis.NumberFormat(item.YValue);
                location.Y -= g.MeasureString(text, MyGUIs.GetFont("Ubuntu Condensed", 9, false)).ToSize().Height + 3;
                g.DrawString(text, MyGUIs.GetFont("Ubuntu Condensed", 9, false), MyChartAux.BarBrush, location);
            }
            this.Image = bmp;
        }

        /// <summary>Converts a date (originally in number-of-ticks format (long data type)) to string.</summary>
        public static string FormatDate(double axisValue)
        {
            DateTime date = new DateTime((long) axisValue);
            return Utils.FormatDateTime(date, Utils.DateTimeFormatD);
        }

        /// <summary>Formats a number of views (originally in long data type).</summary>
        public static string FormatViews(double axisValue)
        {
            return Utils.FormatNumber((long) axisValue);
        }
    }

    /// <summary>Defines a delegate that functions which format the axis labels must match.</summary>
    /// <param name="value">the value of the axis MyChartItem item</param>
    /// <returns>a string object, formatted as desired</returns>
    public delegate string AxisFormatNumber_Delegate(double value);

    /// <summary>
    /// Defines constants to be used with MyChart.RedrawChart.
    /// </summary>
    public static class MyChartAux
    {
        /// <summary>To be used at the top and bottom of the image, and between major sections.</summary>
        public static int VerticalPadding = 8;
        /// <summary>To be used at the left and right margins of the image.</summary>
        public static int HorizontalPadding = 8;
        /// <summary>The height of the label area of the X-axis.</summary>
        public static int XAxisHeight = 30;
        /// <summary>The minimum width of the value bar.</summary>
        public static int MinBarWidth = 2;
        /// <summary>The minimum height of the value bar.</summary>
        public static int MinBarHeight = 1;
        /// <summary>The pen used to draw the axes' lines.</summary>
        public static Pen AxisPen = new Pen(Color.Black, 4);
        /// <summary>The pen used to draw the axes' paralel lines.</summary>
        public static Pen AxisParalelPen = new Pen(Color.LightGray, 1);
        /// <summary>The brush used to draw the bars/lines.</summary>
        public static Brush BarBrush = new SolidBrush(Color.OrangeRed);
        /// <summary>The background color.</summary>
        public static Color BackgroundColor = Color.White;
    }

    /// <summary>
    /// Defines a customized collection of MyChartItem objects.
    /// </summary>
    public class MyChartItemList : List<MyChartItem>
    {
        /// <summary>Defines sorting options to be used with the MyChartItemList.Sort() method.</summary>
        public enum SortingOptions { ByXAxisValues, ByYAxisValues };

        /// <summary>Constructs a new and empty MyChartItemList object.</summary>
        public MyChartItemList()
            : base()
        {
        }

        public void SortList(SortingOptions sortingOptions, bool ascending)
        {
            if (this.Count < 2)
                return;
            for (int i = 0; i < this.Count - 1; i++)
                for (int j = i + 1; j < this.Count; j++)
                {
                    bool areAscending = true;
                    switch (sortingOptions)
                    {
                        case SortingOptions.ByXAxisValues:
                            areAscending = this[i].XValue < this[j].XValue;
                            break;
                        case SortingOptions.ByYAxisValues:
                            areAscending = this[i].YValue < this[j].YValue;
                            break;
                    }
                    if (areAscending != ascending)
                    {
                        MyChartItem aux = this[i];
                        this[i] = this[j];
                        this[j] = aux;
                    }
                }
        }
    }

    /// <summary>
    /// Defines an item with X and Y coordinates to be used with MyChart.ValueList
    /// </summary>
    public class MyChartItem
    {
        private double xValue;
        private double yValue;
        private object item;

        /// <summary>Constructs a MyChartItem object with the given coordinates.</summary>
        /// <param name="xValue">the X coordinate</param>
        /// <param name="yValue">the Y coordinate</param>
        public MyChartItem(double xValue, double yValue, object item)
        {
            this.xValue = xValue;
            this.yValue = yValue;
            this.item = item;
        }

        /// <summary>Gets the X coordinate of the current MyChartItem object.</summary>
        public double XValue
        {
            get { return this.xValue; }
        }

        /// <summary>Gets the Y coordinate of the current MyChartItem object.</summary>
        public double YValue
        {
            get { return this.yValue; }
        }

        /// <summary>Gets the Item object of the current MyChartItem object.</summary>
        public object Item
        {
            get { return this.item; }
        }
    }

    /// <summary>
    /// Defines a label object with a string caption and font formatting settings.
    /// </summary>
    public class MyLabel
    {
        private string caption;
        private FontFormatting fontFormatting;

        /// <summary>Constructs a label object with the given caption and font formatting settings.</summary>
        /// <param name="caption">the string to be displayed</param>
        /// <param name="fontFormatting">the font formatting settings</param>
        public MyLabel(string caption, FontFormatting fontFormatting)
        {
            this.caption = caption;
            this.fontFormatting = fontFormatting;
        }

        /// <summary>Gets or sets the string caption of the current MyLabel object.</summary>
        public string Caption
        {
            get { return this.caption; }
            set { this.caption = value; }
        }

        /// <summary>Gets or sets the the font formatting settings of the current MyLabel object.</summary>
        public FontFormatting FontFormatting
        {
            get { return this.fontFormatting; }
            set { this.fontFormatting = value; }
        }
    }

    /// <summary>
    /// Defines font formatting options to be used with MyChart objects.
    /// </summary>
    public class FontFormatting
    {
        private Font font;
        private Color color;
        private SolidBrush solidBrush;

        /// <summary>Constructs a FontFormatting object with the given settings.</summary>
        /// <param name="font">the Font object containing the font settings</param>
        /// <param name="color">the foreground color</param>
        public FontFormatting(Font font, Color color)
        {
            this.font = font;
            this.color = color;
            this.solidBrush = new SolidBrush(this.color);
        }

        /// <summary>Gets or sets the font of the current FontFormatting object.</summary>
        public Font Font
        {
            get { return this.font; }
            set { this.font = value; }
        }

        /// <summary>Gets or sets the foreground color of the current FontFormatting object.</summary>
        public Color Color
        {
            get { return this.color; }
            set
            {
                this.color = value;
                this.solidBrush = new SolidBrush(this.color);
            }
        }

        /// <summary>Gets a SolidBrush object for the foreground color of the current FontFormatting object.</summary>
        public SolidBrush SolidBrush
        {
            get { return this.solidBrush; }
        }
    }

    /// <summary>
    /// Defines an axis object, with a name and settings.
    /// </summary>
    public class Axis
    {
        private string name;
        private FontFormatting fontFormatting;
        private AxisFormatNumber_Delegate numberFormat;
        private ValueRange valueRange;
        private int intervalCount;

        /// <summary>Constructs an Axis object with the given name and number formatting event handler, and default settings.</summary>
        /// <param name="name">the name of the axis</param>
        /// <param name="numberFormat">the number formatting event handler</param>
        public Axis(string name, AxisFormatNumber_Delegate numberFormat)
            : this(name, new FontFormatting(MyGUIs.GetFont("Segoe UI", 10, false), ColorTranslator.FromHtml("#1C1C1C")), numberFormat, new ValueRange(), 4)
        {
        }

        /// <summary>Constructs an Axis object with the given settings.</summary>
        /// <param name="name">the name of the axis</param>
        /// <param name="fontFormatting">the font formatting settings of the axis labels</param>
        /// <param name="numberFormat">the number formatting event handler</param>
        /// <param name="valueRange">the value range of the axis</param>
        /// <param name="intervalCount">the number of intervals to be displayed; the interval value will be calculated automatically</param>
        public Axis(string name, FontFormatting fontFormatting, AxisFormatNumber_Delegate numberFormat, ValueRange valueRange, int intervalCount)
        {
            this.name = name;
            this.fontFormatting = fontFormatting;
            this.numberFormat = numberFormat;
            this.valueRange = valueRange;
            this.intervalCount = intervalCount;
        }

        /// <summary>Gets the name of the current axis.</summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>Gets or sets the the font formatting settings of the current Axis object.</summary>
        public FontFormatting FontFormatting
        {
            get { return this.fontFormatting; }
            set { this.fontFormatting = value; }
        }

        /// <summary>Gets or sets the number formatting event handler of the current axis.</summary>
        public AxisFormatNumber_Delegate NumberFormat
        {
            get { return this.numberFormat; }
            set { this.numberFormat = value; }
        }

        /// <summary>Gets or sets the value range of the current axis.</summary>
        public ValueRange ValueRange
        {
            get { return this.valueRange; }
            set { this.valueRange = value; }
        }

        /// <summary>Gets or sets the number of intervals of the current axis. Remember that the interval value will be calculated automatically.</summary>
        public int IntervalCount
        {
            get { return this.intervalCount; }
            set { this.intervalCount = value; }
        }
    }

    /// <summary>
    /// Defines a value range, with the option of having it marked to be calculated automatically by the sender function.
    /// </summary>
    public class ValueRange
    {
        private bool auto;
        private double min;
        private double max;

        /// <summary>Constructs a ValueRange object with default attribute values.</summary>
        public ValueRange()
            : this(true)
        {
        }

        /// <summary>Constructs a ValueRange object with the given setting.</summary>
        public ValueRange(bool auto)
            : this(auto, 0, 0)
        {
        }

        /// <summary>Constructs a ValueRange object with the given settings.</summary>
        public ValueRange(bool auto, double min, double max)
        {
            this.auto = auto;
            this.min = min;
            this.max = max;
        }

        /// <summary>Gets or sets a value indicating whether the range is to be calculated automatically by the sender function.</summary>
        public bool Auto
        {
            get { return this.auto; }
            set { this.auto = value; }
        }

        /// <summary>Gets or sets the minimum value of the range. If set to a value greater than Max, Max will be modified.</summary>
        public double Min
        {
            get { return this.min; }
            set
            {
                if (this.max < value)
                    this.max = value;
                this.min = value;
            }
        }

        /// <summary>Gets or sets the maximum value of the range. If set to a value smaller than Min, Min will be modified.</summary>
        public double Max
        {
            get { return this.max; }
            set
            {
                if (this.min > value)
                    this.min = value;
                this.max = value;
            }
        }
    }
}
