using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;

namespace SimpleDonkeyManager
{
    public partial class TrainingControl : UserControl
    {
        private Logger logger;
        private List<FrameData> fullFrameDataList = new List<FrameData>();      // 전체 데이터
        private List<FrameData> filteredFrameDataList = new List<FrameData>();   // 필터된 데이터
        private List<FrameData> currentTrainingData = new List<FrameData>();     // 현재 사용할 데이터
        private string selectedDataFolder = "";
        private string modelSaveFolder = "";
        private Process trainingProcess;
        private bool isTraining = false;
        private bool isFiltered = false;  // 필터 여부
        private bool userStopped = false; // 사용자가 학습을 중지했는지 여부
        private int currentEpoch = 0;     // 로그에서 추적한 현재 에포크 (loss 줄에 epoch이 없을 때 사용)
        private int totalEpochs = 0;      // 전체 에포크 수 (Epoch x/total 에서 추출)
        private double lastTrendMetric = double.NaN; // 직전 에포크의 정확도(또는 loss 폴백) 추적용
        private int lastLabelEpoch = 0;   // 라벨을 마지막으로 갱신한 에포크 (에포크당 1회 갱신 보장)
        private ChartDataModel chartDataModel = new ChartDataModel();  // 그래프 데이터 모델
        private PlotView plotView = null;  // OxyPlot 뷰어
        private PlotModel plotModel = null;  // 플롯 모델 (Donkey UI 형식)

        public TrainingControl()
        {
            InitializeComponent();
            InitializeTrainingControl();
            InitializeChartView();
        }

