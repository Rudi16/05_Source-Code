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
using System.Threading;

namespace TraceabilitySystem
{
    public partial class FrmTrasactionListCreate : Form
    {
        public FrmTrasactionListCreate()
        {
            InitializeComponent();
        }
        Dataacces_CreateRFID dataacces_CreateRFID = new Dataacces_CreateRFID();

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        WaitWnd.WaitWndFun waitForm = new WaitWnd.WaitWndFun();
        private void FrmTrasactionListCreate_Load_1(object sender, EventArgs e)
        {
            panelgrid.Dock = DockStyle.Fill;
            dataacces_CreateRFID.showgrid(DatagridTransactionListCreate,"", date1.Value.ToString("yyyy-MM-dd"), date2.Value.ToString("yyyy-MM-dd"));
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataacces_CreateRFID.showgrid(DatagridTransactionListCreate,txtSerialNumber.Text , date1.Value.ToString("yyyy-MM-dd"), date2.Value.ToString("yyyy-MM-dd"));
        }
    }
}
