using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SimpleDonkeyManager.controlutils
{
    /// <summary>
    /// ImageViewer를 전체 화면 크기로 크게 보여주는 Form입니다.
    /// 하단에 선택 프레임 제거 / 이전 삭제 되돌리기 / 미선택 프레임 필터 버튼을 배치하고
    /// 실제 필터링 동작(ImageManager 기반)을 수행합니다.
    /// </summary>
    public class ImageLargeViewForm : Form
    {
        // ── 컨트롤 ──────────────────────────────────────────────
        private ImageViewer imageViewer;
        private Panel pnlBottom;
        private Button btnRemoveSelectedFrame;
        private Button btnUndoRemove;
        private Button btnFilterUnselected;
        private ToolTip toolTip;

        // ── 데이터 / 연동 ───────────────────────────────────────
        private SimpleDonkeyManager.ImageManager imageManager;
        private SimpleDonkeyManager.DataFilterControl ownerControl;
        private SimpleDonkeyManager.Logger logger;

        /// <summary>
        /// ImageManager와 소유 DataFilterControl을 받아 크게 보기 창을 구성합니다.
        /// </summary>
        public ImageLargeViewForm(SimpleDonkeyManager.ImageManager manager, SimpleDonkeyManager.DataFilterControl owner = null, SimpleDonkeyManager.Logger log = null)
        {
            imageManager = manager;
            ownerControl = owner;
            logger = log;

            BuildUI();
            InitializeTooltips();
            WireEvents();

            if (imageManager != null)
                imageViewer.SetImageManager(imageManager);

            UpdateUndoButtonState();
        }

        // ────────────────────────────────────────────────────────────
        // UI 구성
        // ────────────────────────────────────────────────────────────
        private void BuildUI()
        {
            Text = "이미지 크게 보기";
            Size = new Size(1200, 900);
            MinimumSize = new Size(640, 480);
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            BackColor = Color.FromArgb(30, 30, 30);
            KeyPreview = true;

            // ── 임베드 ImageViewer ───────────────────────────────
            imageViewer = new ImageViewer
            {
                Dock = DockStyle.Fill,
                AutoSize = false,
                Visible = true
            };
            if (logger != null)
                imageViewer.SetLogger(logger);

            // ── 하단 버튼 패널 ───────────────────────────────────
            pnlBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 56,
                BackColor = Color.FromArgb(45, 45, 45),
                Padding = new Padding(8, 8, 8, 8)
            };

            btnRemoveSelectedFrame = MakeBottomButton("✖ 선택 프레임 제거", Color.IndianRed, Color.MistyRose);
            btnUndoRemove = MakeBottomButton("↺ 이전 삭제 되돌리기", Color.SeaGreen, Color.Honeydew);
            btnUndoRemove.Enabled = false;
            btnFilterUnselected = MakeBottomButton("✂ 미선택 프레임 필터", Color.RoyalBlue, Color.AliceBlue);

            // 가로 균등 배치를 위한 TableLayoutPanel
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                BackColor = Color.Transparent
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));

            btnRemoveSelectedFrame.Dock = DockStyle.Fill;
            btnUndoRemove.Dock = DockStyle.Fill;
            btnFilterUnselected.Dock = DockStyle.Fill;
            btnRemoveSelectedFrame.Margin = new Padding(4);
            btnUndoRemove.Margin = new Padding(4);
            btnFilterUnselected.Margin = new Padding(4);

            layout.Controls.Add(btnRemoveSelectedFrame, 0, 0);
            layout.Controls.Add(btnUndoRemove, 1, 0);
            layout.Controls.Add(btnFilterUnselected, 2, 0);

            pnlBottom.Controls.Add(layout);

            Controls.Add(imageViewer);
            Controls.Add(pnlBottom);
        }

        private Button MakeBottomButton(string text, Color fore, Color hover)
        {
            var btn = new Button
            {
                Text = text,
                BackColor = Color.White,
                ForeColor = fore,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("나눔고딕", 10F, FontStyle.Bold),
                UseVisualStyleBackColor = false
            };
            btn.FlatAppearance.BorderColor = fore;
            btn.FlatAppearance.MouseOverBackColor = hover;
            return btn;
        }

        private void InitializeTooltips()
        {
            toolTip = new ToolTip();
            toolTip.SetToolTip(btnRemoveSelectedFrame, "현재 표시 중인 프레임을 제거합니다.");
            toolTip.SetToolTip(btnUndoRemove, "직전에 수행한 삭제를 한 번 되돌립니다.");
            toolTip.SetToolTip(btnFilterUnselected, "타임라인에서 선택한 구간의 프레임만 남기고 나머지를 필터링합니다.");
        }

        private void WireEvents()
        {
            btnRemoveSelectedFrame.Click += BtnRemoveSelectedFrame_Click;
            btnUndoRemove.Click += BtnUndoRemove_Click;
            btnFilterUnselected.Click += BtnFilterUnselected_Click;
        }

        // ────────────────────────────────────────────────────────────
        // 필터링 동작
        // ────────────────────────────────────────────────────────────

        /// <summary>
        /// 현재 ImageViewer에서 표시 중인 프레임을 제거합니다.
        /// </summary>
        private void BtnRemoveSelectedFrame_Click(object sender, EventArgs e)
        {
            try
            {
                if (imageManager == null)
                {
                    MessageBox.Show("로드된 데이터가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var frames = imageViewer.FrameDataList;
                int index = imageViewer.CurrentFrameIndex;

                if (frames == null || frames.Count == 0 || index < 0 || index >= frames.Count)
                {
                    MessageBox.Show("제거할 프레임이 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int frameNumber = frames[index].FrameNumber;
                bool removed = imageManager.RemoveFrame(frameNumber);
                if (!removed)
                {
                    LogWarning($"프레임 {frameNumber} 제거 실패 또는 대상 파일 없음");
                }

                ReloadAfterChange();
                LogInfo($"프레임 {frameNumber} 제거 완료");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"프레임 제거 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"프레임 제거 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 직전 삭제를 한 번 되돌립니다.
        /// </summary>
        private void BtnUndoRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (imageManager == null || !imageManager.CanUndoLastRemove)
                {
                    MessageBox.Show("되돌릴 직전 삭제 내역이 없습니다.", "삭제 되돌리기", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateUndoButtonState();
                    return;
                }

                bool restored = imageManager.UndoLastRemove();
                if (!restored)
                {
                    MessageBox.Show("되돌릴 직전 삭제 내역이 없습니다.", "삭제 되돌리기", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateUndoButtonState();
                    return;
                }

                ReloadAfterChange();
                LogInfo("직전 삭제 되돌리기 완료");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"삭제 되돌리기 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"삭제 되돌리기 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 선택된 구간의 프레임만 남기고 나머지를 필터링합니다.
        /// </summary>
        private void BtnFilterUnselected_Click(object sender, EventArgs e)
        {
            try
            {
                if (imageManager == null)
                {
                    MessageBox.Show("로드된 데이터가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!imageViewer.HasRange)
                {
                    MessageBox.Show("먼저 타임라인에서 남길 구간을 선택해주세요.\n(더블클릭 후 드래그하여 구간을 지정합니다.)",
                        "구간 선택 필요", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int rangeStart = imageViewer.SelectedRangeStart;
                int rangeEnd = imageViewer.SelectedRangeEnd;
                var frames = imageViewer.FrameDataList;

                if (frames == null || rangeStart < 0 || rangeEnd < 0 ||
                    rangeStart >= frames.Count || rangeEnd >= frames.Count)
                {
                    MessageBox.Show("선택된 구간이 올바르지 않습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var keepNumbers = new HashSet<int>();
                for (int i = rangeStart; i <= rangeEnd; i++)
                {
                    if (frames[i] != null)
                        keepNumbers.Add(frames[i].FrameNumber);
                }

                var removeNumbers = frames
                    .Where(f => f != null && !keepNumbers.Contains(f.FrameNumber))
                    .Select(f => f.FrameNumber)
                    .ToList();

                if (removeNumbers.Count == 0)
                {
                    MessageBox.Show("제거할 미선택 프레임이 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int keepCount = frames.Count - removeNumbers.Count;
                var confirm = MessageBox.Show(
                    $"선택된 구간({keepCount:N0}개)만 남기고 나머지 {removeNumbers.Count:N0}개 프레임을 필터링합니다.\n계속하시겠습니까?",
                    "미선택 프레임 필터", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes)
                    return;

                int actuallyRemoved = imageManager.RemoveFrames(removeNumbers);

                ReloadAfterChange();
                LogInfo($"미선택 프레임 필터 완료: {actuallyRemoved}개 제거");

                MessageBox.Show($"미선택 프레임 {actuallyRemoved:N0}개가 필터링되었습니다.",
                    "미선택 프레임 필터", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"미선택 프레임 필터 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"미선택 프레임 필터 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// ImageManager 변경 후 뷰어를 새로고침하고, 소유 컨트롤도 동기화합니다.
        /// </summary>
        private void ReloadAfterChange()
        {
            if (imageManager != null)
                imageViewer.SetImageManager(imageManager);

            UpdateUndoButtonState();

            // 메인 화면 DataFilterControl도 동기화
            ownerControl?.RefreshAfterExternalChange();
        }

        private void UpdateUndoButtonState()
        {
            if (btnUndoRemove != null)
                btnUndoRemove.Enabled = imageManager != null && imageManager.CanUndoLastRemove;
        }

        // ────────────────────────────────────────────────────────────
        // 로깅
        // ────────────────────────────────────────────────────────────
        private void LogInfo(string message) => logger?.AppendLog($"[크게보기] {message}");
        private void LogWarning(string message) => logger?.AppendLog($"[크게보기 경고] {message}");
    }
}
