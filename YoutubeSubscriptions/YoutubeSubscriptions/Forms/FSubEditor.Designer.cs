namespace YoutubeSubscriptions
{
	partial class FSubEditor
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
			this.SubsTB = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.YoutuberIdTB = new System.Windows.Forms.TextBox();
			this.FormControlP = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.YoutuberNameTB = new System.Windows.Forms.TextBox();
			this.EditingP = new System.Windows.Forms.Panel();
			this.ExtrasP = new System.Windows.Forms.Panel();
			this.MoveP = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// SubsTB
			// 
			this.SubsTB.FormattingEnabled = true;
			this.SubsTB.ItemHeight = 21;
			this.SubsTB.Location = new System.Drawing.Point(30, 47);
			this.SubsTB.Name = "SubsTB";
			this.SubsTB.Size = new System.Drawing.Size(177, 466);
			this.SubsTB.TabIndex = 0;
			this.SubsTB.SelectedIndexChanged += new System.EventHandler(this.SubsTB_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Orange;
			this.label1.Location = new System.Drawing.Point(26, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 21);
			this.label1.TabIndex = 1;
			this.label1.Text = "Subscriptions";
			// 
			// YoutuberIdTB
			// 
			this.YoutuberIdTB.Location = new System.Drawing.Point(217, 71);
			this.YoutuberIdTB.Name = "YoutuberIdTB";
			this.YoutuberIdTB.Size = new System.Drawing.Size(300, 29);
			this.YoutuberIdTB.TabIndex = 2;
			this.YoutuberIdTB.TextChanged += new System.EventHandler(this.YoutuberIdTB_TextChanged);
			// 
			// FormControlP
			// 
			this.FormControlP.Location = new System.Drawing.Point(217, 468);
			this.FormControlP.Name = "FormControlP";
			this.FormControlP.Size = new System.Drawing.Size(300, 45);
			this.FormControlP.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Orange;
			this.label2.Location = new System.Drawing.Point(213, 23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(114, 21);
			this.label2.TabIndex = 4;
			this.label2.Text = "Selected item";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(213, 47);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(93, 21);
			this.label3.TabIndex = 5;
			this.label3.Text = "Youtuber ID";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(213, 103);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(99, 21);
			this.label4.TabIndex = 7;
			this.label4.Text = "Casual name";
			// 
			// YoutuberNameTB
			// 
			this.YoutuberNameTB.Location = new System.Drawing.Point(217, 127);
			this.YoutuberNameTB.Name = "YoutuberNameTB";
			this.YoutuberNameTB.Size = new System.Drawing.Size(300, 29);
			this.YoutuberNameTB.TabIndex = 6;
			// 
			// EditingP
			// 
			this.EditingP.Location = new System.Drawing.Point(217, 198);
			this.EditingP.Name = "EditingP";
			this.EditingP.Size = new System.Drawing.Size(300, 135);
			this.EditingP.TabIndex = 8;
			// 
			// ExtrasP
			// 
			this.ExtrasP.Location = new System.Drawing.Point(217, 409);
			this.ExtrasP.Name = "ExtrasP";
			this.ExtrasP.Size = new System.Drawing.Size(300, 45);
			this.ExtrasP.TabIndex = 9;
			// 
			// MoveP
			// 
			this.MoveP.Location = new System.Drawing.Point(217, 348);
			this.MoveP.Name = "MoveP";
			this.MoveP.Size = new System.Drawing.Size(300, 45);
			this.MoveP.TabIndex = 10;
			// 
			// FSubEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(545, 545);
			this.ControlBox = false;
			this.Controls.Add(this.MoveP);
			this.Controls.Add(this.ExtrasP);
			this.Controls.Add(this.EditingP);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.YoutuberNameTB);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.FormControlP);
			this.Controls.Add(this.YoutuberIdTB);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.SubsTB);
			this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "FSubEditor";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Subscription editor";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.FSubEditor_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox SubsTB;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox YoutuberIdTB;
		private System.Windows.Forms.Panel FormControlP;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox YoutuberNameTB;
		private System.Windows.Forms.Panel EditingP;
		private System.Windows.Forms.Panel ExtrasP;
		private System.Windows.Forms.Panel MoveP;
	}
}