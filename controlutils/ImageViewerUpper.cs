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
    public partial class ImageViewerUpper : UserControl
    {
        private SimpleDonkeyManager.ImageManager imageManager;
        private List<SimpleDonkeyManager.FrameData> frameDataList = new List<SimpleDonkeyManager.FrameData>();
        private int currentFrameIndex = 0;
        private System.Windows.Forms.Timer playTimer;
        private bool isPlaying = false;
        private double playbackSpeed = 1.0;
        private const int FRAMES_PER_SECOND = 20;
        private SimpleDonkeyManager.Logger logger;

        public ImageViewerUpper()
        {
            InitializeComponent();

            // PictureBox 설정
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            // 버튼 이벤트
            button1.Click += Button1_Click; // 처음
            button6.Click += Button6_Click; // 이전
            button5.Click += Button5_Click; // 재생
            button2.Click += Button2_Click; // 다음
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            trackBar2.ValueChanged += TrackBar2_ValueChanged;

            // 타이머 설정
            playTimer = new System.Windows.Forms.Timer();
            playTimer.Interval = (int)(1000.0 / (FRAMES_PER_SECOND * playbackSpeed));
            playTimer.Tick += PlayTimer_Tick;

            logger = null;
        }

        public void SetLogger(SimpleDonkeyManager.Logger log)
        {
            logger = log;
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

                frameDataList = imageManager.GetAllFrameData();

                if (frameDataList == null)
                {
                    frameDataList = new List<SimpleDonkeyManager.FrameData>();
                }

                // TrackBar 설정
                if (trackBar2 != null && frameDataList.Count > 0)
                {
                    trackBar2.Minimum = 0;
                    trackBar2.Maximum = frameDataList.Count - 1;
                    trackBar2.Value = 0;
                    currentFrameIndex = 0;
                    DisplayFrameAtIndex(0);
                }

                LogInfo($"ImageViewerUpper 설정: {frameDataList.Count}개 프레임");
            }
            catch (Exception ex)
            {
                LogWarning($"ImageViewerUpper SetImageManager 예외: {ex.Message}");
            }
        }

        public void LoadFrames(List<SimpleDonkeyManager.FrameData> frames)
        {
            try
            {
                frameDataList = frames ?? new List<SimpleDonkeyManager.FrameData>();

                if (trackBar2 != null && frameDataList.Count > 0)
                {
                    trackBar2.Minimum = 0;
                    trackBar2.Maximum = frameDataList.Count - 1;
                    trackBar2.Value = 0;
                    currentFrameIndex = 0;
                    DisplayFrameAtIndex(0);
                }

                LogInfo($"ImageViewerUpper 프레임 로드: {frameDataList.Count}개");
            }
            catch (Exception ex)
            {
                LogWarning($"ImageViewerUpper LoadFrames 예외: {ex.Message}");
            }
        }

        private void DisplayFrameAtIndex(int index)
        {
            try
            {
                if (frameDataList == null || frameDataList.Count == 0)
                    return;

                index = Math.Max(0, Math.Min(index, frameDataList.Count - 1));
                currentFrameIndex = index;

                var frameData = frameDataList[index];
                if (frameData == null || string.IsNullOrEmpty(frameData.ImagePath) || !File.Exists(frameData.ImagePath))
                {
                    LogWarning($"프레임 파일 없음: {frameData?.ImagePath}");
                    return;
                }

                try
                {
                    if (pictureBox1 != null)
                    {
                        if (pictureBox1.Image != null)
                        {
                            try
                            {
                                var oldImage = pictureBox1.Image;
                                pictureBox1.Image = null;
                                oldImage?.Dispose();
                            }
                            catch { }
                        }

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

                UpdateFrameInfo(frameData);

                if (trackBar2 != null && frameDataList.Count > 0)
                {
                    try
                    {
                        trackBar2.Value = Math.Min(index, trackBar2.Maximum);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                LogWarning($"프레임 표시 예외: {ex.Message}");
            }
        }

        private void UpdateFrameInfo(SimpleDonkeyManager.FrameData frameData)
        {
            try
            {
                if (frameData == null)
                    return;

                // 프레임 정보 업데이트
                if (label2 != null)
                {
                    label2.Text = $"Frame: {frameData.FrameNumber:0000} / {frameDataList.Count:0,0}";
                }

                // Angle 정보
                if (label3 != null)
                {
                    double angle = frameData.GetAngle();
                    label3.Text = $"Angle: {angle:F2} rad";
                }

                // Throttle 정보
                if (label4 != null)
                {
                    double throttle = frameData.GetThrottle();
                    label4.Text = $"Throttle: {throttle:F2}";
                }
            }
            catch (Exception ex)
            {
                LogWarning($"프레임 정보 업데이트 예외: {ex.Message}");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // 처음으로
            DisplayFrameAtIndex(0);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            // 이전 프레임
            if (currentFrameIndex > 0)
            {
                DisplayFrameAtIndex(currentFrameIndex - 1);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            // 재생/정지 토글
            if (isPlaying)
            {
                isPlaying = false;
                playTimer.Stop();
                button5.Text = "▶";
                button5.BackColor = Color.DodgerBlue;
            }
            else
            {
                if (frameDataList == null || frameDataList.Count == 0)
                {
                    LogWarning("재생 실패: 프레임 데이터 없음");
                    return;
                }

                isPlaying = true;
                playTimer.Start();
                button5.Text = "⏸";
                button5.BackColor = Color.DarkOrange;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // 다음 프레임
            if (currentFrameIndex < frameDataList.Count - 1)
            {
                DisplayFrameAtIndex(currentFrameIndex + 1);
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
                    else
                    {
                        isPlaying = false;
                        playTimer.Stop();
                        button5.Text = "▶";
                        button5.BackColor = Color.DodgerBlue;
                    }
                }
            }
            catch (Exception ex)
            {
                LogWarning($"재생 타이머 예외: {ex.Message}");
                isPlaying = false;
                playTimer.Stop();
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1 == null)
                    return;

                string speedText = comboBox1.SelectedItem?.ToString() ?? "1.0x";
                speedText = speedText.Replace("x", "");

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

        private void TrackBar2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (trackBar2 != null && frameDataList != null && frameDataList.Count > 0)
                {
                    int index = trackBar2.Value;
                    if (index >= 0 && index < frameDataList.Count)
                    {
                        DisplayFrameAtIndex(index);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWarning($"트랙바 값 변경 예외: {ex.Message}");
            }
        }

        private void LogInfo(string message)
        {
            if (logger != null)
            {
                logger.AppendLog($"[미리보기] {message}");
            }
        }

        private void LogWarning(string message)
        {
            if (logger != null)
            {
                logger.AppendLog($"[미리보기 경고] {message}");
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {
        }

        private void label8_Click_1(object sender, EventArgs e)
        {
        }
    }
}
