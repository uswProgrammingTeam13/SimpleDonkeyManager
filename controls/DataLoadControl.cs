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
        private controlutils.ImageViewer imageViewer = new controlutils.ImageViewer();
        private MainWindow mainWindow;

        public DataLoadControl()
        {
            InitializeComponent();

            // ImageList 설정
            imageList.Dock = DockStyle.Fill;
            imageList.Visible = true;
            pnlFrameList.Controls.Add(imageList);

            imageViewer.Dock = DockStyle.Fill;
            imageViewer.Visible = true;
            pnlImageView.Controls.Add(imageViewer);

            // 이미지 선택 이벤트 구독
            imageList.ImageSelected += ImageList_ImageSelected;
        }

        private void ImageList_ImageSelected(object sender, string imagePath)
        {
            imageViewer.DisplayImage(imagePath);

            // 필요하다면 MainWindow를 통해 DataFilterControl 등 다른 뷰에도 이미지 경로를 전달할 수 있습니다.
            if (mainWindow != null)
            {
                mainWindow.NotifyImageSelected(imagePath);
            }
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

                    // 리스트에 이미지 로드
                    imageList.LoadImages(imageFiles);

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
                        mainWindow.LoadImagesToControls(imageFiles); // 필터 컨트롤에도 이미지 전달
                    }
                }
            }
        }
    }
}