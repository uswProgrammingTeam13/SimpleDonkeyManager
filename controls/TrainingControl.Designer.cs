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
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
            System.Windows.Forms.Panel pnlTrainSettings;
            System.Windows.Forms.Label lblDataStatus;
            System.Windows.Forms.Label lblModelType;
            System.Windows.Forms.Label lblModelPath;
            System.Windows.Forms.Panel pnlProgress;
            System.Windows.Forms.Label lblProgressLabel;
            System.Windows.Forms.ProgressBar prgTrainingProgress;
            System.Windows.Forms.Label lblProgress;
            System.Windows.Forms.Panel pnlTrainLog;
            System.Windows.Forms.Label lblTrainingLog;
            System.Windows.Forms.ListBox lstTrainingLog;
            System.Windows.Forms.Panel pnlTrainButtons;
            System.Windows.Forms.Button btnSelectModelPath;
            System.Windows.Forms.Button btnStartTraining;
            System.Windows.Forms.ComboBox cmbModelType;
            System.Windows.Forms.TextBox txtModelPath;

            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            pnlTrainSettings = new System.Windows.Forms.Panel();
            lblDataStatus = new System.Windows.Forms.Label();
            lblModelType = new System.Windows.Forms.Label();
            lblModelPath = new System.Windows.Forms.Label();
            pnlProgress = new System.Windows.Forms.Panel();
            lblProgressLabel = new System.Windows.Forms.Label();
            prgTrainingProgress = new System.Windows.Forms.ProgressBar();
            lblProgress = new System.Windows.Forms.Label();
            pnlTrainLog = new System.Windows.Forms.Panel();
            lblTrainingLog = new System.Windows.Forms.Label();
            lstTrainingLog = new System.Windows.Forms.ListBox();
            pnlTrainButtons = new System.Windows.Forms.Panel();
            btnSelectModelPath = new System.Windows.Forms.Button();
            btnStartTraining = new System.Windows.Forms.Button();
            cmbModelType = new System.Windows.Forms.ComboBox();
            txtModelPath = new System.Windows.Forms.TextBox();

            this.SuspendLayout();
            pnlTrainSettings.SuspendLayout();
            pnlProgress.SuspendLayout();
            pnlTrainLog.SuspendLayout();
            pnlTrainButtons.SuspendLayout();

            // Store references
            this.lblDataStatus = lblDataStatus;
            this.cmbModelType = cmbModelType;
            this.txtModelPath = txtModelPath;
            this.lstTrainingLog = lstTrainingLog;
            this.btnStartTraining = btnStartTraining;
            this.prgTrainingProgress = prgTrainingProgress;
            this.lblProgress = lblProgress;
            this.btnSelectModelPath = btnSelectModelPath;

            // tableLayoutPanel1
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(pnlTrainSettings, 0, 0);
            tableLayoutPanel1.Controls.Add(pnlProgress, 0, 1);
            tableLayoutPanel1.Controls.Add(pnlTrainLog, 0, 2);
            tableLayoutPanel1.Controls.Add(pnlTrainButtons, 0, 3);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            tableLayoutPanel1.Size = new System.Drawing.Size(600, 500);
            tableLayoutPanel1.TabIndex = 0;

            // pnlTrainSettings
            pnlTrainSettings.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            pnlTrainSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlTrainSettings.Controls.Add(lblDataStatus);
            pnlTrainSettings.Controls.Add(lblModelType);
            pnlTrainSettings.Controls.Add(cmbModelType);
            pnlTrainSettings.Controls.Add(lblModelPath);
            pnlTrainSettings.Controls.Add(txtModelPath);
            pnlTrainSettings.Controls.Add(btnSelectModelPath);
            pnlTrainSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlTrainSettings.Location = new System.Drawing.Point(3, 3);
            pnlTrainSettings.Name = "pnlTrainSettings";
            pnlTrainSettings.Size = new System.Drawing.Size(594, 114);
            pnlTrainSettings.TabIndex = 0;

            // lblDataStatus
            lblDataStatus.AutoSize = true;
            lblDataStatus.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Bold);
            lblDataStatus.Location = new System.Drawing.Point(10, 10);
            lblDataStatus.Name = "lblDataStatus";
            lblDataStatus.Size = new System.Drawing.Size(150, 14);
            lblDataStatus.TabIndex = 0;
            lblDataStatus.Text = "데이터셋: 준비되지 않음";
            lblDataStatus.ForeColor = System.Drawing.Color.Red;

            // lblModelType
            lblModelType.AutoSize = true;
            lblModelType.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            lblModelType.Location = new System.Drawing.Point(10, 35);
            lblModelType.Name = "lblModelType";
            lblModelType.Size = new System.Drawing.Size(86, 14);
            lblModelType.TabIndex = 1;
            lblModelType.Text = "모델 타입:";

            // cmbModelType
            cmbModelType.FormattingEnabled = true;
            cmbModelType.Location = new System.Drawing.Point(100, 35);
            cmbModelType.Name = "cmbModelType";
            cmbModelType.Size = new System.Drawing.Size(120, 20);
            cmbModelType.TabIndex = 2;

            // lblModelPath
            lblModelPath.AutoSize = true;
            lblModelPath.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            lblModelPath.Location = new System.Drawing.Point(10, 60);
            lblModelPath.Name = "lblModelPath";
            lblModelPath.Size = new System.Drawing.Size(86, 14);
            lblModelPath.TabIndex = 3;
            lblModelPath.Text = "모델 경로:";

            // txtModelPath
            txtModelPath.Location = new System.Drawing.Point(100, 60);
            txtModelPath.Name = "txtModelPath";
            txtModelPath.ReadOnly = true;
            txtModelPath.Size = new System.Drawing.Size(380, 20);
            txtModelPath.TabIndex = 4;

            // btnSelectModelPath
            btnSelectModelPath.Location = new System.Drawing.Point(490, 60);
            btnSelectModelPath.Name = "btnSelectModelPath";
            btnSelectModelPath.Size = new System.Drawing.Size(90, 24);
            btnSelectModelPath.TabIndex = 5;
            btnSelectModelPath.Text = "경로 선택";
            btnSelectModelPath.UseVisualStyleBackColor = true;

            // pnlProgress
            pnlProgress.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
            pnlProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlProgress.Controls.Add(lblProgressLabel);
            pnlProgress.Controls.Add(prgTrainingProgress);
            pnlProgress.Controls.Add(lblProgress);
            pnlProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlProgress.Location = new System.Drawing.Point(3, 123);
            pnlProgress.Name = "pnlProgress";
            pnlProgress.Size = new System.Drawing.Size(594, 74);
            pnlProgress.TabIndex = 1;

            // lblProgressLabel
            lblProgressLabel.AutoSize = true;
            lblProgressLabel.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Bold);
            lblProgressLabel.Location = new System.Drawing.Point(10, 5);
            lblProgressLabel.Name = "lblProgressLabel";
            lblProgressLabel.Size = new System.Drawing.Size(87, 14);
            lblProgressLabel.TabIndex = 0;
            lblProgressLabel.Text = "학습 진행도:";

            // prgTrainingProgress
            prgTrainingProgress.Location = new System.Drawing.Point(10, 25);
            prgTrainingProgress.Name = "prgTrainingProgress";
            prgTrainingProgress.Size = new System.Drawing.Size(530, 23);
            prgTrainingProgress.TabIndex = 1;
            prgTrainingProgress.Value = 0;

            // lblProgress
            lblProgress.AutoSize = true;
            lblProgress.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Bold);
            lblProgress.Location = new System.Drawing.Point(550, 25);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new System.Drawing.Size(30, 14);
            lblProgress.TabIndex = 2;
            lblProgress.Text = "0%";

            // pnlTrainLog
            pnlTrainLog.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
            pnlTrainLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlTrainLog.Controls.Add(lblTrainingLog);
            pnlTrainLog.Controls.Add(lstTrainingLog);
            pnlTrainLog.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlTrainLog.Location = new System.Drawing.Point(3, 203);
            pnlTrainLog.Name = "pnlTrainLog";
            pnlTrainLog.Size = new System.Drawing.Size(594, 230);
            pnlTrainLog.TabIndex = 2;

            // lblTrainingLog
            lblTrainingLog.AutoSize = true;
            lblTrainingLog.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Bold);
            lblTrainingLog.Location = new System.Drawing.Point(10, 5);
            lblTrainingLog.Name = "lblTrainingLog";
            lblTrainingLog.Size = new System.Drawing.Size(87, 14);
            lblTrainingLog.TabIndex = 0;
            lblTrainingLog.Text = "학습 로그:";

            // lstTrainingLog
            lstTrainingLog.FormattingEnabled = true;
            lstTrainingLog.Location = new System.Drawing.Point(10, 25);
            lstTrainingLog.Name = "lstTrainingLog";
            lstTrainingLog.Size = new System.Drawing.Size(570, 200);
            lstTrainingLog.TabIndex = 1;

            // pnlTrainButtons
            pnlTrainButtons.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            pnlTrainButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlTrainButtons.Controls.Add(btnStartTraining);
            pnlTrainButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlTrainButtons.Location = new System.Drawing.Point(3, 439);
            pnlTrainButtons.Name = "pnlTrainButtons";
            pnlTrainButtons.Size = new System.Drawing.Size(594, 54);
            pnlTrainButtons.TabIndex = 3;

            // btnStartTraining
            btnStartTraining.BackColor = System.Drawing.SystemColors.Highlight;
            btnStartTraining.Font = new System.Drawing.Font("나눔고딕", 10.2F, System.Drawing.FontStyle.Bold);
            btnStartTraining.ForeColor = System.Drawing.Color.White;
            btnStartTraining.Location = new System.Drawing.Point(150, 10);
            btnStartTraining.Name = "btnStartTraining";
            btnStartTraining.Size = new System.Drawing.Size(300, 35);
            btnStartTraining.TabIndex = 0;
            btnStartTraining.Text = "▷ 학습 시작";
            btnStartTraining.UseVisualStyleBackColor = false;

            // TrainingControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(tableLayoutPanel1);
            this.Name = "TrainingControl";
            this.Size = new System.Drawing.Size(600, 500);

            this.ResumeLayout(false);
            pnlTrainSettings.ResumeLayout(false);
            pnlTrainSettings.PerformLayout();
            pnlProgress.ResumeLayout(false);
            pnlProgress.PerformLayout();
            pnlTrainLog.ResumeLayout(false);
            pnlTrainLog.PerformLayout();
            pnlTrainButtons.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblDataStatus;
        private System.Windows.Forms.ComboBox cmbModelType;
        private System.Windows.Forms.TextBox txtModelPath;
        private System.Windows.Forms.ListBox lstTrainingLog;
        private System.Windows.Forms.Button btnStartTraining;
        private System.Windows.Forms.ProgressBar prgTrainingProgress;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Button btnSelectModelPath;
    }
}
