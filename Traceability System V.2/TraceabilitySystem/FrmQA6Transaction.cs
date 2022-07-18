using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TraceabilitySystem.Dataacces;
using TraceabilitySystem.Entity;
using System.Threading;

namespace TraceabilitySystem
{
    public partial class FrmQA6Transaction : Form
    {
        public FrmQA6Transaction()
        {
            InitializeComponent();
        }
        Dataacces_QA6 dataacces_QA6 = new Dataacces_QA6();

        public string UniqueID;
        public string SerialEPC;
        public string EPC;
        public string Model1;
        public string Color1;
        public string Model2;
        public string Color2;
        public string Model3;
        public string Color3;
        public int model;
        public string timestart;


        WaitWnd.WaitWndFun waitForm = new WaitWnd.WaitWndFun();

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmQA6Transaction_Load(object sender, EventArgs e)
        {
            btnUpdate.Visible = false;
            DateTime now = DateTime.Now;
            if (timestart.Length != 10)
            {
                MessageBox.Show("Time Start Is Blank, Please Contact Administrator / IT");
                return;
            }
            txtStartTime.Text = timestart.Substring(4, 2) + "/" + timestart.Substring(2, 2) + "/20" + timestart.Substring(0, 2) + " " + timestart.Substring(6, 2) + ":" + timestart.Substring(8, 2);
            lblStarttime.Text = timestart;  // now.ToString("yyMMddHHmm") + "";
            txtUnique.Text = UniqueID;
            txtserialRFID.Text = SerialEPC;
            txtEPC.Text = EPC;
            if (model == 1)
            {
                txtModelName.Text = Model1;
                txtColor.Text = Color1;
            }
            else if (model == 2)
            {
                txtModelName.Text = Model2;
                txtColor.Text = Color2;
            }
            else
            {
                txtModelName.Text = Model3;
                txtColor.Text = Color3;
            }
            txtOperatorName.Text = DBConnections.Name;
            panelNG.Enabled = false;
            if (System.IO.File.Exists(Application.StartupPath + @"\\Asset\\image\\NGArea.png"))
            {
                
                picNGArea.Image = Image.FromFile(Application.StartupPath + @"\\Asset\\image\\NGArea.png");
                picNGArea.SizeMode = PictureBoxSizeMode.StretchImage;
              
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            txttimeNow.Text = "" + now.ToString("dd/MM/yyyy HH:mm") + "";
        }
        bool OK = false;
        bool NG = false;
        bool dispose = false;
        private void btnOK_Click(object sender, EventArgs e)
        {

            btnNG.BackColor = System.Drawing.Color.Red;
            btnNG.ForeColor = System.Drawing.Color.White;
            btnOK.ForeColor = System.Drawing.Color.LightGray;
            btnOK.BackColor = System.Drawing.Color.Green;
            btnOK.FlatAppearance.BorderSize = 5;
            btnOK.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            btnNG.FlatAppearance.BorderSize = 0;
            panelNG.Enabled = false;
            btnDispose.FlatAppearance.BorderSize = 0;
            btnDispose.BackColor = System.Drawing.Color.Yellow;
            NG = false;
            OK = true;
            dispose = false;
        }

        private void btnNG_Click(object sender, EventArgs e)
        {
            btnNG.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
            btnNG.ForeColor = System.Drawing.Color.LightGray;
            btnOK.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
            btnOK.ForeColor = System.Drawing.Color.White;
            btnNG.FlatAppearance.BorderSize = 5;
            btnNG.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            btnOK.FlatAppearance.BorderSize = 0;
            panelNG.Enabled = true;
            txtNGType.Text = "~Select Here~";
            btnDispose.FlatAppearance.BorderSize = 0;
            btnDispose.BackColor = System.Drawing.Color.Yellow;
            GetProcessDest();
            GetNGArea();
            cbNGArea.SelectedIndex = 0;
            dataacces_QA6.showgrid(DgvNGList, txtUnique.Text,"QA6");
            NG = true;
            OK = false;
            dispose = false;
        }

        private void btnDispose_Click(object sender, EventArgs e)
        {
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
            btnOK.ForeColor = System.Drawing.Color.White;
            btnNG.BackColor = System.Drawing.Color.Red;
            btnNG.ForeColor = System.Drawing.Color.White;
            btnNG.FlatAppearance.BorderSize = 0;
            panelNG.Enabled = false;

            btnDispose.FlatAppearance.BorderSize = 5;
            btnDispose.BackColor = System.Drawing.Color.FromArgb(192, 192, 0);
            NG = false;
            OK = false;
            dispose = true;
        }
        private void GetProcessDest()
        {
            try
            {
                Dictionary<string, string> test = new Dictionary<string, string>();

                DataTable dataTable = new DataTable();
                dataTable = dataacces_QA6.GetDestination("QA6");
                cbProcessDest.DataSource = null;
                test.Add("", "~Select Here~");
                if (dataTable.Rows.Count > 0)
                {
                    IDictionary<string, string> numberNames = new Dictionary<string, string>();
                    int row = 0;
                    int totrow = dataTable.Rows.Count - 1;
                    while (row <= totrow)
                    {
                        test.Add(dataTable.Rows[row][0].ToString(), dataTable.Rows[row][1].ToString());
                        row++;
                    }
                    cbProcessDest.DataSource = new BindingSource(test, null);
                    cbProcessDest.DisplayMember = "Value";
                    cbProcessDest.ValueMember = "Key";
                }
                else
                {
                    MessageBox.Show("Data Not Found!");
                }
            }
            catch (Exception ex)
            {
                savelog("Error  GetProcessDest!: + " + ex.ToString());
                MessageBox.Show("Error GetProcessDest!" + ex.ToString());
            }
        }
        private void GetNGArea()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = dataacces_QA6.GetNGArea();
                if (dataTable.Rows.Count > 0)
                {
                    int row = 0;
                    int totrow = dataTable.Rows.Count - 1;
                    cbNGArea.Items.Clear();
                    cbNGArea.Items.Add("~Select Here~");
                    while (row <= totrow)
                    {
                        cbNGArea.Items.Add(dataTable.Rows[row][0].ToString());
                        row++;
                    }
                }
                else
                {
                    MessageBox.Show("NG Area Not Found, Please Contact Administrator");
                    panelNG.Enabled = false;
                    return;
                }


            }
            catch (Exception ex)
            {
                savelog("Error  GetProcessDest!: + " + ex.ToString());
                MessageBox.Show("Error GetProcessDest!" + ex.ToString());
            }
        }
        public void savelog(string description)
        {
            string tab = "\t";
            String header = "Datetime" + tab + "Description";
            String str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + tab + description;
            AppSettings.SaveLogs(str, header);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmNG frm = new FrmNG();
            frm.Location = "QA6";
            frm.ShowDialog();
            if (Dataacces_NG.NameNG == null)
            {
                txtNGType.Text = "~Select Here~";
            }
            else
            {
                txtNGType.Text = Dataacces_NG.NameNG;
            }
        }
        ListNG listNg = new ListNG();
        private void btnAddToNG_Click(object sender, EventArgs e)
        {
            listNg.UniqueID = txtUnique.Text;
            listNg.NGType = txtNGType.Text;
            listNg.Destination = ((KeyValuePair<string, string>)cbProcessDest.SelectedItem).Value;
            listNg.NGArea = cbNGArea.Text;
            if (listNg.NGArea == "~Select Here~")
            {
                MessageBox.Show("Please Select NG Area");
                return;
            }
            if (listNg.NGType == "~Select Here~")
            {
                MessageBox.Show("Please Select NG Type");
                return;
            }
            if (listNg.Destination == "~Select Here~")
            {
                MessageBox.Show("Please Select Process Destination");
                return;
            }
            
            listNg.Process = "QA6";
            string location = "";
            string[] lines = ((KeyValuePair<string, string>)cbProcessDest.SelectedItem).Key.Split('-');
            location = lines[1].ToString();
            listNg.Location = location;
            if (dataacces_QA6.InsertoListNG(listNg))
            {
                dataacces_QA6.showgrid(DgvNGList, txtUnique.Text,"QA6");
                cbProcessDest.SelectedIndex = 0;
                txtNGType.Text = "~Select Here~";
                cbNGArea.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Add NG Failed!, Please Logout Application First");
            }
        }

