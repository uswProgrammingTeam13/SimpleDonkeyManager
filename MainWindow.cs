using SimpleDonkeyManager.controls;

namespace SimpleDonkeyManager
{
    public partial class MainWindow : Form
    {
        private InitialScreen initialScreen;
        private DataLoadControl dataLoadControl;
        private DataFilterControl dataFilterControl;
        private TrainingControl trainingControl;
        private ResultControl resultControl;
        private int currentControlIndex = -1;
        private UserControl[] controls;
        private Logger logger;
        private HelpManager helpManager;

        /// <summary>
        /// Logger 인스턴스를 공개적으로 노출합니다.
        /// </summary>
        public Logger GetLogger()
        {
            return logger;
        }

        public MainWindow()
        {
            InitializeComponent();
            logger = new Logger();
            helpManager = new HelpManager();

            // Logger의 LogAdded 이벤트 구독
            logger.LogAdded += Logger_LogAdded;

            tableLayoutPanelButtons.Paint += TableLayoutPanelButtons_Paint;
            pnlConditionView.Paint += PnlConditionView_Paint;

            InitializeControls();
            InitializeHelpTexts();

            logger.AppendLog("프로그램이 실행되었습니다. 환영합니다!");

            // 초기 화면 도움말 표시
            ShowHelpTab(HelpManager.HELP_INITIAL);
        }

        private void InitializeHelpTexts()
        {
            // 각 탭에 도움말 텍스트 로드
            richTxtHelpInitial.Text = helpManager.GetInitialHelp();
            richTxtHelpDataLoad.Text = helpManager.GetDataLoadHelp();
            richTxtHelpDataFilter.Text = helpManager.GetDataFilterHelp();
            richTxtHelpTraining.Text = helpManager.GetTrainingHelp();
            richTxtHelpResult.Text = helpManager.GetResultHelp();
        }

        private void InitializeControls()
        {
            // UserControl 생성
            initialScreen = new InitialScreen();
            dataLoadControl = new DataLoadControl();
            dataFilterControl = new DataFilterControl();
            trainingControl = new TrainingControl();
            resultControl = new ResultControl();

            // Logger 주입
            dataLoadControl.SetLogger(logger);
            dataFilterControl.SetLogger(logger);
            trainingControl.SetLogger(logger);

            // 배열에 저장 (InitialScreen은 배열에 포함 안 함)
            controls = new UserControl[] { dataLoadControl, dataFilterControl, trainingControl, resultControl };

            // 모든 UserControl 설정
            initialScreen.Dock = DockStyle.Fill;
            initialScreen.Visible = true;
            pnlMainContent.Controls.Add(initialScreen);

            foreach (var control in controls)
            {
                control.Dock = DockStyle.Fill;
                control.Visible = false;
                pnlMainContent.Controls.Add(control);
            }

            // 초기 화면을 맨 앞으로 (BringToFront)
            initialScreen.BringToFront();

            // 버튼 클릭 이벤트 연결
            btnDataLoadCon.Click += BtnDataLoadCon_Click;
            btnDataFilterCon.Click += BtnDataFilterCon_Click;
            btnTraningCon.Click += BtnTraningCon_Click;
            btnResultCon.Click += BtnResultCon_Click;
        }

        private void ShowControl(int index)
        {
            initialScreen.Visible = false;
            if (currentControlIndex != -1)
            {
                controls[currentControlIndex].Visible = false;
            }
            currentControlIndex = index;
            controls[currentControlIndex].Visible = true;
            controls[currentControlIndex].BringToFront();

            UpdateButtonIndicator(index);

            // 해당 컨트롤에 맞는 도움말 탭으로 자동 전환
            ShowHelpTab(index + 1); // +1은 InitialHelp가 탭 0이므로

            // 화면 전환 시 단계별 안내 메시지
            switch (index)
            {
                case 0: // 데이터 불러오기
                    SetStatusMessage("① 데이터 불러오기 —  이미지 폴더를 선택한 후 [데이터 로드] 버튼을 눌러주세요.", StatusLevel.Wait);
                    break;
                case 1: // 데이터 필터링
                    SetStatusMessage("② 데이터 필터링 —  조향·스로틀 범위를 설정하고 [필터 미리보기] 후 [필터 적용]을 눌러주세요.", StatusLevel.Wait);
                    break;
                case 2: // 학습 실행
                    SetStatusMessage("③ 학습 실행 —  모델 저장 경로를 선택한 후 [학습 시작] 버튼을 눌러주세요.", StatusLevel.Wait);
                    break;
                case 3: // 결과 확인
                    SetStatusMessage("④ 학습 결과 확인 —  학습이 완료된 결과 그래프와 이미지를 확인하세요.", StatusLevel.Wait);
                    break;
            }
        }

