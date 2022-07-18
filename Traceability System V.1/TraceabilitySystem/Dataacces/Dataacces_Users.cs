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
   public class Dataacces_Users
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
        public string userlogin(string userid,string login)
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
                        if (login == "All")
                        {
                            parameters.Add("@statement", "Select 	UserId 	From  sato_userid WHERE UserId ='" + userid + "'");
                        }
                        else
                        {
                            parameters.Add("@statement", "Select 	UserId 	From  sato_userid WHERE UserId ='" + userid + "' AND authority='Leader'");
                        }
                        const string strQuery = @"[dbo].sato_spall";
                        data = mycon.QueryFirstOrDefault<string>(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        mycon.Close();
                    }
                        return data;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return data;
                }

            }

        public string NameLogin(string userid)
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
                        parameters.Add("@statement", "Select Name From sato_userid WHERE UserId ='" + userid + "'");
                        const string strQuery = @"[dbo].sato_spall";
                        data = mycon.QueryFirstOrDefault<string>(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        mycon.Close();
                    }
                    return data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return data;
            }

        }

        public string Passwordlogin(string userid,string login)
        {
            string data = null;

            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                            DataTable dt = new DataTable();
                            const string strQuery = @"[dbo].[sato_spall]";
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = mycon;
                            cmd.Parameters.Add("@statement", "Select top 1 password,authority From sato_userid WHERE UserId ='" + userid + "'");
                            cmd.CommandText = strQuery;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                data = dt.Rows[0]["password"].ToString();
                                if (data == null || data == "")
                                {
                                    data = null;
                                }
                                else
                                {
                                    if (login == "All")
                                    {
                                        AppSettings.Authority = dt.Rows[0]["authority"].ToString();
                                    }
                                }

                            }
                            mycon.Close();
                    }
                    return data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return data;
            }
        }
        
        public bool CheckUserExists(string userid)
        {
            string data = null;
            bool check = false;
            try
            {

                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@statement", "Select top 1 userid 	From    sato_userid WHERE UserId ='" + userid + "' ");
                        const string strQuery = @"[dbo].sato_spall";
                        data = mycon.QueryFirstOrDefault<string>(strQuery, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        if (data == "" || data == null) { 
                            check = true;
                        }else
                        {
                            check = false;
                        }
                    }
                    mycon.Close();
                    return check;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return check;
            }
        }
        public bool RegisterUser(Users users)
        {
            bool check = false;
            try
            {
                 using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                 {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[sato_sp_users_insert]";
                        mycon.Query(strQuery, users, commandType: System.Data.CommandType.StoredProcedure);
                        check =  true;
                        mycon.Close();
                    }
                    return check;
                 }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Register User : " + ex.ToString());
                return check;
            }
        }

        public bool UpdateUser(Users users)
        {
            bool check = false;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[sato_sp_users_update]";
                      
                        mycon.Query(strQuery, users, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;
                        mycon.Close();
                    }
                    return check;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Update User : " + ex.ToString());
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
                        using (var transaction = mycon.BeginTransaction())
                        {
                            string strQuery = @"[dbo].sato_spall";
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = mycon;
                            cmd.Transaction = transaction;
                            cmd.Parameters.Add("@statement", "Select row_number() OVER (ORDER BY userid) N,userid,password,name,authority,active From sato_UserID");
                            cmd.CommandText = strQuery;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            da.Fill(dt);
                            transaction.Commit();
                            mycon.Close();
                        }
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Get Data User" + ex.ToString());
                return null;
            }
        }
        public void showgrid(DataGridView dv)
        {
            dv.DataSource = null;
            dv.Rows.Clear();
            dv.Refresh();
            dv.Columns.Clear();
            dv.Refresh();
            //DataGridViewCheckBoxColumn dgvCmb = new DataGGridViewCheckBoxColumn();
            //dgvCmb.ValueType = typeof(bool);
            //dgvCmb.Name = "Chk";
            //dgvCmb.HeaderText = "CheckBox";
            //dv.Columns.Add(dgvCmb);
            addcolumn(dv);
            dv.DataSource = GetAll();
            dv.Columns[0].HeaderText = " ";
            dv.Columns[1].HeaderText = "No.";
            dv.Columns[2].HeaderText = "UserID";
            dv.Columns[4].HeaderText = "Name";
            dv.Columns[5].HeaderText = "Privilege";
            dv.Columns[6].HeaderText = "Active";
            dv.Columns[3].Visible = false;

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
            dv.Columns[1].Width = 100;
            dv.Columns[4].Width = 200;

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

        public bool deleteuser(Users users)
        {
           
            bool check = false;
            try
            {
                using (var mycon = new SqlConnection(DBConnections.AppConnectionString))
                {
                    OpenConnect(mycon);
                    if (openconnect == true)
                    {
                        const string strQuery = @"[dbo].[sato_sp_users_delete]";
                     
                        mycon.Query(strQuery, users, commandType: System.Data.CommandType.StoredProcedure);
                        check = true;
                        mycon.Close();
                    }
                    return check;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Delete User : " + ex.ToString());
                return check;
            }
        }
      

    }
}
