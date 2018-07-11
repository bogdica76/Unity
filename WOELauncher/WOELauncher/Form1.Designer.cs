namespace WOELauncher
{
    partial class WOELauncher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WOELauncher));
            this.btnLaunch = new System.Windows.Forms.Button();
            this.prgUpdate = new System.Windows.Forms.ProgressBar();
            this.txtVersion = new System.Windows.Forms.Label();
            this.txtCurrentVersion = new System.Windows.Forms.Label();
            this.txtUpdating = new System.Windows.Forms.Label();
            this.imgSlide = new System.Windows.Forms.PictureBox();
            this.edtNoutati = new System.Windows.Forms.TextBox();
            this.txtUltimeleNoutati = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgSlide)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLaunch
            // 
            this.btnLaunch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLaunch.Location = new System.Drawing.Point(595, 280);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(100, 50);
            this.btnLaunch.TabIndex = 0;
            this.btnLaunch.Text = "Proneste jocul";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // prgUpdate
            // 
            this.prgUpdate.Location = new System.Drawing.Point(12, 306);
            this.prgUpdate.Name = "prgUpdate";
            this.prgUpdate.Size = new System.Drawing.Size(577, 23);
            this.prgUpdate.TabIndex = 1;
            this.prgUpdate.Value = 100;
            // 
            // txtVersion
            // 
            this.txtVersion.AutoSize = true;
            this.txtVersion.Location = new System.Drawing.Point(558, 290);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(31, 13);
            this.txtVersion.TabIndex = 2;
            this.txtVersion.Text = "1.0.0";
            // 
            // txtCurrentVersion
            // 
            this.txtCurrentVersion.AutoSize = true;
            this.txtCurrentVersion.Location = new System.Drawing.Point(471, 290);
            this.txtCurrentVersion.Name = "txtCurrentVersion";
            this.txtCurrentVersion.Size = new System.Drawing.Size(88, 13);
            this.txtCurrentVersion.TabIndex = 3;
            this.txtCurrentVersion.Text = "Versiunea joculu:";
            // 
            // txtUpdating
            // 
            this.txtUpdating.AutoSize = true;
            this.txtUpdating.Location = new System.Drawing.Point(12, 290);
            this.txtUpdating.Name = "txtUpdating";
            this.txtUpdating.Size = new System.Drawing.Size(103, 13);
            this.txtUpdating.TabIndex = 4;
            this.txtUpdating.Text = "Jocul este actualizat";
            // 
            // imgSlide
            // 
            this.imgSlide.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgSlide.BackgroundImage")));
            this.imgSlide.Image = ((System.Drawing.Image)(resources.GetObject("imgSlide.Image")));
            this.imgSlide.Location = new System.Drawing.Point(15, 12);
            this.imgSlide.Name = "imgSlide";
            this.imgSlide.Size = new System.Drawing.Size(450, 265);
            this.imgSlide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgSlide.TabIndex = 5;
            this.imgSlide.TabStop = false;
            this.imgSlide.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // edtNoutati
            // 
            this.edtNoutati.Location = new System.Drawing.Point(471, 54);
            this.edtNoutati.Multiline = true;
            this.edtNoutati.Name = "edtNoutati";
            this.edtNoutati.Size = new System.Drawing.Size(224, 220);
            this.edtNoutati.TabIndex = 6;
            this.edtNoutati.Text = "21.06.2018\r\nLansarea jocului.\r\n\r\n22.05.2018\r\nAnuntarea jocului";
            // 
            // txtUltimeleNoutati
            // 
            this.txtUltimeleNoutati.AutoSize = true;
            this.txtUltimeleNoutati.Location = new System.Drawing.Point(471, 12);
            this.txtUltimeleNoutati.Name = "txtUltimeleNoutati";
            this.txtUltimeleNoutati.Size = new System.Drawing.Size(75, 13);
            this.txtUltimeleNoutati.TabIndex = 7;
            this.txtUltimeleNoutati.Text = "Ultimile noutati";
            // 
            // WOELauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 341);
            this.Controls.Add(this.txtUltimeleNoutati);
            this.Controls.Add(this.edtNoutati);
            this.Controls.Add(this.imgSlide);
            this.Controls.Add(this.txtUpdating);
            this.Controls.Add(this.txtCurrentVersion);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.prgUpdate);
            this.Controls.Add(this.btnLaunch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WOELauncher";
            this.Text = "WOE Launcher";
            this.Load += new System.EventHandler(this.WOELauncher_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgSlide)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.ProgressBar prgUpdate;
        private System.Windows.Forms.Label txtVersion;
        private System.Windows.Forms.Label txtCurrentVersion;
        private System.Windows.Forms.Label txtUpdating;
        private System.Windows.Forms.PictureBox imgSlide;
        private System.Windows.Forms.TextBox edtNoutati;
        private System.Windows.Forms.Label txtUltimeleNoutati;
    }
}

