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



namespace TraceabilitySystem
{
   public class Dataacces_ReportQA6
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

        public DataTable GetDataReportQA6(ReportQA5 reportQA5)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_REPORTQA6_GETDATA]";
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
                        const string strQuery = @"[dbo].[SATO_REPORTQA6_GETCHECKER]";
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
                        const string strQuery = @"[dbo].[SATO_REPORTQA6_COLOR]";
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
                        const string strQuery = @"[dbo].[SATO_REPORTQA6_GETMODEL]";
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
                savelog("Error GetModel: " + EX.ToString());
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
                        const string strQuery = @"[dbo].[SATO_REPORTQA6_INSERTREPORT]";
                        mycon.Query(strQuery, report, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;
                        mycon.Close();
                    }
                }
                return check;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error InsertDataReport : " + ex.ToString());
                savelog("Error InsertDataReport : " + ex.ToString());
                return check;
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
                        const string strQuery = @"[dbo].[SATO_REPORTQA6_REPORT]";
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
        public void showgridReportQA6(DataGridView dv, string DeviceID)
        {
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
                dv.Columns[14].HeaderText = "A";
                dv.Columns[15].HeaderText = "B";
                dv.Columns[16].HeaderText = "C";
                dv.Columns[17].HeaderText = "D";
                dv.Columns[18].HeaderText = "E";
                dv.Columns[19].HeaderText = "F";
                dv.Columns[20].HeaderText = "G";
                dv.Columns[21].HeaderText = "H";
                dv.Columns[22].HeaderText = "I";
                dv.Columns[23].HeaderText = "J";
                dv.Columns[24].HeaderText = "K";
                dv.Columns[25].HeaderText = "L";
                dv.Columns[26].HeaderText = "M";
                dv.Columns[27].HeaderText = "N";
                dv.Columns[28].HeaderText = "O";
                dv.Columns[29].HeaderText = "P";
                dv.Columns[30].HeaderText = "Q";
                dv.Columns[31].HeaderText = "R";
                dv.Columns[32].HeaderText = "S";
                dv.Columns[33].HeaderText = "T";
                dv.Columns[34].HeaderText = "U";
                dv.Columns[35].HeaderText = "V";
                dv.Columns[36].HeaderText = "W";
                dv.Columns[37].HeaderText = "X";
                dv.Columns[38].HeaderText = "Y";
                dv.Columns[39].HeaderText = "Z";
                dv.Columns[40].HeaderText = "AA";
                dv.Columns[41].HeaderText = "AB";
                dv.Columns[42].HeaderText = "AC";
                dv.Columns[43].HeaderText = "AD";
                dv.Columns[44].HeaderText = "AE";
                dv.Columns[45].HeaderText = "AF";
                dv.Columns[46].HeaderText = "AG";
                dv.Columns[47].HeaderText = "AH";
                dv.Columns[48].HeaderText = "AI";
                dv.Columns[49].HeaderText = "AJ";
                dv.Columns[50].HeaderText ="AK";
                dv.Columns[51].HeaderText ="AL";
                dv.Columns[52].HeaderText ="AM";
                dv.Columns[53].HeaderText ="AN";
                dv.Columns[54].HeaderText ="AO";
                dv.Columns[55].HeaderText ="AP";
                dv.Columns[56].HeaderText ="AQ";
                dv.Columns[57].HeaderText ="AR";
                dv.Columns[58].HeaderText ="AS";


                }
                else
                {
                    dv.ColumnCount = 14;
                    dv.ColumnHeadersVisible = true;

                    // Set the column header names.

                    dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "Unique ID";
                    dv.Columns[2].HeaderText = "Model 1";
                    dv.Columns[3].HeaderText = "Color 1";
                    dv.Columns[4].HeaderText = "Model 2";
                    dv.Columns[5].HeaderText = "Color 2";
                    dv.Columns[6].HeaderText = "Model 3";
                    dv.Columns[7].HeaderText = "Color 3";
                    dv.Columns[8].HeaderText = "Time IN";
                    dv.Columns[9].HeaderText = "Time OUT";
                    dv.Columns[10].HeaderText = "Checker";
                    dv.Columns[11].HeaderText = "Status";
                    dv.Columns[12].HeaderText = "Process Dest.";
                    dv.Columns[13].HeaderText = "NG";

                    // Populate the rows.
                    string[] row1 = new string[] { "", "", "", "", "", "", "", "", "", "", "", "","" };

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
                dv.Columns[0].ReadOnly = false;
                



            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Show Datagrid " + ex.ToString());
                savelog(ex.ToString());
            }

        }
        public void savelog(string description)
        {
            string tab = "\t";
            String header = "Datetime" + tab + "Description";
            String str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + tab + description;
            AppSettings.SaveLogs(str, header);
        }
    }
}