        private void InitializeTrainingControl()
        {
            try
            {
                // 모델 타입 초기화
                cmbModelType.Items.Clear();
                cmbModelType.Items.Add("linear");
                cmbModelType.Items.Add("inferred");
                cmbModelType.Items.Add("tensorrt_linear");
                cmbModelType.Items.Add("tflite_linear");
                cmbModelType.SelectedIndex = 0;

                // 버튼 이벤트
                btnSelectModelPath.Click += BtnSelectModelPath_Click;
                btnStartTraining.Click += BtnStartTraining_Click;
                btnCheckTrainingResult.Click += BtnCheckTrainingResult_Click;

                // Resize 이벤트 핸들러
                this.Resize += TrainingControl_Resize;
                this.Load += TrainingControl_Load;

                InitializeTooltips();

                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"TrainingControl 초기화 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeTooltips()
        {
            var toolTip = new ToolTip { AutoPopDelay = 8000, InitialDelay = 400, ReshowDelay = 200, ShowAlways = true };
            toolTip.SetToolTip(cmbModelType, "학습에 사용할 모델 타입을 선택합니다.\n(linear, inferred, tensorrt_linear, tflite_linear)");
            toolTip.SetToolTip(txtModelPath, "학습된 모델이 저장될 경로입니다.");
            toolTip.SetToolTip(btnSelectModelPath, "모델을 저장할 폴더를 선택합니다.");
            toolTip.SetToolTip(btnStartTraining, "설정된 조건으로 학습을 시작하거나 중지합니다.");
            toolTip.SetToolTip(btnCheckTrainingResult, "학습 완료 후 결과 탭으로 이동하여 결과를 확인합니다.");
            toolTip.SetToolTip(prgTrainingProgress, "현재 학습 진행률을 표시합니다.");
            toolTip.SetToolTip(lstTrainingLog, "학습 과정의 로그 메시지를 실시간으로 표시합니다.");
        }

        private void TrainingControl_Load(object sender, EventArgs e)
        {
            // 로드 시 초기 레이아웃 조정
            AdjustLayoutForWindowSize();
        }

        private void TrainingControl_Resize(object sender, EventArgs e)
        {
            // 창 크기 변경 시 레이아웃 조정
            AdjustLayoutForWindowSize();
        }

        /// <summary>
        /// 창 크기에 따라 레이아웃을 동적으로 조정합니다.
        /// </summary>
        private void AdjustLayoutForWindowSize()
        {
            try
            {
                if (splMainTraining == null || splMainTraining.IsDisposed)
                    return;

                int controlWidth = this.Width;
                int controlHeight = this.Height;

                // 작은 화면 (폭이 900 이하): 상하 레이아웃
                // 큰 화면 (폭이 900 초과): 좌우 레이아웃
                if (controlWidth <= 900)
                {
                    // 수직 방향 (상하)
                    if (splMainTraining.Orientation != Orientation.Horizontal)
                    {
                        splMainTraining.Orientation = Orientation.Horizontal;
                        LogDetail($"레이아웃 변경: 수직 방향 (폭: {controlWidth}px)");
                    }

                    // 상하 비율 조정: 위쪽 50%, 아래쪽 50%
                    int splitterDistance = (int)(controlHeight * 0.5);
                    if (splMainTraining.SplitterDistance != splitterDistance && splitterDistance > splMainTraining.Panel1MinSize && splitterDistance < controlHeight - splMainTraining.Panel2MinSize)
                    {
                        splMainTraining.SplitterDistance = splitterDistance;
                    }
                }
                else
                {
                    // 수평 방향 (좌우)
                    if (splMainTraining.Orientation != Orientation.Vertical)
                    {
                        splMainTraining.Orientation = Orientation.Vertical;
                        LogDetail($"레이아웃 변경: 수평 방향 (폭: {controlWidth}px)");
                    }

                    // 좌우 비율 조정: 좌측 약 60%, 우측 약 40%
                    int splitterDistance = (int)(controlWidth * 0.6);
                    if (splMainTraining.SplitterDistance != splitterDistance && splitterDistance > splMainTraining.Panel1MinSize && splitterDistance < controlWidth - splMainTraining.Panel2MinSize)
                    {
                        splMainTraining.SplitterDistance = splitterDistance;
                    }
                }
            }
            catch (Exception ex)
            {
                LogDetail($"레이아웃 조정 오류: {ex.Message}");
            }
        }

        private void InitializeChartView()
        {
            try
            {
                // OxyPlot PlotView 생성 (Donkey UI 형식)
                plotView = new PlotView();
                plotView.Dock = DockStyle.Fill;
                plotView.Name = "plotViewChart";
                plotView.Margin = new Padding(5);

                // 플롯 모델 초기화 (손실값 표시)
                CreatePlotModel();
                plotView.Model = plotModel;

                // pnlChartRight에 추가
                pnlChartRight.Controls.Clear();
                pnlChartRight.Controls.Add(plotView);

                LogDetail("그래프 뷰 초기화 완료");
            }
            catch (Exception ex)
            {
                LogDetail($"그래프 초기화 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// Donkey UI 형식의 학습 그래프 모델 생성
        /// 훈련 손실과 검증 손실을 함께 표시
        /// </summary>
        private void CreatePlotModel()
        {
            plotModel = new PlotModel
            {
                Title = "Training Loss Progress",
                TitleFontSize = 14,
                Background = OxyColors.White,
                PlotAreaBorderColor = OxyColors.Black,
                PlotAreaBorderThickness = new OxyThickness(1)
            };

            // X축 (에포크)
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Epoch",
                TitleFontSize = 12,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColor.FromArgb(200, 200, 200, 200)
            };
            plotModel.Axes.Add(xAxis);

            // Y축 (손실값)
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Loss",
                TitleFontSize = 12,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColor.FromArgb(200, 200, 200, 200)
            };
            plotModel.Axes.Add(yAxis);

            // 훈련 손실 라인 시리즈 (파란색)
            var trainSeries = new LineSeries
            {
                Title = "Train Loss",
                Color = OxyColors.Blue,
                StrokeThickness = 2,
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerFill = OxyColors.Blue
            };
            plotModel.Series.Add(trainSeries);

            // 검증 손실 라인 시리즈 (빨간색)
            var valSeries = new LineSeries
            {
                Title = "Validation Loss",
                Color = OxyColors.Red,
                StrokeThickness = 2,
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerFill = OxyColors.Red
            };
            plotModel.Series.Add(valSeries);
        }

        /// <summary>
        /// 그래프에 새 에포크 데이터 추가 및 실시간 업데이트
        /// </summary>
        private void UpdateChartWithMetric(int epoch, float trainLoss, float? validationLoss = null)
        {
            try
            {
                if (plotView == null || plotModel == null)
                    return;

                // ChartDataModel에 메트릭 추가
                chartDataModel.AddMetric(epoch, trainLoss, validationLoss);

                // UI 스레드에서 업데이트
                if (plotView.InvokeRequired)
                {
                    plotView.Invoke(new Action(() => UpdateChartUI()));
                }
                else
                {
                    UpdateChartUI();
                }
            }
            catch (Exception ex)
            {
                LogDetail($"그래프 업데이트 오류: {ex.Message}");
            }
        }

        private void UpdateChartUI()
        {
            try
            {
                if (plotModel == null || chartDataModel == null)
                    return;

                // 기존 시리즈 데이터 제거 (처음부터 재생성)
                if (plotModel.Series.Count >= 2)
                {
                    ((LineSeries)plotModel.Series[0]).Points.Clear();
                    ((LineSeries)plotModel.Series[1]).Points.Clear();
                }

                // 훈련 손실 데이터 추가
                var trainLosses = chartDataModel.GetTrainLosses();
                var epochs = chartDataModel.GetEpochs();
                for (int i = 0; i < Math.Min(trainLosses.Length, epochs.Length); i++)
                {
                    ((LineSeries)plotModel.Series[0]).Points.Add(
                        new DataPoint(epochs[i], trainLosses[i]));
                }

                // 검증 손실 데이터 추가
                var validationLosses = chartDataModel.GetValidationLosses();
                var metricsCount = chartDataModel.GetMetricCount();
                var filteredEpochs = new List<double>();

                for (int i = 0; i < metricsCount; i++)
                {
                    var metrics = chartDataModel.GetAllMetrics();
                    if (i < metrics.Count && metrics[i].ValidationLoss.HasValue)
                    {
                        filteredEpochs.Add(metrics[i].Epoch);
                    }
                }

                for (int i = 0; i < Math.Min(validationLosses.Length, filteredEpochs.Count); i++)
                {
                    ((LineSeries)plotModel.Series[1]).Points.Add(
                        new DataPoint(filteredEpochs[i], validationLosses[i]));
                }

                // 플롯 새로고침
                plotModel.InvalidatePlot(true);
            }
            catch (Exception ex)
            {
                LogDetail($"그래프 UI 업데이트 오류: {ex.Message}");
            }
        }

        public void SetLogger(Logger log)
        {
            logger = log;
        }

        /// <summary>
        /// 전체 로드된 데이터를 설정합니다 (필터링 이전)
        /// </summary>
        public void SetFullFrameData(List<FrameData> frameDataList, string dataFolder)
        {
            try
            {
                fullFrameDataList = frameDataList ?? new List<FrameData>();
                selectedDataFolder = dataFolder;
                isFiltered = false;
                currentTrainingData = new List<FrameData>(fullFrameDataList);  // 현재 데이터를 전체 데이터로 설정

                UpdateUI();
                LogDetail($"전체 데이터 설정: {fullFrameDataList.Count}개 프레임, 폴더: {dataFolder}");
            }
            catch (Exception ex)
            {
                LogDetail($"전체 데이터 설정 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 필터된 데이터를 설정합니다
        /// </summary>
        public void SetTrainData(List<FrameData> frameDataList, string dataFolder)
        {
            try
            {
                filteredFrameDataList = frameDataList ?? new List<FrameData>();
                selectedDataFolder = dataFolder;
                isFiltered = true;
                currentTrainingData = new List<FrameData>(filteredFrameDataList);  // 현재 데이터를 필터된 데이터로 설정

                UpdateUI();
                LogDetail($"필터된 데이터 설정: {filteredFrameDataList.Count}개 프레임, 폴더: {dataFolder}");
            }
            catch (Exception ex)
            {
                LogDetail($"필터된 데이터 설정 오류: {ex.Message}");
            }
        }

        private void UpdateUI()
        {
            try
            {
                // 데이터 상태 업데이트
                if (lblDataStatus != null)
                {
                    string status = isFiltered ? "필터됨" : "전체";
                    int count = currentTrainingData.Count;

                    if (count > 0)
                    {
                        lblDataStatus.Text = $"데이터: {count}개 프레임 ({status}) 준비됨";
                        lblDataStatus.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDataStatus.Text = "데이터: 준비되지 않음";
                        lblDataStatus.ForeColor = Color.Red;
                    }
                }

                // 모델 경로 표시
                if (txtModelPath != null)
                {
                    txtModelPath.Text = modelSaveFolder;
                }
            }
            catch (Exception ex)
            {
                LogDetail($"UI 업데이트 오류: {ex.Message}");
            }
        }

        private void BtnSelectModelPath_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "모델을 저장할 폴더를 선택하세요";

                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        modelSaveFolder = folderDialog.SelectedPath;
                        UpdateUI();
                        LogDetail($"모델 저장 폴더 선택: {modelSaveFolder}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"폴더 선택 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogDetail($"폴더 선택 오류: {ex.Message}");
            }
        }

        private void BtnStartTraining_Click(object sender, EventArgs e)
        {
            try
            {
                if (isTraining)
                {
                    StopTraining();
                    return;
                }

                // 유효성 검사
                if (currentTrainingData.Count == 0)
                {
                    MessageBox.Show("학습할 데이터가 없습니다. 데이터를 먼저 로드하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(selectedDataFolder))
                {
                    MessageBox.Show("데이터 폴더가 설정되지 않았습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(modelSaveFolder))
                {
                    MessageBox.Show("모델을 저장할 폴더를 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string modelType = cmbModelType.SelectedItem?.ToString() ?? "linear";

                // 모델 파일명 생성
                string modelFileName = $"model_{DateTime.Now:yyyyMMdd_HHmmss}.h5";
                string fullModelPath = Path.Combine(modelSaveFolder, modelFileName);

                LogInfo($"학습 시작 (타입={modelType}, 데이터={currentTrainingData.Count}개)");

                StartTrainingAsync(selectedDataFolder, fullModelPath, modelType);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"학습 시작 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"학습 시작 오류: {ex.Message}");
            }
        }

        private void StartTrainingAsync(string dataFolder, string modelPath, string modelType)
        {
            try
            {
                isTraining = true;
                userStopped = false;
                btnStartTraining.Text = "⏹ 학습 중지";
                prgTrainingProgress.Value = 0;
                lstTrainingLog.Items.Clear();

                // 진행 정보 라벨 초기화
                currentEpoch = 0;
                totalEpochs = 0;
                lastTrendMetric = double.NaN;
                lastLabelEpoch = 0;
                lblEpochInfo.Text = "Epoch -/-";
                lblEpochInfo.ForeColor = Color.FromArgb(40, 40, 40);
                lblAccuracyTrend.Text = "정확성 증가율 : -%";
                lblAccuracyTrend.ForeColor = Color.FromArgb(40, 40, 40);

                // 상태막 업데이트
                FindMainWindow()?.SetStatusMessage(
                    $"③ 학습 실행 —  학습을 시작하는 중입니다... ({currentTrainingData.Count:N0}개 프레임)",
                    MainWindow.StatusLevel.Info);

                // 그래프 초기화
                chartDataModel.Clear();
                CreatePlotModel();
                if (plotView != null)
                {
                    plotView.Model = plotModel;
                }
                currentEpoch = 0;

                Task.Run(() =>
                {
                    try
                    {
                        // Python 스크립트 경로 찾기
                        string pythonScriptPath = FindPythonScript();

                        if (string.IsNullOrEmpty(pythonScriptPath) || !File.Exists(pythonScriptPath))
                        {
                            LogDetail($"Python 스크립트를 찾을 수 없음: train.py");
                            return;
                        }

                        if (!Directory.Exists(dataFolder))
                        {
                            LogDetail($"데이터 폴더를 찾을 수 없음: {dataFolder}");
                            return;
                        }

                        // 모델 저장 폴더 생성
                        string modelDir = Path.GetDirectoryName(modelPath);
                        if (!Directory.Exists(modelDir))
                        {
                            try
                            {
                                Directory.CreateDirectory(modelDir);
                                LogDetail($"모델 저장 폴더 생성: {modelDir}");
                            }
                            catch (Exception ex)
                            {
                                LogDetail($"모델 폴더 생성 실패: {ex.Message}");
                                return;
                            }
                        }

                        LogDetail($"Python 스크립트 경로: {pythonScriptPath}");
                        LogDetail($"데이터 폴더: {dataFolder}");
                        LogDetail($"모델 저장 경로: {modelPath}");

                        // Python 실행 파일 찾기
                        // 우선순위:
                        // 1. 시스템 전역 Python (PATH에 있는 python)
                        // 2. 로컬 가상환경 (donkey_env\Scripts\python.exe)
                        string pythonExe = "python";  // 기본값: 시스템 Python

                        // 로컬 가상환경이 있으면 우선 사용
                        string localVenvPython = FindPythonExecutable();
                        if (!string.IsNullOrEmpty(localVenvPython) && File.Exists(localVenvPython))
                        {
                            pythonExe = localVenvPython;
                            LogDetail($"로컬 가상환경 Python 사용: {pythonExe}");
                        }
                        else
                        {
                            LogDetail($"시스템 전역 Python 사용: {pythonExe}");
                        }

                        // ----- 학습 전 tub 데이터 준비 단계 -----
                        // donkey 폴더처럼 구형(v3) 형식 데이터는 catalog/manifest 가 없어
                        // donkeycar 5.x 학습이 실패합니다. prepare_tub.py 를 실행하여
                        // tub v2 형식(manifest.json + catalog)으로 변환하고 config.py 를 준비합니다.
                        // (이미 변환된 폴더는 prepare_tub.py 내부에서 자동으로 건너뜁니다.)
                        bool prepared = PrepareTubData(pythonExe, pythonScriptPath, dataFolder);
                        if (!prepared)
                        {
                            LogWarning("학습 데이터 준비(catalog 생성)에 실패했습니다. 학습을 중단합니다.");
                            FindMainWindow()?.SetStatusMessage(
                                "③ 학습 실행 —  학습 데이터 준비(catalog 생성)에 실패했습니다.  로그를 확인하세요.",
                                MainWindow.StatusLevel.Error);
                            return;
                        }

                        // Python 인수 구성
                        string arguments = $"--tubs \"{dataFolder}\" --model \"{modelPath}\"";

                        if (!string.IsNullOrWhiteSpace(modelType))
                        {
                            arguments += $" --type {modelType}";
                        }

                        ProcessStartInfo psi = new ProcessStartInfo
                        {
                            FileName = pythonExe,
                            Arguments = $"\"{pythonScriptPath}\" {arguments}",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true,
                            StandardOutputEncoding = System.Text.Encoding.UTF8,
                            StandardErrorEncoding = System.Text.Encoding.UTF8,
                        };

                        // Python이 UTF-8 모드로 동작하도록 환경 변수 설정
                        psi.EnvironmentVariables["PYTHONUTF8"] = "1";
                        psi.EnvironmentVariables["PYTHONIOENCODING"] = "utf-8";
                        // 출력 버퍼링 비활성화 (실시간 로그/그래프 갱신을 위해 필수)
                        psi.EnvironmentVariables["PYTHONUNBUFFERED"] = "1";

                        // train.py가 위치한 폴더(python)를 작업 디렉터리로 설정하여
                        // DEPLOYMENT_GUIDE의 실행 환경(config 탐색 등)과 동일하게 동작하도록 합니다.
                        try
                        {
                            string scriptDir = Path.GetDirectoryName(pythonScriptPath);
                            if (!string.IsNullOrEmpty(scriptDir) && Directory.Exists(scriptDir))
                            {
                                psi.WorkingDirectory = scriptDir;
                            }
                        }
                        catch
                        {
                            // 작업 디렉터리 설정 실패 시 기본값 사용
                        }

                        LogDetail($"실행 명령어: {psi.FileName} {psi.Arguments}");

                        trainingProcess = new Process { StartInfo = psi };
                        trainingProcess.OutputDataReceived += TrainingProcess_OutputDataReceived;
                        trainingProcess.ErrorDataReceived += TrainingProcess_ErrorDataReceived;
                        trainingProcess.Start();
                        trainingProcess.BeginOutputReadLine();
                        trainingProcess.BeginErrorReadLine();

                        // 프로세스 종료 대기 (실제 학습은 데이터 규모에 따라 수 시간 소요될 수 있음)
                        bool exited = trainingProcess.WaitForExit(3 * 60 * 60 * 1000); // 3시간 타임아웃

                        if (!exited)
                        {
                            LogWarning("학습 타임아웃 (3시간): 프로세스를 강제 종료합니다.");
                            trainingProcess.Kill();
                            FindMainWindow()?.SetStatusMessage(
                                "③ 학습 실행 —  학습이 타임아웃(3시간)으로 강제 종료되었습니다.  데이터 또는 Python 환경을 확인하세요.",
                                MainWindow.StatusLevel.Error);
                        }
                        else
                        {
                            int exitCode = trainingProcess.ExitCode;
                            LogDetail($"학습 프로세스 종료 (코드: {exitCode})");

                            if (exitCode == 0)
                            {
                                LogInfo("학습이 정상 완료되었습니다.");
                                // 학습 완료 후 메트릭 저장
                                SaveTrainingMetrics(modelPath);

                                // 학습 완료 후 결과 데이터를 ResultControl로 전달
                                NotifyTrainingCompleted(modelPath);

                                FindMainWindow()?.SetStatusMessage(
                                    "③ 학습 실행 —  학습 완료!  →  [학습 결과 확인] 버튼을 눈러 ④ 결과를 확인하세요.",
                                    MainWindow.StatusLevel.Success);
                            }
                            else if (userStopped)
                            {
                                // 사용자가 중지한 경우에도 donkeycar 가 ModelCheckpoint 로
                                // 그때까지의 best 모델을 저장해 두었다면 결과를 전달합니다.
                                if (File.Exists(modelPath))
                                {
                                    LogInfo($"학습이 중지되었지만 모델이 저장되었습니다: {modelPath}");
                                    SaveTrainingMetrics(modelPath);
                                    NotifyTrainingCompleted(modelPath);

                                    FindMainWindow()?.SetStatusMessage(
                                        "③ 학습 실행 —  학습이 중지되었습니다.  중지 시점까지의 모델이 저장되었습니다.  →  [학습 결과 확인] 버튼을 눌러 ④ 결과를 확인하세요.",
                                        MainWindow.StatusLevel.Success);
                                }
                                else
                                {
                                    LogWarning("학습이 중지되었으나 저장된 모델 파일이 없습니다 (첫 에포크 완료 전에 중지된 것으로 보입니다).");
                                    FindMainWindow()?.SetStatusMessage(
                                        "③ 학습 실행 —  학습이 중지되었습니다.  저장된 모델이 없습니다(첫 에포크 완료 전 중지).",
                                        MainWindow.StatusLevel.Warning);
                                }
                            }
                            else
                            {
                                LogWarning($"학습 프로세스 오류 코드: {exitCode}");
                                FindMainWindow()?.SetStatusMessage(
                                    $"③ 학습 실행 —  학습이 오류로 종료되었습니다 (코드 {exitCode}).  로그를 확인하세요.",
                                    MainWindow.StatusLevel.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogWarning($"학습 프로세스 오류: {ex.Message}");
                        LogDetail($"스택 추적: {ex.StackTrace}");
                    }
                    finally
                    {
                        isTraining = false;
                        if (btnStartTraining != null && !btnStartTraining.IsDisposed)
                        {
                            btnStartTraining.Invoke((Action)(() =>
                            {
                                btnStartTraining.Text = "▷ 학습 시작";
                                prgTrainingProgress.Value = 100;
                                lblProgress.Text = "100%";
                            }));
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LogDetail($"비동기 학습 시작 오류: {ex.Message}");
                isTraining = false;
            }
        }

        /// <summary>
        /// Python 실행 파일을 찾습니다.
        /// 우선순위:
        /// 1. 로컬 가상환경 (donkey_env\Scripts\python.exe)
        /// 2. null 반환 (시스템 전역 Python "python" 사용)
        /// </summary>
        private string FindPythonExecutable()
        {
            try
            {
                LogDetail("로컬 가상환경 Python 검색 시작...");
                foreach (string root in RuntimePathResolver.GetSearchRoots())
                {
                    LogDetail($"  검색 위치: {Path.Combine(root, "donkey_env", "Scripts", "python.exe")}");
                }

                string? pythonPath = RuntimePathResolver.FindLocalVenvPython();
                if (!string.IsNullOrEmpty(pythonPath))
                {
                    LogDetail($"로컬 가상환경 Python 찾음: {pythonPath}");
                    return pythonPath;
                }

                LogDetail("로컬 가상환경을 찾을 수 없습니다. 시스템 전역 Python을 사용합니다.");
                return null;
            }
            catch (Exception ex)
            {
                LogDetail($"Python 실행 파일 검색 오류: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 학습 전에 데이터 폴더를 donkeycar 5.x 가 요구하는 tub v2 형식으로 준비합니다.
        /// prepare_tub.py 를 동기 실행하여 (1) catalog/manifest 자동 생성,
        /// (2) config.py 생성을 수행하고, 출력은 학습 로그에 표시합니다.
        /// 이미 변환된 폴더(manifest.json 존재)는 스크립트 내부에서 건너뜁니다.
        /// </summary>
        /// <returns>준비 성공 시 true, 실패 시 false</returns>
        private bool PrepareTubData(string pythonExe, string trainScriptPath, string dataFolder)
        {
            try
            {
                // prepare_tub.py 는 train.py 와 같은 python 폴더에 있습니다.
                string scriptDir = Path.GetDirectoryName(trainScriptPath);
                string prepareScript = Path.Combine(scriptDir, "prepare_tub.py");

                if (!File.Exists(prepareScript))
                {
                    // 준비 스크립트가 없으면 변환 없이 진행 (이미 tub v2 형식일 수 있음)
                    LogWarning($"prepare_tub.py 를 찾을 수 없습니다. catalog 자동 생성을 건너뜁니다: {prepareScript}");
                    return true;
                }

                LogInfo("학습 데이터 준비 중... (catalog/config 자동 생성)");
                LogDetail($"준비 스크립트: {prepareScript}");
                LogDetail($"대상 데이터 폴더: {dataFolder}");

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = pythonExe,
                    Arguments = $"\"{prepareScript}\" --tubs \"{dataFolder}\" --config-dir \"{scriptDir}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    StandardOutputEncoding = System.Text.Encoding.UTF8,
                    StandardErrorEncoding = System.Text.Encoding.UTF8,
                    WorkingDirectory = scriptDir,
                };

                psi.EnvironmentVariables["PYTHONUTF8"] = "1";
                psi.EnvironmentVariables["PYTHONIOENCODING"] = "utf-8";
                psi.EnvironmentVariables["PYTHONUNBUFFERED"] = "1";
                psi.EnvironmentVariables["NO_ALBUMENTATIONS_UPDATE"] = "1";

                using (Process prepareProcess = new Process { StartInfo = psi })
                {
                    prepareProcess.OutputDataReceived += (s, e) =>
                    {
                        if (!string.IsNullOrWhiteSpace(e.Data))
                        {
                            LogDetail(e.Data);
                        }
                    };
                    prepareProcess.ErrorDataReceived += (s, e) =>
                    {
                        if (!string.IsNullOrWhiteSpace(e.Data))
                        {
                            LogDetail(e.Data);
                        }
                    };

                    prepareProcess.Start();
                    prepareProcess.BeginOutputReadLine();
                    prepareProcess.BeginErrorReadLine();

                    // 데이터 변환은 보통 수 분 이내. 넉넉히 30분 타임아웃.
                    bool exited = prepareProcess.WaitForExit(30 * 60 * 1000);
                    if (!exited)
                    {
                        LogWarning("학습 데이터 준비가 타임아웃(30분)되었습니다. 프로세스를 종료합니다.");
                        try { prepareProcess.Kill(); } catch { }
                        return false;
                    }

                    int exitCode = prepareProcess.ExitCode;
                    LogDetail($"데이터 준비 프로세스 종료 (코드: {exitCode})");

                    if (exitCode == 0)
                    {
                        LogInfo("학습 데이터 준비 완료.");
                        return true;
                    }

                    LogWarning($"학습 데이터 준비 실패 (코드: {exitCode}).");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogWarning($"학습 데이터 준비 오류: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// train.py 스크립트 경로를 찾습니다.
        /// 여러 위치에서 검색합니다:
        /// 1. 실행 파일 디렉토리\python\train.py
        /// 2. 프로젝트 솔루션 디렉토리\python\train.py (상대 경로)
        /// 3. 현재 작업 디렉토리\python\train.py
        /// </summary>
        private string FindPythonScript()
        {
            try
            {
                LogDetail($"Python 스크립트 검색 시작...");
                LogDetail($"  BaseDirectory: {AppDomain.CurrentDomain.BaseDirectory}");
                LogDetail($"  CurrentDirectory: {Directory.GetCurrentDirectory()}");

                foreach (string root in RuntimePathResolver.GetSearchRoots())
                {
                    LogDetail($"  검색 위치: {Path.Combine(root, "python", "train.py")}");
                }

                string? pythonScriptPath = RuntimePathResolver.FindPythonScript("train.py");
                if (!string.IsNullOrEmpty(pythonScriptPath))
                {
                    LogDetail($"  찾음: {pythonScriptPath}");
                    return pythonScriptPath;
                }

                LogWarning("train.py를 찾을 수 없습니다. 실행 폴더 또는 프로젝트 루트의 python 폴더를 확인하세요.");
                return null;
            }
            catch (Exception ex)
            {
                LogWarning($"Python 스크립트 검색 오류: {ex.Message}");
                return null;
            }
        }

        private void TrainingProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                LogDetail($"[출력] {e.Data}");

                // 로그 표시
                if (lstTrainingLog != null && !lstTrainingLog.IsDisposed)
                {
                    lstTrainingLog.Invoke((Action)(() =>
                    {
                        lstTrainingLog.Items.Add($"[{DateTime.Now:HH:mm:ss}] {e.Data}");
                        lstTrainingLog.TopIndex = lstTrainingLog.Items.Count - 1;
                    }));
                }

                // 진행도 파싱
                ParseProgressFromLog(e.Data);
            }
        }

        private void TrainingProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                LogDetail($"[오류출력] {e.Data}");

                if (lstTrainingLog != null && !lstTrainingLog.IsDisposed)
                {
                    lstTrainingLog.Invoke((Action)(() =>
                    {
                        lstTrainingLog.Items.Add($"[{DateTime.Now:HH:mm:ss}] [오류] {e.Data}");
                        lstTrainingLog.TopIndex = lstTrainingLog.Items.Count - 1;
                    }));
                }
            }
        }

        private void ParseProgressFromLog(string logLine)
        {
            try
            {
                // "Epoch 1/100" 형식 파싱 (진행도)
                if (logLine.Contains("Epoch") && logLine.Contains("/"))
                {
                    // 정규식으로 더 정확하게 파싱
                    var epochMatch = System.Text.RegularExpressions.Regex.Match(logLine, @"Epoch\s+(\d+)/(\d+)");
                    if (epochMatch.Success && int.TryParse(epochMatch.Groups[1].Value, out int current) && int.TryParse(epochMatch.Groups[2].Value, out int total))
                    {
                        // 현재 에포크 추적 (loss 줄에는 Epoch 정보가 없는 경우가 많으므로 별도 보관)
                        currentEpoch = current;
                        totalEpochs = total;

                        int progress = (int)((double)current / total * 100);
                        if (prgTrainingProgress != null && !prgTrainingProgress.IsDisposed)
                        {
                            prgTrainingProgress.Invoke((Action)(() =>
                                    {
                                        prgTrainingProgress.Value = Math.Min(progress, 100);
                                        lblProgress.Text = $"{progress}%";
                                        LogDetail($"진행도 업데이트: {progress}% ({current}/{total})");
                                        FindMainWindow()?.SetStatusMessage(
                                            $"③ 학습 실행 —  학습 진행 중... {progress}% ({current} / {total} 에포크)  │  학습 완료까지 기다려주세요.",
                                            MainWindow.StatusLevel.Info);
                                    }));
                        }
                    }
                }

                // 손실값 파싱: "loss: 0.1234 - val_loss: 0.5678" 형식
                if (logLine.Contains("loss:"))
                {
                    float? trainLoss = null;
                    float? valLoss = null;

                    // 에포크 번호 추출: 같은 줄에 Epoch이 있으면 사용하고,
                    // 없으면 직전에 추적한 currentEpoch 값을 사용 (Keras는 별개 줄로 출력)
                    int epochNum = currentEpoch;
                    if (logLine.Contains("Epoch"))
                    {
                        var epochMatch = System.Text.RegularExpressions.Regex.Match(logLine, @"Epoch\s+(\d+)");
                        if (epochMatch.Success && int.TryParse(epochMatch.Groups[1].Value, out int ep))
                        {
                            epochNum = ep;
                        }
                    }

                    // 훈련 손실 추출 (loss: 값). 진행 줄에서 마지막(최종) 값을 사용하기 위해 모든 매치 중 마지막을 선택
                    var lossMatches = System.Text.RegularExpressions.Regex.Matches(logLine, @"(?<!val_)loss:\s*([\d.eE+-]+)");
                    if (lossMatches.Count > 0 && float.TryParse(lossMatches[lossMatches.Count - 1].Groups[1].Value,
                        System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float loss))
                    {
                        trainLoss = loss;
                    }

                    // 검증 손실 추출 (val_loss: 값)
                    var valLossMatch = System.Text.RegularExpressions.Regex.Match(logLine, @"val_loss:\s*([\d.eE+-]+)");
                    if (valLossMatch.Success && float.TryParse(valLossMatch.Groups[1].Value,
                        System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float vLoss))
                    {
                        valLoss = vLoss;
                    }

                    // 데이터가 있으면 그래프 업데이트.
                    // 검증 손실(val_loss)이 포함된 줄은 에포크 종료 시점의 최종 값이므로 우선 반영하고,
                    // val_loss가 없어도 에포크 정보와 train loss가 있으면 갱신합니다.
                    if (trainLoss.HasValue && epochNum > 0)
                    {
                        UpdateChartWithMetric(epochNum, trainLoss.Value, valLoss);
                    }

                    // 라벨(에포크/정확성 증가율)은 "에포크가 완료될 때"만 갱신합니다.
                    // Keras는 한 에포크 안에서 진행 줄(ETA: 포함)을 여러 번 출력하므로,
                    // 진행 중 줄은 제외하고(= ETA: 없음) 에포크당 1회만 반영합니다.
                    bool isEpochCompletionLine = !logLine.Contains("ETA:");
                    if (epochNum > 0 && isEpochCompletionLine && epochNum != lastLabelEpoch)
                    {
                        lastLabelEpoch = epochNum;

                        // 에포크 라벨 갱신
                        if (lblEpochInfo != null && !lblEpochInfo.IsDisposed)
                        {
                            int totalForLabel = totalEpochs > 0 ? totalEpochs : epochNum;
                            UpdateEpochInfo(epochNum, totalForLabel);
                        }

                        // 정확성 증가율 라벨 갱신.
                        // accuracy 로그가 있으면 그것을 사용하고, 없으면(회귀 모델) 검증/훈련 손실 기반의
                        // 의사 정확도(1 / (1 + loss))로 추이를 계산합니다.
                        double? metric = null;

                        // accuracy/acc 로그 추출 (val_accuracy 우선, 없으면 accuracy)
                        var accMatch = System.Text.RegularExpressions.Regex.Match(logLine, @"(?:val_)?(?:accuracy|acc):\s*([\d.eE+-]+)");
                        if (accMatch.Success && double.TryParse(accMatch.Groups[1].Value,
                            System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double accVal))
                        {
                            metric = accVal;
                        }
                        else
                        {
                            // 손실 기반 의사 정확도 (손실이 낮을수록 정확도 높음)
                            float? refLoss = valLoss ?? trainLoss;
                            if (refLoss.HasValue)
                            {
                                metric = 1.0 / (1.0 + refLoss.Value);
                            }
                        }

                        if (metric.HasValue)
                        {
                            UpdateAccuracyTrend(metric.Value);
                        }
                    }
                }
            }
            catch
            {
                // 파싱 실패해도 계속 진행
            }
        }

        /// <summary>
        /// 에포크 정보 라벨을 갱신합니다. (UI 스레드 안전)
        /// </summary>
        private void UpdateEpochInfo(int current, int total)
        {
            if (lblEpochInfo == null || lblEpochInfo.IsDisposed)
            {
                return;
            }

            string text = $"Epoch {current}/{total}";
            try
            {
                if (lblEpochInfo.InvokeRequired)
                {
                    lblEpochInfo.Invoke((Action)(() => lblEpochInfo.Text = text));
                }
                else
                {
                    lblEpochInfo.Text = text;
                }
            }
            catch
            {
                // UI 갱신 실패 무시
            }
        }

        /// <summary>
        /// 직전 에포크 대비 현재 에포크의 정확성 증가율을 계산하여 라벨을 갱신합니다.
        /// - 첫 번째 에포크(비교 대상 없음): "-%" 표시 (검정색)
        /// - 정확성이 증가한 경우: "정확성 증가율 : +X.X%" (초록색)
        /// - 정확성이 유지/감소한 경우: "정확성 증가율 : X.X%" (회색)
        /// </summary>
        private void UpdateAccuracyTrend(double currentMetric)
        {
            if (lblAccuracyTrend == null || lblAccuracyTrend.IsDisposed)
            {
                return;
            }

            string text;
            Color color;

            if (double.IsNaN(lastTrendMetric) || lastTrendMetric <= 0)
            {
                // 첫 에포크: 비교 대상이 없으므로 증가율 없음
                text = "정확성 증가율 : -%";
                color = Color.FromArgb(40, 40, 40);
            }
            else
            {
                double increaseRate = (currentMetric - lastTrendMetric) / lastTrendMetric * 100.0;
                if (increaseRate > 0.0)
                {
                    text = $"정확성 증가율 : +{increaseRate:F1}%";
                    color = Color.FromArgb(0, 153, 0); // 초록색
                }
                else
                {
                    text = $"정확성 증가율 : {increaseRate:F1}%";
                    color = Color.FromArgb(110, 110, 110); // 회색
                }
            }

            lastTrendMetric = currentMetric;

            try
            {
                if (lblAccuracyTrend.InvokeRequired)
                {
                    lblAccuracyTrend.Invoke((Action)(() =>
                    {
                        lblAccuracyTrend.Text = text;
                        lblAccuracyTrend.ForeColor = color;
                    }));
                }
                else
                {
                    lblAccuracyTrend.Text = text;
                    lblAccuracyTrend.ForeColor = color;
                }
            }
            catch
            {
                // UI 갱신 실패 무시
            }
        }

        private void StopTraining()
        {
            try
            {
                userStopped = true;

                if (trainingProcess != null && !trainingProcess.HasExited)
                {
                    trainingProcess.Kill();
                    LogInfo("학습이 중지되었습니다.");
                }

                isTraining = false;
                btnStartTraining.Text = "▷ 학습 시작";
            }
            catch (Exception ex)
            {
                LogDetail($"학습 중지 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 학습 완료를 MainWindow에 알리고 ResultControl에 데이터를 전달합니다.
        /// </summary>
        private void NotifyTrainingCompleted(string modelPath)
        {
            try
            {
                // 소요 시간을 정확히 기록
                chartDataModel.MarkAsCompleted();

                MainWindow mainWindow = this.FindMainWindow();
                if (mainWindow != null)
                {
                    // ResultControl에 학습 메트릭과 데이터 전달
                    mainWindow.SetTrainingResults(chartDataModel, currentTrainingData, modelPath);
                    LogDetail("학습 결과를 결과 화면으로 전달했습니다.");
                }
            }
            catch (Exception ex)
            {
                LogWarning($"학습 완료 알림 오류: {ex.Message}");
            }
        }

        private void BtnCheckTrainingResult_Click(object sender, EventArgs e)
        {
            try
            {
                MainWindow mainWindow = this.FindMainWindow();
                if (mainWindow != null)
                {
                    // 데이터가 있으면 먼저 결과 화면에 전달 후 이동
                    if (chartDataModel != null && chartDataModel.GetMetricCount() > 0)
                    {
                        mainWindow.SetTrainingResults(chartDataModel, currentTrainingData);
                    }
                    mainWindow.ShowResultControl();
                    LogDetail("결과 화면으로 이동했습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"결과 화면 이동 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogDetail($"결과 화면 이동 오류: {ex.Message}");
            }
        }

        private void LogInfo(string message)
        {
            if (logger != null)
            {
                logger.AppendLog($"[학습] {message}");
            }
        }

        private void LogWarning(string message)
        {
            if (logger != null)
            {
                logger.AppendLog($"[학습 경고] {message}");
            }
        }

        /// <summary>
        /// 학습 화면 로그에만 기록하고 실행 로그(MainWindow)에는 표시하지 않습니다.
        /// Python 출력, 경로 탐색 등 세부 로그에 사용합니다.
        /// </summary>
        private void LogDetail(string message)
        {
            if (logger != null)
            {
                logger.AppendLogSilent($"[학습] {message}");
            }
        }

        /// <summary>
        /// 학습 메트릭을 JSON 파일로 저장합니다.
        /// 결과 화면에서 불러올 수 있도록 모델 파일과 같은 폴더에 저장합니다.
        /// </summary>
        private void SaveTrainingMetrics(string modelPath)
        {
            try
            {
                if (chartDataModel == null || chartDataModel.GetMetricCount() == 0)
                {
                    LogWarning("저장할 학습 메트릭이 없습니다.");
                    return;
                }

                // 메트릭 JSON 파일 경로 (모델 파일과 같은 폴더에 저장)
                string modelDir = Path.GetDirectoryName(modelPath);
                string modelFileName = Path.GetFileNameWithoutExtension(modelPath);
                string metricsPath = Path.Combine(modelDir, $"{modelFileName}_metrics.json");

                // JSON 저장
                string json = chartDataModel.ToJson();
                File.WriteAllText(metricsPath, json);

                LogDetail($"학습 메트릭 저장 완료: {metricsPath}");
            }
            catch (Exception ex)
            {
                LogWarning($"메트릭 저장 오류: {ex.Message}");
            }
        }

        public ChartDataModel GetChartDataModel()
        {
            return chartDataModel;
        }

        /// <summary>
        /// MainWindow를 부모 계층에서 찾습니다.
        /// UserControl → pnlMainContent(Panel) → MainWindow(Form) 구조를 지원합니다.
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

        private void btnStartTraining_Click_1(object sender, EventArgs e)
        {

        }
    }
}
