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

        public MainWindow()
        {
            InitializeComponent();
            pnlButtons.Paint += PnlButtons_Paint;
            pnlConditionView.Paint += PnlConditionView_Paint;
            InitializeControls();
        }

        private void InitializeControls()
        {
            // UserControl 생성
            initialScreen = new InitialScreen();
            dataLoadControl = new DataLoadControl();
            dataFilterControl = new DataFilterControl();
            trainingControl = new TrainingControl();
            resultControl = new ResultControl();

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
        }

        private void UpdateButtonIndicator(int index)
        {
            // 기본 상태로 텍스트와 배경색 초기화
            btnDataLoadCon.Text = "파일 로드";
            btnDataFilterCon.Text = "파일 필터";
            btnTraningCon.Text = "트레이닝";
            btnResultCon.Text = "결과";

            btnDataLoadCon.BackColor = SystemColors.Control;
            btnDataFilterCon.BackColor = SystemColors.Control;
            btnTraningCon.BackColor = SystemColors.Control;
            btnResultCon.BackColor = SystemColors.Control;

            // 활성화된 버튼에만 아이콘(●) 표시 및 색상 변경
            switch (index)
            {
                case 0:
                    btnDataLoadCon.Text = "● 파일 로드";
                    btnDataLoadCon.BackColor = Color.LightSkyBlue;
                    break;
                case 1:
                    btnDataFilterCon.Text = "● 파일 필터";
                    btnDataFilterCon.BackColor = Color.LightSkyBlue;
                    break;
                case 2:
                    btnTraningCon.Text = "● 트레이닝";
                    btnTraningCon.BackColor = Color.LightSkyBlue;
                    break;
                case 3:
                    btnResultCon.Text = "● 결과";
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

        private void PnlButtons_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(SystemColors.ControlDark, 1))
            {
                e.Graphics.DrawLine(pen, 0, pnlButtons.Height - 1, pnlButtons.Width, pnlButtons.Height - 1);
            }
        }

        private void PnlConditionView_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(SystemColors.ControlDark, 1))
            {
                e.Graphics.DrawLine(pen, 0, 0, pnlConditionView.Width, 0);
            }
        }


    }
}
