namespace YoutubeSubscriptions.Forms
{
    partial class FPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPlayer));
            this.MenuP = new System.Windows.Forms.Panel();
            this.TitleP = new System.Windows.Forms.Panel();
            this.StatusP = new System.Windows.Forms.Panel();
            this.StatusL = new System.Windows.Forms.Label();
            this.PlayerSF = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.StatusP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerSF)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuP
            // 
            this.MenuP.BackColor = System.Drawing.SystemColors.ControlLight;
            this.MenuP.Location = new System.Drawing.Point(51, 312);
            this.MenuP.Name = "MenuP";
            this.MenuP.Size = new System.Drawing.Size(600, 60);
            this.MenuP.TabIndex = 2;
            // 
            // TitleP
            // 
            this.TitleP.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TitleP.Location = new System.Drawing.Point(37, 30);
            this.TitleP.Name = "TitleP";
            this.TitleP.Size = new System.Drawing.Size(600, 60);
            this.TitleP.TabIndex = 3;
            // 
            // StatusP
            // 
            this.StatusP.BackColor = System.Drawing.SystemColors.ControlLight;
            this.StatusP.Controls.Add(this.StatusL);
            this.StatusP.Location = new System.Drawing.Point(51, 246);
            this.StatusP.Name = "StatusP";
            this.StatusP.Size = new System.Drawing.Size(600, 60);
            this.StatusP.TabIndex = 4;
            // 
            // StatusL
            // 
            this.StatusL.AutoSize = true;
            this.StatusL.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusL.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.StatusL.Location = new System.Drawing.Point(4, 16);
            this.StatusL.Name = "StatusL";
            this.StatusL.Size = new System.Drawing.Size(63, 25);
            this.StatusL.TabIndex = 7;
            this.StatusL.Text = "label1";
            // 
            // PlayerSF
            // 
            this.PlayerSF.Enabled = true;
            this.PlayerSF.Location = new System.Drawing.Point(124, 124);
            this.PlayerSF.Name = "PlayerSF";
            this.PlayerSF.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("PlayerSF.OcxState")));
            this.PlayerSF.Size = new System.Drawing.Size(192, 192);
            this.PlayerSF.TabIndex = 5;
            // 
            // FPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(790, 400);
            this.Controls.Add(this.PlayerSF);
            this.Controls.Add(this.StatusP);
            this.Controls.Add(this.TitleP);
            this.Controls.Add(this.MenuP);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FPlayer";
            this.Text = "Video player";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FPlayer_Load);
            this.Resize += new System.EventHandler(this.FPlayer_Resize);
            this.StatusP.ResumeLayout(false);
            this.StatusP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerSF)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MenuP;
        private System.Windows.Forms.Panel TitleP;
        private System.Windows.Forms.Panel StatusP;
        private AxShockwaveFlashObjects.AxShockwaveFlash PlayerSF;
        private System.Windows.Forms.Label StatusL;
    }
}