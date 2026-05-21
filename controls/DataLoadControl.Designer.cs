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
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            pnlImageView = new Panel();
            pnlFrameList = new Panel();
            groupBox1 = new GroupBox();
            btnSelectFolder = new Button();
            lblFileSizeValue = new Label();
            lblResolutionValue = new Label();
            lblImageFormat = new Label();
            lblTotalImagesValue = new Label();
            btnLoadStart = new Button();
            btnRefresh = new Button();
            lblFileSizeTitle = new Label();
            lblResolutionTitle = new Label();
            lblFormatTitle = new Label();
            lblTotalImagesTitle = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1 (메인 2열: 좌측 이미지 리스트, 우측 컨텐츠)
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 77F));
            tableLayoutPanel1.Controls.Add(pnlFrameList, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1176, 600);
            tableLayoutPanel1.TabIndex = 14;
            // 
            // tableLayoutPanel2 (우측 2행: 위 이미지, 아래 정보)
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(pnlImageView, 0, 0);
            tableLayoutPanel2.Controls.Add(groupBox1, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(273, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel2.Size = new Size(900, 594);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3 (사용하지 않음 - pnlImageView가 직접 포함됨)
            // 
            // pnlImageView
            // 
            pnlImageView.BackColor = Color.FromArgb(248, 248, 248);
            pnlImageView.Dock = DockStyle.Fill;
            pnlImageView.Location = new Point(3, 3);
            pnlImageView.Margin = new Padding(3);
            pnlImageView.Name = "pnlImageView";
            pnlImageView.Size = new Size(894, 409);
            pnlImageView.TabIndex = 1;
            // 
            // pnlFrameList
            // 
            pnlFrameList.BackColor = Color.FromArgb(248, 248, 248);
            pnlFrameList.Dock = DockStyle.Fill;
            pnlFrameList.Location = new Point(3, 3);
            pnlFrameList.Margin = new Padding(3);
            pnlFrameList.Name = "pnlFrameList";
            pnlFrameList.Size = new Size(264, 594);
            pnlFrameList.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.ControlLightLight;
            groupBox1.Controls.Add(btnSelectFolder);
            groupBox1.Controls.Add(lblFileSizeValue);
            groupBox1.Controls.Add(lblResolutionValue);
            groupBox1.Controls.Add(lblImageFormat);
            groupBox1.Controls.Add(lblTotalImagesValue);
            groupBox1.Controls.Add(btnLoadStart);
            groupBox1.Controls.Add(btnRefresh);
            groupBox1.Controls.Add(lblFileSizeTitle);
            groupBox1.Controls.Add(lblResolutionTitle);
            groupBox1.Controls.Add(lblFormatTitle);
            groupBox1.Controls.Add(lblTotalImagesTitle);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            groupBox1.ForeColor = Color.RoyalBlue;
            groupBox1.Location = new Point(3, 415);
            groupBox1.Margin = new Padding(3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(894, 173);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "데이터 폴더 로드";
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSelectFolder.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnSelectFolder.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnSelectFolder.FlatStyle = FlatStyle.Flat;
            btnSelectFolder.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSelectFolder.ForeColor = SystemColors.Highlight;
            btnSelectFolder.Location = new Point(669, 102);
            btnSelectFolder.Margin = new Padding(2, 1, 2, 1);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(220, 29);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.Text = "폴더 선택";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // lblFileSizeValue
            // 
            lblFileSizeValue.AutoSize = true;
            lblFileSizeValue.Location = new Point(515, 84);
            lblFileSizeValue.Margin = new Padding(2, 0, 2, 0);
            lblFileSizeValue.Name = "lblFileSizeValue";
            lblFileSizeValue.Size = new Size(72, 21);
            lblFileSizeValue.TabIndex = 12;
            lblFileSizeValue.Text = "2.45G";
            // 
            // lblResolutionValue
            // 
            lblResolutionValue.AutoSize = true;
            lblResolutionValue.Location = new Point(355, 84);
            lblResolutionValue.Margin = new Padding(2, 0, 2, 0);
            lblResolutionValue.Name = "lblResolutionValue";
            lblResolutionValue.Size = new Size(124, 21);
            lblResolutionValue.TabIndex = 11;
            lblResolutionValue.Text = "1280 x 720";
            // 
            // lblImageFormat
            // 
            lblImageFormat.AutoSize = true;
            lblImageFormat.Location = new Point(201, 84);
            lblImageFormat.Margin = new Padding(2, 0, 2, 0);
            lblImageFormat.Name = "lblImageFormat";
            lblImageFormat.Size = new Size(49, 21);
            lblImageFormat.TabIndex = 10;
            lblImageFormat.Text = ".jpg";
            // 
            // lblTotalImagesValue
            // 
            lblTotalImagesValue.AutoSize = true;
            lblTotalImagesValue.Location = new Point(24, 84);
            lblTotalImagesValue.Margin = new Padding(2, 0, 2, 0);
            lblTotalImagesValue.Name = "lblTotalImagesValue";
            lblTotalImagesValue.Size = new Size(107, 21);
            lblTotalImagesValue.TabIndex = 8;
            lblTotalImagesValue.Text = "12,345 장";
            // 
            // btnLoadStart
            // 
            btnLoadStart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoadStart.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnLoadStart.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnLoadStart.FlatStyle = FlatStyle.Flat;
            btnLoadStart.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnLoadStart.ForeColor = SystemColors.Highlight;
            btnLoadStart.Location = new Point(783, 26);
            btnLoadStart.Margin = new Padding(2, 1, 2, 1);
            btnLoadStart.Name = "btnLoadStart";
            btnLoadStart.Size = new Size(106, 69);
            btnLoadStart.TabIndex = 7;
            btnLoadStart.Text = "데이터 로드";
            btnLoadStart.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnRefresh.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRefresh.ForeColor = SystemColors.Highlight;
            btnRefresh.ImageAlign = ContentAlignment.TopCenter;
            btnRefresh.Location = new Point(669, 26);
            btnRefresh.Margin = new Padding(2, 1, 2, 1);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(102, 69);
            btnRefresh.TabIndex = 6;
            btnRefresh.Text = "새로고침 ↻";
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
            lblFileSizeTitle.Location = new Point(513, 51);
            lblFileSizeTitle.Margin = new Padding(2, 0, 2, 0);
            lblFileSizeTitle.Name = "lblFileSizeTitle";
            lblFileSizeTitle.Size = new Size(74, 20);
            lblFileSizeTitle.TabIndex = 5;
            lblFileSizeTitle.Text = "파일 크기";
            lblFileSizeTitle.Click += label6_Click;
            // 
            // lblResolutionTitle
            // 
            lblResolutionTitle.AutoSize = true;
            lblResolutionTitle.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblResolutionTitle.ForeColor = Color.RoyalBlue;
            lblResolutionTitle.Location = new Point(355, 51);
            lblResolutionTitle.Margin = new Padding(2, 0, 2, 0);
            lblResolutionTitle.Name = "lblResolutionTitle";
            lblResolutionTitle.Size = new Size(54, 20);
            lblResolutionTitle.TabIndex = 4;
            lblResolutionTitle.Text = "해상도";
            // 
            // lblFormatTitle
            // 
            lblFormatTitle.AutoSize = true;
            lblFormatTitle.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFormatTitle.ForeColor = Color.RoyalBlue;
            lblFormatTitle.Location = new Point(201, 51);
            lblFormatTitle.Margin = new Padding(2, 0, 2, 0);
            lblFormatTitle.Name = "lblFormatTitle";
            lblFormatTitle.Size = new Size(89, 20);
            lblFormatTitle.TabIndex = 3;
            lblFormatTitle.Text = "이미지 형식";
            // 
            // lblTotalImagesTitle
            // 
            lblTotalImagesTitle.AutoSize = true;
            lblTotalImagesTitle.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTotalImagesTitle.ForeColor = Color.RoyalBlue;
            lblTotalImagesTitle.Location = new Point(24, 51);
            lblTotalImagesTitle.Margin = new Padding(2, 0, 2, 0);
            lblTotalImagesTitle.Name = "lblTotalImagesTitle";
            lblTotalImagesTitle.Size = new Size(89, 20);
            lblTotalImagesTitle.TabIndex = 1;
            lblTotalImagesTitle.Text = "전체 이미지";
            lblTotalImagesTitle.Click += label2_Click;
            // 
            // DataLoadControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            Controls.Add(tableLayoutPanel1);
            Name = "DataLoadControl";
            Size = new Size(1176, 600);
            Load += DataLoadControl_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel pnlImageView;
        private PictureBox pictureBox1;
        private Panel pnlFrameList;
        private Button btnSelectFolder;
        private Label lblFileSizeValue;
        private Label lblResolutionValue;
        private Label lblImageFormat;
        private Label lblTotalImagesValue;
        private Button btnLoadStart;
        private Button btnRefresh;
        private Label lblFileSizeTitle;
        private Label lblResolutionTitle;
        private Label lblFormatTitle;
        private Label lblTotalImagesTitle;
        private GroupBox groupBox1;
    }
}