        private void btnDeleteNG_Click(object sender, EventArgs e)
        {
            if (DgvNGList.SelectedCells.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to delete the data?", "attention", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int IDNGList = Convert.ToInt32(DgvNGList.SelectedCells[1].Value.ToString());
                    if (dataacces_QA6.DeleteNGList(txtUnique.Text, IDNGList, "QA6") == true)
                    {
                        MessageBox.Show("Delete NG List succesfully", "Success");
                        dataacces_QA6.showgrid(DgvNGList, txtUnique.Text, "QA6");
                    }
                    else
                    {
                        MessageBox.Show("Delete NG List Failed", "Failed");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select the data to be deleted", "Failed");
                return;
            }
        }

        private void btnChangeModelColor_Click(object sender, EventArgs e)
        {
            if (btnChangeModel.Text == "Change Model and Color")
            {
               
                    if (model == 3)
                    {
                        MessageBox.Show("Change Model Already 3!");
                        return;
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Change Model and Color?", "attention", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            
                            txtModelName.Enabled = true;
                            txtColor.Enabled = true;
                            txtModelName.BackColor = Color.White;
                            txtColor.BackColor = Color.White;
                            txtModelName.Select();
                            GetDataModel();
                            GetDataColor();
                            btnChangeModel.Text = "Cancel";

                           // btnUpdate.Visible = true;
                        }
                    }
            }
            else
            {
                btnUpdate.Visible = false;
                btnChangeModel.Text = "Change Model and Color";
                txtModelName.Enabled = false;
                txtColor.Enabled = false;
                txtModelName.BackColor = Color.LightGray;
                txtColor.BackColor = Color.LightGray;
                if (model == 1)
                {
                    txtModelName.Text = Model1;
                    txtColor.Text = Color1;
                }
                else if (model == 2)
                {
                    txtModelName.Text = Model2;
                    txtColor.Text = Color2;
                }
                else if (model == 3)
                {
                    txtModelName.Text = Model3;
                    txtColor.Text = Color3;
                }
                //txtModelName.Text = Model;
                //txtColor.Text = color;
            }
        }

