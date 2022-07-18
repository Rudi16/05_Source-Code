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
    public partial class FrmTrasactionListQA5 : Form
    {
        public FrmTrasactionListQA5()
        {
            InitializeComponent();
        }
        Dataacces_QA5 dataacces_QA5 = new Dataacces_QA5();

        WaitWnd.WaitWndFun waitForm = new WaitWnd.WaitWndFun();

        public void savelog(string description)
        {
            string tab = "\t";
            String header = "Datetime" + tab + "Description";
            String str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + tab + description;
            AppSettings.SaveLogs(str, header);
        }
        private void FrmTrasactionListCreate_Load_1(object sender, EventArgs e)
        {
            cbfilter.SelectedIndex = 0;

            string select = cbfilter.Text;
            if (select == "-Select-")
            {
                select = "";
            }
            panelgrid.Dock = DockStyle.Fill;
            dataacces_QA5.showgridTransactionQA5(gridQA5TransactionList, date1.Value.ToString("yyyy-MM-dd"), date2.Value.ToString("yyyy-MM-dd"), select);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string select = cbfilter.Text;
            if (select == "-Select-")
            {
                select = "";
            }
            dataacces_QA5.showgridTransactionQA5(gridQA5TransactionList, date1.Value.ToString("yyyy-MM-dd"), date2.Value.ToString("yyyy-MM-dd") , select);
        }

        private void DatagridTransactionListCreate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            if (AppSettings.Authority == "Operator")
            {
                MessageBox.Show("Can't Access!");
                return;
            }

            if (gridQA5TransactionList.SelectedCells.Count > 0)
            {
                int cr = gridQA5TransactionList.CurrentCell.RowIndex;

                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gridQA5TransactionList.Rows[cr].Cells[0];

                if (chk.Value != null)
                {
                    if (gridQA5TransactionList.SelectedCells[7].Value.ToString() == "NG")
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure want to Approved the data?", "attention", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            if (dataacces_QA5.ApprovedNG(gridQA5TransactionList.SelectedCells[2].Value.ToString()))
                            {
                                MessageBox.Show("Approved Success");
                                string select = cbfilter.Text;
                                if (select == "-Select-")
                                {
                                    select = "";
                                }
                                dataacces_QA5.showgridTransactionQA5(gridQA5TransactionList, date1.Value.ToString("yyyy-MM-dd"), date2.Value.ToString("yyyy-MM-dd"), select);
                            }
                            else
                            {
                                MessageBox.Show("Approved Failed!");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Only NG status can use Approved button!");
                        return;
                    }
                }
            }
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void GetProcessDest()
        {
            try
            {

                Dictionary<string, string> test = new Dictionary<string, string>();

                DataTable dataTable = new DataTable();
                dataTable = dataacces_QA5.GetDestination("QA5");
                cbProcessDest.DataSource = null;
                if (dataTable.Rows.Count > 0)
                {
                    IDictionary<string, string> numberNames = new Dictionary<string, string>();
                    int row = 0;
                    int totrow = dataTable.Rows.Count - 1;
                    while (row <= totrow)
                    {
                        test.Add(dataTable.Rows[row][0].ToString(), dataTable.Rows[row][1].ToString());
                        row++;
                    }
                    cbProcessDest.DataSource = new BindingSource(test, null);
                    cbProcessDest.DisplayMember = "Value";
                    cbProcessDest.ValueMember = "Key";
                }
                else
                {
                    MessageBox.Show("Data Not Found!");
                }
            }
            catch (Exception ex)
            {
                savelog("Error  GetProcessDest!: + " + ex.ToString());
                MessageBox.Show("Error GetProcessDest!" + ex.ToString());
            }
        }
        Dataacces_NG dataacces_NG = new Dataacces_NG();
        public string Location;
        private void getNGCategory()
        {
            try
            {
                Dictionary<string, string> test = new Dictionary<string, string>();

                DataTable dataTable = new DataTable();
                dataTable = dataacces_NG.GetNGCategory(Location);
                cbNGCategory.DataSource = null;
                if (dataTable.Rows.Count > 0)
                {
                    IDictionary<string, string> numberNames = new Dictionary<string, string>();
                    int row = 0;
                    int totrow = dataTable.Rows.Count - 1;
                    while (row <= totrow)
                    {

                        test.Add(dataTable.Rows[row][0].ToString(), dataTable.Rows[row][1].ToString());
                        row++;

                    }
                    cbNGCategory.DataSource = new BindingSource(test, null);
                    cbNGCategory.DisplayMember = "Value";
                    cbNGCategory.ValueMember = "Key";

                }
            }
            catch (Exception ex)
            {
                savelog("Error  Get NG Category!: + " + ex.ToString());
                MessageBox.Show("Error Get NG Category!" + ex.ToString());
            }
        }

        private void GetNGType(int IDNGCategory)
        {
            try
            {

                Dictionary<string, string> test = new Dictionary<string, string>();

                DataTable dataTable = new DataTable();
                dataTable = dataacces_NG.GetNGType(IDNGCategory);
                cbNGType.DataSource = null;
                if (dataTable.Rows.Count > 0)
                {
                    IDictionary<string, string> numberNames = new Dictionary<string, string>();
                    int row = 0;
                    int totrow = dataTable.Rows.Count - 1;
                    while (row <= totrow)
                    {

                        test.Add(dataTable.Rows[row][0].ToString(), dataTable.Rows[row][1].ToString());
                        row++;

                    }

                    cbNGType.DataSource = new BindingSource(test, null);
                    cbNGType.DisplayMember = "Value";
                    cbNGType.ValueMember = "Key";

                }
                else
                {
                    MessageBox.Show("Data Not Found!");
                }
            }
            catch (Exception ex)
            {
                savelog("Error  Get NG Category!: + " + ex.ToString());
                MessageBox.Show("Error Get NG Category!" + ex.ToString());
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                if (AppSettings.Authority == "Operator")
                {
                    MessageBox.Show("Can't Access!");
                    return;
                }

                if (gridQA5TransactionList.SelectedCells.Count > 0)
                {
                    int cr = gridQA5TransactionList.CurrentCell.RowIndex;

                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gridQA5TransactionList.Rows[cr].Cells[0];

                    if (chk.Value != null)
                    {
                        if (gridQA5TransactionList.SelectedCells[7].Value.ToString() == "NG")
                        {
                            if (dgvNGList.Visible == true)
                            {
                                label5.Visible = true;
                                cbNGCategory.Visible = true;
                                label6.Visible = true;
                                cbNGType.Visible = true;
                                label10.Visible = true;
                                cbProcessDest.Visible = true;
                                btnUpdate.Visible = true;
                                btnEdit.Text = "Cancel";
                                dataacces_QA5.showgridLISTNGTransaction(dgvNGList, gridQA5TransactionList.SelectedCells[2].Value.ToString());
                                GetProcessDest();
                                getNGCategory();
                                cbNGCategory.SelectedIndex = -1;
                                cbProcessDest.SelectedIndex = -1;
                            }
                            else
                            {
                                label5.Visible = false;
                                cbNGCategory.Visible = false;
                                label6.Visible = false;
                                cbNGType.Visible = false;
                                label10.Visible = false;
                                cbProcessDest.Visible = false;
                                btnUpdate.Visible = false;
                                btnEdit.Text = "Edit";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Only NG status can use Edit button!");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select data!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select data!");
                    return;
                }
            }
            else
            {
                label5.Visible = false;
                cbNGCategory.Visible = false;
                label6.Visible = false;
                cbNGType.Visible = false;
                label10.Visible = false;
                cbProcessDest.Visible = false;
                btnUpdate.Visible = false;
                btnEdit.Text = "Edit";
            }
        }

        private void cbNGCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNGCategory.SelectedIndex != -1)
            {
                if (((KeyValuePair<string, string>)cbNGCategory.SelectedItem).Key.Equals(""))
                {
                    MessageBox.Show("Please Select NG Category!");
                    return;
                }
                int IDNGCategory = Convert.ToInt32(((KeyValuePair<string, string>)cbNGCategory.SelectedItem).Key);
                GetNGType(IDNGCategory);
                cbNGType.SelectedIndex = -1;

            }
            else
            {
                cbNGType.DataSource = null;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvNGList.SelectedCells.Count == 0)
            {
                MessageBox.Show("Please select data!");
                return;
            }

            if (cbNGType.SelectedIndex != -1)
            {
                if (((KeyValuePair<string, string>)cbNGType.SelectedItem).Value.Equals(""))
                {
                    MessageBox.Show("Please Select NG Type!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please Select NG Type!");
                return;
            }

            if (cbProcessDest.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Destination!");
                return;
            }

            string NGType = ((KeyValuePair<string, string>)cbNGType.SelectedItem).Value;
            string Dest = ((KeyValuePair<string, string>)cbProcessDest.SelectedItem).Value;

            DialogResult dialogResult = MessageBox.Show("Are you sure want to Update the data?", "attention", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (dataacces_QA5.UpdateNG(Convert.ToInt32(dgvNGList.SelectedCells[1].Value), NGType, Dest))
                {
                    dataacces_QA5.showgridLISTNGTransaction(dgvNGList, gridQA5TransactionList.SelectedCells[2].Value.ToString());
                    MessageBox.Show("Update NG List Success");
                    cbNGCategory.SelectedIndex = -1;
                    cbNGType.SelectedIndex = -1;
                    cbProcessDest.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Update NG List Failed!");
                }
            }
            
        }

        private void gridQA5TransactionList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var rows = gridQA5TransactionList.Rows;
                for (int i = 0; i < rows.Count; i++)
                {
                    if (rows[i].Selected)
                    {
                        if (gridQA5TransactionList.Rows[i].Cells[0].Value == "true")
                        {
                            gridQA5TransactionList.Rows[i].Cells[0].Value = "false";
                            dgvNGList.Visible = false;
                        }
                        else
                        {
                            gridQA5TransactionList.Rows[i].Cells[0].Value = "true";
                            dataacces_QA5.showgridLISTNGTransaction(dgvNGList, gridQA5TransactionList.SelectedCells[2].Value.ToString());
                            dgvNGList.Visible = true;
                        }
                    }
                    else
                    {
                        gridQA5TransactionList.Rows[i].Cells[0].Value = "false";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
