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
        private Logger logger;

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

            // 버튼 이벤트
            btnFilterStart.Click += BtnFilterStart_Click;
            btnFilterPreview.Click += BtnFilterPreview_Click;
            btnFilterReset.Click += BtnFilterReset_Click;

            // Logger 참조 (MainWindow에서 주입받을 때까지 null 가능)
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
                    UpdateStatistics();
                    return;
                }

                this.imageManager = manager;
                this.originalFrameDataList = new List<FrameData>(frameDataList);
                this.filteredFrameDataList = new List<FrameData>(frameDataList);

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

                // 중복 프레임 제거
                if (chkDelFrames != null)
                {
                    conditions.RemoveDuplicateFrames = chkDelFrames.Checked;
                    if (conditions.RemoveDuplicateFrames)
                        LogInfo($"추가 필터: 중복 프레임 제거");
                }

                // 조향 값 급변 구간 제거
                if (chkHighlightDel != null)
                {
                    conditions.RemoveHighlightChanges = chkHighlightDel.Checked;
                    if (conditions.RemoveHighlightChanges)
                        LogInfo($"추가 필터: 조향 값 급변 제거");
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
                if (imageManager == null || originalFrameDataList == null || originalFrameDataList.Count == 0 ||
                    filteredFrameDataList == null || filteredFrameDataList.Count == 0)
                {
                    SetSummaryData("0", "0", "0 (0.0%)", "0.0%");
                    return;
                }

                int totalFrames = originalFrameDataList.Count;
                int filteredFrames = filteredFrameDataList.Count;
                int deletedFrames = totalFrames - filteredFrames;
                double activeRatio = totalFrames > 0 ? (double)filteredFrames / totalFrames * 100 : 0;

                SetSummaryData(
                    totalFrames.ToString("N0"),
                    filteredFrames.ToString("N0"),
                    $"{deletedFrames} ({(totalFrames > 0 ? (double)deletedFrames / totalFrames * 100 : 0):F1}%)",
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
                // 필터 적용 - 필터링된 데이터로 학습 진행
                if (filteredFrameDataList == null || filteredFrameDataList.Count == 0)
                {
                    MessageBox.Show("필터링된 데이터가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogWarning("필터 적용 실패: 필터링된 데이터가 없음");
                    return;
                }

                // 필터 조건 수집
                FilterConditions conditions = GetFilterConditions();
                if (conditions == null)
                {
                    LogWarning("필터 조건을 수집할 수 없습니다");
                    return;
                }

                // 필터 적용 (영구 변경)
                ApplyFilters(conditions);

                LogInfo($"필터가 적용됨: {filteredFrameDataList.Count}개 프레임 선택");
                MessageBox.Show($"{filteredFrameDataList.Count}개의 프레임으로 학습을 시작합니다.", "필터 적용", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 필터된 데이터를 Training으로 전달
                MainWindow mainWindow = FindMainWindow();
                if (mainWindow != null && imageManager != null)
                {
                    string dataFolder = imageManager.SelectedFolderPath ?? "";
                    mainWindow.SetTrainingData(filteredFrameDataList, dataFolder);
                    mainWindow.SetStatusMessage(
                        $"② 데이터 필터링 —  필터 적용 완료 ({filteredFrameDataList.Count:N0}개 프레임)  →  ③ [학습 실행] 화면으로 이동해주세요.",
                        MainWindow.StatusLevel.Success);
                    LogInfo($"학습 데이터 전달: {filteredFrameDataList.Count}개 프레임, 폴더: {dataFolder}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"필터 적용 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"필터 적용 예외: {ex.Message}");
            }
        }

        private void BtnFilterPreview_Click(object sender, EventArgs e)
        {
            try
            {
                // 필터 미리보기 (임시 적용)
                FilterConditions conditions = GetFilterConditions();
                if (conditions == null)
                {
                    LogWarning("필터 조건을 수집할 수 없습니다");
                    return;
                }

                // 임시 필터링
                List<FrameData> previewList = ApplyFiltersTemporarily(conditions);
                if (previewList == null)
                {
                    LogWarning("필터링된 리스트가 null입니다");
                    return;
                }

                // UI 업데이트 (임시)
                filteredFrameDataList = previewList;
                if (imageList != null)
                {
                    imageList.LoadFrames(filteredFrameDataList);
                }
                UpdateStatistics();

                LogInfo($"필터 미리보기: {filteredFrameDataList.Count}개 프레임");

                MainWindow mainWindowPreview = FindMainWindow();
                mainWindowPreview?.SetStatusMessage(
                    $"② 데이터 필터링 —  미리보기: {filteredFrameDataList.Count:N0}개 프레임 선택됨  →  결과 확인 후 [필터 적용]을 눌러주세요.",
                    MainWindow.StatusLevel.Info);

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

                MessageBox.Show($"미리보기: {filteredFrameDataList.Count}개 프레임이 선택됩니다.", "필터 미리보기", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"필터 미리보기 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"필터 미리보기 예외: {ex.Message}");
            }
        }

        private void BtnFilterReset_Click(object sender, EventArgs e)
        {
            try
            {
                // 필터 초기화
                if (imageManager == null || originalFrameDataList == null || originalFrameDataList.Count == 0)
                {
                    LogWarning("필터 초기화 실패: 원본 데이터가 없습니다");
                    return;
                }

                filteredFrameDataList = new List<FrameData>(originalFrameDataList);
                if (imageList != null)
                {
                    imageList.LoadFrames(filteredFrameDataList);
                }
                UpdateStatistics();

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
