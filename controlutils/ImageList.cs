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
            // 부모 컨트롤에 추가될 때 크기를 자동으로 채우도록 설정
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
    }
}
