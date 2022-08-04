using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

using System.IO;
using System.Threading;
using System.Diagnostics;

namespace TraceabilitySystem
{
    public partial class FrmMenu : Form
    {
        FormWindowState lastState;
        DataaccessMenu dataaccesmenu = new DataaccessMenu();
        public FrmMenu()
        {
            InitializeComponent();
            this.SizeChanged += (s, e) =>
            {

                //when window size changed we check if current state
                //is not the same with the previous
                if (WindowState != lastState)
                {

                    //i did use switch to show all 
                    //but you can use if to get only minimized status
                    switch (WindowState)
                    {
                        case FormWindowState.Normal:
                            this.WindowState = FormWindowState.Maximized;
                            break;
                        //case FormWindowState.Minimized:
                        //    this.WindowState = FormWindowState.Maximized;
                        //    break;
                        case FormWindowState.Maximized:
                           
                            break;
                        default:
                            break;
                    }
                    //and at the and of the event we store last window state in our
                    //variable so we get single message when state changed.
                    lastState = WindowState;
                }
            };
        }

        //private void icon_menu_Click(object sender, EventArgs e)
        //{
        //    if (panel_menu.Width == 186)
        //    {
        //        icon_sato_large.Visible = false;
        //        icon_sato_small.Visible = true;
        //        panel_menu.Width = 41;
        //        icon_menu.Location = new System.Drawing.Point(54, -2);
        //        panelMDI.Refresh();
        //    }
        //    else
        //    {
        //        icon_sato_large.Visible = true;
        //        icon_sato_small.Visible = false;
        //        panel_menu.Width = 186;
        //        icon_menu.Location = new System.Drawing.Point(184, -2);
        //        panelMDI.Refresh();
        //    }

        //}

        private void pict_logout_Click(object sender, EventArgs e)
        {
            Close();
        }

        //private void icon_menu_MouseHover(object sender, EventArgs e)
        //{
        //    icon_menu.BackColor = Color.Gainsboro;
        //}

        //private void icon_menu_MouseLeave(object sender, EventArgs e)
        //{
        //    icon_menu.BackColor = Color.White;
        //}

        private void pict_logout_MouseHover(object sender, EventArgs e)
        {
            pict_logout.BackColor = Color.Gainsboro;
        }

        private void pict_logout_MouseLeave(object sender, EventArgs e)
        {
            pict_logout.BackColor = Color.White;
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }
        private bool isCollapsed;
        private void FrmMenu_Load(object sender, EventArgs e)

        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            this.Text = " MAIN MENU [ PT. Yamaha Music Manufacturing Indonesia Traceability System V. " + version + " ]";

            timmerx.Enabled = true;
            timmerx.Start();
            //icon_menu.Visible = false;
            new DBConnections()
            .AppConnectionStringx();
           
            timer_dropdown.Start();
            timer_History.Start();
            timer_masterlabel.Start();
            btnMasterColor.Visible = false;
           
            user_id.Text = "User ID : " + DBConnections.UserID;
            txtuser_name.Text = "Name : " + dataaccesmenu.get_nameUser(DBConnections.UserID);

        }


        private bool isCollapsed2;
       
