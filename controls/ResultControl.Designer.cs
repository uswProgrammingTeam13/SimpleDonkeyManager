namespace SimpleDonkeyManager.controls
{
    partial class ResultControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            tlpResultMain = new TableLayoutPanel();
            pnlLeft = new Panel();
            tlpResultLeft = new TableLayoutPanel();
            grpSummary = new GroupBox();
            tlpSummary = new TableLayoutPanel();
            lblTotalEpochs = new Label();
            lblMinLoss = new Label();
            lblMaxAccuracy = new Label();
            lblTrainingTime = new Label();
            btnOpenModelFolder = new Button();
            grpChart = new GroupBox();
            pnlResultChart = new Panel();
            pnlRight = new Panel();
            grpValidation = new GroupBox();
            tlpValidation = new TableLayoutPanel();
            pnlValidationTop = new Panel();
            btnStartValidation = new Button();
            prgValidation = new ProgressBar();
            lblValidationProgress = new Label();
            validationViewer1 = new SimpleDonkeyManager.controlutils.ValidationViewer();
            grpValidationSummary = new GroupBox();
            tlpValidationSummary = new TableLayoutPanel();
            lblValCount = new Label();
            lblValAvgAngle = new Label();
            lblValMaxAngle = new Label();
            lblValAvgThrottle = new Label();
            lblValVerdict = new Label();
            tlpResultMain.SuspendLayout();
            pnlLeft.SuspendLayout();
            tlpResultLeft.SuspendLayout();
            grpSummary.SuspendLayout();
            tlpSummary.SuspendLayout();
            grpChart.SuspendLayout();
            pnlRight.SuspendLayout();
            grpValidation.SuspendLayout();
            tlpValidation.SuspendLayout();
            pnlValidationTop.SuspendLayout();
            grpValidationSummary.SuspendLayout();
            tlpValidationSummary.SuspendLayout();
            SuspendLayout();
            // 
            // tlpResultMain
            // 
            tlpResultMain.ColumnCount = 2;
            tlpResultMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67F));
            tlpResultMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tlpResultMain.Controls.Add(pnlLeft, 0, 0);
            tlpResultMain.Controls.Add(pnlRight, 1, 0);
            tlpResultMain.Dock = DockStyle.Fill;
            tlpResultMain.Location = new Point(0, 0);
            tlpResultMain.Name = "tlpResultMain";
            tlpResultMain.RowCount = 1;
            tlpResultMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpResultMain.Size = new Size(1176, 600);
            tlpResultMain.TabIndex = 1;
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.White;
            pnlLeft.Controls.Add(tlpResultLeft);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(3, 3);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(781, 594);
            pnlLeft.TabIndex = 0;
            // 
            // tlpResultLeft
            // 
            tlpResultLeft.ColumnCount = 1;
            tlpResultLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpResultLeft.Controls.Add(grpSummary, 0, 0);
            tlpResultLeft.Controls.Add(grpChart, 0, 1);
            tlpResultLeft.Dock = DockStyle.Fill;
            tlpResultLeft.Location = new Point(0, 0);
            tlpResultLeft.Name = "tlpResultLeft";
            tlpResultLeft.RowCount = 2;
            tlpResultLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpResultLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpResultLeft.Size = new Size(781, 594);
            tlpResultLeft.TabIndex = 0;
            // 
            // grpSummary
            // 
            grpSummary.Controls.Add(tlpSummary);
            grpSummary.Dock = DockStyle.Fill;
            grpSummary.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold);
            grpSummary.ForeColor = Color.RoyalBlue;
            grpSummary.Location = new Point(3, 3);
            grpSummary.Name = "grpSummary";
            grpSummary.Size = new Size(775, 291);
            grpSummary.TabIndex = 0;
            grpSummary.TabStop = false;
            grpSummary.Text = "결과 요약";
            // 
            // tlpSummary
            // 
            tlpSummary.ColumnCount = 1;
            tlpSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpSummary.Controls.Add(lblTotalEpochs, 0, 0);
            tlpSummary.Controls.Add(lblMinLoss, 0, 1);
            tlpSummary.Controls.Add(lblMaxAccuracy, 0, 2);
            tlpSummary.Controls.Add(lblTrainingTime, 0, 3);
            tlpSummary.Controls.Add(btnOpenModelFolder, 0, 4);
            tlpSummary.Dock = DockStyle.Fill;
            tlpSummary.Location = new Point(3, 25);
            tlpSummary.Name = "tlpSummary";
            tlpSummary.Padding = new Padding(6, 4, 6, 4);
            tlpSummary.RowCount = 5;
            tlpSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpSummary.Size = new Size(769, 263);
            tlpSummary.TabIndex = 4;
            // 
            // lblTotalEpochs
            // 
            lblTotalEpochs.Dock = DockStyle.Fill;
            lblTotalEpochs.Font = new Font("나눔고딕", 18F, FontStyle.Bold);
            lblTotalEpochs.Location = new Point(9, 4);
            lblTotalEpochs.Name = "lblTotalEpochs";
            lblTotalEpochs.Size = new Size(751, 51);
            lblTotalEpochs.TabIndex = 0;
            lblTotalEpochs.Text = "총 에포크: 0";
            lblTotalEpochs.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMinLoss
            // 
            lblMinLoss.Dock = DockStyle.Fill;
            lblMinLoss.Font = new Font("나눔고딕", 18F, FontStyle.Bold);
            lblMinLoss.Location = new Point(9, 55);
            lblMinLoss.Name = "lblMinLoss";
            lblMinLoss.Size = new Size(751, 51);
            lblMinLoss.TabIndex = 1;
            lblMinLoss.Text = "최소 손실값: 0.0000";
            lblMinLoss.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMaxAccuracy
            // 
            lblMaxAccuracy.Dock = DockStyle.Fill;
            lblMaxAccuracy.Font = new Font("나눔고딕", 18F, FontStyle.Bold);
            lblMaxAccuracy.Location = new Point(9, 106);
            lblMaxAccuracy.Name = "lblMaxAccuracy";
            lblMaxAccuracy.Size = new Size(751, 51);
            lblMaxAccuracy.TabIndex = 2;
            lblMaxAccuracy.Text = "최고 정확도: 0.0000";
            lblMaxAccuracy.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTrainingTime
            // 
            lblTrainingTime.Dock = DockStyle.Fill;
            lblTrainingTime.Font = new Font("나눔고딕", 18F, FontStyle.Bold);
            lblTrainingTime.Location = new Point(9, 157);
            lblTrainingTime.Name = "lblTrainingTime";
            lblTrainingTime.Size = new Size(751, 51);
            lblTrainingTime.TabIndex = 3;
            lblTrainingTime.Text = "소요 시간: 0초";
            lblTrainingTime.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnOpenModelFolder
            // 
            btnOpenModelFolder.Dock = DockStyle.Fill;
            btnOpenModelFolder.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnOpenModelFolder.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnOpenModelFolder.FlatStyle = FlatStyle.Flat;
            btnOpenModelFolder.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnOpenModelFolder.ForeColor = SystemColors.Highlight;
            btnOpenModelFolder.Location = new Point(9, 211);
            btnOpenModelFolder.Name = "btnOpenModelFolder";
            btnOpenModelFolder.Size = new Size(751, 45);
            btnOpenModelFolder.TabIndex = 4;
            btnOpenModelFolder.Text = "📂 저장된 폴더 열기";
            btnOpenModelFolder.UseVisualStyleBackColor = true;
            btnOpenModelFolder.Click += BtnOpenModelFolder_Click;
            // 
            // grpChart
            // 
            grpChart.Controls.Add(pnlResultChart);
            grpChart.Dock = DockStyle.Fill;
            grpChart.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold);
            grpChart.ForeColor = Color.RoyalBlue;
            grpChart.Location = new Point(3, 300);
            grpChart.Name = "grpChart";
            grpChart.Size = new Size(775, 291);
            grpChart.TabIndex = 1;
            grpChart.TabStop = false;
            grpChart.Text = "학습 결과 추이";
            // 
            // pnlResultChart
            // 
            pnlResultChart.BackColor = Color.White;
            pnlResultChart.BorderStyle = BorderStyle.FixedSingle;
            pnlResultChart.Dock = DockStyle.Fill;
            pnlResultChart.Location = new Point(3, 25);
            pnlResultChart.Name = "pnlResultChart";
            pnlResultChart.Padding = new Padding(5);
            pnlResultChart.Size = new Size(769, 263);
            pnlResultChart.TabIndex = 0;
            // 
            // pnlRight
            // 
            pnlRight.BackColor = Color.White;
            pnlRight.Controls.Add(grpValidation);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Location = new Point(790, 3);
            pnlRight.Name = "pnlRight";
            pnlRight.Size = new Size(383, 594);
            pnlRight.TabIndex = 1;
            // 
            // grpValidation
            // 
            grpValidation.Controls.Add(tlpValidation);
            grpValidation.Dock = DockStyle.Fill;
            grpValidation.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold);
            grpValidation.ForeColor = Color.RoyalBlue;
            grpValidation.Location = new Point(0, 0);
            grpValidation.Name = "grpValidation";
            grpValidation.Size = new Size(383, 594);
            grpValidation.TabIndex = 0;
            grpValidation.TabStop = false;
            grpValidation.Text = "학습 결과 검증";
            // 
            // tlpValidation
            // 
            tlpValidation.ColumnCount = 1;
            tlpValidation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpValidation.Controls.Add(pnlValidationTop, 0, 0);
            tlpValidation.Controls.Add(validationViewer1, 0, 1);
            tlpValidation.Controls.Add(grpValidationSummary, 0, 2);
            tlpValidation.Dock = DockStyle.Fill;
            tlpValidation.Location = new Point(3, 25);
            tlpValidation.Name = "tlpValidation";
            tlpValidation.RowCount = 3;
            tlpValidation.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tlpValidation.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpValidation.RowStyles.Add(new RowStyle(SizeType.Absolute, 175F));
            tlpValidation.Size = new Size(377, 566);
            tlpValidation.TabIndex = 0;
            // 
            // pnlValidationTop
            // 
            pnlValidationTop.Controls.Add(prgValidation);
            pnlValidationTop.Controls.Add(lblValidationProgress);
            pnlValidationTop.Controls.Add(btnStartValidation);
            pnlValidationTop.Dock = DockStyle.Fill;
            pnlValidationTop.Location = new Point(0, 0);
            pnlValidationTop.Margin = new Padding(0);
            pnlValidationTop.Name = "pnlValidationTop";
            pnlValidationTop.Size = new Size(377, 80);
            pnlValidationTop.TabIndex = 0;
            // 
            // btnStartValidation
            // 
            btnStartValidation.Dock = DockStyle.Top;
            btnStartValidation.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnStartValidation.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnStartValidation.FlatStyle = FlatStyle.Flat;
            btnStartValidation.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnStartValidation.ForeColor = SystemColors.Highlight;
            btnStartValidation.Location = new Point(0, 0);
            btnStartValidation.Name = "btnStartValidation";
            btnStartValidation.Size = new Size(377, 50);
            btnStartValidation.TabIndex = 0;
            btnStartValidation.Text = "🔍 검증 시작";
            btnStartValidation.UseVisualStyleBackColor = true;
            btnStartValidation.Click += BtnStartValidation_Click;
            // 
            // prgValidation
            // 
            prgValidation.Dock = DockStyle.Bottom;
            prgValidation.Location = new Point(0, 68);
            prgValidation.Margin = new Padding(0);
            prgValidation.Name = "prgValidation";
            prgValidation.Size = new Size(377, 12);
            prgValidation.TabIndex = 2;
            prgValidation.Visible = false;
            // 
            // lblValidationProgress
            // 
            lblValidationProgress.Dock = DockStyle.Bottom;
            lblValidationProgress.Font = new Font("나눔고딕", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lblValidationProgress.ForeColor = Color.FromArgb(80, 80, 80);
            lblValidationProgress.Location = new Point(0, 50);
            lblValidationProgress.Name = "lblValidationProgress";
            lblValidationProgress.Size = new Size(377, 18);
            lblValidationProgress.TabIndex = 1;
            lblValidationProgress.Text = "";
            lblValidationProgress.TextAlign = ContentAlignment.MiddleCenter;
            lblValidationProgress.Visible = false;
            // 
            // validationViewer1
            // 
            validationViewer1.Dock = DockStyle.Fill;
            validationViewer1.Location = new Point(3, 53);
            validationViewer1.Name = "validationViewer1";
            validationViewer1.Size = new Size(371, 335);
            validationViewer1.TabIndex = 1;
            validationViewer1.Load += validationViewer1_Load;
            // 
            // grpValidationSummary
            // 
            grpValidationSummary.Controls.Add(tlpValidationSummary);
            grpValidationSummary.Dock = DockStyle.Fill;
            grpValidationSummary.Font = new Font("나눔고딕", 11F, FontStyle.Bold);
            grpValidationSummary.ForeColor = Color.RoyalBlue;
            grpValidationSummary.Location = new Point(3, 394);
            grpValidationSummary.Name = "grpValidationSummary";
            grpValidationSummary.Size = new Size(371, 169);
            grpValidationSummary.TabIndex = 2;
            grpValidationSummary.TabStop = false;
            grpValidationSummary.Text = "검증 결과 요약";
            // 
            // tlpValidationSummary
            // 
            tlpValidationSummary.ColumnCount = 1;
            tlpValidationSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpValidationSummary.Controls.Add(lblValCount, 0, 0);
            tlpValidationSummary.Controls.Add(lblValAvgAngle, 0, 1);
            tlpValidationSummary.Controls.Add(lblValMaxAngle, 0, 2);
            tlpValidationSummary.Controls.Add(lblValAvgThrottle, 0, 3);
            tlpValidationSummary.Controls.Add(lblValVerdict, 0, 4);
            tlpValidationSummary.Dock = DockStyle.Fill;
            tlpValidationSummary.Location = new Point(3, 20);
            tlpValidationSummary.Name = "tlpValidationSummary";
            tlpValidationSummary.Padding = new Padding(6, 2, 6, 2);
            tlpValidationSummary.RowCount = 5;
            tlpValidationSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpValidationSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpValidationSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpValidationSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpValidationSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpValidationSummary.Size = new Size(365, 146);
            tlpValidationSummary.TabIndex = 0;
            // 
            // lblValCount
            // 
            lblValCount.Dock = DockStyle.Fill;
            lblValCount.Font = new Font("나눔고딕", 11F, FontStyle.Bold);
            lblValCount.ForeColor = Color.Black;
            lblValCount.Location = new Point(9, 2);
            lblValCount.Name = "lblValCount";
            lblValCount.Size = new Size(347, 28);
            lblValCount.TabIndex = 0;
            lblValCount.Text = "검증 이미지 수: -";
            lblValCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblValAvgAngle
            // 
            lblValAvgAngle.Dock = DockStyle.Fill;
            lblValAvgAngle.Font = new Font("나눔고딕", 11F, FontStyle.Bold);
            lblValAvgAngle.ForeColor = Color.Black;
            lblValAvgAngle.Location = new Point(9, 30);
            lblValAvgAngle.Name = "lblValAvgAngle";
            lblValAvgAngle.Size = new Size(347, 28);
            lblValAvgAngle.TabIndex = 1;
            lblValAvgAngle.Text = "평균 조향 오차: -";
            lblValAvgAngle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblValMaxAngle
            // 
            lblValMaxAngle.Dock = DockStyle.Fill;
            lblValMaxAngle.Font = new Font("나눔고딕", 11F, FontStyle.Bold);
            lblValMaxAngle.ForeColor = Color.Black;
            lblValMaxAngle.Location = new Point(9, 58);
            lblValMaxAngle.Name = "lblValMaxAngle";
            lblValMaxAngle.Size = new Size(347, 28);
            lblValMaxAngle.TabIndex = 2;
            lblValMaxAngle.Text = "최대 조향 오차: -";
            lblValMaxAngle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblValAvgThrottle
            // 
            lblValAvgThrottle.Dock = DockStyle.Fill;
            lblValAvgThrottle.Font = new Font("나눔고딕", 11F, FontStyle.Bold);
            lblValAvgThrottle.ForeColor = Color.Black;
            lblValAvgThrottle.Location = new Point(9, 86);
            lblValAvgThrottle.Name = "lblValAvgThrottle";
            lblValAvgThrottle.Size = new Size(347, 28);
            lblValAvgThrottle.TabIndex = 3;
            lblValAvgThrottle.Text = "평균 속도 오차: -";
            lblValAvgThrottle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblValVerdict
            // 
            lblValVerdict.Dock = DockStyle.Fill;
            lblValVerdict.Font = new Font("나눔고딕", 12F, FontStyle.Bold);
            lblValVerdict.ForeColor = Color.SeaGreen;
            lblValVerdict.Location = new Point(9, 114);
            lblValVerdict.Name = "lblValVerdict";
            lblValVerdict.Size = new Size(347, 30);
            lblValVerdict.TabIndex = 4;
            lblValVerdict.Text = "검증 결과: -";
            lblValVerdict.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ResultControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpResultMain);
            Name = "ResultControl";
            Size = new Size(1176, 600);
            tlpResultMain.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            tlpResultLeft.ResumeLayout(false);
            grpSummary.ResumeLayout(false);
            tlpSummary.ResumeLayout(false);
            grpChart.ResumeLayout(false);
            pnlRight.ResumeLayout(false);
            grpValidation.ResumeLayout(false);
            tlpValidation.ResumeLayout(false);
            pnlValidationTop.ResumeLayout(false);
            grpValidationSummary.ResumeLayout(false);
            tlpValidationSummary.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpResultMain;
        private Panel pnlLeft;
        private TableLayoutPanel tlpResultLeft;
        private TableLayoutPanel tlpSummary;
        private Panel pnlRight;
        private Panel pnlResultChart;
        private GroupBox grpSummary;
        private GroupBox grpChart;
        private GroupBox grpValidation;
        private Label lblTotalEpochs;
        private Label lblMinLoss;
        private Label lblMaxAccuracy;
        private Label lblTrainingTime;
        private Button btnOpenModelFolder;
        private TableLayoutPanel tlpValidation;
        private Panel pnlValidationTop;
        private Button btnStartValidation;
        private ProgressBar prgValidation;
        private Label lblValidationProgress;
        private GroupBox grpValidationSummary;
        private TableLayoutPanel tlpValidationSummary;
        private Label lblValCount;
        private Label lblValAvgAngle;
        private Label lblValMaxAngle;
        private Label lblValAvgThrottle;
        private Label lblValVerdict;
        private SimpleDonkeyManager.controlutils.ValidationViewer validationViewer1;
    }
}
