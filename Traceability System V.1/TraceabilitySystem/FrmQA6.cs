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
    public partial class FrmQA6 : Form
    {
        public FrmQA6()
        {
            InitializeComponent();
        }
        Dataacces_QA6 dataacces_QA6 = new Dataacces_QA6();


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        WaitWnd.WaitWndFun waitForm = new WaitWnd.WaitWndFun();

        
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            DateTime now = DateTime.Now;
            txttimeNow.Text = "" + now.ToString("dd/MM/yyyy HH:mm") + "";
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


        Ur21 ur = new Ur21();
        public string Model1;
        public string Color1;
        public string Model2;
        public string Color2;
        public string Model3;
        public string Color3;
        private void FrmQA6_Load(object sender, EventArgs e)
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

        public void savelog(string description)
        {
            string tab = "\t";
            String header = "Datetime" + tab + "Description";
            String str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + tab + description;
            AppSettings.SaveLogs(str, header);
        }

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
        int model;
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
                        txtMessage.Text = "Error [Multiple RFID Label are detected. Please read only one RFID Label]";
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
                                    DataTable getProduct = dataacces_QA6.GetDataProduct(epc);
                                    if (getProduct.Rows.Count > 0)
                                    {
                                        if (getProduct.Rows[0]["Status"].ToString() == "0")
                                        {
                                            if (getProduct.Rows[0]["Status_Process"].ToString() == "QA5")
                                            {
                                                txtMessage.Text = "[Label RFID For Transaction QA5]";
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
                                                txtEPC.Text = epc;
                                                txtUnique.Text = getProduct.Rows[0][0].ToString();
                                                Model1 = getProduct.Rows[0]["History_Model1"].ToString();
                                                Color1 = getProduct.Rows[0]["History_Color1"].ToString();
                                                Model2 = getProduct.Rows[0]["History_Model2"].ToString();
                                                Color2 = getProduct.Rows[0]["History_Color2"].ToString();
                                                Model3 = getProduct.Rows[0]["History_Model3"].ToString();
                                                Color3 = getProduct.Rows[0]["History_Color3"].ToString();


                                                if (getProduct.Rows[0]["History_Model3"].ToString() != null && getProduct.Rows[0]["History_Model3"].ToString() != "")
                                                {
                                                    txtModelName.Text = getProduct.Rows[0]["History_Model3"].ToString();
                                                    model = 3;
                                                }
                                                else if (getProduct.Rows[0]["History_Model2"].ToString() != null && getProduct.Rows[0]["History_Model2"].ToString() != "")
                                                {
                                                    txtModelName.Text = getProduct.Rows[0]["History_Model2"].ToString();
                                                    model = 2;
                                                }
                                                else
                                                {
                                                    txtModelName.Text = getProduct.Rows[0]["History_Model1"].ToString();
                                                    model = 1;
                                                }
                                                if (getProduct.Rows[0]["History_Color3"].ToString() != null && getProduct.Rows[0]["History_Color3"].ToString() != "")
                                                {
                                                    txtColorName.Text = getProduct.Rows[0]["History_Color3"].ToString();
                                                }
                                                else if (getProduct.Rows[0]["History_Color2"].ToString() != null && getProduct.Rows[0]["History_Color2"].ToString() != "")
                                                {
                                                    txtColorName.Text = getProduct.Rows[0]["History_Color2"].ToString();
                                                }
                                                else
                                                {
                                                    txtColorName.Text = getProduct.Rows[0]["History_Color1"].ToString();
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
                                        txtMessage.Text = "Error [Label RFID not Register in QA6]";
                                        txtMessage.BackColor = Color.Yellow;
                                        txtEPC.Text = "";
                                        txtUnique.Text = "";
                                        txtRFIDSerial.Text = "";
                                        txtModelName.Text = "";
                                        txtColorName.Text = "";
                                    }
                                }
                                else
                                {
                                    txtMessage.Text = "Error [Wrong RFID Label are detected]";
                                    txtMessage.BackColor = Color.Yellow;
                                    txtEPC.Text = "";
                                    txtUnique.Text = "";
                                    txtRFIDSerial.Text = "";
                                    txtModelName.Text = "";
                                    txtColorName.Text = "";
                                }

                            }
                            else
                            {
                                txtMessage.Text = "Error [Wrong RFID Label are detected]";
                                txtMessage.BackColor = Color.Yellow;
                                txtEPC.Text = "";
                                txtUnique.Text = "";
                                txtRFIDSerial.Text = "";
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
                        txtRFIDSerial.Text = "";
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

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (txtUnique.Text == "" && txtEPC.Text == "")
            {
                MessageBox.Show("Please read Label RFID!");
                return;
            }

            if (dataacces_QA6.CheckUniqueStatusOK(txtUnique.Text))
            {
                MessageBox.Show("UniqueID Already Finish!");
                return;
            }
            if (dataacces_QA6.CheckUniqueStatusNG(txtUnique.Text))
            {
                MessageBox.Show("UniqueID already input\nplease check transaction list!");
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
            if (dataacces_QA6.CheckTimeStart(txtUnique.Text))
            {

                timestart = now.ToString("yyMMddHHmm") + "";
            }
            else
            {
                timestart = dataacces_QA6.GetTimeStart(txtUnique.Text);
                if (timestart == null)
                {
                    timestart = now.ToString("yyMMddHHmm") + "";
                }

            }
            FrmQA6Transaction frm = new FrmQA6Transaction();
            frm.timestart = timestart;
            frm.UniqueID = txtUnique.Text;
            frm.SerialEPC = txtRFIDSerial.Text;
            frm.EPC = txtEPC.Text;
            frm.Model1 = Model1;
            frm.Color1 = Color1;
            frm.Model2 = Model2;
            frm.Color2 = Color2;
            frm.Model3 = Model3;
            frm.Color3 = Color3;
            frm.model = model;
            frm.ShowDialog();
            txtEPC.Text = "";
            txtRFIDSerial.Text = "";
            txtUnique.Text = "";
            txtModelName.Text = "";
            txtColorName.Text = "";
            connect_Text = MyConst.CONNECT;
            ConnectAction();
        }

        private void btnTransaction_Click(object sender, EventArgs e)
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
            FrmTrasactionListQA6 frm = new FrmTrasactionListQA6();
            frm.Location = "QA6";
            frm.ShowDialog();
            connect_Text = MyConst.CONNECT;
            ConnectAction();
        }
    }
}
