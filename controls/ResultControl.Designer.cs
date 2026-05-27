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
            grpChart = new GroupBox();
            pnlResultChart = new Panel();
            pnlRight = new Panel();
            grpImagePreview = new GroupBox();
            imageViewerUpper1 = new SimpleDonkeyManager.controlutils.ImageViewerUpper();
            tlpResultMain.SuspendLayout();
            pnlLeft.SuspendLayout();
            tlpResultLeft.SuspendLayout();
            grpSummary.SuspendLayout();
            tlpSummary.SuspendLayout();
            grpChart.SuspendLayout();
            pnlRight.SuspendLayout();
            grpImagePreview.SuspendLayout();
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
            tlpSummary.Dock = DockStyle.Fill;
            tlpSummary.Name = "tlpSummary";
            tlpSummary.RowCount = 4;
            tlpSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpSummary.TabIndex = 4;
            tlpSummary.Padding = new Padding(6, 4, 6, 4);
            // 
            // lblTotalEpochs
            // 
            lblTotalEpochs.AutoSize = false;
            lblTotalEpochs.Dock = DockStyle.Fill;
            lblTotalEpochs.Font = new Font("나눔고딕", 18F, FontStyle.Bold);
            lblTotalEpochs.Name = "lblTotalEpochs";
            lblTotalEpochs.TabIndex = 0;
            lblTotalEpochs.Text = "총 에포크: 0";
            lblTotalEpochs.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMinLoss
            // 
            lblMinLoss.AutoSize = false;
            lblMinLoss.Dock = DockStyle.Fill;
            lblMinLoss.Font = new Font("나눔고딕", 18F, FontStyle.Bold);
            lblMinLoss.Name = "lblMinLoss";
            lblMinLoss.TabIndex = 1;
            lblMinLoss.Text = "최소 손실값: 0.0000";
            lblMinLoss.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMaxAccuracy
            // 
            lblMaxAccuracy.AutoSize = false;
            lblMaxAccuracy.Dock = DockStyle.Fill;
            lblMaxAccuracy.Font = new Font("나눔고딕", 18F, FontStyle.Bold);
            lblMaxAccuracy.Name = "lblMaxAccuracy";
            lblMaxAccuracy.TabIndex = 2;
            lblMaxAccuracy.Text = "최고 정확도: 0.0000";
            lblMaxAccuracy.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTrainingTime
            // 
            lblTrainingTime.AutoSize = false;
            lblTrainingTime.Dock = DockStyle.Fill;
            lblTrainingTime.Font = new Font("나눔고딕", 18F, FontStyle.Bold);
            lblTrainingTime.Name = "lblTrainingTime";
            lblTrainingTime.TabIndex = 3;
            lblTrainingTime.Text = "소요 시간: 0초";
            lblTrainingTime.TextAlign = ContentAlignment.MiddleLeft;
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
            pnlRight.Controls.Add(grpImagePreview);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Location = new Point(790, 3);
            pnlRight.Name = "pnlRight";
            pnlRight.Size = new Size(383, 594);
            pnlRight.TabIndex = 1;
            // 
            // grpImagePreview
            // 
            grpImagePreview.Controls.Add(imageViewerUpper1);
            grpImagePreview.Dock = DockStyle.Fill;
            grpImagePreview.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold);
            grpImagePreview.ForeColor = Color.RoyalBlue;
            grpImagePreview.Location = new Point(0, 0);
            grpImagePreview.Name = "grpImagePreview";
            grpImagePreview.Size = new Size(383, 594);
            grpImagePreview.TabIndex = 0;
            grpImagePreview.TabStop = false;
            grpImagePreview.Text = "이미지 미리보기";
            // 
            // imageViewerUpper1
            // 
            imageViewerUpper1.Dock = DockStyle.Fill;
            imageViewerUpper1.Location = new Point(3, 25);
            imageViewerUpper1.Name = "imageViewerUpper1";
            imageViewerUpper1.Size = new Size(377, 566);
            imageViewerUpper1.TabIndex = 0;
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
            grpImagePreview.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
        private GroupBox grpImagePreview;
        private Label lblTotalEpochs;
        private Label lblMinLoss;
        private Label lblMaxAccuracy;
        private Label lblTrainingTime;
        private SimpleDonkeyManager.controlutils.ImageViewerUpper imageViewerUpper1;
    }
}
