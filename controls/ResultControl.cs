using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;

namespace SimpleDonkeyManager.controls
{
    public partial class ResultControl : UserControl
    {
        private PlotView plotView = null;
        private PlotModel plotModel = null;
        private ChartDataModel trainingMetrics = null;
        private List<FrameData> trainingData = null;
        private string lastModelPath = null;
        private bool isValidationMode = false;
        private bool isValidating = false;
        private readonly StringBuilder validationLog = new StringBuilder();

        public ResultControl()
        {
            InitializeComponent();
            InitializeChartView();
            InitializeImageViewer();
            InitializeTooltips();

            // Resize 이벤트 핸들러
            this.Resize += ResultControl_Resize;
            this.Load += ResultControl_Load;
            this.VisibleChanged += ResultControl_VisibleChanged;
        }

        private void InitializeTooltips()
        {
            var toolTip = new ToolTip { AutoPopDelay = 8000, InitialDelay = 400, ReshowDelay = 200, ShowAlways = true };
            toolTip.SetToolTip(lblTotalEpochs, "총 학습 에포크(반복) 횟수입니다.");
            toolTip.SetToolTip(lblMinLoss, "학습 중 기록된 최소 손실값(Loss)입니다. 낮을수록 좋습니다.");
            toolTip.SetToolTip(lblMaxAccuracy, "학습 중 기록된 최고 정확도(Accuracy)입니다. 높을수록 좋습니다.");
            toolTip.SetToolTip(lblTrainingTime, "전체 학습에 소요된 시간입니다.");
            toolTip.SetToolTip(pnlResultChart, "에포크별 Loss / Accuracy 변화 그래프입니다.");
            toolTip.SetToolTip(validationViewer1, "검증된 프레임을 미리보기합니다. 재생 버튼으로 슬라이드쇼를 실행할 수 있습니다.");
            toolTip.SetToolTip(btnStartValidation, "학습된 모델로 프레임별 추론을 수행하여 실제값과 비교 검증합니다.");
            toolTip.SetToolTip(btnOpenModelFolder, "학습된 모델이 저장된 폴더를 파일 탐색기로 엽니다.");
        }

        private void ResultControl_Load(object sender, EventArgs e)
        {
            // Load 시점은 Width가 0일 수 있으므로 레이아웃 조정 생략
            // Resize 이벤트에서 처리
        }

        private void ResultControl_VisibleChanged(object sender, EventArgs e)
        {
            // 화면이 표시될 때 기존 데이터로 강제 재렌더링
            if (this.Visible && trainingMetrics != null)
            {
                try
                {
                    if (plotView != null)
                    {
                        plotView.Invalidate();
                        plotView.InvalidatePlot(true);
                    }
                }
                catch { }
            }
        }

        private void ResultControl_Resize(object sender, EventArgs e)
        {
            // 창 크기 변경 시 레이아웃 조정
            AdjustLayoutForWindowSize();
        }

