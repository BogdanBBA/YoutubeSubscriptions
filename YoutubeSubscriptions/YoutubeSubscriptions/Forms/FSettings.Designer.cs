namespace YoutubeSubscriptions.Forms
{
	partial class FSettings
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.daysNUD = new System.Windows.Forms.NumericUpDown();
            this.videoColsNUD = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dateFormat = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.showVideoTitleChB = new System.Windows.Forms.CheckBox();
            this.showVideoStatsChB = new System.Windows.Forms.CheckBox();
            this.downloadThumbnailsAnywayChB = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.showLengthOnThumbChB = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.showVideoUploaderChB = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.compactSubBoxChB = new System.Windows.Forms.CheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.MenuP = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.volumeL = new System.Windows.Forms.Label();
            this.volumeTrB = new System.Windows.Forms.TrackBar();
            this.qualityCB = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.playbackRateNUD = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.subBoxSubtitleCB = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.skipSecondsNUD = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.startFullWindowChB = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.uploaderInfoTypeCB = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.daysNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.videoColsNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playbackRateNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.skipSecondsNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(503, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(451, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "From the last how many days ago (plus today) to include videos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Orange;
            this.label2.Location = new System.Drawing.Point(503, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 21);
            this.label2.TabIndex = 7;
            this.label2.Text = "Update settings";
            // 
            // daysNUD
            // 
            this.daysNUD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.daysNUD.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.daysNUD.Location = new System.Drawing.Point(507, 74);
            this.daysNUD.Maximum = new decimal(new int[] {
            3650,
            0,
            0,
            0});
            this.daysNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.daysNUD.Name = "daysNUD";
            this.daysNUD.Size = new System.Drawing.Size(120, 25);
            this.daysNUD.TabIndex = 13;
            this.daysNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.daysNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.daysNUD.ValueChanged += new System.EventHandler(this.daysNUD_ValueChanged);
            // 
            // videoColsNUD
            // 
            this.videoColsNUD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.videoColsNUD.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.videoColsNUD.Location = new System.Drawing.Point(16, 225);
            this.videoColsNUD.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.videoColsNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.videoColsNUD.Name = "videoColsNUD";
            this.videoColsNUD.Size = new System.Drawing.Size(120, 25);
            this.videoColsNUD.TabIndex = 16;
            this.videoColsNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.videoColsNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 21);
            this.label6.TabIndex = 15;
            this.label6.Text = "Video columns";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(216, 197);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(187, 21);
            this.label7.TabIndex = 17;
            this.label7.Text = "Video upload date format";
            // 
            // dateFormat
            // 
            this.dateFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dateFormat.FormattingEnabled = true;
            this.dateFormat.Location = new System.Drawing.Point(220, 221);
            this.dateFormat.MaxDropDownItems = 20;
            this.dateFormat.Name = "dateFormat";
            this.dateFormat.Size = new System.Drawing.Size(260, 29);
            this.dateFormat.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(13, 453);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(250, 21);
            this.label8.TabIndex = 21;
            this.label8.Text = "Show video upload date and views";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(13, 326);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 21);
            this.label9.TabIndex = 19;
            this.label9.Text = "Show video title";
            // 
            // showVideoTitleChB
            // 
            this.showVideoTitleChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showVideoTitleChB.Location = new System.Drawing.Point(17, 350);
            this.showVideoTitleChB.Name = "showVideoTitleChB";
            this.showVideoTitleChB.Size = new System.Drawing.Size(90, 24);
            this.showVideoTitleChB.TabIndex = 22;
            this.showVideoTitleChB.Text = "checkBox1";
            this.showVideoTitleChB.UseVisualStyleBackColor = true;
            this.showVideoTitleChB.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // showVideoStatsChB
            // 
            this.showVideoStatsChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showVideoStatsChB.Location = new System.Drawing.Point(17, 477);
            this.showVideoStatsChB.Name = "showVideoStatsChB";
            this.showVideoStatsChB.Size = new System.Drawing.Size(90, 24);
            this.showVideoStatsChB.TabIndex = 23;
            this.showVideoStatsChB.Text = "checkBox2";
            this.showVideoStatsChB.UseVisualStyleBackColor = true;
            this.showVideoStatsChB.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // downloadThumbnailsAnywayChB
            // 
            this.downloadThumbnailsAnywayChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.downloadThumbnailsAnywayChB.Location = new System.Drawing.Point(507, 137);
            this.downloadThumbnailsAnywayChB.Name = "downloadThumbnailsAnywayChB";
            this.downloadThumbnailsAnywayChB.Size = new System.Drawing.Size(90, 24);
            this.downloadThumbnailsAnywayChB.TabIndex = 25;
            this.downloadThumbnailsAnywayChB.Text = "checkBox1";
            this.downloadThumbnailsAnywayChB.UseVisualStyleBackColor = true;
            this.downloadThumbnailsAnywayChB.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(503, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(385, 21);
            this.label10.TabIndex = 24;
            this.label10.Text = "Download thumbnails even if they already exist locally";
            // 
            // showLengthOnThumbChB
            // 
            this.showLengthOnThumbChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showLengthOnThumbChB.Location = new System.Drawing.Point(17, 414);
            this.showLengthOnThumbChB.Name = "showLengthOnThumbChB";
            this.showLengthOnThumbChB.Size = new System.Drawing.Size(90, 24);
            this.showLengthOnThumbChB.TabIndex = 27;
            this.showLengthOnThumbChB.Text = "checkBox1";
            this.showLengthOnThumbChB.UseVisualStyleBackColor = true;
            this.showLengthOnThumbChB.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(13, 390);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(211, 21);
            this.label11.TabIndex = 26;
            this.label11.Text = "Show video length on thumb";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Orange;
            this.label12.Location = new System.Drawing.Point(12, 171);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(196, 21);
            this.label12.TabIndex = 36;
            this.label12.Text = "Display settings / videos";
            // 
            // showVideoUploaderChB
            // 
            this.showVideoUploaderChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showVideoUploaderChB.Location = new System.Drawing.Point(17, 287);
            this.showVideoUploaderChB.Name = "showVideoUploaderChB";
            this.showVideoUploaderChB.Size = new System.Drawing.Size(90, 24);
            this.showVideoUploaderChB.TabIndex = 38;
            this.showVideoUploaderChB.Text = "checkBox1";
            this.showVideoUploaderChB.UseVisualStyleBackColor = true;
            this.showVideoUploaderChB.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(13, 263);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(157, 21);
            this.label17.TabIndex = 37;
            this.label17.Text = "Show video uploader";
            // 
            // compactSubBoxChB
            // 
            this.compactSubBoxChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.compactSubBoxChB.Location = new System.Drawing.Point(17, 72);
            this.compactSubBoxChB.Name = "compactSubBoxChB";
            this.compactSubBoxChB.Size = new System.Drawing.Size(90, 24);
            this.compactSubBoxChB.TabIndex = 43;
            this.compactSubBoxChB.Text = "checkBox1";
            this.compactSubBoxChB.UseVisualStyleBackColor = true;
            this.compactSubBoxChB.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(13, 48);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(190, 21);
            this.label23.TabIndex = 42;
            this.label23.Text = "Compact subscription box";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Orange;
            this.label24.Location = new System.Drawing.Point(12, 18);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(247, 21);
            this.label24.TabIndex = 41;
            this.label24.Text = "Display settings / subscriptions";
            // 
            // MenuP
            // 
            this.MenuP.Location = new System.Drawing.Point(346, 479);
            this.MenuP.Name = "MenuP";
            this.MenuP.Size = new System.Drawing.Size(300, 45);
            this.MenuP.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Orange;
            this.label1.Location = new System.Drawing.Point(503, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 21);
            this.label1.TabIndex = 52;
            this.label1.Text = "Video playback settings";
            // 
            // volumeL
            // 
            this.volumeL.AutoSize = true;
            this.volumeL.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.volumeL.Location = new System.Drawing.Point(503, 201);
            this.volumeL.Name = "volumeL";
            this.volumeL.Size = new System.Drawing.Size(116, 21);
            this.volumeL.TabIndex = 53;
            this.volumeL.Text = "Default volume";
            // 
            // volumeTrB
            // 
            this.volumeTrB.AutoSize = false;
            this.volumeTrB.Location = new System.Drawing.Point(507, 225);
            this.volumeTrB.Maximum = 100;
            this.volumeTrB.Name = "volumeTrB";
            this.volumeTrB.Size = new System.Drawing.Size(260, 31);
            this.volumeTrB.TabIndex = 54;
            this.volumeTrB.Scroll += new System.EventHandler(this.volumeTrB_Scroll);
            // 
            // qualityCB
            // 
            this.qualityCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.qualityCB.FormattingEnabled = true;
            this.qualityCB.Location = new System.Drawing.Point(507, 287);
            this.qualityCB.MaxDropDownItems = 20;
            this.qualityCB.Name = "qualityCB";
            this.qualityCB.Size = new System.Drawing.Size(260, 29);
            this.qualityCB.TabIndex = 58;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(503, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 21);
            this.label5.TabIndex = 57;
            this.label5.Text = "Default quality";
            // 
            // playbackRateNUD
            // 
            this.playbackRateNUD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.playbackRateNUD.DecimalPlaces = 2;
            this.playbackRateNUD.Enabled = false;
            this.playbackRateNUD.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playbackRateNUD.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.playbackRateNUD.Location = new System.Drawing.Point(507, 343);
            this.playbackRateNUD.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.playbackRateNUD.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.playbackRateNUD.Name = "playbackRateNUD";
            this.playbackRateNUD.Size = new System.Drawing.Size(120, 25);
            this.playbackRateNUD.TabIndex = 56;
            this.playbackRateNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.playbackRateNUD.Value = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(503, 373);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(238, 21);
            this.label13.TabIndex = 55;
            this.label13.Text = "Skip intros (seek ahead, seconds)";
            // 
            // subBoxSubtitleCB
            // 
            this.subBoxSubtitleCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.subBoxSubtitleCB.FormattingEnabled = true;
            this.subBoxSubtitleCB.Location = new System.Drawing.Point(16, 123);
            this.subBoxSubtitleCB.MaxDropDownItems = 20;
            this.subBoxSubtitleCB.Name = "subBoxSubtitleCB";
            this.subBoxSubtitleCB.Size = new System.Drawing.Size(260, 29);
            this.subBoxSubtitleCB.TabIndex = 60;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(12, 99);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(294, 21);
            this.label14.TabIndex = 59;
            this.label14.Text = "Subscription box subtitle (if not compact)";
            // 
            // skipSecondsNUD
            // 
            this.skipSecondsNUD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skipSecondsNUD.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skipSecondsNUD.Location = new System.Drawing.Point(507, 397);
            this.skipSecondsNUD.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.skipSecondsNUD.Name = "skipSecondsNUD";
            this.skipSecondsNUD.Size = new System.Drawing.Size(120, 25);
            this.skipSecondsNUD.TabIndex = 61;
            this.skipSecondsNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(503, 319);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(156, 21);
            this.label15.TabIndex = 62;
            this.label15.Text = "Default playback rate";
            // 
            // startFullWindowChB
            // 
            this.startFullWindowChB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startFullWindowChB.Location = new System.Drawing.Point(797, 234);
            this.startFullWindowChB.Name = "startFullWindowChB";
            this.startFullWindowChB.Size = new System.Drawing.Size(90, 24);
            this.startFullWindowChB.TabIndex = 64;
            this.startFullWindowChB.Text = "checkBox1";
            this.startFullWindowChB.UseVisualStyleBackColor = true;
            this.startFullWindowChB.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(793, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 21);
            this.label4.TabIndex = 63;
            this.label4.Text = "Start full-window";
            // 
            // uploaderInfoTypeCB
            // 
            this.uploaderInfoTypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uploaderInfoTypeCB.FormattingEnabled = true;
            this.uploaderInfoTypeCB.Location = new System.Drawing.Point(220, 287);
            this.uploaderInfoTypeCB.MaxDropDownItems = 20;
            this.uploaderInfoTypeCB.Name = "uploaderInfoTypeCB";
            this.uploaderInfoTypeCB.Size = new System.Drawing.Size(260, 29);
            this.uploaderInfoTypeCB.TabIndex = 66;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(216, 263);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(194, 21);
            this.label16.TabIndex = 65;
            this.label16.Text = "Uploader information type";
            // 
            // FSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(966, 536);
            this.ControlBox = false;
            this.Controls.Add(this.uploaderInfoTypeCB);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.startFullWindowChB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.skipSecondsNUD);
            this.Controls.Add(this.subBoxSubtitleCB);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.qualityCB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.playbackRateNUD);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.volumeTrB);
            this.Controls.Add(this.volumeL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MenuP);
            this.Controls.Add(this.compactSubBoxChB);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.showVideoUploaderChB);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.showLengthOnThumbChB);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.downloadThumbnailsAnywayChB);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.showVideoStatsChB);
            this.Controls.Add(this.showVideoTitleChB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dateFormat);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.videoColsNUD);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.daysNUD);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.daysNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.videoColsNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playbackRateNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.skipSecondsNUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown daysNUD;
		private System.Windows.Forms.NumericUpDown videoColsNUD;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox dateFormat;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox showVideoTitleChB;
		private System.Windows.Forms.CheckBox showVideoStatsChB;
		private System.Windows.Forms.CheckBox downloadThumbnailsAnywayChB;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.CheckBox showLengthOnThumbChB;
        private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.CheckBox showVideoUploaderChB;
        private System.Windows.Forms.Label label17;
		private System.Windows.Forms.CheckBox compactSubBoxChB;
		private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel MenuP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label volumeL;
        private System.Windows.Forms.TrackBar volumeTrB;
        private System.Windows.Forms.ComboBox qualityCB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown playbackRateNUD;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox subBoxSubtitleCB;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown skipSecondsNUD;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox startFullWindowChB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox uploaderInfoTypeCB;
        private System.Windows.Forms.Label label16;
	}
}