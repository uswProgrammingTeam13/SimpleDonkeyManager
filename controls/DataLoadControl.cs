using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace SimpleDonkeyManager
{
    public partial class DataLoadControl : UserControl
    {

        private controlutils.ImageList imageList = new controlutils.ImageList();
        private controlutils.ImageViewer imageViewer = new controlutils.ImageViewer();
        private MainWindow mainWindow;
        private ImageManager imageManager = new ImageManager();
        private Logger logger;

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

            logger = null;
        }

        /// <summary>
        /// Logger를 설정합니다.
        /// </summary>
        public void SetLogger(Logger log)
        {
            logger = log;
            // 자식 컨트롤에도 Logger 전달
            imageList.SetLogger(log);
            imageViewer.SetLogger(log);
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


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "이미지 폴더를 선택하세요";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string folderPath = dialog.SelectedPath;

                        if (string.IsNullOrWhiteSpace(folderPath))
                        {
                            LogWarning("선택된 폴더 경로가 유효하지 않습니다");
                            return;
                        }

                        LogInfo($"폴더 선택: {folderPath}");

                        // 폴더 스캔 (이미지 및 JSON 파일)
                        if (!imageManager.ScanFolder(folderPath))
                        {
                            MessageBox.Show("폴더 스캔에 실패했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LogWarning($"폴더 스캔 실패: {folderPath}");
                            InitializeDefaultState();
                            return;
                        }

                        // 폴더 통계 정보 가져오기
                        FolderStatistics stats = imageManager.GetFolderStatistics();
                        if (stats == null)
                        {
                            LogWarning("폴더 통계 정보를 가져올 수 없습니다");
                            InitializeDefaultState();
                            return;
                        }

                        LogInfo($"폴더 스캔 완료: {stats.TotalImageCount}개 이미지, {stats.GetFormattedFileSize()} 크기");

                        // UI 업데이트 - 이미지 정보 표시 (안전한 접근)
                        try
                        {
                            lblTotalImagesValue.Text = $"{stats.TotalImageCount:N0} 장";
                            lblImageFormat.Text = (stats.ImageFormats?.Length ?? 0) > 0 
                                ? string.Join(", ", stats.ImageFormats ?? new string[] { }) 
                                : "Unknown";
                            lblResolutionValue.Text = (stats.Resolutions?.Count ?? 0) > 0 
                                ? string.Join(", ", stats.Resolutions ?? new List<string>()) 
                                : "Unknown";
                            lblFileSizeValue.Text = stats.GetFormattedFileSize() ?? "0 byte";
                        }
                        catch (Exception ex)
                        {
                            LogWarning($"UI 업데이트 오류: {ex.Message}");
                            InitializeDefaultState();
                            return;
                        }

                        // MainWindow의 상태 라벨 업데이트
                        if (mainWindow != null)
                        {
                            try
                            {
                                mainWindow.UpdateProgramStatus(
                                    folderPath,
                                    stats.TotalImageCount,
                                    0,  // 현재는 로드된 프레임이 0
                                    "데이터 로드 준비 완료"
                                );
                            }
                            catch (Exception ex)
                            {
                                LogWarning($"상태 업데이트 오류: {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"폴더 선택 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"폴더 선택 예외: {ex.Message}");
                InitializeDefaultState();
            }
        }

        private void btnLoadStart_Click(object sender, EventArgs e)
        {
            try
            {
                // ImageManager null 체크
                if (imageManager == null)
                {
                    MessageBox.Show("ImageManager가 초기화되지 않았습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LogWarning("데이터 로드 실패: ImageManager가 null");
                    return;
                }

                // 폴더가 선택되었는지 확인
                List<FrameData> allFrames = imageManager.GetAllFrameData();
                if (allFrames == null || allFrames.Count == 0)
                {
                    MessageBox.Show("먼저 이미지 폴더를 선택하세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogWarning("데이터 로드 실패: 폴더 선택 안 됨");
                    return;
                }

                // ImageList null 체크 및 데이터 로드
                if (imageList == null)
                {
                    MessageBox.Show("ImageList 컨트롤이 초기화되지 않았습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LogWarning("데이터 로드 실패: ImageList가 null");
                    return;
                }

                imageList.LoadFrames(allFrames);

                // ImageViewer null 체크
                if (imageViewer == null)
                {
                    LogWarning("데이터 로드 경고: ImageViewer가 null");
                }
                else
                {
                    imageList.SetImageManager(imageManager);
                    imageViewer.SetImageManager(imageManager);
                }

                // 첫 번째 프레임 표시
                if (allFrames.Count > 0)
                {
                    try
                    {
                        imageList.SelectFrame(0);
                    }
                    catch (Exception ex)
                    {
                        LogWarning($"첫 번째 프레임 선택 실패: {ex.Message}");
                    }
                }

                // MainWindow의 DataFilterControl에도 동일한 데이터 전달
                if (mainWindow != null)
                {
                    try
                    {
                        mainWindow.SetFilterControlData(imageManager, allFrames);

                        // TrainingControl에도 전체 데이터 전달 (필터링 이전)
                        mainWindow.SetTrainingFullData(imageManager, allFrames);

                        mainWindow.UpdateProgramStatus(
                            imageManager.SelectedFolderPath,
                            allFrames.Count,
                            allFrames.Count,
                            "데이터 로드 완료"
                        );
                        LogInfo($"데이터 로드 완료: {allFrames.Count}개 프레임 로드");
                    }
                    catch (Exception ex)
                    {
                        LogWarning($"MainWindow 데이터 전달 오류: {ex.Message}");
                    }
                }
                else
                {
                    LogWarning("MainWindow 참조를 사용할 수 없습니다");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터 로드 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"데이터 로드 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 정보 로그를 기록합니다.
        /// </summary>
        private void LogInfo(string message)
        {
            if (logger != null)
            {
                logger.AppendLog($"[데이터로드] {message}");
            }
        }

        /// <summary>
        /// 경고 로그를 기록합니다.
        /// </summary>
        private void LogWarning(string message)
        {
            if (logger != null)
            {
                logger.AppendLog($"[데이터로드 경고] {message}");
            }
        }
    }
}
