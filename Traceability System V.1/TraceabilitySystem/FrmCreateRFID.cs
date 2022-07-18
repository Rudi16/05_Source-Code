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
using static TraceabilitySystem.NativeMethods;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.Runtime.CompilerServices;

namespace TraceabilitySystem
{
    public partial class FrmCreateRFID : Form
    {
        private ClsRFIDDenso C_RFIDDenso;
        private ClsRFIDDenso.ClsUiiData _UiiData;
        private List<string> _ArayReadBuff;
        private ClsLinkValue C_LinkValue;
        Dataacces_CreateRFID dataacces_CreateRFID = new Dataacces_CreateRFID();
        public FrmCreateRFID()
        {
            InitializeComponent();
            this._ArayReadBuff = new List<string>();
            this.C_LinkValue = new ClsLinkValue();
        }
        #region Getter / Setter

        private string connect_Text;
        public string Connect_Text
        {
            get { return connect_Text; }
            set { connect_Text = value; }
        }


        private string defaultMsg;
        public string DefaultMsg
        {
            get { return defaultMsg; }
            set { defaultMsg = value; }
        }



        private string version;
        public string Version
        {
            get { return version; }
            set { version = value;}
        }



        private string statusMsg;
        public string StatusMsg
        {
            get { return statusMsg; }
            set { statusMsg = value;}
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



        private string scanTag;
        public string ScanTag
        {
            get { return scanTag; }
            set { scanTag = value; }
        }



        private string writeTag;
        public string WriteTag
        {
            get { return writeTag; }
            set { writeTag = value;}
        }


        private bool connectReady;
        public bool ConnectReady
        {
            get { return connectReady; }
            set { connectReady = value; }
        }


        #endregion

        private void ClearAction()
        {
            StatusMsg = defaultMsg;
            ScanTag = "";
            WriteTag = "";
        }

        private void FrmMasterModel_Load(object sender, EventArgs e)
        {
            //     this.Size = new Size(900, 700);
            //   panelclientuser.Visible = false;
            txtExistingSerial.Enabled = false;
           
            // RfidStart();
            txtSerialRFID.Text = dataacces_CreateRFID.GetSerialNumber();
            txtLastSerial.Text = "Last Serial Number  : " + dataacces_CreateRFID.GetlastSerialNumber().PadLeft(6,'0');
            txtWriteEFC.Text = DateTime.Now.ToString("yyMMdd") + txtSerialRFID.Text;
            connect_Text = MyConst.CONNECT;
            ConnectAction();


        }
      
        WaitWnd.WaitWndFun waitForm = new WaitWnd.WaitWndFun();
        private void btnclose_Click(object sender, EventArgs e)
        {
            // Stop RFID reading.
       //     ur.StopReading();
            IsReady = true;
            InAction = false;
            timGetRFIDData.Stop();
            timGetRFIDData.Enabled = false;
            timGetRFIDData.Dispose();
            Close();
        }
       


        public bool InAction { get; private set; }
        public bool IsReady { get; private set; }
        Ur21 ur = new Ur21();

        private void RfidStart()
        {
            try
            {
                // Get COM port number in byte.
                string port = "3";
                byte bPort = byte.Parse(port);
                // Start RFID reading.
                InAction = true;
                IsReady = false;
               // ur.StartRead(bPort);
                //    txtEPC.Text = AppSettings.GetDataRFID;
            }
            catch (Exception e)
            {
                savelog("Error Read Tag : + " + e.ToString());
                MessageBox.Show("Error Read Tag : + " + Environment.NewLine + e.Message);
            }
        }


        public void savelog(string description)
        {
            string tab = "\t";
            String header = "Datetime" + tab + "Description";
            String str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + tab + description;
            AppSettings.SaveLogs(str, header);
        }
       
       
        private void ConnectAction()
        {
            try { 
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
                }
                else
                {
                    // Disconnect from UR21.
                    ur.DisconnectUR21();

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
        public static bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        private void ScanTagAction()
        {
            try { 
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
                    }
                    else
                    {
                        if (tIn.Uii == null)
                        {
                            txtEPC.Text = tIn.Uii;
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
                                    txtEPC.Text = epc;
                                }
                                else
                                {
                                    txtEPC.Text = tIn.Uii;
                                }
                            }
                            else
                            {
                                txtEPC.Text = tIn.Uii;
                            }
                            txtMessage.Text = "";
                            txtMessage.BackColor = Color.White;
                            scanTag = tIn.Uii;
                        }
                    
                    }
                }
                else
                {
                    txtMessage.Text = "Please read RFID Label";
                    txtMessage.BackColor = Color.White;
                }
            }
            catch (Exception e)
            {
                savelog("Error ScanTagAction : + " + e.ToString());
                 MessageBox.Show("Error ScanTagAction : + " + Environment.NewLine + e.Message);
            }
        }


