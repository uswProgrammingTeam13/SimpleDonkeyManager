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

            // listBoxImages 이벤트 연결을 코드로 추가
            listBoxImages.SelectedIndexChanged += listBoxImages_SelectedIndexChanged;
        }

        // 이미지 선택 이벤트 정의
        public event EventHandler<string> ImageSelected;

        public void LoadImages(string[] imageFiles)
        {
            listBoxImages.Items.Clear();
            foreach (string file in imageFiles)
            {
                // 파일명만 표시를 원할 수 있으나, 현재는 경로를 바로 넣습니다. (UI에 따라 변경 가능)
                listBoxImages.Items.Add(file);
            }
        }

        private void listBoxImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxImages.SelectedItem != null)
            {
                string selectedPath = listBoxImages.SelectedItem.ToString();
                ImageSelected?.Invoke(this, selectedPath);
            }
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
