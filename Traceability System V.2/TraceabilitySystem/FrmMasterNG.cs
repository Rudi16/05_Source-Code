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
using System.IO;

namespace TraceabilitySystem
{
    public partial class FrmMasterNG : Form
    {
        ngCategory ngcategory = new ngCategory();
        NGList nglist = new NGList();
        Dataacces_NG Dataacces_ng = new Dataacces_NG();
        public FrmMasterNG()
        {
            InitializeComponent();
        }
        string[][] words = null;
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
                                if (words[i][0].ToString().Contains("QA"))
                                {
                                    cbLocation.Items.Add(words[i][0].ToString());
                             
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
        private void FrmMasterModel_Load(object sender, EventArgs e)
        {
            panelgrid.Dock = DockStyle.Fill;
            ReadMasterLocation();
            endiscategory(false);
            Dataacces_ng.showgridNGCategory(DatagridCategory);
            int Sw = System.Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 0.4);
            int Sh = System.Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.4);
            panelCategory.Size = new Size(Sw, Sh);
            int x = Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2;

            int y = Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height / 2;
            panelCategory.Location = new Point(x, y);

            int Swx = System.Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 0.4);
            int Shx = System.Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.4);
            PanelNGName.Size = new Size(Swx, Shx);
            
        }
        public void endiscategory(bool x)
        {
            txtNGCategory.Enabled = x;
            cbLocation.Enabled = x;
            txtDescription.Enabled = x;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void DatagridModel_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnNewCategory_Click(object sender, EventArgs e)
        {
            if (btnNewCategory.Text == "Create New")
            {
                btnNewCategory.Text = "Cancel";
                endiscategory(true);
                txtNGCategory.Focus();
                txtNGCategory.BackColor = Color.White;
                txtDescription.BackColor = Color.White;
                cbLocation.BackColor = Color.White;
            }
            else
            {
                btnNewCategory.Text = "Create New";
                endiscategory(false);
                txtNGCategory.Text = "";
                txtDescription.Text = "";
                cbLocation.SelectedIndex = -1;
                txtNGCategory.BackColor = Color.FromArgb(224, 224, 224);
                txtDescription.BackColor = Color.FromArgb(224, 224, 224);
                cbLocation.BackColor = Color.FromArgb(224, 224, 224);
            }

        }

        private void txtNGCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (txtNGCategory.Text == "")
                {
                    MessageBox.Show("Error : Please Enter NG Category");
                    txtNGCategory.Focus();
                    return;
                }
                cbLocation.Focus();
            }
        }

        private void cbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDescription.Focus();
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnSaveCategory.Focus();
            }
        }

        private void btnSaveCategory_Click(object sender, EventArgs e)
        {
            if (btnSaveCategory.Text == "Save")

            {
                if(btnNewCategory.Text == "Create New")
                {
                    MessageBox.Show("Error : Please Click Button Create New");
                    btnNewCategory.Focus();
                    return;
                }
                if (txtNGCategory.Text == "")
                {
                    MessageBox.Show("Error : Please Enter NG Category");
                    txtNGCategory.Focus();
                    return;
                }
                if (cbLocation.SelectedIndex == -1)
                {
                    MessageBox.Show("Error : Please Select Location");
                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Are you sure want to save the data?", "attention", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    ngcategory.NGCategory = txtNGCategory.Text;
                    ngcategory.Location = cbLocation.Text;
                    ngcategory.Description = txtDescription.Text;
                    if (Dataacces_ng.SaveNGCategory(ngcategory))
                    {
                        txtDescription.Text = "";
                        txtNGCategory.Text = "";
                        cbLocation.SelectedIndex = - 1;
                        endiscategory(false);
                        Dataacces_ng.showgridNGCategory(DatagridCategory);
                        MessageBox.Show("save data successfully");
                    }
                    else
                    {
                        MessageBox.Show("Save data failed");
                    }
                }

            }
            else
            {
                //Update Data
          
                if (txtNGCategory.Text == "")
                {
                    MessageBox.Show("Error : Please Enter NG Category");
                    txtNGCategory.Focus();
                    return;
                }
                if (cbLocation.SelectedIndex == -1)
                {
                    MessageBox.Show("Error : Please Select Location");
                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Are you sure want to update the data?", "attention", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    ngcategory.NGCategory = txtNGCategory.Text;
                    ngcategory.Location = cbLocation.Text;
                    ngcategory.Description = txtDescription.Text;
                    if (Dataacces_ng.UpdateNGCategory(ngcategory))
                    {
                        txtDescription.Text = "";
                        txtNGCategory.Text = "";
                        cbLocation.SelectedIndex = -1;
                        endiscategory(false);
                        btnSaveCategory.Text = "Save";
                        btnEditCategory.Text = "Edit";
                        txtNGCategory.ReadOnly = false;
                        Dataacces_ng.showgridNGCategory(DatagridCategory);
                        MessageBox.Show("Update data successfully");
                    }
                    else
                    {
                        MessageBox.Show("Update data failed");
                    }
                }

            }
        }

        private void DatagridCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var rows = DatagridCategory.Rows;
                for (int i = 0; i < rows.Count; i++)
                {
                    if (rows[i].Selected)
                    {

                        DatagridCategory.Rows[i].Cells[0].Value = "true";
                        ngcategory.IdNGCategory = Convert.ToInt32(DatagridCategory.SelectedCells[2].Value);
                        ngcategory.NGCategory = DatagridCategory.SelectedCells[3].Value.ToString();
                        ngcategory.Location = DatagridCategory.SelectedCells[4].Value.ToString();
                        ngcategory.Description = DatagridCategory.SelectedCells[5].Value.ToString();
                        Dataacces_ng.showgridNGList(datagridListNG, ngcategory.IdNGCategory);
                    }
                    else
                    {
                        DatagridCategory.Rows[i].Cells[0].Value = "false";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            if (btnEditCategory.Text == "Edit")
            {
                if (DatagridCategory.SelectedCells.Count > 0)
                {
                    int cr = DatagridCategory.CurrentCell.RowIndex;
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)DatagridCategory.Rows[cr].Cells[0];
                    if (chk.Value != null)
                    {
                        btnSaveCategory.Text = "Update";
                        btnEditCategory.Text = "Cancel";
                        txtNGCategory.Text = ngcategory.NGCategory;
                        txtNGCategory.ReadOnly = true;
                        cbLocation.Text = ngcategory.Location;
                        txtDescription.Text  = ngcategory.Description;
                        txtNGCategory.BackColor = Color.White;
                        txtDescription.BackColor = Color.White;
                        cbLocation.BackColor = Color.White;
                        endiscategory(true);
                    }
                    else
                    {
                        MessageBox.Show("Please select data");
                        return;
                    }
                }
            }
            else
            {
                btnSaveCategory.Text = "Save";
                btnEditCategory.Text = "Edit";
                txtNGCategory.Text = "";
                txtNGCategory.ReadOnly = false;
                cbLocation.Text ="";
                txtDescription.Text = "";
                cbLocation.SelectedIndex = -1;
                txtNGCategory.BackColor = Color.FromArgb(224, 224, 224);
                txtDescription.BackColor = Color.FromArgb(224, 224, 224);
                cbLocation.BackColor = Color.FromArgb(224, 224, 224);

            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (DatagridCategory.SelectedCells.Count > 0)
            {
                int cr = DatagridCategory.CurrentCell.RowIndex;

                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)DatagridCategory.Rows[cr].Cells[0];

                if (chk.Value != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure want to delete the data?", "attention", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {

                        if (Dataacces_ng.DeleteNGCategory(ngcategory, "nul") == true)
                        {
                            MessageBox.Show("Delete NG category succesfully", "Success");
                            Dataacces_ng.showgridNGCategory(DatagridCategory);
                        }
                        else
                        {
                            MessageBox.Show("Delete NG category Failed", "Failed");
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

        private void btnNewNG_Click(object sender, EventArgs e)
        {
            if (btnNewNG.Text == "Create New"){
                if (DatagridCategory.SelectedCells.Count > 0)
                {
                    int cr = DatagridCategory.CurrentCell.RowIndex;

                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)DatagridCategory.Rows[cr].Cells[0];

                    if (chk.Value != null)
                    {
                        btnNewNG.Text = "Cancel";
                        txtNGName.Enabled = true;
                        txtDescriptionListNG.Enabled = true;
                        btnEditNG.Enabled = false;
                        txtNGName.Focus();
                        txtNGName.BackColor = Color.White;
                        txtDescriptionListNG.BackColor = Color.White;

                    }
                    else
                    {
                        MessageBox.Show("Please Select Category and Location!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Category and Location!");
                    return;
                }
            }
            else
            {
                btnNewNG.Text = "Create New";
                txtNGName.Text = "";
                txtDescriptionListNG.Text = "";
                txtNGName.Enabled = false;
                txtDescriptionListNG.Enabled = false;
                btnEditNG.Enabled = true;
                txtNGName.BackColor = Color.FromArgb(224, 224, 224);
                txtDescriptionListNG.BackColor = Color.FromArgb(224, 224, 224);

            }
        }
        private void btnSaveNG_Click(object sender, EventArgs e)
        {
            if (btnSaveNG.Text == "Save")
            {
                if (txtNGName.Text == "")
                {
                    MessageBox.Show("Please Enter NG Name!");
                    return;
                }
                if (Dataacces_ng.checkNameNG(txtNGName.Text,ngcategory.Location))
                {
                    MessageBox.Show("NG name already exists!");
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Are you sure want to save the data?", "attention", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (Dataacces_ng.SaveNGList(ngcategory.IdNGCategory, txtNGName.Text, txtDescriptionListNG.Text))
                    {
                        MessageBox.Show("Save NG List Success!");
                        Dataacces_ng.showgridNGList(datagridListNG, ngcategory.IdNGCategory);
                        txtNGName.Text = "";
                        txtDescriptionListNG.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Save NG List Failed!");
                    }
                }
            }
            else
            {
                if (txtNGName.Text == "")
                {
                    MessageBox.Show("Please Enter NG Name!");
                    return;
                }

                if (Dataacces_ng.checkNameNGUpdate(txtNGName.Text, ngcategory.Location,lblIdNG.Text))
                {
                    MessageBox.Show("NG name already exists!");
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Are you sure want to update the data?", "attention", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (Dataacces_ng.UpdateNGList(ngcategory.IdNGCategory, txtNGName.Text, txtDescriptionListNG.Text,lblIdNG.Text))
                    {
                        MessageBox.Show("Update NG List Success!");
                        Dataacces_ng.showgridNGList(datagridListNG, ngcategory.IdNGCategory);
                        txtNGName.Text = "";
                        txtDescriptionListNG.Text = "";
                        btnSaveNG.Text = "Save";
                        btnNewNG.Text = "Create New";
                        btnEditNG.Text = "Edit";
                    }
                    else
                    {
                        MessageBox.Show("Save NG List Failed!");
                    }
                }
            }
        }

        private void btnEditNG_Click(object sender, EventArgs e)
        {
            if (btnEditNG.Text == "Edit")
            {
                if (datagridListNG.SelectedCells.Count > 0)
                {
                    int cr = datagridListNG.CurrentCell.RowIndex;
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)datagridListNG.Rows[cr].Cells[0];
                    if (chk.Value != null)
                    {
                        btnSaveNG.Text = "Update";
                        btnEditNG.Text = "Cancel";
                        txtNGName.Text = nglist.NGName;
                        txtDescriptionListNG.Text = nglist.Description;
                        lblIdNG.Text = nglist.IdNG.ToString();
                        txtNGName.Enabled = true;
                        txtDescriptionListNG.Enabled = true;
                        btnNewNG.Enabled = false;
                        btnNewNG.Text = "Create New";
                        txtNGName.BackColor = Color.White;
                        txtDescriptionListNG.BackColor = Color.White;
                    }
                    else
                    {
                        MessageBox.Show("Please select data");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select data");
                    return;
                }

            }
            else
            {
                btnSaveNG.Text = "Save";
                btnEditNG.Text = "Edit";
                txtNGName.Text = "";
                txtDescriptionListNG.Text = "";
                lblIdNG.Text = "";
                txtNGName.Enabled = false;
                txtDescription.Enabled = false;
                btnNewNG.Enabled = true;
                btnNewNG.Text = "Create New";
                txtNGName.BackColor = Color.FromArgb(224, 224, 224);
                txtDescriptionListNG.BackColor = Color.FromArgb(224, 224, 224);

            }
        }
        private void datagridListNG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var rows = datagridListNG.Rows;
                for (int i = 0; i < rows.Count; i++)
                {
                    if (rows[i].Selected)
                    {

                        datagridListNG.Rows[i].Cells[0].Value = "true";
                        nglist.IdNG = Convert.ToInt32(datagridListNG.SelectedCells[2].Value);
                        nglist.NGName = datagridListNG.SelectedCells[3].Value.ToString();
                        nglist.Description = datagridListNG.SelectedCells[4].Value.ToString();
                    }
                    else
                    {
                        datagridListNG.Rows[i].Cells[0].Value = "false";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDeleteNG_Click(object sender, EventArgs e)
        {
            if (datagridListNG.SelectedCells.Count > 0)
            {
                int cr = datagridListNG.CurrentCell.RowIndex;
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)datagridListNG.Rows[cr].Cells[0];
                if (chk.Value != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure want to Delete the data? " + Environment.NewLine + " NG Name : " +  nglist.NGName, "attention", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        
                        if (Dataacces_ng.DeleteNGList(nglist.IdNG.ToString()))
                        {
                            MessageBox.Show("Delete NG List Success!");
                            Dataacces_ng.showgridNGList(datagridListNG, ngcategory.IdNGCategory);
                        }
                        
                    }
                }
                else
                {
                    MessageBox.Show("Please select data");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select data");
                return;
            }

        }
    }
}
