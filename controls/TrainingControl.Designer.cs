namespace SimpleDonkeyManager
{
    partial class TrainingControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            splMainTraining = new SplitContainer();
            tableLayoutPanelLeft = new TableLayoutPanel();
            pnlTrainSettings = new Panel();
            lblDataStatus = new Label();
            lblModelType = new Label();
            cmbModelType = new ComboBox();
            lblModelPath = new Label();
            txtModelPath = new TextBox();
            btnSelectModelPath = new Button();
            pnlProgress = new Panel();
            lblProgressLabel = new Label();
            prgTrainingProgress = new ProgressBar();
            lblProgress = new Label();
            pnlTrainLog = new Panel();
            lblTrainingLog = new Label();
            lstTrainingLog = new ListBox();
            pnlTrainButtons = new TableLayoutPanel();
            btnStartTraining = new Button();
            btnCheckTrainingResult = new Button();
            pnlChartRight = new Panel();
            lblChartTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)splMainTraining).BeginInit();
            splMainTraining.Panel1.SuspendLayout();
            splMainTraining.Panel2.SuspendLayout();
            splMainTraining.SuspendLayout();
            tableLayoutPanelLeft.SuspendLayout();
            pnlTrainSettings.SuspendLayout();
            pnlProgress.SuspendLayout();
            pnlTrainLog.SuspendLayout();
            pnlTrainButtons.SuspendLayout();
            pnlChartRight.SuspendLayout();
            SuspendLayout();
            // 
            // splMainTraining
            // 
            splMainTraining.Dock = DockStyle.Fill;
            splMainTraining.Location = new Point(0, 0);
            splMainTraining.Margin = new Padding(3, 4, 3, 4);
            splMainTraining.Name = "splMainTraining";
            // 
            // splMainTraining.Panel1
            // 
            splMainTraining.Panel1.Controls.Add(tableLayoutPanelLeft);
            splMainTraining.Panel1MinSize = 400;
            // 
            // splMainTraining.Panel2
            // 
            splMainTraining.Panel2.Controls.Add(pnlChartRight);
            splMainTraining.Panel2MinSize = 300;
            splMainTraining.Size = new Size(900, 625);
            splMainTraining.SplitterDistance = 550;
            splMainTraining.SplitterWidth = 8;
            splMainTraining.TabIndex = 0;
            // 
            // tableLayoutPanelLeft
            // 
            tableLayoutPanelLeft.ColumnCount = 1;
            tableLayoutPanelLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelLeft.Controls.Add(pnlTrainSettings, 0, 0);
            tableLayoutPanelLeft.Controls.Add(pnlProgress, 0, 1);
            tableLayoutPanelLeft.Controls.Add(pnlTrainLog, 0, 2);
            tableLayoutPanelLeft.Controls.Add(pnlTrainButtons, 0, 3);
            tableLayoutPanelLeft.Dock = DockStyle.Fill;
            tableLayoutPanelLeft.Location = new Point(0, 0);
            tableLayoutPanelLeft.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelLeft.Name = "tableLayoutPanelLeft";
            tableLayoutPanelLeft.RowCount = 4;
            tableLayoutPanelLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            tableLayoutPanelLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanelLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tableLayoutPanelLeft.Size = new Size(550, 625);
            tableLayoutPanelLeft.TabIndex = 0;
            // 
            // pnlTrainSettings
            // 
            pnlTrainSettings.BackColor = Color.FromArgb(248, 248, 248);
            pnlTrainSettings.BorderStyle = BorderStyle.FixedSingle;
            pnlTrainSettings.Controls.Add(lblDataStatus);
            pnlTrainSettings.Controls.Add(lblModelType);
            pnlTrainSettings.Controls.Add(cmbModelType);
            pnlTrainSettings.Controls.Add(lblModelPath);
            pnlTrainSettings.Controls.Add(txtModelPath);
            pnlTrainSettings.Controls.Add(btnSelectModelPath);
            pnlTrainSettings.Dock = DockStyle.Fill;
            pnlTrainSettings.Location = new Point(3, 4);
            pnlTrainSettings.Margin = new Padding(3, 4, 3, 4);
            pnlTrainSettings.Name = "pnlTrainSettings";
            pnlTrainSettings.Size = new Size(544, 142);
            pnlTrainSettings.TabIndex = 0;
            // 
            // lblDataStatus
            // 
            lblDataStatus.AutoSize = true;
            lblDataStatus.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            lblDataStatus.ForeColor = Color.Red;
            lblDataStatus.Location = new Point(10, 12);
            lblDataStatus.Name = "lblDataStatus";
            lblDataStatus.Size = new Size(152, 15);
            lblDataStatus.TabIndex = 0;
            lblDataStatus.Text = "데이터셋: 준비되지 않음";
            // 
            // lblModelType
            // 
            lblModelType.AutoSize = true;
            lblModelType.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold);
            lblModelType.Location = new Point(10, 44);
            lblModelType.Name = "lblModelType";
            lblModelType.Size = new Size(64, 14);
            lblModelType.TabIndex = 1;
            lblModelType.Text = "모델 타입:";
            // 
            // cmbModelType
            // 
            cmbModelType.FormattingEnabled = true;
            cmbModelType.Location = new Point(100, 44);
            cmbModelType.Margin = new Padding(3, 4, 3, 4);
            cmbModelType.Name = "cmbModelType";
            cmbModelType.Size = new Size(120, 23);
            cmbModelType.TabIndex = 2;
            // 
            // lblModelPath
            // 
            lblModelPath.AutoSize = true;
            lblModelPath.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold);
            lblModelPath.Location = new Point(10, 78);
            lblModelPath.Name = "lblModelPath";
            lblModelPath.Size = new Size(64, 14);
            lblModelPath.TabIndex = 3;
            lblModelPath.Text = "저장 경로:";
            // 
            // txtModelPath
            // 
            txtModelPath.Location = new Point(100, 75);
            txtModelPath.Margin = new Padding(3, 4, 3, 4);
            txtModelPath.Name = "txtModelPath";
            txtModelPath.ReadOnly = true;
            txtModelPath.Size = new Size(330, 23);
            txtModelPath.TabIndex = 4;
            // 
            // btnSelectModelPath
            // 
            btnSelectModelPath.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnSelectModelPath.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnSelectModelPath.FlatStyle = FlatStyle.Flat;
            btnSelectModelPath.Font = new Font("나눔고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSelectModelPath.ForeColor = SystemColors.Highlight;
            btnSelectModelPath.Location = new Point(440, 69);
            btnSelectModelPath.Margin = new Padding(3, 4, 3, 4);
            btnSelectModelPath.Name = "btnSelectModelPath";
            btnSelectModelPath.Size = new Size(95, 29);
            btnSelectModelPath.TabIndex = 5;
            btnSelectModelPath.Text = "📂 경로 선택";
            btnSelectModelPath.UseVisualStyleBackColor = true;
            // 
            // pnlProgress
            // 
            pnlProgress.BackColor = Color.FromArgb(242, 242, 242);
            pnlProgress.BorderStyle = BorderStyle.FixedSingle;
            pnlProgress.Controls.Add(lblProgressLabel);
            pnlProgress.Controls.Add(prgTrainingProgress);
            pnlProgress.Controls.Add(lblProgress);
            pnlProgress.Dock = DockStyle.Fill;
            pnlProgress.Location = new Point(3, 154);
            pnlProgress.Margin = new Padding(3, 4, 3, 4);
            pnlProgress.Name = "pnlProgress";
            pnlProgress.Size = new Size(544, 92);
            pnlProgress.TabIndex = 1;
            // 
            // lblProgressLabel
            // 
            lblProgressLabel.AutoSize = true;
            lblProgressLabel.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            lblProgressLabel.Location = new Point(10, 6);
            lblProgressLabel.Name = "lblProgressLabel";
            lblProgressLabel.Size = new Size(82, 15);
            lblProgressLabel.TabIndex = 0;
            lblProgressLabel.Text = "학습 진행도:";
            // 
            // prgTrainingProgress
            // 
            prgTrainingProgress.Location = new Point(10, 31);
            prgTrainingProgress.Margin = new Padding(3, 4, 3, 4);
            prgTrainingProgress.Name = "prgTrainingProgress";
            prgTrainingProgress.Size = new Size(480, 29);
            prgTrainingProgress.TabIndex = 1;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            lblProgress.Location = new Point(500, 31);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(31, 15);
            lblProgress.TabIndex = 2;
            lblProgress.Text = "0%";
            // 
            // pnlTrainLog
            // 
            pnlTrainLog.BackColor = Color.FromArgb(242, 242, 242);
            pnlTrainLog.BorderStyle = BorderStyle.FixedSingle;
            pnlTrainLog.Controls.Add(lblTrainingLog);
            pnlTrainLog.Controls.Add(lstTrainingLog);
            pnlTrainLog.Dock = DockStyle.Fill;
            pnlTrainLog.Location = new Point(3, 254);
            pnlTrainLog.Margin = new Padding(3, 4, 3, 4);
            pnlTrainLog.Name = "pnlTrainLog";
            pnlTrainLog.Size = new Size(544, 292);
            pnlTrainLog.TabIndex = 2;
            // 
            // lblTrainingLog
            // 
            lblTrainingLog.AutoSize = true;
            lblTrainingLog.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            lblTrainingLog.Location = new Point(10, 6);
            lblTrainingLog.Name = "lblTrainingLog";
            lblTrainingLog.Size = new Size(69, 15);
            lblTrainingLog.TabIndex = 0;
            lblTrainingLog.Text = "학습 로그:";
            // 
            // lstTrainingLog
            // 
            lstTrainingLog.FormattingEnabled = true;
            lstTrainingLog.Location = new Point(10, 31);
            lstTrainingLog.Margin = new Padding(3, 4, 3, 4);
            lstTrainingLog.Name = "lstTrainingLog";
            lstTrainingLog.Size = new Size(520, 244);
            lstTrainingLog.TabIndex = 1;
            // 
            // pnlTrainButtons
            // 
            pnlTrainButtons.BackColor = Color.FromArgb(240, 240, 240);
            pnlTrainButtons.ColumnCount = 2;
            pnlTrainButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlTrainButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlTrainButtons.Controls.Add(btnStartTraining, 0, 0);
            pnlTrainButtons.Controls.Add(btnCheckTrainingResult, 1, 0);
            pnlTrainButtons.Dock = DockStyle.Fill;
            pnlTrainButtons.Location = new Point(3, 554);
            pnlTrainButtons.Margin = new Padding(3, 4, 3, 4);
            pnlTrainButtons.Name = "pnlTrainButtons";
            pnlTrainButtons.Padding = new Padding(4);
            pnlTrainButtons.RowCount = 1;
            pnlTrainButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlTrainButtons.Size = new Size(544, 67);
            pnlTrainButtons.TabIndex = 3;
            // 
            // btnStartTraining
            // 
            btnStartTraining.Dock = DockStyle.Fill;
            btnStartTraining.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnStartTraining.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnStartTraining.FlatStyle = FlatStyle.Flat;
            btnStartTraining.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnStartTraining.ForeColor = SystemColors.Highlight;
            btnStartTraining.Location = new Point(7, 7);
            btnStartTraining.Name = "btnStartTraining";
            btnStartTraining.Size = new Size(262, 53);
            btnStartTraining.TabIndex = 0;
            btnStartTraining.Text = "▷ 학습 시작";
            btnStartTraining.UseVisualStyleBackColor = true;
            btnStartTraining.Click += btnStartTraining_Click_1;
            // 
            // btnCheckTrainingResult
            // 
            btnCheckTrainingResult.Dock = DockStyle.Fill;
            btnCheckTrainingResult.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnCheckTrainingResult.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnCheckTrainingResult.FlatStyle = FlatStyle.Flat;
            btnCheckTrainingResult.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnCheckTrainingResult.ForeColor = SystemColors.Highlight;
            btnCheckTrainingResult.Location = new Point(275, 7);
            btnCheckTrainingResult.Name = "btnCheckTrainingResult";
            btnCheckTrainingResult.Size = new Size(262, 53);
            btnCheckTrainingResult.TabIndex = 1;
            btnCheckTrainingResult.Text = "학습 결과 확인";
            btnCheckTrainingResult.UseVisualStyleBackColor = true;
            // 
            // pnlChartRight
            // 
            pnlChartRight.BackColor = Color.FromArgb(248, 248, 248);
            pnlChartRight.BorderStyle = BorderStyle.FixedSingle;
            pnlChartRight.Controls.Add(lblChartTitle);
            pnlChartRight.Dock = DockStyle.Fill;
            pnlChartRight.Location = new Point(0, 0);
            pnlChartRight.Margin = new Padding(3, 4, 3, 4);
            pnlChartRight.Name = "pnlChartRight";
            pnlChartRight.Padding = new Padding(3, 4, 3, 4);
            pnlChartRight.Size = new Size(342, 625);
            pnlChartRight.TabIndex = 4;
            // 
            // lblChartTitle
            // 
            lblChartTitle.AutoSize = true;
            lblChartTitle.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            lblChartTitle.Location = new Point(10, 10);
            lblChartTitle.Name = "lblChartTitle";
            lblChartTitle.Size = new Size(82, 15);
            lblChartTitle.TabIndex = 0;
            lblChartTitle.Text = "학습 그래프:";
            // 
            // TrainingControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splMainTraining);
            Margin = new Padding(3, 4, 3, 4);
            Name = "TrainingControl";
            Size = new Size(900, 625);
            splMainTraining.Panel1.ResumeLayout(false);
            splMainTraining.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splMainTraining).EndInit();
            splMainTraining.ResumeLayout(false);
            tableLayoutPanelLeft.ResumeLayout(false);
            pnlTrainSettings.ResumeLayout(false);
            pnlTrainSettings.PerformLayout();
            pnlProgress.ResumeLayout(false);
            pnlProgress.PerformLayout();
            pnlTrainLog.ResumeLayout(false);
            pnlTrainLog.PerformLayout();
            pnlTrainButtons.ResumeLayout(false);
            pnlChartRight.ResumeLayout(false);
            pnlChartRight.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.SplitContainer splMainTraining;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLeft;
        private System.Windows.Forms.Panel pnlTrainSettings;
        private System.Windows.Forms.Label lblDataStatus;
        private System.Windows.Forms.ComboBox cmbModelType;
        private System.Windows.Forms.TextBox txtModelPath;
        private System.Windows.Forms.ListBox lstTrainingLog;
        private System.Windows.Forms.Button btnStartTraining;
        private System.Windows.Forms.ProgressBar prgTrainingProgress;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Button btnSelectModelPath;
        private System.Windows.Forms.Panel pnlChartRight;
        private System.Windows.Forms.Label lblChartTitle;
        private System.Windows.Forms.Label lblModelType;
        private System.Windows.Forms.Label lblModelPath;
        private System.Windows.Forms.Panel pnlProgress;
        private System.Windows.Forms.Label lblProgressLabel;
        private System.Windows.Forms.Panel pnlTrainLog;
        private System.Windows.Forms.Label lblTrainingLog;
        private System.Windows.Forms.TableLayoutPanel pnlTrainButtons;
        private Button btnCheckTrainingResult;
    }
}
