using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data;
using Dapper;
using System.Drawing;
using TraceabilitySystem.Entity;
using System.Threading.Tasks;


namespace TraceabilitySystem
{
   public class Dataacces_ReportQA5
    {
        bool openconnect = false;
        private void OpenConnect(SqlConnection mycon)
        {
            if (mycon.State == ConnectionState.Closed)
            {
                try
                {
                    mycon.Open();
                    openconnect = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("WIFI or Database Connection lost !");
                    openconnect = false;
                    return;
                }
            }
        }

        public DataTable GetDataReportQA5(ReportQA5 reportQA5)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_REPORTQA5_GETDATA]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = mycon;
                        cmd.Parameters.Add("@date1", reportQA5.date1);
                        cmd.Parameters.Add("@date2", reportQA5.date2);
                        cmd.Parameters.Add("@UniqueID", reportQA5.UniqueID);
                        cmd.Parameters.Add("@Model", reportQA5.Model);
                        cmd.Parameters.Add("@ModelFilter", reportQA5.ModelFilter);
                        cmd.Parameters.Add("@Color", reportQA5.color);
                        cmd.Parameters.Add("@ColorFilter", reportQA5.colorFilter);
                        cmd.Parameters.Add("@PIC", reportQA5.PIC);
                        cmd.Parameters.Add("@StatusProcess", reportQA5.StatusProcess);
                        cmd.Parameters.Add("@DeviceID", reportQA5.DeviceID);
                        cmd.CommandText = strQuery;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dt);
                        mycon.Close();
                    }

                }
                return dt;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error GetDataReportQA5 : " + EX.ToString());
                savelog("ErrorGetDataReportQA5 : " + EX.ToString());
                return dt;
            }
        }

        internal DataTable GetChecker()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_REPORTQA5_GETCHECKER]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = mycon;
                        cmd.CommandText = strQuery;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dt);
                        mycon.Close();
                    }

                }
                return dt;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error GetChecker : " + EX.ToString());
                savelog("Error GetChecker : " + EX.ToString());
                return dt;
            }
        }

        internal DataTable GetColor(string text)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_REPORTQA5_COLOR]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = mycon;
                        cmd.Parameters.Add("@Color", text);
                        cmd.CommandText = strQuery;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dt);
                        mycon.Close();
                    }

                }
                return dt;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error GetColor : " + EX.ToString());
                savelog("Error GetColor : " + EX.ToString());
                return dt;
            }
        }

        internal bool InsertDataReport(Report report)
        {
            bool check = false;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_REPORTQA5_INSERTREPORT]";
                        mycon.Query(strQuery, report, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;
                        mycon.Close();
                    }
                }
                return check;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ApprovedNG : " + ex.ToString());
                savelog("Error ApprovedNG : " + ex.ToString());
                return check;
            }
        }

        internal DataTable GetModel(string text)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_REPORTQA5_GETMODEL]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = mycon;
                        cmd.Parameters.Add("@Model", text);
                        cmd.CommandText = strQuery;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dt);
                        mycon.Close();
                    }

                }
                return dt;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error GetModel : " + EX.ToString());
                savelog("Error Get Data NG GetDataProduct: " + EX.ToString());
                return dt;
            }
        }
        private DataTable AutoNumberedTable(DataTable SourceTable)

        {

            DataTable ResultTable = new DataTable();

            DataColumn AutoNumberColumn = new DataColumn();

            AutoNumberColumn.ColumnName = "No.";

            AutoNumberColumn.DataType = typeof(int);

            AutoNumberColumn.AutoIncrement = true;

            AutoNumberColumn.AutoIncrementSeed = 1;

            AutoNumberColumn.AutoIncrementStep = 1;

            ResultTable.Columns.Add(AutoNumberColumn);

            ResultTable.Merge(SourceTable);

            return ResultTable;

        }

        public DataTable GetDataReport(string DeviceID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_REPORTQA5_REPORT]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = mycon;
                        cmd.Parameters.Add("@DeviceID", DeviceID);
                        cmd.CommandText = strQuery;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dt);
                        mycon.Close();
                    }

                }
                return dt;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error GetDataReport : " + EX.ToString());
                savelog("Error GetDataReport : " + EX.ToString());
                return dt;
            }
        }
        public void showgridReportQA5(DataGridView dv,string DeviceID)
        {
            waitForm.Show();
            try
            {
                dv.DataSource = null;
                dv.Rows.Clear();
                dv.Refresh();
                dv.Columns.Clear();
                dv.Refresh();
                DataTable dt = new DataTable();
                dt = GetDataReport(DeviceID);
                if (dt.Rows.Count > 0)
                {
                    dv.DataSource = dt;
                    dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "Unique ID";
                    dv.Columns[2].HeaderText = "EPC";
                    dv.Columns[3].HeaderText = "Model 1";
                    dv.Columns[4].HeaderText = "Color 1";
                    dv.Columns[5].HeaderText = "Model 2";
                    dv.Columns[6].HeaderText = "Color 2";
                    dv.Columns[7].HeaderText = "Model 3";
                    dv.Columns[8].HeaderText = "Color 3";
                    dv.Columns[9].HeaderText = "Time IN";
                    dv.Columns[10].HeaderText = "Time OUT";
                    dv.Columns[11].HeaderText = "Checker";
                    dv.Columns[12].HeaderText = "Status";
                    dv.Columns[13].HeaderText = "Process Dest.";
                    dv.Columns[14].HeaderText = "NG";

                    //var col = new DataGridViewMergedTextBoxColumn();
                    //var col1 = new DataGridViewMergedTextBoxColumn();
                    //var col2 = new DataGridViewMergedTextBoxColumn();
                    //var col3 = new DataGridViewMergedTextBoxColumn();
                    //var col4 = new DataGridViewMergedTextBoxColumn();
                    //var col5 = new DataGridViewMergedTextBoxColumn();
                    //var col6 = new DataGridViewMergedTextBoxColumn();
                    //const string History_Unique = "History_Unique";
                    //col.Name = History_Unique;
                    //col.DataPropertyName = History_Unique;
                    //int colidx = dv.Columns[History_Unique].Index;
                    //dv.Columns.Remove(History_Unique);
                    //dv.Columns.Insert(colidx, col);

                    //const string History_Model1 = "History_Model1";
                    //col1.Name = History_Model1;
                    //col1.DataPropertyName = History_Model1;
                    //int colmodel1 = dv.Columns[History_Model1].Index;
                    //dv.Columns.Remove(History_Model1);
                    //dv.Columns.Insert(colmodel1, col1);

                    //const string History_Color1 = "History_Color1";
                    //col2.Name = History_Color1;
                    //col2.DataPropertyName = History_Color1;
                    //int colcolor1 = dv.Columns[History_Color1].Index;
                    //dv.Columns.Remove(History_Color1);
                    //dv.Columns.Insert(colcolor1, col2);

                    //const string History_Model2 = "History_Model2";
                    //col3.Name = History_Model2;
                    //col3.DataPropertyName = History_Model2;
                    //int colmodel2 = dv.Columns[History_Model2].Index;
                    //dv.Columns.Remove(History_Model2);
                    //dv.Columns.Insert(colmodel2, col3);

                    //const string History_Color2 = "History_Color2";
                    //col4.Name = History_Color2;
                    //col4.DataPropertyName = History_Color2;
                    //int colcolor2 = dv.Columns[History_Color2].Index;
                    //dv.Columns.Remove(History_Color2);
                    //dv.Columns.Insert(colcolor2, col4);

                    //const string History_Model3 = "History_Model3";
                    //col5.Name = History_Model3;
                    //col5.DataPropertyName = History_Model3;
                    //int colmodel3 = dv.Columns[History_Model3].Index;
                    //dv.Columns.Remove(History_Model3);
                    //dv.Columns.Insert(colmodel3, col5);

                    //const string History_Color3 = "History_Color3";
                    //col6.Name = History_Color3;
                    //col6.DataPropertyName = History_Color3;
                    //int colcolor3 = dv.Columns[History_Color3].Index;
                    //dv.Columns.Remove(History_Color3);
                    //dv.Columns.Insert(colcolor3, col6);

                }
                else
                {
                    dv.ColumnCount = 15;
                    dv.ColumnHeadersVisible = true;

                    // Set the column header names.

                    dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "Unique ID";
                    dv.Columns[2].HeaderText = "EPC";
                    dv.Columns[3].HeaderText = "Model 1";
                    dv.Columns[4].HeaderText = "Color 1";
                    dv.Columns[5].HeaderText = "Model 2";
                    dv.Columns[6].HeaderText = "Color 2";
                    dv.Columns[7].HeaderText = "Model 3";
                    dv.Columns[8].HeaderText = "Color 3";
                    dv.Columns[9].HeaderText = "Time IN";
                    dv.Columns[10].HeaderText = "Time OUT";
                    dv.Columns[11].HeaderText = "Checker";
                    dv.Columns[12].HeaderText = "Status";
                    dv.Columns[13].HeaderText = "Process Dest.";
                    dv.Columns[14].HeaderText = "NG";

                    // Populate the rows.
                    string[] row1 = new string[] { "", "", "", "", "", "", "", "", "", "", "", "","","" };

                    object[] rows = new object[] { row1 };

                    foreach (string[] rowArray in rows)
                    {
                        dv.Rows.Add(rowArray);
                    }
                }
                foreach (DataGridViewColumn item in dv.Columns)
                {
                    if (item.Index == 0)
                    {
                        item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                }
                dv.AllowUserToAddRows = false;
                dv.AllowUserToDeleteRows = false;
                dv.AllowUserToOrderColumns = false;
                dv.BorderStyle = BorderStyle.FixedSingle;
                dv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dv.Font = new System.Drawing.Font("Arial", 9);
                dv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dv.GridColor = System.Drawing.Color.White;
                dv.EnableHeadersVisualStyles = false;
                dv.ColumnHeadersDefaultCellStyle.Font = new Font(dv.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);
                dv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                dv.RowsDefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;

                dv.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.Gray;
                dv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
                dv.AlternatingRowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.Gray;
                dv.RowHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.Gray;
                dv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dv.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.ActiveCaption;
                dv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
                dv.RowHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.ActiveCaption;
                dv.RowHeadersDefaultCellStyle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
                dv.ReadOnly = true;
                waitForm.Close();

            }
            catch (Exception ex)
            {

                waitForm.Close();
                MessageBox.Show("Error Show Datagrid " + ex.ToString());
                savelog(ex.ToString());
            }

        }
        WaitWnd.WaitWndFun waitForm = new WaitWnd.WaitWndFun();
        public void savelog(string description)
        {
            string tab = "\t";
            String header = "Datetime" + tab + "Description";
            String str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + tab + description;
            AppSettings.SaveLogs(str, header);
        }
    }
}