        private void button5_Click(object sender, EventArgs e)
        {
            if (isCollapsed2 == false)
            {
                timer_History.Start();
                paneldropHistory.Height -= 10;
                if (paneldropHistory.Size == paneldropHistory.MinimumSize)
                {
                    BtnHistory.BackgroundImage = Image.FromFile(Application.StartupPath + @"\\Asset\\image\\ico_setting.png");
                    timer_History.Stop();
                    isCollapsed2 = true;

                }
            }

            if (isCollapsedmasterlabel == false)
            {
                timer_masterlabel.Start();
                panel_masterlabel.Height -= 10;
                if (panel_masterlabel.Size == panel_masterlabel.MinimumSize)
                {
                    btnmasterlabel.BackgroundImage = Image.FromFile(Application.StartupPath + @"\\Asset\\image\\ico_setting.png");
                    timer_History.Stop();
                    isCollapsedmasterlabel = true;
                }
            }

            timer_dropdown.Start();

        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {

           
            if (isCollapsedmasterlabel == false)
            {
                timer_masterlabel.Start();
                panel_masterlabel.Height -= 10;
                if (panel_masterlabel.Size == panel_masterlabel.MinimumSize)
                {
                    btnmasterlabel.BackgroundImage = Image.FromFile(Application.StartupPath + @"\\Asset\\image\\ico_setting.png");
                    timer_History.Stop();
                    isCollapsedmasterlabel = true;
                }
            }
            timer_History.Start();
        }

        private void BtnHistoryScan_Click(object sender, EventArgs e)
        {
            FrmReportQA5 fs = new FrmReportQA5();
            checkingformrunning(fs.Name.ToString());
            if (checkform == false)
            {

                panelMDI.Visible = true;
                panelMDI.Dock = DockStyle.Fill;
                fs.TopLevel = false;
                panelMDI.Controls.Add(fs);
                fs.Dock = DockStyle.Fill;

                fs.Show();
            }
        }
        private void btnSettingConnection_Click(object sender, EventArgs e)
        {
           
        }
        private void btnmasterlabel_Click(object sender, EventArgs e)
        {
            if (isCollapsed2 == false)
            {
                timer_History.Start();
                paneldropHistory.Height -= 10;
                if (paneldropHistory.Size == paneldropHistory.MinimumSize)
                {
                    BtnHistory.BackgroundImage = Image.FromFile(Application.StartupPath + @"\\Asset\\image\\ico_setting.png");
                    timer_History.Stop();
                    isCollapsed2 = true;

                }
            }

            if (isCollapsed == false)
            {
                timer_dropdown.Start();
                //button5.Image = Resources.Collapse_Arrow_20px;
              
            }

            timer_masterlabel.Start();
        }

        private void timer_dropdown_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                //button5.Image = Resources.Collapse_Arrow_20px;
               
            }
            else
            {
                //button5.Image = Resources.Expand_Arrow_20px;
                
            }
        }

        private void timer_History_Tick(object sender, EventArgs e)
        {
            if (isCollapsed2)
            {
                //button5.Image = Resources.Collapse_Arrow_20px;
                paneldropHistory.Height += 10;
                if (paneldropHistory.Size == paneldropHistory.MaximumSize)
                {
                    BtnHistory.BackgroundImage = Image.FromFile(Application.StartupPath + @"\\Asset\\image\\ico_setting kebalik.png");
                    timer_History.Stop();
                    timer_History.Enabled = false;
                    timer_History.Dispose();
                    isCollapsed2 = false;
                }
            }
            else
            {

                paneldropHistory.Height -= 10;
                if (paneldropHistory.Size == paneldropHistory.MinimumSize)
                {
                    BtnHistory.BackgroundImage = Image.FromFile(Application.StartupPath + @"\\Asset\\image\\ico_setting.png");
                    timer_History.Stop();
                    timer_History.Enabled = false;
                    timer_History.Dispose();
                    isCollapsed2 = true;
                }
            }
        }
        private bool isCollapsedmasterlabel;
        private void timer_masterlabel_Tick(object sender, EventArgs e)
        {
            if (isCollapsedmasterlabel)
            {
                //button5.Image = Resources.Collapse_Arrow_20px;
                panel_masterlabel.Height += 10;
                if (panel_masterlabel.Size == panel_masterlabel.MaximumSize)
                {
                    btnmasterlabel.BackgroundImage = Image.FromFile(Application.StartupPath + @"\\Asset\\image\\ico_setting kebalik.png");
                    timer_masterlabel.Stop();
                    timer_masterlabel.Enabled = false;
                    timer_History.Dispose();
                    isCollapsedmasterlabel = false;
                }
            }
            else
            {

                panel_masterlabel.Height -= 10;
                if (panel_masterlabel.Size == panel_masterlabel.MinimumSize)
                {
                    btnmasterlabel.BackgroundImage = Image.FromFile(Application.StartupPath + @"\\Asset\\image\\ico_setting.png");
                    timer_masterlabel.Stop();
                    timer_masterlabel.Enabled = false;
                    timer_History.Dispose();
                    isCollapsedmasterlabel = true;
                }
            }
        }

        private void BtnMasterLineProd_Click(object sender, EventArgs e)
        {

        }

        private void BtnArea_Click(object sender, EventArgs e)
        {

          
        }
        private void btnmasteruser_Click(object sender, EventArgs e)
        {


        }
        
        private bool checkform = false;
        private void checkingformrunning(string nameform)
        {
            System.Collections.ArrayList list = new ArrayList(panelMDI.Controls);
            if (list.Count > 0)
            {
                foreach (Control c in list)
                {

                    if (nameform == c.Name)
                    {
                        checkform = true;
                    }
                    else if (nameform != c.Name)
                    {
                        panelMDI.Controls.Remove(c);
                        c.Dispose();
                        checkform = false;
                    }
                }
            }else if(list.Count == 0)
            {
                checkform = false;
            }
          

        }

        private void FrmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Are you sure want to Logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;

            }
        }
        private void BtnMasterSKUNo_Click(object sender, EventArgs e)
        {

            FrmMasterusers fs = new FrmMasterusers();
            checkingformrunning(fs.Name.ToString());

            if (checkform == false)
            {
                fs.TopLevel = false;
                panelMDI.Controls.Add(fs);
                fs.Dock = DockStyle.Fill;
                fs.Show();
            }
        }
        private void panelMDI_Paint(object sender, PaintEventArgs e)
        {

        }
        public string isdashboard { set; get; }
        private void buangfrm()
        {
            System.Collections.ArrayList list = new ArrayList(panelMDI.Controls);
            foreach (Control c in list)
            {
                panelMDI.Controls.Remove(c);
                c.Dispose();
            }
            // panelutama.Controls.Clear();
            //
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timmerx.Interval = 1;
            DateTime now = DateTime.Now;
            lbldatetime.Text = "" + now.ToString("dd/MM/yyyy HH:mm:ss") + "";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnmasterlabel_Click_1(object sender, EventArgs e)
        {
            timer_masterlabel.Start();
            if (isCollapsed2 == false)
            {
                timer_History.Start();
                paneldropHistory.Height -= 10;
                if (paneldropHistory.Size == paneldropHistory.MinimumSize)
                {
                    BtnHistory.BackgroundImage = Image.FromFile(Application.StartupPath + @"\\Asset\\image\\ico_setting.png");
                    timer_History.Stop();
                    timer_History.Enabled = false;
                    timer_History.Dispose();
                    isCollapsed2 = true;

                }
            }
           
        }

        private void btnmasteruser_Click_1(object sender, EventArgs e)
        {

            FrmMasterusers fs = new FrmMasterusers();
            checkingformrunning(fs.Name.ToString());

            if (checkform == false)
            {
                fs.TopLevel = false;
                panelMDI.Controls.Add(fs);
                fs.Dock = DockStyle.Fill;
                fs.Show();
            }
        }
        private void btnModel_Click(object sender, EventArgs e)
        {
            FrmMasterModel fs = new FrmMasterModel();
            checkingformrunning(fs.Name.ToString());

            if (checkform == false)
            {
                fs.TopLevel = false;
                panelMDI.Controls.Add(fs);
                fs.Dock = DockStyle.Fill;
                fs.Show();
            }
        }

        private void btnMasterColor_Click(object sender, EventArgs e)
        {
            FrmMasterColor fs = new FrmMasterColor();
            checkingformrunning(fs.Name.ToString());

            if (checkform == false)
            {
                fs.TopLevel = false;
                panelMDI.Controls.Add(fs);
                fs.Dock = DockStyle.Fill;
                fs.Show();
            }
        }
        private void btnMasterNG_Click(object sender, EventArgs e)
        {
            FrmMasterNG fs = new FrmMasterNG();
            checkingformrunning(fs.Name.ToString());

            if (checkform == false)
            {
                fs.TopLevel = false;
                panelMDI.Controls.Add(fs);
                fs.Dock = DockStyle.Fill;
                fs.Show();
            }
        }

        private void BtnReportQA6_Click(object sender, EventArgs e)
        {
            FrmReportQA6 fs = new FrmReportQA6();
            checkingformrunning(fs.Name.ToString());
            if (checkform == false)
            {

                panelMDI.Visible = true;
                panelMDI.Dock = DockStyle.Fill;
                fs.TopLevel = false;
                panelMDI.Controls.Add(fs);
                fs.Dock = DockStyle.Fill;

                fs.Show();
            }
        }

        private void btnReportNG_Click(object sender, EventArgs e)
        {
            FrmReportNG fs = new FrmReportNG();
            checkingformrunning(fs.Name.ToString());
            if (checkform == false)
            {

                panelMDI.Visible = true;
                panelMDI.Dock = DockStyle.Fill;
                fs.TopLevel = false;
                panelMDI.Controls.Add(fs);
                fs.Dock = DockStyle.Fill;

                fs.Show();
            }
        }
        //public void button1_Click(object sender, EventArgs e)
        //{
        //    FrmMasterPointCheck fs = new FrmMasterPointCheck();
        //    fs.ShowDialog();
        //    //checkingformrunning(fs.Name.ToString());

        //    //if (checkform == false)
        //    //{
        //    //    panelMDI.Visible = true;
        //    //    panelMDI.Dock = DockStyle.Fill;
        //    //    fs.TopLevel = false;
        //    //    panelMDI.Controls.Add(fs);
        //    //    fs.Dock = DockStyle.Fill;
        //    //}

        //}
    }
}
