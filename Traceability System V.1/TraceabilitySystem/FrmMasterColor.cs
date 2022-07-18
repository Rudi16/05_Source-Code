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
using System.Threading;

namespace TraceabilitySystem
{
    public partial class FrmMasterColor : Form
    {
        public FrmMasterColor()
        {
            InitializeComponent();
        }
        Dataacces_Color dataacces_Color = new Dataacces_Color();
        XColor color = new XColor();
        private void FrmMasterColor_Load(object sender, EventArgs e)
        {
            panelgrid.Dock = DockStyle.Fill;
         //   panelclientuser.Visible = false;
            
         
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtinitial.Text == "")
            {
                MessageBox.Show("Please Enter Initial Color");
                txtinitial.Focus();
                return;
            }
            if (txtColorName.Text == "")
            {
                MessageBox.Show("Please Enter Color Name");
                txtColorName.Focus();
                return;
            }
            if (btnSave.Text == "Save")
            {

                color.Initial_Color = txtinitial.Text;
                color.Color_Name = txtColorName.Text;
                string check = dataacces_Color.CheckDataColorNameExists(color);

                if (check == null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure want to save the data?", "attention", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (dataacces_Color.RegisterColor(color))
                        {
                            MessageBox.Show("Save Color Successfully");
                            txtinitial.Text = "";
                            txtColorName.Text = "";
                            dataacces_Color.showgrid(DatagridColor);
                            txtinitial.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Save Color Failed!");
                            txtColorName.Focus();
                            return;
                        }
                    }
                }
                else if (check == "Yes")
                {
                    DialogResult dialogResult2 = MessageBox.Show("Color Name Already Exists Flag Delete" + Environment.NewLine + " Will the data be reactivated?", "attention", MessageBoxButtons.YesNo);
                    if (dialogResult2 == DialogResult.Yes)
                    {
                        if (dataacces_Color.DeleteColor(color, "reactived") == true)
                        {
                            MessageBox.Show("Reactived Color succesfully", "Success");
                            dataacces_Color.showgrid(DatagridColor);
                            txtinitial.Text = "";
                            txtColorName.Text = "";
                            dataacces_Color.showgrid(DatagridColor);
                            txtinitial.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Reactived Color Failed", "Failed");
                            return;
                        }
                    }
                }
                else if (check == "No")
                {
                    MessageBox.Show("Color Name Already Exists!");
                    txtColorName.Focus();
                    return;

                }

            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to Update the data?", "attention", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    color.Initial_Color = txtinitial.Text;
                    color.Color_Name = txtColorName.Text;
                    if (dataacces_Color.UpdateColor(color))
                    {
                        MessageBox.Show("Update Color Successfully");
                        btnSave.Text = "Save";
                        BtnEdit.Text = "Edit";
                        txtinitial.Text = "";
                        txtColorName.Text = "";
                        dataacces_Color.showgrid(DatagridColor);
                        txtinitial.Focus();

                    }
                    else
                    {
                        MessageBox.Show("Update Color Failed!");
                        txtColorName.Focus();
                        return;
                    }
                }
            }
        }

        private void FrmMasterColor_Load_1(object sender, EventArgs e)
        {
            panelgrid.Dock = DockStyle.Fill;
            dataacces_Color.showgrid(DatagridColor);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (BtnEdit.Text == "Edit")
            {
                if (DatagridColor.SelectedCells.Count > 0)
                {
                    int cr = DatagridColor.CurrentCell.RowIndex;

                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)DatagridColor.Rows[cr].Cells[0];

                    if (chk.Value != null)
                    {

                        btnSave.Text = "Update";
                        BtnEdit.Text = "Cancel";
                        txtinitial.Text = color.Initial_Color;
                        txtColorName.Text = color.Color_Name;
                        txtinitial.Focus();
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
                btnSave.Text = "Save";
                BtnEdit.Text = "Edit";
                txtinitial.Text = "";
                txtColorName.Text = "";
                txtinitial.Focus();

            }
        }

        private void DatagridColor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                var rows = DatagridColor.Rows;
                for (int i = 0; i < rows.Count; i++)
                {
                    if (rows[i].Selected)
                    {
                        DatagridColor.Rows[i].Cells[0].Value = "true";

                        color.Id_Color = Convert.ToInt32(DatagridColor.SelectedCells[2].Value);
                        color.Initial_Color = DatagridColor.SelectedCells[3].Value.ToString();
                        color.Color_Name = DatagridColor.SelectedCells[4].Value.ToString();

                    }
                    else
                    {
                        DatagridColor.Rows[i].Cells[0].Value = "false";
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
            if (DatagridColor.SelectedCells.Count > 0)
            {
                int cr = DatagridColor.CurrentCell.RowIndex;

                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)DatagridColor.Rows[cr].Cells[0];

                if (chk.Value != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure want to delete the data?", "attention", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {

                        if (dataacces_Color.DeleteColor(color, "nul") == true)
                        {
                            MessageBox.Show("Delete Color succesfully", "Success");
                            dataacces_Color.showgrid(DatagridColor);
                        }
                        else
                        {
                            MessageBox.Show("Delete Color Failed", "Failed");
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

        private void txtinitial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (txtinitial.Text == "")
                {
                    MessageBox.Show("Please Enter Initial Color");
                    txtinitial.Focus();
                    return;
                }
                txtColorName.Focus();
            }
        }

        private void txtColorName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (txtColorName.Text == "")
                {
                    MessageBox.Show("Please Enter Color Name");
                    txtColorName.Focus();
                    return;
                }
                btnSave.Focus();
            }
        }
    }
}
