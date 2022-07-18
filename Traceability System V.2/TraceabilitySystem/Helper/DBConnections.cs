using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
namespace TraceabilitySystem
{
	 public class DBConnections
	{
        public static string UserID;

        public static string Name { get; internal set; }

        public static string AppConnectionString;
        public static string AppConnectionStringCE;
        public static string TypeConnection;
        public string AppConnectionStringx()
		 {
            if (System.IO.File.Exists(Application.StartupPath + "\\Config\\Database.dat") == true)
            {
                string FILE_NAME = Application.StartupPath + "\\Config\\Database.dat";

                //string TextLine = "";
                if (System.IO.File.Exists(FILE_NAME) == true)
                {
                    System.IO.StreamReader objReader = new System.IO.StreamReader(FILE_NAME);
                    while (objReader.Peek() != -1)
                    {
                        AppConnectionString = AppConnectionString + objReader.ReadLine();
                        string[] lines = File.ReadAllLines(FILE_NAME);
                        foreach (string line in lines)
                        {
                            string[] col = line.Split('|');
                            if(col[0].ToString() == "Not Settings")
                            {
                                MessageBox.Show("Please Settings Server SQL Server First");
                                AppConnectionString = "Data Source = Nothings ; Initial Catalog =Db; User ID = sa;Password =123; pooling = false; CONNECTION TIMEOUT = 200; ";
                                objReader.Close();
                                new FrmUseSQLServer().ShowDialog();
                                objReader = new System.IO.StreamReader(FILE_NAME);
                            }
                            else
                            {
                                AppConnectionString = "Data Source=" + col[0].ToString()+ ";Initial Catalog=" + col[1].ToString() + ";User ID=" + col[2].ToString() + ";Password=" + col[3].ToString() + ";pooling=false;CONNECTION TIMEOUT=200;";
                                AppSettings.comPort = col[4].ToString();
                            }

                           
                        }
                    }
                    objReader.Dispose();
                }
                else
                {
                    MessageBox.Show("Can't Read Database.dat");
                    return "";

                }

               
                //MessageBox.Show(TextLine);
                return AppConnectionString;
            }
            else
            {
                MessageBox.Show("Database.dat Dosn't Exits");
                new FrmUseSQLServer().ShowDialog();
                return "";
            }
        }
       
        public string AppConnectionStringCEx()
        {
            try
            {
                AppConnectionStringCE = @"Data Source=" + Application.StartupPath + @"\Database\db_pointcheck.sdf;Persist Security Info=False";
                return AppConnectionStringCE;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }

        }

        //private string dec(string enc)
        //{
        //    string dat = null;
        //    if (!string.IsNullOrWhiteSpace(enc) == true)
        //    {
        //        try
        //        {
        //            dat = Encrypt.DecryptString(enc, "1605");

        //        }

        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString());
        //            return null;
        //        }

        //    }
        //    return dat;
        //}
    }
}

