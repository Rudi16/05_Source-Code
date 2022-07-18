namespace TraceabilitySystem
{
    partial class FrmReportNG
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelListNG = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbNGType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbNGCategory = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.date2 = new System.Windows.Forms.DateTimePicker();
            this.date1 = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cbLocation = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panelCategory = new System.Windows.Forms.Panel();
            this.Datagridsummary = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnExportsummary = new System.Windows.Forms.Button();
            this.panelgrid = new System.Windows.Forms.Panel();
            this.PanelMasterNG = new System.Windows.Forms.Panel();
            this.PanelNGName = new System.Windows.Forms.Panel();
            this.datagriddetail = new System.Windows.Forms.DataGridView();
            this.panel10 = new System.Windows.Forms.Panel();
            this.btnExportDetail = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datagridsummary)).BeginInit();
            this.panel5.SuspendLayout();
            this.panelgrid.SuspendLayout();
            this.PanelMasterNG.SuspendLayout();
            this.PanelNGName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagriddetail)).BeginInit();
            this.panel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(23, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1313, 13);
            this.panel2.TabIndex = 0;
            // 
            // panelListNG
            // 
            this.panelListNG.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelListNG.Location = new System.Drawing.Point(1336, 0);
            this.panelListNG.Margin = new System.Windows.Forms.Padding(4);
            this.panelListNG.Name = "panelListNG";
            this.panelListNG.Size = new System.Drawing.Size(5, 748);
            this.panelListNG.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(1269, 6);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(37, 28);
            this.button2.TabIndex = 1;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 35);
            this.label2.TabIndex = 0;
            this.label2.Text = "Report NG";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkGray;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(23, 13);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1313, 43);
            this.panel3.TabIndex = 6;
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Margin = new System.Windows.Forms.Padding(4);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(19, 685);
            this.panel9.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(23, 748);
            this.panel4.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbNGType);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cbNGCategory);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.date2);
            this.panel1.Controls.Add(this.date1);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.cbLocation);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(555, 244);
            this.panel1.TabIndex = 23;
            // 
            // cbNGType
            // 
            this.cbNGType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbNGType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNGType.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNGType.FormattingEnabled = true;
            this.cbNGType.Location = new System.Drawing.Point(171, 135);
            this.cbNGType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbNGType.Name = "cbNGType";
            this.cbNGType.Size = new System.Drawing.Size(302, 25);
            this.cbNGType.TabIndex = 5;
            this.cbNGType.SelectedIndexChanged += new System.EventHandler(this.cbNGType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 17);
            this.label5.TabIndex = 29;
            this.label5.Text = "NG TYPE :";
            // 
            // cbNGCategory
            // 
            this.cbNGCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbNGCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNGCategory.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNGCategory.FormattingEnabled = true;
            this.cbNGCategory.Location = new System.Drawing.Point(171, 96);
            this.cbNGCategory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbNGCategory.Name = "cbNGCategory";
            this.cbNGCategory.Size = new System.Drawing.Size(302, 25);
            this.cbNGCategory.TabIndex = 4;
            this.cbNGCategory.SelectedIndexChanged += new System.EventHandler(this.cbNGCategory_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 17);
            this.label1.TabIndex = 27;
            this.label1.Text = "NG CATEGORY :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 26;
            this.label4.Text = "DATE :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(311, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 17);
            this.label3.TabIndex = 25;
            this.label3.Text = "to";
            // 
            // date2
            // 
            this.date2.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date2.CustomFormat = "dd/MM/yyyy";
            this.date2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date2.Location = new System.Drawing.Point(340, 17);
            this.date2.Margin = new System.Windows.Forms.Padding(4);
            this.date2.Name = "date2";
            this.date2.Size = new System.Drawing.Size(133, 25);
            this.date2.TabIndex = 2;
            // 
            // date1
            // 
            this.date1.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date1.CustomFormat = "dd/MM/yyyy";
            this.date1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date1.Location = new System.Drawing.Point(171, 17);
            this.date1.Margin = new System.Windows.Forms.Padding(4);
            this.date1.Name = "date1";
            this.date1.Size = new System.Drawing.Size(131, 25);
            this.date1.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(171, 179);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(127, 36);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbLocation
            // 
            this.cbLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocation.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLocation.FormattingEnabled = true;
            this.cbLocation.Items.AddRange(new object[] {
            "-Select-",
            "QA5 (Testing)",
            "QA6 (Final Inspection)"});
            this.cbLocation.Location = new System.Drawing.Point(171, 58);
            this.cbLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbLocation.Name = "cbLocation";
            this.cbLocation.Size = new System.Drawing.Size(302, 25);
            this.cbLocation.TabIndex = 3;
            this.cbLocation.SelectedIndexChanged += new System.EventHandler(this.cbLocation_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(25, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "QA :";
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(576, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(42, 685);
            this.panel7.TabIndex = 10;
            // 
            // panelCategory
            // 
            this.panelCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCategory.Controls.Add(this.Datagridsummary);
            this.panelCategory.Controls.Add(this.panel5);
            this.panelCategory.Controls.Add(this.panel1);
            this.panelCategory.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelCategory.Location = new System.Drawing.Point(19, 0);
            this.panelCategory.Margin = new System.Windows.Forms.Padding(4);
            this.panelCategory.Name = "panelCategory";
            this.panelCategory.Size = new System.Drawing.Size(557, 685);
            this.panelCategory.TabIndex = 9;
            // 
            // Datagridsummary
            // 
            this.Datagridsummary.BackgroundColor = System.Drawing.Color.White;
            this.Datagridsummary.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Datagridsummary.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Datagridsummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Datagridsummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Datagridsummary.GridColor = System.Drawing.Color.Blue;
            this.Datagridsummary.Location = new System.Drawing.Point(0, 244);
            this.Datagridsummary.Margin = new System.Windows.Forms.Padding(4);
            this.Datagridsummary.Name = "Datagridsummary";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Datagridsummary.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Datagridsummary.RowHeadersWidth = 51;
            this.Datagridsummary.Size = new System.Drawing.Size(555, 377);
            this.Datagridsummary.TabIndex = 24;
            this.Datagridsummary.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Datagridsummary_CellClick);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnExportsummary);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 621);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(555, 62);
            this.panel5.TabIndex = 25;
            // 
            // btnExportsummary
            // 
            this.btnExportsummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnExportsummary.FlatAppearance.BorderSize = 0;
            this.btnExportsummary.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnExportsummary.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnExportsummary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportsummary.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportsummary.ForeColor = System.Drawing.Color.Black;
            this.btnExportsummary.Location = new System.Drawing.Point(6, 9);
            this.btnExportsummary.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportsummary.Name = "btnExportsummary";
            this.btnExportsummary.Size = new System.Drawing.Size(193, 44);
            this.btnExportsummary.TabIndex = 53;
            this.btnExportsummary.Text = "EXPORT SUMMARY";
            this.btnExportsummary.UseVisualStyleBackColor = false;
            this.btnExportsummary.Click += new System.EventHandler(this.btnExportsummary_Click);
            // 
            // panelgrid
            // 
            this.panelgrid.AutoScroll = true;
            this.panelgrid.BackColor = System.Drawing.Color.White;
            this.panelgrid.Controls.Add(this.PanelMasterNG);
            this.panelgrid.Controls.Add(this.panel6);
            this.panelgrid.Controls.Add(this.panel3);
            this.panelgrid.Controls.Add(this.panel2);
            this.panelgrid.Controls.Add(this.panel4);
            this.panelgrid.Controls.Add(this.panelListNG);
            this.panelgrid.Location = new System.Drawing.Point(-31, 33);
            this.panelgrid.Margin = new System.Windows.Forms.Padding(4);
            this.panelgrid.Name = "panelgrid";
            this.panelgrid.Size = new System.Drawing.Size(1341, 748);
            this.panelgrid.TabIndex = 1;
            // 
            // PanelMasterNG
            // 
            this.PanelMasterNG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelMasterNG.Controls.Add(this.PanelNGName);
            this.PanelMasterNG.Controls.Add(this.panel7);
            this.PanelMasterNG.Controls.Add(this.panelCategory);
            this.PanelMasterNG.Controls.Add(this.panel9);
            this.PanelMasterNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMasterNG.Location = new System.Drawing.Point(23, 56);
            this.PanelMasterNG.Margin = new System.Windows.Forms.Padding(4);
            this.PanelMasterNG.Name = "PanelMasterNG";
            this.PanelMasterNG.Size = new System.Drawing.Size(1313, 687);
            this.PanelMasterNG.TabIndex = 5;
            // 
            // PanelNGName
            // 
            this.PanelNGName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelNGName.Controls.Add(this.datagriddetail);
            this.PanelNGName.Controls.Add(this.panel10);
            this.PanelNGName.Controls.Add(this.panel8);
            this.PanelNGName.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelNGName.Location = new System.Drawing.Point(618, 0);
            this.PanelNGName.Margin = new System.Windows.Forms.Padding(4);
            this.PanelNGName.Name = "PanelNGName";
            this.PanelNGName.Size = new System.Drawing.Size(657, 685);
            this.PanelNGName.TabIndex = 11;
            // 
            // datagriddetail
            // 
            this.datagriddetail.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagriddetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.datagriddetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagriddetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datagriddetail.GridColor = System.Drawing.Color.Blue;
            this.datagriddetail.Location = new System.Drawing.Point(0, 244);
            this.datagriddetail.Margin = new System.Windows.Forms.Padding(4);
            this.datagriddetail.Name = "datagriddetail";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagriddetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.datagriddetail.RowHeadersWidth = 51;
            this.datagriddetail.Size = new System.Drawing.Size(655, 377);
            this.datagriddetail.TabIndex = 24;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.btnExportDetail);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel10.Location = new System.Drawing.Point(0, 621);
            this.panel10.Margin = new System.Windows.Forms.Padding(4);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(655, 62);
            this.panel10.TabIndex = 26;
            // 
            // btnExportDetail
            // 
            this.btnExportDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnExportDetail.FlatAppearance.BorderSize = 0;
            this.btnExportDetail.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnExportDetail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnExportDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportDetail.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportDetail.ForeColor = System.Drawing.Color.Black;
            this.btnExportDetail.Location = new System.Drawing.Point(449, 6);
            this.btnExportDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportDetail.Name = "btnExportDetail";
            this.btnExportDetail.Size = new System.Drawing.Size(193, 44);
            this.btnExportDetail.TabIndex = 53;
            this.btnExportDetail.Text = "EXPORT DETAIL";
            this.btnExportDetail.UseVisualStyleBackColor = false;
            this.btnExportDetail.Click += new System.EventHandler(this.btnExportDetail_Click);
            // 
            // panel8
            // 
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(655, 244);
            this.panel8.TabIndex = 23;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(23, 743);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1313, 5);
            this.panel6.TabIndex = 4;
            // 
            // FrmReportNG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1325, 815);
            this.Controls.Add(this.panelgrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmReportNG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmReportNG";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmReportNG_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelCategory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datagridsummary)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panelgrid.ResumeLayout(false);
            this.PanelMasterNG.ResumeLayout(false);
            this.PanelNGName.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datagriddetail)).EndInit();
            this.panel10.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelListNG;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cbLocation;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panelCategory;
        private System.Windows.Forms.DataGridView Datagridsummary;
        private System.Windows.Forms.Panel panelgrid;
        private System.Windows.Forms.Panel PanelMasterNG;
        private System.Windows.Forms.Panel PanelNGName;
        private System.Windows.Forms.DataGridView datagriddetail;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker date2;
        private System.Windows.Forms.DateTimePicker date1;
        private System.Windows.Forms.ComboBox cbNGCategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbNGType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnExportsummary;
        private System.Windows.Forms.Button btnExportDetail;
    }
}