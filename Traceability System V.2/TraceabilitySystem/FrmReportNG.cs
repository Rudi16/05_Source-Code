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
using Excel = Microsoft.Office.Interop.Excel;
namespace TraceabilitySystem
{
    public partial class FrmReportNG : Form
    {
        public FrmReportNG()
        {
            InitializeComponent();
        }
        Dataacces_ReportNG dataacces_ReportNG = new Dataacces_ReportNG();
        WaitWnd.WaitWndFun waitForm = new WaitWnd.WaitWndFun();
        private void FrmReportNG_Load(object sender, EventArgs e)
        {

            panelgrid.Dock = DockStyle.Fill;
            int Sw = System.Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 0.4);
            int Sh = System.Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.4);
            panelCategory.Size = new Size(Sw, Sh);
            int x = Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2;

            int y = Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height / 2;
            panelCategory.Location = new Point(x, y);

            int Swx = System.Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 0.4);
            int Shx = System.Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.4);
            PanelNGName.Size = new Size(Swx, Shx);
            cbNGType.Text = "-Select-";
            // waitForm.Show();
            // panelgrid.Dock = DockStyle.Fill;
            // cbLocation.SelectedIndex = 0;
            //// cbNG.SelectedIndex = 0;

            // reportQA5.date1 = date1.Value.ToString("yyyy-MM-dd") + " 00:00:01";
            // reportQA5.date2 = date2.Value.ToString("yyyy-MM-dd") + " 23:59:59";

            //// dataacces_ReportQA.showgridReportQA5(Datagridsummary, reportQA5);
            // waitForm.Close();
        }

      
        ReportQA5 reportQA5 = new ReportQA5();
        private void btnSearch_Click(object sender, EventArgs e)
        {
            waitForm.Show();
            string dat1 = date1.Value.ToString("yyyy-MM-dd") + " 00:00:01";
            string dat2 = date2.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            string location = "";
            if (cbLocation.Text == "QA5 (Testing)")
                location = "QA5";
            else if (cbLocation.Text == "QA6 (Final Inspection)")
                location = "QA6";
            else
                location = "-Select-";

            dataacces_ReportNG.showgridReportNG(Datagridsummary, dat1, dat2, location,cbNGType.Text);
            waitForm.Close();

        }

        private void getNGCategory()
        {
            try
            {
                Dictionary<string, string> test = new Dictionary<string, string>();
                
                DataTable dataTable = new DataTable();
                string location = "";
                if (cbLocation.Text == "QA5 (Testing)")
                    location = "QA5";
                else if (cbLocation.Text == "QA6 (Final Inspection)")
                    location = "QA6";
                else
                    location = "-Select-";
                cbNGType.Items.Clear();
                cbNGType.Items.Add("-Select-");
                cbNGType.SelectedIndex = 0;
                dataTable = dataacces_ReportNG.GetNGCategory(location);
                cbNGCategory.DataSource = null;
                if (dataTable.Rows.Count > 0)
                {
                    IDictionary<string, string> numberNames = new Dictionary<string, string>();
                    int row = 0;
                    int totrow = dataTable.Rows.Count - 1;
                    test.Add("00000000", "-Select-");
                    while (row <= totrow)
                    {

                        test.Add(dataTable.Rows[row][0].ToString(), dataTable.Rows[row][1].ToString());
                        row++;

                    }
                    cbNGCategory.DataSource = new BindingSource(test, null);
                    cbNGCategory.DisplayMember = "Value";
                    cbNGCategory.ValueMember = "Key";
                    cbNGCategory.SelectedIndex = 0;

                }
            }
            catch (Exception ex)
            {
                savelog("Error  Get NG Category!: + " + ex.ToString());
                MessageBox.Show("Error Get NG Category!" + ex.ToString());
            }
        }
        public void savelog(string description)
        {
            string tab = "\t";
            String header = "Datetime" + tab + "Description";
            String str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + tab + description;
            AppSettings.SaveLogs(str, header);
        }

        private void cbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            getNGCategory();
        }
        private void GetNGType(int IDNGCategory)
        {
            try
            {

                Dictionary<string, string> test = new Dictionary<string, string>();

                DataTable dataTable = new DataTable();
                dataTable = dataacces_ReportNG.GetNGType(IDNGCategory);
                cbNGType.DataSource = null;
                if (dataTable.Rows.Count > 0)
                {
                    IDictionary<string, string> numberNames = new Dictionary<string, string>();
                    int row = 0;
                    int totrow = dataTable.Rows.Count - 1;
                    test.Add("0000000", "-Select-");
                    while (row <= totrow)
                    {

                        test.Add(dataTable.Rows[row][0].ToString(), dataTable.Rows[row][1].ToString());
                        row++;

                    }

                    cbNGType.DataSource = new BindingSource(test, null);
                    cbNGType.DisplayMember = "Value";
                    cbNGType.ValueMember = "Key";
                    cbNGType.SelectedIndex = 0;
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
        private void cbNGCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNGCategory.SelectedIndex != 0)
            {
                int IDNGCategory = Convert.ToInt32(((KeyValuePair<string, string>)cbNGCategory.SelectedItem).Key);
                GetNGType(IDNGCategory);
            }
            else
            {
                cbNGType.DataSource = null;
                cbNGType.Items.Clear();
                cbNGType.Items.Add("-Select-");
                cbNGType.SelectedIndex = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Datagridsummary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            waitForm.Show();
            string dat1 = date1.Value.ToString("yyyy-MM-dd") + " 00:00:01";
            string dat2 = date2.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            string location = "";
            if (cbLocation.Text == "QA5 (Testing)")
                location = "QA5";
            else if (cbLocation.Text == "QA6 (Final Inspection)")
                location = "QA6";
            else
                location = "-Select-";
             string ngtype = Datagridsummary.SelectedCells[1].Value.ToString();
            dataacces_ReportNG.showgridReportNGDetail(datagriddetail, dat1, dat2, location, ngtype);
            waitForm.Close();

        }

        private void btnExport_Click(object sender, EventArgs e)
        {

        }

        private void btnExportsummary_Click(object sender, EventArgs e)
        {
            if (Datagridsummary.RowCount == 0)
            {
                MessageBox.Show("No Data Exported", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Datagridsummary.Rows.Clear();

                return;
            }

            Datagridsummary.AllowUserToAddRows = true;
            ExportToExcel();
            Datagridsummary.AllowUserToAddRows = false;
        }

        private Excel.Range workSheet_range = null;
        public void ExportToExcel()
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Report QA5";

            // maximized excel
            app.WindowState = Excel.XlWindowState.xlMaximized;

            // storing header part in Excel  
            for (int i = 1; i < Datagridsummary.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = Datagridsummary.Columns[i - 1].HeaderText;
            }
            //storing Each row and column value to excel sheet
            for (int i = 1; i < Datagridsummary.Rows.Count; i++)
            {
                for (int j = 0; j < Datagridsummary.Columns.Count; j++)
                {
                    worksheet.Cells[i + 1, j + 1] = Datagridsummary.Rows[i - 1].Cells[j].Value.ToString();
                }
            }
            // worksheet.Cells["A:B"].Style.Font.Bold = true;
            worksheet.Columns.HorizontalAlignment = 2;
            worksheet.Columns.AutoFit();
            this.Enabled = true;
            this.Cursor = Cursors.Default;


        }
        public void createHeaders(int row, int col, string htext, string cell1, string cell2, int mergeColumns, string b, bool font, int size, string fcolor, Microsoft.Office.Interop.Excel._Worksheet worksheet)
        {
            worksheet.Cells[row, col] = htext;
            workSheet_range = worksheet.get_Range(cell1, cell2);
            workSheet_range.Merge(mergeColumns);
            workSheet_range.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.Font.Bold = font;
            workSheet_range.ColumnWidth = size;
        }

        private void btnExportDetail_Click(object sender, EventArgs e)
        {
            if (datagriddetail.RowCount == 0)
            {
                MessageBox.Show("No Data Exported", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Datagridsummary.Rows.Clear();

                return;
            }

            datagriddetail.AllowUserToAddRows = true;
            ExportToExcel2();
            datagriddetail.AllowUserToAddRows = false;
        }


        public void ExportToExcel2()
        {
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Report QA5";

            // maximized excel
            app.WindowState = Excel.XlWindowState.xlMaximized;

            // storing header part in Excel  
            for (int i = 1; i < datagriddetail.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = datagriddetail.Columns[i - 1].HeaderText;
            }
            //storing Each row and column value to excel sheet
            for (int i = 1; i < datagriddetail.Rows.Count; i++)
            {
                for (int j = 0; j < datagriddetail.Columns.Count; j++)
                {
                    worksheet.Cells[i + 1, j + 1] = datagriddetail.Rows[i - 1].Cells[j].Value.ToString();
                }
            }
            // worksheet.Cells["A:B"].Style.Font.Bold = true;
            worksheet.Columns.HorizontalAlignment = 2;
            worksheet.Columns.AutoFit();
            this.Enabled = true;
            this.Cursor = Cursors.Default;


        }

        private void cbNGType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}