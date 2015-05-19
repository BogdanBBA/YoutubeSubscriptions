namespace YoutubeSubscriptions.Forms
{
    partial class FChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MenuP = new System.Windows.Forms.Panel();
            this.ChartP = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // MenuP
            // 
            this.MenuP.Location = new System.Drawing.Point(295, 590);
            this.MenuP.Name = "MenuP";
            this.MenuP.Size = new System.Drawing.Size(600, 60);
            this.MenuP.TabIndex = 5;
            // 
            // ChartP
            // 
            this.ChartP.Location = new System.Drawing.Point(12, 12);
            this.ChartP.Name = "ChartP";
            this.ChartP.Size = new System.Drawing.Size(1160, 572);
            this.ChartP.TabIndex = 8;
            // 
            // FChart
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1184, 662);
            this.ControlBox = false;
            this.Controls.Add(this.ChartP);
            this.Controls.Add(this.MenuP);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FChart";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chart";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FChart_Load);
            this.VisibleChanged += new System.EventHandler(this.FChart_VisibleChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MenuP;
        private System.Windows.Forms.Panel ChartP;
    }
}