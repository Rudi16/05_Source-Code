namespace TraceabilitySystem
{
    partial class FrmPreviewImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPreviewImage));
            this.PanelUtama = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.picNGArea = new System.Windows.Forms.PictureBox();
            this.PanelUtama.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNGArea)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelUtama
            // 
            this.PanelUtama.BackColor = System.Drawing.Color.Azure;
            this.PanelUtama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelUtama.Controls.Add(this.picNGArea);
            this.PanelUtama.Controls.Add(this.panel7);
            this.PanelUtama.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelUtama.Location = new System.Drawing.Point(0, 0);
            this.PanelUtama.Margin = new System.Windows.Forms.Padding(2);
            this.PanelUtama.Name = "PanelUtama";
            this.PanelUtama.Size = new System.Drawing.Size(1166, 701);
            this.PanelUtama.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Blue;
            this.panel7.Controls.Add(this.pictureBox3);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.ForeColor = System.Drawing.Color.White;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1164, 37);
            this.panel7.TabIndex = 2;
            // 
            // pictureBox3
            // 
            this.pictureBox3.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.ErrorImage")));
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(8, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(30, 32);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // picNGArea
            // 
            this.picNGArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picNGArea.Location = new System.Drawing.Point(0, 37);
            this.picNGArea.Name = "picNGArea";
            this.picNGArea.Size = new System.Drawing.Size(1164, 662);
            this.picNGArea.TabIndex = 3;
            this.picNGArea.TabStop = false;
            // 
            // FrmPreviewImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1166, 701);
            this.Controls.Add(this.PanelUtama);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPreviewImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmMasterusers";
            this.Load += new System.EventHandler(this.FrmPreviewImage_Load);
            this.PanelUtama.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNGArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelUtama;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox picNGArea;
    }
}