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
        private const int FRAMES_PER_SECOND = 60;
        private double frameAdvanceAccumulator = 0.0;
        // ImageList 동기화 (일시정지/정지 시 현재 프레임 선택). 재귀 방지 플래그 포함.
        private SimpleDonkeyManager.controlutils.ImageList linkedImageList;
        private bool isSyncingSelection = false;
        private SimpleDonkeyManager.Logger logger;

        // 제거 예정(pending) 프레임 번호 집합. 빨간 오버레이/타임라인 마커/재생 스킵에 사용.
        private HashSet<int> pendingRemovedNumbers = new HashSet<int>();

        /// <summary>
        /// 제거 예정으로 표시된 프레임을 우클릭하여 취소를 요청했을 때 발생합니다. 인자는 프레임 번호입니다.
        /// </summary>
        public event EventHandler<int> PendingRemoveCancelRequested;

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
            frameTimeline.FramePreviewed += FrameTimeline_FramePreviewed;
            frameTimeline.FrameAnchored += FrameTimeline_FrameAnchored;
            frameTimeline.RangeSelected += FrameTimeline_RangeSelected;
            frameTimeline.SelectionCleared += FrameTimeline_SelectionCleared;
            frameTimeline.PendingRemoveCancelRequested += FrameTimeline_PendingRemoveCancelRequested;
            btnLargeView.Click += BtnLargeView_Click;

            // 현재 프레임이 제거 예정일 때 빨간 테두리+오버레이를 그리기 위한 Paint 핸들러
            pictureBox1.Paint += PictureBox1_Paint;

            InitializeTooltips();

            // 리사이즈 이벤트 추가 - 썸네일 적응형 표시
            this.Resize += ImageViewer_Resize;
            pnlLeftThumbnail.Resize += PnlThumbnail_Resize;
            pnlRightThumbnail.Resize += PnlThumbnail_Resize;

            // 60fps 고정 타이머. 배속은 틱마다 진행할 프레임 수로 처리.
            playTimer = new System.Windows.Forms.Timer();
            playTimer.Interval = (int)(1000.0 / FRAMES_PER_SECOND);
            playTimer.Tick += PlayTimer_Tick;

            UpdateCurrentFrameDisplay();
            UpdateSelectionDisplay();

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

        private void InitializeTooltips()
        {
            var toolTip = new ToolTip { AutoPopDelay = 8000, InitialDelay = 400, ReshowDelay = 200, ShowAlways = true };
            toolTip.SetToolTip(button1, "이전 프레임으로 이동합니다.");
            toolTip.SetToolTip(button2, "다음 프레임으로 이동합니다.");
            toolTip.SetToolTip(button3, "이미지를 자동으로 재생합니다. (재생 중에는 일시정지로 변경)");
            toolTip.SetToolTip(button4, "재생을 정지하고 첫 프레임으로 돌아갑니다. 재생 중이 아니면 구간 선택을 해제합니다.");
            toolTip.SetToolTip(frameTimeline, "프레임 타임라인입니다.\n클릭/드래그: 프레임 확인 / 더블클릭: 프레임 선택\n더블클릭 후 드래그: 구간 선택 / 휠 클릭: 현재 지점 선택 / 우클릭: 선택 취소");
            toolTip.SetToolTip(comboBox1, "재생 배속을 선택합니다. (0.25 ~ 4.0배)");
            toolTip.SetToolTip(lstJSONSummary, "현재 프레임의 Angle / Throttle 등 JSON 데이터를 표시합니다.");
            toolTip.SetToolTip(btnLargeView, "현재 이미지를 별도 창에서 크게 봅니다. (단축키: ESC로 닫기)");
            toolTip.SetToolTip(pictureBox1, "현재 선택된 프레임 이미지입니다.");
            toolTip.SetToolTip(pictureBox2, "이전 프레임 미리보기 이미지입니다.");
            toolTip.SetToolTip(pictureBox3, "다음 프레임 미리보기 이미지입니다.");
        }

        private void BtnLargeView_Click(object sender, EventArgs e)
        {
            try
            {
                if (frameDataList == null || frameDataList.Count == 0)
                {
                    MessageBox.Show("표시할 이미지가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 부모 DataFilterControl을 찾아 변경 동기화에 사용
                SimpleDonkeyManager.DataFilterControl ownerControl = null;
                Control parent = this.Parent;
                while (parent != null)
                {
                    if (parent is SimpleDonkeyManager.DataFilterControl dfc)
                    {
                        ownerControl = dfc;
                        break;
                    }
                    parent = parent.Parent;
                }

                var form = new ImageLargeViewForm(imageManager, ownerControl, logger);
                form.Show(this.FindForm());
            }
            catch (Exception ex)
            {
                LogWarning($"크게 보기 오류: {ex.Message}");
            }
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

                // 프레임 타임라인 설정
                if (frameTimeline != null && frameDataList.Count > 0)
                {
                    frameTimeline.FrameCount = frameDataList.Count;
                    frameTimeline.CurrentIndex = 0;
                    currentFrameIndex = 0;
                    UpdateCurrentFrameDisplay();
                    UpdateSelectionDisplay();

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
                else if (frameTimeline != null)
                {
                    frameTimeline.FrameCount = 0;
                    frameTimeline.CurrentIndex = 0;
                }

                // 프레임 목록 갱신 후 제거 마커(제거 예정/이미 삭제)를 다시 동기화
                SyncTimelineRemovalMarks();

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

                // 제거 예정 오버레이 갱신
                if (pictureBox1 != null)
                    pictureBox1.Invalidate();

                if (frameTimeline != null)
                {
                    try
                    {
                        frameTimeline.CurrentIndex = index;
                    }
                    catch
                    {
                        // 타임라인 값 설정 실패 시 계속
                    }
                }
            }
            catch (Exception ex)
            {
                LogWarning($"프레임 표시 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 현재 프레임이 제거 예정일 때 pictureBox1 위에 빨간 테두리와 반투명 오버레이를 그립니다.
        /// </summary>
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (!IsPendingIndex(currentFrameIndex))
                    return;

                var g = e.Graphics;
                Rectangle rect = pictureBox1.ClientRectangle;
                if (rect.Width <= 2 || rect.Height <= 2)
                    return;

                // 반투명 빨간 오버레이
                using (var overlay = new SolidBrush(Color.FromArgb(70, 220, 40, 40)))
                {
                    g.FillRectangle(overlay, rect);
                }

                // 빨간 테두리 (안쪽으로 들여 그려 잘리지 않게 함)
                var borderRect = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 5, rect.Height - 5);
                using (var borderPen = new Pen(Color.FromArgb(220, 40, 40), 4f))
                {
                    g.DrawRectangle(borderPen, borderRect);
                }
            }
            catch (Exception ex)
            {
                LogWarning($"제거 예정 오버레이 그리기 예외: {ex.Message}");
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
                    int target = FindNextNonPending(currentFrameIndex, -1);
                    if (target < 0)
                        return; // 더 이전에 표시 가능한(제거 예정 아닌) 프레임 없음
                    DisplayFrameAtIndex(target);
                    UpdateSelectionDisplay();
                    if (!isPlaying) SyncImageListSelection();
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
                    int target = FindNextNonPending(currentFrameIndex, 1);
                    if (target < 0)
                        return; // 더 이후에 표시 가능한(제거 예정 아닌) 프레임 없음
                    DisplayFrameAtIndex(target);
                    UpdateSelectionDisplay();
                    if (!isPlaying) SyncImageListSelection();
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

                if (!isPlaying)
                {
                    StartPlayback();
                }
                else
                {
                    PausePlayback();
                }
            }
            catch (Exception ex)
            {
                LogWarning($"재생/일시정지 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 재생을 시작합니다. 버튼 텍스트를 '일시정지'로 변경합니다.
        /// 구간이 선택되어 있으면 구간 시작에서 시작합니다.
        /// </summary>
        private void StartPlayback()
        {
            if (playTimer == null)
                return;

            // 구간이 선택되어 있고 현재 위치가 구간 밖이면 구간 시작으로 이동
            if (frameTimeline != null && frameTimeline.HasRange)
            {
                int start = Math.Min(frameTimeline.RangeStart, frameTimeline.RangeEnd);
                int end = Math.Max(frameTimeline.RangeStart, frameTimeline.RangeEnd);
                if (currentFrameIndex < start || currentFrameIndex >= end)
                {
                    DisplayFrameAtIndex(start);
                }
            }

            isPlaying = true;
            frameAdvanceAccumulator = 0.0;
            playTimer.Start();
            if (button3 != null) button3.Text = "❚❚ 일시정지";
            LogInfo("재생 시작");
        }

        /// <summary>
        /// 재생을 일시정지합니다. 현재 프레임에서 멈추고 버튼 텍스트를 '재생'으로 변경합니다.
        /// 일시정지 시 ImageList에서 현재 프레임을 선택합니다.
        /// </summary>
        private void PausePlayback()
        {
            isPlaying = false;
            if (playTimer != null) playTimer.Stop();
            if (button3 != null) button3.Text = "▶ 재생";
            SyncImageListSelection();
            LogInfo("일시정지");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (isPlaying)
                {
                    // 재생 중 정지: 재생을 멈추고 첫 프레임으로 복귀
                    isPlaying = false;
                    if (playTimer != null) playTimer.Stop();
                    if (button3 != null) button3.Text = "▶ 재생";
                    currentFrameIndex = 0;
                    DisplayFrameAtIndex(0);
                    SyncImageListSelection();
                    LogInfo("재생 정지 및 첫 프레임 복귀");
                }
                else
                {
                    // 재생 중이 아닐 때 정지: 구간 선택 해제 후 첫 프레임으로 복귀
                    if (frameTimeline != null)
                    {
                        frameTimeline.ClearSelection();
                    }
                    currentFrameIndex = 0;
                    DisplayFrameAtIndex(0);
                    UpdateSelectionDisplay();
                    SyncImageListSelection();
                    LogInfo("구간 선택 해제 및 첫 프레임 복귀");
                }
            }
            catch (Exception ex)
            {
                LogWarning($"정지 예외: {ex.Message}");
            }
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!isPlaying || frameDataList == null || frameDataList.Count == 0)
                    return;

                // 배속만큼 인덱스를 진행 (60fps 고정, 분수 배속은 누적으로 처리)
                frameAdvanceAccumulator += playbackSpeed;
                int advance = (int)frameAdvanceAccumulator;
                if (advance < 1)
                    return;
                frameAdvanceAccumulator -= advance;

                int nextIndex = currentFrameIndex + advance;

                // 제거 예정 프레임은 재생 시 건너뜀: nextIndex가 pending이면 다음 non-pending으로 전진
                if (IsPendingIndex(nextIndex))
                {
                    int skipped = FindNextNonPending(nextIndex, 1);
                    if (skipped < 0)
                    {
                        // 이후 표시 가능한 프레임이 없으면 마지막 non-pending에서 정지
                        int lastVisible = FindNextNonPending(nextIndex, -1);
                        if (lastVisible >= 0)
                            DisplayFrameAtIndex(lastVisible);
                        StopPlaybackAtEnd();
                        return;
                    }
                    nextIndex = skipped;
                }

                // 구간 재생: 구간 끝에 도달하면 정지
                if (frameTimeline != null && frameTimeline.HasRange)
                {
                    int end = Math.Max(frameTimeline.RangeStart, frameTimeline.RangeEnd);
                    if (nextIndex >= end)
                    {
                        DisplayFrameAtIndex(end);
                        StopPlaybackAtEnd();
                        return;
                    }
                }

                // 전체 재생: 마지막 프레임에 도달하면 정지
                if (nextIndex >= frameDataList.Count - 1)
                {
                    DisplayFrameAtIndex(frameDataList.Count - 1);
                    StopPlaybackAtEnd();
                    return;
                }

                DisplayFrameAtIndex(nextIndex);
            }
            catch (Exception ex)
            {
                LogWarning($"재생 타이머 예외: {ex.Message}");
                isPlaying = false;
                if (playTimer != null)
                    playTimer.Stop();
                if (button3 != null) button3.Text = "▶ 재생";
            }
        }

        /// <summary>
        /// 재생이 끝(또는 구간 끝)에 도달하여 자동 정지될 때 호출됩니다.
        /// </summary>
        private void StopPlaybackAtEnd()
        {
            isPlaying = false;
            if (playTimer != null) playTimer.Stop();
            if (button3 != null) button3.Text = "▶ 재생";
            frameAdvanceAccumulator = 0.0;
            SyncImageListSelection();
            LogInfo("재생 종료");
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
                        // 60fps는 고정, 배속은 틱당 진행 프레임 수로 반영되므로 Interval은 그대로 둠.
                        LogInfo($"재생 속도 변경: {speed}x");
                    }
                }
            }
            catch (Exception ex)
            {
                LogWarning($"재생 속도 변경 예외: {ex.Message}");
            }
        }

        // ────────────────────────────────────────────────────────────
        // 프레임 타임라인 이벤트 핸들러
        // ────────────────────────────────────────────────────────────

        private void FrameTimeline_FramePreviewed(object sender, int index)
        {
            try
            {
                if (isPlaying)
                    return; // 재생 중 타임라인 조작 무시
                if (index >= 0 && index < frameDataList.Count)
                {
                    DisplayFrameAtIndex(index);
                    UpdateSelectionDisplay();
                }
            }
            catch (Exception ex)
            {
                LogWarning($"타임라인 프레임 확인 예외: {ex.Message}");
            }
        }

        private void FrameTimeline_FrameAnchored(object sender, int index)
        {
            try
            {
                if (index >= 0 && index < frameDataList.Count)
                {
                    DisplayFrameAtIndex(index);
                    UpdateSelectionDisplay();
                    if (!isPlaying) SyncImageListSelection();
                }
            }
            catch (Exception ex)
            {
                LogWarning($"타임라인 프레임 선택 예외: {ex.Message}");
            }
        }

        private void FrameTimeline_RangeSelected(object sender, RangeSelectedEventArgs e)
        {
            try
            {
                UpdateSelectionDisplay();
                LogInfo($"구간 선택: {e.StartIndex} ~ {e.EndIndex}");
            }
            catch (Exception ex)
            {
                LogWarning($"타임라인 구간 선택 예외: {ex.Message}");
            }
        }

        private void FrameTimeline_SelectionCleared(object sender, EventArgs e)
        {
            try
            {
                UpdateSelectionDisplay();
                LogInfo("타임라인 선택/구간 취소");
            }
            catch (Exception ex)
            {
                LogWarning($"타임라인 선택 취소 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 타임라인에서 제거 예정 프레임을 우클릭하여 취소를 요청했을 때, 부모(DataFilterControl)로 전달합니다.
        /// </summary>
        private void FrameTimeline_PendingRemoveCancelRequested(object sender, int index)
        {
            try
            {
                if (frameDataList == null || index < 0 || index >= frameDataList.Count)
                    return;
                var fd = frameDataList[index];
                if (fd == null)
                    return;
                PendingRemoveCancelRequested?.Invoke(this, fd.FrameNumber);
            }
            catch (Exception ex)
            {
                LogWarning($"제거 예정 취소 요청 전달 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// ImageList를 연결하여 일시정지/정지 시 현재 프레임을 리스트에서 선택하도록 합니다.
        /// </summary>
        public void SetLinkedImageList(SimpleDonkeyManager.controlutils.ImageList imageList)
        {
            linkedImageList = imageList;
        }

        /// <summary>
        /// 제거 예정(pending) 프레임 번호 집합을 설정합니다.
        /// 현재 프레임 빨간 오버레이, 타임라인 마커, 재생 스킵에 반영됩니다.
        /// </summary>
        public void SetPendingRemovedNumbers(System.Collections.Generic.IEnumerable<int> numbers)
        {
            pendingRemovedNumbers = numbers != null
                ? new HashSet<int>(numbers)
                : new HashSet<int>();
            SyncTimelineRemovalMarks();
            if (pictureBox1 != null)
                pictureBox1.Invalidate();
        }

        /// <summary>
        /// 특정 프레임 인덱스가 제거 예정 상태인지 여부를 반환합니다.
        /// </summary>
        private bool IsPendingIndex(int index)
        {
            if (pendingRemovedNumbers == null || pendingRemovedNumbers.Count == 0)
                return false;
            if (frameDataList == null || index < 0 || index >= frameDataList.Count)
                return false;
            var fd = frameDataList[index];
            return fd != null && pendingRemovedNumbers.Contains(fd.FrameNumber);
        }

        /// <summary>
        /// from 인덱스에서 direction(+1/-1) 방향으로 제거 예정이 아닌 첫 프레임 인덱스를 찾습니다.
        /// from 자체는 검사하지 않고 그 다음부터 탐색합니다. 못 찾으면 -1을 반환합니다.
        /// </summary>
        private int FindNextNonPending(int from, int direction)
        {
            if (frameDataList == null || frameDataList.Count == 0)
                return -1;
            int i = from + direction;
            while (i >= 0 && i < frameDataList.Count)
            {
                if (!IsPendingIndex(i))
                    return i;
                i += direction;
            }
            return -1;
        }

        /// <summary>
        /// 현재 pending 집합과 원본 대비 삭제 위치를 타임라인 마커로 동기화합니다.
        /// </summary>
        private void SyncTimelineRemovalMarks()
        {
            if (frameTimeline == null)
                return;

            // 제거 예정: 현재 프레임 목록에서 pending 번호에 해당하는 인덱스
            var pendingIndices = new System.Collections.Generic.List<int>();
            if (frameDataList != null && pendingRemovedNumbers != null && pendingRemovedNumbers.Count > 0)
            {
                for (int i = 0; i < frameDataList.Count; i++)
                {
                    if (frameDataList[i] != null && pendingRemovedNumbers.Contains(frameDataList[i].FrameNumber))
                        pendingIndices.Add(i);
                }
            }
            frameTimeline.SetPendingRemovedIndices(pendingIndices);

            // 이미 삭제(확정)된 위치: 원본 프레임 번호 대비 현재 누락된 번호의 비율
            var appliedRatios = new System.Collections.Generic.List<double>();
            if (imageManager != null && frameDataList != null && frameDataList.Count > 0)
            {
                try
                {
                    var deletedNumbers = imageManager.GetCurrentDeletedFrameNumbers();
                    if (deletedNumbers != null && deletedNumbers.Count > 0)
                    {
                        int minNum = frameDataList[0].FrameNumber;
                        int maxNum = frameDataList[frameDataList.Count - 1].FrameNumber;
                        foreach (var fd in frameDataList)
                        {
                            if (fd == null) continue;
                            if (fd.FrameNumber < minNum) minNum = fd.FrameNumber;
                            if (fd.FrameNumber > maxNum) maxNum = fd.FrameNumber;
                        }
                        double span = Math.Max(1, maxNum - minNum);
                        foreach (int dn in deletedNumbers)
                        {
                            double ratio = (dn - minNum) / span;
                            appliedRatios.Add(ratio);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogWarning($"삭제 위치 마커 계산 예외: {ex.Message}");
                }
            }
            frameTimeline.SetAppliedRemovedRatios(appliedRatios);
        }

        /// <summary>
        /// 현재 프레임을 연결된 ImageList에서 선택합니다. (재귀 호출 방지)
        /// </summary>
        private void SyncImageListSelection()
        {
            try
            {
                if (linkedImageList == null || isSyncingSelection)
                    return;
                if (currentFrameIndex < 0 || currentFrameIndex >= frameDataList.Count)
                    return;

                isSyncingSelection = true;
                try
                {
                    linkedImageList.SelectFrame(currentFrameIndex);
                }
                finally
                {
                    isSyncingSelection = false;
                }
            }
            catch (Exception ex)
            {
                LogWarning($"ImageList 동기화 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 외부(DataFilterControl 등)에서 ImageList 선택 변경으로 DisplayImage가 호출될 때
        /// 동기화 재귀를 막기 위해 사용하는 플래그입니다.
        /// </summary>
        public bool IsSyncingSelection => isSyncingSelection;

        /// <summary>
        /// 타임라인에 구간이 선택되어 있는지 여부입니다.
        /// </summary>
        public bool HasRange => frameTimeline != null && frameTimeline.HasRange;

        /// <summary>
        /// 타임라인의 선택/구간을 해제합니다.
        /// </summary>
        public void ClearTimelineSelection()
        {
            try
            {
                frameTimeline?.ClearSelection();
            }
            catch (Exception ex)
            {
                LogWarning($"타임라인 선택 해제 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 선택된 구간의 시작 인덱스입니다. 구간이 없으면 -1입니다.
        /// </summary>
        public int SelectedRangeStart =>
            (frameTimeline != null && frameTimeline.HasRange)
                ? Math.Min(frameTimeline.RangeStart, frameTimeline.RangeEnd)
                : -1;

        /// <summary>
        /// 선택된 구간의 끝 인덱스입니다. 구간이 없으면 -1입니다.
        /// </summary>
        public int SelectedRangeEnd =>
            (frameTimeline != null && frameTimeline.HasRange)
                ? Math.Max(frameTimeline.RangeStart, frameTimeline.RangeEnd)
                : -1;

        /// <summary>
        /// 현재 표시 중인 프레임의 인덱스입니다.
        /// </summary>
        public int CurrentFrameIndex => currentFrameIndex;

        /// <summary>
        /// 현재 ImageViewer가 보유한 프레임 데이터 목록입니다.
        /// </summary>
        public List<SimpleDonkeyManager.FrameData> FrameDataList => frameDataList;

        /// <summary>
        /// 선택/구간 정보를 라벨에 표시합니다.
        /// </summary>
        private void UpdateSelectionDisplay()
        {
            try
            {
                if (label6 == null)
                    return;

                if (frameTimeline != null && frameTimeline.HasRange)
                {
                    int start = Math.Min(frameTimeline.RangeStart, frameTimeline.RangeEnd);
                    int end = Math.Max(frameTimeline.RangeStart, frameTimeline.RangeEnd);
                    string startText = (start >= 0 && start < frameDataList.Count && frameDataList[start] != null)
                        ? $"Frame {frameDataList[start].FrameNumber}" : $"#{start}";
                    string endText = (end >= 0 && end < frameDataList.Count && frameDataList[end] != null)
                        ? $"Frame {frameDataList[end].FrameNumber}" : $"#{end}";
                    label6.Text = $"선택 : {startText} ~ {endText}";
                }
                else if (frameTimeline != null && frameTimeline.AnchorIndex >= 0)
                {
                    int a = frameTimeline.AnchorIndex;
                    string anchorText = (a >= 0 && a < frameDataList.Count && frameDataList[a] != null)
                        ? $"Frame {frameDataList[a].FrameNumber}" : $"#{a}";
                    label6.Text = $"선택 : {anchorText}";
                }
                else
                {
                    label6.Text = "";
                }
            }
            catch (Exception ex)
            {
                LogWarning($"선택 표시 업데이트 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 키보드 방향키로 프레임을 이동합니다.
        /// - 좌/우: 1프레임 이동, Shift+좌/우: 10프레임 이동
        /// - 구간/앵커가 선택되어 있으면 고정점(앵커)은 유지하고
        ///   현재 위치 이동에 따라 구간이 재설정됩니다.
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & Keys.KeyCode;
            if (key == Keys.Left || key == Keys.Right)
            {
                bool shift = (keyData & Keys.Shift) == Keys.Shift;
                int step = shift ? 10 : 1;
                int direction = (key == Keys.Left) ? -1 : 1;
                if (HandleArrowNavigation(direction * step))
                {
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 방향키 이동 처리. delta 만큼 현재 프레임을 이동하고,
        /// 앵커가 있으면 앵커를 고정한 채 구간을 갱신합니다.
        /// </summary>
        private bool HandleArrowNavigation(int delta)
        {
            try
            {
                if (frameDataList == null || frameDataList.Count == 0)
                    return false;
                if (isPlaying)
                    return false;

                int target = currentFrameIndex + delta;
                if (target < 0) target = 0;
                if (target > frameDataList.Count - 1) target = frameDataList.Count - 1;

                if (target == currentFrameIndex)
                    return true; // 경계에서 더 이동할 수 없어도 키 입력은 소비

                // 이동 전, 고정해야 할 앵커(고정점)를 먼저 결정합니다.
                int prevIndex = currentFrameIndex;
                int anchor = -1;
                if (frameTimeline != null)
                {
                    if (frameTimeline.HasRange)
                    {
                        // 현재 위치(prevIndex)는 구간의 한쪽 끝이므로 반대쪽 끝을 고정 앵커로 사용
                        int start = frameTimeline.RangeStart;
                        int end = frameTimeline.RangeEnd;
                        if (prevIndex == end) anchor = start;
                        else if (prevIndex == start) anchor = end;
                        else anchor = (frameTimeline.AnchorIndex >= 0) ? frameTimeline.AnchorIndex : start;
                    }
                    else if (frameTimeline.AnchorIndex >= 0)
                    {
                        anchor = frameTimeline.AnchorIndex;
                    }
                }

                DisplayFrameAtIndex(target);

                // 구간 또는 앵커가 선택되어 있으면 고정점을 유지한 채 구간 갱신
                if (frameTimeline != null && anchor >= 0)
                {
                    frameTimeline.SetRangeFromAnchor(anchor, target);
                }

                UpdateSelectionDisplay();
                if (!isPlaying) SyncImageListSelection();

                if (currentFrameIndex >= 0 && currentFrameIndex < frameDataList.Count && frameDataList[currentFrameIndex] != null)
                {
                    LogInfo($"키보드 이동: Frame {frameDataList[currentFrameIndex].FrameNumber}");
                }
                return true;
            }
            catch (Exception ex)
            {
                LogWarning($"키보드 프레임 이동 예외: {ex.Message}");
                return false;
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

