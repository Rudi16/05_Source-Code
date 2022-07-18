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
    public partial class FrmNG : Form
    {
        public string Location;
        public FrmNG()
        {
            InitializeComponent();
        }
        Dataacces_NG dataacces_NG = new Dataacces_NG();
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmNG_Load(object sender, EventArgs e)
        {
           // Location = "QA6";
            lblCategoryNG.Text = "Category NG " + Location;
            getNGCategory();
            cbNGCategory.SelectedIndex = -1;
        }

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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbNGType.SelectedIndex != -1)
            {
                if (((KeyValuePair<string, string>)cbNGType.SelectedItem).Value.Equals(""))
                {
                    MessageBox.Show("Please Select NG Type!");
                    return;
                }
                Dataacces_NG.NameNG = ((KeyValuePair<string, string>)cbNGType.SelectedItem).Value;
                Close();
            }
            else
            {
                MessageBox.Show("Please Select NG Type!");
                return;
            }
        }

        public void savelog(string description)
        {
            string tab = "\t";
            String header = "Datetime" + tab + "Description";
            String str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + tab + description;
            AppSettings.SaveLogs(str, header);
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

       
    }
}
