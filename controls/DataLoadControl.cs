using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;

namespace SimpleDonkeyManager
{
    public partial class DataLoadControl : UserControl
    {

        private controlutils.ImageList imageList = new controlutils.ImageList();
        private MainWindow mainWindow;

        public DataLoadControl()
        {
            InitializeComponent();

            imageList.Dock = DockStyle.Fill;
            imageList.Visible = true;
            pnlFrameList.Controls.Add(imageList);
        }

        private void DataLoadControl_Load(object sender, EventArgs e)
        {
            // 부모 폼(MainWindow) 찾기
            mainWindow = this.FindForm() as MainWindow;
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

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "이미지 폴더를 선택하세요";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = dialog.SelectedPath;

                    // .jpg 이미지 파일 수집
                    string[] imageFiles = Directory.GetFiles(folderPath, "*.jpg");

                    string[] jsonFiles = Directory.GetFiles(folderPath, "*.json");

                    MessageBox.Show($"JSON 파일 {jsonFiles.Length}개를 찾았습니다.");

                    // 총 이미지 수 업데이트
                    lblTotalImagesValue.Text = $"{imageFiles.Length:N0} 장";

                    // MainWindow의 상태 라벨 업데이트
                    if (mainWindow != null)
                    {
                        mainWindow.UpdateProgramStatus(
                            folderPath,
                            imageFiles.Length,
                            0,  // 현재는 로드된 프레임이 0
                            "데이터 필터링 필요"
                        );
                    }
                }
            }
        }
    }
}