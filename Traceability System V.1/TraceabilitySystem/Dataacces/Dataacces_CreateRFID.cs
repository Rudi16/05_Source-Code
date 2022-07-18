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
   public class Dataacces_CreateRFID
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

        public bool CreateRFID(RFIDTag rFID)
        {
            bool check = false;
            try
            {
                 using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                 {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_RFIDTAG_INSERT]";
                        mycon.Query(strQuery, rFID, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;
                        mycon.Close();
                    }
                    
                 }
                return check;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error CreateRFID : " + ex.ToString());
                savelog(ex.ToString());
                return check;
            }
        }
        public bool UPDATERFID(RFIDTag rFID)
        {
            bool check = false;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                       
                        const string strQuery = @"[dbo].[SATO_RFIDTAG_UPDATE]";
                        mycon.Query(strQuery, rFID, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;
                        mycon.Close();
                        
                    }

                }
                return check;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error UPDATERFID : " + ex.ToString());
                savelog(ex.ToString());
                return check;
            }
        }
        public DataTable GetAll(string serialnumber, string date1, string date2)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {


                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_RFIDTAG_SELECT]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Parameters.Add("@SerialNumber", serialnumber);
                        cmd.Parameters.Add("@date1", date1 + " 00:01:01");
                        cmd.Parameters.Add("@date2", date2 + " 23:59:59");
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
                MessageBox.Show("Error Get Data RFID TAG : " + EX.ToString());
                savelog(EX.ToString());
                return dt;
            }
        }

        internal string CheckUniqueHistory(string uniqueID)
        {
            string data = null;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("UniqueID", uniqueID);
                        string strQuery = "[SATO_REGISTERTAG_CHECKUNIQUEIDHISTORY]";
                        data = mycon.QueryFirstOrDefault<string>(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        mycon.Close();
                    }
                }
                return data;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error CheckUniqueHistory : " + EX.ToString());
                savelog("Error CheckUniqueHistory : " + EX.ToString());
                return data;
            }
        }

        public void showgrid(DataGridView dv, string serialnumber, string date1, string date2)
         {
            try
            {
            dv.DataSource = null;
            dv.Rows.Clear();
            dv.Refresh();
            dv.Columns.Clear();
            dv.Refresh();
            DataTable dt = new DataTable();
            dt = GetAll(serialnumber,date1,date2);
                if (dt.Rows.Count > 0)
                {
                   
                    dv.DataSource = dt;
                   
                    dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "Serial Number";
                    dv.Columns[2].HeaderText = "EPC Number";
                    dv.Columns[3].HeaderText = "Create Date";
                    dv.Columns[4].HeaderText = "PIC";
                   
                }
                else
                {
                    dv.ColumnCount = 5;
                    dv.ColumnHeadersVisible = true;

                    // Set the column header names.
                 
                    dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "Serial Number";
                    dv.Columns[2].HeaderText = "EPC Number";
                    dv.Columns[3].HeaderText = "Create Date";
                    dv.Columns[4].HeaderText = "PIC";
                    dv.Columns[0].Width = 50;

                    // Populate the rows.
                    string[] row1 = new string[] {  "", "", "","",""};

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
                dv.Columns[0].Width = 50;
                }
              
            
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Show Datagrid " + ex.ToString());
                savelog(ex.ToString());
            }

        }

        public DataTable GetAllTransactionRegister(string model, string date1, string date2)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_REGISTERTAG_SELECT]";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Parameters.Add("@Id_Model", model);
                        cmd.Parameters.Add("@date1", date1);
                        cmd.Parameters.Add("@date2", date2);
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
                MessageBox.Show("Error Get Data RFID TAG : " + EX.ToString());
                savelog(EX.ToString());
                return dt;
            }
        }

        public void showgridTransactionRegister(DataGridView dv, string model, string date1, string date2)
        {
            try
            {
                dv.DataSource = null;
                dv.Rows.Clear();
                dv.Refresh();
                dv.Columns.Clear();
                dv.Refresh();
                DataTable dt = new DataTable();
                dt = GetAllTransactionRegister(model, date1, date2);
                if (dt.Rows.Count > 0)
                {
                    addcolumn(dv);
                    dv.DataSource = dt; 
                    dv.Columns[0].HeaderText = "";
                    dv.Columns[1].HeaderText = "No.";
                    dv.Columns[2].HeaderText = "UniqueID";
                    dv.Columns[3].HeaderText = "RFID Serial";
                    dv.Columns[4].HeaderText = "EPC Number";
                    dv.Columns[5].HeaderText = "Model";
                    dv.Columns[6].HeaderText = "Color";
                    dv.Columns[7].HeaderText = "Create Date";
                    dv.Columns[8].HeaderText = "PIC";

                }
                else
                {
                    dv.ColumnCount = 8;
                    dv.ColumnHeadersVisible = true;

                    // Set the column header names.

                    dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "UniqueID";
                    dv.Columns[2].HeaderText = "RFID Serial";
                    dv.Columns[3].HeaderText = "EPC Number";
                    dv.Columns[4].HeaderText = "Model";
                    dv.Columns[5].HeaderText = "Color";
                    dv.Columns[6].HeaderText = "Create Date";
                    dv.Columns[7].HeaderText = "PIC";
                    dv.Columns[0].Width = 50;

                    // Populate the rows.
                    string[] row1 = new string[] { "", "", "", "", "","","","" };

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
                dv.Font = new System.Drawing.Font("Arial", 11);
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
                dv.Columns[2].Visible = false;
                dv.Columns[0].ReadOnly = false;
                dv.Columns[0].Width = 50;
               


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

      
        public string GetSerialNumber()
        {
            string data = null;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                            string strQuery = "select dbo.sato_fun_get_id('SerialEPC')";
                            data = mycon.QueryFirstOrDefault<string>(strQuery, commandType: System.Data.CommandType.Text);
                            mycon.Close();
                    }
                }
                return data;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error GetSerialNumber : " + EX.ToString());
                savelog("Error Check GetSerialNumber : " + EX.ToString());
                return data;
            }
        }
        public string GetUnique()
        {
            string data = null;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        string strQuery = "SELECT DBO.sato_fun_get_id('SerialUnique')";
                        data = mycon.QueryFirstOrDefault<string>(strQuery, commandType: System.Data.CommandType.Text);
                        mycon.Close();
                    }
                }
                return data;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error GetUnique : " + EX.ToString());
                savelog("Error Check GetUnique : " + EX.ToString());
                return data;
            }
        }
        
        public string GetlastSerialNumber()
        {
            string data = null;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                      
                        string strQuery = "select dbo.sato_fun_get_id('LastSerial')";
                        data = mycon.QueryFirstOrDefault<string>(strQuery, commandType: System.Data.CommandType.Text);
                        mycon.Close();
                        
                    }
                }
                return data;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error Check GetlastSerialNumber : " + EX.ToString());
                savelog("Error Check GetlastSerialNumber : " + EX.ToString());
                return data;
            }
        }
        public string CheckSerialNumber(string serial)
        {
            string data = null;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                       
                        string strQuery = "[SATO_RFIDTAG_GETSERIAL]";
                        var parameters = new DynamicParameters();
                        parameters.Add("@serial", serial);
                        data = mycon.QueryFirstOrDefault<string>(strQuery ,parameters, commandType: System.Data.CommandType.StoredProcedure);
                        mycon.Close();
                    }
                }
                return data;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error CheckSerialNumber : " + EX.ToString());
                savelog("Error Check CheckSerialNumber : " + EX.ToString());
                return data;
            }
        }
        public void savelog(string description) {
            string tab = "\t";
            String header = "Datetime" + tab + "Description";
            String str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + tab + description;
            AppSettings.SaveLogs(str, header);
        }

        internal bool RegisterTag(RegisterTag registerTag)
        {
            bool check = false;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_REGISTERTAG_INSERT]";
                        mycon.Query(strQuery, registerTag, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;
                        mycon.Close();
                    }
                }
                return check;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error RegisterTag : " + ex.ToString());
                savelog("Error RegisterTag : "+ ex.ToString());
                return check;
            }
        }
        internal bool EditModelColor(RegisterTag registerTag)
        {
            bool check = false;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_REGISTERTAG_EDITMODELCOLOR]";
                        mycon.Query(strQuery, registerTag, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;
                        mycon.Close();
                    }
                }
                return check;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error EditModelColor : " + ex.ToString());
                savelog("Error EditModelColor : " + ex.ToString());
                return check;
            }
        }
        internal bool CheckStatusEPC(RegisterTag registerTag)
        {
            bool check = true;
            string data = null;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        string strQuery = "[SATO_REGISTERTAG_CHECK]";
                        data = mycon.QueryFirstOrDefault<string>(strQuery, registerTag, commandType: System.Data.CommandType.StoredProcedure);
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
                MessageBox.Show("Error CheckStatusEPC : " + EX.ToString());
                savelog("Error Check CheckStatusEPC : " + EX.ToString());
                return check;
            }
        }
    }
}