        // Buat Nulis ke TAG
        private void WriteTagAction()
        {
            try { 
                if (txtEPC.Text == "") {
                    MessageBox.Show("RFID Label not found!");
                }
                else
                {
                    if (cbExistsSerialNumber.Checked == true)
                    {
                        if (txtExistingSerial.Text.Length != 6)
                        {
                            MessageBox.Show("Wrong Serial Number!");
                            txtExistingSerial.Focus();
                            return;
                        }
                    }

                    Tag tIn = new Tag();
                    if (ur.ReadOneTag(ref tIn))
                    {

                        if (tIn.Uii == null)
                        {
                            txtEPC.Text = tIn.Uii;
                            MessageBox.Show("Please read RFID Label");
                        }
                        else
                        {
                            string check = tIn.Uii;
                            if (scanTag == check)
                            {
                                DialogResult dialogResult = MessageBox.Show("Are you sure want to Written to the Label?", "attention", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    if (ur.WriteOneTag(txtEPC.Text, AsciiToHex(txtWriteEFC.Text)))
                                    {
                                        ClearAction();
                                        if (cbExistsSerialNumber.Checked == true )
                                        {
                                                rFIDTag.EPCNumber = txtWriteEFC.Text;
                                                rFIDTag.SerialRFID = txtExistingSerial.Text;
                                                rFIDTag.Pic = DBConnections.Name;
                                                rFIDTag.LastUpdate = DateTime.Now;
                                                if (dataacces_CreateRFID.CreateRFID(rFIDTag))
                                                {
                                                    MessageBox.Show("Update Serial to Label RFID Successfully");
                                                    txtExistingSerial.Text = "";
                                                    cbExistsSerialNumber.Checked = false;
                                                    txtSerialRFID.Text = dataacces_CreateRFID.GetSerialNumber();
                                                    txtLastSerial.Text = "Last Serial Number  : " + dataacces_CreateRFID.GetlastSerialNumber().PadLeft(6, '0');
                                                    txtWriteEFC.Text = DateTime.Now.ToString("yyMMdd") + txtSerialRFID.Text;
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Update Serial to Label RFID Failed");
                                                }
                                           
                                        }
                                        else
                                        {
                                            rFIDTag.EPCNumber = txtWriteEFC.Text;
                                            rFIDTag.SerialRFID = txtSerialRFID.Text;
                                            rFIDTag.Pic = DBConnections.Name;
                                            rFIDTag.LastUpdate = DateTime.Now;
                                            if (dataacces_CreateRFID.CreateRFID(rFIDTag))
                                            {
                                                MessageBox.Show("Write Serial to Label RFID Successfully");
                                                txtSerialRFID.Text = dataacces_CreateRFID.GetSerialNumber();
                                                txtLastSerial.Text = "Last Serial Number  : " + dataacces_CreateRFID.GetlastSerialNumber().PadLeft(6,'0');
                                                txtWriteEFC.Text = DateTime.Now.ToString("yyMMdd") + txtSerialRFID.Text;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Write Serial to Label RFID Failed");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(MyConst.INFO + Environment.NewLine + "Written to the Label failed.");
                                        ClearAction();
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

            }
            catch (Exception e)
            {
                savelog("Error WriteTagAction : " + e.ToString());
                MessageBox.Show("Error WriteTagAction :" + Environment.NewLine + e.ToString()); 
            }
        }
        private void tmrReadRFID_Tick(object sender, EventArgs e)
        {
            try
            {

                timGetRFIDData.Stop();
                timGetRFIDData.Enabled = false;
                //  txtEPC.Text = "EPC : " + Ur21.EncodeData;
                Application.DoEvents();
                #region bikinsendiri
                ScanTagAction();
                #endregion
                #region dari asetora
                //string name = MethodBase.GetCurrentMethod().Name;
                //bool flag = true;
                //try
                //{
                //    int num1;
                //    this.timGetRFIDData.Enabled = false;
                //    this.C_RFIDDenso = new ClsRFIDDenso(3);
                //    this.C_RFIDDenso.p_Type = "COM";
                //    this.C_RFIDDenso.p_SignalStrength = 50;
                //    this.C_RFIDDenso.p_AntenaPort = 1;
                //    this.C_RFIDDenso.p_QFactor = 4;
                //    Application.DoEvents();
                //    do
                //    {
                //        num1 = this.C_RFIDDenso.fncGetData(ref this._UiiData);
                //        if ((num1 > 0) && !this._ArayReadBuff.Contains(this._UiiData.EncodeData))
                //        {
                //            this._ArayReadBuff.Add(this._UiiData.EncodeData);
                //            int count = _ArayReadBuff.Count;
                //            modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Transfer, "FrmLinkTagRead", name, "RFIDタグ取得", "【EPC】" + this._UiiData.EncodeData, "");
                //            this.C_RFIDDenso_ScanedRFID(this._UiiData.EncodeData);

                //        }
                //    }
                //    while (num1 > 0);
                //}
                //catch (Exception exception1)
                //{
                //    ProjectData.SetProjectError(exception1);
                //    Exception exception = exception1;
                //    modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Err, "FrmLinkTagRead", name, "System Error", exception.Message, "");
                //    flag = false;
                //    ProjectData.ClearProjectError();
                //    MessageBox.Show("Error" + exception1.ToString());
                //}
                //finally
                //{
                //    if (flag)
                //    {
                //        this.timGetRFIDData.Enabled = true;
                //    }
                //}
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

        private void subRFIDReaderOFF()
        {
            try
            {

                this.C_RFIDDenso.fncStopRead();

                this.C_RFIDDenso.subRFID_ConnectOFF();


            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
            }

        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
        RFIDTag rFIDTag = new RFIDTag();

        private void btnCreateRFID_Click(object sender, EventArgs e)
        {
            WriteTagAction();
        }
       
        public static string AsciiToHex(string asciiString)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in asciiString)
            {
                builder.Append(Convert.ToInt32(c).ToString("X"));
            }
            return builder.ToString();
        }
        private void btnCloseRegister_Click(object sender, EventArgs e)
        {
        }
        private bool fncRFIDReaderON()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            bool flag = false;
            try
            {
                Application.DoEvents();
                this.C_RFIDDenso = new ClsRFIDDenso(3);
                this.C_RFIDDenso.p_Type = "COM";
                this.C_RFIDDenso.p_SignalStrength =50;
                this.C_RFIDDenso.p_AntenaPort = 1;
                this.C_RFIDDenso.p_QFactor = 4;
                if (!this.C_RFIDDenso.fncRFID_ConnectON())
                {
                    flag  = false;
                }
                if (!this.C_RFIDDenso.fncAbort())
                {
                    flag = false;
                }
                if (!this.C_RFIDDenso.fncStartRead())
                {
                    flag = false;
                }
                this.timGetRFIDData.Enabled = true;
                flag = true;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                //modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Err, "FrmLinkTagRead", name, "System Error", exception.Message, "");
                ProjectData.ClearProjectError();
            }
            return flag;
        }

        public static string ConvertHex(String hexString)
        {
            try
            {
                string ascii = string.Empty;

                for (int i = 0; i < hexString.Length; i += 2)
                {
                    String hs = string.Empty;

                    hs = hexString.Substring(i, 2);
                    uint decval = System.Convert.ToUInt32(hs, 16);
                    char character = System.Convert.ToChar(decval);
                    ascii += character;

                }

                return ascii;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);}

            return string.Empty;
        }
        

       
        private void cbExistsSerialNumber_CheckedChanged(object sender, EventArgs e)
        {
            if (cbExistsSerialNumber.Checked)
            {
                txtExistingSerial.Enabled = true;
                txtExistingSerial.Focus();
                txtSerialRFID.ForeColor = Color.White;
                txtWriteEFC.ForeColor = Color.White;
            }
            else
            {
                txtSerialRFID.ForeColor = Color.Black;
                txtWriteEFC.ForeColor = Color.Black;
                txtWriteEFC.Text = DateTime.Now.ToString("yyMMdd") + txtSerialRFID.Text;
                txtExistingSerial.Enabled = false   ;
                txtExistingSerial.Text = "";
            }
        }

        private void txtExistingSerial_TextChanged(object sender, EventArgs e)
        {
            if (cbExistsSerialNumber.Checked)
            {
                if (txtExistingSerial.Text.Length == 6)
                {
                    string checkserial = dataacces_CreateRFID.CheckSerialNumber(txtExistingSerial.Text);
                    if (checkserial != null && checkserial != "")
                    {
                        txtWriteEFC.Text = DateTime.Now.ToString("yyMMdd") + txtExistingSerial.Text;
                        txtWriteEFC.ForeColor = Color.Black;
                    }
                    else
                    {
                        MessageBox.Show("Error , Please Check Existing Serial RFID!");
                        txtWriteEFC.ForeColor = Color.White;
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                timGetRFIDData.Stop();
                timGetRFIDData.Enabled = false;
               
            }
            catch (Exception)
            {

            }
            finally
            {
                timGetRFIDData.Enabled = true;
                timGetRFIDData.Start();
            }
        }

        private void pict_logout_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //Close Create
            this.subRFIDReaderOFF();
            timGetRFIDData.Enabled = false;
            timGetRFIDData.Stop();
            timGetRFIDData.Dispose();
            Close();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.LightGray;
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

            FrmTrasactionListCreate frmRegisterRFID = new FrmTrasactionListCreate();
            frmRegisterRFID.ShowDialog();
            connect_Text = MyConst.CONNECT;
            ConnectAction();
            timGetRFIDData.Enabled = true;
            timGetRFIDData.Start();
        }

       

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            this.subRFIDReaderOFF();
          
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
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

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
