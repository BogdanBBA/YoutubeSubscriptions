namespace YoutubeSubscriptions
{
	partial class FMain
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
			this.MenuP = new System.Windows.Forms.Panel();
			this.VideoListP = new System.Windows.Forms.Panel();
			this.StatusP = new System.Windows.Forms.Panel();
			this.UpdateAllTimer = new System.Windows.Forms.Timer(this.components);
			this.UpdateCurrentTimer = new System.Windows.Forms.Timer(this.components);
			this.LeftP = new System.Windows.Forms.Panel();
			this.ModeP = new System.Windows.Forms.Panel();
			this.SubSearchP = new System.Windows.Forms.Panel();
			this.FilteredSubsCountL = new System.Windows.Forms.Label();
			this.SubSearchTB = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SubListP = new System.Windows.Forms.Panel();
			this.GeneralInfoP = new System.Windows.Forms.Panel();
			this.CurrentDateL = new System.Windows.Forms.Label();
			this.FilteredSubsInfoL = new System.Windows.Forms.Label();
			this.CurrentDateT = new System.Windows.Forms.Timer(this.components);
			this.FindSubsT = new System.Windows.Forms.Timer(this.components);
			this.LeftP.SuspendLayout();
			this.SubSearchP.SuspendLayout();
			this.GeneralInfoP.SuspendLayout();
			this.SuspendLayout();
			// 
			// MenuP
			// 
			this.MenuP.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.MenuP.Location = new System.Drawing.Point(12, 3);
			this.MenuP.Name = "MenuP";
			this.MenuP.Size = new System.Drawing.Size(200, 100);
			this.MenuP.TabIndex = 0;
			// 
			// VideoListP
			// 
			this.VideoListP.AutoScroll = true;
			this.VideoListP.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.VideoListP.Location = new System.Drawing.Point(435, 152);
			this.VideoListP.Name = "VideoListP";
			this.VideoListP.Size = new System.Drawing.Size(200, 100);
			this.VideoListP.TabIndex = 2;
			// 
			// StatusP
			// 
			this.StatusP.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.StatusP.Location = new System.Drawing.Point(257, 84);
			this.StatusP.Name = "StatusP";
			this.StatusP.Size = new System.Drawing.Size(200, 100);
			this.StatusP.TabIndex = 3;
			// 
			// UpdateAllTimer
			// 
			this.UpdateAllTimer.Interval = 500;
			this.UpdateAllTimer.Tick += new System.EventHandler(this.UpdateAllTimer_Tick);
			// 
			// UpdateCurrentTimer
			// 
			this.UpdateCurrentTimer.Interval = 500;
			this.UpdateCurrentTimer.Tick += new System.EventHandler(this.UpdateCurrentTimer_Tick);
			// 
			// LeftP
			// 
			this.LeftP.AutoScroll = true;
			this.LeftP.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.LeftP.Controls.Add(this.ModeP);
			this.LeftP.Controls.Add(this.SubSearchP);
			this.LeftP.Controls.Add(this.SubListP);
			this.LeftP.Location = new System.Drawing.Point(54, 109);
			this.LeftP.Name = "LeftP";
			this.LeftP.Size = new System.Drawing.Size(294, 307);
			this.LeftP.TabIndex = 4;
			this.LeftP.Visible = false;
			// 
			// ModeP
			// 
			this.ModeP.AutoScroll = true;
			this.ModeP.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ModeP.Location = new System.Drawing.Point(21, 3);
			this.ModeP.Name = "ModeP";
			this.ModeP.Size = new System.Drawing.Size(200, 112);
			this.ModeP.TabIndex = 4;
			// 
			// SubSearchP
			// 
			this.SubSearchP.AutoScroll = true;
			this.SubSearchP.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.SubSearchP.Controls.Add(this.FilteredSubsCountL);
			this.SubSearchP.Controls.Add(this.SubSearchTB);
			this.SubSearchP.Controls.Add(this.label1);
			this.SubSearchP.Location = new System.Drawing.Point(46, 81);
			this.SubSearchP.Name = "SubSearchP";
			this.SubSearchP.Size = new System.Drawing.Size(200, 89);
			this.SubSearchP.TabIndex = 3;
			// 
			// FilteredSubsCountL
			// 
			this.FilteredSubsCountL.AutoSize = true;
			this.FilteredSubsCountL.Font = new System.Drawing.Font("Andika", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FilteredSubsCountL.ForeColor = System.Drawing.Color.White;
			this.FilteredSubsCountL.Location = new System.Drawing.Point(3, 64);
			this.FilteredSubsCountL.Name = "FilteredSubsCountL";
			this.FilteredSubsCountL.Size = new System.Drawing.Size(80, 24);
			this.FilteredSubsCountL.TabIndex = 2;
			this.FilteredSubsCountL.Text = "Subs (k/n)";
			// 
			// SubSearchTB
			// 
			this.SubSearchTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.SubSearchTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.SubSearchTB.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SubSearchTB.ForeColor = System.Drawing.Color.White;
			this.SubSearchTB.Location = new System.Drawing.Point(4, 24);
			this.SubSearchTB.Name = "SubSearchTB";
			this.SubSearchTB.Size = new System.Drawing.Size(100, 25);
			this.SubSearchTB.TabIndex = 1;
			this.SubSearchTB.TextChanged += new System.EventHandler(this.SubSearchTB_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Andika", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(175, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Search for subscriptions";
			// 
			// SubListP
			// 
			this.SubListP.AutoScroll = true;
			this.SubListP.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.SubListP.Location = new System.Drawing.Point(46, 178);
			this.SubListP.Name = "SubListP";
			this.SubListP.Size = new System.Drawing.Size(200, 112);
			this.SubListP.TabIndex = 2;
			// 
			// GeneralInfoP
			// 
			this.GeneralInfoP.AutoScroll = true;
			this.GeneralInfoP.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.GeneralInfoP.Controls.Add(this.CurrentDateL);
			this.GeneralInfoP.Controls.Add(this.FilteredSubsInfoL);
			this.GeneralInfoP.Location = new System.Drawing.Point(452, 287);
			this.GeneralInfoP.Name = "GeneralInfoP";
			this.GeneralInfoP.Size = new System.Drawing.Size(200, 100);
			this.GeneralInfoP.TabIndex = 5;
			this.GeneralInfoP.Visible = false;
			// 
			// CurrentDateL
			// 
			this.CurrentDateL.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CurrentDateL.ForeColor = System.Drawing.Color.Goldenrod;
			this.CurrentDateL.Location = new System.Drawing.Point(31, 49);
			this.CurrentDateL.Name = "CurrentDateL";
			this.CurrentDateL.Size = new System.Drawing.Size(119, 21);
			this.CurrentDateL.TabIndex = 2;
			this.CurrentDateL.Text = "Acquiring date and time information...";
			this.CurrentDateL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FilteredSubsInfoL
			// 
			this.FilteredSubsInfoL.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FilteredSubsInfoL.ForeColor = System.Drawing.Color.Gray;
			this.FilteredSubsInfoL.Location = new System.Drawing.Point(31, 11);
			this.FilteredSubsInfoL.Name = "FilteredSubsInfoL";
			this.FilteredSubsInfoL.Size = new System.Drawing.Size(119, 21);
			this.FilteredSubsInfoL.TabIndex = 1;
			this.FilteredSubsInfoL.Text = "Infoinfo";
			this.FilteredSubsInfoL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CurrentDateT
			// 
			this.CurrentDateT.Interval = 1000;
			this.CurrentDateT.Tick += new System.EventHandler(this.CurrentDateT_Tick);
			// 
			// FindSubsT
			// 
			this.FindSubsT.Interval = 50;
			this.FindSubsT.Tick += new System.EventHandler(this.FindSubsT_Tick);
			// 
			// FMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(756, 428);
			this.Controls.Add(this.GeneralInfoP);
			this.Controls.Add(this.LeftP);
			this.Controls.Add(this.StatusP);
			this.Controls.Add(this.VideoListP);
			this.Controls.Add(this.MenuP);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "FMain";
			this.Text = "Youtube subscriptions";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FMain_Load);
			this.Shown += new System.EventHandler(this.FMain_Shown);
			this.LeftP.ResumeLayout(false);
			this.SubSearchP.ResumeLayout(false);
			this.SubSearchP.PerformLayout();
			this.GeneralInfoP.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel MenuP;
		private System.Windows.Forms.Panel VideoListP;
		private System.Windows.Forms.Panel StatusP;
		private System.Windows.Forms.Timer UpdateAllTimer;
		private System.Windows.Forms.Timer UpdateCurrentTimer;
		private System.Windows.Forms.Panel LeftP;
		private System.Windows.Forms.Panel SubSearchP;
		private System.Windows.Forms.Panel SubListP;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel GeneralInfoP;
		private System.Windows.Forms.Label CurrentDateL;
		private System.Windows.Forms.Timer CurrentDateT;
		public System.Windows.Forms.TextBox SubSearchTB;
		private System.Windows.Forms.Timer FindSubsT;
		public System.Windows.Forms.Label FilteredSubsCountL;
		public System.Windows.Forms.Label FilteredSubsInfoL;
		private System.Windows.Forms.Panel ModeP;
	}
}

