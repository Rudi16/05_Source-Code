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
   public class Dataacces_Color
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
       
        public bool RegisterColor(XColor Color)
        {
            bool check = false;
            try
            {
                 using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                 {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                            const string strQuery = @"[dbo].[SATO_COLOR_INSERT]";
                            mycon.Query(strQuery, Color, commandType: System.Data.CommandType.StoredProcedure);
                            check = true;
                            mycon.Close();
                    }
                    
                 }
                return check;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Register Color : " + ex.ToString());
                savelog(ex.ToString());
                return check;
            }
        }

        public bool UpdateColor(XColor Color)
        {
            bool check = false;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[SATO_COLOR_UPDATE]";
                        mycon.Query(strQuery, Color, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;
                        mycon.Close();
                    }
                    return check;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Update Color : " + ex.ToString());
                savelog(ex.ToString());
                return check;
            }
        }
        public DataTable GetAll()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {


                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                            const string strQuery = @"[dbo].[SATO_COLOR_SELECT]";
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
                MessageBox.Show("Error Get Data Color : " + EX.ToString());
                savelog(EX.ToString());
                return dt;
            }
        }
        public void showgrid(DataGridView dv)
        {
            try
            {

            
            dv.DataSource = null;
            dv.Rows.Clear();
            dv.Refresh();
            dv.Columns.Clear();
            dv.Refresh();
            DataTable dt = new DataTable();
            dt = GetAll();
                if (dt.Rows.Count > 0)
                {
                    addcolumn(dv);
                    dv.DataSource = dt;
                    dv.Columns[0].HeaderText = " ";
                    dv.Columns[1].HeaderText = "No.";
                    dv.Columns[3].HeaderText = "Initial Color";
                    dv.Columns[4].HeaderText = "Color Name";
                    dv.Columns[5].HeaderText = "Last Update";
                    dv.Columns[2].Visible = false;
                }
                else
                {
                    dv.ColumnCount = 4;
                    dv.ColumnHeadersVisible = true;

                    // Set the column header names.
                    addcolumn(dv);
                   
                    dv.Columns[0].HeaderText = "No.";
                    dv.Columns[1].HeaderText = "Initial Color";
                    dv.Columns[2].HeaderText = "Color Name";
                    dv.Columns[3].HeaderText = "Last Update";
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

        public bool DeleteColor(XColor Color, string v)
        {
           
            bool check = false;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        if (v == "nul")
                        {
                            const string strQuery = @"[DBO].[SATO_Color_DELETE]";
                            mycon.Query(strQuery, Color, commandType: System.Data.CommandType.StoredProcedure);
                            check = true;
                            mycon.Close();
                        }
                        else
                        {
                            const string strQuery = @"[DBO].[SATO_Color_UPDATEREACTIVED]";
                            mycon.Query(strQuery, Color, commandType: System.Data.CommandType.StoredProcedure);
                            check = true;
                            mycon.Close();
                        }
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

        public string CheckDataColorNameExists(XColor Color)
        {
            string data = null;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = "[SATO_COLOR_CHECK]";
                        data = mycon.QueryFirstOrDefault<string>(strQuery, Color, commandType: System.Data.CommandType.StoredProcedure);
                        mycon.Close();
                    }
                }
                return data;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error Check Color Name : " + EX.ToString());
                savelog("Error Check Color Name : " + EX.ToString());
                return data;
            }
        }
        public void savelog(string description) {
            string tab = "\t";
            String header = "Datetime" + tab + "Description";
            String str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + tab + description;
            AppSettings.SaveLogs(str, header);
        }

    }
}
