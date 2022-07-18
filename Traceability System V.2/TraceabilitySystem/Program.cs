using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Management;
using System.Diagnostics;

namespace TraceabilitySystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (IsApplicationAlreadyRunning() == true)
            {
                MessageBox.Show("The application is already running");
            }
            else
            {
                DBConnections dbconnection = new DBConnections();
                dbconnection.AppConnectionStringx();
                AppSettings.BacaMasterLocation();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var start = new FrmLogin();
                start.FormClosed += WindowClosed;
                start.Show();
                Application.Run();
            }
        }

        static bool IsApplicationAlreadyRunning()
        {
            string proc = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(proc);
            if (processes.Length > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void WindowClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0) Application.Exit();
            else Application.OpenForms[0].FormClosed += WindowClosed;
        }
        
    }
}