        Dataacces_Model dataacces_Model = new Dataacces_Model();
        Dataacces_Color dataacces_Color = new Dataacces_Color();
        private void GetDataModel()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = dataacces_Model.GetAll();
                if (dataTable.Rows.Count > 0)
                {
                    txtModelName.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtModelName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        DataCollection.Add(row[2].ToString());
                    }
                    txtModelName.AutoCompleteCustomSource = DataCollection;
                }
            }
            catch (Exception ex)
            {
                savelog("Error Get Data Model!: + " + ex.ToString());
                MessageBox.Show("Error Get Data Model!" + ex.ToString());
            }
        }
        private void GetDataColor()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = dataacces_Color.GetAll();
                if (dataTable.Rows.Count > 0)
                {
                    txtColor.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtColor.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        DataCollection.Add(row[2].ToString() + "-" + row[3].ToString());
                    }
                    txtColor.AutoCompleteCustomSource = DataCollection;
                }
            }
            catch (Exception ex)
            {
                savelog("Error Get Data Color!: + " + ex.ToString());
                MessageBox.Show("Error Get Data Color!" + ex.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (AppSettings.Authority == "Leader" || AppSettings.Authority == "Administrator")
            {
                if (txtModelName.Enabled == true)
                {
                    //Check Master Model Master Product

                    //if (txtModelName.Text == Model && txtColor.Text == color)
                    //{
                    //    MessageBox.Show("Model and Color Same");
                    //    return;
                    //}
                    if (model == 1)
                    {
                        if (txtModelName.Text == Model1 && txtColor.Text == Color1)
                        {
                            MessageBox.Show("Model and Color Same");
                            return;
                        }
                    }
                    else if (model == 2)
                    {
                        if (txtModelName.Text == Model2 && txtColor.Text == Color2)
                        {
                            MessageBox.Show("Model and Color Same");
                            return;
                        }
                    }
                    else if (model == 3)
                    {
                        if (txtModelName.Text == Model3 && txtColor.Text == Color3)
                        {
                            MessageBox.Show("Model and Color Same");
                            return;
                        }
                    }

                    if (!dataacces_QA6.CheckModel(txtModelName.Text))
                    {
                        MessageBox.Show("Model not found!");
                        return;
                    }
                    if (!dataacces_QA6.CheckColor(txtColor.Text))
                    {
                        MessageBox.Show("Color not found!");
                        return;
                    }
                    if (dataacces_QA6.UpdateModel(txtUnique.Text, model, txtModelName.Text, txtColor.Text))
                    {
                        model = model + 1;
                        //Model = txtModelName.Text;
                        //color = txtColor.Text;
                        if (model == 1)
                        {
                            Model1 = txtModelName.Text;
                            Color1 = txtColor.Text;
                        }
                        else if (model == 2)
                        {
                            Model2 = txtModelName.Text;
                            Color2 = txtColor.Text;
                        }
                        else if (model == 3)
                        {
                            Model3 = txtModelName.Text;
                            Color3 = txtColor.Text;
                        }
                        MessageBox.Show("Update Model and Color Success");
                        txtModelName.Enabled = false;
                        txtColor.Enabled = false;
                        btnChangeModel.Text = "Change Model and Color";
                    }
                    else
                    {
                        MessageBox.Show("Change Model and Color Failed!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Can't Access!");
                return;
            }
        }
        History history = new History();
        Ur21 ur = new Ur21();
        private void BtnConfirmn_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUnique.Text != "")
                {
                    if (OK == false && NG == false && dispose == false)
                    {
                        MessageBox.Show("Please Select Status Process (OK/NG/Dispose");
                        return;
                    }
                    if (NG == true)
                    {
                        if (DgvNGList.Rows.Count != 0)
                        {
                            if (DgvNGList.Rows[0].Cells[2].Value.ToString() == "")
                            {
                                MessageBox.Show("Please Select NG and Destination NG");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please Select NG and Destination NG");
                            return;
                        }
                    }
                    if (btnChangeModel.Text == "Cancel")
                    {
                        if (txtModelName.Enabled == true)
                        {
                            //Check Master Model Master Product
                            if (model == 1)
                            {
                                if (txtModelName.Text == Model1 && txtColor.Text == Color1)
                                {
                                    MessageBox.Show("Model and Color Same");
                                    return;
                                }
                            }
                            else if (model == 2)
                            {
                                if (txtModelName.Text == Model2 && txtColor.Text == Color2)
                                {
                                    MessageBox.Show("Model and Color Same");
                                    return;
                                }
                            }
                            else if (model == 3)
                            {
                                if (txtModelName.Text == Model3 && txtColor.Text == Color3)
                                {
                                    MessageBox.Show("Model and Color Same");
                                    return;
                                }
                            }

                            if (!dataacces_QA6.CheckModel(txtModelName.Text))
                            {
                                MessageBox.Show("Model not found!");
                                return;
                            }
                            if (!dataacces_QA6.CheckColor(txtColor.Text))
                            {
                                MessageBox.Show("Color not found!");
                                return;
                            }

                            // Show user dan password sebelum execute
                        }
                    }


                    connect_Text = MyConst.CONNECT;
                    txtMessage.Text = "Connection to UR21";
                    txtMessage.ForeColor = System.Drawing.Color.Black;
                    ConnectAction();
                    txtMessage.Text = "Read label tag";
                    txtMessage.ForeColor = System.Drawing.Color.Black;
                    Tag tIn = new Tag();
                    if (ur.ReadOneTag(ref tIn))
                    {

                        if (tIn.Uii == null)
                        {
                            MessageBox.Show("Please read RFID Label");
                        }
                        else
                        {
                            string epc = General.gHexToString(tIn.Uii);
                            if (txtEPC.Text == epc)
                            {

                                txtMessage.Text = "";
                                txtMessage.ForeColor = System.Drawing.Color.Azure;
                                DialogResult dialogResult = MessageBox.Show("Are you sure to comfirm data?", "attention", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {

                                    if (btnChangeModel.Text == "Cancel")
                                    {
                                        new FrmApproved().ShowDialog();
                                        if (AppSettings.Approved == true)
                                        {

                                            if (dataacces_QA6.UpdateModel(txtUnique.Text, model, txtModelName.Text, txtColor.Text))
                                            {
                                                model = model + 1;
                                                if (model == 1)
                                                {
                                                    Model1 = txtModelName.Text;
                                                    Color1 = txtColor.Text;
                                                }
                                                else if (model == 2)
                                                {
                                                    Model2 = txtModelName.Text;
                                                    Color2 = txtColor.Text;
                                                    history.App_Model2 = AppSettings.ApprovedName;

                                                }
                                                else if (model == 3)
                                                {
                                                    Model3 = txtModelName.Text;
                                                    Color3 = txtColor.Text;
                                                    history.App_Model3 = AppSettings.ApprovedName;
                                                }
                                                // MessageBox.Show("Update Model and Color Success");
                                                txtModelName.Enabled = false;
                                                txtColor.Enabled = false;
                                                btnChangeModel.Text = "Change Model and Color";
                                            }
                                            else
                                            {
                                                MessageBox.Show("Change Model and Color Failed!");
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            btnChangeModel.Text = "Change Model and Color";
                                            txtModelName.Enabled = false;
                                            txtColor.Enabled = false;
                                            txtColor.BackColor = Color.LightGray;
                                            txtModelName.BackColor = Color.LightGray;
                                            if (model == 1)
                                            {
                                                txtModelName.Text = Model1;
                                                txtColor.Text = Color1;
                                            }
                                            else if (model == 2)
                                            {
                                                txtModelName.Text = Model2;
                                                txtColor.Text = Color2;
                                            }
                                            else if (model == 3)
                                            {
                                                txtModelName.Text = Model3;
                                                txtColor.Text = Color3;
                                            }
                                            return;
                                        }
                                    }

                                    if (dispose == true)
                                    {
                                        new FrmApproved().ShowDialog();
                                        if (AppSettings.Approved == false)
                                        {
                                            return;
                                        }
                                        history.App_Dispose = AppSettings.ApprovedName;
                                    }

                                    history.History_Unique = txtUnique.Text;
                                    history.History_Model1 = Model1;
                                    history.History_Color1 = Color1;
                                    history.History_Model2 = Model2;
                                    history.History_Color2 = Color2;
                                    history.History_Model3 = Model3;
                                    history.History_Color3 = Color3;
                                    #region GAK KEPAKE
                                    //if (model == 1)
                                    //{
                                    //    history.History_Model1 = txtModelName.Text;
                                    //    history.History_Color1 = txtColor.Text;
                                    //    history.History_Model2 = "";
                                    //    history.History_Color2 = "";
                                    //    history.History_Model3 = "";
                                    //    history.History_Color3 = "";
                                    //}
                                    //else if (model == 2)
                                    //{
                                    //    history.History_Model1 = "";
                                    //    history.History_Color1 = "";
                                    //    history.History_Model2 = txtModelName.Text;
                                    //    history.History_Color2 = txtColor.Text;
                                    //    history.History_Model3 = "";
                                    //    history.History_Color3 = "";
                                    //}
                                    //else if (model == 3)
                                    //{
                                    //    history.History_Model1 = "";
                                    //    history.History_Color1 = "";
                                    //    history.History_Model2 = "";
                                    //    history.History_Color2 = "";
                                    //    history.History_Model3 = txtModelName.Text;
                                    //    history.History_Color3 = txtColor.Text;
                                    //}
                                    #endregion
                                    history.History_TimeIN = lblStarttime.Text;
                                    if (OK == true)
                                        history.History_TimeOUT = DateTime.Now.ToString("yyMMddHHmm");
                                    else
                                        history.History_TimeOUT = "";

                                    history.History_PIC = txtOperatorName.Text;
                                    if (OK == true)
                                        history.History_Status = "OK";
                                    else if (NG == true)
                                        history.History_Status = "NG";
                                    else if (dispose == true)
                                        history.History_Status = "DS";
                                    history.History_ProcessLine = "QA6";
                                    history.History_ReceiveLine = "";
                                    history.model = model;
                                    history.History_SerianRFID = txtserialRFID.Text;
                                    if (dataacces_QA6.InserttoHistory(history))
                                    {
                                        MessageBox.Show("Save Data Success!");
                                        Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Save Data Failed!");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Different RFID Label");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please Back to form scan RFID");
                }
             }
            catch (Exception)
            {


            }
            finally
            {
                ur.DisconnectUR21();
                Connected = false;
                // Change btn text to CONNECT.
                Connect_Text = MyConst.CONNECT;
            }
        }

        private string connect_Text;
        public string Connect_Text
        {
            get { return connect_Text; }
            set { connect_Text = value; }
        }
        private bool connected;
        public bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }
        private string comPort;
        public string ComPort
        {
            get { return comPort; }
            set { comPort = value; }
        }

        private void ConnectAction()
        {
            try
            {
                // Check COM port, if ok, connect to it.
                if (comPort == "")
                {
                    MessageBox.Show(MyConst.WARNING + Environment.NewLine + "COM port empty!");
                    return;
                }

                byte bPort = byte.Parse(AppSettings.comPort);

                if (connect_Text == MyConst.CONNECT)
                {


                    // Start RFID reading.
                    if (ur.ConnectUR21(bPort))
                    {
                        // Change btn text to DISCONNECT.
                        Connect_Text = MyConst.DISCONNECT;
                        Connected = true;

                    }
                    else
                    {
                        ur.DisconnectUR21();
                        txtMessage.Text = "Error[Connection UR21 Failed, Please Check Conection PC - UR21]";
                        txtMessage.BackColor = System.Drawing.Color.Yellow;
                        Connected = false;

                        // Change btn text to CONNECT.
                        Connect_Text = MyConst.CONNECT;
                    }
                }
                else
                {

                    // Disconnect from UR21.
                    ur.DisconnectUR21();
                    txtMessage.Text = "Error[Connection UR21 Failed, Please Check Conection PC - UR21]";
                    txtMessage.BackColor = System.Drawing.Color.Yellow;
                    Connected = false;
                    // Change btn text to CONNECT.
                    Connect_Text = MyConst.CONNECT;
                }
            }
            catch (Exception e)
            {
                savelog("Error ConnectAction : + " + e.ToString());
                MessageBox.Show("Error ConnectAction : + " + Environment.NewLine + e.Message);
            }
        }

        private void linkPreviewImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmPreviewImage frm = new FrmPreviewImage();
            frm.ShowDialog();
        }

        private void cbNGArea_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            if (cbx != null)
            {
                // Always draw the background
                e.DrawBackground();

                // Drawing one of the items?
                if (e.Index >= 0)
                {
                    // Set the string alignment.  Choices are Center, Near and Far
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    // Set the Brush to ComboBox ForeColor to maintain any ComboBox color settings
                    // Assumes Brush is solid
                    Brush brush = new SolidBrush(cbx.ForeColor);

                    // If drawing highlighted selection, change brush
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        brush = SystemBrushes.HighlightText;

                    // Draw the string
                    e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
                }
            }
        }
    }
}