        private void InitializeImageViewer()
        {
            try
            {
                // ValidationViewer는 Designer에서 이미 추가됨
                if (btnStartValidation != null)
                {
                    btnStartValidation.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"검증 뷰어 초기화 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 창 크기에 따라 레이아웃을 동적으로 조정합니다.
        /// </summary>
        private void AdjustLayoutForWindowSize()
        {
            try
            {
                if (tlpResultMain == null || tlpResultMain.IsDisposed)
                    return;

                if (pnlLeft == null || pnlRight == null)
                    return;

                int controlWidth = this.Width;

                // Width가 아직 확정되지 않은 경우 조정 생략
                if (controlWidth <= 0)
                    return;

                tlpResultMain.SuspendLayout();
                try
                {
                    if (controlWidth <= 900)
                    {
                        // 상하 레이아웃
                        tlpResultMain.ColumnCount = 1;
                        tlpResultMain.RowCount = 2;

                        tlpResultMain.ColumnStyles.Clear();
                        tlpResultMain.RowStyles.Clear();
                        tlpResultMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                        tlpResultMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                        tlpResultMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

                        tlpResultMain.SetCellPosition(pnlLeft, new TableLayoutPanelCellPosition(0, 0));
                        tlpResultMain.SetCellPosition(pnlRight, new TableLayoutPanelCellPosition(0, 1));
                    }
                    else
                    {
                        // 좌우 레이아웃
                        tlpResultMain.ColumnCount = 2;
                        tlpResultMain.RowCount = 1;

                        tlpResultMain.ColumnStyles.Clear();
                        tlpResultMain.RowStyles.Clear();
                        tlpResultMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67F));
                        tlpResultMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
                        tlpResultMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

                        tlpResultMain.SetCellPosition(pnlLeft, new TableLayoutPanelCellPosition(0, 0));
                        tlpResultMain.SetCellPosition(pnlRight, new TableLayoutPanelCellPosition(1, 0));
                    }
                }
                finally
                {
                    tlpResultMain.ResumeLayout(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"레이아웃 조정 오류: {ex.Message}");
            }
        }

        private void InitializeChartView()
        {
            try
            {
                // OxyPlot PlotView 생성
                plotView = new PlotView();
                plotView.Dock = DockStyle.Fill;
                plotView.Name = "plotViewResultChart";

                // 플롯 모델 초기화
                CreateResultPlotModel();
                plotView.Model = plotModel;

                // pnlResultChart에 추가
                pnlResultChart.Controls.Clear();
                pnlResultChart.Controls.Add(plotView);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"결과 그래프 초기화 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 결과 표시용 그래프 모델 생성
        /// </summary>
        private void CreateResultPlotModel()
        {
            plotModel = new PlotModel
            {
                Title = "Training Results",
                TitleFontSize = 12,
                Background = OxyColors.White,
                PlotAreaBorderColor = OxyColors.Black,
                PlotAreaBorderThickness = new OxyThickness(1)
            };

            // X축 (에포크)
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Epoch",
                TitleFontSize = 11,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColor.FromArgb(200, 200, 200, 200)
            };
            plotModel.Axes.Add(xAxis);

            // Y축 (손실값)
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Loss",
                TitleFontSize = 11,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColor.FromArgb(200, 200, 200, 200)
            };
            plotModel.Axes.Add(yAxis);

            // 훈련 손실 라인
            var trainSeries = new LineSeries
            {
                Title = "Train Loss",
                Color = OxyColors.Blue,
                StrokeThickness = 2,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };
            plotModel.Series.Add(trainSeries);

            // 검증 손실 라인
            var valSeries = new LineSeries
            {
                Title = "Validation Loss",
                Color = OxyColors.Red,
                StrokeThickness = 2,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };
            plotModel.Series.Add(valSeries);
        }

        /// <summary>
        /// 학습 메트릭을 결과 화면에 표시
        /// </summary>
        public void DisplayTrainingResults(ChartDataModel metrics)
        {
            try
            {
                if (metrics == null)
                    return;

                trainingMetrics = metrics;

                // 그래프 표시
                if (plotModel == null || plotModel.Series.Count < 2)
                {
                    CreateResultPlotModel();
                    if (plotView != null)
                        plotView.Model = plotModel;
                }

                // 기존 데이터 제거
                ((LineSeries)plotModel.Series[0]).Points.Clear();
                ((LineSeries)plotModel.Series[1]).Points.Clear();

                // 훈련 손실 데이터 추가
                var trainLosses = metrics.GetTrainLosses();
                var epochs = metrics.GetEpochs();
                for (int i = 0; i < Math.Min(trainLosses.Length, epochs.Length); i++)
                {
                    ((LineSeries)plotModel.Series[0]).Points.Add(
                        new DataPoint(epochs[i], trainLosses[i]));
                }

                // 검증 손실 데이터 추가
                var allMetrics = metrics.GetAllMetrics();
                var valEpochs = new List<double>();
                var valLosses = new List<double>();

                foreach (var metric in allMetrics)
                {
                    if (metric.ValidationLoss.HasValue)
                    {
                        valEpochs.Add(metric.Epoch);
                        valLosses.Add(metric.ValidationLoss.Value);
                    }
                }

                for (int i = 0; i < valLosses.Count; i++)
                {
                    ((LineSeries)plotModel.Series[1]).Points.Add(
                        new DataPoint(valEpochs[i], valLosses[i]));
                }

                // 플롯 새로고침
                if (plotView != null)
                {
                    plotView.InvalidatePlot(true);
                    plotView.Refresh();
                }
                else
                    plotModel.InvalidatePlot(true);

                // 결과 요약 표시
                UpdateResultSummary(metrics);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"그래프 표시 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateResultSummary(ChartDataModel metrics)
        {
            try
            {
                if (metrics == null)
                    return;

                // 총 에포크
                if (lblTotalEpochs != null)
                {
                    lblTotalEpochs.Text = $"총 에포크: {metrics.GetMetricCount()}";
                }

                // 최소 손실값
                if (lblMinLoss != null)
                {
                    float minLoss = metrics.GetMinimumLoss();
                    lblMinLoss.Text = $"최소 손실값: {minLoss:F4}";
                }

                // 최고 정확도
                if (lblMaxAccuracy != null)
                {
                    float maxAccuracy = metrics.GetMaximumAccuracy();
                    lblMaxAccuracy.Text = $"최고 정확도: {maxAccuracy:F4}";
                }

                // 소요 시간
                if (lblTrainingTime != null)
                {
                    TimeSpan elapsed = metrics.GetElapsedTime();
                    string timeStr;
                    if (elapsed.TotalSeconds < 60)
                    {
                        timeStr = $"{elapsed.TotalSeconds:F0}초";
                    }
                    else if (elapsed.TotalMinutes < 60)
                    {
                        timeStr = $"{elapsed.TotalMinutes:F1}분";
                    }
                    else
                    {
                        timeStr = $"{elapsed.TotalHours:F1}시간";
                    }
                    lblTrainingTime.Text = $"소요 시간: {timeStr}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"결과 요약 표시 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 학습된 모델 파일 경로를 설정합니다.
        /// </summary>
        public void SetModelPath(string modelPath)
        {
            lastModelPath = modelPath;
            if (btnOpenModelFolder != null)
            {
                bool hasPath = !string.IsNullOrEmpty(modelPath) && System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(modelPath));
                btnOpenModelFolder.Enabled = hasPath;
                string dir = hasPath ? System.IO.Path.GetDirectoryName(modelPath) : "(없음)";
                btnOpenModelFolder.Text = $"📂 저장된 폴더 열기  ({System.IO.Path.GetFileName(modelPath)})";
            }

            UpdateValidationButtonState();
        }

        private void BtnOpenModelFolder_Click(object sender, EventArgs e)
        {
            try
            {
                string dir = string.IsNullOrEmpty(lastModelPath)
                    ? null
                    : System.IO.Path.GetDirectoryName(lastModelPath);

                if (string.IsNullOrEmpty(dir) || !System.IO.Directory.Exists(dir))
                {
                    MessageBox.Show("모델 저장 폴더를 찾을 수 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 파일 탐색기에서 해당 파일을 선택한 상태로 열기
                if (!string.IsNullOrEmpty(lastModelPath) && System.IO.File.Exists(lastModelPath))
                    System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{lastModelPath}\"");
                else
                    System.Diagnostics.Process.Start("explorer.exe", $"\"{dir}\"");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"폴더 열기 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 학습 데이터를 설정합니다.
        /// </summary>
        public void SetTrainingData(List<FrameData> data)
        {
            if (data != null)
            {
                this.trainingData = new List<FrameData>(data);
            }

            try
            {
                // 학습 데이터가 있고 모델 경로가 준비되면 검증 버튼을 활성화합니다.
                UpdateValidationButtonState();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"검증 데이터 설정 오류: {ex.Message}");
            }
        }

        private void UpdateValidationButtonState()
        {
            if (btnStartValidation == null)
                return;

            bool hasData = trainingData != null && trainingData.Count > 0;
            bool hasModel = !string.IsNullOrEmpty(lastModelPath) && File.Exists(lastModelPath);
            btnStartValidation.Enabled = hasData && hasModel && !isValidating;
        }

        #region 학습 결과 검증

        private async void BtnStartValidation_Click(object sender, EventArgs e)
        {
            if (isValidating)
                return;

            if (trainingData == null || trainingData.Count == 0)
            {
                MessageBox.Show("검증할 학습 데이터가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(lastModelPath) || !File.Exists(lastModelPath))
            {
                MessageBox.Show("학습된 모델 파일을 찾을 수 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isValidating = true;
            btnStartValidation.Enabled = false;
            btnStartValidation.Text = "⏳ 검증 중...";

            // 진행률 상태바 초기화 및 표시 (이미지 미리보기 대신 진행도만 표시)
            int totalFrames = trainingData.Count(f => f != null && !string.IsNullOrEmpty(f.ImagePath));
            ShowValidationProgress(0, totalFrames);

            lock (validationLog) { validationLog.Clear(); }

            try
            {
                var results = await Task.Run(() => RunValidation());

                if (results == null)
                {
                    string log;
                    lock (validationLog) { log = validationLog.ToString(); }
                    if (log.Length > 1500)
                        log = log.Substring(log.Length - 1500);

                    string detail = string.IsNullOrWhiteSpace(log)
                        ? "추가 로그가 없습니다."
                        : log.Trim();

                    MessageBox.Show(
                        $"검증에 실패했습니다.\n\n[상세 로그]\n{detail}",
                        "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (results.Count == 0)
                {
                    MessageBox.Show(
                        "검증 결과가 비어 있습니다. 이미지 경로가 올바른지 확인하세요.\n" +
                        "(학습에 사용한 이미지 파일이 현재 위치에 존재해야 합니다.)",
                        "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 뷰어에 결과 로드
                validationViewer1.LoadResults(results);

                // 그래프를 검증 그래프로 전환
                isValidationMode = true;
                DisplayValidationGraph(results);

                // 요약 표시
                var summary = ValidationSummary.FromResults(results);
                UpdateValidationSummary(summary);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"검증 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isValidating = false;
                btnStartValidation.Text = "🔍 검증 시작";
                HideValidationProgress();
                UpdateValidationButtonState();
            }
        }

        /// <summary>
        /// validate_model.py 를 실행하여 프레임별 추론 결과를 받아옵니다.
        /// 백그라운드 스레드에서 호출됩니다.
        /// </summary>
        private List<ValidationResult> RunValidation()
        {
            try
            {
                string scriptPath = FindValidationScript();
                if (string.IsNullOrEmpty(scriptPath) || !File.Exists(scriptPath))
                {
                    AppendValidationError($"validate_model.py 를 찾을 수 없습니다: {scriptPath}");
                    return null;
                }

                string scriptDir = Path.GetDirectoryName(scriptPath);
                string pythonExe = FindPythonExecutable() ?? "python";

                // 입력 JSON 작성 (프레임 목록 + 실제값)
                string inputPath = Path.Combine(Path.GetTempPath(), $"validate_input_{Guid.NewGuid():N}.json");
                string outputPath = Path.Combine(Path.GetTempPath(), $"validate_output_{Guid.NewGuid():N}.json");

                WriteValidationInput(inputPath);

                var psi = new ProcessStartInfo
                {
                    FileName = pythonExe,
                    Arguments = $"\"{scriptPath}\" --model \"{lastModelPath}\" --input \"{inputPath}\" --output \"{outputPath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8,
                    WorkingDirectory = Directory.Exists(scriptDir) ? scriptDir : Environment.CurrentDirectory,
                };
                psi.EnvironmentVariables["PYTHONUTF8"] = "1";
                psi.EnvironmentVariables["PYTHONIOENCODING"] = "utf-8";
                psi.EnvironmentVariables["PYTHONUNBUFFERED"] = "1";
                psi.EnvironmentVariables["NO_ALBUMENTATIONS_UPDATE"] = "1";

                using (var process = new Process { StartInfo = psi })
                {
                    process.OutputDataReceived += (s, e) => HandleValidationOutput(e.Data);
                    process.ErrorDataReceived += (s, e) => HandleValidationOutput(e.Data);

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    bool exited = process.WaitForExit(30 * 60 * 1000); // 30분 타임아웃
                    if (!exited)
                    {
                        try { process.Kill(); } catch { }
                        AppendValidationError("검증이 타임아웃(30분)되었습니다.");
                        return null;
                    }

                    if (process.ExitCode != 0)
                    {
                        AppendValidationError($"검증 프로세스 오류 코드: {process.ExitCode}");
                        return null;
                    }
                }

                if (!File.Exists(outputPath))
                {
                    AppendValidationError("검증 결과 파일이 생성되지 않았습니다.");
                    return null;
                }

                var results = ParseValidationOutput(outputPath);

                try { File.Delete(inputPath); } catch { }
                try { File.Delete(outputPath); } catch { }

                return results;
            }
            catch (Exception ex)
            {
                AppendValidationError($"검증 실행 오류: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 검증할 프레임 목록(이미지 경로 + 실제값)을 JSON 으로 기록합니다.
        /// </summary>
        private void WriteValidationInput(string inputPath)
        {
            var sb = new StringBuilder();
            sb.Append("{\"frames\":[");

            bool first = true;
            foreach (var frame in trainingData)
            {
                if (frame == null || string.IsNullOrEmpty(frame.ImagePath))
                    continue;

                if (!first) sb.Append(',');
                first = false;

                string img = frame.ImagePath.Replace("\\", "\\\\").Replace("\"", "\\\"");
                sb.Append('{');
                sb.Append($"\"frame\":{frame.FrameNumber},");
                sb.Append($"\"image\":\"{img}\",");
                sb.Append($"\"actual_angle\":{frame.GetAngle().ToString(System.Globalization.CultureInfo.InvariantCulture)},");
                sb.Append($"\"actual_throttle\":{frame.GetThrottle().ToString(System.Globalization.CultureInfo.InvariantCulture)}");
                sb.Append('}');
            }

            sb.Append("]}");
            File.WriteAllText(inputPath, sb.ToString(), new UTF8Encoding(false));
        }

        /// <summary>
        /// validate_model.py 가 생성한 결과 JSON 을 파싱합니다.
        /// </summary>
        private List<ValidationResult> ParseValidationOutput(string outputPath)
        {
            using (var doc = System.Text.Json.JsonDocument.Parse(File.ReadAllText(outputPath)))
            {
                var root = doc.RootElement;
                var list = new List<ValidationResult>();

                if (root.TryGetProperty("results", out var resultsEl) &&
                    resultsEl.ValueKind == System.Text.Json.JsonValueKind.Array)
                {
                    foreach (var item in resultsEl.EnumerateArray())
                    {
                        list.Add(new ValidationResult
                        {
                            Frame = GetInt(item, "frame"),
                            ImagePath = GetString(item, "image"),
                            ActualAngle = GetDouble(item, "actual_angle"),
                            PredAngle = GetDouble(item, "pred_angle"),
                            ActualThrottle = GetDouble(item, "actual_throttle"),
                            PredThrottle = GetDouble(item, "pred_throttle"),
                            AngleError = GetDouble(item, "angle_error"),
                            ThrottleError = GetDouble(item, "throttle_error"),
                        });
                    }
                }

                return list;
            }
        }

        private static int GetInt(System.Text.Json.JsonElement el, string name)
            => el.TryGetProperty(name, out var v) && v.TryGetInt32(out int i) ? i : 0;

        private static double GetDouble(System.Text.Json.JsonElement el, string name)
            => el.TryGetProperty(name, out var v) && v.ValueKind == System.Text.Json.JsonValueKind.Number ? v.GetDouble() : 0.0;

        private static string GetString(System.Text.Json.JsonElement el, string name)
            => el.TryGetProperty(name, out var v) && v.ValueKind == System.Text.Json.JsonValueKind.String ? v.GetString() : null;

        private void AppendValidationError(string message)
        {
            Debug.WriteLine($"[검증] {message}");
            lock (validationLog)
            {
                validationLog.AppendLine(message);
            }
        }

        /// <summary>
        /// 검증 진행률 상태바를 표시하고 초기화합니다. (UI 스레드 안전)
        /// </summary>
        private void ShowValidationProgress(int current, int total)
        {
            if (lblValidationProgress == null || prgValidation == null)
                return;

            Action update = () =>
            {
                prgValidation.Maximum = total > 0 ? total : 1;
                prgValidation.Value = Math.Min(current, prgValidation.Maximum);
                prgValidation.Visible = true;
                lblValidationProgress.Text = total > 0
                    ? $"검증 진행 중...  {current} / {total} 프레임"
                    : "검증 준비 중...";
                lblValidationProgress.Visible = true;
            };

            if (InvokeRequired)
                BeginInvoke(update);
            else
                update();
        }

        /// <summary>
        /// 검증 진행률 상태바를 갱신합니다. (UI 스레드 안전)
        /// </summary>
        private void UpdateValidationProgress(int current, int total)
        {
            if (lblValidationProgress == null || prgValidation == null)
                return;

            Action update = () =>
            {
                if (total > 0)
                    prgValidation.Maximum = total;
                prgValidation.Value = Math.Min(current, prgValidation.Maximum);

                int percent = total > 0 ? (int)((double)current / total * 100) : 0;
                lblValidationProgress.Text = $"검증 진행 중...  {current} / {total} 프레임 ({percent}%)";
            };

            if (InvokeRequired)
                BeginInvoke(update);
            else
                update();
        }

        /// <summary>
        /// 검증 진행률 상태바를 숨깁니다. (UI 스레드 안전)
        /// </summary>
        private void HideValidationProgress()
        {
            if (lblValidationProgress == null || prgValidation == null)
                return;

            Action update = () =>
            {
                prgValidation.Visible = false;
                lblValidationProgress.Visible = false;
                lblValidationProgress.Text = "";
            };

            if (InvokeRequired)
                BeginInvoke(update);
            else
                update();
        }

        /// <summary>
        /// validate_model.py 의 출력 한 줄을 처리합니다.
        /// 진행 마커([PROGRESS]\t현재\t전체\t프레임번호)를 만나면
        /// 검증 진행률 상태바를 갱신합니다. (이미지 미리보기는 검증 속도를 위해 표시하지 않음)
        /// </summary>
        private void HandleValidationOutput(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return;

            Debug.WriteLine(line);

            const string marker = "[PROGRESS]";
            int idx = line.IndexOf(marker, StringComparison.Ordinal);
            if (idx < 0)
            {
                // 진행 마커가 아닌 일반 로그/오류 라인은 실패 진단을 위해 수집합니다.
                lock (validationLog)
                {
                    validationLog.AppendLine(line);
                    // 로그가 과도하게 커지지 않도록 제한
                    if (validationLog.Length > 8000)
                        validationLog.Remove(0, validationLog.Length - 8000);
                }
                return;
            }

            try
            {
                string payload = line.Substring(idx + marker.Length).Trim();
                string[] parts = payload.Split('\t');
                if (parts.Length >= 2)
                {
                    int current = int.TryParse(parts[0], out int c) ? c : 0;
                    int total = int.TryParse(parts[1], out int t) ? t : 0;

                    // 이미지 로드/표시 없이 진행률만 갱신하여 검증 속도를 극대화합니다.
                    UpdateValidationProgress(current, total);
                }
            }
            catch
            {
                // 진행 파싱 실패는 무시
            }
        }

        /// <summary>
        /// 검증 요약 라벨을 갱신합니다.
        /// </summary>
        private void UpdateValidationSummary(ValidationSummary summary)
        {
            if (summary == null)
                return;

            lblValCount.Text = $"검증 이미지 수: {summary.Count}장";
            lblValAvgAngle.Text = $"평균 조향 오차: {summary.AvgAngleError:F3}";
            lblValMaxAngle.Text = $"최대 조향 오차: {summary.MaxAngleError:F3}";
            lblValAvgThrottle.Text = $"평균 속도 오차: {summary.AvgThrottleError:F3}";
            lblValVerdict.Text = $"검증 결과: {summary.Verdict}";

            switch (summary.Verdict)
            {
                case "양호":
                    lblValVerdict.ForeColor = Color.SeaGreen;
                    break;
                case "보통":
                    lblValVerdict.ForeColor = Color.DarkOrange;
                    break;
                default:
                    lblValVerdict.ForeColor = Color.OrangeRed;
                    break;
            }
        }

        /// <summary>
        /// 좌측 그래프를 검증 그래프(실제 vs 예측 조향)로 전환합니다.
        /// </summary>
        private void DisplayValidationGraph(List<ValidationResult> results)
        {
            try
            {
                if (results == null || results.Count == 0)
                    return;

                var valModel = new PlotModel
                {
                    Title = "검증 결과 (실제 vs AI 예측 조향)",
                    TitleFontSize = 12,
                    Background = OxyColors.White,
                    PlotAreaBorderColor = OxyColors.Black,
                    PlotAreaBorderThickness = new OxyThickness(1)
                };

                valModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    Title = "프레임 인덱스",
                    TitleFontSize = 11,
                    MajorGridlineStyle = LineStyle.Solid,
                    MajorGridlineColor = OxyColor.FromArgb(200, 200, 200, 200)
                });

                valModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    Title = "조향값",
                    TitleFontSize = 11,
                    MajorGridlineStyle = LineStyle.Solid,
                    MajorGridlineColor = OxyColor.FromArgb(200, 200, 200, 200)
                });

                var actualSeries = new LineSeries
                {
                    Title = "실제 조향값",
                    Color = OxyColors.Blue,
                    StrokeThickness = 1.5
                };

                var predSeries = new LineSeries
                {
                    Title = "AI 예측 조향값",
                    Color = OxyColors.Red,
                    StrokeThickness = 1.5
                };

                for (int i = 0; i < results.Count; i++)
                {
                    actualSeries.Points.Add(new DataPoint(i, results[i].ActualAngle));
                    predSeries.Points.Add(new DataPoint(i, results[i].PredAngle));
                }

                valModel.Series.Add(actualSeries);
                valModel.Series.Add(predSeries);

                plotModel = valModel;
                if (plotView != null)
                {
                    plotView.Model = valModel;
                    plotView.InvalidatePlot(true);
                    plotView.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"검증 그래프 표시 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FindValidationScript()
        {
            return FindPythonScriptByName("validate_model.py");
        }

        private string FindPythonScriptByName(string scriptName)
        {
            try
            {
                return RuntimePathResolver.FindPythonScript(scriptName);
            }
            catch (Exception ex)
            {
                AppendValidationError($"{scriptName} 검색 오류: {ex.Message}");
            }

            return null;
        }

        private string FindPythonExecutable()
        {
            try
            {
                return RuntimePathResolver.FindLocalVenvPython();
            }
            catch (Exception ex)
            {
                AppendValidationError($"Python 실행 파일 검색 오류: {ex.Message}");
            }

            return null;
        }

        #endregion

        private void validationViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
