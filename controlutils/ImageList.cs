using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleDonkeyManager.controlutils
{
    public partial class ImageList : UserControl
    {
        public ImageList()
        {
            InitializeComponent();
            // 부모 컨트롤의 크기 변경에 반응하도록 설정
            this.Dock = DockStyle.Fill;
            this.AutoSize = false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void ImageList_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void ImageList_Load(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