        /// <summary>
        /// 도움말 탭을 지정된 인덱스로 변경합니다.
        /// </summary>
        private void ShowHelpTab(int helpIndex)
        {
            if (tabControlHelp != null && helpIndex >= 0 && helpIndex < tabControlHelp.TabCount)
            {
                tabControlHelp.SelectedIndex = helpIndex;
            }
        }

        private void UpdateButtonIndicator(int index)
        {
            // 기본 상태로 텍스트와 배경색 초기화
            btnDataLoadCon.Text = "① 데이터 불러오기";
            btnDataFilterCon.Text = "② 데이터 필터링";
            btnTraningCon.Text = "③ 학습 실행";
            btnResultCon.Text = "④ 학습 결과 확인";

            btnDataLoadCon.BackColor = SystemColors.Control;
            btnDataFilterCon.BackColor = SystemColors.Control;
            btnTraningCon.BackColor = SystemColors.Control;
            btnResultCon.BackColor = SystemColors.Control;

            // 활성화된 버튼에만 색상 변경
            switch (index)
            {
                case 0:
                    btnDataLoadCon.BackColor = Color.LightSkyBlue;
                    break;
                case 1:
                    btnDataFilterCon.BackColor = Color.LightSkyBlue;
                    break;
                case 2:
                    btnTraningCon.BackColor = Color.LightSkyBlue;
                    break;
                case 3:
                    btnResultCon.BackColor = Color.LightSkyBlue;
                    break;
            }
        }

        private void BtnDataLoadCon_Click(object? sender, EventArgs e)
        {
            ShowControl(0); // DataLoadControl
        }

        private void BtnDataFilterCon_Click(object? sender, EventArgs e)
        {
            ShowControl(1); // DataFilterControl
        }

        private void BtnTraningCon_Click(object? sender, EventArgs e)
        {
            ShowControl(2); // TrainingControl
        }

        private void BtnResultCon_Click(object? sender, EventArgs e)
        {
            ShowControl(3); // ResultControl
        }

        private void BtnDebugControlChanger_Click(object? sender, EventArgs e)
        {
            // InitialScreen에서 첫 번째 컨트롤로 이동
            if (currentControlIndex == -1)
            {
                initialScreen.Visible = false;
                currentControlIndex = 0;
                controls[currentControlIndex].Visible = true;
            }
            else
            {
                // 현재 컨트롤 숨기기
                controls[currentControlIndex].Visible = false;

                // 다음 컨트롤로 이동 (순환)
                currentControlIndex = (currentControlIndex + 1) % controls.Length;

                // 다음 컨트롤 표시
                controls[currentControlIndex].Visible = true;
            }
        }

