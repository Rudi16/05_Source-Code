using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.IO;

namespace TraceabilitySystem
{
    public partial class FrmActivation : Form
    {
        public FrmActivation()
        {
            InitializeComponent();
        }
        public bool IsSaveActivate { get; set; }
        private void FrmActivation_Load(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Double startingPoint = (this.Width / 2) - (g.MeasureString(this.Text.Trim(), this.Font).Width / 2);
            Double widthOfASpace = g.MeasureString(" ", this.Font).Width;
            String tmp = " ";
            Double tmpWidth = 0;

            while ((tmpWidth + widthOfASpace) < startingPoint)
            {
                tmp += " ";
                tmpWidth += widthOfASpace;
            }

            this.Text = tmp + this.Text.Trim();
         
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       
        private bool checklicensekey = false;
        private string getidSN = "";
        private void txtlisencekey_TextChanged(object sender, EventArgs e)
        {
            string getlicense = txtlisencekey.Text.Trim();
            if (getlicense.Length == 16)
            {
                string decrypted = "";
                HexConverter HexConvert = new HexConverter();
                string sData = getlicense;
                decrypted = HexConvert.Data_Hex_Asc(ref sData,txtlisencekey);
                if (decrypted == null) 
                    return;
               
                if (decrypted.Length == 8)
                {
                    if (decrypted.Substring(2,2) == "TP")
                    {
                        pictlisencekey.Image = Image.FromFile(Application.StartupPath + @"\\Asset\\icon\\ceklist.png");
                        checklicensekey = true;
                        getidSN = decrypted.Substring(4, 4);
                        return;
                    }
                    else
                    {
                        pictlisencekey.Image = Image.FromFile(Application.StartupPath + @"\\Asset\\icon\\x.png");
                        checklicensekey = false;
                        return;
                       
                    }
                   
                } else
                {
                    pictlisencekey.Image = Image.FromFile(Application.StartupPath + @"\\Asset\\icon\\x.png");
                    checklicensekey = false;
                    return;
                }

            }else
            {
                pictlisencekey.Image = Image.FromFile(Application.StartupPath + @"\\Asset\\icon\\x.png");
                checklicensekey = false;
                return;
               
            }
        }
        private string dec(string enc,TextBox txt)
        {
            string dat = null;
            if (!string.IsNullOrWhiteSpace(enc) == true)
            {
                try
                {
                    dat = Encrypt.DecryptString(enc, "1605");

                }

                catch (Exception)
                {
                    txt.Focus();
                    return null;
                }

            }
            return dat;
        }
        private string enc(string enc, TextBox txt)
        {
            string dat = null;
            if ((!string.IsNullOrWhiteSpace(enc)) == true)
            {
                try
                {
                    dat = Encrypt.EncryptString(enc, "1605");

                }
                catch (Exception)
                {
                    txt.Focus();
                    return null;
                }

            }
            return dat;
        }


        private string GetBoardSerialNumbers()
        {
            string results = null;
            try
            {

         
            string query = "SELECT * FROM Win32_BaseBoard";
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(query);
            foreach (ManagementObject info in searcher.Get())
            {
               results =info.GetPropertyValue("SerialNumber").ToString();
            }

            return results;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Get Board " + ex.ToString());
                return results;

            }
        }

        // Use WMI to return the CPUs' IDs.
        private string GetCpuIds()
        {
            string results = null;
            try
            {


                    string query = "Select * FROM Win32_Processor";
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(query);
                foreach (ManagementObject info in searcher.Get())
                {
                    results = info.GetPropertyValue("ProcessorId").ToString();
                }

                return results;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error CPU " + ex.ToString());
                return results;

            }
        }

        private void btngetinstallationcode_Click(object sender, EventArgs e)
        {
        
            if (checklicensekey == true)
            {
                if(getidSN != "")
                { 
                    string getinstallationcode = getidSN + GetCpuIds();
                    HexConverter HexConvert = new HexConverter();
                    string sData = getinstallationcode;
                    txtinstallationcode.Text = HexConvert.Data_Asc_Hex(ref sData,txtinstallationcode);
                   // txtinstallationcode.Text = enc(getinstallationcode);
                }
            }
            else
            {
                MessageBox.Show("Please enter the license key correctly!");
                txtlisencekey.Focus();
                return;
            }

        }
        bool checkavtivationcode = false;
        private void txtactivationcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
            string getactivation = txtactivationcode.Text.Trim();
            if (getactivation.Length > 6)
            {
                string decrypted = "";
                decrypted = dec(txtactivationcode.Text, txtactivationcode);
                if (decrypted.Length > 6)
                {
                    int checkCPUid = decrypted.Length - 6;
                    string getidSNActivation = decrypted.Substring(2, 4);
                    string getCPUIDsActivation = decrypted.Substring(6, checkCPUid);
                    if (getidSNActivation  == getidSN && getCPUIDsActivation == GetCpuIds() && checklicensekey == true)
                    {
                        pictactivationcode.Image = Image.FromFile(Application.StartupPath + @"\\Asset\\icon\\ceklist.png");
                        checkavtivationcode = true;
                        
                        return;
                    }
                    else
                    {
                        pictactivationcode.Image = Image.FromFile(Application.StartupPath + @"\\Asset\\icon\\x.png");
                        checkavtivationcode = false;
                        return;

                    }

                }
                else
                {
                    pictactivationcode.Image = Image.FromFile(Application.StartupPath + @"\\Asset\\icon\\x.png");
                    checkavtivationcode = false;
                    return;
                }

            }
            else
            {
                pictactivationcode.Image = Image.FromFile(Application.StartupPath + @"\\Asset\\icon\\x.png");
                checkavtivationcode = false;
                return;

            }
            }
            catch (Exception)
            {
                txtactivationcode.Focus();
                pictactivationcode.Image = Image.FromFile(Application.StartupPath + @"\\Asset\\icon\\x.png");
                checkavtivationcode = false;
                return;
                
            }
        }

        private void btnactivate_Click(object sender, EventArgs e)
        {
            if ( checklicensekey == true && checkavtivationcode == true)
            {
                string pth = Application.StartupPath + "\\Config\\License.dat";
                StreamWriter SaveSetting = new StreamWriter(pth, false);
                string strData = "";
                strData = txtlisencekey.Text;
                SaveSetting.WriteLine(strData);
                strData = txtinstallationcode.Text;
                SaveSetting.WriteLine(strData);
                strData = txtactivationcode.Text;
                SaveSetting.WriteLine(strData);
                SaveSetting.Close();
                IsSaveActivate = true;
                this.Close();



            }
        }
    }
}
