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
using System.Security.Cryptography;
using System.IO;
using System.Collections.ObjectModel;
using System.Management;
using System.Diagnostics;

namespace TraceabilitySystem
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        Users users = new Users();
        Dataacces_Users dataacces_user = new Dataacces_Users();
        DBConnections dbconnection = new DBConnections();
        
        private String DecryptIt(String s, byte[] key, byte[] IV)
        {
            String result;
            RijndaelManaged rijn = new RijndaelManaged();
            using (MemoryStream msDecrypt = new MemoryStream(System.Convert.FromBase64String(s)))
            {
                using (ICryptoTransform decryptor = rijn.CreateDecryptor(key, IV))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader swDecrypt = new StreamReader(csDecrypt))
                        {
                            result = swDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            rijn.Clear();
            return result;
        }


        private void FrmLogin_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            lblVersi.Text = "Traceability System V. " + version ;
            pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\\Asset\\image\\Logo.png");
            btnSetting.Image = Image.FromFile(Application.StartupPath + @"\\Asset\\image\\Setting2.png");

            string path = Application.StartupPath + @"\Config\";
            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }

            if (System.IO.File.Exists(path + "Database.dat") == false)
            {
                MessageBox.Show("Please set the connection first");
                this.Hide();
                FrmUseSQLServer fus = new FrmUseSQLServer();
                fus.ShowDialog();
                this.Show();
                txtuser.Focus();
            }
            
            
            lblLocation.Text = AppSettings.Location;
            if (AppSettings.Location != "Office")
            {
                lblLocation.ForeColor = Color.Black;
                label4.Text = "RFID Reader";
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                StatusMsg.Text = "";
                StatusMsg.BackColor = Color.Transparent;
                label4.Text = "";
                label4.BackColor = Color.Transparent;

            }
           
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtuser_Enter(object sender, EventArgs e)
        {
            if(txtuser.Text == "User ID")
            {
                txtuser.Text = "";
                txtuser.ForeColor = Color.Black;
            }

        }

        private void txtuser_Leave(object sender, EventArgs e)
        {

        }

        private void txtpassword_Enter(object sender, EventArgs e)
        {
            if (txtpassword.Text == "Password")
            {
                txtpassword.Text = "";
                txtpassword.ForeColor = Color.Black;
            }
        }

        private void txtpassword_Leave(object sender, EventArgs e)
        {

        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
           

            if (txtuser.Text == "" || txtpassword.Text == "")
            {
                MessageBox.Show("Sorry, Login Data Not Complete..", "Info");
                txtuser.Text = "";
                txtpassword.Text = "";
                txtuser.Focus();
            }
            else
            {

                login();   
            }
            txtpassword.Text = "";
        }

        private void txtuser_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (txtuser.Text == "")
                    {
                        MessageBox.Show("User Login Data Not Complete..", "Info");
                        txtuser.Focus();
                        return;
                    }else
                    {
                        txtpassword.Focus();
                    }
                break;
            }

        }


        private void login()
        {
            users.userid= txtuser.Text;
            users.password = txtpassword.Text;
            if (dataacces_user.userlogin(users.userid,"All") == null)
            {
                MessageBox.Show("User Not Registered", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtuser.Text = "";
                txtpassword.Text = "";
                txtuser.Focus();
            }
            else
            {
                byte[] rijnKey = Encoding.ASCII.GetBytes("abcdefg_abcdefg_abcdefg_abcdefg_");
                byte[] rijnIV = Encoding.ASCII.GetBytes("abcdefg_abcdefg_");
                string getpasswordinput = users.password;
                string getpasswordsql = dataacces_user.Passwordlogin(users.userid,"All");
                string getpassworddata = DecryptIt(getpasswordsql, rijnKey, rijnIV); 

                if (getpassworddata != getpasswordinput)
                {
                    MessageBox.Show("Wrong Password", "Wrong Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtpassword.Text = "";
                    txtpassword.Focus();
                }
                else
                {

                    this.Hide();
                    DBConnections.UserID = txtuser.Text;
                    DBConnections.Name = dataacces_user.NameLogin(users.userid);
                    timer1.Enabled = false;
                    timer1.Stop();
                    timer1.Dispose();

                    if (AppSettings.Location == "Office")
                    {
                        FrmMenu frm = new FrmMenu();
                        frm.ShowDialog();
                        this.Show();
                    }else if(AppSettings.Location == "CREATE RFID")
                    {
                        FrmCreateReg frm = new FrmCreateReg();
                        frm.ShowDialog();
                        this.Show();
                        timer1.Enabled = true;
                        timer1.Start();
                    }
                    else if (AppSettings.Location == "QA5")
                    {
                        FrmQA5 frm = new FrmQA5();
                        frm.ShowDialog();
                        this.Show();
                        timer1.Enabled = true;
                        timer1.Start();
                    }
                    else if (AppSettings.Location == "QA6")
                    {
                        FrmQA6 frm = new FrmQA6();
                        frm.ShowDialog();
                        this.Show();
                        timer1.Enabled = true;
                        timer1.Start();
                    }
                  
                    txtuser.Text = "";
                    txtpassword.Text = "";
                    txtuser.Focus();
                }
            }
        }

        //public string CreateMD5Hash(string input)
        //{            // Step 1, calculate MD5 hash from input
        //    MD5 md5 = System.Security.Cryptography.MD5.Create();
        //    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        //    byte[] hashBytes = md5.ComputeHash(inputBytes);

        //    // Step 2, convert byte array to hex string
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < hashBytes.Length; i++)
        //    {
        //        sb.Append(hashBytes[i].ToString("X2"));
        //    }
        //    return sb.ToString();
        //}
        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (txtpassword.Text == "")
                    {
                        MessageBox.Show("Password Login Data Not Complete..", "Info");
                        txtpassword.Focus();
                        return;
                    }
                    else
                    {
                        login();
                    }
                    break;
            }
        }

      
      

        private void btnSetting_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmUseSQLServer fus = new FrmUseSQLServer();
            fus.ShowDialog();
            dbconnection.AppConnectionStringx();
            AppSettings.BacaMasterLocation();
            lblLocation.Text = AppSettings.Location;
            if (AppSettings.Location != "Office")
            {
                lblLocation.ForeColor = Color.Black;
                label4.Text = "RFID Reader";
                label4.BackColor = Color.Green ;
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                timer1.Enabled = false;
                timer1.Stop();
                StatusMsg.Text = "";
                StatusMsg.BackColor = Color.Transparent;
                label4.Text = "";
                label4.BackColor = Color.Transparent;

            }
            this.Show();
            txtuser.Focus();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();
                timer1.Enabled = false;
                Auto_Get_COM_Ports(true);
            }
            catch (Exception)
            {
                timer1.Stop();
                timer1.Enabled = false;
            }
            finally
            {
                timer1.Start();
                timer1.Enabled = true;
            }
           
        }
       
        string[][] words = null;
       
        private void Auto_Get_COM_Ports(bool bSilent = false)
        {
            try
            {
                string strPORT = "";
                bool iCount = false;

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_SerialPort"))
                {
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        if (queryObj["Caption"] != null && queryObj["Caption"].ToString().Trim().ToUpper().Contains("DENSO WAVE"))
                        {
                            string Caption = queryObj["Caption"].ToString().ToUpper();
                            string DeviceID = queryObj["DeviceID"].ToString();
                            if (!queryObj["Caption"].ToString().Trim().ToUpper().Contains("DISCONNECTED") && DeviceID.Replace("COM","") == AppSettings.comPort)
                            {
                                iCount = true;
                            }else{
                                iCount = false;
                            }
                        }
                    }
                }

                if (iCount == false)
                {
                    StatusMsg.Text = "Disconnected";
                    StatusMsg.BackColor = Color.Red;
                    StatusMsg.ForeColor = Color.White;
                    if (!bSilent)
                        MessageBox.Show(MyConst.WARNING + Environment.NewLine + "More than one DENSO WAVE USB-COM devices are detected. Disconnect the one you don't need",
                                            MsgType.MAIN_V.ToString()) ;
                }
                else
                {
                    StatusMsg.Text = "Connected";
                    StatusMsg.BackColor = Color.FromArgb(0, 192, 0);
                    StatusMsg.ForeColor = Color.White;
                }
                
                if (string.IsNullOrEmpty(AppSettings.comPort))
                {
                    StatusMsg.Text = "No Port!";
                    StatusMsg.BackColor = Color.Red;
                    StatusMsg.ForeColor = Color.White;

                    if (!bSilent)
                        MessageBox.Show(MyConst.WARNING + Environment.NewLine + "No DENSO WAVE USB-COM device is connected to this PC!", MsgType.MAIN_V.ToString());
                }
               
                
            }
            catch (ManagementException e)
            {
                timer1.Stop();
                StatusMsg.Text = "Error Port";
                MessageBox.Show(MyConst.ERROR + Environment.NewLine + "An error occurred while trying to retrieve COM port." + Environment.NewLine +
                                        Environment.NewLine + "Error detail: " + e.Message, MsgType.MAIN_V.ToString());
            }
        }
        internal static class MyConst
        {
            internal const string ERROR = "<< ERROR >>";
            internal const string WARNING = "<< WARNING >>";
            internal const string INFO = "<< INFO >>";

            internal const string OK = "OK";
            internal const string CANCEL = "CANCEL";

            internal const string EXIT = "EXIT";

            internal const string START = "START";
            internal const string STOP = "STOP";

            internal const string TITLE = "UR21 READ DEMO APP";
        }
      
        public string ComPort
        {
            get { return AppSettings.comPort; }
            set { Set(ref AppSettings.comPort, value); }
        }

        private void Set(ref string comPort, string value)
        {
            throw new NotImplementedException();
        }

        public enum MsgType
        {
            MAIN_V,
            MAIN_V_CONFIRM,
            MAIN_VM,
            TERMINATOR
        }
        
        AppSettings appSettings = new AppSettings();


      

    }
}
