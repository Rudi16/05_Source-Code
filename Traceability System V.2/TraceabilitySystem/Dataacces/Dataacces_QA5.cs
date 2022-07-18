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
   public class Dataacces_QA5
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

        public DataTable GetDataProduct(string epc)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_QA5_GETDATAPRODUCT]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = mycon;
                        cmd.Parameters.Add("@EPC", epc);
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
                MessageBox.Show("Error Get Data NG GetDataProduct : " + EX.ToString());
                savelog("Error Get Data NG GetDataProduct: " + EX.ToString());
                return dt;
            }
        }

        internal bool CheckUniqueStatusOK(string UniqueID)
        {
            bool check = false;
            string data = null;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {

                        var parameters = new DynamicParameters();
                        parameters.Add("@UniqueID", UniqueID);
                        string strQuery = "[SATO_QA5_CHECKUNIQUEID]";
                        data = mycon.QueryFirstOrDefault<string>(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        mycon.Close();
                    }
                }
                if (data == "0" || data == null)
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
                return check;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error CheckUniqueStatusOK : " + EX.ToString());
                savelog("Error CheckUniqueStatusOK : " + EX.ToString());
                return check;
            }
        }

        internal bool CheckTimeStart(string UniqueID)
        {
            bool check = false;
            string data = null;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {

                        var parameters = new DynamicParameters();
                        parameters.Add("@UniqueID", UniqueID);
                        string strQuery = "[SATO_QA5_CHECKTIMESTART]";
                        data = mycon.QueryFirstOrDefault<string>(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        mycon.Close();
                    }
                }
                if (data == "" || data == null)
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
                return check;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error CheckTimeStart : " + EX.ToString());
                savelog("Error CheckTimeStart : " + EX.ToString());
                return check;
            }
        }
        internal string GetTimeStart(string UniqueID)
        {
            string data = null;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {

                        var parameters = new DynamicParameters();
                        parameters.Add("@UniqueID", UniqueID);
                        string strQuery = "[SATO_QA5_TIMESTART]";
                        data = mycon.QueryFirstOrDefault<string>(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        mycon.Close();
                    }
                }
                
                return data;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error GetTimeStart : " + EX.ToString());
                savelog("Error GetTimeStart : " + EX.ToString());
                return data;
            }
        }
        internal bool ApprovedNG(string v)
        {

            bool check = false;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@History_ID", v);
                        const string strQuery = @"[dbo].[SATO_QA5_APPROVEDNG]";
                        mycon.Query(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
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
        internal bool CheckUniqueStatusNG(string UniqueID)
        {
            bool check = false;
            string data = null;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@UniqueID", UniqueID);
                        string strQuery = "[SATO_QA5_CHECKUNIQUEIDNG]";
                        data = mycon.QueryFirstOrDefault<string>(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        mycon.Close();
                    }
                }
                if (data == "0" || data == null)
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
                return check;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error CheckUniqueStatusNG : " + EX.ToString());
                savelog("Error CheckUniqueStatusNG : " + EX.ToString());
                return check;
            }
        }

        internal DataTable GetDestination(string Location)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_QA5_GETPROCESSDEST]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = mycon;
                        cmd.Parameters.Add("@Location", Location);
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
                MessageBox.Show("Error Get GetDestination : " + EX.ToString());
                savelog("Error GetDestination : " + EX.ToString());
                return dt;
            }
        }
        internal DataTable GetListNG(string UniqueID,string Process)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_QA5_SELECTLISTNG]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = mycon;
                        cmd.Parameters.Add("@UniqueID", UniqueID);
                        cmd.Parameters.Add("@Process", Process);
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
                MessageBox.Show("Error GetListNG : " + EX.ToString());
                savelog("Error GetListNG : " + EX.ToString());
                return dt;
            }
        }

        internal bool DeleteNGList(string UniqueID,int idNGList,string Process)
        {
            bool check = false;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@UniqueID", UniqueID);
                        parameters.Add("@idNGList", idNGList);
                        parameters.Add("@Process", Process);
                        const string strQuery = @"[dbo].[SATO_QA5_DELETELISTNG]";
                        mycon.Query(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;
                        mycon.Close();
                    }

                }
                return check;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error InsertoListNG : " + ex.ToString());
                savelog("Error InsertoListNG : " + ex.ToString());
                return check;
            }
        }

        public bool InsertoListNG(ListNG listng)
        {
            bool check = false;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_QA5_INSERTLISTNG]";
                        mycon.Query(strQuery, listng, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;
                        mycon.Close();
                    }
                }
                return check;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error InsertoListNG : " + ex.ToString());
                savelog("Error InsertoListNG : " + ex.ToString());
                return check;
            }
        }
        public void showgrid(DataGridView dv, string UniqueID,string Process)
        {
            try
            {
                dv.DataSource = null;
                dv.Rows.Clear();
                dv.Refresh();
                dv.Columns.Clear();
                dv.Refresh();
                DataTable dt = new DataTable();
                dt = GetListNG(UniqueID, Process);
                if (dt.Rows.Count > 0)
                {

                    dv.DataSource = dt;
                    dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "ID";
                    dv.Columns[2].HeaderText = "NG Type";
                    dv.Columns[3].HeaderText = "Process Dest";

                }
                else
                {
                    dv.ColumnCount = 3;
                    dv.ColumnHeadersVisible = true;

                    // Set the column header names.

                    dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "NG Type";
                    dv.Columns[2].HeaderText = "Process Dest";
                    dv.Columns[0].Width = 30;

                    // Populate the rows.
                    string[] row1 = new string[] { "", "", "" };

                    object[] rows = new object[] { row1 };

                    foreach (string[] rowArray in rows)
                    {
                        dv.Rows.Add(rowArray);
                    }
                }
                foreach (DataGridViewColumn item in dv.Columns)
                {
                    if (item.Index == 0 )
                    {
                        item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                }
                dv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dv.AllowUserToAddRows = false;
                dv.AllowUserToDeleteRows = false;
                dv.AllowUserToOrderColumns = false;
                dv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dv.Font = new System.Drawing.Font("Arial", 10);
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
                if (dt.Rows.Count > 0)
                {
                    dv.Columns[0].Width = 50;
                    dv.Columns[1].Visible = false;
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Show Datagrid " + ex.ToString());
                savelog(ex.ToString());
            }

        }

        internal bool CheckColor(string color)
        {
            bool check = false;
            string data = null;
            try
            {
                string initialcolor = "";
                string namecolor = "";
                string[] lines = color.Split('-');
                if (lines.Length == 2)
                {
                    initialcolor = lines[0].ToString();
                    namecolor = lines[1].ToString();
                }
                else
                {
                    return false;
                }
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {

                        var parameters = new DynamicParameters();
                        parameters.Add("@initialcolor", initialcolor);
                        parameters.Add("@namecolor", namecolor);
                        string strQuery = "[SATO_QA5_CHECKCOLOR]";
                        data = mycon.QueryFirstOrDefault<string>(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        mycon.Close();
                    }
                }
                if (data == "0" || data == null)
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
                return check;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error CheckColor : " + EX.ToString());
                savelog("Error CheckColor : " + EX.ToString());
                return check;
            }
        }

        internal bool CheckModel(string model)
        {
            bool check = false;
            string data = null;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {

                        var parameters = new DynamicParameters();
                        parameters.Add("@Model", model);
                        string strQuery = "[SATO_QA5_CHECKMODEL]";
                        data = mycon.QueryFirstOrDefault<string>(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        mycon.Close();
                    }
                }
                if (data == "0" || data == null)
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
                return check;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error CheckModel : " + EX.ToString());
                savelog("Error CheckModel : " + EX.ToString());
                return check;
            }
        }

        internal bool UpdateNG(int IDNGList,string NGType,string Destination)
        {
            bool check = false;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@IDNGList", IDNGList);
                        parameters.Add("@NGType", NGType);
                        parameters.Add("@Destination", Destination);
                        const string strQuery = @"[dbo].[SATO_QA5_UPDATENG]";
                        mycon.Query(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;
                        mycon.Close();
                    }
                }
                return check;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error UpdateNG : " + ex.ToString());
                savelog("Error UpdateNG : " + ex.ToString());
                return check;
            }
        }

        internal bool UpdateModel(string UniqueID, int model , string ModelName,string ColorName)
        {
            bool check = false;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@UniqueID", UniqueID);
                        parameters.Add("@model", model);
                        parameters.Add("@ModelName", ModelName);
                        parameters.Add("ColorName", ColorName);
                        const string strQuery = @"[dbo].[SATO_QA5_UPDATEMODELCOLOR]";
                        mycon.Query(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;
                        mycon.Close();
                    }
                }
                return check;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error UpdateModel : " + ex.ToString());
                savelog("Error UpdateModel : " + ex.ToString());
                return check;
            }
        }

        internal bool InserttoHistory(History history)
        {
            bool check = false;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_QA5_HISTORY]";
                        mycon.Query(strQuery, history, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;
                        mycon.Close();
                    }
                }
                return check;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error InserttoHistory : " + ex.ToString());
                savelog("Error InserttoHistory : " + ex.ToString());
                return check;
            }
        }

        public void savelog(string description)
        {
            string tab = "\t";
            String header = "Datetime" + tab + "Description";
            String str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + tab + description;
            AppSettings.SaveLogs(str, header);
        }
        // For Transaction List
        public DataTable GetAllTransactionQA5(string date1, string date2,string filter)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_QA5_SELECTTRANSACTION]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Parameters.Add("@date1", date1 + " 00:00:01");
                        cmd.Parameters.Add("@date2", date2 + " 23:59:59");
                        cmd.Parameters.Add("@filter", filter);
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
                MessageBox.Show("Error Transaction List QA5 : " + EX.ToString());
                savelog("Error Transaction List QA5 : " + EX.ToString());
                return dt;
            }
        }

        public void showgridTransactionQA5(DataGridView dv, string date1, string date2, string filter)
        {
            try
            {
                dv.DataSource = null;
                dv.Rows.Clear();
                dv.Refresh();
                dv.Columns.Clear();
                dv.Refresh();
                DataTable dt = new DataTable();
                dt = GetAllTransactionQA5(date1, date2, filter);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        dt.Rows[i]["History_Status"] = dt.Rows[i]["History_Status"].ToString().Replace("DS", "Dispose");
                    }
                    addcolumn(dv);
                    dv.DataSource = dt;
                    dv.Columns[0].HeaderText = "";
                    dv.Columns[1].HeaderText = "No.";
                    dv.Columns[2].HeaderText = "History_ID";
                    dv.Columns[3].HeaderText = "UniqueID";
                    dv.Columns[4].HeaderText = "RFID";
                    dv.Columns[5].HeaderText = "PIC";
                    dv.Columns[6].HeaderText = "Record Time";
                    dv.Columns[7].HeaderText = "Status";
                    dv.Columns[8].HeaderText = "Model 1";
                    dv.Columns[9].HeaderText = "Color 1";
                    dv.Columns[10].HeaderText = "Model 2";
                    dv.Columns[11].HeaderText = "Color 2";
                    dv.Columns[12].HeaderText = "Model 3";
                    dv.Columns[13].HeaderText = "Color 3";
                    dv.Columns[14].HeaderText = "Approved Model 2";
                    dv.Columns[15].HeaderText = "Approved Model 3";
                    dv.Columns[16].HeaderText = "Approved Dispose";

                }
                else
                {
                    dv.ColumnCount = 6;
                    dv.ColumnHeadersVisible = true;

                    // Set the column header names.

                    dv.Columns[0].HeaderText = "No.";
                    
                    dv.Columns[1].HeaderText = "UniqueID";
                    dv.Columns[2].HeaderText = "RFID";
                    dv.Columns[3].HeaderText = "PIC";
                    dv.Columns[4].HeaderText = "Record Time";
                    dv.Columns[5].HeaderText = "Status";
                    
                    dv.Columns[0].Width = 50;

                    // Populate the rows.
                    string[] row1 = new string[] { "", "", "", "", "", ""};

                    object[] rows = new object[] { row1 };

                    foreach (string[] rowArray in rows)
                    {
                        dv.Rows.Add(rowArray);
                    }
                }
                foreach (DataGridViewColumn item in dv.Columns)
                {
                    if (item.Index == 1 || item.Index == 2)
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
                dv.Columns[2].Visible = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Show Datagrid " + ex.ToString());
                savelog(ex.ToString());
            }

        }

      

        public void addcolumn(DataGridView dv)
        {
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "Select";
            checkColumn.HeaderText = " ";
            checkColumn.Width = 50;
            checkColumn.ReadOnly = false;

            checkColumn.TrueValue = "true";

            checkColumn.FalseValue = "false";
            //checkColumn.DataPropertyName = "chk";
            checkColumn.FillWeight = 10; //if the datagridview is resized (on form resize) the checkbox won't take up too much; value is relative to the other columns' fill values
            dv.Columns.Add(checkColumn);
            dv.Columns["Select"].DisplayIndex = 0;
        }

        internal DataTable GetListNGTransaction(string HistoryID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_QA5_TRANSACTIONLISTNG]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = mycon;
                        cmd.Parameters.Add("@HistoryID", HistoryID);
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
                MessageBox.Show("Error GetListNGTransaction : " + EX.ToString());
                savelog("Error GetListNGTransaction : " + EX.ToString());
                return dt;
            }
        }

        public void showgridLISTNGTransaction(DataGridView dv, string HistoryID)
        {
            try
            {
                dv.DataSource = null;
                dv.Rows.Clear();
                dv.Refresh();
                dv.Columns.Clear();
                dv.Refresh();
                DataTable dt = new DataTable();
                dt = GetListNGTransaction(HistoryID);
                if (dt.Rows.Count > 0)
                {
                    dv.DataSource = dt;
                    dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "ID";
                    dv.Columns[2].HeaderText = "NG Type";
                    dv.Columns[3].HeaderText = "Process Dest";
                }
                else
                {
                    dv.ColumnCount = 4;
                    dv.ColumnHeadersVisible = true;

                    // Set the column header names.

                    dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "ID";
                    dv.Columns[2].HeaderText = "NG Type";
                    dv.Columns[3].HeaderText = "Process Dest";
                    dv.Columns[0].Width = 50;

                    // Populate the rows.
                    string[] row1 = new string[] { "", "", "", "" };

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
                dv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dv.AllowUserToAddRows = false;
                dv.AllowUserToDeleteRows = false;
                dv.AllowUserToOrderColumns = false;
                dv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dv.Font = new System.Drawing.Font("Arial", 10);
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
                if (dt.Rows.Count > 0)
                {
                    dv.Columns[0].Width = 30;
                    dv.Columns[1].Visible = false;
                    dv.Columns[3].Width = 120;
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Show Datagrid " + ex.ToString());
                savelog(ex.ToString());
            }

        }

    }
}
