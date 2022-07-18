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

using System.Security.Cryptography;
using System.IO;

namespace TraceabilitySystem
{
    public partial class FrmMasterusers : Form
    {
        public FrmMasterusers()
        {
            InitializeComponent();
        }
        Dataacces_Users dataacces_users = new Dataacces_Users();
        Users users = new Users();
        byte[] rijnKey = Encoding.ASCII.GetBytes("abcdefg_abcdefg_abcdefg_abcdefg_");
        byte[] rijnIV = Encoding.ASCII.GetBytes("abcdefg_abcdefg_");

        private String EncryptIt(String s, byte[] key, byte[] IV)
        {
            String result;
            RijndaelManaged rijn = new RijndaelManaged();
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (ICryptoTransform encryptor = rijn.CreateEncryptor(key, IV))
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(s);
                        }
                    }
                }
                result = System.Convert.ToBase64String(msEncrypt.ToArray());
            }
            rijn.Clear();
            return result;
        }

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


        private void FrmMasterusers_Load(object sender, EventArgs e)
        {
           
            dataacces_users.showgrid(DatagridUser);
            panel1.Dock = DockStyle.Fill;
            panelclientuser.Visible = false;

            if (DBConnections.TypeConnection == "Batch")
            {
                tmrcheckHandyTherminal.Enabled = true;
                tmrcheckHandyTherminal.Start();
                btnupdatemaster.Visible = true;
                panelstatushandy.Visible = true;
            }
            if (AppSettings.Authority == "Operator")
            {
                Btnadd.Enabled = false;
                BtnEdit.Enabled = false;
                BtnDelete.Enabled = false;
            }

        }

        private void Btnadd_Click(object sender, EventArgs e)
        {
           
            panel1.Visible = false;
            panelclientuser.Visible = true;
            panelclientuser.Dock = DockStyle.Fill;
            lbltitleregisedit.Text = "Register User";
            btnSaveUpdate.Text = "Save";
            txtuserid.ReadOnly = false;
            action = "Register";
            txtuserid.Focus();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            users.userid = "";
            users.name = "";
            users.password = "";
            users.authority = "";
            users.active = "";
            txtuserid.Text = "";
            txtusername.Text = "";
            cbactive.Checked = false;
            cbprivilege.Text = "";
            txtpassword.Text = "";
            panel1.Visible = true;
            panelclientuser.Visible = false;
            panelclientuser.Dock = DockStyle.Fill;
            dataacces_users.showgrid(DatagridUser);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        string action = null;
        private void btnSaveUpdate_Click(object sender, EventArgs e)
        {
            if(action == "Register")
            {
                RegisterUser();
            }
            else if(action == "Update")
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to update the data?", "attention", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    UpdateUsers();
                }
               
            }
        }

        private void RegisterUser()
        {
        if (txtuserid.Text == "") { MessageBox.Show("Column UserID is Empty", "Info"); txtuserid.Focus(); return; }
        else if (txtusername.Text == "") { MessageBox.Show("Column Username is Empty", "Info"); txtusername.Focus(); return; }
        else if (cbprivilege.Text == "") { MessageBox.Show("Column Privilege is Empty", "Info"); cbprivilege.Focus(); return; }
        else if (txtpassword.Text == "") { MessageBox.Show("Column Password is Empty", "Info"); txtpassword.Focus(); return; }
            if (dataacces_users.CheckUserExists(txtuserid.Text.Trim()) == false)
            {

                MessageBox.Show("User ID already exists");
                txtuserid.Focus();
                return;
            }

        users.userid = txtuserid.Text.Trim();
        users.password = EncryptIt(txtpassword.Text.Trim(), rijnKey, rijnIV);
        users.name = txtusername.Text.Trim();
        users.authority = cbprivilege.Text.Trim();

        if (cbactive.Checked == true)
        {
            users.active = "Y";
        }
        else
        {
            users.active = "N";
        }


        DialogResult dialogResult = MessageBox.Show("Are you sure want to save the data?", "attention", MessageBoxButtons.YesNo);
        if (dialogResult == DialogResult.Yes)
        {
            if (dataacces_users.RegisterUser(users) == true)
            {
                MessageBox.Show("Register User Succesfully", "Success");
                txtuserid.Text = "";
                txtpassword.Text = "";
                txtusername.Text = "";
                cbprivilege.Text = "";
                cbactive.Checked = false;
                txtuserid.Focus();
            }
            else
            {
                MessageBox.Show("Register Users Failed", "Failed");
                return;
            }

        }
    }

         private void UpdateUsers()
          {
            if (txtuserid.Text == "") { MessageBox.Show("Column UserID is Empty", "Info"); txtuserid.Focus(); return; }
            else if (txtusername.Text == "") { MessageBox.Show("Column Username is Empty", "Info"); txtusername.Focus(); return; }
            else if (cbprivilege.Text == "") { MessageBox.Show("Column Privilege is Empty", "Info"); cbprivilege.Focus(); return; }
            else if (txtpassword.Text == "") { MessageBox.Show("Column Password is Empty", "Info"); txtpassword.Focus(); return; }


            users.userid = txtuserid.Text.Trim();
            users.password = EncryptIt(txtpassword.Text.Trim(), rijnKey, rijnIV);
            users.name = txtusername.Text.Trim();
            users.authority = cbprivilege.Text.Trim();
            if (cbactive.Checked == true)
            {
                users.active = "Y";
            }
            else
            {
                users.active = "N";

            }


            if (dataacces_users.UpdateUser(users) == true)
            {
                MessageBox.Show("Update User Succesfully", "Success");
                panel1.Visible = true;
                panelclientuser.Visible = false;
                panelclientuser.Dock = DockStyle.Fill;
                dataacces_users.showgrid(DatagridUser);
            }
            else
            {
                MessageBox.Show("Update User Failed", "Failed");
                return;
            }


        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (DatagridUser.SelectedCells.Count > 0)
            {
                int cr = DatagridUser.CurrentCell.RowIndex;

                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)DatagridUser.Rows[cr].Cells[0];

                if (chk.Value != null)
                {
                    panel1.Visible = false;
                    panelclientuser.Visible = true;
                    panelclientuser.Dock = DockStyle.Fill;
                    lbltitleregisedit.Text = "Update User";
                    btnSaveUpdate.Text = "Update";
                    action = "Update";
                    txtuserid.ReadOnly = true;

                    txtuserid.Text = users.userid;
                    txtpassword.Text = users.password;
                    txtusername.Text = users.name;
                    cbprivilege.Text = users.authority;
                    if (users.active == "Y")
                    {
                        cbactive.Checked = true;
                    }
                    else
                    {
                        cbactive.Checked = false;

                    }
                    txtusername.Focus();
                }
                else
                {
                    MessageBox.Show("Please select data");
                    return;
                }
            }
        }
        private void txtuserid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (txtuserid.Text == "") { MessageBox.Show("Column UserID is Empty", "Info"); txtuserid.Focus(); return; }else {
                        if (dataacces_users.CheckUserExists(txtuserid.Text.Trim()) == false)
                        {

                            MessageBox.Show("User ID already exists");
                            txtuserid.Focus();
                            return;
                        }
                        txtusername.Focus(); }
                break;
            }
        }

        private void txtusername_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (txtusername.Text == "") { MessageBox.Show("Column UserName is Empty", "Info"); txtusername.Focus(); return; } else { cbprivilege.Focus(); }
                    break;
            }
        }

        private void cbprivilege_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (cbprivilege.Text == "") { MessageBox.Show("Column UserName is Empty", "Info"); cbprivilege.Focus(); return; } else { txtpassword.Focus(); }
                    break;
            }
        }

        private void cbprivilege_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbprivilege.Text == "") { MessageBox.Show("Column Privilege is Empty", "Info"); cbprivilege.Focus(); return; } else { txtpassword.Focus(); }
        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (txtpassword.Text == "") { MessageBox.Show("Column Password is Empty", "Info"); txtpassword.Focus(); return; } else { btnSaveUpdate.Focus(); }
                    break;
            }
        }

        private void DatagridUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                var rows = DatagridUser.Rows;
                for (int i = 0; i < rows.Count; i++)
                {
                    if (rows[i].Selected)
                    {
                        DatagridUser.Rows[i].Cells[0].Value = "true";

                        users.userid = DatagridUser.SelectedCells[2].Value.ToString();
                        users.name = DatagridUser.SelectedCells[4].Value.ToString();
                        users.password = DecryptIt(DatagridUser.SelectedCells[3].Value.ToString(), rijnKey, rijnIV);
                        users.authority = DatagridUser.SelectedCells[5].Value.ToString();
                        users.active = DatagridUser.SelectedCells[6].Value.ToString();

                    }
                    else
                    {
                        DatagridUser.Rows[i].Cells[0].Value = "false";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (DatagridUser.SelectedCells.Count > 0)
            {
                int cr = DatagridUser.CurrentCell.RowIndex;

                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)DatagridUser.Rows[cr].Cells[0];

                if (chk.Value !=null)
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure want to delete the data?", "attention", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {

                            if (dataacces_users.deleteuser(users) == true)
                            {
                                MessageBox.Show("Data delete succesfully", "Success");
                                dataacces_users.showgrid(DatagridUser);
                            }
                            else
                            {
                                MessageBox.Show("Delete Users Failed", "Failed");
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
               
            }

        private void DatagridUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       
       
        private void btnupdatemaster_Click(object sender, EventArgs e)
        {

        }

      private void ckshowpassword_CheckedChanged(object sender, EventArgs e)
        {
        //    if (ckshowpassword.Checked)
        //    {
        //        txtpassword.UseSystemPasswordChar = true;

        //    }
        //    else

        //    { 
        //        txtpassword.UseSystemPasswordChar = false;
        //    }
        }

      
    }
}
