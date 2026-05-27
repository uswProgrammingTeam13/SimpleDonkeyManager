using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;

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

        public TrainingControl()
        {
            InitializeComponent();
            InitializeTrainingControl();
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

                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"TrainingControl 초기화 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                LogInfo($"전체 데이터 설정: {fullFrameDataList.Count}개 프레임, 폴더: {dataFolder}");
            }
            catch (Exception ex)
            {
                LogWarning($"전체 데이터 설정 오류: {ex.Message}");
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
                LogInfo($"필터된 데이터 설정: {filteredFrameDataList.Count}개 프레임, 폴더: {dataFolder}");
            }
            catch (Exception ex)
            {
                LogWarning($"필터된 데이터 설정 오류: {ex.Message}");
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
                        lblDataStatus.Text = $"데이터셋: {count}개 프레임 ({status}) 준비됨";
                        lblDataStatus.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblDataStatus.Text = "데이터셋: 준비되지 않음";
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
                LogWarning($"UI 업데이트 오류: {ex.Message}");
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
                        LogInfo($"모델 저장 폴더 선택: {modelSaveFolder}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"폴더 선택 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"폴더 선택 오류: {ex.Message}");
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

                LogInfo($"학습 시작: 타입={modelType}, 데이터={currentTrainingData.Count}개, 모델경로={fullModelPath}");

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
                btnStartTraining.Text = "⏹ 학습 중지";
                prgTrainingProgress.Value = 0;
                lstTrainingLog.Items.Clear();

                Task.Run(() =>
                {
                    try
                    {
                        // Python 스크립트 경로 찾기
                        string pythonScriptPath = FindPythonScript();

                        if (string.IsNullOrEmpty(pythonScriptPath) || !File.Exists(pythonScriptPath))
                        {
                            LogWarning($"Python 스크립트를 찾을 수 없음: train.py");
                            return;
                        }

                        if (!Directory.Exists(dataFolder))
                        {
                            LogWarning($"데이터 폴더를 찾을 수 없음: {dataFolder}");
                            return;
                        }

                        // 모델 저장 폴더 생성
                        string modelDir = Path.GetDirectoryName(modelPath);
                        if (!Directory.Exists(modelDir))
                        {
                            try
                            {
                                Directory.CreateDirectory(modelDir);
                                LogInfo($"모델 저장 폴더 생성: {modelDir}");
                            }
                            catch (Exception ex)
                            {
                                LogWarning($"모델 폴더 생성 실패: {ex.Message}");
                                return;
                            }
                        }

                        LogInfo($"Python 스크립트 경로: {pythonScriptPath}");
                        LogInfo($"데이터 폴더: {dataFolder}");
                        LogInfo($"모델 저장 경로: {modelPath}");

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
                            LogInfo($"로컬 가상환경 Python 사용: {pythonExe}");
                        }
                        else
                        {
                            LogInfo($"시스템 전역 Python 사용: {pythonExe}");
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
                            CreateNoWindow = true
                        };

                        LogInfo($"실행 명령어: {psi.FileName} {psi.Arguments}");

                        trainingProcess = new Process { StartInfo = psi };
                        trainingProcess.OutputDataReceived += TrainingProcess_OutputDataReceived;
                        trainingProcess.ErrorDataReceived += TrainingProcess_ErrorDataReceived;
                        trainingProcess.Start();
                        trainingProcess.BeginOutputReadLine();
                        trainingProcess.BeginErrorReadLine();

                        // 프로세스 종료 대기
                        bool exited = trainingProcess.WaitForExit(5 * 60 * 1000); // 5분 타임아웃

                        if (!exited)
                        {
                            LogWarning("학습 타임아웃 (5분): 프로세스를 강제 종료합니다.");
                            trainingProcess.Kill();
                        }
                        else
                        {
                            int exitCode = trainingProcess.ExitCode;
                            LogInfo($"학습 프로세스 종료 (코드: {exitCode})");

                            if (exitCode == 0)
                            {
                                LogInfo("학습이 정상 완료되었습니다.");
                            }
                            else
                            {
                                LogWarning($"학습 프로세스 오류 코드: {exitCode}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogWarning($"학습 프로세스 오류: {ex.Message}");
                        LogWarning($"스택 추적: {ex.StackTrace}");
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
                            }));
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LogWarning($"비동기 학습 시작 오류: {ex.Message}");
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
                // 로컬 가상환경 경로들
                string[] possiblePaths = new string[]
                {
                    // donkey_env 가상환경 (프로젝트 루트)
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "donkey_env", "Scripts", "python.exe"),
                    Path.Combine(Directory.GetCurrentDirectory(), "donkey_env", "Scripts", "python.exe"),

                    // 부모 디렉토리 탐색
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "donkey_env", "Scripts", "python.exe"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "donkey_env", "Scripts", "python.exe"),
                };

                foreach (string path in possiblePaths)
                {
                    try
                    {
                        string fullPath = Path.GetFullPath(path);
                        if (File.Exists(fullPath))
                        {
                            LogInfo($"로컬 가상환경 Python 찾음: {fullPath}");
                            return fullPath;
                        }
                    }
                    catch
                    {
                        // 경로 파싱 오류 무시
                    }
                }

                // 직접 탐색
                DirectoryInfo currentDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                for (int i = 0; i < 6; i++)
                {
                    if (currentDir.Parent == null) break;

                    string pythonPath = Path.Combine(currentDir.FullName, "donkey_env", "Scripts", "python.exe");
                    if (File.Exists(pythonPath))
                    {
                        LogInfo($"로컬 가상환경 Python 찾음: {pythonPath}");
                        return pythonPath;
                    }

                    currentDir = currentDir.Parent;
                }

                // 로컬 가상환경이 없으면 null 반환 (시스템 Python 사용)
                LogInfo("로컬 가상환경을 찾을 수 없습니다. 시스템 전역 Python을 사용합니다.");
                return null;
            }
            catch (Exception ex)
            {
                LogWarning($"Python 실행 파일 검색 오류: {ex.Message}");
                return null;
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
                LogInfo($"Python 스크립트 검색 시작...");
                LogInfo($"  BaseDirectory: {AppDomain.CurrentDomain.BaseDirectory}");
                LogInfo($"  CurrentDirectory: {Directory.GetCurrentDirectory()}");

                // 경로 후보들
                string[] possiblePaths = new string[]
                {
                    // 1. 실행 파일 디렉토리 (bin/Debug/net10)
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "python", "train.py"),

                    // 2. 프로젝트 루트로 상대 경로 이동 (bin/Debug/net10 -> 루트로 4단계 상위)
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "python", "train.py"),

                    // 3. 현재 작업 디렉토리
                    Path.Combine(Directory.GetCurrentDirectory(), "python", "train.py"),

                    // 4. 실행 파일의 부모 디렉토리들 탐색
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "python", "train.py"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "python", "train.py"),
                };

                foreach (string path in possiblePaths)
                {
                    try
                    {
                        string fullPath = Path.GetFullPath(path);
                        LogInfo($"  검색 위치: {fullPath}");

                        if (File.Exists(fullPath))
                        {
                            LogInfo($"  ✓ 찾음!");
                            return fullPath;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogInfo($"  경로 파싱 오류: {ex.Message}");
                    }
                }

                // 마지막 수단: 프로젝트 폴더 직접 검색
                LogInfo($"프로젝트 폴더 직접 검색 중...");

                // SimpleDonkeyManager 프로젝트 폴더 찾기
                DirectoryInfo currentDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

                for (int i = 0; i < 6; i++)
                {
                    if (currentDir.Parent == null) break;

                    string pythonPath = Path.Combine(currentDir.FullName, "python", "train.py");
                    LogInfo($"  검색 위치: {pythonPath}");

                    if (File.Exists(pythonPath))
                    {
                        LogInfo($"  ✓ 찾음!");
                        return pythonPath;
                    }

                    currentDir = currentDir.Parent;
                }

                LogWarning("train.py를 찾을 수 없습니다. 프로젝트 루트의 python 폴더에 train.py가 있는지 확인하세요.");
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
                LogInfo($"[학습] {e.Data}");

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
                LogWarning($"[학습 오류] {e.Data}");

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
                // "Epoch 1/100" 형식 파싱
                if (logLine.Contains("Epoch") && logLine.Contains("/"))
                {
                    string[] parts = logLine.Split(new[] { "Epoch", "/", " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 3 && int.TryParse(parts[1], out int current) && int.TryParse(parts[2], out int total))
                    {
                        int progress = (int)((double)current / total * 100);
                        if (prgTrainingProgress != null && !prgTrainingProgress.IsDisposed)
                        {
                            prgTrainingProgress.Invoke((Action)(() =>
                            {
                                prgTrainingProgress.Value = Math.Min(progress, 100);
                                lblProgress.Text = $"{progress}%";
                            }));
                        }
                    }
                }
            }
            catch
            {
                // 파싱 실패해도 계속 진행
            }
        }

        private void StopTraining()
        {
            try
            {
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
                LogWarning($"학습 중지 오류: {ex.Message}");
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {
        }
    }
}
