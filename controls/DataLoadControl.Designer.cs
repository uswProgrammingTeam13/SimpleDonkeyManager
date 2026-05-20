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
            btnSelecFolder = new Button();
            lblFileSize1 = new Label();
            lblResolution1 = new Label();
            lblImageFormat1 = new Label();
            lblFolderPath1 = new Label();
            lblTotalImages1 = new Label();
            btnDataReroad = new Button();
            btnRefresh = new Button();
            lblFileSize = new Label();
            lblResolution = new Label();
            lblImageFormat = new Label();
            lblFolderPath = new Label();
            lblTotalImages = new Label();
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
            panelBottomInfo.Controls.Add(btnSelecFolder);
            panelBottomInfo.Controls.Add(lblFileSize1);
            panelBottomInfo.Controls.Add(lblResolution1);
            panelBottomInfo.Controls.Add(lblImageFormat1);
            panelBottomInfo.Controls.Add(lblFolderPath1);
            panelBottomInfo.Controls.Add(lblTotalImages1);
            panelBottomInfo.Controls.Add(btnDataReroad);
            panelBottomInfo.Controls.Add(btnRefresh);
            panelBottomInfo.Controls.Add(lblFileSize);
            panelBottomInfo.Controls.Add(lblResolution);
            panelBottomInfo.Controls.Add(lblImageFormat);
            panelBottomInfo.Controls.Add(lblFolderPath);
            panelBottomInfo.Controls.Add(lblTotalImages);
            panelBottomInfo.Controls.Add(lblSummary);
            panelBottomInfo.Location = new Point(17, 964);
            panelBottomInfo.Name = "panelBottomInfo";
            panelBottomInfo.Size = new Size(2332, 297);
            panelBottomInfo.TabIndex = 1;
            // 
            // btnSelecFolder
            // 
            btnSelecFolder.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnSelecFolder.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnSelecFolder.FlatStyle = FlatStyle.Flat;
            btnSelecFolder.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSelecFolder.ForeColor = SystemColors.Highlight;
            btnSelecFolder.Location = new Point(1877, 201);
            btnSelecFolder.Name = "btnSelecFolder";
            btnSelecFolder.Size = new Size(441, 61);
            btnSelecFolder.TabIndex = 0;
            btnSelecFolder.Text = "폴더선택";
            btnSelecFolder.UseVisualStyleBackColor = true;
            // 
            // lblFileSize1
            // 
            lblFileSize1.AutoSize = true;
            lblFileSize1.Location = new Point(1629, 149);
            lblFileSize1.Name = "lblFileSize1";
            lblFileSize1.Size = new Size(75, 32);
            lblFileSize1.TabIndex = 12;
            lblFileSize1.Text = "2.45G";
            // 
            // lblResolution1
            // 
            lblResolution1.AutoSize = true;
            lblResolution1.Location = new Point(1297, 154);
            lblResolution1.Name = "lblResolution1";
            lblResolution1.Size = new Size(132, 32);
            lblResolution1.TabIndex = 11;
            lblResolution1.Text = "1280 x 720";
            // 
            // lblImageFormat1
            // 
            lblImageFormat1.AutoSize = true;
            lblImageFormat1.Location = new Point(988, 149);
            lblImageFormat1.Name = "lblImageFormat1";
            lblImageFormat1.Size = new Size(53, 32);
            lblImageFormat1.TabIndex = 10;
            lblImageFormat1.Text = ".jpg";
            // 
            // lblFolderPath1
            // 
            lblFolderPath1.AutoSize = true;
            lblFolderPath1.Location = new Point(635, 154);
            lblFolderPath1.Name = "lblFolderPath1";
            lblFolderPath1.Size = new Size(34, 32);
            lblFolderPath1.TabIndex = 9;
            lblFolderPath1.Text = "C:";
            lblFolderPath1.Click += label3_Click;
            // 
            // lblTotalImages1
            // 
            lblTotalImages1.AutoSize = true;
            lblTotalImages1.Location = new Point(304, 154);
            lblTotalImages1.Name = "lblTotalImages1";
            lblTotalImages1.Size = new Size(116, 32);
            lblTotalImages1.TabIndex = 8;
            lblTotalImages1.Text = "12,345 장";
            // 
            // btnDataReroad
            // 
            btnDataReroad.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnDataReroad.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnDataReroad.FlatStyle = FlatStyle.Flat;
            btnDataReroad.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnDataReroad.ForeColor = SystemColors.Highlight;
            btnDataReroad.Location = new Point(2105, 39);
            btnDataReroad.Name = "btnDataReroad";
            btnDataReroad.Size = new Size(213, 147);
            btnDataReroad.TabIndex = 7;
            btnDataReroad.Text = "데이터 로드";
            btnDataReroad.UseVisualStyleBackColor = true;
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
            // lblFileSize
            // 
            lblFileSize.AutoSize = true;
            lblFileSize.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFileSize.ForeColor = Color.RoyalBlue;
            lblFileSize.Location = new Point(1609, 88);
            lblFileSize.Name = "lblFileSize";
            lblFileSize.Size = new Size(143, 40);
            lblFileSize.TabIndex = 5;
            lblFileSize.Text = "파일 크기";
            lblFileSize.Click += label6_Click;
            // 
            // lblResolution
            // 
            lblResolution.AutoSize = true;
            lblResolution.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblResolution.ForeColor = Color.RoyalBlue;
            lblResolution.Location = new Point(1297, 88);
            lblResolution.Name = "lblResolution";
            lblResolution.Size = new Size(104, 40);
            lblResolution.TabIndex = 4;
            lblResolution.Text = "해상도";
            // 
            // lblImageFormat
            // 
            lblImageFormat.AutoSize = true;
            lblImageFormat.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblImageFormat.ForeColor = Color.RoyalBlue;
            lblImageFormat.Location = new Point(979, 88);
            lblImageFormat.Name = "lblImageFormat";
            lblImageFormat.Size = new Size(172, 40);
            lblImageFormat.TabIndex = 3;
            lblImageFormat.Text = "이미지 형식";
            // 
            // lblFolderPath
            // 
            lblFolderPath.AutoSize = true;
            lblFolderPath.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFolderPath.ForeColor = Color.RoyalBlue;
            lblFolderPath.Location = new Point(635, 90);
            lblFolderPath.Name = "lblFolderPath";
            lblFolderPath.Size = new Size(172, 40);
            lblFolderPath.TabIndex = 2;
            lblFolderPath.Text = "선택된 폴더";
            // 
            // lblTotalImages
            // 
            lblTotalImages.AutoSize = true;
            lblTotalImages.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTotalImages.ForeColor = Color.RoyalBlue;
            lblTotalImages.Location = new Point(291, 88);
            lblTotalImages.Name = "lblTotalImages";
            lblTotalImages.Size = new Size(162, 40);
            lblTotalImages.TabIndex = 1;
            lblTotalImages.Text = "전체이미지";
            lblTotalImages.Click += label2_Click;
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
        private Label lblFileSize;
        private Label lblResolution;
        private Label lblImageFormat;
        private Label lblFolderPath;
        private Label lblTotalImages;
        private Button btnDataReroad;
        private Button btnRefresh;
        private Label lblFileSize1;
        private Label lblResolution1;
        private Label lblImageFormat1;
        private Label lblFolderPath1;
        private Label lblTotalImages1;
        private PictureBox pictureBox1;
        private Button btnSelecFolder;
    }
}
