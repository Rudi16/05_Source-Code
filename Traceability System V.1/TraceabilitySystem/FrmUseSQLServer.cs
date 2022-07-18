using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using WaitWnd;
using System.Management;

namespace TraceabilitySystem
{
    public partial class FrmUseSQLServer : Form
    {
        public FrmUseSQLServer()
        {
            InitializeComponent();
        }

        WaitWnd.WaitWndFun waitForm = new WaitWnd.WaitWndFun();
        string[][] words = null;
        private void FrmUseSQLServer_Load(object sender, EventArgs e)
        {
            btnsave.Enabled = false;
            txtservername.Select();
            waitForm.Close();
            ifexistsconfig();
            ReadMasterLocation();
            // rbbatch.Checked = true;
            ReadRfidTsParam();
            Auto_Get_COM_Ports(true);
        }
        private void Auto_Get_COM_Ports(bool bSilent = false)
        {
            try
            {
                string strPORT = "";
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_SerialPort"))
                {
                    foreach (ManagementObject queryObj in searcher.Get())
                    {

                        if (queryObj["Caption"] != null && queryObj["Caption"].ToString().Trim().ToUpper().Contains("DENSO WAVE"))
                        {
                            string Caption = queryObj["Caption"].ToString().ToUpper();
                            string DeviceID = queryObj["DeviceID"].ToString();
                            if (!queryObj["Caption"].ToString().Trim().ToUpper().Contains("DISCONNECTED"))
                            {
                                strPORT = queryObj["DeviceID"].ToString().ToUpper();
                                
                            }
                            else
                            {
                                strPORT = queryObj["DeviceID"].ToString().ToUpper();
                                
                            }
                            cbCom.Items.Add(strPORT);
                        }
                    }
                }


            }
            catch (ManagementException e)
            {
                MessageBox.Show(MyConst.ERROR + Environment.NewLine + "An error occurred while trying to retrieve COM port." + Environment.NewLine +
                                        Environment.NewLine + "Error detail: " + e.Message, MsgType.MAIN_V.ToString());
            }
        }
        private void ifexistsconfig()
        {

             if (System.IO.File.Exists(Application.StartupPath + "\\Config\\Database.dat") == true)
            {
                string FILE_NAME = Application.StartupPath + "\\Config\\Database.dat";

                string TextLine = "";
                if (System.IO.File.Exists(FILE_NAME) == true)
                {
                    System.IO.StreamReader objReader = new System.IO.StreamReader(FILE_NAME);
                    while (objReader.Peek() != -1)
                    {
                        TextLine = TextLine + objReader.ReadLine();
                        string[] lines = File.ReadAllLines(FILE_NAME);
                        foreach (string line in lines)
                        {
                            string[] col = line.Split('|');
                            if (col[0].ToString() != "Not Settings")
                            {
                                txtservername.Text = col[0].ToString();
                                txtDatabase.Text = col[1].ToString();
                                txtUserID.Text = col[2].ToString();
                                txtPassword.Text = col[3].ToString();
                                cbCom.Text = "COM" + col[4].ToString();

                            }

                        }
                    }
                    objReader.Dispose();
                }
                else
                {
                    MessageBox.Show("Can't Read Database Config");
                    

                }
                //MessageBox.Show(TextLine);
               
            }
            else
            {
                MessageBox.Show("Config.ini Dosn't Exits");
               
            }
        }

