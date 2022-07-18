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

namespace TraceabilitySystem.Dataacces
{
   
   public class Dataacces_ReportNG
    {
        public static string NameNG;
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
       
        public bool SaveNGCategory(ngCategory ngCategory)
        {
            bool check = false;
            try
            {
                 using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                 {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                      
                        const string strQuery = @"[dbo].[SATO_NGCATEGORY_INSERT]";
                        mycon.Query(strQuery, ngCategory, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;
                      
                        mycon.Close();
                       
                    }
                 }
                return check;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Register NG Category : " + ex.ToString());
                savelog(ex.ToString());
                return check;
            }
        }

        public bool UpdateNGCategory(ngCategory ngCategory)
        {
            bool check = false;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                     
                            const string strQuery = @"[dbo].[SATO_NGCATEGORY_UPDATE]";
                            mycon.Query(strQuery, ngCategory, commandType: System.Data.CommandType.StoredProcedure);
                            check = true;
                            mycon.Close();
                    
                    }
                    return check;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Update NG Category : " + ex.ToString());
                savelog(ex.ToString());
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
        public DataTable GetDataReportNG(string dat1, string dat2, string location, string ngtype)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_REPORTNG_SELECT]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = mycon;
                        cmd.Parameters.Add("@date1", dat1);
                        cmd.Parameters.Add("@date2", dat2);
                        cmd.Parameters.Add("@Location", location);
                        cmd.Parameters.Add("@NGType", ngtype);
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
                MessageBox.Show("Error GetDataReportNG : " + EX.ToString());
                savelog("Error GetDataReportNG : " + EX.ToString());
                return dt;
            }
        }

        internal void showgridReportNG(DataGridView dv, string dat1, string dat2, string location, string tengtypext)
        {
            try
            {
                dv.DataSource = null;
                dv.Rows.Clear();
                dv.Refresh();
                dv.Columns.Clear();
                dv.Refresh();
                DataTable dt = new DataTable();
                dt = GetDataReportNG(dat1, dat2, location, tengtypext);
                if (dt.Rows.Count > 0)
                {
                    dv.DataSource = AutoNumberedTable(dt);
                    dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "NG Name";
                    dv.Columns[2].HeaderText = "Total NG";
                    dv.Columns[3].HeaderText = "Product Qty";
                    

                }
                else
                {
                    dv.ColumnCount = 4;
                    dv.ColumnHeadersVisible = true;

                    // Set the column header names.
                    dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "NG Name";
                    dv.Columns[2].HeaderText = "Total NG";
                    dv.Columns[3].HeaderText = "Product Qty";
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
                    if (item.Index == 0 || item.Index == 2 || item.Index == 3)
                    {
                        item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                }
                dv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dv.AllowUserToAddRows = false;
                dv.AllowUserToDeleteRows = false;
                dv.AllowUserToOrderColumns = false;
                dv.BorderStyle = BorderStyle.FixedSingle;
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
                dv.Columns[0].Width = 40;
               



            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Show Datagrid " + ex.ToString());
                savelog(ex.ToString());
            }
        }
        public DataTable GetDataReportNGDetail(string dat1, string dat2, string location, string ngtype)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_REPORTNG_SELECTDETAIL]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = mycon;
                        cmd.Parameters.Add("@date1", dat1);
                        cmd.Parameters.Add("@date2", dat2);
                        cmd.Parameters.Add("@Location", location);
                        cmd.Parameters.Add("@NGType", ngtype);
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
                MessageBox.Show("Error GetDataReportNG : " + EX.ToString());
                savelog("Error GetDataReportNG : " + EX.ToString());
                return dt;
            }
        }
        internal void showgridReportNGDetail(DataGridView dv, string dat1, string dat2, string location, string tengtypext)
        {
            try
            {
                dv.DataSource = null;
                dv.Rows.Clear();
                dv.Refresh();
                dv.Columns.Clear();
                dv.Refresh();
                DataTable dt = new DataTable();
                dt = GetDataReportNGDetail(dat1, dat2, location, tengtypext);
                if (dt.Rows.Count > 0)
                {
                    dv.DataSource = AutoNumberedTable(dt);
                    dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "HistoryID";
                    dv.Columns[2].HeaderText = "Unique ID";
                    dv.Columns[3].HeaderText = "Time Stamp(IN)";
                    dv.Columns[4].HeaderText = "NG Area";
                    dv.Columns[5].HeaderText = "NG";



                }
                else
                {
                    dv.ColumnCount = 5;
                    dv.ColumnHeadersVisible = true;

                    // Set the column header names.
                     dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "Unique ID";
                    dv.Columns[2].HeaderText = "Time Stamp(IN)";
                    dv.Columns[3].HeaderText = "NG Area";
                    dv.Columns[4].HeaderText = "NG";
                    // Populate the rows.
                    string[] row1 = new string[] { "", "", "", "" ,""};

                    object[] rows = new object[] { row1 };

                    foreach (string[] rowArray in rows)
                    {
                        dv.Rows.Add(rowArray);
                    }
                }
                foreach (DataGridViewColumn item in dv.Columns)
                {
                    if (item.Index == 0 || item.Index == 2 || item.Index == 3)
                    {
                        item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                }
                dv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dv.AllowUserToAddRows = false;
                dv.AllowUserToDeleteRows = false;
                dv.AllowUserToOrderColumns = false;
                dv.BorderStyle = BorderStyle.FixedSingle;
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
                dv.Columns[0].Width = 40;
                dv.Columns[1].Visible = false;



            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Show Datagrid " + ex.ToString());
                savelog(ex.ToString());
            }
        }
        internal DataTable GetNGType(int IDNGCategory)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {


                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_NG_SELECTTYPE]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = mycon;
                        cmd.CommandText = strQuery;
                        cmd.Parameters.Add("@IDNGCategory", IDNGCategory);
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
                MessageBox.Show("Error Get Data NG CATEGORY : " + EX.ToString());
                savelog(EX.ToString());
                return dt;
            }
        }

        public DataTable GetAllNGCategory()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {


                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                       const string strQuery = @"[dbo].[SATO_NGCATEGORY_SELECT]";
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
                MessageBox.Show("Error Get Data NG CATEGORY : " + EX.ToString());
                savelog(EX.ToString());
                return dt;
            }
        }
        public DataTable GetNGCategory(string Location)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {


                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_NG_SELECTCATEGORY]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = mycon;
                        cmd.CommandText = strQuery;
                        cmd.Parameters.Add("@Location", Location);
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
                MessageBox.Show("Error Get Data NG CATEGORY : " + EX.ToString());
                savelog(EX.ToString());
                return dt;
            }
        }
        public void showgridNGCategory(DataGridView dv)
        {
            try
            {

            
            dv.DataSource = null;
            dv.Rows.Clear();
            dv.Refresh();
            dv.Columns.Clear();
            dv.Refresh();
            DataTable dt = new DataTable();
            dt = GetAllNGCategory();
                if (dt.Rows.Count > 0)
                {
                    addcolumn(dv);
                    dv.DataSource = dt;
                    dv.Columns[0].HeaderText = " ";
                    dv.Columns[1].HeaderText = "No.";
                    dv.Columns[3].HeaderText = "NG Category";
                    dv.Columns[4].HeaderText = "Location";
                    dv.Columns[5].HeaderText = "Description";
                    dv.Columns[2].Visible = false;
                }
                else
                {
                    dv.ColumnCount = 4;
                    dv.ColumnHeadersVisible = true;

                    // Set the column header names.
                    addcolumn(dv);
                   
                    dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "NG Category";
                    dv.Columns[2].HeaderText = "Location";
                    dv.Columns[3].HeaderText = "Description";
                    dv.Columns[1].Width = 50;

                    // Populate the rows.
                    string[] row1 = new string[] {  "", "", "",""};

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
                    dv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dv.AllowUserToAddRows = false;
                dv.AllowUserToDeleteRows = false;
                dv.AllowUserToOrderColumns = false;
                dv.BorderStyle = BorderStyle.None;
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
                dv.Columns[0].ReadOnly = false;
                if (dt.Rows.Count > 0)
                {
                    dv.Columns[0].Width = 30;
                    dv.Columns[1].Width = 50;
                    dv.Columns[2].Width = 200;
                }
              
            
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Show Datagrid " + ex.ToString());
                savelog(ex.ToString());
            }

        }
        public DataTable GetAllNGList(int idNGCategory)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_NGLIST_SELECT]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = mycon;
                        cmd.Parameters.Add("@idNGCategory", idNGCategory);
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
                MessageBox.Show("Error Get Data NG CATEGORY : " + EX.ToString());
                savelog(EX.ToString());
                return dt;
            }
        }
        public void showgridNGList(DataGridView dv, int idNGCategory)
        {
            try
            {
                dv.DataSource = null;
                dv.Rows.Clear();
                dv.Refresh();
                dv.Columns.Clear();
                dv.Refresh();
                DataTable dt = new DataTable();
                dt = GetAllNGList(idNGCategory);
                if (dt.Rows.Count > 0)
                {
                    addcolumn(dv);
                    dv.DataSource = dt;
                    dv.Columns[0].HeaderText = " ";
                    dv.Columns[1].HeaderText = "No.";
                    dv.Columns[2].HeaderText = "";
                    dv.Columns[3].HeaderText = "NG Name";
                    dv.Columns[4].HeaderText = "Description";
                }
                else
                {
                    dv.ColumnCount = 3;
                    dv.ColumnHeadersVisible = true;

                    // Set the column header names.
                    addcolumn(dv);

                    dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "NG Category";
                    dv.Columns[2].HeaderText = "Description";

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
                    if (item.Index == 1 || item.Index == 2)
                    {
                        item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                }
                dv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dv.AllowUserToAddRows = false;
                dv.AllowUserToDeleteRows = false;
                dv.AllowUserToOrderColumns = false;
                dv.BorderStyle = BorderStyle.None;
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
                dv.Columns[0].ReadOnly = false;
                if (dt.Rows.Count > 0)
                {
                    dv.Columns[2].Visible = false;
                    dv.Columns[0].Width = 30;
                    dv.Columns[1].Width = 50;
                    dv.Columns[2].Width = 200;
                }


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

        public bool DeleteNGCategory(ngCategory ngCategory, string v)
        {
           
            bool check = false;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                      
                        const string strQuery = @"[DBO].[SATO_NGCATEGORY_DELETE]";
                        mycon.Query(strQuery, ngCategory, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;
                        mycon.Close();
                   
                    }
                    return check;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Delete Color : " + ex.ToString());
                savelog(ex.ToString());
                return check;
            }
        }

        public string CheckDataColorNameExists(ngCategory ngCategory)
        {
            string data = null;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        
                        const string strQuery = "[SATO_NGCATEGORY_CHECK]";
                        data = mycon.QueryFirstOrDefault<string>(strQuery, ngCategory, commandType: System.Data.CommandType.StoredProcedure);
                        mycon.Close();
                     
                    }
                }
                return data;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error Check NG CATEGORY Name : " + EX.ToString());
                savelog("Error Check Color Name : " + EX.ToString());
                return data;
            }
        }

        internal bool checkNameNGUpdate(string NGName, string location, string idng)
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
                        parameters.Add("@NGName", NGName);
                        parameters.Add("@Location", location);
                        parameters.Add("@IdNG", idng);

                        string strQuery = "[SATO_NGLIST_CHECKUPDATE]";
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
                MessageBox.Show("Error checkNameNGUpdate : " + EX.ToString());
                savelog("Error checkNameNGUpdate : " + EX.ToString());
                return check;
            }
        }

        internal bool UpdateNGList(int idNGCategory, string NGName, string Description, string idNG)
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
                        parameters.Add("@idNGCategory", idNGCategory);
                        parameters.Add("@NGName", NGName);
                        parameters.Add("@Description", Description);
                        parameters.Add("@IdNG", idNG);
                        const string strQuery = @"[dbo].[SATO_NGLIST_UPDATE]";
                        mycon.Query(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;

                        mycon.Close();

                    }
                }
                return check;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error UpdateNGList : " + ex.ToString());
                savelog("Error UpdateNGList : " + ex.ToString());
                return check;
            }
        }

        public void savelog(string description) {
            string tab = "\t";
            String header = "Datetime" + tab + "Description";
            String str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + tab + description;
            AppSettings.SaveLogs(str, header);
        }

        internal bool SaveNGList(int idNGCategory, string NGName, string Description)
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
                        parameters.Add("@idNGCategory", idNGCategory);
                        parameters.Add("@NGName", NGName);
                        parameters.Add("@Description", Description);
                        const string strQuery = @"[dbo].[SATO_NGLIST_INSERT]";
                        mycon.Query(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;

                        mycon.Close();

                    }
                }
                return check;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error SaveNGList : " + ex.ToString());
                savelog("Error SaveNGList : " + ex.ToString());
                return check;
            }
        }

        internal bool DeleteNGList(string idNG)
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
                        parameters.Add("@IdNG", idNG);
                        const string strQuery = @"[dbo].[SATO_NGLIST_DELETE]";
                        mycon.Query(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;

                        mycon.Close();

                    }
                }
                return check;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error SaveNGList : " + ex.ToString());
                savelog("Error SaveNGList : " + ex.ToString());
                return check;
            }
        }

        internal bool checkNameNG(string ngName,string Location)
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
                        parameters.Add("@NGName", ngName);
                        parameters.Add("@Location", Location);
                        string strQuery = "[SATO_NGLIST_CHECK]";
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
                MessageBox.Show("Error checkNameNG : " + EX.ToString());
                savelog("Error checkNameNG : " + EX.ToString());
                return check;
            }
        }
    }
}
