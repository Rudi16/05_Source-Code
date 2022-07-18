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
    public partial class FrmTrasactionListRegister : Form
    {
        public FrmTrasactionListRegister()
        {
            InitializeComponent();
        }
        Dataacces_CreateRFID dataacces_CreateRFID = new Dataacces_CreateRFID();

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        WaitWnd.WaitWndFun waitForm = new WaitWnd.WaitWndFun();
        Dataacces_Model dataacces_Model = new Dataacces_Model();
        private void GetDataModel()
        {
            try
            {
                Dictionary<string, string> test = new Dictionary<string, string>();

                DataTable dataTable = new DataTable();
                dataTable = dataacces_Model.GetAll();
                cbModel.DataSource = null;
                if (dataTable.Rows.Count > 0)
                {
                    IDictionary<string, string> numberNames = new Dictionary<string, string>();
                    int row = 0;
                    int totrow = dataTable.Rows.Count - 1;
                    test.Add("", "");
                    while (row <= totrow)
                    {

                        test.Add(dataTable.Rows[row][1].ToString(), dataTable.Rows[row][2].ToString());
                        row++;

                    }

                    cbModel.DataSource = new BindingSource(test, null);
                    cbModel.DisplayMember = "Value";
                    cbModel.ValueMember = "Key";

                }
            }
            catch (Exception ex)
            {
                savelog("Error Get Data Model!: + " + ex.ToString());
                MessageBox.Show("Error Get Data Model!" + ex.ToString());
            }
        }
        public void savelog(string description)
        {
            string tab = "\t";
            String header = "Datetime" + tab + "Description";
            String str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + tab + description;
            AppSettings.SaveLogs(str, header);
        }
        private void FrmTrasactionListCreate_Load_1(object sender, EventArgs e)
        {
            GetDataModel();
            panelgrid.Dock = DockStyle.Fill;
            dataacces_CreateRFID.showgridTransactionRegister(DatagridTransactionListCreate, "", date1.Value.ToString("yyyy-MM-dd"), date2.Value.ToString("yyyy-MM-dd"));
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string Id_Model = "";
            if (cbModel.SelectedIndex == -1)
            {
                Id_Model = "";
            }
            else
            {
                if ((((KeyValuePair<string, string>)cbModel.SelectedItem).Value) == "")
                {
                    Id_Model = "";

                }
                else
                {
                    Id_Model =((KeyValuePair<string, string>)cbModel.SelectedItem).Value;
                }
            }
            dataacces_CreateRFID.showgridTransactionRegister(DatagridTransactionListCreate, Id_Model, date1.Value.ToString("yyyy-MM-dd"), date2.Value.ToString("yyyy-MM-dd"));
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (DatagridTransactionListCreate.SelectedCells.Count > 0)
            {
                int cr = DatagridTransactionListCreate.CurrentCell.RowIndex;

                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)DatagridTransactionListCreate.Rows[cr].Cells[0];

                if (chk.Value != null)
                {
                  string UniqueID =  DatagridTransactionListCreate.SelectedCells[2].Value.ToString();
                    string model = DatagridTransactionListCreate.SelectedCells[5].Value.ToString();
                    string color = DatagridTransactionListCreate.SelectedCells[6].Value.ToString();

                    if (dataacces_CreateRFID.CheckUniqueHistory(UniqueID) != null)
                    {
                        MessageBox.Show("Can't Edit/nUniqueID has entered the transaction QA5 / QA6");
                        return;
                    }

                    DialogResult dialogResult = MessageBox.Show("Are you sure want to edit the data?", "attention", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        FrmEditModelColorTransaction frm = new FrmEditModelColorTransaction();
                        frm.UniqueID = UniqueID;
                        frm.model = model;
                        frm.color = color;
                        frm.ShowDialog();
                        string Id_Model = "";
                        if (cbModel.SelectedIndex == -1)
                        {
                            Id_Model = "";
                        }
                        else
                        {
                            if ((((KeyValuePair<string, string>)cbModel.SelectedItem).Value) == "")
                            {
                                Id_Model = "";

                            }
                            else
                            {
                                Id_Model = ((KeyValuePair<string, string>)cbModel.SelectedItem).Value;
                            }
                        }
                        dataacces_CreateRFID.showgridTransactionRegister(DatagridTransactionListCreate, Id_Model, date1.Value.ToString("yyyy-MM-dd"), date2.Value.ToString("yyyy-MM-dd"));
                    }
                }
            }
        }

        private void DatagridTransactionListCreate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var rows = DatagridTransactionListCreate.Rows;
                for (int i = 0; i < rows.Count; i++)
                {
                    if (rows[i].Selected)
                    {
                        if (DatagridTransactionListCreate.Rows[i].Cells[0].Value == "true")
                        {
                            DatagridTransactionListCreate.Rows[i].Cells[0].Value = "false";
                        }
                        else
                        {
                            DatagridTransactionListCreate.Rows[i].Cells[0].Value = "true";
                        }
                    }
                    else
                    {
                        DatagridTransactionListCreate.Rows[i].Cells[0].Value = "false";
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
