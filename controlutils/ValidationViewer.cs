using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        // 조향 화살표 오버레이 상태
        private bool showArrows = false;
        private double overlayActualAngle = 0.0;
        private double overlayActualThrottle = 0.0;
        private double overlayPredAngle = 0.0;
        private double overlayPredThrottle = 0.0;

        public ValidationViewer()
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Paint += PictureBox1_Paint;

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
                showArrows = false;
                pictureBox1.Invalidate();
            }
        }

        public void ClearResults()
        {
            LoadResults(new List<SimpleDonkeyManager.ValidationResult>());
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

                // 조향 화살표 오버레이 갱신
                overlayActualAngle = item.ActualAngle;
                overlayActualThrottle = item.ActualThrottle;
                overlayPredAngle = item.PredAngle;
                overlayPredThrottle = item.PredThrottle;
                showArrows = true;
                pictureBox1.Invalidate();

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

        /// <summary>
        /// 이미지 위에 조향(Angle) 방향 화살표를 그립니다.
        /// - 시작점: 이미지 중앙 하단. 방향: Angle 0.0 → 위(↑), -1 → 좌 90°, +1 → 우 90°.
        /// - 길이: Throttle 0~1 을 최대 길이의 30%~100% 로 매핑.
        /// - 초록색: 실제 조향값 / 빨간색: AI 예측 조향값.
        /// </summary>
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (!showArrows)
                return;

            try
            {
                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int w = pictureBox1.ClientSize.Width;
                int h = pictureBox1.ClientSize.Height;
                if (w <= 0 || h <= 0)
                    return;

                // 시작점: 중앙 하단(약간의 여백)
                float originX = w / 2f;
                float originY = h - 8f;

                // 최대 길이: 바닥에서 맨 위까지 (위쪽 여백 약간)
                float maxLength = h - 16f;
                if (maxLength < 10f)
                    maxLength = h * 0.9f;

                // Angle/Throttle 가늠용 그리드(각도선 + 거리 호)를 먼저 그립니다.
                DrawGrid(g, originX, originY, maxLength);

                // 실제값(초록) → AI 예측(빨강) 순으로 그려 예측값이 위에 보이도록
                DrawAngleArrow(g, originX, originY, maxLength, overlayActualAngle, overlayActualThrottle,
                    Color.FromArgb(230, 30, 170, 60));
                DrawAngleArrow(g, originX, originY, maxLength, overlayPredAngle, overlayPredThrottle,
                    Color.FromArgb(230, 220, 40, 40));

                // 범례
                DrawLegend(g, w);
            }
            catch
            {
                // 오버레이 그리기 실패는 무시
            }
        }

        /// <summary>
        /// Angle(각도)과 Throttle(거리)을 대략 가늠할 수 있는 그리드를 그립니다.
        /// - 방사형 각도선: Angle -1.0 ~ +1.0 (좌우 90°)을 0.5 간격으로 표시.
        /// - 거리 호: Throttle 0 ~ 1 (반경의 30% ~ 100%)을 0.25 간격으로 표시.
        /// - 검정색 실선, 투명도 약 40%.
        /// </summary>
        private void DrawGrid(Graphics g, float originX, float originY, float maxLength)
        {
            // 알파 약 40% (0.4 * 255 ≈ 102)
            const int gridAlpha = 102;
            Color gridColor = Color.FromArgb(gridAlpha, 0, 0, 0);

            using (var gridPen = new Pen(gridColor, 1f))
            using (var labelBrush = new SolidBrush(gridColor))
            using (var labelFont = new Font("나눔고딕", 7.5f, FontStyle.Regular))
            {
                // ── 방사형 각도선 (Angle 눈금) ──
                // Angle 0.0(위) 기준, -1.0 ~ +1.0 을 0.5 간격으로
                double[] angleTicks = { -1.0, -0.5, 0.0, 0.5, 1.0 };
                foreach (double a in angleTicks)
                {
                    double rad = a * (Math.PI / 2.0);
                    float ex = originX + (float)(Math.Sin(rad) * maxLength);
                    float ey = originY - (float)(Math.Cos(rad) * maxLength);
                    g.DrawLine(gridPen, originX, originY, ex, ey);

                    // 각도선 끝에 Angle 값 라벨
                    string label = a.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
                    var sz = g.MeasureString(label, labelFont);
                    float lx = originX + (float)(Math.Sin(rad) * (maxLength + 8f)) - sz.Width / 2f;
                    float ly = originY - (float)(Math.Cos(rad) * (maxLength + 8f)) - sz.Height / 2f;
                    g.DrawString(label, labelFont, labelBrush, lx, ly);
                }

                // ── 거리 호 (Throttle 눈금) ──
                // Throttle 0~1 → 반경 30%~100%. 0.0, 0.25, 0.5, 0.75, 1.0 표시.
                double[] throttleTicks = { 0.0, 0.25, 0.5, 0.75, 1.0 };
                foreach (double t in throttleTicks)
                {
                    double ratio = 0.3 + 0.7 * t;
                    float radius = (float)(maxLength * ratio);
                    // 좌 90° ~ 우 90° (위쪽 반원) 호를 그립니다.
                    // GDI+ 각도: 0°=오른쪽, 시계방향. 180°(왼쪽)에서 180° 만큼 → 위쪽 반원.
                    var rect = new RectangleF(originX - radius, originY - radius, radius * 2f, radius * 2f);
                    g.DrawArc(gridPen, rect, 180f, 180f);

                    // 호 우측(오른쪽 90° 방향) 끝에 Throttle 값 라벨
                    string label = t.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                    var sz = g.MeasureString(label, labelFont);
                    float lx = originX + radius - sz.Width - 2f;
                    float ly = originY - sz.Height - 1f;
                    g.DrawString(label, labelFont, labelBrush, lx, ly);
                }
            }
        }

        /// <summary>
        /// 단일 조향 화살표를 그립니다.
        /// </summary>
        private void DrawAngleArrow(Graphics g, float originX, float originY, float maxLength,
            double angle, double throttle, Color color)
        {
            // Angle 범위 제한 (-1 ~ 1)
            double a = Math.Max(-1.0, Math.Min(1.0, angle));

            // Angle → 각도(라디안). 0 = 위(↑), -1 = 좌(-90°), +1 = 우(+90°)
            double rad = a * (Math.PI / 2.0);

            // Throttle → 길이 비율 (0~1 을 0.3~1.0 으로 매핑). 음수는 0 으로 처리.
            double t = Math.Max(0.0, Math.Min(1.0, throttle));
            double lengthRatio = 0.3 + 0.7 * t;
            float length = (float)(maxLength * lengthRatio);

            // 위 방향이 0° 이므로 위쪽(-Y)을 기준으로 회전
            float tipX = originX + (float)(Math.Sin(rad) * length);
            float tipY = originY - (float)(Math.Cos(rad) * length);

            Color outlineColor = Color.FromArgb(235, 0, 0, 0);
            const float outlineWidth = 10f;
            const float arrowWidth = 6f;

            using (var outlinePen = new Pen(outlineColor, outlineWidth))
            using (var pen = new Pen(color, arrowWidth))
            {
                outlinePen.StartCap = LineCap.Round;
                outlinePen.EndCap = LineCap.Round;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                g.DrawLine(outlinePen, originX, originY, tipX, tipY);
                g.DrawLine(pen, originX, originY, tipX, tipY);

                // 화살촉
                DrawArrowHead(g, color, outlineColor, originX, originY, tipX, tipY);
            }

            // 시작점 표시
            using (var outlineBrush = new SolidBrush(outlineColor))
            {
                g.FillEllipse(outlineBrush, originX - 6f, originY - 6f, 12f, 12f);
            }

            using (var dotBrush = new SolidBrush(color))
            {
                g.FillEllipse(dotBrush, originX - 4f, originY - 4f, 8f, 8f);
            }
        }

        private void DrawArrowHead(Graphics g, Color color, Color outlineColor, float x1, float y1, float x2, float y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            double len = Math.Sqrt(dx * dx + dy * dy);
            if (len < 1e-3)
                return;

            double ux = dx / len;
            double uy = dy / len;

            float headSize = 20f;
            double angleSpread = 25.0 * Math.PI / 180.0;

            // 화살촉 양쪽 점 계산
            double cos = Math.Cos(angleSpread);
            double sin = Math.Sin(angleSpread);

            double leftX = x2 - headSize * (ux * cos + uy * sin);
            double leftY = y2 - headSize * (uy * cos - ux * sin);
            double rightX = x2 - headSize * (ux * cos - uy * sin);
            double rightY = y2 - headSize * (uy * cos + ux * sin);

            PointF[] headPoints =
            {
                new PointF(x2, y2),
                new PointF((float)leftX, (float)leftY),
                new PointF((float)rightX, (float)rightY)
            };

            using (var outlineBrush = new SolidBrush(outlineColor))
            using (var outlinePen = new Pen(outlineColor, 3f))
            using (var brush = new SolidBrush(color))
            {
                using (GraphicsPath outlinePath = CreateExpandedArrowHeadPath(headPoints, 3.5f))
                {
                    g.FillPath(outlineBrush, outlinePath);
                }

                g.FillPolygon(brush, headPoints);
                g.DrawPolygon(outlinePen, headPoints);
            }
        }

        private GraphicsPath CreateExpandedArrowHeadPath(PointF[] points, float expansion)
        {
            float cx = 0f;
            float cy = 0f;
            foreach (var point in points)
            {
                cx += point.X;
                cy += point.Y;
            }
            cx /= points.Length;
            cy /= points.Length;

            PointF[] expanded = new PointF[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                float vx = points[i].X - cx;
                float vy = points[i].Y - cy;
                double len = Math.Sqrt(vx * vx + vy * vy);
                if (len < 1e-3)
                {
                    expanded[i] = points[i];
                    continue;
                }

                expanded[i] = new PointF(
                    points[i].X + (float)(vx / len * expansion),
                    points[i].Y + (float)(vy / len * expansion));
            }

            var path = new GraphicsPath();
            path.AddPolygon(expanded);
            return path;
        }

        private void DrawLegend(Graphics g, int width)
        {
            using (var font = new Font("나눔고딕", 9f, FontStyle.Bold))
            {
                const string actualText = "● 실제";
                const string predText = "● AI 예측";
                var actualColor = Color.FromArgb(30, 170, 60);
                var predColor = Color.FromArgb(220, 40, 40);

                float y = 6f;
                g.DrawString(actualText, font, new SolidBrush(actualColor), 8f, y);
                var sz = g.MeasureString(actualText, font);
                g.DrawString(predText, font, new SolidBrush(predColor), 8f, y + sz.Height + 2f);
            }
        }
    }
}
