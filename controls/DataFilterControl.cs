using SimpleDonkeyManager.controls;
using SimpleDonkeyManager.controlutils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using ImageList = System.Windows.Forms.ImageList;

namespace SimpleDonkeyManager
{
    public partial class DataFilterControl : UserControl
    {
        private controlutils.ImageList imageList = new controlutils.ImageList();
        private controlutils.ImageViewer imageViewer = new controlutils.ImageViewer();
        private ImageManager imageManager;
        private List<FrameData> originalFrameDataList = new List<FrameData>();
        private List<FrameData> filteredFrameDataList = new List<FrameData>();
        private int originalTotalFrameCount = 0;
        private Logger logger;

        /// <summary>
        /// 제거 예정(pending) 프레임 번호 집합. "필터 적용"을 누르기 전까지 실제 삭제하지 않고 시각 표시만 합니다.
        /// </summary>
        private readonly HashSet<int> pendingRemovedNumbers = new HashSet<int>();

        /// <summary>
        /// 필터 적용 버튼의 원래 배경색. 제거 예정 강조 후 복원에 사용합니다.
        /// </summary>
        private Color? filterStartOriginalBackColor = null;

        /// <summary>
        /// 필터 스냅샷(버전) 저장소. 데이터 폴더 로드 시 초기화됩니다.
        /// </summary>
        private SnapshotStore snapshotStore;

        /// <summary>
        /// 현재 열려 있는 스냅샷 내역 창 (non-modal, 단일 인스턴스).
        /// </summary>
        private SnapshotHistoryForm snapshotHistoryForm;

        public DataFilterControl()
        {
            InitializeComponent();

            lstFilterSummary.Columns.Clear();
            lstFilterSummary.Columns.Add("항목", 200);
            lstFilterSummary.Columns.Add("값", 220);

            ImageList rowHeight = new ImageList();
            rowHeight.ImageSize = new Size(1, 25);
            lstFilterSummary.SmallImageList = rowHeight;

            lstFilterSummary.Font = new Font("나눔고딕", 10F);

            SetSummaryData("0", "0", "0 (0.0%)", "0.0%");

            // ImageList 설정
            imageList.Dock = DockStyle.Fill;
            imageList.AutoSize = false;
            imageList.Visible = true;
            pnlFrameList.Controls.Clear();
            pnlFrameList.Controls.Add(imageList);

            // ImageViewer 설정
            imageViewer.Dock = DockStyle.Fill;
            imageViewer.AutoSize = false;
            imageViewer.Visible = true;
            pnlImageView.Controls.Clear();
            pnlImageView.Controls.Add(imageViewer);

            // 이미지 선택 이벤트 구독
            imageList.ImageSelected += ImageList_ImageSelected;

            // ImageViewer가 일시정지/정지 시 현재 프레임을 ImageList에서 선택하도록 연결
            imageViewer.SetLinkedImageList(imageList);

            // 제거 예정 프레임 우클릭 취소 요청 처리
            imageViewer.PendingRemoveCancelRequested += ImageViewer_PendingRemoveCancelRequested;

            // 버튼 이벤트
            btnFilterStart.Click += BtnFilterStart_Click;
            btnFilterReset.Click += BtnFilterReset_Click;
            btnRemoveSelectedFrame.Click += BtnRemoveSelectedFrame_Click;
            btnUndoRemove.Click += BtnUndoRemove_Click;
            btnFilterUnselected.Click += BtnFilterUnselected_Click;

            InitializeTooltips();

            // Logger 참조 (MainWindow에서 주입받을 때까지 null 가능)
            logger = null;
        }

        private void InitializeTooltips()
        {
            var toolTip = new ToolTip { AutoPopDelay = 8000, InitialDelay = 400, ReshowDelay = 200, ShowAlways = true };
            toolTip.SetToolTip(chkThrottle, "Throttle 값이 0인 프레임을 필터링에서 제외합니다.");
            toolTip.SetToolTip(chkDisable, "기본 반전(flip) 이미지를 필터링에서 제외합니다.");
            toolTip.SetToolTip(numFilterAngle1, "조향각(Angle) 필터의 최솟값을 설정합니다. (단위: Rad)");
            toolTip.SetToolTip(numFilterAngle2, "조향각(Angle) 필터의 최댓값을 설정합니다. (단위: Rad)");
            toolTip.SetToolTip(numFilterThrottle1, "Throttle 필터의 최솟값을 설정합니다.");
            toolTip.SetToolTip(numFilterThrottle2, "Throttle 필터의 최댓값을 설정합니다.");
            toolTip.SetToolTip(comboBox1, "해상도 필터를 선택합니다. '(전체)'를 선택하면 모든 해상도를 포함합니다.");
            toolTip.SetToolTip(btnFilterStart, "현재 필터 조건을 적용하여 데이터를 필터링합니다. 제외된 프레임은 filtered 폴더로 백업됩니다.");
            toolTip.SetToolTip(btnFilterReset, "filtered 폴더의 백업을 토대로 원본 데이터를 모두 복구합니다.");
            toolTip.SetToolTip(btnRemoveSelectedFrame, "ImageList에서 현재 선택된 프레임을 필터링 결과에서 제거합니다.");
            toolTip.SetToolTip(btnUndoRemove, "직전에 수행한 삭제(프레임 제거 또는 필터 적용)를 한 번 되돌립니다.");
            toolTip.SetToolTip(btnFilterUnselected, "타임라인에서 선택한 구간의 프레임만 남기고 나머지를 필터링합니다.");
            toolTip.SetToolTip(lstFilterSummary, "필터링 결과 요약 정보를 표시합니다.");
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
        }

