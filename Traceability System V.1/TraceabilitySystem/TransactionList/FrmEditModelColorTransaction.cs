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

namespace TraceabilitySystem
{
    public partial class FrmEditModelColorTransaction : Form
    {
        Dataacces_Model dataacces_Model = new Dataacces_Model();
        Dataacces_Color dataacces_Color = new Dataacces_Color();
        Dataacces_CreateRFID dataacces_CreateRFID = new Dataacces_CreateRFID();
        public string UniqueID;
        public string model;
        public string color;
        public FrmEditModelColorTransaction()
        {
            InitializeComponent();
        }
        Dataacces_NG dataacces_NG = new Dataacces_NG();
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Close();
        }


        public void savelog(string description)
        {
            string tab = "\t";
            String header = "Datetime" + tab + "Description";
            String str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + tab + description;
            AppSettings.SaveLogs(str, header);
        }

        private void FrmEditModelColorTransaction_Load(object sender, EventArgs e)
        {
            
            GetDataModel();
            GetDataColor();
            cbModel.Text = model;
            cbColor.Text = color;
        }

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
        private void GetDataColor()
        {
            try
            {
                Dictionary<string, string> test = new Dictionary<string, string>();

                DataTable dataTable = new DataTable();
                dataTable = dataacces_Color.GetAll();
                cbColor.DataSource = null;
                if (dataTable.Rows.Count > 0)
                {
                    IDictionary<string, string> numberNames = new Dictionary<string, string>();
                    int row = 0;
                    int totrow = dataTable.Rows.Count - 1;
                    while (row <= totrow)
                    {

                        test.Add(dataTable.Rows[row][1].ToString(), dataTable.Rows[row][2].ToString() + "-" + dataTable.Rows[row][3].ToString());
                        row++;

                    }

                    cbColor.DataSource = new BindingSource(test, null);
                    cbColor.DisplayMember = "Value";
                    cbColor.ValueMember = "Key";

                }
            }
            catch (Exception ex)
            {
                savelog("Error Get Data Color!: + " + ex.ToString());
                MessageBox.Show("Error Get Data Color!" + ex.ToString());
            }
        }
        RegisterTag registerTag = new RegisterTag();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            new FrmApproved().ShowDialog();
            if (AppSettings.Approved == true)
            {
                string Model = ((KeyValuePair<string, string>)cbModel.SelectedItem).Value;
                string color = ((KeyValuePair<string, string>)cbColor.SelectedItem).Value;
                registerTag.UniqueID = UniqueID;
                registerTag.Date = "";
                registerTag.Model = Model;
                registerTag.Color = color;
                registerTag.EPC = "";
                registerTag.PIC = "";
                if (dataacces_CreateRFID.EditModelColor(registerTag))
                {
                    MessageBox.Show("Edit Model Color Success");
                    Close();
                }
                else
                {
                    MessageBox.Show("Edit Model Color Failed");
                }

            }
         }
    }
}
