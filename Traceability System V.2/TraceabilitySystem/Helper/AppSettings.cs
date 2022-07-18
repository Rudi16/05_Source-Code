using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TraceabilitySystem
{
   unsafe public class AppSettings
    {
        public static string foldershare;
        public static string Authority;
        public static string comPort;
        public static bool Approved;
        public static string ApprovedName;
        internal static class MyConst
        {
            internal const string ERROR = "<< ERROR >>";
            internal const string WARNING = "<< WARNING >>";
            internal const string INFO = "<< INFO >>";

            internal const string OK = "OK";
            internal const string CANCEL = "CANCEL";

            internal const string EXIT = "EXIT";

            internal const string START = "START";
            internal const string STOP = "STOP";

            internal const string TITLE = "UR21 READ DEMO APP";
        }
        public class TagArgs : EventArgs
        {
            public TagArgs()
            {
                Uii = null;
                Qty = 0;
            }

            public int qty;
            public int Qty
            {
                get { return qty; }
                set { qty = value; }
            }


            private string uii;
            public string Uii
            {
                get { return uii; }
                set { uii = value; }
            }

        }
     

        public static void SaveLogs(String str, String strHeader = "")
        {
            //FilePerjam
            string sFile = Application.StartupPath + @"\Logerr_" + DateTime.Now.ToString("yyMM") + ".logs";

            // This text is added only once to the file.
            if (!File.Exists(sFile))
            {
                // Create a file to write to.
                string createText = strHeader + Environment.NewLine + str + Environment.NewLine;
                File.WriteAllText(sFile, createText);

            }
            else
            {
                string appendText = str + Environment.NewLine;
                File.AppendAllText(sFile, appendText);
            }
        }

        public static string ComPort { get; internal set; }
      


        /// <summary>
        /// Baca Location
        /// </summary>

        public static void BacaMasterLocation()
        {
            string[][] words = null;
            if (System.IO.File.Exists(Application.StartupPath + "\\Config\\MasterLocation.dat") == true)
            {
                string FILE_NAME = Application.StartupPath + "\\Config\\MasterLocation.dat";

                if (System.IO.File.Exists(FILE_NAME) == true)
                {
                    try
                    {
                        // int current_index = -1;
                        string contents = "";
                        try
                        {
                            using (StreamReader file = new StreamReader(FILE_NAME))
                            {
                                contents = file.ReadToEnd();
                            }
                            string[] lines = contents.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            words = new string[lines.GetLength(0)][];
                            for (int i = 0; i < lines.GetLength(0); i++)
                            {
                                string[] split_words = lines[i].Split(new char[] { '|' });
                                words[i] = new string[split_words.GetLength(0)];
                                for (int j = 0; j < split_words.GetLength(0); j++)
                                {
                                    words[i][j] = split_words[j];
                                }
                            }
                            for (int i = 0; i < words.Length; i++)
                            {
                                
                                if (words[i][1].ToString() == "Active")
                                {
                                    Location = words[i][0].ToString();
                                }
                            }
                            //  MessageBox.Show("File read", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "File not found" + FILE_NAME);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.ToString());
                    }


                }
            }
            else
            {
                MessageBox.Show("Settings.config Dosn't Exits");

            }

        }
        public static string Location;



        #region Konek RFID

        public static string GetDataRFID;

        public static string SerialEPC = "123456789012123456789012";
        public static bool AssetID_RegDiv = true;

        #endregion
    }
}
