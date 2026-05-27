using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SimpleDonkeyManager.controlutils
{
    public partial class ImageViewer : UserControl
    {
        private SimpleDonkeyManager.ImageManager imageManager;
        private List<SimpleDonkeyManager.FrameData> frameDataList = new List<SimpleDonkeyManager.FrameData>();
        private int currentFrameIndex = 0;
        private System.Windows.Forms.Timer playTimer;
        private bool isPlaying = false;
        private double playbackSpeed = 1.0;
        private const int FRAMES_PER_SECOND = 20;
        private SimpleDonkeyManager.Logger logger;

        public ImageViewer()
        {
            InitializeComponent();

            // pictureBox1은 이미 Designer에서 pnlCenterMain에 추가됨
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            InitializeListView();

            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
            button4.Click += Button4_Click;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            trackBar1.ValueChanged += TrackBar1_ValueChanged;

            // 리사이즈 이벤트 추가 - 썸네일 적응형 표시
            this.Resize += ImageViewer_Resize;
            pnlLeftThumbnail.Resize += PnlThumbnail_Resize;
            pnlRightThumbnail.Resize += PnlThumbnail_Resize;

            playTimer = new System.Windows.Forms.Timer();
            playTimer.Interval = (int)(1000.0 / (FRAMES_PER_SECOND * playbackSpeed));
            playTimer.Tick += PlayTimer_Tick;

            UpdateCurrentFrameDisplay();

            logger = null;
        }

        /// <summary>
        /// Logger를 설정합니다.
        /// </summary>
        public void SetLogger(SimpleDonkeyManager.Logger log)
        {
            logger = log;
        }

        private void InitializeListView()
        {
            lstJSONSummary.Columns.Clear();
            lstJSONSummary.Columns.Add("항목", 80);
            lstJSONSummary.Columns.Add("값", 120);
        }

        public void SetImageManager(SimpleDonkeyManager.ImageManager manager)
        {
            try
            {
                imageManager = manager;

                if (imageManager == null)
                {
                    LogWarning("SetImageManager: ImageManager가 null입니다");
                    frameDataList = new List<SimpleDonkeyManager.FrameData>();
                    return;
                }

                // ImageManager에서 프레임 데이터 가져오기
                frameDataList = imageManager.GetAllFrameData();

                if (frameDataList == null)
                {
                    frameDataList = new List<SimpleDonkeyManager.FrameData>();
                }

                // TrackBar 설정
                if (trackBar1 != null && frameDataList.Count > 0)
                {
                    trackBar1.Minimum = 0;
                    trackBar1.Maximum = frameDataList.Count - 1;
                    trackBar1.Value = 0;
                    currentFrameIndex = 0;
                    UpdateCurrentFrameDisplay();

                    // 첫 번째 프레임 자동 표시
                    try
                    {
                        DisplayFrameAtIndex(0);
                    }
                    catch (Exception ex)
                    {
                        LogWarning($"첫 번째 프레임 자동 표시 오류: {ex.Message}");
                    }
                }
                else if (trackBar1 != null)
                {
                    trackBar1.Minimum = 0;
                    trackBar1.Maximum = 0;
                    trackBar1.Value = 0;
                }

                LogInfo($"ImageManager 설정: {frameDataList.Count}개 프레임");
            }
            catch (Exception ex)
            {
                LogWarning($"SetImageManager 예외: {ex.Message}");
                frameDataList = new List<SimpleDonkeyManager.FrameData>();
            }
        }

        public void DisplayImage(string imagePath)
        {
            try
            {
                if (frameDataList == null || frameDataList.Count == 0)
                {
                    LogWarning("DisplayImage: 프레임 데이터가 없습니다");
                    return;
                }

                if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
                {
                    LogWarning($"DisplayImage: 이미지 파일 없음: {imagePath}");
                    return;
                }

                // imagePath와 일치하는 프레임 찾기
                int foundIndex = -1;
                for (int i = 0; i < frameDataList.Count; i++)
                {
                    if (frameDataList[i] != null && frameDataList[i].ImagePath == imagePath)
                    {
                        foundIndex = i;
                        break;
                    }
                }

                // 일치하는 프레임을 찾지 못하면 경고 후 반환
                if (foundIndex < 0)
                {
                    LogWarning($"DisplayImage: 프레임 데이터에서 경로를 찾을 수 없음: {imagePath}");
                    return;
                }

                currentFrameIndex = foundIndex;
                DisplayFrameAtIndex(currentFrameIndex);

                if (currentFrameIndex >= 0 && currentFrameIndex < frameDataList.Count && frameDataList[currentFrameIndex] != null)
                {
                    LogInfo($"프레임 표시: {frameDataList[currentFrameIndex].FrameNumber}");
                }
            }
            catch (Exception ex)
            {
                LogWarning($"이미지 표시 예외: {ex.Message}");
            }
        }

        private void DisplayFrameAtIndex(int index)
        {
            try
            {
                if (frameDataList == null || frameDataList.Count == 0)
                {
                    LogWarning("DisplayFrameAtIndex: 프레임 데이터가 없습니다");
                    return;
                }

                if (index < 0 || index >= frameDataList.Count)
                {
                    LogWarning($"DisplayFrameAtIndex: 인덱스 {index}가 범위를 벗어났습니다 (총 {frameDataList.Count}개)");
                    return;
                }

                currentFrameIndex = index;
                var frameData = frameDataList[index];

                if (frameData == null)
                {
                    LogWarning($"DisplayFrameAtIndex: 프레임 데이터가 null입니다 (인덱스 {index})");
                    return;
                }

                if (string.IsNullOrEmpty(frameData.ImagePath) || !File.Exists(frameData.ImagePath))
                {
                    LogWarning($"DisplayFrameAtIndex: 이미지 파일 없음: {frameData.ImagePath}");
                    return;
                }

                try
                {
                    if (pictureBox1 != null)
                    {
                        // 기존 이미지 정리
                        if (pictureBox1.Image != null)
                        {
                            try
                            {
                                var oldImage = pictureBox1.Image;
                                pictureBox1.Image = null;
                                oldImage?.Dispose();
                            }
                            catch
                            {
                                // 이미지 해제 실패 시 계속
                            }
                        }

                        // 새 이미지 로드
                        using (var stream = new FileStream(frameData.ImagePath, FileMode.Open, FileAccess.Read))
                        {
                            pictureBox1.Image = Image.FromStream(stream);
                        }
                    }
                }
                catch (IOException ex)
                {
                    LogWarning($"이미지 파일 읽기 오류: {ex.Message}");
                    return;
                }
                catch (OutOfMemoryException ex)
                {
                    LogWarning($"이미지 메모리 오류: {ex.Message}");
                    return;
                }

                DisplayThumbnails();
                UpdateJSONInfo(frameData);
                UpdateCurrentFrameDisplay();

                if (trackBar1 != null && frameDataList.Count > 0)
                {
                    try
                    {
                        trackBar1.Value = Math.Min(index, trackBar1.Maximum);
                    }
                    catch
                    {
                        // 트랙바 값 설정 실패 시 계속
                    }
                }
            }
            catch (Exception ex)
            {
                LogWarning($"프레임 표시 예외: {ex.Message}");
            }
        }

        private void DisplayThumbnails()
        {
            try
            {
                if (frameDataList == null || frameDataList.Count == 0)
                    return;

                // 이전 프레임 썸네일
                if (currentFrameIndex > 0)
                {
                    var prevFrame = frameDataList[currentFrameIndex - 1];
                    if (prevFrame != null && !string.IsNullOrEmpty(prevFrame.ImagePath) && File.Exists(prevFrame.ImagePath))
                    {
                        try
                        {
                            if (pictureBox2 != null)
                            {
                                if (pictureBox2.Image != null)
                                {
                                    pictureBox2.Image?.Dispose();
                                }
                                pictureBox2.Image = Image.FromFile(prevFrame.ImagePath);
                            }
                        }
                        catch
                        {
                            // 이전 썸네일 로드 실패 시 계속
                            if (pictureBox2 != null)
                                pictureBox2.Image = null;
                        }
                    }
                    else if (pictureBox2 != null)
                    {
                        pictureBox2.Image = null;
                    }
                }
                else if (pictureBox2 != null)
                {
                    pictureBox2.Image = null;
                }

                // 다음 프레임 썸네일
                if (currentFrameIndex < frameDataList.Count - 1)
                {
                    var nextFrame = frameDataList[currentFrameIndex + 1];
                    if (nextFrame != null && !string.IsNullOrEmpty(nextFrame.ImagePath) && File.Exists(nextFrame.ImagePath))
                    {
                        try
                        {
                            if (pictureBox3 != null)
                            {
                                if (pictureBox3.Image != null)
                                {
                                    pictureBox3.Image?.Dispose();
                                }
                                pictureBox3.Image = Image.FromFile(nextFrame.ImagePath);
                            }
                        }
                        catch
                        {
                            // 다음 썸네일 로드 실패 시 계속
                            if (pictureBox3 != null)
                                pictureBox3.Image = null;
                        }
                    }
                    else if (pictureBox3 != null)
                    {
                        pictureBox3.Image = null;
                    }
                }
                else if (pictureBox3 != null)
                {
                    pictureBox3.Image = null;
                }
            }
            catch (Exception ex)
            {
                LogWarning($"썸네일 표시 예외: {ex.Message}");
            }
        }

        private void UpdateJSONInfo(SimpleDonkeyManager.FrameData frameData)
        {
            try
            {
                if (frameData == null)
                {
                    if (lstJSONSummary != null)
                    {
                        lstJSONSummary.Items.Clear();
                    }
                    return;
                }

                if (lstJSONSummary == null)
                    return;

                lstJSONSummary.Items.Clear();

                if (frameData.Metadata != null)
                {
                    // 스로틀 정보
                    if (frameData.Metadata.ContainsKey("user/throttle"))
                    {
                        try
                        {
                            var item = new ListViewItem("스로틀");
                            item.SubItems.Add(frameData.Metadata["user/throttle"]?.ToString() ?? "");
                            lstJSONSummary.Items.Add(item);
                        }
                        catch
                        {
                            // 항목 추가 실패 시 계속
                        }
                    }

                    // 앵글 정보
                    if (frameData.Metadata.ContainsKey("user/angle"))
                    {
                        try
                        {
                            var item = new ListViewItem("앵글");
                            item.SubItems.Add(frameData.Metadata["user/angle"]?.ToString() ?? "");
                            lstJSONSummary.Items.Add(item);
                        }
                        catch
                        {
                            // 항목 추가 실패 시 계속
                        }
                    }
                }

                // 이미지 이름
                if (!string.IsNullOrEmpty(frameData.ImageFileName))
                {
                    try
                    {
                        var imgItem = new ListViewItem("이미지");
                        imgItem.SubItems.Add(frameData.ImageFileName);
                        lstJSONSummary.Items.Add(imgItem);
                    }
                    catch
                    {
                        // 항목 추가 실패 시 계속
                    }
                }
            }
            catch (Exception ex)
            {
                LogWarning($"JSON 정보 업데이트 예외: {ex.Message}");
            }
        }

        private void UpdateCurrentFrameDisplay()
        {
            try
            {
                if (label5 == null)
                    return;

                if (currentFrameIndex >= 0 && currentFrameIndex < frameDataList.Count)
                {
                    var currentFrame = frameDataList[currentFrameIndex];
                    if (currentFrame != null)
                    {
                        label5.Text = $"현재 : Frame {currentFrame.FrameNumber}";
                    }
                    else
                    {
                        label5.Text = "현재 : 프레임 없음";
                    }
                }
                else
                {
                    label5.Text = "현재 : 프레임 없음";
                }
            }
            catch (Exception ex)
            {
                LogWarning($"현재 프레임 표시 업데이트 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 이미지 크기 변경 이벤트 - 썸네일 크기를 패널에 맞춤
        /// </summary>
        private void ImageViewer_Resize(object sender, EventArgs e)
        {
            // pnlCenterMain의 크기에 따라 pictureBox1 크기 조정
            if (pnlCenterMain != null && pictureBox1 != null)
            {
                pictureBox1.Width = pnlCenterMain.Width;
                pictureBox1.Height = pnlCenterMain.Height;
            }
        }

        /// <summary>
        /// 썸네일 패널 리사이즈 이벤트
        /// </summary>
        private void PnlThumbnail_Resize(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null && panel.Controls.Count > 0)
            {
                // 패널의 모든 PictureBox 크기 조정
                foreach (Control control in panel.Controls)
                {
                    if (control is PictureBox picBox)
                    {
                        // 패널 크기에 맞춰 PictureBox 크기 자동 조정
                        picBox.Width = panel.Width;
                        picBox.Height = panel.Height - (panel.Height > 50 ? 50 : 0);
                    }
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (frameDataList == null || frameDataList.Count == 0)
                {
                    LogWarning("이전 프레임 이동 실패: 프레임 데이터 없음");
                    return;
                }

                if (currentFrameIndex > 0)
                {
                    DisplayFrameAtIndex(currentFrameIndex - 1);
                    if (currentFrameIndex >= 0 && currentFrameIndex < frameDataList.Count && frameDataList[currentFrameIndex] != null)
                    {
                        LogInfo($"이전 프레임으로 이동: {frameDataList[currentFrameIndex].FrameNumber}");
                    }
                }
            }
            catch (Exception ex)
            {
                LogWarning($"이전 프레임 이동 예외: {ex.Message}");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (frameDataList == null || frameDataList.Count == 0)
                {
                    LogWarning("다음 프레임 이동 실패: 프레임 데이터 없음");
                    return;
                }

                if (currentFrameIndex < frameDataList.Count - 1)
                {
                    DisplayFrameAtIndex(currentFrameIndex + 1);
                    if (currentFrameIndex >= 0 && currentFrameIndex < frameDataList.Count && frameDataList[currentFrameIndex] != null)
                    {
                        LogInfo($"다음 프레임으로 이동: {frameDataList[currentFrameIndex].FrameNumber}");
                    }
                }
            }
            catch (Exception ex)
            {
                LogWarning($"다음 프레임 이동 예외: {ex.Message}");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (frameDataList == null || frameDataList.Count == 0)
                {
                    LogWarning("재생 실패: 프레임 데이터 없음");
                    return;
                }

                if (!isPlaying && playTimer != null)
                {
                    isPlaying = true;
                    playTimer.Start();
                    LogInfo("재생 시작");
                }
            }
            catch (Exception ex)
            {
                LogWarning($"재생 시작 예외: {ex.Message}");
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                isPlaying = false;
                if (playTimer != null)
                {
                    playTimer.Stop();
                }
                currentFrameIndex = 0;
                DisplayFrameAtIndex(0);
                LogInfo("재생 정지 및 초기화");
            }
            catch (Exception ex)
            {
                LogWarning($"재생 정지 예외: {ex.Message}");
            }
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (isPlaying && frameDataList != null && frameDataList.Count > 0)
                {
                    if (currentFrameIndex < frameDataList.Count - 1)
                    {
                        DisplayFrameAtIndex(currentFrameIndex + 1);
                    }
                    else if (isPlaying && playTimer != null)
                    {
                        isPlaying = false;
                        playTimer.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                LogWarning($"재생 타이머 예외: {ex.Message}");
                isPlaying = false;
                if (playTimer != null)
                    playTimer.Stop();
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1 == null)
                    return;

                string speedText = comboBox1.SelectedItem?.ToString() ?? "1.0";
                if (double.TryParse(speedText, out double speed))
                {
                    if (speed > 0)
                    {
                        playbackSpeed = speed;
                        if (playTimer != null)
                        {
                            playTimer.Interval = (int)(1000.0 / (FRAMES_PER_SECOND * playbackSpeed));
                        }
                        LogInfo($"재생 속도 변경: {speed}x");
                    }
                }
            }
            catch (Exception ex)
            {
                LogWarning($"재생 속도 변경 예외: {ex.Message}");
            }
        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!isPlaying && trackBar1 != null)
                {
                    int value = trackBar1.Value;
                    if (value >= 0 && value < frameDataList.Count)
                    {
                        DisplayFrameAtIndex(value);
                        if (frameDataList[value] != null)
                        {
                            LogInfo($"트랙바로 프레임 이동: {frameDataList[value].FrameNumber}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogWarning($"트랙바 값 변경 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 정보 로그를 기록합니다.
        /// </summary>
        private void LogInfo(string message)
        {
            if (logger != null)
            {
                logger.AppendLog($"[이미지뷰어] {message}");
            }
        }

        /// <summary>
        /// 경고 로그를 기록합니다.
        /// </summary>
        private void LogWarning(string message)
        {
            if (logger != null)
            {
                logger.AppendLog($"[이미지뷰어 경고] {message}");
            }
        }
    }
}

