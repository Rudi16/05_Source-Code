using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TraceabilitySystem.Dataacces;

namespace TraceabilitySystem
{
    public partial class FrmApproved : Form
    {
        public FrmApproved()
        {
            InitializeComponent();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Close();
        }
        Users users = new Users();
        Dataacces_Users dataacces_user = new Dataacces_Users();

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
        private void FrmNG_Load(object sender, EventArgs e)
        {
           
        }
       
        private void btnOK_Click(object sender, EventArgs e)
        {
            login();
        }
        private void login()
        {
            users.userid = txtuser.Text;
            users.password = txtpassword.Text;
            if (dataacces_user.userlogin(users.userid,"Leader") == null)
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
                string getpasswordsql = dataacces_user.Passwordlogin(users.userid,"Leader");
                string getpassworddata = DecryptIt(getpasswordsql, rijnKey, rijnIV);

                if (getpassworddata != getpasswordinput)
                {
                    MessageBox.Show("Wrong Password", "Wrong Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtpassword.Text = "";
                    txtpassword.Focus();
                }
                else
                {
                    txtuser.Text = "";
                    txtpassword.Text = "";
                    AppSettings.Approved = true;
                    AppSettings.ApprovedName = dataacces_user.NameLogin(users.userid);
                    Close();
                   
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            AppSettings.Approved = false;
            Close();

        }

        private void txtuser_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtuser_Enter(object sender, EventArgs e)
        {
            if (txtuser.Text == "User ID")
            {
                txtuser.Text = "";
                txtuser.ForeColor = Color.Black;
            }
        }

        private void txtpassword_Enter(object sender, EventArgs e)
        {
            if (txtpassword.Text == "Password")
            {
                txtpassword.Text = "";
                txtpassword.ForeColor = Color.Black;
            }
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
                    }
                    else
                    {
                        txtpassword.Focus();
                    }
                    break;
            }

        }

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
    }
}
