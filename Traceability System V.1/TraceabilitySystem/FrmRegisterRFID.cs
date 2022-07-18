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

    public partial class FrmRegisterRFID : Form
    {
        RegisterTag registerTag = new RegisterTag();
        private ClsRFIDDenso C_RFIDDenso;
        private ClsRFIDDenso.ClsUiiData _UiiData;
        private List<string> _ArayReadBuff;
        private ClsLinkValue C_LinkValue;
        Dataacces_Model dataacces_Model = new Dataacces_Model();
        Dataacces_Color dataacces_Color = new Dataacces_Color();
        Dataacces_CreateRFID dataacces_CreateRFID = new Dataacces_CreateRFID();
        public FrmRegisterRFID()
        {
            InitializeComponent();
            //this._ArayReadBuff = new List<string>();
            //this.C_LinkValue = new ClsLinkValue();
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
            set { version = value; }
        }



        private string statusMsg;
        public string StatusMsg
        {
            get { return statusMsg; }
            set { statusMsg = value; }
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
            set { writeTag = value; }
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
        private void GetDataModel()
        {
            try
            {
                Dictionary<string, string> test = new Dictionary<string, string>();

                DataTable dataTable = new DataTable();
                dataTable = dataacces_Model.GetAll();
                cbModel.DataSource = null;
                if (dataTable.Rows.Count > 0)
                {
                    IDictionary<string, string> numberNames = new Dictionary<string, string>();
                    int row = 0;
                    int totrow = dataTable.Rows.Count - 1;
                    while (row <= totrow)
                    {

                        test.Add(dataTable.Rows[row][1].ToString(), dataTable.Rows[row][2].ToString());
                        row++;

                    }

                    cbModel.DataSource = new BindingSource(test, null);
                    cbModel.DisplayMember = "Value";
                    cbModel.ValueMember = "Key";

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
                Dictionary<string, string> test = new Dictionary<string, string>();

                DataTable dataTable = new DataTable();
                dataTable = dataacces_Color.GetAll();
                cbColor.DataSource = null;
                if (dataTable.Rows.Count > 0)
                {
                    IDictionary<string, string> numberNames = new Dictionary<string, string>();
                    int row = 0;
                    int totrow = dataTable.Rows.Count - 1;
                    while (row <= totrow)
                    {

                        test.Add(dataTable.Rows[row][1].ToString(), dataTable.Rows[row][2].ToString() + "-" + dataTable.Rows[row][3].ToString());
                        row++;

                    }

                    cbColor.DataSource = new BindingSource(test, null);
                    cbColor.DisplayMember = "Value";
                    cbColor.ValueMember = "Key";

                }
            }
            catch (Exception ex)
            {
                savelog("Error Get Data Color!: + " + ex.ToString());
                MessageBox.Show("Error Get Data Color!" + ex.ToString());
            }
        }
        private void FrmMasterModel_Load(object sender, EventArgs e)
        {

            connect_Text = MyConst.CONNECT;
            ConnectAction();
            txtUnique.Text = 'P' + dtDate.Value.ToString("yyMMdd") + dataacces_CreateRFID.GetUnique();
            GetDataModel();
            GetDataColor();


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

        private void FrmLinkTagRead_Shown(object sender, EventArgs e)
        {
            string name = MethodBase.GetCurrentMethod().Name;
            try
            {
                if (!this.fncRFIDReaderON())
                {
                    //modRfidAmPackageCS.C_LanguageWord.fncPopUpMsg("M", "990014", ClsLanguageWord.EnPopUpTitle.Warning, ClsLanguageWord.EnPopUpStyle.OKONLY_CRI);
                    base.Close();
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Err, "FrmLinkTagRead", name, "System Error", exception.Message, "");
                ProjectData.ClearProjectError();
            }
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
        private void btnCreateLabelRFID_Click(object sender, EventArgs e)
        {
            // StatusProcess = "CreateLabel";
            ////panelCreateRFID.Visible = true;
            ////PanelUtama.Visible = false;
            ////panelCreateRFID.Dock = DockStyle.Fill;

            // RfidStart();
            //txtSerialRFID.Text = dataacces_CreateRFID.GetSerialNumber();
            //txtLastSerial.Text = "Last Serial Number  : " + dataacces_CreateRFID.GetlastSerialNumber();
            //txtWriteEFC.Text = DateTime.Now.ToString("yyMMdd") + txtSerialRFID.Text;

            //fncRFIDReaderON();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Close Create
            this.subRFIDReaderOFF();
            timGetRFIDData.Enabled = false;
            timGetRFIDData.Stop();
            timGetRFIDData.Dispose();
            ////panelCreateRFID.Visible = false;
            //PanelUtama.Visible = true;
            //PanelUtama.Dock = DockStyle.Fill;

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
        //Thread t1;
        //public bool StartRead()
        //{
        //    t1 = new Thread(ReadTag);
        //    t1.Start();
        //    return true;
        //}
        //private void ReadTag()
        //{
        //    string name = MethodBase.GetCurrentMethod().Name;
        //    try
        //    {
        //        int num1;

        //        this.C_RFIDDenso = new ClsRFIDDenso(3);
        //        this.C_RFIDDenso.p_Type = "COM";
        //        this.C_RFIDDenso.p_SignalStrength = 50;
        //        this.C_RFIDDenso.p_AntenaPort = 1;
        //        this.C_RFIDDenso.p_QFactor = 4;
        //        Application.DoEvents();
        //        do
        //        {

        //            num1 = this.C_RFIDDenso.fncGetData(ref this._UiiData);
        //            if ((num1 > 0) && !this._ArayReadBuff.Contains(this._UiiData.EncodeData))
        //            {
        //                C_LinkValue = new ClsLinkValue();
        //                this._ArayReadBuff.Add(this._UiiData.EncodeData);
        //                modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Transfer, "FrmLinkTagRead", name, "RFIDタグ取得", "【EPC】" + this._UiiData.EncodeData, "");
        //                //  this.C_LinkValue.p_ArrayScanTagID[0].EPC_Value = this._UiiData.EncodeData;
        //                this.C_RFIDDenso_ScanedRFID(this._UiiData.EncodeData);

        //            }
        //        }
        //        while (num1 > 0);
        //    }
        //    catch (ThreadAbortException)
        //    {
        //        subRFIDReaderOFF();
        //    }

        //    catch (Exception exception1)
        //    {
        //        ProjectData.SetProjectError(exception1);
        //        Exception exception = exception1;
        //        modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Err, "FrmLinkTagRead", name, "System Error", exception.Message, "");
        //        ProjectData.ClearProjectError();
        //        MessageBox.Show("Error" + exception1.ToString());
        //    }
        //    finally
        //    {

        //    }
        //}



        //private void ScanTagAction()
        //{

        //    Tag tIn = new Tag();
        //    if (ur.ReadOneTag(ref tIn))
        //    {
        //        // Display read tag.
        //        txtEPC.Text = tIn.Uii;
        //        StatusMsg = "Read Tag Data: " + scanTag;
        //    }
        //    else
        //    {
        //        StatusMsg = "Read Tag Data: " + scanTag;
        //    }
        //    StatusMsg = "Read Tag Data: " + scanTag;
        //}

        private void WriteTagAction()
        {
            if (scanTag == "")
                MessageBox.Show(MyConst.WARNING + Environment.NewLine + "Please scan the tag that you want to write data to.");
            else
            {
                // Write tag data.
                if (ur.WriteOneTag(scanTag, writeTag))
                {
                    MessageBox.Show(MyConst.INFO + Environment.NewLine + "New data has been written to the tag.");
                    ClearAction();
                }
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
                    }
                    else
                    {
                        if (tIn.Uii == null)
                        {
                            txtEPC.Text = tIn.Uii;
                            txtRFIDSerial.Text = "";
                            txtMessage.Text = "Please read RFID Label";
                            txtMessage.BackColor = Color.White;
                        }
                        else
                        {
                            txtEPC.Text = General.gHexToString(tIn.Uii);
                            if (txtEPC.Text.Length == 12)
                            {
                                txtRFIDSerial.Text = txtEPC.Text.Substring(6, 6);
                                txtMessage.Text = "";
                                txtMessage.BackColor = Color.White;
                                scanTag = tIn.Uii;
                            }
                            else
                            {
                                txtMessage.Text = "Error[Wrong RFID Label are detected]";
                                txtMessage.BackColor = Color.Yellow;
                            }

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


        private void tmrReadRFID_Tick(object sender, EventArgs e)
        {
            try
            {

                timGetRFIDData.Stop();
                timGetRFIDData.Enabled = false;
                //  txtEPC.Text = "EPC : " + Ur21.EncodeData;
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
        //private void subClearProc()
        //{
        //    int num2 =0;
        //    try
        //    {
        //        Application.DoEvents();
        //        int num3;
        //    Label_0000:
        //        ProjectData.ClearProjectError();
        //        int num = 1;
        //    Label_0007:
        //        num3 = 2;
        //        this.txtReadCount.Text = "";
        //    Label_0019:
        //        num3 = 3;
        //        this.txtReadCount.ForeColor = Color.Black;
        //    Label_002B:
        //        num3 = 4;
        //        this.txtEPC.Text = "";
        //    Label_003D:
        //        num3 = 5;
        //    Label_0058:
        //        num3 = 7;
        //        this._ArayReadBuff.Clear();
        //    Label_0065:
        //        num3 = 8;
        //        this.C_RFIDDenso.fncAbort();
        //    Label_0073:
        //        num3 = 9;
        //        this.C_RFIDDenso.fncStartRead();
        //        goto Label_0092;
        //    Label_0084:
        //        num3 = 11;

        //    Label_0092:
        //        num3 = 13;

        //    Label_00A0:
        //        num3 = 14;
        //        //this.btnCreateRFID.Enabled = false;
        //    Label_00AF:
        //        num3 = 15;
        //        Label lblMsg = this.lblMsg;
        //        //clsSV_MSG.SVSubMsgClear(ref lblMsg);
        //        this.lblMsg = lblMsg;
        //        goto Label_015B;
        //    Label_00CE:
        //        num2 = 0;
        //        switch ((num2 + 1))
        //        {
        //            case 1:
        //                goto Label_0000;

        //            case 2:
        //                goto Label_0007;

        //            case 3:
        //                goto Label_0019;

        //            case 4:
        //                goto Label_002B;

        //            case 5:
        //                goto Label_003D;

        //            case 6:
        //            case 10:
        //            case 12:
        //            case 13:
        //                goto Label_0092;

        //            case 7:
        //                goto Label_0058;

        //            case 8:
        //                goto Label_0065;

        //            case 9:
        //                goto Label_0073;

        //            case 11:
        //                goto Label_0084;

        //            case 14:
        //                goto Label_00A0;

        //            case 15:
        //                goto Label_00AF;

        //            case 0x10:
        //                goto Label_015B;

        //            default:
        //                goto Label_0150;
        //        }
        //    }
        //    catch (Exception exception1)
        //    {
        //        ProjectData.SetProjectError(exception1);
        //        goto Label_0150;
        //    }
        //    finally
        //    {

        //    }
        //Label_0150:
        //    throw ProjectData.CreateProjectError(-2146828237);
        //Label_015B:
        //    if (num2 != 0)
        //    {
        //        ProjectData.ClearProjectError();
        //    }

        //}

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
        RFIDTag rFIDTag = new RFIDTag();

        //private void btnCreateRFID_Click(object sender, EventArgs e)
        //{
        //    string name = MethodBase.GetCurrentMethod().Name;
        //    try
        //    {
        //        timGetRFIDData.Enabled = false;

        //        modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Ope, "FrmLinkTagRead", name, "ボタンクリック", ((Button)sender).Text + "ボタン", "");
        //        if (this.fncDataConfirmProcess())
        //        {
        //            try
        //            {


        //                this.subRFIDReaderOFF();


        //                if (cbExistsSerialNumber.Checked == true)
        //                {
        //                    rFIDTag.EPCNumber = txtExistingSerial.Text;
        //                    rFIDTag.SerialRFID = txtExistingSerial.Text.Substring(6, 6);
        //                    rFIDTag.Pic = Users.Name;
        //                    rFIDTag.LastUpdate = DateTime.Now;
        //                    if (dataacces_CreateRFID.UPDATERFID(rFIDTag))
        //                    {
        //                        MessageBox.Show("Update Serial to Tag RFID Successfully");
        //                        txtSerialRFID.Text = dataacces_CreateRFID.GetSerialNumber();
        //                        txtLastSerial.Text = "Last Serial Number  : " + dataacces_CreateRFID.GetlastSerialNumber();
        //                        txtWriteEFC.Text = DateTime.Now.ToString("yyMMdd") + txtSerialRFID.Text;
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("Update Serial to Tag RFID Failed");
        //                    }
        //                }
        //                else
        //                {
        //                    rFIDTag.EPCNumber = txtEPC.Text;
        //                    rFIDTag.SerialRFID = txtEPC.Text.Substring(6, 6);
        //                    rFIDTag.Pic = Users.Name;
        //                    rFIDTag.LastUpdate = DateTime.Now;
        //                    if (dataacces_CreateRFID.CreateRFID(rFIDTag))
        //                    {
        //                        MessageBox.Show("Write Serial to Tag RFID Successfully");
        //                        txtSerialRFID.Text = dataacces_CreateRFID.GetSerialNumber();
        //                        txtLastSerial.Text = "Last Serial Number  : " + dataacces_CreateRFID.GetlastSerialNumber();
        //                        txtWriteEFC.Text = DateTime.Now.ToString("yyMMdd") + txtSerialRFID.Text;
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("Write Serial to Tag RFID Failed");
        //                    }
        //                }

        //            }
        //            catch (Exception)
        //            {

        //            }
        //            finally
        //            {
        //                this.fncRFIDReaderON();
        //                this.subClearProc();
        //            }


        //        }
        //    }
        //    catch (Exception exception1)
        //    {
        //        ProjectData.SetProjectError(exception1);
        //        Exception exception = exception1;
        //        modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Err, "FrmLinkTagRead", name, "System Error", exception.Message, "");
        //        ProjectData.ClearProjectError();
        //    }
        //}
        //private bool fncDataConfirmProcess()
        //{
        //    string name = MethodBase.GetCurrentMethod().Name;
        //    bool flag = false;
        //    bool flag2 = false;
        //    try
        //    {
        //        //if (this.C_LinkValue.p_ArrayScanTagID.Count <= 0)
        //        //{
        //        //    return false;
        //        //}
        //        Application.UseWaitCursor = true;
        //        //fncRFIDReaderON();
        //        //ReadTag();
        //        if (C_RFIDDenso.fncStopRead())
        //        {


        //            //if (modRfidAmPackageCS.C_LanguageWord.fncPopUpMsg("M", "990012", ClsLanguageWord.EnPopUpTitle.Confirm, 
        //            //    ClsLanguageWord.EnPopUpStyle.YN_QUE_1) != ((MsgBoxResult)((int)MsgBoxResult.Yes)))
        //            //{
        //            //    flag2 = true;
        //            //    return false;
        //            //}
        //            Label lblMsg = this.lblMsg;
        //            //modRfidAmPackageCS.C_LanguageWord.subMsgDisp(ref lblMsg, "M", "990016", ClsLanguageWord.EnLabelColor.GREEN, ClsLanguageWord.EnLabelColor.BLACK);
        //            this.lblMsg = lblMsg;
        //            this.Refresh();

        //            //Serial EPC Harus 24 digit Hex 

        //            if (!this.C_RFIDDenso.fncWriteRFIDData(ref this._UiiData,
        //                (int)Math.Round((double)(((double)ClsRFIDDenso.EPC_SIZE) / 8.0)), AsciiToHex(txtWriteEFC.Text)))
        //            {
        //                lblMsg = this.lblMsg;
        //                //  modRfidAmPackageCS.C_LanguageWord.subMsgDisp(ref lblMsg, "M", "050014", ClsLanguageWord.EnLabelColor.YELLOW, ClsLanguageWord.EnLabelColor.BLACK);
        //                this.lblMsg = lblMsg;
        //                // modRfidAmPackageCS.C_LanguageWord.fncPopUpMsg("M", "050014", ClsLanguageWord.EnPopUpTitle.Warning, ClsLanguageWord.EnPopUpStyle.OKONLY_EXC);
        //                // modRfidAmPackageCS.subInsertOperationLog(this._SVClsDbControl, "ER", this.lblFormName.Text + modRfidAmPackageCS.C_LanguageWord.fncGetPopTitleString(ClsLanguageWord.EnPopUpTitle.Warning) + " " + this.lblMsg.Text, this._InfoLogin.WorkerCode, this._InfoLogin.WorkerName, "TyingEntryProcess", "FrmLinkTagRead", name);
        //                flag2 = true;
        //                return false;

        //            }

        //            Application.UseWaitCursor = false;
        //            lblMsg = this.lblMsg;
        //            this.lblMsg = lblMsg;


        //            //if (!this.C_LinkValue.fncSetEntryLinking(this._InfoLogin.WorkerCode, ref lblMsg))
        //            //{
        //            //    string text = this.lblMsg.Text;
        //            //    if (this.C_LinkValue.p_AditionalWarning != "")
        //            //    {
        //            //        lblMsg = this.lblMsg;
        //            //        lblMsg.Text = lblMsg.Text + this.C_LinkValue.p_AditionalWarning;
        //            //    }
        //            //    string[] prmMSG = new string[] { text };
        //            //    //clsSV_MSG.SVFncPopUpMsg(modRfidAmPackageCS.C_LanguageWord.fncGetPopTitleString(ClsLanguageWord.EnPopUpTitle.Warning), 0x4e20L, prmMSG);
        //            //   // modRfidAmPackageCS.subInsertOperationLog(this._SVClsDbControl, "ER", this.lblFormName.Text + modRfidAmPackageCS.C_LanguageWord.fncGetPopTitleString(ClsLanguageWord.EnPopUpTitle.Warning) + " " + this.lblMsg.Text, this._InfoLogin.WorkerCode, this._InfoLogin.WorkerName, "TyingEntryProcess", "FrmLinkTagRead", name);
        //            //    flag2 = true;
        //            //    return false;
        //            //}

        //            //this.C_LinkValue.p_ArrayScanTagID[0].EPC_Value = AppSettings.SerialEPC;
        //            //string prmProcContents = modRfidAmPackageCS.C_LanguageWord.fncGetDisplayString("M", this.C_LinkValue.p_SelectedAsset.AssetID_RegDiv ? "050001" : "050002", "") + " 【" + modRfidAmPackageCS.C_LanguageWord.fncGetDisplayString("I", "990019", "") + "】1";
        //            //modRfidAmPackageCS.subInsertOperationLog(this._SVClsDbControl, "EV", prmProcContents, this._InfoLogin.WorkerCode, this._InfoLogin.WorkerName, "TyingEntryProcess", "FrmLinkTagRead", name);
        //            flag = true;
        //        }
        //    }
        //    catch (Exception exception1)
        //    {
        //        ProjectData.SetProjectError(exception1);
        //        Exception exception = exception1;
        //        modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Err, "FrmLinkTagRead", name, "System Error", exception.Message, "");
        //        ProjectData.ClearProjectError();
        //        return false;
        //    }
        //    finally
        //    {
        //        if (flag2)
        //            this.C_RFIDDenso.fncStartRead();
        //        this.timGetRFIDData.Enabled = true;
        //    }

        //    return flag;
        //}

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
            panelRegister.Visible = false;
            //PanelUtama.Visible = true;
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
                this.C_RFIDDenso.p_SignalStrength = 50;
                this.C_RFIDDenso.p_AntenaPort = 1;
                this.C_RFIDDenso.p_QFactor = 4;
                if (!this.C_RFIDDenso.fncRFID_ConnectON())
                {
                    flag = false;
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
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            return string.Empty;
        }
        //private void C_RFIDDenso_ScanedRFID(string prmReadValue)
        //{
        //    string name = MethodBase.GetCurrentMethod().Name;
        //    try
        //    {
        //        Label lblMsg;
        //        if ((ClsRFIDDenso.EPC_SIZE > 0) && ((prmReadValue.Length * 4) != ClsRFIDDenso.EPC_SIZE))
        //        {
        //            modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Evt, "FrmLinkTagRead", name, "読取りEPCサイズ不一致", "カテゴリーマスタサイズ=" + ClsRFIDDenso.EPC_SIZE.ToString() + ",  読取りサイズ=" + ((prmReadValue.Length * 4)).ToString(), "");
        //            lblMsg = this.lblMsg;
        //            //modRfidAmPackageCS.C_LanguageWord.subMsgDisp(ref lblMsg, "M", "050018", ClsLanguageWord.EnLabelColor.YELLOW, ClsLanguageWord.EnLabelColor.BLACK);
        //            this.lblMsg = lblMsg;
        //            this.txtEPC.Text = prmReadValue;
        //        }
        //        else
        //        {
        //            //lblMsg = null;


        //            //int num2 = this.C_LinkValue.fncReadTagValidCheck(ref this._SVClsDbControl, prmReadValue, this.C_RFIDDenso.p_ReadWithPC_EPC);
        //            //if (num2 < 0)
        //            //{
        //            //    lblMsg = this.lblMsg;
        //            //   //modRfidAmPackageCS.C_LanguageWord.subMsgDisp(ref lblMsg, "M", "999999", ClsLanguageWord.EnLabelColor.RED, ClsLanguageWord.EnLabelColor.WHITE);
        //            //    this.lblMsg = lblMsg;
        //            //}
        //            //else if (num2 == 0)
        //            //{
        //            //    lblMsg = this.lblMsg;
        //            //   // modRfidAmPackageCS.C_LanguageWord.subMsgDisp(ref lblMsg, "M", "050007", ClsLanguageWord.EnLabelColor.YELLOW, ClsLanguageWord.EnLabelColor.BLACK);
        //            //    this.lblMsg = lblMsg;
        //            //}
        //            //else
        //            //{
        //                //int count = this.C_LinkValue.p_ArrayScanTagID.Count;
        //                //this.SvTxtReadCount.Text = this.C_LinkValue.p_ArrayScanTagID.Count.ToString();
        //                this.txtEPC.Text = General.gHexToString(prmReadValue);
        //                //if (this.C_LinkValue.p_ArrayScanTagID.Count > 1)
        //                //{
        //                //    if (count <= 1)
        //                //    {
        //                //        txtMessage.Text = "Error[Multiple RFID Detected. Please Read Only One RFID Label]";
        //                //        this.SvTxtReadCount.ForeColor = Color.Yellow;
        //                //        this.btnCreateLabelRFID.Enabled = false;
        //                //    }
        //                //}
        //                //else
        //                //{
        //                //    this.btnCreateLabelRFID.Enabled = true;

        //                //}
        //            //}

        //        }
        //    }
        //    catch (Exception exception1)
        //    {
        //        ProjectData.SetProjectError(exception1);
        //        Exception exception = exception1;
        //        modSV_LogWrite.SVSubWriteLog(modSV_LogWrite.EN_LogType.Err, "FrmLinkTagRead", name, "System Error", exception.Message, "");
        //        ProjectData.ClearProjectError();
        //    }
        //}


        //private void cbExistsSerialNumber_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (cbExistsSerialNumber.Checked)
        //    {
        //        txtExistingSerial.ReadOnly = false;
        //        txtExistingSerial.Focus();

        //    }
        //    else
        //    {
        //        txtExistingSerial.ReadOnly = true;
        //    }
        //}

        //private void txtExistingSerial_TextChanged(object sender, EventArgs e)
        //{
        //    if (cbExistsSerialNumber.Checked)
        //    {
        //        if (txtExistingSerial.Text.Length == 6)
        //        {
        //            string checkserial = dataacces_CreateRFID.CheckSerialNumber(txtExistingSerial.Text);
        //            if (checkserial != null && checkserial != "")
        //                txtWriteEFC.Text = DateTime.Now.ToString("yyMMdd") + txtExistingSerial.Text;
        //            else
        //                MessageBox.Show("Error , Please Check Existing Serial RFID!");
        //        }
        //    }
        //}

        //private void btnClear_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        timGetRFIDData.Stop();
        //        timGetRFIDData.Enabled = false;
        //        this.subClearProc();
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    finally
        //    {
        //        timGetRFIDData.Enabled = true;
        //        timGetRFIDData.Start();
        //    }
        //}

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
            //panelCreateRFID.Visible = false;
            //PanelUtama.Visible = true;
            //PanelUtama.Dock = DockStyle.Fill;
        }


        private void btnTransactionList_Click(object sender, EventArgs e)
        {
            FrmTrasactionListCreate frmRegisterRFID = new FrmTrasactionListCreate();
            frmRegisterRFID.ShowDialog();
        }

        private void btnRegisterProduct_Click(object sender, EventArgs e)
        {
            // StatusProcess = "RegisterRFID";
            panelRegister.Visible = true;
            //PanelUtama.Visible = false;
            //panelCreateRFID.Visible = false;
            panelRegister.Dock = DockStyle.Fill;

            // RfidStart();

            fncRFIDReaderON();
            timGetRFIDData.Interval = 500;
            timGetRFIDData.Enabled = true;
            timGetRFIDData.Start();

        }


        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

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

        private void dtDate_ValueChanged(object sender, EventArgs e)
        {
            txtUnique.Text = 'P' + dtDate.Value.ToString("yyMMdd") + dataacces_CreateRFID.GetUnique();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtEPC.Text == "" && txtRFIDSerial.Text == "")
            {
                MessageBox.Show("RFID Label Not Found");
                return;
            }

            if (cbModel.SelectedIndex == -1)
            {
                MessageBox.Show("Please select model");
                return;
            }

            if (cbColor.SelectedIndex == -1)
            {
                MessageBox.Show("Please select color");
                return;
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
                        string Model = ((KeyValuePair<string, string>)cbModel.SelectedItem).Value;
                        string Color = ((KeyValuePair<string, string>)cbColor.SelectedItem).Value;

                        registerTag.UniqueID = txtUnique.Text;
                        registerTag.Date = dtDate.Value.ToString("yyyy-MM-dd");
                        registerTag.Model = Model;
                        registerTag.Color = Color;
                        registerTag.EPC = txtEPC.Text;
                        registerTag.PIC = DBConnections.Name;
                        if (dataacces_CreateRFID.CheckStatusEPC(registerTag) == false)
                        {
                            DialogResult dialogResult = MessageBox.Show("Are you sure want to Written to the tag?", "attention", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {

                                if (dataacces_CreateRFID.RegisterTag(registerTag))
                                {
                                    MessageBox.Show("Register Product Success");
                                    txtUnique.Text = "P" + dtDate.Value.ToString("yyMMdd") + dataacces_CreateRFID.GetUnique();

                                }

                            }

                        }
                        else
                        {
                            MessageBox.Show("This Tag On Process!");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Different RFID Label");
                    }
                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ur.DisconnectUR21();
            Connected = false;
            timGetRFIDData.Stop();
            timGetRFIDData.Enabled = false;
            // Change btn text to CONNECT.
            Connect_Text = MyConst.CONNECT;
            timGetRFIDData.Dispose();

            FrmTrasactionListRegister frmRegisterRFID = new FrmTrasactionListRegister();
            frmRegisterRFID.ShowDialog();
            connect_Text = MyConst.CONNECT;
            ConnectAction();
            timGetRFIDData.Enabled = true;
            timGetRFIDData.Start();
        }
    }
}
