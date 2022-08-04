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
    public partial class FrmMasterModel : Form
    {

        Dataacces_Model dataacces_Model = new Dataacces_Model();
        Model model = new Model();
        public FrmMasterModel()
        {
            InitializeComponent();
        }
        
        private void FrmMasterModel_Load(object sender, EventArgs e)
        {
            panelgrid.Dock = DockStyle.Fill;
         //   panelclientuser.Visible = false;
            dataacces_Model.showgrid(DatagridModel);


        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
          
        }
        private void btnSaveUpdate_Click(object sender, EventArgs e)
        {
           
        }

       
        private void DatagridUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }
        WaitWnd.WaitWndFun waitForm = new WaitWnd.WaitWndFun();
        private void Btnadd_Click(object sender, EventArgs e)
        {
            if (txtGmcCode.Text == "")
            {
                MessageBox.Show("Please Enter GMC Code");
                txtGmcCode.Focus();
                return;
            }
            if (txtModel.Text == "")
            {
                MessageBox.Show("Please Enter Model");
                txtModel.Focus();
                return;
            }
            if (txtColor.Text == "")
            {
                MessageBox.Show("Please Enter Color");
                txtGmcCode.Focus();
                return;
            }
            if (txtEAN.Text == "")
            {
                MessageBox.Show("Please Enter EAN");
                txtEAN.Focus();
                return;
            }
            if (txtUPC.Text == "")
            {
                MessageBox.Show("Please Enter UPC");
                txtUPC.Focus();
                return;
            }
            if (Btnadd.Text == "Save")
            {

                model.gmccode = txtGmcCode.Text;
                model.model = txtModel.Text;
                model.color = txtColor.Text;
                model.ean = txtEAN.Text;
                model.upc = txtUPC.Text;
                string check = dataacces_Model.CheckDataModelNameExists(model);

                if (check == null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure want to save the data?", "attention", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (dataacces_Model.RegisterModel(model))
                        {
                            MessageBox.Show("Save Model Successfully");
                            txtGmcCode.Text = "";
                            txtModel.Text = "";
                            txtColor.Text = "";
                            txtEAN.Text = "";
                            txtUPC.Text = "";
                            dataacces_Model.showgrid(DatagridModel);
                            txtGmcCode.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Save Model Failed!");
                            txtGmcCode.Focus();
                            return;
                        }
                    }
                }
                else if (check == "Yes")
                {
                    DialogResult dialogResult2 = MessageBox.Show("GMC Code Already Exists Flag Delete" + Environment.NewLine + " Will the data be reactivated?", "attention", MessageBoxButtons.YesNo);
                    if (dialogResult2 == DialogResult.Yes)
                    {
                        if (dataacces_Model.DeleteModel(model, "reactived") == true)
                        {
                            MessageBox.Show("Reactived Model succesfully", "Success");
                            dataacces_Model.showgrid(DatagridModel);
                            txtGmcCode.Text = "";
                            txtModel.Text = "";
                            txtColor.Text = "";
                            txtEAN.Text = "";
                            txtUPC.Text = "";
                            dataacces_Model.showgrid(DatagridModel);
                            txtGmcCode.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Reactived Model Failed", "Failed");
                            return;
                        }
                    }
                }
                else if (check == "No")
                {
                    MessageBox.Show("Gmc Code Already Exists!");
                    txtGmcCode.Focus();
                    return;

                }
                
            }
            else
            {
                model.gmccode = txtGmcCode.Text;
                model.model = txtModel.Text;
                model.color = txtColor.Text;
                model.ean = txtEAN.Text;
                model.upc = txtUPC.Text;
                // string model
                string check = dataacces_Model.CheckDataModelNameExists(model);
                if (check == null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure want to Update the data?", "attention", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (dataacces_Model.UpdateModel(model))
                        {
                            MessageBox.Show("Update Model Successfully");
                            Btnadd.Text = "Save";
                            BtnEdit.Text = "Edit";
                            txtGmcCode.Text = "";
                            txtModel.Text = "";
                            txtColor.Text = "";
                            txtEAN.Text = "";
                            txtUPC.Text = "";
                            dataacces_Model.showgrid(DatagridModel);
                            txtEAN.Focus();

                        }
                        else
                        {
                            MessageBox.Show("Update Model Failed!");
                            txtGmcCode.Focus();
                            return;
                        }
                    }
                }
                else if (check == "No")
                {
                    MessageBox.Show("GMC Code Already Exists!");
                    txtGmcCode.Focus();
                    return;

                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (BtnEdit.Text == "Edit")
            {
                if (DatagridModel.SelectedCells.Count > 0)
                {
                    int cr = DatagridModel.CurrentCell.RowIndex;

                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)DatagridModel.Rows[cr].Cells[0];

                    if (chk.Value != null)
                    {

                        Btnadd.Text = "Update";
                        BtnEdit.Text = "Cancel";
                        txtGmcCode.Text = model.gmccode;
                        txtModel.Text = model.model;
                        txtColor.Text = model.color;
                        txtEAN.Text = model.ean;
                        txtUPC.Text = model.upc;
                        txtGmcCode.Focus();
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
                Btnadd.Text = "Save";
                BtnEdit.Text = "Edit";
                txtGmcCode.Text = "";
                txtModel.Text = "";
                txtColor.Text = "";
                txtEAN.Text = "";
                txtUPC.Text = "";
                txtGmcCode.Focus();

            }
        }

        private void DatagridModel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                var rows = DatagridModel.Rows;
                for (int i = 0; i < rows.Count; i++)
                {
                    if (rows[i].Selected)   
                    {
                        DatagridModel.Rows[i].Cells[0].Value = "true";

                        model.id_model = Convert.ToInt32(DatagridModel.SelectedCells[2].Value);
                        model.gmccode = DatagridModel.SelectedCells[3].Value.ToString();
                        model.model = DatagridModel.SelectedCells[4].Value.ToString();
                        model.color = DatagridModel.SelectedCells[5].Value.ToString();
                        model.ean = DatagridModel.SelectedCells[6].Value.ToString();
                        model.upc = DatagridModel.SelectedCells[7].Value.ToString();
                    }
                    else
                    {
                        DatagridModel.Rows[i].Cells[0].Value = "false";
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
            if (DatagridModel.SelectedCells.Count > 0)
            {
                int cr = DatagridModel.CurrentCell.RowIndex;

                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)DatagridModel.Rows[cr].Cells[0];

                if (chk.Value != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure want to delete the data?", "attention", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {

                        if (dataacces_Model.DeleteModel(model,"nul") == true)
                        {
                            MessageBox.Show("Delete Model succesfully", "Success");
                            dataacces_Model.showgrid(DatagridModel);
                        }
                        else
                        {
                            MessageBox.Show("Delete Model Failed", "Failed");
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
    }
}
