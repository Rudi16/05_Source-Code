namespace TraceabilitySystem
{
    partial class FrmCreateReg
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
            this.PanelUtama = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRegisterProduct = new System.Windows.Forms.Button();
            this.btnCreateLabelRFID = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pict_logout = new System.Windows.Forms.PictureBox();
            this.PanelUtama.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_logout)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelUtama
            // 
            this.PanelUtama.BackColor = System.Drawing.Color.Azure;
            this.PanelUtama.Controls.Add(this.panel20);
            this.PanelUtama.Controls.Add(this.panel1);
            this.PanelUtama.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelUtama.Location = new System.Drawing.Point(0, 0);
            this.PanelUtama.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PanelUtama.Name = "PanelUtama";
            this.PanelUtama.Size = new System.Drawing.Size(966, 567);
            this.PanelUtama.TabIndex = 0;
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.pictureBox1);
            this.panel20.Controls.Add(this.pict_logout);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel20.Location = new System.Drawing.Point(898, 0);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(68, 567);
            this.panel20.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.btnRegisterProduct);
            this.panel1.Controls.Add(this.btnCreateLabelRFID);
            this.panel1.Location = new System.Drawing.Point(81, 215);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 205);
            this.panel1.TabIndex = 1;
            // 
            // btnRegisterProduct
            // 
            this.btnRegisterProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnRegisterProduct.FlatAppearance.BorderSize = 0;
            this.btnRegisterProduct.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnRegisterProduct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnRegisterProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegisterProduct.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegisterProduct.ForeColor = System.Drawing.Color.White;
            this.btnRegisterProduct.Location = new System.Drawing.Point(421, 50);
            this.btnRegisterProduct.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRegisterProduct.Name = "btnRegisterProduct";
            this.btnRegisterProduct.Size = new System.Drawing.Size(248, 94);
            this.btnRegisterProduct.TabIndex = 1;
            this.btnRegisterProduct.Text = "Register Product";
            this.btnRegisterProduct.UseVisualStyleBackColor = false;
            this.btnRegisterProduct.Click += new System.EventHandler(this.btnRegisterProduct_Click);
            // 
            // btnCreateLabelRFID
            // 
            this.btnCreateLabelRFID.BackColor = System.Drawing.Color.Blue;
            this.btnCreateLabelRFID.FlatAppearance.BorderSize = 0;
            this.btnCreateLabelRFID.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.btnCreateLabelRFID.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.btnCreateLabelRFID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateLabelRFID.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateLabelRFID.ForeColor = System.Drawing.Color.White;
            this.btnCreateLabelRFID.Location = new System.Drawing.Point(112, 50);
            this.btnCreateLabelRFID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCreateLabelRFID.Name = "btnCreateLabelRFID";
            this.btnCreateLabelRFID.Size = new System.Drawing.Size(248, 94);
            this.btnCreateLabelRFID.TabIndex = 0;
            this.btnCreateLabelRFID.Text = "Create Label RFID";
            this.btnCreateLabelRFID.UseVisualStyleBackColor = false;
            this.btnCreateLabelRFID.Click += new System.EventHandler(this.btnCreateLabelRFID_Click);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.PanelUtama);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(970, 571);
            this.panel5.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TraceabilitySystem.Properties.Resources.pictureBox1_Image;
            this.pictureBox1.Location = new System.Drawing.Point(5, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 29);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pict_logout
            // 
            this.pict_logout.Image = global::TraceabilitySystem.Properties.Resources.log_out2;
            this.pict_logout.Location = new System.Drawing.Point(35, 8);
            this.pict_logout.Name = "pict_logout";
            this.pict_logout.Size = new System.Drawing.Size(26, 27);
            this.pict_logout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_logout.TabIndex = 1;
            this.pict_logout.TabStop = false;
            this.pict_logout.Click += new System.EventHandler(this.pict_logout_Click);
            // 
            // FrmCreateReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(970, 571);
            this.ControlBox = false;
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmCreateReg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register Product";
            this.PanelUtama.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_logout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelUtama;
        private System.Windows.Forms.Button btnCreateLabelRFID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRegisterProduct;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pict_logout;
    }
}