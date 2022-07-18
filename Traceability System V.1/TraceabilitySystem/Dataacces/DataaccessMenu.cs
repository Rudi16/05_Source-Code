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
    public class DataaccessMenu
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
        public string get_nameUser(string usrid)
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
                        parameters.Add("@statement", "select name from Sato_UserID where userid='" + usrid + "'");
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

        public string getcount_customer()
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
                        parameters.Add("@statement", "SELECT COUNT(DISTINCT Customer_ID) FROM Sato_Customer");
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

        public string getcount_label()
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
                        parameters.Add("@statement", "SELECT count(DISTINCT MasterLabel_labelid)  from  Sato_MasterLabel GROUP BY MasterLabel_labelid;");
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


        public string getcount_twopointcheck()
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
                        parameters.Add("@statement", "SELECT count(DISTINCT TP_Id) from Sato_MasterTPC group by TP_Id");
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


    }
}
