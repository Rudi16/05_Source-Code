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

using System.Data.SqlClient;
using System.IO;

namespace TraceabilitySystem
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
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
                                txtserver.Text = col[0].ToString();
                                cboDatabase.Text = col[1].ToString();
                                txtUsername.Text = col[2].ToString();
                                txtPassword.Text = col[3].ToString();
                                txtfoldershare.Text = col[5].ToString();
                                if (col[4].ToString() == "Wifi")
                                {
                                    rbwifi.Checked = true;
                                }
                                else
                                {
                                    rbbatch.Checked = true;
                                }
                                

                            }

                        }
                    }
                    objReader.Dispose();
                }
                else
                {
                    MessageBox.Show("Can't Read Database dat");


                }
                //MessageBox.Show(TextLine);

            }
            else
            {
                MessageBox.Show("Database.dat Dosn't Exits");

            }

          
        }
        private void FrmMastercustomer_Load(object sender, EventArgs e)
        {
            PanelUtama.Dock = DockStyle.Fill;
           // waitForm.Show(this);

            ifexistsconfig();
           // waitForm.Close();
            

        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            
            Close();
       
        }

        private void btnsearchdirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                string xx = (folderDlg.SelectedPath + @"\");
                txtfoldershare.Text = xx.Replace("\\\\", @"\");

            }
            btnsearchdirectory.Focus();
        }
        WaitWnd.WaitWndFun waitForm = new WaitWnd.WaitWndFun();
        bool testconnection = false;
        private void Txttest_Click(object sender, EventArgs e)
        {

            SqlConnection SqlCon = new SqlConnection("server=" + txtserver.Text + ";uid=" + txtUsername.Text + ";pwd=" + txtPassword.Text);
            try
            {
                if (SqlCon.State == ConnectionState.Closed)
                {
                    SqlCon.Open();
                }
               
                //if connection was successful,fetch the list of databases available in that server
                SqlCommand SqlCom = new SqlCommand();
                SqlCom.Connection = SqlCon;
                SqlCom.CommandType = CommandType.StoredProcedure;
                SqlCom.CommandText = "sp_databases";        //sp_databases procedure used to fetch list of available databases

                SqlDataReader SqlDR;
                SqlDR = SqlCom.ExecuteReader();
                waitForm.Close();
                while (SqlDR.Read())
                {
                    cboDatabase.Items.Add(SqlDR.GetString(0));
                    testconnection = true;
                }

                MessageBox.Show("Connection Succes", "Succes");
                cboDatabase.Focus();
            }
            catch
            {
                waitForm.Close();
                MessageBox.Show("Connection Failed...Please check username and password", "Error");
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if(txtfoldershare.Text == "")
            {
                MessageBox.Show("Please enter the directory for the export folder!");
                btntestconnection.Focus();
                return;
            }
            if (rbbatch.Checked  == false && rbwifi.Checked == false) { 
            MessageBox.Show("Please select type connection handy therminal!");
                return;
            }
            if (testconnection == false)
            {
                MessageBox.Show("Please test connection first!");
                btntestconnection.Focus();
                return;
            
            }

            try
            {
                SqlConnection SqlCon = new SqlConnection("server=" + txtserver.Text + ";uid=" + txtUsername.Text + ";pwd=" + txtPassword.Text + ";Initial Catalog=" + cboDatabase.Text);
                SqlDataAdapter dtAdapter;
                DataTable dtt = new DataTable();
                string strQuery = "Select *from " + cboDatabase.Text + ".INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Sato_MasterSKU'";
                if(SqlCon.State == ConnectionState.Closed)
                {
                    SqlCon.Open();
                }
                
                dtAdapter = new SqlDataAdapter(strQuery, SqlCon);
                dtAdapter.Fill(dtt);
                SqlCon.Close();
                if (dtt.Rows.Count > 0)
                {

                    string pthConfig = Application.StartupPath + "\\Config\\Database.dat";

                    StreamWriter SaveSettingConfig = new StreamWriter(pthConfig, false);
                    string strDataConfig = "";
                    string typeconnection = "";
                    // object dt = DateTime.Today.ToString("yyyy-MM-dd");
                    if (rbwifi.Checked == true)
                    {
                        typeconnection = "Wifi";

                    }else if(rbbatch.Checked == true)
                    {
                        typeconnection = "Batch";
                    }
                    strDataConfig = txtserver.Text + "|" + cboDatabase.Text + "|" + txtUsername.Text + "|" + txtPassword.Text + "|" + typeconnection.Trim() + "|"  + txtfoldershare.Text ;
                    DBConnections.TypeConnection = typeconnection.Trim() ;
                    AppSettings.foldershare = txtfoldershare.Text.Trim();
                    SaveSettingConfig.WriteLine(strDataConfig);
                    SaveSettingConfig.Close();

                    DBConnections dbconnection = new DBConnections();
                    dbconnection.AppConnectionStringx();

                    if (DBConnections.TypeConnection == "Batch")
                    {
                        dbconnection.AppConnectionStringCEx();
                    }
                }
                else
                {
                    MessageBox.Show("Database Not Valid", "Attention");
                    return;
                }
                
                MessageBox.Show("Save Configuration Settings Succes");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return;
            }

        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