        private void TableLayoutPanelButtons_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(SystemColors.ControlDark, 1))
            {
                e.Graphics.DrawLine(pen, 0, tableLayoutPanelButtons.Height - 1, tableLayoutPanelButtons.Width, tableLayoutPanelButtons.Height - 1);
            }
        }

        private void PnlConditionView_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(SystemColors.ControlDark, 1))
            {
                e.Graphics.DrawLine(pen, 0, 0, pnlConditionView.Width, 0);
            }
        }

        /// <summary>
        /// TrainingControl에 전체 로드된 데이터를 전달합니다.
        /// </summary>
        public void SetTrainingFullData(ImageManager imageManager, List<FrameData> frameDataList)
        {
            if (trainingControl != null && imageManager != null)
            {
                trainingControl.SetFullFrameData(frameDataList, imageManager.SelectedFolderPath ?? "");
            }
        }

        /// <summary>
        /// TrainingControl에 필터된 데이터를 전달합니다.
        /// </summary>
        public void SetTrainingData(List<FrameData> frameDataList, string dataFolder)
        {
            if (trainingControl != null)
            {
                trainingControl.SetTrainData(frameDataList, dataFolder);
            }
        }

        /// <summary>
        /// 학습 완료 후 ResultControl에 결과를 전달합니다.
        /// 백그라운드 스레드에서 호출되므로 UI 스레드로 전환합니다.
        /// </summary>
        public void SetTrainingResults(ChartDataModel metrics, List<FrameData> trainingData, string modelPath = null)
        {
            if (resultControl == null || metrics == null)
                return;

            if (resultControl.InvokeRequired)
            {
                resultControl.Invoke(new Action(() => SetTrainingResults(metrics, trainingData, modelPath)));
                return;
            }

            resultControl.DisplayTrainingResults(metrics);
            resultControl.SetTrainingData(trainingData);
            if (modelPath != null)
                resultControl.SetModelPath(modelPath);
        }

        /// <summary>
        /// ResultControl을 표시합니다. (결과 화면으로 이동)
        /// </summary>
        public void ShowResultControl()
        {
            ShowControl(3); // ResultControl은 인덱스 3
            // 화면 전환 후 그래프 강제 갱신
            resultControl?.Refresh();
        }

        /// <summary>
        /// 프로그램 상태 라벨 업데이트 (폴더/프레임 수 포함)
        /// </summary>
        public void UpdateProgramStatus(string folderPath, int totalImages, int loadedFrames, string status)
        {
            string displayPath = folderPath ?? "-";
            if (displayPath.Length > 40)
                displayPath = "..." + displayPath.Substring(displayPath.Length - 37);

            SetStatusMessage($"📂 {displayPath}    |    프레임 수 : {loadedFrames} / {totalImages}    |    {status}");
        }

        /// <summary>
        /// 상태 메시지와 색상을 지정하여 상태바를 업데이트합니다.
        /// </summary>
        public void SetStatusMessage(string message, StatusLevel level = StatusLevel.Info)
        {
            if (lblProgramCon == null) return;

            Action update = () =>
            {
                lblProgramCon.Text = message;
                lblProgramCon.ForeColor = level switch
                {
                    StatusLevel.Wait    => Color.FromArgb(100, 100, 100),   // 회색  – 대기
                    StatusLevel.Info    => Color.FromArgb(30,  80,  160),   // 파랑  – 진행
                    StatusLevel.Success => Color.FromArgb(0,   130,  60),   // 초록  – 완료
                    StatusLevel.Warning => Color.FromArgb(180,  90,   0),   // 주황  – 경고
                    StatusLevel.Error   => Color.FromArgb(180,  20,  20),   // 빨강  – 오류
                    _                   => SystemColors.ControlDarkDark
                };
            };

            if (lblProgramCon.InvokeRequired)
                lblProgramCon.Invoke(update);
            else
                update();
        }

        public enum StatusLevel { Wait, Info, Success, Warning, Error }

        /// <summary>
        /// DataLoadControl에서 DataFilterControl으로 데이터를 전달합니다.
        /// </summary>
        public void SetFilterControlData(ImageManager imageManager, List<FrameData> frameDataList)
        {
            if (dataFilterControl != null)
            {
                dataFilterControl.SetFrameData(imageManager, frameDataList);
            }
        }

        /// <summary>
        /// Logger의 LogAdded 이벤트 핸들러
        /// </summary>
        private void Logger_LogAdded(object sender, LogAddedEventArgs e)
        {
            // richTxtLog에 로그 추가 (UI 스레드 안전)
            if (richTxtLog != null)
            {
                if (richTxtLog.InvokeRequired)
                {
                    richTxtLog.Invoke(new Action(() =>
                    {
                        richTxtLog.AppendText(e.LogMessage + Environment.NewLine);
                        // 자동으로 맨 아래로 스크롤
                        richTxtLog.SelectionStart = richTxtLog.Text.Length;
                        richTxtLog.ScrollToCaret();
                    }));
                }
                else
                {
                    richTxtLog.AppendText(e.LogMessage + Environment.NewLine);
                    // 자동으로 맨 아래로 스크롤
                    richTxtLog.SelectionStart = richTxtLog.Text.Length;
                    richTxtLog.ScrollToCaret();
                }
            }
        }


                            public void NotifyImageSelected(string imagePath)
                            {
                                // DataFilterControl에도 선택된 이미지를 전달
                                if (dataFilterControl != null)
                                {
                                    dataFilterControl.DisplayImage(imagePath);
                                }
                            }
                        }
                    }

