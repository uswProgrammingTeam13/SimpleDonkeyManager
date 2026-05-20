using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleDonkeyManager
{
    public partial class DataLoadControl : UserControl
    {

            private controlutils.ImageList imageList = new controlutils.ImageList();

            public DataLoadControl()
            {
                InitializeComponent();

                imageList.Dock = DockStyle.Fill;
                imageList.Visible = true;
                imgListPan.Controls.Add(imageList);
            }

            private void DataLoadControl_Load(object sender, EventArgs e)
            {

            }
        

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void imgListPan_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
