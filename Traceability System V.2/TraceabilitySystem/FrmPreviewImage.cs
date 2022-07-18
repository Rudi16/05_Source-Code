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

namespace TraceabilitySystem
{
    public partial class FrmPreviewImage : Form
    {

        public FrmPreviewImage()
        {
            InitializeComponent();
        }

        private void FrmPreviewImage_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(Application.StartupPath + @"\\Asset\\image\\NGArea.png"))
            {

                picNGArea.Image = Image.FromFile(Application.StartupPath + @"\\Asset\\image\\NGArea.png");
                picNGArea.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
