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
using System.Management;

namespace TraceabilitySystem
{
    public partial class FrmReportQA6 : Form
    {
        public FrmReportQA6()
        {
            InitializeComponent();
        }
        Dataacces_ReportQA6 dataacces_ReportQA = new Dataacces_ReportQA6();
        private void FrmReportQA6_Load(object sender, EventArgs e)
        {
            waitForm.Show();
            panelgrid.Dock = DockStyle.Fill;
            cbModel.SelectedIndex = 0;
            cbColor.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            GetChecker();
            cbChecker.SelectedIndex = 0;

            //reportQA5.date1 = date1.Value.ToString("yyyy-MM-dd") + " 00:00:01";
            //reportQA5.date2 = date2.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            //reportQA5.UniqueID = txtUniqueID.Text;
            //reportQA5.Model = cbModel.Text;
            //reportQA5.ModelFilter = cbModelFilter.Text;
            //reportQA5.color = cbColor.Text;
            //reportQA5.colorFilter = cbColorFilter.Text;
            //reportQA5.PIC = cbChecker.Text;
            //reportQA5.StatusProcess = cbStatus.Text;
            //dataacces_ReportQA.showgridReportQA6(DatagridReportQA5, reportQA5);
            getReport();
            waitForm.Close();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }
        ReportQA5 reportQA5 = new ReportQA5();
        WaitWnd.WaitWndFun waitForm = new WaitWnd.WaitWndFun();
        private void btnSearch_Click(object sender, EventArgs e)
        {
            waitForm.Show();
            //reportQA5.date1 = date1.Value.ToString("yyyy-MM-dd") + " 00:00:01";
            //reportQA5.date2 = date2.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            //reportQA5.UniqueID = txtUniqueID.Text;
            //reportQA5.Model = cbModel.Text;
            //reportQA5.ModelFilter = cbModelFilter.Text;
            //reportQA5.color = cbColor.Text;
            //reportQA5.colorFilter = cbColorFilter.Text;
            //reportQA5.PIC = cbChecker.Text;
            //reportQA5.StatusProcess = cbStatus.Text;
            //dataacces_ReportQA.showgridReportQA6(DatagridReportQA5,reportQA5);
            getReport();
            waitForm.Close();
        }
        Report report = new Report();
        private string GetBoardSerialNumbers()
        {
            string results = null;
            try
            {


                string query = "SELECT * FROM Win32_BaseBoard";
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(query);
                foreach (ManagementObject info in searcher.Get())
                {
                    results = info.GetPropertyValue("SerialNumber").ToString().Replace("/", "");
                }

                return results;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Get Board " + ex.ToString());
                return results;

            }
        }
        private void getReport()
        {
            string deviceID = GetBoardSerialNumbers();
            reportQA5.date1 = date1.Value.ToString("yyyy-MM-dd") + " 00:00:01";
            reportQA5.date2 = date2.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            reportQA5.UniqueID = txtUniqueID.Text;
            reportQA5.Model = cbModel.Text;
            reportQA5.ModelFilter = cbModelFilter.Text;
            reportQA5.color = cbColor.Text;
            reportQA5.colorFilter = cbColorFilter.Text;
            reportQA5.PIC = cbChecker.Text;
            reportQA5.StatusProcess = cbStatus.Text;
            reportQA5.DeviceID = deviceID;
            DataTable dataTable = new DataTable();
            dataTable = dataacces_ReportQA.GetDataReportQA6(reportQA5);

            if (dataTable.Rows.Count > 0)
            {

                int number = 0;
                int row = 0;
                int totrow = dataTable.Rows.Count - 1;
                while (row <= totrow)
                {
                    string ROW_NUM = dataTable.Rows[row]["ROW_NUM"].ToString();
                    if (ROW_NUM == "1")
                    {
                        number = number + 1;
                        report.Row_Number = number.ToString();
                        report.History_Unique = dataTable.Rows[row]["History_Unique"].ToString();
                        report.EPC = dataTable.Rows[row]["EPC"].ToString();
                        report.History_Model1 = dataTable.Rows[row]["History_Model1"].ToString();
                        report.History_Color1 = dataTable.Rows[row]["History_Color1"].ToString();
                        report.History_Model2 = dataTable.Rows[row]["History_Model2"].ToString();
                        report.History_Color2 = dataTable.Rows[row]["History_Color2"].ToString();
                        report.History_Model3 = dataTable.Rows[row]["History_Model3"].ToString();
                        report.History_Color3 = dataTable.Rows[row]["History_Color3"].ToString();
                        report.History_TimeIN = dataTable.Rows[row]["History_TimeIN"].ToString();
                        report.History_TimeOUT = dataTable.Rows[row]["History_TimeOUT"].ToString();
                        report.History_PIC = dataTable.Rows[row]["History_PIC"].ToString();
                        report.History_Status = dataTable.Rows[row]["History_Status"].ToString();
                        report.Destination = dataTable.Rows[row]["Destination"].ToString();
                        report.A = dataTable.Rows[row]["A"].ToString();
                        report.B = dataTable.Rows[row]["B"].ToString();
                        report.C = dataTable.Rows[row]["C"].ToString();
                        report.D = dataTable.Rows[row]["D"].ToString();
                        report.E = dataTable.Rows[row]["E"].ToString();
                        report.F = dataTable.Rows[row]["F"].ToString();
                        report.G = dataTable.Rows[row]["G"].ToString();
                        report.H = dataTable.Rows[row]["H"].ToString();
                        report.I = dataTable.Rows[row]["I"].ToString();
                        report.J = dataTable.Rows[row]["J"].ToString();
                        report.K = dataTable.Rows[row]["K"].ToString();
                        report.L = dataTable.Rows[row]["L"].ToString();
                        report.M = dataTable.Rows[row]["M"].ToString();
                        report.N = dataTable.Rows[row]["N"].ToString();
                        report.O = dataTable.Rows[row]["O"].ToString();
                        report.P = dataTable.Rows[row]["P"].ToString();
                        report.Q = dataTable.Rows[row]["Q"].ToString();
                        report.R = dataTable.Rows[row]["R"].ToString();
                        report.S = dataTable.Rows[row]["S"].ToString();
                        report.T = dataTable.Rows[row]["T"].ToString();
                        report.U = dataTable.Rows[row]["U"].ToString();
                        report.V = dataTable.Rows[row]["V"].ToString();
                        report.W = dataTable.Rows[row]["W"].ToString();
                        report.X = dataTable.Rows[row]["X"].ToString();
                        report.Y = dataTable.Rows[row]["Y"].ToString();
                        report.Z = dataTable.Rows[row]["Z"].ToString();
                        report.AA = dataTable.Rows[row]["AA"].ToString();
                        report.AB = dataTable.Rows[row]["AB"].ToString();
                        report.AC = dataTable.Rows[row]["AC"].ToString();
                        report.AD = dataTable.Rows[row]["AD"].ToString();
                        report.AE = dataTable.Rows[row]["AE"].ToString();
                        report.AF = dataTable.Rows[row]["AF"].ToString();
                        report.AG = dataTable.Rows[row]["AG"].ToString();
                        report.AH = dataTable.Rows[row]["AH"].ToString();
                        report.AI = dataTable.Rows[row]["AI"].ToString();
                        report.AJ = dataTable.Rows[row]["AJ"].ToString();
                        report.AK = dataTable.Rows[row]["AK"].ToString();
                        report.AL = dataTable.Rows[row]["AL"].ToString();
                        report.AM = dataTable.Rows[row]["AM"].ToString();
                        report.AN = dataTable.Rows[row]["AN"].ToString();
                        report.AO = dataTable.Rows[row]["AO"].ToString();
                        report.AP = dataTable.Rows[row]["AP"].ToString();
                        report.AQ = dataTable.Rows[row]["AQ"].ToString();
                        report.AR = dataTable.Rows[row]["AR"].ToString();
                        report.AS = dataTable.Rows[row]["AS"].ToString();

                        report.DeviceID = deviceID;
                        dataacces_ReportQA.InsertDataReport(report);
                    }
                    else
                    {
                        report.Row_Number = "";
                        report.History_Unique = "";
                        report.EPC = "";
                        report.History_Model1 = "";
                        report.History_Color1 = "";
                        report.History_Model2 = "";
                        report.History_Color2 = "";
                        report.History_Model3 = "";
                        report.History_Color3 = "";
                        report.History_TimeIN = dataTable.Rows[row]["History_TimeIN"].ToString();
                        report.History_TimeOUT = dataTable.Rows[row]["History_TimeOUT"].ToString();
                        report.History_PIC = dataTable.Rows[row]["History_PIC"].ToString();
                        report.History_Status = dataTable.Rows[row]["History_Status"].ToString();
                        report.Destination = dataTable.Rows[row]["Destination"].ToString();
                        report.A = dataTable.Rows[row]["A"].ToString();
                        report.B = dataTable.Rows[row]["B"].ToString();
                        report.C = dataTable.Rows[row]["C"].ToString();
                        report.D = dataTable.Rows[row]["D"].ToString();
                        report.E = dataTable.Rows[row]["E"].ToString();
                        report.F = dataTable.Rows[row]["F"].ToString();
                        report.G = dataTable.Rows[row]["G"].ToString();
                        report.H = dataTable.Rows[row]["H"].ToString();
                        report.I = dataTable.Rows[row]["I"].ToString();
                        report.J = dataTable.Rows[row]["J"].ToString();
                        report.K = dataTable.Rows[row]["K"].ToString();
                        report.L = dataTable.Rows[row]["L"].ToString();
                        report.M = dataTable.Rows[row]["M"].ToString();
                        report.N = dataTable.Rows[row]["N"].ToString();
                        report.O = dataTable.Rows[row]["O"].ToString();
                        report.P = dataTable.Rows[row]["P"].ToString();
                        report.Q = dataTable.Rows[row]["Q"].ToString();
                        report.R = dataTable.Rows[row]["R"].ToString();
                        report.S = dataTable.Rows[row]["S"].ToString();
                        report.T = dataTable.Rows[row]["T"].ToString();
                        report.U = dataTable.Rows[row]["U"].ToString();
                        report.V = dataTable.Rows[row]["V"].ToString();
                        report.W = dataTable.Rows[row]["W"].ToString();
                        report.X = dataTable.Rows[row]["X"].ToString();
                        report.Y = dataTable.Rows[row]["Y"].ToString();
                        report.Z = dataTable.Rows[row]["Z"].ToString();
                        report.AA = dataTable.Rows[row]["AA"].ToString();
                        report.AB = dataTable.Rows[row]["AB"].ToString();
                        report.AC = dataTable.Rows[row]["AC"].ToString();
                        report.AD = dataTable.Rows[row]["AD"].ToString();
                        report.AE = dataTable.Rows[row]["AE"].ToString();
                        report.AF = dataTable.Rows[row]["AF"].ToString();
                        report.AG = dataTable.Rows[row]["AG"].ToString();
                        report.AH = dataTable.Rows[row]["AH"].ToString();
                        report.AI = dataTable.Rows[row]["AI"].ToString();
                        report.AJ = dataTable.Rows[row]["AJ"].ToString();
                        report.AK = dataTable.Rows[row]["AK"].ToString();
                        report.AL = dataTable.Rows[row]["AL"].ToString();
                        report.AM = dataTable.Rows[row]["AM"].ToString();
                        report.AN = dataTable.Rows[row]["AN"].ToString();
                        report.AO = dataTable.Rows[row]["AO"].ToString();
                        report.AP = dataTable.Rows[row]["AP"].ToString();
                        report.AQ = dataTable.Rows[row]["AQ"].ToString();
                        report.AR = dataTable.Rows[row]["AR"].ToString();
                        report.AS = dataTable.Rows[row]["AS"].ToString();
                        report.DeviceID = deviceID;
                        dataacces_ReportQA.InsertDataReport(report);
                    }
                    row++;
                }
            }
            dataacces_ReportQA.showgridReportQA6(DatagridReportQA5, deviceID);
        }
        private void GetChecker()
        {
            DataTable dataTable = new DataTable();
            dataTable = dataacces_ReportQA.GetChecker();

            if (dataTable.Rows.Count > 0)
            {
                int row = 0;
                int totrow = dataTable.Rows.Count - 1;
                cbChecker.Items.Clear();
                cbChecker.Items.Add("-Select-");
                while (row <= totrow)
                {
                    cbChecker.Items.Add(dataTable.Rows[row]["name"].ToString());
                    row++;
                }
            }
            else
            {
                cbChecker.Items.Clear();
            }
        }
        private void cbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbModel.Text != "-Select-")
            {
                DataTable dataTable = new DataTable();
                dataTable = dataacces_ReportQA.GetModel(cbModel.Text);

                if (dataTable.Rows.Count > 0)
                {
                    int row = 0;
                    int totrow = dataTable.Rows.Count - 1;
                    cbModelFilter.Items.Clear();
                    cbModelFilter.Items.Add("-Select-");
                    while (row <= totrow)
                    {
                        cbModelFilter.Items.Add(dataTable.Rows[row]["Model"].ToString());
                        row++;
                    }
                    cbModelFilter.SelectedIndex = 0;
                }
                else
                {
                    cbModelFilter.Items.Clear();
                    cbModelFilter.Items.Add("-Select-");
                    cbModelFilter.SelectedIndex = 0;

                }
            }
            else
            {
                cbModelFilter.Items.Clear();
                cbModelFilter.Items.Add("-Select-");
                cbModelFilter.SelectedIndex = 0;

            }
        }
        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbColor.Text != "-Select-")
            {
                DataTable dataTable = new DataTable();
                dataTable = dataacces_ReportQA.GetColor(cbColor.Text);
                if (dataTable.Rows.Count > 0)
                {
                    int row = 0;
                    int totrow = dataTable.Rows.Count - 1;
                    cbColorFilter.Items.Clear();
                    cbColorFilter.Items.Add("-Select-");
                    while (row <= totrow)
                    {
                        cbColorFilter.Items.Add(dataTable.Rows[row]["Color"].ToString());
                        row++;
                    }
                    cbColorFilter.SelectedIndex = 0;
                }
                else
                {
                    cbColorFilter.Items.Clear();
                    cbColorFilter.Items.Add("-Select-");
                    cbColorFilter.SelectedIndex = 0;
                }
            }
            else
            {
                cbColorFilter.Items.Clear();
                cbColorFilter.Items.Add("-Select-");
                cbColorFilter.SelectedIndex = 0;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                waitForm.Show();
            if (DatagridReportQA5.RowCount == 0)
            {
                MessageBox.Show("No Data Exported", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DatagridReportQA5.Rows.Clear();
                    waitForm.Close();
                    return;
            }
            DatagridReportQA5.AllowUserToAddRows = true;
            ExportToExcel();
            DatagridReportQA5.AllowUserToAddRows = false;
                
            }
            catch (Exception)
            {
                waitForm.Close();
            }
            finally
            {
                waitForm.Close();
            }
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
            for (int i = 1; i < DatagridReportQA5.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = DatagridReportQA5.Columns[i - 1].HeaderText;
            }
            //storing Each row and column value to excel sheet
            for (int i = 1; i < DatagridReportQA5.Rows.Count; i++)
            {
                for (int j = 0; j < DatagridReportQA5.Columns.Count; j++)
                {
                    worksheet.Cells[i + 1, j + 1] = DatagridReportQA5.Rows[i - 1].Cells[j].Value.ToString();
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
    }
}