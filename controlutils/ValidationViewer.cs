using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SimpleDonkeyManager.controlutils
{
    /// <summary>
    /// 검증 결과를 프레임 단위로 보여주는 뷰어입니다.
    /// 현재 프레임 이미지, 실제값/AI 예측값/오차를 표시하며
    /// ImageViewer 와 유사한 처음/재생/다음 버튼과 트랙바를 제공합니다.
    /// </summary>
    public partial class ValidationViewer : UserControl
    {
        private List<SimpleDonkeyManager.ValidationResult> results = new List<SimpleDonkeyManager.ValidationResult>();
        private int currentIndex = 0;
        private System.Windows.Forms.Timer playTimer;
        private bool isPlaying = false;
        private double playbackSpeed = 1.0;
        private const int FRAMES_PER_SECOND = 20;

        public ValidationViewer()
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            btnFirst.Click += BtnFirst_Click;
            btnPlay.Click += BtnPlay_Click;
            btnNext.Click += BtnNext_Click;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            trackBar1.ValueChanged += TrackBar1_ValueChanged;

            playTimer = new System.Windows.Forms.Timer();
            playTimer.Interval = (int)(1000.0 / (FRAMES_PER_SECOND * playbackSpeed));
            playTimer.Tick += PlayTimer_Tick;
        }

        /// <summary>
        /// 검증 결과 목록을 로드하고 첫 프레임을 표시합니다.
        /// </summary>
        public void LoadResults(List<SimpleDonkeyManager.ValidationResult> validationResults)
        {
            results = validationResults ?? new List<SimpleDonkeyManager.ValidationResult>();

            StopPlayback();

            if (results.Count > 0)
            {
                trackBar1.Minimum = 0;
                trackBar1.Maximum = results.Count - 1;
                trackBar1.Value = 0;
                currentIndex = 0;
                DisplayFrameAtIndex(0);
            }
            else
            {
                if (pictureBox1.Image != null)
                {
                    var old = pictureBox1.Image;
                    pictureBox1.Image = null;
                    old.Dispose();
                }
                lblFrame.Text = "현재 프레임: -";
                lblAngle.Text = "실제 조향값: -    AI 예측 조향값: -";
                lblThrottle.Text = "실제 속도값: -    AI 예측 속도값: -";
                lblError.Text = "오차: -";
            }
        }

        public void ClearResults()
        {
            LoadResults(new List<SimpleDonkeyManager.ValidationResult>());
        }

        /// <summary>
        /// 검증이 진행되는 동안 현재 처리 중인 프레임 이미지를 실시간으로 표시합니다.
        /// 백그라운드 스레드에서 호출될 수 있으므로 UI 스레드로 전환합니다.
        /// </summary>
        public void ShowProgressFrame(string imagePath, int frameNumber, int current, int total)
        {
            if (InvokeRequired)
            {
                try { BeginInvoke(new Action(() => ShowProgressFrame(imagePath, frameNumber, current, total))); }
                catch { }
                return;
            }

            try
            {
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    if (pictureBox1.Image != null)
                    {
                        try
                        {
                            var old = pictureBox1.Image;
                            pictureBox1.Image = null;
                            old.Dispose();
                        }
                        catch { }
                    }

                    using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox1.Image = Image.FromStream(stream);
                    }
                }

                lblFrame.Text = $"검증 중... ({current}/{total})  Frame {frameNumber}";
                lblAngle.Text = "실제 조향값: -    AI 예측 조향값: -";
                lblThrottle.Text = "실제 속도값: -    AI 예측 속도값: -";
                lblError.Text = "오차 계산 중...";
            }
            catch
            {
                // 진행 표시 실패는 무시
            }
        }

        private void DisplayFrameAtIndex(int index)
        {
            try
            {
                if (results == null || results.Count == 0)
                    return;

                index = Math.Max(0, Math.Min(index, results.Count - 1));
                currentIndex = index;

                var item = results[index];

                if (!string.IsNullOrEmpty(item.ImagePath) && File.Exists(item.ImagePath))
                {
                    if (pictureBox1.Image != null)
                    {
                        try
                        {
                            var old = pictureBox1.Image;
                            pictureBox1.Image = null;
                            old.Dispose();
                        }
                        catch { }
                    }

                    using (var stream = new FileStream(item.ImagePath, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox1.Image = Image.FromStream(stream);
                    }
                }

                lblFrame.Text = $"현재 프레임: Frame {item.Frame}";
                lblAngle.Text = $"실제 조향값: {item.ActualAngle:F2}    AI 예측 조향값: {item.PredAngle:F2}";
                lblThrottle.Text = $"실제 속도값: {item.ActualThrottle:F2}    AI 예측 속도값: {item.PredThrottle:F2}";
                lblError.Text = $"오차: {item.AngleError:F2}";

                if (trackBar1.Maximum >= index)
                {
                    try { trackBar1.Value = index; } catch { }
                }
            }
            catch
            {
                // 이미지 로드 실패 시 무시
            }
        }

        private void BtnFirst_Click(object sender, EventArgs e)
        {
            DisplayFrameAtIndex(0);
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                StopPlayback();
            }
            else
            {
                if (results == null || results.Count == 0)
                    return;

                isPlaying = true;
                playTimer.Start();
                btnPlay.Text = "⏸";
                btnPlay.BackColor = Color.DarkOrange;
            }
        }

        private void StopPlayback()
        {
            isPlaying = false;
            playTimer.Stop();
            btnPlay.Text = "▶";
            btnPlay.BackColor = Color.DodgerBlue;
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (currentIndex < results.Count - 1)
            {
                DisplayFrameAtIndex(currentIndex + 1);
            }
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            if (isPlaying && results != null && results.Count > 0)
            {
                if (currentIndex < results.Count - 1)
                {
                    DisplayFrameAtIndex(currentIndex + 1);
                }
                else
                {
                    StopPlayback();
                }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sel = comboBox1.SelectedItem?.ToString() ?? "1.0x";
                sel = sel.Replace("x", "");
                if (double.TryParse(sel, out double speed) && speed > 0)
                {
                    playbackSpeed = speed;
                    playTimer.Interval = (int)(1000.0 / (FRAMES_PER_SECOND * playbackSpeed));
                }
            }
            catch { }
        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (trackBar1.Value != currentIndex)
            {
                DisplayFrameAtIndex(trackBar1.Value);
            }
        }
    }
}
