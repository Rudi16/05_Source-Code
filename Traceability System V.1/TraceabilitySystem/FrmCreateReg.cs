using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TraceabilitySystem.Dataacces;
using TraceabilitySystem.Entity;
using System.Threading;
using static TraceabilitySystem.NativeMethods;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.Runtime.CompilerServices;

namespace TraceabilitySystem
{
    public partial class FrmCreateReg : Form
    {
      
        public FrmCreateReg()
        {
            InitializeComponent();
        }
    
        private void btnCreateLabelRFID_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmCreateRFID frm = new FrmCreateRFID();
            frm.ShowDialog();
            this.Show();

        }



        public void savelog(string description)
        {
            string tab = "\t";
            String header = "Datetime" + tab + "Description";
            String str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + tab + description;
            AppSettings.SaveLogs(str, header);
        }

        private void pict_logout_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnRegisterProduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmRegisterRFID frm = new FrmRegisterRFID();
            frm.ShowDialog();
            this.Show();
        }
    }
}
