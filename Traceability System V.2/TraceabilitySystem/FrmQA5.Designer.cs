namespace TraceabilitySystem
{
    partial class FrmQA5
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
            this.PanelUtama = new System.Windows.Forms.Panel();
            this.BtnStartProcess = new System.Windows.Forms.Button();
            this.txttimeNow = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOperatorName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnTransactionList = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtColorName = new System.Windows.Forms.TextBox();
            this.txtModelName = new System.Windows.Forms.TextBox();
            this.txtEPC = new System.Windows.Forms.TextBox();
            this.txtRFIDSerial = new System.Windows.Forms.TextBox();
            this.txtUnique = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.timGetRFIDData = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PanelUtama.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelUtama
            // 
            this.PanelUtama.BackColor = System.Drawing.Color.Azure;
            this.PanelUtama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelUtama.Controls.Add(this.BtnStartProcess);
            this.PanelUtama.Controls.Add(this.txttimeNow);
            this.PanelUtama.Controls.Add(this.label5);
            this.PanelUtama.Controls.Add(this.txtOperatorName);
            this.PanelUtama.Controls.Add(this.label3);
            this.PanelUtama.Controls.Add(this.textBox5);
            this.PanelUtama.Controls.Add(this.panel2);
            this.PanelUtama.Controls.Add(this.panel1);
            this.PanelUtama.Controls.Add(this.panel3);
            this.PanelUtama.Controls.Add(this.panel7);
            this.PanelUtama.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelUtama.Location = new System.Drawing.Point(0, 0);
            this.PanelUtama.Margin = new System.Windows.Forms.Padding(2);
            this.PanelUtama.Name = "PanelUtama";
            this.PanelUtama.Size = new System.Drawing.Size(1040, 640);
            this.PanelUtama.TabIndex = 0;
            this.PanelUtama.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelUtama_Paint);
            // 
            // BtnStartProcess
            // 
            this.BtnStartProcess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.BtnStartProcess.FlatAppearance.BorderSize = 0;
            this.BtnStartProcess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.BtnStartProcess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.BtnStartProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnStartProcess.Font = new System.Drawing.Font("Arial", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnStartProcess.ForeColor = System.Drawing.Color.White;
            this.BtnStartProcess.Location = new System.Drawing.Point(328, 526);
            this.BtnStartProcess.Margin = new System.Windows.Forms.Padding(2);
            this.BtnStartProcess.Name = "BtnStartProcess";
            this.BtnStartProcess.Size = new System.Drawing.Size(435, 61);
            this.BtnStartProcess.TabIndex = 39;
            this.BtnStartProcess.Text = "Start Process";
            this.BtnStartProcess.UseVisualStyleBackColor = false;
            this.BtnStartProcess.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // txttimeNow
            // 
            this.txttimeNow.BackColor = System.Drawing.Color.LightGray;
            this.txttimeNow.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttimeNow.Location = new System.Drawing.Point(752, 284);
            this.txttimeNow.Margin = new System.Windows.Forms.Padding(2);
            this.txttimeNow.Name = "txttimeNow";
            this.txttimeNow.ReadOnly = true;
            this.txttimeNow.Size = new System.Drawing.Size(213, 26);
            this.txttimeNow.TabIndex = 38;
            this.txttimeNow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(624, 286);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 18);
            this.label5.TabIndex = 37;
            this.label5.Text = "Time Now";
            // 
            // txtOperatorName
            // 
            this.txtOperatorName.BackColor = System.Drawing.Color.LightGray;
            this.txtOperatorName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOperatorName.Location = new System.Drawing.Point(752, 219);
            this.txtOperatorName.Margin = new System.Windows.Forms.Padding(2);
            this.txtOperatorName.Name = "txtOperatorName";
            this.txtOperatorName.ReadOnly = true;
            this.txtOperatorName.Size = new System.Drawing.Size(213, 26);
            this.txtOperatorName.TabIndex = 36;
            this.txtOperatorName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(624, 221);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 18);
            this.label3.TabIndex = 35;
            this.label3.Text = "Operator Name";
            // 
            // textBox5
            // 
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(640, 170);
            this.textBox5.Margin = new System.Windows.Forms.Padding(2);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(324, 19);
            this.textBox5.TabIndex = 34;
            this.textBox5.Text = "QA 5 Process";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Azure;
            this.panel2.Controls.Add(this.btnTransactionList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 76);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1038, 44);
            this.panel2.TabIndex = 33;
            // 
            // btnTransactionList
            // 
            this.btnTransactionList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTransactionList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnTransactionList.FlatAppearance.BorderSize = 0;
            this.btnTransactionList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnTransactionList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Olive;
            this.btnTransactionList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransactionList.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransactionList.ForeColor = System.Drawing.Color.White;
            this.btnTransactionList.Location = new System.Drawing.Point(848, 5);
            this.btnTransactionList.Margin = new System.Windows.Forms.Padding(2);
            this.btnTransactionList.Name = "btnTransactionList";
            this.btnTransactionList.Size = new System.Drawing.Size(182, 35);
            this.btnTransactionList.TabIndex = 2;
            this.btnTransactionList.Text = "Transaction List";
            this.btnTransactionList.UseVisualStyleBackColor = false;
            this.btnTransactionList.Click += new System.EventHandler(this.btnTransactionList_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtColorName);
            this.panel1.Controls.Add(this.txtModelName);
            this.panel1.Controls.Add(this.txtEPC);
            this.panel1.Controls.Add(this.txtRFIDSerial);
            this.panel1.Controls.Add(this.txtUnique);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(63, 158);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(471, 281);
            this.panel1.TabIndex = 0;
            // 
            // txtColorName
            // 
            this.txtColorName.BackColor = System.Drawing.Color.LightGray;
            this.txtColorName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColorName.Location = new System.Drawing.Point(181, 210);
            this.txtColorName.Margin = new System.Windows.Forms.Padding(2);
            this.txtColorName.Name = "txtColorName";
            this.txtColorName.Size = new System.Drawing.Size(232, 26);
            this.txtColorName.TabIndex = 30;
            this.txtColorName.Text = "MT-NATURAL-MT-NATURAL";
            this.txtColorName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtModelName
            // 
            this.txtModelName.BackColor = System.Drawing.Color.LightGray;
            this.txtModelName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModelName.Location = new System.Drawing.Point(181, 159);
            this.txtModelName.Margin = new System.Windows.Forms.Padding(2);
            this.txtModelName.Name = "txtModelName";
            this.txtModelName.Size = new System.Drawing.Size(232, 26);
            this.txtModelName.TabIndex = 29;
            this.txtModelName.Text = "APK700";
            this.txtModelName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEPC
            // 
            this.txtEPC.BackColor = System.Drawing.Color.LightGray;
            this.txtEPC.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEPC.Location = new System.Drawing.Point(181, 112);
            this.txtEPC.Margin = new System.Windows.Forms.Padding(2);
            this.txtEPC.Name = "txtEPC";
            this.txtEPC.Size = new System.Drawing.Size(232, 26);
            this.txtEPC.TabIndex = 28;
            this.txtEPC.Text = "220207000003";
            this.txtEPC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRFIDSerial
            // 
            this.txtRFIDSerial.BackColor = System.Drawing.Color.LightGray;
            this.txtRFIDSerial.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRFIDSerial.Location = new System.Drawing.Point(181, 65);
            this.txtRFIDSerial.Margin = new System.Windows.Forms.Padding(2);
            this.txtRFIDSerial.Name = "txtRFIDSerial";
            this.txtRFIDSerial.Size = new System.Drawing.Size(232, 26);
            this.txtRFIDSerial.TabIndex = 27;
            this.txtRFIDSerial.Text = "000003";
            this.txtRFIDSerial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtUnique
            // 
            this.txtUnique.BackColor = System.Drawing.Color.LightGray;
            this.txtUnique.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnique.Location = new System.Drawing.Point(181, 16);
            this.txtUnique.Margin = new System.Windows.Forms.Padding(2);
            this.txtUnique.Name = "txtUnique";
            this.txtUnique.Size = new System.Drawing.Size(232, 26);
            this.txtUnique.TabIndex = 26;
            this.txtUnique.Text = "P220211000006";
            this.txtUnique.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 214);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 18);
            this.label1.TabIndex = 25;
            this.label1.Text = "Color";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(22, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 18);
            this.label4.TabIndex = 21;
            this.label4.Text = "UniqueID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 18);
            this.label2.TabIndex = 22;
            this.label2.Text = "RFID Serial Number";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(22, 115);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 18);
            this.label7.TabIndex = 23;
            this.label7.Text = "EPC";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(22, 162);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 18);
            this.label8.TabIndex = 24;
            this.label8.Text = "Model";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Azure;
            this.panel3.Controls.Add(this.txtMessage);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 40);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1038, 36);
            this.panel3.TabIndex = 40;
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.Color.Azure;
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtMessage.Font = new System.Drawing.Font("Arial", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.ForeColor = System.Drawing.Color.Black;
            this.txtMessage.Location = new System.Drawing.Point(0, 0);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(2);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(1038, 38);
            this.txtMessage.TabIndex = 32;
            this.txtMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Blue;
            this.panel7.Controls.Add(this.pictureBox1);
            this.panel7.Controls.Add(this.pictureBox3);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.ForeColor = System.Drawing.Color.White;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1038, 40);
            this.panel7.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(1004, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 29);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.ErrorImage = null;
            this.pictureBox3.Location = new System.Drawing.Point(8, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(30, 32);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(462, 4);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(203, 29);
            this.label6.TabIndex = 0;
            this.label6.Text = "Transaction QA5";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timGetRFIDData
            // 
            this.timGetRFIDData.Interval = 1000;
            this.timGetRFIDData.Tick += new System.EventHandler(this.timGetRFIDData_Tick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmQA5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1040, 640);
            this.Controls.Add(this.PanelUtama);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmQA5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmMasterusers";
            this.Load += new System.EventHandler(this.FrmMasterModel_Load);
            this.PanelUtama.ResumeLayout(false);
            this.PanelUtama.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelUtama;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRFIDSerial;
        private System.Windows.Forms.TextBox txtUnique;
        private System.Windows.Forms.TextBox txtColorName;
        private System.Windows.Forms.TextBox txtModelName;
        private System.Windows.Forms.TextBox txtEPC;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnTransactionList;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox txtOperatorName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txttimeNow;
        private System.Windows.Forms.Button BtnStartProcess;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Timer timGetRFIDData;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Timer timer1;
    }
}