        public void ReadMasterLocation()
        {

            if (System.IO.File.Exists(Application.StartupPath + "\\Config\\MasterLocation.dat") == true)
            {
                string FILE_NAME = Application.StartupPath + "\\Config\\MasterLocation.dat";

                if (System.IO.File.Exists(FILE_NAME) == true)
                {
                    try
                    {
                      // int current_index = -1;
                        string contents = "";
                        try
                        {
                            using (StreamReader file = new StreamReader(FILE_NAME))
                            {
                                contents = file.ReadToEnd();
                            }
                            string[] lines = contents.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            words = new string[lines.GetLength(0)][];
                            for (int i = 0; i < lines.GetLength(0); i++)
                            {
                                string[] split_words = lines[i].Split(new char[] { '|' });
                                words[i] = new string[split_words.GetLength(0)];
                                for (int j = 0; j < split_words.GetLength(0); j++)
                                {
                                    words[i][j] = split_words[j];
                                }



                            }

                            for (int i = 0; i < words.Length; i++)
                            {
                                cbLocation.Items.Add(words[i][0].ToString());
                                if (words[i][1].ToString() == "Active")
                                {
                                    cbLocation.Text = words[i][0].ToString();
                                }
                            }
                            //  MessageBox.Show("File read", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "File not found" + FILE_NAME);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.ToString());
                    }


                }
            }
            else
            {
                MessageBox.Show("Settings.config Dosn't Exits");

            }
        }
        public void ReadRfidTsParam()
        {
            string RfidTsParampth = Application.StartupPath + "\\RfidTsParam.Ini";
            if (System.IO.File.Exists(RfidTsParampth) == true)
            {
                string FILE_NAME = RfidTsParampth;

                if (System.IO.File.Exists(FILE_NAME) == true)
                {
                    try
                    {
                        // int current_index = -1;
                        string contents = "";
                        try
                        {
                            using (StreamReader file = new StreamReader(FILE_NAME))
                            {
                                contents = file.ReadToEnd();
                            }
                            string[] lines = contents.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            words = new string[lines.GetLength(0)][];
                            for (int i = 0; i < lines.GetLength(0); i++)
                            {
                                string[] split_words = lines[i].Split(new char[] { '|' });
                                words[i] = new string[split_words.GetLength(0)];
                                for (int j = 0; j < split_words.GetLength(0); j++)
                                {
                                    words[i][j] = split_words[j];
                                }
                            }
                            string cek = words[1][0].ToString().Replace("CARRIER_POWER_DBM=", "");
                            cbPower.Text = cek;

                            string antena = words[2][0].ToString().Replace("ANTENNA_PORT=", "");
                            cbAntena.Text = antena;
                            //  MessageBox.Show("File read", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "File not found" + FILE_NAME);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("ReadRfidTsParam.config Dosn't Exits");

            }
        }
        public static void SaveLogs(String strEevent, String str, String strHeader = "")
        {
            //FilePerjam
            string sFile = Application.StartupPath + @"\logs_" + DateTime.Now.ToString("yyMMdd_HH") + ".logs";

            // This text is added only once to the file.
            if (!File.Exists(sFile))
            {
                // Create a file to write to.
                string createText = " " + "\t\t\t\t\t" + strHeader + Environment.NewLine + strEevent + Environment.NewLine + "\t\t\t\t\t" + str + Environment.NewLine;
                File.WriteAllText(sFile, createText);

            }
            else
            {
                string appendText = strEevent + Environment.NewLine + "\t\t\t\t\t" + str + Environment.NewLine;
                File.AppendAllText(sFile, appendText);
            }

        }
        private void Txttest_Click(object sender, EventArgs e)
        {
           
            SqlConnection SqlCon = new SqlConnection("server=" + txtservername.Text + ";uid=" + txtUserID.Text + ";pwd=" + txtPassword.Text);
            try
            {
                SqlCon.Open();
                //if connection was successful,fetch the list of databases available in that server
                SqlCommand SqlCom = new SqlCommand();
                SqlCom.Connection = SqlCon;
                SqlCom.CommandType = CommandType.StoredProcedure;
                SqlCom.CommandText = "sp_databases";        //sp_databases procedure used to fetch list of available databases
                SqlDataReader SqlDR;
                SqlDR = SqlCom.ExecuteReader();
                MessageBox.Show("Test Connection Success");
                btnsave.Enabled = true;
            }
            catch(Exception ex)
            {
              
                MessageBox.Show("Connection Failed...Please check username and password: " + ex.ToString());
            }
           
          
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if(txtservername.Text != "" && txtUserID.Text != "" && txtDatabase.Text !="")
            {
                if (System.IO.File.Exists(Application.StartupPath + "\\Traceability.ini"))
                {
                    MessageBox.Show("Please Contact Hamabo Indonesia!");
                    return;
                }
                try
                {

                   SqlConnection SqlCon = new SqlConnection("server=" + txtservername.Text + ";uid=" + txtUserID.Text + ";pwd=" + txtPassword.Text+";Initial Catalog=" + txtDatabase.Text);
                    SqlDataAdapter dtAdapter;
                    DataTable dtt = new DataTable();
                    string strQuery = "Select * from " + txtDatabase.Text + ".INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Sato_UserID'";
                    SqlCon.Open();
                    dtAdapter = new SqlDataAdapter(strQuery, SqlCon);
                    dtAdapter.Fill(dtt);
                    if (dtt.Rows.Count > 0)
                    { 

                        string pth = Application.StartupPath + "\\Config\\Database.dat";
                        
                        StreamWriter SaveSetting = new StreamWriter(pth, false);
                        string strData = "";
                        strData = txtservername.Text + "|" + txtDatabase.Text + "|" + txtUserID.Text + "|" + txtPassword.Text + "|" + cbCom.Text.Replace("COM","");
                        SaveSetting.WriteLine(strData);
                        SaveSetting.Close();
                        DBConnections dbconnection = new DBConnections();
                        dbconnection.AppConnectionStringx();

                        string pth2 = Application.StartupPath + "\\Config\\MasterLocation.dat";
                        StreamWriter SaveSettingx = new StreamWriter(pth2, false);
                        string Location = "";
                        Location = "QA5" + "|";
                        if (cbLocation.Text == "QA5")
                        {
                            Location = "QA5" + "|Active";
                            AppSettings.Location = "QA6";
                        }
                        SaveSettingx.WriteLine(Location);
                        Location = "QA6" + "|";
                        if (cbLocation.Text == "QA6")
                        {
                            Location = "QA6" + "|Active";
                            AppSettings.Location = "QA6";
                        }
                        SaveSettingx.WriteLine(Location);
                        Location = "Office" + "|";
                        
                        if (cbLocation.Text == "Office")
                        {
                            Location = "Office" + "|Active";
                            AppSettings.Location = "Office";
                        }

                        SaveSettingx.WriteLine(Location);
                        Location = "CREATE RFID" + "|";

                        if (cbLocation.Text == "CREATE RFID")
                        {
                            Location = "CREATE RFID" + "|Active";
                            AppSettings.Location = "CREATE RFID";
                        }
                        SaveSettingx.WriteLine(Location);
                        SaveSettingx.Close();

                        string RfidTsParampth = Application.StartupPath + "\\RfidTsParam.Ini";
                        StreamWriter SaveRfidTsParam = new StreamWriter(RfidTsParampth, false);
                        string isiRfidTsParam = "[RF_SETTING]" + Environment.NewLine + "CARRIER_POWER_DBM=" + cbPower.Text + Environment.NewLine + "ANTENNA_PORT=" + cbAntena.Text
                             + Environment.NewLine + "[QUERY]" + Environment.NewLine + "Q_FACTOR=4";
                        SaveRfidTsParam.WriteLine(isiRfidTsParam);
                        SaveRfidTsParam.Close();
                        MessageBox.Show("Save Setting Succes");

                    }
                    else
                    {
                        MessageBox.Show("Database Not Valid","Attention");
                        return;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error Save Database: " + ex.Message);
                    
                 }
            }
            else
            {
                MessageBox.Show("please check the connection first and select the database");
                Txttest.Focus();
                return;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

      
    }
}
