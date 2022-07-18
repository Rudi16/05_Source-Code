namespace TraceabilitySystem
{
    partial class FrmCreateRFID
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCreateRFID));
            this.panelCreateRFID = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnTransactionList = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtLastSerial = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.txtEPC = new System.Windows.Forms.TextBox();
            this.btnCreateRFID = new System.Windows.Forms.Button();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.cbExistsSerialNumber = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWriteEFC = new System.Windows.Forms.TextBox();
            this.txtExistingSerial = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSerialRFID = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timGetRFIDData = new System.Windows.Forms.Timer(this.components);
            this.panel5 = new System.Windows.Forms.Panel();
            this.panelCreateRFID.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCreateRFID
            // 
            this.panelCreateRFID.Controls.Add(this.panel4);
            this.panelCreateRFID.Controls.Add(this.panel2);
            this.panelCreateRFID.Controls.Add(this.panel3);
            this.panelCreateRFID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCreateRFID.Location = new System.Drawing.Point(0, 0);
            this.panelCreateRFID.Name = "panelCreateRFID";
            this.panelCreateRFID.Size = new System.Drawing.Size(1472, 1098);
            this.panelCreateRFID.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Azure;
            this.panel4.Controls.Add(this.btnTransactionList);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 54);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1472, 54);
            this.panel4.TabIndex = 18;
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
            this.btnTransactionList.Location = new System.Drawing.Point(1218, 6);
            this.btnTransactionList.Name = "btnTransactionList";
            this.btnTransactionList.Size = new System.Drawing.Size(243, 43);
            this.btnTransactionList.TabIndex = 2;
            this.btnTransactionList.Text = "Transaction List";
            this.btnTransactionList.UseVisualStyleBackColor = false;
            this.btnTransactionList.Click += new System.EventHandler(this.btnTransactionList_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Azure;
            this.panel2.Controls.Add(this.txtMessage);
            this.panel2.Controls.Add(this.btnClear);
            this.panel2.Controls.Add(this.txtLastSerial);
            this.panel2.Controls.Add(this.lblMsg);
            this.panel2.Controls.Add(this.txtEPC);
            this.panel2.Controls.Add(this.btnCreateRFID);
            this.panel2.Controls.Add(this.textBox6);
            this.panel2.Controls.Add(this.cbExistsSerialNumber);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtWriteEFC);
            this.panel2.Controls.Add(this.txtExistingSerial);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtSerialRFID);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 54);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1472, 1044);
            this.panel2.TabIndex = 17;
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.Color.White;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtMessage.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.ForeColor = System.Drawing.Color.Black;
            this.txtMessage.Location = new System.Drawing.Point(0, 1002);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(1472, 42);
            this.txtMessage.TabIndex = 21;
            this.txtMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(327, 663);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(176, 68);
            this.btnClear.TabIndex = 19;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Visible = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtLastSerial
            // 
            this.txtLastSerial.AutoSize = true;
            this.txtLastSerial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastSerial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtLastSerial.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastSerial.Location = new System.Drawing.Point(787, 202);
            this.txtLastSerial.Name = "txtLastSerial";
            this.txtLastSerial.Size = new System.Drawing.Size(173, 24);
            this.txtLastSerial.TabIndex = 18;
            this.txtLastSerial.Text = "Last Serial Number";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(3, 413);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 19);
            this.lblMsg.TabIndex = 17;
            // 
            // txtEPC
            // 
            this.txtEPC.BackColor = System.Drawing.Color.Yellow;
            this.txtEPC.Font = new System.Drawing.Font("Arial", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEPC.ForeColor = System.Drawing.Color.Black;
            this.txtEPC.Location = new System.Drawing.Point(182, 523);
            this.txtEPC.Name = "txtEPC";
            this.txtEPC.Size = new System.Drawing.Size(578, 45);
            this.txtEPC.TabIndex = 16;
            this.txtEPC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCreateRFID
            // 
            this.btnCreateRFID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnCreateRFID.FlatAppearance.BorderSize = 0;
            this.btnCreateRFID.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnCreateRFID.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnCreateRFID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateRFID.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateRFID.ForeColor = System.Drawing.Color.White;
            this.btnCreateRFID.Location = new System.Drawing.Point(787, 466);
            this.btnCreateRFID.Name = "btnCreateRFID";
            this.btnCreateRFID.Size = new System.Drawing.Size(340, 113);
            this.btnCreateRFID.TabIndex = 15;
            this.btnCreateRFID.Text = "Create RFID";
            this.btnCreateRFID.UseVisualStyleBackColor = false;
            this.btnCreateRFID.Click += new System.EventHandler(this.btnCreateRFID_Click);
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.Yellow;
            this.textBox6.Font = new System.Drawing.Font("Arial", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.ForeColor = System.Drawing.Color.Black;
            this.textBox6.Location = new System.Drawing.Point(182, 475);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(578, 45);
            this.textBox6.TabIndex = 14;
            this.textBox6.Text = "Please Put RFID Label On Reader";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cbExistsSerialNumber
            // 
            this.cbExistsSerialNumber.AutoSize = true;
            this.cbExistsSerialNumber.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbExistsSerialNumber.Location = new System.Drawing.Point(531, 299);
            this.cbExistsSerialNumber.Name = "cbExistsSerialNumber";
            this.cbExistsSerialNumber.Size = new System.Drawing.Size(229, 27);
            this.cbExistsSerialNumber.TabIndex = 13;
            this.cbExistsSerialNumber.Text = "Existing Serial Number";
            this.cbExistsSerialNumber.UseVisualStyleBackColor = true;
            this.cbExistsSerialNumber.CheckedChanged += new System.EventHandler(this.cbExistsSerialNumber_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(531, 358);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 23);
            this.label5.TabIndex = 11;
            this.label5.Text = "Write EPC Number";
            // 
            // txtWriteEFC
            // 
            this.txtWriteEFC.BackColor = System.Drawing.Color.White;
            this.txtWriteEFC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWriteEFC.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWriteEFC.Location = new System.Drawing.Point(787, 356);
            this.txtWriteEFC.Name = "txtWriteEFC";
            this.txtWriteEFC.ReadOnly = true;
            this.txtWriteEFC.Size = new System.Drawing.Size(340, 30);
            this.txtWriteEFC.TabIndex = 10;
            this.txtWriteEFC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtExistingSerial
            // 
            this.txtExistingSerial.BackColor = System.Drawing.Color.White;
            this.txtExistingSerial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExistingSerial.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExistingSerial.Location = new System.Drawing.Point(787, 299);
            this.txtExistingSerial.MaxLength = 6;
            this.txtExistingSerial.Name = "txtExistingSerial";
            this.txtExistingSerial.Size = new System.Drawing.Size(340, 30);
            this.txtExistingSerial.TabIndex = 8;
            this.txtExistingSerial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtExistingSerial.TextChanged += new System.EventHandler(this.txtExistingSerial_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(531, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "Serial Number";
            // 
            // txtSerialRFID
            // 
            this.txtSerialRFID.BackColor = System.Drawing.Color.White;
            this.txtSerialRFID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSerialRFID.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerialRFID.Location = new System.Drawing.Point(787, 238);
            this.txtSerialRFID.Name = "txtSerialRFID";
            this.txtSerialRFID.ReadOnly = true;
            this.txtSerialRFID.Size = new System.Drawing.Size(340, 30);
            this.txtSerialRFID.TabIndex = 6;
            this.txtSerialRFID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightGray;
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1472, 54);
            this.panel3.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Blue;
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1472, 54);
            this.panel1.TabIndex = 12;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(1428, 8);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(33, 36);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Visible = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click_1);
            // 
            // pictureBox4
            // 
            this.pictureBox4.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.ErrorImage")));
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(9, 5);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(40, 40);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 10;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(509, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(274, 35);
            this.label2.TabIndex = 0;
            this.label2.Text = "Create RFID Label";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1428, 8);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click_1);
            // 
            // pictureBox2
            // 
            this.pictureBox2.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.ErrorImage")));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(11, 10);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 29);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            this.pictureBox2.MouseHover += new System.EventHandler(this.pictureBox2_MouseHover);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(509, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Create RFID Label";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timGetRFIDData
            // 
            this.timGetRFIDData.Interval = 1000;
            this.timGetRFIDData.Tick += new System.EventHandler(this.tmrReadRFID_Tick);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.panelCreateRFID);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1476, 1102);
            this.panel5.TabIndex = 3;
            // 
            // FrmCreateRFID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1476, 1102);
            this.ControlBox = false;
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmCreateRFID";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register Product";
            this.Load += new System.EventHandler(this.FrmMasterModel_Load);
            this.panelCreateRFID.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelCreateRFID;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTransactionList;
        private System.Windows.Forms.Button btnCreateRFID;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtEPC;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.CheckBox cbExistsSerialNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtWriteEFC;
        private System.Windows.Forms.TextBox txtExistingSerial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSerialRFID;
        private System.Windows.Forms.Timer timGetRFIDData;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label txtLastSerial;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}