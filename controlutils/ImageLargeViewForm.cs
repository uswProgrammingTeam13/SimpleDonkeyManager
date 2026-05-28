using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SimpleDonkeyManager.controlutils
{
    /// <summary>
    /// 현재 이미지를 별도 창에서 크게 보여주는 Form입니다.
    /// 재생/정지/이전/다음 프레임 이동 및 배속 조절 기능을 포함합니다.
    /// </summary>
    public class ImageLargeViewForm : Form
    {
        // ── 레이아웃 컨트롤 ──────────────────────────────────────
        private PictureBox pictureBox;
        private Panel pnlToolbar;
        private TrackBar trackBar;
        private Panel pnlInfo;

        // ── 툴바 버튼 / 컨트롤 ──────────────────────────────────
        private Button btnFirst;
        private Button btnPrev;
        private Button btnPlayPause;
        private Button btnNext;
        private Button btnLast;
        private Label lblSpeed;
        private ComboBox cmbSpeed;
        private Label lblFrame;

        // ── 하단 정보 라벨 ───────────────────────────────────────
        private Label lblAngle;
        private Label lblThrottle;
        private Label lblImageName;

        // ── 데이터 ───────────────────────────────────────────────
        private List<FrameData> frameDataList;
        private int currentIndex = 0;
        private System.Windows.Forms.Timer playTimer;
        private bool isPlaying = false;
        private double playbackSpeed = 1.0;
        private const int FPS = 20;

        // ── 툴팁 ─────────────────────────────────────────────────
        private ToolTip toolTip;

        public ImageLargeViewForm(List<FrameData> frames, int startIndex = 0)
        {
            frameDataList = frames ?? new List<FrameData>();
            currentIndex = Math.Max(0, Math.Min(startIndex, frameDataList.Count - 1));

            BuildUI();
            InitializeTooltips();
            WireEvents();

            playTimer = new System.Windows.Forms.Timer { Interval = (int)(1000.0 / FPS) };
            playTimer.Tick += PlayTimer_Tick;

            ShowFrame(currentIndex);
        }

        // ────────────────────────────────────────────────────────────
        // UI 구성
        // ────────────────────────────────────────────────────────────
        private void BuildUI()
        {
            Text = "이미지 크게 보기";
            Size = new Size(960, 780);
            MinimumSize = new Size(480, 400);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(20, 20, 20);
            KeyPreview = true;

            // ── 메인 이미지 ──────────────────────────────────────
            pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Black
            };

            // ── 툴바 패널 ────────────────────────────────────────
            pnlToolbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 52,
                BackColor = Color.FromArgb(40, 40, 40),
                Padding = new Padding(6, 4, 6, 4)
            };

            btnFirst = MakeToolbarButton("⏮", Color.DodgerBlue);
            btnFirst.Location = new Point(8, 8);

            btnPrev = MakeToolbarButton("◀", Color.DodgerBlue);
            btnPrev.Location = new Point(66, 8);

            btnPlayPause = MakeToolbarButton("▶", Color.SeaGreen);
            btnPlayPause.Location = new Point(124, 8);

            btnNext = MakeToolbarButton("▶|", Color.DodgerBlue);
            btnNext.Location = new Point(182, 8);

            btnLast = MakeToolbarButton("⏭", Color.DodgerBlue);
            btnLast.Location = new Point(240, 8);

            lblSpeed = new Label
            {
                Text = "배속:",
                ForeColor = Color.White,
                Font = new Font("나눔고딕", 10F, FontStyle.Bold),
                Location = new Point(316, 16),
                AutoSize = true
            };

            cmbSpeed = new ComboBox
            {
                Font = new Font("나눔고딕", 10F, FontStyle.Bold),
                Location = new Point(366, 12),
                Size = new Size(80, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbSpeed.Items.AddRange(new object[] { "0.25x", "0.5x", "1.0x", "2.0x", "4.0x" });
            cmbSpeed.SelectedIndex = 2;

            lblFrame = new Label
            {
                Text = "0000 / 0",
                ForeColor = Color.White,
                Font = new Font("나눔고딕", 10F, FontStyle.Bold),
                Location = new Point(466, 16),
                AutoSize = true
            };

            pnlToolbar.Controls.AddRange(new Control[]
            {
                btnFirst, btnPrev, btnPlayPause, btnNext, btnLast,
                lblSpeed, cmbSpeed, lblFrame
            });

            // ── 트랙바 ───────────────────────────────────────────
            trackBar = new TrackBar
            {
                Dock = DockStyle.Top,
                Height = 36,
                Minimum = 0,
                Maximum = Math.Max(0, frameDataList.Count - 1),
                TickStyle = TickStyle.None,
                BackColor = Color.FromArgb(30, 30, 30)
            };

            // ── 하단 정보 패널 ───────────────────────────────────
            pnlInfo = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 56,
                BackColor = Color.FromArgb(30, 30, 30),
                Padding = new Padding(10, 4, 10, 4)
            };

            lblImageName = new Label
            {
                ForeColor = Color.Silver,
                Font = new Font("나눔고딕", 9F, FontStyle.Regular),
                Location = new Point(10, 6),
                AutoSize = true,
                Text = ""
            };

            lblAngle = new Label
            {
                ForeColor = Color.LightSkyBlue,
                Font = new Font("나눔고딕", 11F, FontStyle.Bold),
                Location = new Point(10, 28),
                AutoSize = true,
                Text = "Angle: -"
            };

            lblThrottle = new Label
            {
                ForeColor = Color.LightGreen,
                Font = new Font("나눔고딕", 11F, FontStyle.Bold),
                Location = new Point(200, 28),
                AutoSize = true,
                Text = "Throttle: -"
            };

            pnlInfo.Controls.AddRange(new Control[] { lblImageName, lblAngle, lblThrottle });

            // ── 컨트롤 배치 (순서 중요: Top → Fill → Bottom) ────
            Controls.Add(pictureBox);   // Fill
            Controls.Add(trackBar);     // Top (이미 pnlToolbar 아래)
            Controls.Add(pnlToolbar);   // Top (가장 위)
            Controls.Add(pnlInfo);      // Bottom
        }

        private static Button MakeToolbarButton(string text, Color backColor)
        {
            return new Button
            {
                Text = text,
                Size = new Size(50, 36),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("나눔고딕", 11F, FontStyle.Bold),
                BackColor = backColor,
                ForeColor = Color.White,
                UseVisualStyleBackColor = false
            };
        }

        // ────────────────────────────────────────────────────────────
        // 툴팁
        // ────────────────────────────────────────────────────────────
        private void InitializeTooltips()
        {
            toolTip = new ToolTip { AutoPopDelay = 8000, InitialDelay = 400, ReshowDelay = 200, ShowAlways = true };
            toolTip.SetToolTip(btnFirst,     "첫 번째 프레임으로 이동합니다. (Home)");
            toolTip.SetToolTip(btnPrev,      "이전 프레임으로 이동합니다. (← 방향키)");
            toolTip.SetToolTip(btnPlayPause, "재생 / 일시정지를 전환합니다. (Space)");
            toolTip.SetToolTip(btnNext,      "다음 프레임으로 이동합니다. (→ 방향키)");
            toolTip.SetToolTip(btnLast,      "마지막 프레임으로 이동합니다. (End)");
            toolTip.SetToolTip(cmbSpeed,     "재생 배속을 선택합니다.");
            toolTip.SetToolTip(trackBar,     "슬라이더를 드래그하여 원하는 프레임으로 이동합니다.");
            toolTip.SetToolTip(pictureBox,   "현재 프레임 이미지입니다. ESC 키로 창을 닫습니다.");
        }

        // ────────────────────────────────────────────────────────────
        // 이벤트 연결
        // ────────────────────────────────────────────────────────────
        private void WireEvents()
        {
            btnFirst.Click     += (s, e) => MoveToFrame(0);
            btnPrev.Click      += (s, e) => MoveToFrame(currentIndex - 1);
            btnPlayPause.Click += BtnPlayPause_Click;
            btnNext.Click      += (s, e) => MoveToFrame(currentIndex + 1);
            btnLast.Click      += (s, e) => MoveToFrame(frameDataList.Count - 1);

            cmbSpeed.SelectedIndexChanged += CmbSpeed_SelectedIndexChanged;
            trackBar.ValueChanged         += TrackBar_ValueChanged;

            KeyDown += ImageLargeViewForm_KeyDown;
            FormClosed += (s, e) => { playTimer?.Stop(); playTimer?.Dispose(); };
        }

        // ────────────────────────────────────────────────────────────
        // 외부에서 프레임 데이터 전달 (ImageViewer와 연동)
        // ────────────────────────────────────────────────────────────
        public void SyncFrame(int index)
        {
            if (IsDisposed || !Visible) return;
            currentIndex = Math.Max(0, Math.Min(index, frameDataList.Count - 1));
            ShowFrame(currentIndex);
        }

        // ────────────────────────────────────────────────────────────
        // 프레임 표시
        // ────────────────────────────────────────────────────────────
        private void MoveToFrame(int index)
        {
            if (frameDataList == null || frameDataList.Count == 0) return;
            index = Math.Max(0, Math.Min(index, frameDataList.Count - 1));
            currentIndex = index;
            ShowFrame(currentIndex);
        }

        private void ShowFrame(int index)
        {
            if (frameDataList == null || frameDataList.Count == 0) return;
            index = Math.Max(0, Math.Min(index, frameDataList.Count - 1));
            currentIndex = index;

            var fd = frameDataList[index];
            if (fd == null || string.IsNullOrEmpty(fd.ImagePath) || !File.Exists(fd.ImagePath))
                return;

            try
            {
                var oldImg = pictureBox.Image;
                using (var stream = new FileStream(fd.ImagePath, FileMode.Open, FileAccess.Read))
                    pictureBox.Image = Image.FromStream(stream);
                oldImg?.Dispose();
            }
            catch { return; }

            // 정보 업데이트
            lblFrame.Text    = $"{fd.FrameNumber:0000} / {frameDataList.Count}";
            lblImageName.Text = fd.ImageFileName ?? "";
            lblAngle.Text    = $"Angle: {fd.GetAngle():F3} rad";
            lblThrottle.Text = $"Throttle: {fd.GetThrottle():F3}";

            // 트랙바 동기화 (이벤트 루프 방지)
            if (trackBar.Maximum != frameDataList.Count - 1)
                trackBar.Maximum = Math.Max(0, frameDataList.Count - 1);
            if (trackBar.Value != index)
                trackBar.Value = index;
        }

        // ────────────────────────────────────────────────────────────
        // 재생 / 정지
        // ────────────────────────────────────────────────────────────
        private void BtnPlayPause_Click(object sender, EventArgs e)
        {
            if (frameDataList == null || frameDataList.Count == 0) return;

            if (isPlaying)
            {
                isPlaying = false;
                playTimer.Stop();
                btnPlayPause.Text      = "▶";
                btnPlayPause.BackColor = Color.SeaGreen;
            }
            else
            {
                isPlaying = true;
                playTimer.Start();
                btnPlayPause.Text      = "⏸";
                btnPlayPause.BackColor = Color.DarkOrange;
            }
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            if (!isPlaying) return;
            if (currentIndex < frameDataList.Count - 1)
            {
                MoveToFrame(currentIndex + 1);
            }
            else
            {
                isPlaying = false;
                playTimer.Stop();
                btnPlayPause.Text      = "▶";
                btnPlayPause.BackColor = Color.SeaGreen;
            }
        }

        // ────────────────────────────────────────────────────────────
        // 배속
        // ────────────────────────────────────────────────────────────
        private void CmbSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            string raw = (cmbSpeed.SelectedItem?.ToString() ?? "1.0x").Replace("x", "");
            if (double.TryParse(raw, out double spd) && spd > 0)
            {
                playbackSpeed = spd;
                playTimer.Interval = (int)(1000.0 / (FPS * playbackSpeed));
            }
        }

        // ────────────────────────────────────────────────────────────
        // 트랙바
        // ────────────────────────────────────────────────────────────
        private bool trackBarChanging = false;
        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (trackBarChanging) return;
            trackBarChanging = true;
            try { MoveToFrame(trackBar.Value); }
            finally { trackBarChanging = false; }
        }

        // ────────────────────────────────────────────────────────────
        // 키보드 단축키
        // ────────────────────────────────────────────────────────────
        private void ImageLargeViewForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:  Close();                          break;
                case Keys.Space:   BtnPlayPause_Click(null, null);   break;
                case Keys.Left:    MoveToFrame(currentIndex - 1);    break;
                case Keys.Right:   MoveToFrame(currentIndex + 1);    break;
                case Keys.Home:    MoveToFrame(0);                   break;
                case Keys.End:     MoveToFrame(frameDataList.Count - 1); break;
            }
        }
    }
}
