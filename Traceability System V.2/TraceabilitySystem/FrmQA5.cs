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
    public partial class FrmQA5 : Form
    {

        public FrmQA5()
        {
            InitializeComponent();
        }
        public string Model1;
        public string Color1;
        public string Model2;
        public string Color2;
        public string Model3;
        public string Color3;
        Ur21 ur = new Ur21();
        Dataacces_QA5 dataacces_QA5 = new Dataacces_QA5();
        private void FrmMasterModel_Load(object sender, EventArgs e)
        {
            txtOperatorName.Text = DBConnections.Name;
            connect_Text = MyConst.CONNECT;
            ConnectAction();
            txtUnique.Text = "";
            txtRFIDSerial.Text = "";
            txtEPC.Text = "";
            txtModelName.Text = "";
            txtColorName.Text = "";
        }
        private string connect_Text;
        public string Connect_Text
        {
            get { return connect_Text; }
            set { connect_Text = value; }
        }
        private string scanTag;
        public string ScanTag
        {
            get { return scanTag; }
            set { scanTag = value; }
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


        WaitWnd.WaitWndFun waitForm = new WaitWnd.WaitWndFun();

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ur.DisconnectUR21();
            Connected = false;
            timGetRFIDData.Stop();
            timGetRFIDData.Enabled = false;
            // Change btn text to CONNECT.
            Connect_Text = MyConst.CONNECT;
            timGetRFIDData.Dispose();
            Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtUnique.Text == "" && txtEPC.Text == "")
            {
                MessageBox.Show("Please read Label RFID!");
                return;
            }

            if (dataacces_QA5.CheckUniqueStatusOK(txtUnique.Text))
            {
                MessageBox.Show("UniqueID Already Finish!");
                return;
            }
            if (dataacces_QA5.CheckUniqueStatusNG(txtUnique.Text))
            {
                MessageBox.Show("UniqueID on process Approved!");
                return;
            }

            ur.DisconnectUR21();
            Connected = false;
            timGetRFIDData.Stop();
            timGetRFIDData.Enabled = false;
            // Change btn text to CONNECT.
            Connect_Text = MyConst.CONNECT;
            timGetRFIDData.Dispose();
            string timestart = "";
            DateTime now = DateTime.Now;
            if (dataacces_QA5.CheckTimeStart(txtUnique.Text)) 
            {
                timestart = now.ToString("yyMMddHHmm") + "";
            }
            else
            {
                timestart = dataacces_QA5.GetTimeStart(txtUnique.Text);
                if (timestart == null)
                {
                    timestart = now.ToString("yyMMddHHmm") + "";
                }
            }
            FrmQA5Transaction frmQA5Transaction = new FrmQA5Transaction();
            frmQA5Transaction.UniqueID = txtUnique.Text;
            frmQA5Transaction.SerialEPC = txtRFIDSerial.Text;
            frmQA5Transaction.EPC = txtEPC.Text;
            frmQA5Transaction.Model1 = Model1;
            frmQA5Transaction.Color1 = Color1;
            frmQA5Transaction.timestart = timestart;
            frmQA5Transaction.Model2 = Model2;
            frmQA5Transaction.Color2 = Color2;

            frmQA5Transaction.Model3 = Model3;
            frmQA5Transaction.Color3 = Color3;
            frmQA5Transaction.model = model;
            frmQA5Transaction.ShowDialog();
            txtEPC.Text = "";
            txtUnique.Text = "";
            txtModelName.Text = "";
            txtColorName.Text = "";
            txtRFIDSerial.Text = "";
            connect_Text = MyConst.CONNECT;
            ConnectAction();

        }
        int model = 0;
        private void timGetRFIDData_Tick(object sender, EventArgs e)
        {
            try
            {

                timGetRFIDData.Stop();
                timGetRFIDData.Enabled = false;
                //  txtEPC.Text = "EPC : " + Ur21.EncodeData;
                #region bikinsendiri
                ScanTagAction();
                #endregion
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.ToString());
            }
            finally
            {
                timGetRFIDData.Enabled = true;
                timGetRFIDData.Start();
            }
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
                    if (timGetRFIDData.Enabled)
                        timGetRFIDData.Stop();

                    // Start RFID reading.
                    if (ur.ConnectUR21(bPort))
                    {
                        // Change btn text to DISCONNECT.
                        Connect_Text = MyConst.DISCONNECT;
                        Connected = true;
                        timGetRFIDData.Interval = Convert.ToInt32(TimeSpan.FromSeconds(1).TotalMilliseconds); ;
                        timGetRFIDData.Enabled = true;
                        timGetRFIDData.Start();
                    }
                    else
                    {
                        ur.DisconnectUR21();
                        txtMessage.Text = "Error[Connection UR21 Failed, Please Check Conection PC - UR21]";
                        txtMessage.BackColor = Color.Yellow;
                        txtEPC.Text = "";
                        txtUnique.Text = "";
                        txtModelName.Text = "";
                        txtColorName.Text = "";
                        txtRFIDSerial.Text = "";
                        Connected = false;
                        timGetRFIDData.Stop();
                        timGetRFIDData.Enabled = false;
                        // Change btn text to CONNECT.
                        Connect_Text = MyConst.CONNECT;
                    }
                }
                else
                {

                    // Disconnect from UR21.
                    ur.DisconnectUR21();
                    txtMessage.Text = "Error[Connection UR21 Failed, Please Check Conection PC - UR21]";
                    txtMessage.BackColor = Color.Yellow;
                    txtEPC.Text = "";
                    txtUnique.Text = "";
                    txtModelName.Text = "";
                    txtColorName.Text = "";
                    txtRFIDSerial.Text = "";
                    Connected = false;
                    timGetRFIDData.Stop();
                    timGetRFIDData.Enabled = false;
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
        private void ScanTagAction()
        {
            try
            {
                Tag tIn = new Tag();
                if (ur.ReadOneTag(ref tIn))
                {
                    // Display read tag.
                    if (Ur21.ReadCount > 1)
                    {
                        txtMessage.Text = "Error[Multiple RFID Label are detected. Please read only one RFID Label]";
                        txtMessage.BackColor = Color.Yellow;
                        Ur21.ReadCount = 0;
                        txtEPC.Text = "";
                        txtRFIDSerial.Text = "";
                        txtUnique.Text = "";
                        txtModelName.Text = "";
                        txtColorName.Text = "";
                    }
                    else
                    {
                        if (tIn.Uii == null)
                        {
                            txtEPC.Text = tIn.Uii;
                            txtRFIDSerial.Text = "";
                            txtUnique.Text = "";
                            txtModelName.Text = "";
                            txtColorName.Text = "";
                            txtMessage.Text = "Please read RFID Label";
                            txtMessage.BackColor = Color.White;
                        }
                        else
                        {
                            string epc = General.gHexToString(tIn.Uii);
                            
                            if (epc.Length == 12)
                            {
                                if (IsNumeric(epc))
                                {
                                    DataTable getProduct = dataacces_QA5.GetDataProduct(epc);
                                    if (getProduct.Rows.Count > 0)
                                    {
                                        if (getProduct.Rows[0]["Status"].ToString() == "0")
                                        {
                                            if (getProduct.Rows[0]["Status_Process"].ToString() == "QA6")
                                            {
                                                txtMessage.Text = "[Label RFID For Transaction QA6]";
                                                txtMessage.BackColor = Color.Yellow;
                                                txtEPC.Text = "";
                                                txtUnique.Text = "";
                                                txtRFIDSerial.Text = "";
                                                txtModelName.Text = "";
                                                txtColorName.Text = "";
                                            }
                                            else if (getProduct.Rows[0]["Status_Process"].ToString() == "DS")
                                            {
                                                txtMessage.Text = "[Label RFID Has Been Dispose]";
                                                txtMessage.BackColor = Color.Yellow;
                                                txtEPC.Text = "";
                                                txtUnique.Text = "";
                                                txtRFIDSerial.Text = "";
                                                txtModelName.Text = "";
                                                txtColorName.Text = "";
                                            }
                                            else
                                            {
                                                Model1 = getProduct.Rows[0]["Id_Model1"].ToString();
                                                Color1 = getProduct.Rows[0]["Id_Color1"].ToString();
                                                Model2 = getProduct.Rows[0]["Id_Model2"].ToString();
                                                Color2 = getProduct.Rows[0]["Id_Color2"].ToString();
                                                Model3 = getProduct.Rows[0]["Id_Model3"].ToString();
                                                Color3 = getProduct.Rows[0]["Id_Color3"].ToString();

                                                txtEPC.Text = epc;
                                                txtUnique.Text = getProduct.Rows[0][0].ToString();
                                                if (getProduct.Rows[0]["Id_Model3"].ToString() != null && getProduct.Rows[0]["Id_Model3"].ToString() != "")
                                                {
                                                    txtModelName.Text = getProduct.Rows[0]["Id_Model3"].ToString();
                                                    model = 3;
                                                }
                                                else if (getProduct.Rows[0]["Id_Model2"].ToString() != null && getProduct.Rows[0]["Id_Model2"].ToString() != "")
                                                {
                                                    txtModelName.Text = getProduct.Rows[0]["Id_Model2"].ToString();
                                                    model = 2;
                                                }
                                                else
                                                {
                                                    txtModelName.Text = getProduct.Rows[0]["Id_Model1"].ToString();
                                                    model = 1;
                                                }
                                                if (getProduct.Rows[0]["Id_Color3"].ToString() != null && getProduct.Rows[0]["Id_Color3"].ToString() != "")
                                                {
                                                    txtColorName.Text = getProduct.Rows[0]["Id_Color3"].ToString();
                                                }
                                                else if (getProduct.Rows[0]["Id_Color2"].ToString() != null && getProduct.Rows[0]["Id_Color2"].ToString() != "")
                                                {
                                                    txtColorName.Text = getProduct.Rows[0]["Id_Color2"].ToString();
                                                }
                                                else
                                                {
                                                    txtColorName.Text = getProduct.Rows[0]["Id_Color1"].ToString();
                                                }
                                                txtRFIDSerial.Text = epc.Substring(6, 6);
                                                txtMessage.Text = "";
                                                txtMessage.BackColor = Color.White;
                                                scanTag = tIn.Uii;
                                            }
                                        }
                                        else
                                        {

                                            if (getProduct.Rows[0]["Status_Process"].ToString() == "DS")
                                            {
                                                txtMessage.Text = "[Label RFID Has Been Dispose]";
                                                txtMessage.BackColor = Color.Yellow;
                                                txtEPC.Text = "";
                                                txtUnique.Text = "";
                                                txtRFIDSerial.Text = "";
                                                txtModelName.Text = "";
                                                txtColorName.Text = "";
                                            }
                                            else
                                            {
                                                txtMessage.Text = "[Label RFID already finish]";
                                                txtMessage.BackColor = Color.Yellow;
                                                txtEPC.Text = "";
                                                txtUnique.Text = "";
                                                txtRFIDSerial.Text = "";
                                                txtModelName.Text = "";
                                                txtColorName.Text = "";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        txtMessage.Text = "Error[Label RFID Not Register]";
                                        txtMessage.BackColor = Color.Yellow;
                                        txtEPC.Text = "";
                                        txtUnique.Text = "";
                                        txtModelName.Text = "";
                                        txtColorName.Text = "";
                                    }
                                }
                                else
                                {
                                    txtMessage.Text = "Error[Wrong RFID Label are detected]";
                                    txtMessage.BackColor = Color.Yellow;
                                    txtEPC.Text = "";
                                    txtUnique.Text = "";
                                    txtModelName.Text = "";
                                    txtColorName.Text = "";
                                }

                            }
                            else
                            {
                                txtMessage.Text = "Error[Wrong RFID Label are detected]";
                                txtMessage.BackColor = Color.Yellow;
                                txtEPC.Text = "";
                                txtUnique.Text = "";
                                txtModelName.Text = "";
                                txtColorName.Text = "";
                            }

                        }

                    }
                }
                else
                {
                    if (Ur21.ErrorMsg != null)
                    {
                        txtMessage.Text = Ur21.ErrorMsg;
                        txtMessage.BackColor = Color.Yellow;
                        Ur21.ErrorMsg = null;
                        txtEPC.Text = "";
                        txtUnique.Text = "";
                        txtModelName.Text = "";
                        txtColorName.Text = "";
                    }
                }
            }
            catch (Exception e)
            {
                txtEPC.Text = "";
                txtRFIDSerial.Text = "";
                txtUnique.Text = "";
                txtModelName.Text = "";
                txtColorName.Text = "";
                savelog("Error ScanTagAction : + " + e.ToString());
                MessageBox.Show("Error ScanTagAction : + " + Environment.NewLine + e.Message);
            }
        }
        public static bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        public void savelog(string description)
        {
            string tab = "\t";
            String header = "Datetime" + tab + "Description";
            String str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + tab + description;
            AppSettings.SaveLogs(str, header);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            DateTime now = DateTime.Now;
            txttimeNow.Text = "" + now.ToString("dd/MM/yyyy HH:mm") + "";
        }

        private void btnTransactionList_Click(object sender, EventArgs e)
        {
           
            ur.DisconnectUR21();
            Connected = false;
            timGetRFIDData.Stop();
            timGetRFIDData.Enabled = false;
            // Change btn text to CONNECT.
            Connect_Text = MyConst.CONNECT;
            timGetRFIDData.Dispose();
            txtEPC.Text = "";
            txtRFIDSerial.Text = "";
            txtUnique.Text = "";
            txtModelName.Text = "";
            txtColorName.Text = "";
            FrmTrasactionListQA5 frm = new FrmTrasactionListQA5();
            frm.Location = "QA5";
            frm.ShowDialog();
            connect_Text = MyConst.CONNECT;
            ConnectAction();
        }

        private void PanelUtama_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