        /// <summary>
        /// DataLoadControl에서 데이터를 받아옵니다.
        /// </summary>
        public void SetFrameData(ImageManager manager, List<FrameData> frameDataList)
        {
            try
            {
                if (manager == null)
                {
                    LogWarning("SetFrameData: ImageManager가 null입니다");
                    return;
                }

                if (frameDataList == null || frameDataList.Count == 0)
                {
                    LogWarning("SetFrameData: 프레임 데이터가 없습니다");
                    this.originalFrameDataList = new List<FrameData>();
                    this.filteredFrameDataList = new List<FrameData>();
                    this.originalTotalFrameCount = 0;
                    UpdateStatistics();
                    return;
                }

                this.imageManager = manager;
                this.originalFrameDataList = new List<FrameData>(frameDataList);
                this.filteredFrameDataList = new List<FrameData>(frameDataList);
                this.originalTotalFrameCount = frameDataList.Count;

                // 첫 몇 프레임의 메타데이터 디버깅 로깅
                try
                {
                    for (int i = 0; i < Math.Min(3, frameDataList.Count); i++)
                    {
                        var frame = frameDataList[i];
                        if (frame != null && frame.Metadata != null && frame.Metadata.Count > 0)
                        {
                            var metadataKeys = string.Join(", ", frame.Metadata.Keys.Take(8));
                            LogInfo($"  Frame {frame.FrameNumber}: 메타데이터 키={metadataKeys}");

                            // Throttle, Angle 값 로깅
                            double throttle = frame.GetThrottle();
                            double angle = frame.GetAngle();
                            bool disable = frame.GetDisable();
                            LogInfo($"    Throttle={throttle}, Angle={angle}, Disable={disable}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogWarning($"메타데이터 디버깅 오류: {ex.Message}");
                }

                // ImageList와 ImageViewer에 데이터 설정
                if (imageList != null)
                {
                    imageList.SetImageManager(manager);
                    imageList.LoadFrames(filteredFrameDataList);
                }

                if (imageViewer != null)
                {
                    imageViewer.SetImageManager(manager);
                }

                // 첫 번째 프레임 선택
                if (frameDataList.Count > 0 && imageList != null)
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

                // 통계 업데이트
                UpdateStatistics();

                // 해상도 필터 초기화
                InitializeResolutionFilter(frameDataList);

                // 삭제 되돌리기 버튼 상태 갱신
                UpdateUndoButtonState();

                // 스냅샷 저장소 초기화 및 마지막 작성자 ID 복원
                InitializeSnapshotStore();

                LogInfo($"필터 컨트롤에 데이터 로드됨: {frameDataList.Count}개 프레임");
            }
            catch (Exception ex)
            {
                LogWarning($"SetFrameData 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 해상도 필터를 초기화합니다.
        /// </summary>
        private void InitializeResolutionFilter(List<FrameData> frameDataList)
        {
            try
            {
                if (comboBox1 == null)
                    return;

                comboBox1.Items.Clear();
                comboBox1.Items.Add("모든 해상도"); // 기본 옵션

                if (frameDataList == null || frameDataList.Count == 0)
                {
                    comboBox1.SelectedIndex = 0;
                    return;
                }

                // 고유한 해상도 추출
                var uniqueResolutions = frameDataList
                    .Where(f => f != null && !string.IsNullOrEmpty(f.Resolution))
                    .Select(f => f.Resolution)
                    .Distinct()
                    .OrderBy(r => r)
                    .ToList();

                // ComboBox에 추가
                foreach (var resolution in uniqueResolutions)
                {
                    comboBox1.Items.Add(resolution);
                }

                // 첫 번째 항목 선택 (모든 해상도)
                comboBox1.SelectedIndex = 0;

                LogInfo($"해상도 필터 초기화: {uniqueResolutions.Count}개 해상도 발견");
            }
            catch (Exception ex)
            {
                LogWarning($"해상도 필터 초기화 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 현재 필터 조건을 수집합니다.
        /// </summary>
        private FilterConditions GetFilterConditions()
        {
            try
            {
                FilterConditions conditions = new FilterConditions();

                // Throttle 0 제외: chkThrottle이 체크되면 활성화 (범위 필터와 독립)
                if (chkThrottle != null && chkThrottle.Checked)
                {
                    conditions.ExcludeThrottleZero = true;
                    LogInfo("Throttle 필터: 0 제외");
                }

                // Throttle 범위 필터: 범위값이 기본값(-1 ~ 1)에서 벗어난 경우 자동 활성화
                try
                {
                    decimal throttleVal1 = numFilterThrottle1?.Value ?? -1m;
                    decimal throttleVal2 = numFilterThrottle2?.Value ?? 1m;
                    double tMin = (double)Math.Min(throttleVal1, throttleVal2);
                    double tMax = (double)Math.Max(throttleVal1, throttleVal2);

                    // 기본 전체 범위(-1~1)가 아닐 때만 범위 필터 적용
                    if (tMin > -1.0 || tMax < 1.0)
                    {
                        conditions.FilterThrottle = true;
                        conditions.ThrottleMin = tMin;
                        conditions.ThrottleMax = tMax;
                        LogInfo($"Throttle 범위 필터: Min={tMin:F2}, Max={tMax:F2}");
                    }
                }
                catch (Exception ex)
                {
                    LogWarning($"Throttle 범위 조건 수집 오류: {ex.Message}");
                }

                // Angle 범위 필터: 범위값이 기본값(-1 ~ 1)에서 벗어난 경우 자동 활성화
                // chkDisable과 완전히 독립적으로 동작
                try
                {
                    decimal angleVal1 = numFilterAngle1?.Value ?? -1m;
                    decimal angleVal2 = numFilterAngle2?.Value ?? 1m;
                    double aMin = (double)Math.Min(angleVal1, angleVal2);
                    double aMax = (double)Math.Max(angleVal1, angleVal2);

                    // 기본 전체 범위(-1~1)가 아닐 때만 범위 필터 적용
                    if (aMin > -1.0 || aMax < 1.0)
                    {
                        conditions.FilterAngle = true;
                        conditions.AngleMin = aMin;
                        conditions.AngleMax = aMax;
                        LogInfo($"Angle 범위 필터: Min={aMin:F2}, Max={aMax:F2}");
                    }
                }
                catch (Exception ex)
                {
                    LogWarning($"Angle 범위 조건 수집 오류: {ex.Message}");
                }

                // 기본 반전 이미지 제외: chkDisable이 체크되면 disable=true 프레임 제외
                if (chkDisable != null && chkDisable.Checked)
                {
                    conditions.ExcludeDisabled = true;
                    LogInfo("기본 반전 이미지 제외 (disable=true 프레임 제거)");
                }

                // 해상도 필터
                try
                {
                    if (comboBox1 != null && comboBox1.SelectedItem != null)
                    {
                        string selectedRes = comboBox1.SelectedItem.ToString();

                        // "모든 해상도"가 아니면 필터 활성화
                        if (selectedRes != "모든 해상도")
                        {
                            conditions.FilterResolution = true;
                            conditions.SelectedResolution = selectedRes;
                            LogInfo($"해상도 필터: {selectedRes}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogWarning($"해상도 필터 조건 수집 오류: {ex.Message}");
                }

                return conditions;
            }
            catch (Exception ex)
            {
                LogWarning($"필터 조건 수집 중 예외: {ex.Message}");
                return new FilterConditions();
            }
        }

        /// <summary>
        /// 필터링된 데이터로 통계를 업데이트합니다.
        /// </summary>
        private void UpdateStatistics()
        {
            try
            {
                if (imageManager == null || originalTotalFrameCount <= 0 ||
                    filteredFrameDataList == null)
                {
                    SetSummaryData("0", "0", "0 (0.0%)", "0.0%");
                    return;
                }

                int totalFrames = originalTotalFrameCount;
                int filteredFrames = filteredFrameDataList.Count;
                int deletedFrames = totalFrames - filteredFrames;
                if (deletedFrames < 0) deletedFrames = 0;
                double activeRatio = totalFrames > 0 ? (double)filteredFrames / totalFrames * 100 : 0;

                SetSummaryData(
                    totalFrames.ToString("N0"),
                    filteredFrames.ToString("N0"),
                    $"{deletedFrames:N0} ({(totalFrames > 0 ? (double)deletedFrames / totalFrames * 100 : 0):F1}%)",
                    $"{activeRatio:F1}%"
                );
            }
            catch (Exception ex)
            {
                LogWarning($"통계 업데이트 오류: {ex.Message}");
                SetSummaryData("0", "0", "0 (0.0%)", "0.0%");
            }
        }

        private void SetSummaryData(string frame, string filterframe, string delframe, string activeframe)
        {
            try
            {
                if (lstFilterSummary == null)
                    return;

                lstFilterSummary.Items.Clear();

                AddSummaryRow("총 프레임 수", frame ?? "0");
                AddSummaryRow("필터링 후 프레임 수", filterframe ?? "0");
                AddSummaryRow("제거된 프레임 수", delframe ?? "0");
                AddSummaryRow("활성 프레임 비율", activeframe ?? "0.0%");
            }
            catch (Exception ex)
            {
                LogWarning($"요약 데이터 설정 오류: {ex.Message}");
            }
        }

        private void AddSummaryRow(string title, string value)
        {
            try
            {
                if (lstFilterSummary == null)
                    return;

                ListViewItem item = new ListViewItem(title ?? "");
                item.SubItems.Add(value ?? "");
                lstFilterSummary.Items.Add(item);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"요약 행 추가 오류: {ex.Message}");
            }
        }

        private void BtnFilterStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (imageManager == null)
                {
                    MessageBox.Show("로드된 데이터가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogWarning("필터 적용 실패: ImageManager가 없음");
                    return;
                }

                if (originalFrameDataList == null || originalFrameDataList.Count == 0)
                {
                    MessageBox.Show("필터링할 데이터가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogWarning("필터 적용 실패: 원본 데이터가 없음");
                    return;
                }

                // 필터 조건 수집
                FilterConditions conditions = GetFilterConditions();
                if (conditions == null)
                {
                    LogWarning("필터 조건을 수집할 수 없습니다");
                    return;
                }

                int totalBefore = originalFrameDataList.Count;

                // 조건을 통과(유지)하는 프레임 계산
                List<FrameData> keepList = ApplyFiltersTemporarily(conditions);
                if (keepList == null)
                    keepList = new List<FrameData>();

                // 제외 대상(유지되지 않는) 프레임 번호 계산
                var keepNumbers = new HashSet<int>(keepList.Where(f => f != null).Select(f => f.FrameNumber));
                var removeNumbers = originalFrameDataList
                    .Where(f => f != null && !keepNumbers.Contains(f.FrameNumber))
                    .Select(f => f.FrameNumber)
                    .Distinct()
                    .ToList();

                // 사용자가 수동으로 '제거 예정'으로 표시한 프레임도 함께 제외 대상에 포함
                int pendingCount = pendingRemovedNumbers.Count;
                if (pendingCount > 0)
                {
                    var combined = new HashSet<int>(removeNumbers);
                    foreach (int n in pendingRemovedNumbers)
                        combined.Add(n);
                    removeNumbers = combined.ToList();
                }

                if (removeNumbers.Count == 0)
                {
                    MessageBox.Show("필터 조건에 해당하여 제외되는 프레임이 없습니다.", "필터 적용", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogInfo("필터 적용: 제외 대상 프레임 없음");
                    UpdateStatistics();
                    return;
                }

                string pendingInfo = pendingCount > 0
                    ? $"(수동으로 표시한 제거 예정 {pendingCount:N0}개 포함)\n"
                    : "";
                var confirm = MessageBox.Show(
                    $"필터 조건에 따라 {removeNumbers.Count:N0}개의 프레임이 제외됩니다.\n" +
                    pendingInfo +
                    "제외된 프레임은 filtered 폴더로 백업되며 [필터 초기화] 또는 [삭제 되돌리기]로 복구할 수 있습니다.\n\n계속하시겠습니까?",
                    "필터 적용 확인", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (confirm != DialogResult.OK)
                {
                    LogInfo("필터 적용 취소됨");
                    return;
                }

                // 실제 파일을 filtered 폴더로 백업하며 제거
                int actuallyRemoved = imageManager.RemoveFrames(removeNumbers);
                LogInfo($"필터 적용: {actuallyRemoved}개 프레임 제거 및 filtered 백업 완료");

                // 제거 예정 상태 초기화 (필터 적용으로 확정됨)
                pendingRemovedNumbers.Clear();
                UpdatePendingUi();

                // 메모리 목록 갱신 (제거된 프레임 반영)
                originalFrameDataList = imageManager.GetAllFrameData();
                filteredFrameDataList = new List<FrameData>(originalFrameDataList);

                if (imageList != null)
                    imageList.LoadFrames(filteredFrameDataList);
                if (imageViewer != null)
                    imageViewer.SetImageManager(imageManager);

                UpdateStatistics();
                UpdateUndoButtonState();

                int totalAfter = filteredFrameDataList.Count;

                // 원본 총 프레임 수 기준 누적 제거 통계
                int totalRemovedFromOriginal = originalTotalFrameCount - totalAfter;
                if (totalRemovedFromOriginal < 0) totalRemovedFromOriginal = 0;
                double totalRemovedRatio = originalTotalFrameCount > 0
                    ? (double)totalRemovedFromOriginal / originalTotalFrameCount * 100 : 0;

                // 필터링 결과 요약 출력
                string summary =
                    $"필터링 결과 요약\n" +
                    $"────────────────────\n" +
                    $"원본 전체 프레임 수 : {originalTotalFrameCount:N0}\n" +
                    $"이번 필터로 제외된 수 : {actuallyRemoved:N0}\n" +
                    $"남은 프레임 수 : {totalAfter:N0}\n" +
                    $"누적 제거된 프레임 수 : {totalRemovedFromOriginal:N0} ({totalRemovedRatio:F1}%)";
                MessageBox.Show(summary, "필터 적용 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 첫 번째 프레임 선택
                if (filteredFrameDataList.Count > 0 && imageList != null)
                {
                    try { imageList.SelectFrame(0); }
                    catch (Exception ex) { LogWarning($"첫 번째 프레임 선택 실패: {ex.Message}"); }
                }

                // 필터된 데이터를 Training으로 전달
                MainWindow mainWindow = FindMainWindow();
                if (mainWindow != null)
                {
                    string dataFolder = imageManager.SelectedFolderPath ?? "";
                    mainWindow.SetTrainingData(filteredFrameDataList, dataFolder);
                    mainWindow.SetStatusMessage(
                        $"② 데이터 필터링 —  필터 적용 완료 (남은 프레임: {totalAfter:N0}개, 제외: {actuallyRemoved:N0}개)  →  ③ [학습 실행] 화면으로 이동해주세요.",
                        MainWindow.StatusLevel.Success);
                    LogInfo($"학습 데이터 전달: {totalAfter}개 프레임, 폴더: {dataFolder}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"필터 적용 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"필터 적용 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 직전 삭제(필터 적용 또는 선택 프레임 제거)를 한 번 되돌립니다.
        /// </summary>
        private void BtnUndoRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (imageManager == null)
                {
                    LogWarning("삭제 되돌리기 실패: ImageManager가 없습니다");
                    return;
                }

                if (!imageManager.CanUndoLastRemove)
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

                // 복구 후 재스캔된 데이터로 메모리 목록 갱신
                originalFrameDataList = imageManager.GetAllFrameData();
                filteredFrameDataList = new List<FrameData>(originalFrameDataList);

                if (imageList != null)
                    imageList.LoadFrames(filteredFrameDataList);
                if (imageViewer != null)
                    imageViewer.SetImageManager(imageManager);

                UpdateStatistics();
                UpdateUndoButtonState();

                LogInfo($"직전 삭제 되돌리기 완료 (현재 프레임: {filteredFrameDataList.Count}개)");

                // 프레임 목록이 갱신되었으므로 제거 예정 마커 재동기화
                UpdatePendingUi();

                if (filteredFrameDataList.Count > 0 && imageList != null)
                {
                    try { imageList.SelectFrame(0); }
                    catch (Exception ex) { LogWarning($"첫 번째 프레임 선택 실패: {ex.Message}"); }
                }

                MainWindow mainWindow = FindMainWindow();
                mainWindow?.SetStatusMessage(
                    $"② 데이터 필터링 —  직전 삭제 되돌리기 완료 (현재 프레임: {filteredFrameDataList.Count:N0}개)",
                    MainWindow.StatusLevel.Info);

                MessageBox.Show("직전 삭제가 되돌려졌습니다.", "삭제 되돌리기", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"삭제 되돌리기 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"삭제 되돌리기 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 삭제 되돌리기 버튼의 활성화 상태를 갱신합니다.
        /// </summary>
        private void UpdateUndoButtonState()
        {
            try
            {
                if (btnUndoRemove != null)
                    btnUndoRemove.Enabled = imageManager != null && imageManager.CanUndoLastRemove;
            }
            catch (Exception ex)
            {
                LogWarning($"되돌리기 버튼 상태 갱신 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 외부(크게 보기 창 등)에서 ImageManager를 직접 변경한 뒤,
        /// 메모리 목록/리스트/뷰어/통계를 디스크 상태로 다시 동기화합니다.
        /// </summary>
        public void RefreshAfterExternalChange()
        {
            try
            {
                if (imageManager == null)
                    return;

                originalFrameDataList = imageManager.GetAllFrameData();
                filteredFrameDataList = new List<FrameData>(originalFrameDataList);

                if (imageList != null)
                    imageList.LoadFrames(filteredFrameDataList);
                if (imageViewer != null)
                    imageViewer.SetImageManager(imageManager);

                UpdateStatistics();
                UpdateUndoButtonState();

                // 외부에서 삭제 상태가 교체되었으므로 제거 예정 상태 초기화
                pendingRemovedNumbers.Clear();
                UpdatePendingUi();

                if (filteredFrameDataList.Count > 0 && imageList != null)
                {
                    try { imageList.SelectFrame(0); }
                    catch (Exception ex) { LogWarning($"첫 번째 프레임 선택 실패: {ex.Message}"); }
                }
            }
            catch (Exception ex)
            {
                LogWarning($"외부 변경 후 새로고침 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 현재 ImageManager를 반환합니다. (크게 보기 창 연동용)
        /// </summary>
        public ImageManager GetImageManager() => imageManager;

        private void BtnFilterReset_Click(object sender, EventArgs e)
        {
            try
            {
                // 필터 초기화
                if (imageManager == null)
                {
                    LogWarning("필터 초기화 실패: ImageManager가 없습니다");
                    return;
                }

                // filtered 폴더의 백업 파일을 토대로 실제 원본 폴더 복구
                bool restored = imageManager.RestoreAllFrames();
                if (restored)
                {
                    // 복구 후 재스캔된 데이터로 메모리 목록 갱신
                    originalFrameDataList = imageManager.GetAllFrameData();
                    LogInfo($"filtered 폴더 기반 원본 복구 완료 ({originalFrameDataList.Count}개 프레임)");
                }
                else
                {
                    LogInfo("복구할 filtered 백업이 없거나 복구가 필요하지 않습니다");
                }

                if (originalFrameDataList == null || originalFrameDataList.Count == 0)
                {
                    LogWarning("필터 초기화 실패: 원본 데이터가 없습니다");
                    return;
                }

                filteredFrameDataList = new List<FrameData>(originalFrameDataList);
                // 모든 프레임이 복구되었으므로 원본 총 프레임 수도 갱신 (제거 0개 상태)
                originalTotalFrameCount = originalFrameDataList.Count;
                if (imageList != null)
                {
                    imageList.LoadFrames(filteredFrameDataList);
                }
                if (imageViewer != null)
                {
                    imageViewer.SetImageManager(imageManager);
                }
                UpdateStatistics();

                // 제거 예정 상태도 초기화 (모든 프레임 복원됨)
                pendingRemovedNumbers.Clear();
                UpdatePendingUi();

                LogInfo("필터가 초기화됨 (모든 프레임 복원)");

                // 첫 번째 프레임 선택
                if (filteredFrameDataList.Count > 0 && imageList != null)
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

                MessageBox.Show("필터가 초기화되었습니다. 모든 프레임이 복원되었습니다.", "필터 초기화", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"필터 초기화 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"필터 초기화 예외: {ex.Message}");
            }
            finally
            {
                UpdateUndoButtonState();
            }
        }

        /// <summary>
        /// 제거 예정(pending) 집합을 ImageViewer/타임라인과 라벨/버튼 강조에 반영합니다.
        /// </summary>
        private void UpdatePendingUi()
        {
            try
            {
                if (imageViewer != null)
                    imageViewer.SetPendingRemovedNumbers(pendingRemovedNumbers);

                int count = pendingRemovedNumbers.Count;

                if (lblPendingRemove != null)
                {
                    if (count > 0)
                    {
                        lblPendingRemove.Text = $"제거 예정 {count:N0}개 · 필터 적용을 눌러 확정";
                        lblPendingRemove.Visible = true;
                    }
                    else
                    {
                        lblPendingRemove.Visible = false;
                    }
                }

                // 필터 적용 버튼 강조 (제거 예정이 있을 때만)
                if (btnFilterStart != null)
                {
                    if (filterStartOriginalBackColor == null)
                        filterStartOriginalBackColor = btnFilterStart.BackColor;

                    if (count > 0)
                    {
                        btnFilterStart.BackColor = Color.FromArgb(220, 53, 69);
                        btnFilterStart.ForeColor = Color.White;
                        btnFilterStart.Font = new Font(btnFilterStart.Font, FontStyle.Bold);
                    }
                    else
                    {
                        btnFilterStart.BackColor = filterStartOriginalBackColor.Value;
                        btnFilterStart.ForeColor = SystemColors.ControlText;
                        btnFilterStart.Font = new Font(btnFilterStart.Font, FontStyle.Regular);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWarning($"제거 예정 UI 갱신 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 제거 예정으로 표시된 프레임을 우클릭하여 취소를 요청했을 때 호출됩니다.
        /// 사용자 확인 후 해당 프레임을 제거 예정에서 해제합니다.
        /// </summary>
        private void ImageViewer_PendingRemoveCancelRequested(object sender, int frameNumber)
        {
            try
            {
                if (!pendingRemovedNumbers.Contains(frameNumber))
                    return;

                var confirm = MessageBox.Show(
                    $"Frame {frameNumber}의 제거 예정을 취소하시겠습니까?",
                    "제거 예정 취소", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes)
                    return;

                pendingRemovedNumbers.Remove(frameNumber);
                UpdatePendingUi();

                MainWindow mainWindow = FindMainWindow();
                mainWindow?.SetStatusMessage(
                    $"② 데이터 필터링 —  Frame {frameNumber} 제거 예정 취소됨  (남은 제거 예정: {pendingRemovedNumbers.Count:N0}개)",
                    MainWindow.StatusLevel.Info);
                LogInfo($"Frame {frameNumber} 제거 예정 취소");
            }
            catch (Exception ex)
            {
                LogWarning($"제거 예정 취소 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// ImageList에서 현재 선택된 프레임을 필터링 결과에서 제거합니다.
        /// ImageViewer 타임라인에서 구간이 선택된 경우, 해당 구간의 모든 프레임을 한 번에 제거합니다.
        /// </summary>
        private void BtnRemoveSelectedFrame_Click(object sender, EventArgs e)
        {
            try
            {
                if (filteredFrameDataList == null || filteredFrameDataList.Count == 0)
                {
                    MessageBox.Show("필터링된 데이터가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogWarning("프레임 제거 실패: 필터링된 데이터 없음");
                    return;
                }

                if (imageList == null)
                {
                    LogWarning("프레임 제거 실패: imageList가 null입니다");
                    return;
                }

                // ImageViewer 타임라인에서 구간이 선택된 경우 → 구간 전체 삭제
                if (imageViewer != null && imageViewer.HasRange)
                {
                    RemoveSelectedRangeFrames();
                    return;
                }

                int selectedIndex = imageList.SelectedIndex;
                FrameData selectedFrame = imageList.SelectedFrame;

                if (selectedFrame == null || selectedIndex < 0)
                {
                    MessageBox.Show("제거할 프레임을 ImageList에서 먼저 선택하거나\n타임라인에서 구간을 선택해주세요.", "선택 필요", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogWarning("프레임 제거 실패: 선택된 프레임 없음");
                    return;
                }

                int frameNumber = selectedFrame.FrameNumber;

                // 즉시 삭제하지 않고 '제거 예정'으로 등록 (필터 적용 시 확정)
                if (pendingRemovedNumbers.Contains(frameNumber))
                {
                    LogInfo($"프레임 {frameNumber}은(는) 이미 제거 예정 상태입니다");
                    return;
                }

                pendingRemovedNumbers.Add(frameNumber);
                UpdatePendingUi();

                LogInfo($"프레임 {frameNumber} 제거 예정 등록 (제거 예정: {pendingRemovedNumbers.Count}개)");

                MainWindow mainWindow = FindMainWindow();
                mainWindow?.SetStatusMessage(
                    $"② 데이터 필터링 —  Frame {frameNumber} 제거 예정  (총 제거 예정: {pendingRemovedNumbers.Count:N0}개)  ·  [필터 적용]을 눌러 확정",
                    MainWindow.StatusLevel.Info);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"프레임 제거 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"프레임 제거 예외: {ex.Message}");
            }
            finally
            {
                UpdateUndoButtonState();
            }
        }

        /// <summary>
        /// ImageViewer 타임라인에서 선택된 구간의 모든 프레임을 한 번에 제거합니다.
        /// 제거된 프레임은 filtered 폴더로 백업되어 되돌리기/초기화로 복구할 수 있습니다.
        /// </summary>
        private void RemoveSelectedRangeFrames()
        {
            try
            {
                int rangeStart = imageViewer.SelectedRangeStart;
                int rangeEnd = imageViewer.SelectedRangeEnd;
                var viewerFrames = imageViewer.FrameDataList;

                if (viewerFrames == null || rangeStart < 0 || rangeEnd < 0 ||
                    rangeStart >= viewerFrames.Count || rangeEnd >= viewerFrames.Count)
                {
                    MessageBox.Show("선택된 구간이 올바르지 않습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogWarning($"구간 프레임 제거 실패: 구간 인덱스 오류 ({rangeStart}~{rangeEnd})");
                    return;
                }

                // 선택 구간에 해당하는 프레임 번호 수집
                var removeNumbers = new List<int>();
                for (int i = rangeStart; i <= rangeEnd; i++)
                {
                    if (viewerFrames[i] != null)
                        removeNumbers.Add(viewerFrames[i].FrameNumber);
                }

                if (removeNumbers.Count == 0)
                {
                    MessageBox.Show("제거할 프레임이 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var confirm = MessageBox.Show(
                    $"선택된 구간의 {removeNumbers.Count:N0}개 프레임을 제거 예정으로 표시합니다.\n" +
                    "표시된 프레임은 빨갛게 표시되며, [필터 적용]을 눌러야 실제로 제거됩니다.\n\n계속하시겠습니까?",
                    "선택 구간 제거 예정", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes)
                    return;

                // 즉시 삭제하지 않고 '제거 예정'으로 등록 (필터 적용 시 확정)
                int newlyAdded = 0;
                foreach (int num in removeNumbers)
                {
                    if (pendingRemovedNumbers.Add(num))
                        newlyAdded++;
                }

                // 구간 선택 해제 후 마커/오버레이 갱신
                if (imageViewer != null)
                    imageViewer.ClearTimelineSelection();
                UpdatePendingUi();

                LogInfo($"선택 구간 제거 예정 등록: {newlyAdded}개 추가 (총 제거 예정: {pendingRemovedNumbers.Count}개)");

                MainWindow mainWindow = FindMainWindow();
                mainWindow?.SetStatusMessage(
                    $"② 데이터 필터링 —  선택 구간 {newlyAdded:N0}개 프레임 제거 예정  (총 제거 예정: {pendingRemovedNumbers.Count:N0}개)  ·  [필터 적용]을 눌러 확정",
                    MainWindow.StatusLevel.Info);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"구간 프레임 제거 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"구간 프레임 제거 예외: {ex.Message}");
            }
            finally
            {
                UpdateUndoButtonState();
            }
        }

        /// <summary>
        /// 현재 ImageViewer에서 선택된 구간의 프레임만 남기고 나머지 프레임을 필터링(제거)합니다.
        /// </summary>
        private void BtnFilterUnselected_Click(object sender, EventArgs e)
        {
            try
            {
                if (imageManager == null)
                {
                    MessageBox.Show("로드된 데이터가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogWarning("미선택 프레임 필터 실패: ImageManager가 없습니다");
                    return;
                }

                if (filteredFrameDataList == null || filteredFrameDataList.Count == 0)
                {
                    MessageBox.Show("필터링할 데이터가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogWarning("미선택 프레임 필터 실패: 필터링된 데이터 없음");
                    return;
                }

                if (imageViewer == null || !imageViewer.HasRange)
                {
                    MessageBox.Show("먼저 타임라인에서 남길 구간을 선택해주세요.\n(더블클릭 후 드래그하여 구간을 지정합니다.)",
                        "구간 선택 필요", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogWarning("미선택 프레임 필터 실패: 선택된 구간 없음");
                    return;
                }

                int rangeStart = imageViewer.SelectedRangeStart;
                int rangeEnd = imageViewer.SelectedRangeEnd;
                var viewerFrames = imageViewer.FrameDataList;

                if (viewerFrames == null || rangeStart < 0 || rangeEnd < 0 ||
                    rangeStart >= viewerFrames.Count || rangeEnd >= viewerFrames.Count)
                {
                    MessageBox.Show("선택된 구간이 올바르지 않습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogWarning($"미선택 프레임 필터 실패: 구간 인덱스 오류 ({rangeStart}~{rangeEnd})");
                    return;
                }

                // 남길 프레임 번호 집합 (선택 구간)
                var keepNumbers = new HashSet<int>();
                for (int i = rangeStart; i <= rangeEnd; i++)
                {
                    if (viewerFrames[i] != null)
                        keepNumbers.Add(viewerFrames[i].FrameNumber);
                }

                // 제거 대상: 선택 구간 밖의 모든 프레임
                var removeNumbers = filteredFrameDataList
                    .Where(f => f != null && !keepNumbers.Contains(f.FrameNumber))
                    .Select(f => f.FrameNumber)
                    .ToList();

                if (removeNumbers.Count == 0)
                {
                    MessageBox.Show("제거할 미선택 프레임이 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int keepCount = filteredFrameDataList.Count - removeNumbers.Count;
                var confirm = MessageBox.Show(
                    $"선택된 구간({keepCount:N0}개)만 남기고 나머지 {removeNumbers.Count:N0}개 프레임을 필터링합니다.\n계속하시겠습니까?",
                    "미선택 프레임 필터", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes)
                    return;

                int actuallyRemoved = imageManager.RemoveFrames(removeNumbers);

                // 복구된 디스크 상태로 메모리 목록 갱신
                originalFrameDataList = imageManager.GetAllFrameData();
                filteredFrameDataList = new List<FrameData>(originalFrameDataList);

                if (imageList != null)
                    imageList.LoadFrames(filteredFrameDataList);
                if (imageViewer != null)
                    imageViewer.SetImageManager(imageManager);

                UpdateStatistics();
                UpdateUndoButtonState();

                if (filteredFrameDataList.Count > 0 && imageList != null)
                {
                    try { imageList.SelectFrame(0); }
                    catch (Exception ex) { LogWarning($"첫 번째 프레임 선택 실패: {ex.Message}"); }
                }

                LogInfo($"미선택 프레임 필터 완료: {actuallyRemoved}개 제거 (남은 프레임: {filteredFrameDataList.Count}개)");

                MainWindow mainWindow = FindMainWindow();
                mainWindow?.SetStatusMessage(
                    $"② 데이터 필터링 —  미선택 프레임 필터: {actuallyRemoved:N0}개 제거  (남은 프레임: {filteredFrameDataList.Count:N0}개)",
                    MainWindow.StatusLevel.Info);

                MessageBox.Show($"미선택 프레임 {actuallyRemoved:N0}개가 필터링되었습니다.\n(남은 프레임: {filteredFrameDataList.Count:N0}개)",
                    "미선택 프레임 필터", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"미선택 프레임 필터 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"미선택 프레임 필터 예외: {ex.Message}");
            }
            finally
            {
                UpdateUndoButtonState();
            }
        }

        /// <summary>
        /// 임시로 필터를 적용하고 필터링된 리스트를 반환합니다.
        /// </summary>
        private List<FrameData> ApplyFiltersTemporarily(FilterConditions conditions)
        {
            try
            {
                if (conditions == null || originalFrameDataList == null)
                    return new List<FrameData>();

                List<FrameData> result = new List<FrameData>(originalFrameDataList);
                int beforeCount = result.Count;

                // Throttle 0 제외 (범위 필터와 독립)
                if (conditions.ExcludeThrottleZero)
                {
                    try
                    {
                        int beforeZero = result.Count;
                        result = result.Where(f => f != null && f.GetThrottle() != 0.0).ToList();
                        LogInfo($"Throttle 0 제외: {beforeZero} → {result.Count} ({beforeZero - result.Count}개 제거)");
                    }
                    catch (Exception ex)
                    {
                        LogWarning($"Throttle 0 제외 중 오류: {ex.Message}");
                    }
                }

                // Throttle 범위 필터
                if (conditions.FilterThrottle)
                {
                    try
                    {
                        int beforeThrottle = result.Count;
                        result = result.Where(f =>
                        {
                            if (f == null) return false;
                            double throttle = f.GetThrottle();
                            return throttle >= conditions.ThrottleMin && throttle <= conditions.ThrottleMax;
                        }).ToList();

                        int removedCount = beforeThrottle - result.Count;
                        LogInfo($"Throttle 범위 필터: {beforeThrottle} → {result.Count} ({removedCount}개 제거, 범위: {conditions.ThrottleMin:F2}~{conditions.ThrottleMax:F2})");
                    }
                    catch (Exception ex)
                    {
                        LogWarning($"Throttle 범위 필터 적용 중 오류: {ex.Message}");
                    }
                }

                // Angle 필터
                if (conditions.FilterAngle)
                {
                    try
                    {
                        int beforeAngle = result.Count;
                        result = result.Where(f =>
                        {
                            if (f == null)
                                return false;

                            double angle = f.GetAngle();

                            // 범위 체크
                            bool inRange = angle >= conditions.AngleMin && angle <= conditions.AngleMax;
                            return inRange;
                        }).ToList();

                        int removedCount = beforeAngle - result.Count;
                        LogInfo($"Angle 범위 필터: {beforeAngle} → {result.Count} ({removedCount}개 제거, 범위: {conditions.AngleMin:F2}~{conditions.AngleMax:F2})");
                    }
                    catch (Exception ex)
                    {
                        LogWarning($"Angle 범위 필터 적용 중 오류: {ex.Message}");
                    }
                }

                // 기본 반전 이미지 제외 (disable=true 프레임 제거)
                if (conditions.ExcludeDisabled)
                {
                    try
                    {
                        int beforeDisable = result.Count;
                        result = result.Where(f => f != null && !f.GetDisable()).ToList();
                        LogInfo($"기본 반전 이미지 제외: {beforeDisable} → {result.Count} ({beforeDisable - result.Count}개 제거)");
                    }
                    catch (Exception ex)
                    {
                        LogWarning($"기본 반전 이미지 제외 중 오류: {ex.Message}");
                    }
                }

                // 해상도 필터
                if (conditions.FilterResolution && !string.IsNullOrEmpty(conditions.SelectedResolution))
                {
                    try
                    {
                        int beforeResolution = result.Count;
                        result = result.Where(f =>
                        {
                            if (f == null)
                                return false;
                            return f.Resolution == conditions.SelectedResolution;
                        }).ToList();

                        int removedCount = beforeResolution - result.Count;
                        LogInfo($"해상도 필터 적용 ({conditions.SelectedResolution}): {beforeResolution} → {result.Count} ({removedCount}개 제거)");
                    }
                    catch (Exception ex)
                    {
                        LogWarning($"해상도 필터 적용 중 오류: {ex.Message}");
                    }
                }

                // 중복 프레임 제거
                if (conditions.RemoveDuplicateFrames)
                {
                    try
                    {
                        int beforeDuplicate = result.Count;
                        result = RemoveDuplicates(result);

                        int removedCount = beforeDuplicate - result.Count;
                        LogInfo($"중복 프레임 제거: {beforeDuplicate} → {result.Count} ({removedCount}개 제거)");
                    }
                    catch (Exception ex)
                    {
                        LogWarning($"중복 프레임 제거 중 오류: {ex.Message}");
                    }
                }

                // 조향 값 급변 제거
                if (conditions.RemoveHighlightChanges)
                {
                    try
                    {
                        int beforeHighlight = result.Count;
                        result = RemoveHighlightChanges(result);

                        int removedCount = beforeHighlight - result.Count;
                        LogInfo($"조향 값 급변 제거: {beforeHighlight} → {result.Count} ({removedCount}개 제거)");
                    }
                    catch (Exception ex)
                    {
                        LogWarning($"조향 값 급변 제거 중 오류: {ex.Message}");
                    }
                }

                LogInfo($"전체 필터 결과: {beforeCount} → {result.Count}개 프레임");
                return result ?? new List<FrameData>();
            }
            catch (Exception ex)
            {
                LogWarning($"임시 필터 적용 예외: {ex.Message}");
                return new List<FrameData>();
            }
        }

        /// <summary>
        /// 필터를 영구적으로 적용합니다.
        /// </summary>
        private void ApplyFilters(FilterConditions conditions)
        {
            try
            {
                if (conditions == null)
                    return;

                filteredFrameDataList = ApplyFiltersTemporarily(conditions);
                if (filteredFrameDataList == null)
                    filteredFrameDataList = new List<FrameData>();

                if (imageList != null)
                {
                    imageList.LoadFrames(filteredFrameDataList);
                }
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                LogWarning($"필터 적용 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 중복 프레임을 제거합니다.
        /// </summary>
        private List<FrameData> RemoveDuplicates(List<FrameData> frames)
        {
            try
            {
                if (frames == null || frames.Count == 0)
                    return new List<FrameData>();

                List<FrameData> result = new List<FrameData>();
                FrameData lastFrame = null;

                foreach (var frame in frames)
                {
                    if (frame == null)
                        continue;

                    // 이전 프레임과 비교
                    if (lastFrame == null || frame.ImagePath != lastFrame.ImagePath)
                    {
                        result.Add(frame);
                        lastFrame = frame;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                LogWarning($"중복 제거 예외: {ex.Message}");
                return frames ?? new List<FrameData>();
            }
        }

        /// <summary>
        /// 조향 값 급변 구간을 제거합니다.
        /// </summary>
        private List<FrameData> RemoveHighlightChanges(List<FrameData> frames)
        {
            try
            {
                if (frames == null || frames.Count < 2)
                    return frames ?? new List<FrameData>();

                List<FrameData> result = new List<FrameData>();
                const double angleThreshold = 5.0; // 조향 값 급변 기준 (도 단위)

                if (frames[0] != null)
                {
                    result.Add(frames[0]);
                }

                for (int i = 1; i < frames.Count; i++)
                {
                    if (frames[i] == null || frames[i - 1] == null)
                        continue;

                    try
                    {
                        double prevAngle = frames[i - 1].GetAngle();
                        double currAngle = frames[i].GetAngle();
                        double angleDiff = Math.Abs(currAngle - prevAngle);

                        // 급변하지 않으면 포함
                        if (angleDiff <= angleThreshold)
                        {
                            result.Add(frames[i]);
                        }
                    }
                    catch
                    {
                        // 개별 항목 처리 실패 시 계속
                        continue;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                LogWarning($"조향 급변 제거 예외: {ex.Message}");
                return frames ?? new List<FrameData>();
            }
        }

        private void lstFilterSummary_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void DisplayImage(string imagePath)
        {
            if (imageViewer != null)
            {
                imageViewer.DisplayImage(imagePath);
            }
        }

        /// <summary>
        /// 필터된 프레임 데이터 리스트를 반환합니다.
        /// </summary>
        public List<FrameData> GetFilteredFrameData()
        {
            return filteredFrameDataList ?? new List<FrameData>();
        }

        /// <summary>
        /// 원본 프레임 데이터 리스트를 반환합니다.
        /// </summary>
        public List<FrameData> GetOriginalFrameData()
        {
            return originalFrameDataList ?? new List<FrameData>();
        }

        /// <summary>
        /// 로그 메시지를 기록합니다.
        /// </summary>
        private void LogInfo(string message)
        {
            if (logger != null)
            {
                logger.AppendLog($"[필터] {message}");
            }
        }

        /// <summary>
        /// 경고 로그를 기록합니다.
        /// </summary>
        private void LogWarning(string message)
        {
            if (logger != null)
            {
                logger.AppendLog($"[필터 경고] {message}");
            }
        }

        private void chkThrottle_CheckedChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 스냅샷 저장소를 초기화하고, 마지막 스냅샷의 작성자 ID를 입력란에 복원합니다.
        /// </summary>
        private void InitializeSnapshotStore()
        {
            try
            {
                if (imageManager == null)
                    return;

                string managerFolder = imageManager.ManagerFolderPath;
                if (string.IsNullOrEmpty(managerFolder))
                    return;

                snapshotStore = new SnapshotStore(managerFolder);

                // 마지막 스냅샷의 작성자 ID 복원 (메모는 복원하지 않음)
                if (txtSnapshotAuthor != null)
                    txtSnapshotAuthor.Text = snapshotStore.LastAuthorId;

                // 내역 창이 열려 있으면 갱신
                RefreshSnapshotHistory();

                LogInfo($"스냅샷 저장소 초기화 완료 ({snapshotStore.Snapshots.Count}개 스냅샷)");
            }
            catch (Exception ex)
            {
                LogWarning($"스냅샷 저장소 초기화 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 현재 삭제(제외) 상태를 새 스냅샷으로 저장합니다.
        /// </summary>
        private void btnSaveSnapshot_Click(object sender, EventArgs e)
        {
            try
            {
                if (imageManager == null)
                {
                    MessageBox.Show("저장할 데이터가 없습니다. 먼저 데이터 폴더를 불러오세요.",
                        "스냅샷 저장", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (snapshotStore == null)
                    InitializeSnapshotStore();

                if (snapshotStore == null)
                {
                    MessageBox.Show("스냅샷 저장소를 초기화할 수 없습니다.",
                        "스냅샷 저장", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string authorId = txtSnapshotAuthor?.Text?.Trim() ?? string.Empty;
                if (string.IsNullOrEmpty(authorId))
                {
                    MessageBox.Show("작성자 ID를 입력하세요.",
                        "스냅샷 저장", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSnapshotAuthor?.Focus();
                    return;
                }

                string memo = txtSnapshotMemo?.Text?.Trim() ?? string.Empty;

                var deletedFrames = imageManager.GetCurrentDeletedFrameNumbers();
                var snapshot = snapshotStore.Add(authorId, memo, deletedFrames);

                // 저장 후 메모 입력란 비우기 (작성자 ID는 유지)
                if (txtSnapshotMemo != null)
                    txtSnapshotMemo.Text = string.Empty;

                RefreshSnapshotHistory();

                LogInfo($"스냅샷 저장됨: 작성자={authorId}, 삭제={snapshot.DeletedCount}개");

                MainWindow mainWindow = FindMainWindow();
                mainWindow?.SetStatusMessage(
                    $"필터 스냅샷이 저장되었습니다. (제외 프레임 {snapshot.DeletedCount}개)",
                    MainWindow.StatusLevel.Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"스냅샷 저장 중 오류 발생: {ex.Message}",
                    "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"스냅샷 저장 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 스냅샷 내역 창을 엽니다. (non-modal, 메인 창 오른쪽에 배치)
        /// </summary>
        private void btnSnapshotHistory_Click(object sender, EventArgs e)
        {
            try
            {
                if (imageManager == null)
                {
                    MessageBox.Show("먼저 데이터 폴더를 불러오세요.",
                        "스냅샷 내역", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (snapshotStore == null)
                    InitializeSnapshotStore();

                // 이미 열려 있으면 앞으로 가져오기
                if (snapshotHistoryForm != null && !snapshotHistoryForm.IsDisposed)
                {
                    snapshotHistoryForm.RefreshList();
                    snapshotHistoryForm.BringToFront();
                    snapshotHistoryForm.Activate();
                    return;
                }

                snapshotHistoryForm = new SnapshotHistoryForm(this);
                snapshotHistoryForm.FormClosed += (s, args) => snapshotHistoryForm = null;

                // 메인 창 오른쪽에 배치하되, 화면 밖으로 나가지 않도록 보정
                MainWindow mainWindow = FindMainWindow();
                PositionHistoryForm(snapshotHistoryForm, mainWindow);

                snapshotHistoryForm.Show(mainWindow);
                snapshotHistoryForm.BringToFront();
                snapshotHistoryForm.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"스냅샷 내역 창을 여는 중 오류 발생: {ex.Message}",
                    "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"스냅샷 내역 창 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 내역 창을 메인 창 오른쪽에 배치하되, 화면 작업 영역을 벗어나면
        /// 화면 안쪽(오른쪽 정렬 또는 메인 창 위에 겹치기)으로 보정합니다.
        /// </summary>
        private void PositionHistoryForm(SnapshotHistoryForm form, MainWindow mainWindow)
        {
            try
            {
                form.StartPosition = FormStartPosition.Manual;

                Rectangle anchor = mainWindow != null ? mainWindow.Bounds : this.Bounds;
                Rectangle workingArea = Screen.FromRectangle(anchor).WorkingArea;

                int width = form.Width;
                int height = form.Height;

                // 1순위: 메인 창 오른쪽
                int x = anchor.Right + 5;
                int y = anchor.Top;

                // 오른쪽에 공간이 부족하면 화면 오른쪽 끝에 붙임
                if (x + width > workingArea.Right)
                    x = workingArea.Right - width;

                // 그래도 메인 창과 너무 겹치면 화면 왼쪽 경계까지만 보정
                if (x < workingArea.Left)
                    x = workingArea.Left;

                // 세로 위치 보정
                if (y + height > workingArea.Bottom)
                    y = workingArea.Bottom - height;
                if (y < workingArea.Top)
                    y = workingArea.Top;

                form.Location = new Point(x, y);
            }
            catch (Exception ex)
            {
                LogWarning($"내역 창 위치 보정 오류: {ex.Message}");
                form.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        /// <summary>
        /// 스냅샷 내역 창에서 사용할 저장소를 반환합니다.
        /// </summary>
        public SnapshotStore GetSnapshotStore() => snapshotStore;

        /// <summary>
        /// 지정한 스냅샷 시점으로 데이터 상태를 되돌립니다.
        /// 현재 삭제 상태를 모두 복구한 뒤 스냅샷의 삭제 집합을 다시 적용합니다.
        /// </summary>
        public bool LoadSnapshot(string snapshotId)
        {
            try
            {
                if (imageManager == null || snapshotStore == null)
                    return false;

                var snapshot = snapshotStore.GetById(snapshotId);
                if (snapshot == null)
                {
                    LogWarning($"불러올 스냅샷을 찾을 수 없습니다: {snapshotId}");
                    return false;
                }

                bool ok = imageManager.ApplyDeletedFrameNumbers(snapshot.DeletedFrameNumbers);
                if (!ok)
                {
                    LogWarning("스냅샷 적용에 실패했습니다.");
                    return false;
                }

                // 메모리 목록/리스트/뷰어/통계 동기화
                RefreshAfterExternalChange();

                LogInfo($"스냅샷 불러오기 완료: 작성자={snapshot.AuthorId}, 삭제={snapshot.DeletedCount}개");

                MainWindow mainWindow = FindMainWindow();
                mainWindow?.SetStatusMessage(
                    $"스냅샷을 불러왔습니다. (제외 프레임 {snapshot.DeletedCount}개)",
                    MainWindow.StatusLevel.Success);

                return true;
            }
            catch (Exception ex)
            {
                LogWarning($"스냅샷 불러오기 예외: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 지정한 스냅샷을 이력에서만 제거합니다. (실제 데이터는 변경하지 않음)
        /// </summary>
        public bool DeleteSnapshot(string snapshotId)
        {
            try
            {
                if (snapshotStore == null)
                    return false;

                bool removed = snapshotStore.Remove(snapshotId);
                if (removed)
                    LogInfo($"스냅샷 이력 제거됨: {snapshotId}");
                return removed;
            }
            catch (Exception ex)
            {
                LogWarning($"스냅샷 제거 예외: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 내역 창이 열려 있으면 목록을 다시 로드합니다.
        /// </summary>
        private void RefreshSnapshotHistory()
        {
            if (snapshotHistoryForm != null && !snapshotHistoryForm.IsDisposed)
                snapshotHistoryForm.RefreshList();
        }

        /// <summary>
        /// 부모 계층에서 MainWindow를 찾습니다.
        /// </summary>
        private MainWindow FindMainWindow()
        {
            Control parent = this.Parent;
            while (parent != null)
            {
                if (parent is MainWindow mw)
                    return mw;
                parent = parent.Parent;
            }
            return null;
        }

        private void numFilterAngle2_ValueChanged(object sender, EventArgs e)
        {

        }
    }

    /// <summary>
    /// 필터 조건을 담는 클래스
    /// </summary>
    public class FilterConditions
    {
        public bool FilterThrottle { get; set; }
        public double ThrottleMin { get; set; }
        public double ThrottleMax { get; set; }
        public bool ExcludeThrottleZero { get; set; } // 스로틀 0 제외 옵션

        public bool FilterAngle { get; set; }
        public double AngleMin { get; set; }
        public double AngleMax { get; set; }

        public bool ExcludeDisabled { get; set; } // 기본 반전 이미지 제외

        public bool FilterResolution { get; set; }
        public string SelectedResolution { get; set; } // 선택된 해상도

        public bool RemoveDuplicateFrames { get; set; }
        public bool RemoveHighlightChanges { get; set; }
    }
}
