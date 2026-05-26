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
        private ImageManager imageManager = new ImageManager();

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

            // 초기 상태 - 데이터 폴더 선택 전
            InitializeDefaultState();
        }

        /// <summary>
        /// 초기 상태 설정
        /// </summary>
        private void InitializeDefaultState()
        {
            lblTotalImagesValue.Text = "- 장";
            lblImageFormat.Text = "-";
            lblResolutionValue.Text = "- x -";
            lblFileSizeValue.Text = "- byte";
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

                    // 폴더 스캔 (이미지 및 JSON 파일)
                    if (!imageManager.ScanFolder(folderPath))
                    {
                        MessageBox.Show("폴더 스캔에 실패했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 폴더 통계 정보 가져오기
                    FolderStatistics stats = imageManager.GetFolderStatistics();

                    // UI 업데이트 - 이미지 정보 표시
                    lblTotalImagesValue.Text = $"{stats.TotalImageCount:N0} 장";
                    lblImageFormat.Text = stats.ImageFormats.Length > 0 ? string.Join(", ", stats.ImageFormats) : "Unknown";
                    lblResolutionValue.Text = stats.Resolutions.Count > 0 ? string.Join(", ", stats.Resolutions) : "Unknown";
                    lblFileSizeValue.Text = stats.GetFormattedFileSize();

                    // MainWindow의 상태 라벨 업데이트
                    if (mainWindow != null)
                    {
                        mainWindow.UpdateProgramStatus(
                            folderPath,
                            stats.TotalImageCount,
                            0,  // 현재는 로드된 프레임이 0
                            "데이터 로드 준비 완료"
                        );
                    }
                }
            }
        }

        private void btnLoadStart_Click(object sender, EventArgs e)
        {
            // 폴더가 선택되었는지 확인
            if (imageManager.GetAllFrameData().Count == 0)
            {
                MessageBox.Show("먼저 이미지 폴더를 선택하세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ImageList에 프레임 정보 표시
            List<FrameData> allFrames = imageManager.GetAllFrameData();
            imageList.LoadFrames(allFrames);

            // ImageList와 ImageViewer에 데이터 전달
            imageList.SetImageManager(imageManager);
            imageViewer.SetImageManager(imageManager);

            // 첫 번째 프레임 표시
            if (allFrames.Count > 0)
            {
                imageList.SelectFrame(0);
            }

            // MainWindow의 DataFilterControl에도 동일한 데이터 전달
            if (mainWindow != null)
            {
                mainWindow.SetFilterControlData(imageManager, allFrames);

                mainWindow.UpdateProgramStatus(
                    imageManager.SelectedFolderPath,
                    allFrames.Count,
                    allFrames.Count,
                    "데이터 로드 완료"
                );
            }
        }
    }
}
