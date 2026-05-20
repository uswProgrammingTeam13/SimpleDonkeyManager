namespace SimpleDonkeyManager
{
    partial class DataLoadControl
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
            label1 = new Label();
            imgListPan = new Panel();
            mainPan = new Panel();
            panelBottomInfo = new Panel();
            btnSelectFolder = new Button();
            lblFileSizeValue = new Label();
            lblResolutionValue = new Label();
            lblImageFormat = new Label();
            lblFolderPath = new Label();
            lblTotalImagesValue = new Label();
            btnLoadStart = new Button();
            btnRefresh = new Button();
            lblFileSizeTitle = new Label();
            lblResolutionTitle = new Label();
            lblFormatTitle = new Label();
            lblFolderTitle = new Label();
            lblTotalImagesTitle = new Label();
            lblSummary = new Label();
            panelBottomInfo.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2266, 1229);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(115, 32);
            label1.TabIndex = 0;
            label1.Text = "DataLoad";
            // 
            // imgListPan
            // 
            imgListPan.BackColor = Color.FromArgb(248, 248, 248);
            imgListPan.Location = new Point(17, 29);
            imgListPan.Name = "imgListPan";
            imgListPan.Size = new Size(438, 910);
            imgListPan.TabIndex = 1;
            imgListPan.Paint += imgListPan_Paint;
            // 
            // mainPan
            // 
            mainPan.BackColor = Color.FromArgb(248, 248, 248);
            mainPan.Location = new Point(489, 29);
            mainPan.Name = "mainPan";
            mainPan.Size = new Size(1860, 910);
            mainPan.TabIndex = 0;
            // 
            // panelBottomInfo
            // 
            panelBottomInfo.BackColor = Color.FromArgb(248, 248, 248);
            panelBottomInfo.Controls.Add(btnSelectFolder);
            panelBottomInfo.Controls.Add(lblFileSizeValue);
            panelBottomInfo.Controls.Add(lblResolutionValue);
            panelBottomInfo.Controls.Add(lblImageFormat);
            panelBottomInfo.Controls.Add(lblFolderPath);
            panelBottomInfo.Controls.Add(lblTotalImagesValue);
            panelBottomInfo.Controls.Add(btnLoadStart);
            panelBottomInfo.Controls.Add(btnRefresh);
            panelBottomInfo.Controls.Add(lblFileSizeTitle);
            panelBottomInfo.Controls.Add(lblResolutionTitle);
            panelBottomInfo.Controls.Add(lblFormatTitle);
            panelBottomInfo.Controls.Add(lblFolderTitle);
            panelBottomInfo.Controls.Add(lblTotalImagesTitle);
            panelBottomInfo.Controls.Add(lblSummary);
            panelBottomInfo.Location = new Point(17, 964);
            panelBottomInfo.Name = "panelBottomInfo";
            panelBottomInfo.Size = new Size(2332, 297);
            panelBottomInfo.TabIndex = 1;
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnSelectFolder.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnSelectFolder.FlatStyle = FlatStyle.Flat;
            btnSelectFolder.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSelectFolder.ForeColor = SystemColors.Highlight;
            btnSelectFolder.Location = new Point(1877, 201);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(441, 61);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.Text = "폴더선택";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // lblFileSizeValue
            // 
            lblFileSizeValue.AutoSize = true;
            lblFileSizeValue.Location = new Point(1629, 149);
            lblFileSizeValue.Name = "lblFileSizeValue";
            lblFileSizeValue.Size = new Size(75, 32);
            lblFileSizeValue.TabIndex = 12;
            lblFileSizeValue.Text = "2.45G";
            // 
            // lblResolutionValue
            // 
            lblResolutionValue.AutoSize = true;
            lblResolutionValue.Location = new Point(1297, 154);
            lblResolutionValue.Name = "lblResolutionValue";
            lblResolutionValue.Size = new Size(132, 32);
            lblResolutionValue.TabIndex = 11;
            lblResolutionValue.Text = "1280 x 720";
            // 
            // lblImageFormat
            // 
            lblImageFormat.AutoSize = true;
            lblImageFormat.Location = new Point(988, 149);
            lblImageFormat.Name = "lblImageFormat";
            lblImageFormat.Size = new Size(53, 32);
            lblImageFormat.TabIndex = 10;
            lblImageFormat.Text = ".jpg";
            // 
            // lblFolderPath
            // 
            lblFolderPath.AutoSize = true;
            lblFolderPath.Location = new Point(635, 154);
            lblFolderPath.Name = "lblFolderPath";
            lblFolderPath.Size = new Size(34, 32);
            lblFolderPath.TabIndex = 9;
            lblFolderPath.Text = "C:";
            lblFolderPath.Click += label3_Click;
            // 
            // lblTotalImagesValue
            // 
            lblTotalImagesValue.AutoSize = true;
            lblTotalImagesValue.Location = new Point(304, 154);
            lblTotalImagesValue.Name = "lblTotalImagesValue";
            lblTotalImagesValue.Size = new Size(116, 32);
            lblTotalImagesValue.TabIndex = 8;
            lblTotalImagesValue.Text = "12,345 장";
            // 
            // btnLoadStart
            // 
            btnLoadStart.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnLoadStart.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnLoadStart.FlatStyle = FlatStyle.Flat;
            btnLoadStart.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnLoadStart.ForeColor = SystemColors.Highlight;
            btnLoadStart.Location = new Point(2105, 39);
            btnLoadStart.Name = "btnLoadStart";
            btnLoadStart.Size = new Size(213, 147);
            btnLoadStart.TabIndex = 7;
            btnLoadStart.Text = "데이터 로드";
            btnLoadStart.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnRefresh.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRefresh.ForeColor = SystemColors.Highlight;
            btnRefresh.ImageAlign = ContentAlignment.TopCenter;
            btnRefresh.Location = new Point(1877, 39);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(184, 147);
            btnRefresh.TabIndex = 6;
            btnRefresh.Text = "새로고침↻";
            btnRefresh.TextImageRelation = TextImageRelation.ImageAboveText;
            btnRefresh.UseCompatibleTextRendering = true;
            btnRefresh.UseMnemonic = false;
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // lblFileSizeTitle
            // 
            lblFileSizeTitle.AutoSize = true;
            lblFileSizeTitle.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFileSizeTitle.ForeColor = Color.RoyalBlue;
            lblFileSizeTitle.Location = new Point(1609, 88);
            lblFileSizeTitle.Name = "lblFileSizeTitle";
            lblFileSizeTitle.Size = new Size(143, 40);
            lblFileSizeTitle.TabIndex = 5;
            lblFileSizeTitle.Text = "파일 크기";
            lblFileSizeTitle.Click += label6_Click;
            // 
            // lblResolutionTitle
            // 
            lblResolutionTitle.AutoSize = true;
            lblResolutionTitle.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblResolutionTitle.ForeColor = Color.RoyalBlue;
            lblResolutionTitle.Location = new Point(1297, 88);
            lblResolutionTitle.Name = "lblResolutionTitle";
            lblResolutionTitle.Size = new Size(104, 40);
            lblResolutionTitle.TabIndex = 4;
            lblResolutionTitle.Text = "해상도";
            // 
            // lblFormatTitle
            // 
            lblFormatTitle.AutoSize = true;
            lblFormatTitle.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFormatTitle.ForeColor = Color.RoyalBlue;
            lblFormatTitle.Location = new Point(979, 88);
            lblFormatTitle.Name = "lblFormatTitle";
            lblFormatTitle.Size = new Size(172, 40);
            lblFormatTitle.TabIndex = 3;
            lblFormatTitle.Text = "이미지 형식";
            // 
            // lblFolderTitle
            // 
            lblFolderTitle.AutoSize = true;
            lblFolderTitle.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFolderTitle.ForeColor = Color.RoyalBlue;
            lblFolderTitle.Location = new Point(635, 90);
            lblFolderTitle.Name = "lblFolderTitle";
            lblFolderTitle.Size = new Size(172, 40);
            lblFolderTitle.TabIndex = 2;
            lblFolderTitle.Text = "선택된 폴더";
            // 
            // lblTotalImagesTitle
            // 
            lblTotalImagesTitle.AutoSize = true;
            lblTotalImagesTitle.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTotalImagesTitle.ForeColor = Color.RoyalBlue;
            lblTotalImagesTitle.Location = new Point(291, 88);
            lblTotalImagesTitle.Name = "lblTotalImagesTitle";
            lblTotalImagesTitle.Size = new Size(162, 40);
            lblTotalImagesTitle.TabIndex = 1;
            lblTotalImagesTitle.Text = "전체이미지";
            lblTotalImagesTitle.Click += label2_Click;
            // 
            // lblSummary
            // 
            lblSummary.AutoSize = true;
            lblSummary.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblSummary.ForeColor = Color.RoyalBlue;
            lblSummary.Location = new Point(41, 26);
            lblSummary.Name = "lblSummary";
            lblSummary.Size = new Size(221, 36);
            lblSummary.TabIndex = 0;
            lblSummary.Text = "로드 요약 정보";
            // 
            // DataLoadControl
            // 
            AutoScaleDimensions = new SizeF(14F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelBottomInfo);
            Controls.Add(mainPan);
            Controls.Add(imgListPan);
            Controls.Add(label1);
            Margin = new Padding(6);
            Name = "DataLoadControl";
            Size = new Size(2352, 1280);
            Load += DataLoadControl_Load;
            panelBottomInfo.ResumeLayout(false);
            panelBottomInfo.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel imgListPan;
        private Panel mainPan;
        private Panel panelBottomInfo;
        private Label lblSummary;
        private Label lblFileSizeTitle;
        private Label lblResolutionTitle;
        private Label lblFormatTitle;
        private Label lblFolderTitle;
        private Label lblTotalImagesTitle;
        private Button btnLoadStart;
        private Button btnRefresh;
        private Label lblFileSizeValue;
        private Label lblResolutionValue;
        private Label lblImageFormat;
        private Label lblFolderPath;
        private Label lblTotalImagesValue;
        private PictureBox pictureBox1;
        private Button btnSelectFolder;
    }
}
