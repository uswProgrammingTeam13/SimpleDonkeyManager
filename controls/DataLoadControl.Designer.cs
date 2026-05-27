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
            pnlFrameList = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            pnlImageView = new Panel();
            groupBox1 = new GroupBox();
            tlpInfoBar = new TableLayoutPanel();
            btnSelectFolder = new Button();
            lblFileSizeValue = new Label();
            lblResolutionValue = new Label();
            lblImageFormat = new Label();
            lblTotalImagesValue = new Label();
            btnLoadStart = new Button();
            lblFileSizeTitle = new Label();
            lblResolutionTitle = new Label();
            lblFormatTitle = new Label();
            lblTotalImagesTitle = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox1.SuspendLayout();
            tlpInfoBar.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
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
            // pnlFrameList
            // 
            pnlFrameList.BackColor = Color.FromArgb(248, 248, 248);
            pnlFrameList.Dock = DockStyle.Fill;
            pnlFrameList.Location = new Point(3, 3);
            pnlFrameList.Name = "pnlFrameList";
            pnlFrameList.Size = new Size(264, 594);
            pnlFrameList.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(pnlImageView, 0, 0);
            tableLayoutPanel2.Controls.Add(groupBox1, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(273, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 77.27273F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 22.727272F));
            tableLayoutPanel2.Size = new Size(900, 594);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // pnlImageView
            // 
            pnlImageView.BackColor = Color.FromArgb(248, 248, 248);
            pnlImageView.Dock = DockStyle.Fill;
            pnlImageView.Location = new Point(3, 3);
            pnlImageView.Name = "pnlImageView";
            pnlImageView.Size = new Size(894, 453);
            pnlImageView.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.ControlLightLight;
            groupBox1.Controls.Add(tlpInfoBar);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            groupBox1.ForeColor = Color.RoyalBlue;
            groupBox1.Location = new Point(3, 462);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(6, 4, 6, 4);
            groupBox1.Size = new Size(894, 129);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "데이터 폴더 로드";
            // 
            // tlpInfoBar — 5컬럼 적응형 레이아웃 (정보 4개 + 버튼 영역)
            // 
            tlpInfoBar.ColumnCount = 5;
            tlpInfoBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpInfoBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpInfoBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpInfoBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpInfoBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpInfoBar.Controls.Add(lblTotalImagesTitle, 0, 0);
            tlpInfoBar.Controls.Add(lblTotalImagesValue, 0, 1);
            tlpInfoBar.Controls.Add(lblFormatTitle, 1, 0);
            tlpInfoBar.Controls.Add(lblImageFormat, 1, 1);
            tlpInfoBar.Controls.Add(lblResolutionTitle, 2, 0);
            tlpInfoBar.Controls.Add(lblResolutionValue, 2, 1);
            tlpInfoBar.Controls.Add(lblFileSizeTitle, 3, 0);
            tlpInfoBar.Controls.Add(lblFileSizeValue, 3, 1);
            tlpInfoBar.Controls.Add(btnLoadStart, 4, 0);
            tlpInfoBar.SetRowSpan(btnLoadStart, 2);
            tlpInfoBar.Controls.Add(btnSelectFolder, 4, 2);
            tlpInfoBar.RowCount = 3;
            tlpInfoBar.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tlpInfoBar.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tlpInfoBar.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tlpInfoBar.Dock = DockStyle.Fill;
            tlpInfoBar.Location = new Point(6, 26);
            tlpInfoBar.Name = "tlpInfoBar";
            tlpInfoBar.TabIndex = 0;
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Dock = DockStyle.Fill;
            btnSelectFolder.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnSelectFolder.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnSelectFolder.FlatStyle = FlatStyle.Flat;
            btnSelectFolder.Font = new Font("나눔고딕", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSelectFolder.ForeColor = SystemColors.Highlight;
            btnSelectFolder.Margin = new Padding(4, 2, 4, 2);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.TabIndex = 1;
            btnSelectFolder.Text = "📁 폴더 선택";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // lblFileSizeValue
            // 
            lblFileSizeValue.AutoSize = false;
            lblFileSizeValue.Dock = DockStyle.Fill;
            lblFileSizeValue.Margin = new Padding(4, 0, 4, 0);
            lblFileSizeValue.Name = "lblFileSizeValue";
            lblFileSizeValue.TabIndex = 12;
            lblFileSizeValue.Text = "- byte";
            // 
            // lblResolutionValue
            // 
            lblResolutionValue.AutoSize = false;
            lblResolutionValue.Dock = DockStyle.Fill;
            lblResolutionValue.Margin = new Padding(4, 0, 4, 0);
            lblResolutionValue.Name = "lblResolutionValue";
            lblResolutionValue.TabIndex = 11;
            lblResolutionValue.Text = "- x -";
            // 
            // lblImageFormat
            // 
            lblImageFormat.AutoSize = false;
            lblImageFormat.Dock = DockStyle.Fill;
            lblImageFormat.Margin = new Padding(4, 0, 4, 0);
            lblImageFormat.Name = "lblImageFormat";
            lblImageFormat.TabIndex = 10;
            lblImageFormat.Text = "-";
            // 
            // lblTotalImagesValue
            // 
            lblTotalImagesValue.AutoSize = false;
            lblTotalImagesValue.Dock = DockStyle.Fill;
            lblTotalImagesValue.Margin = new Padding(4, 0, 4, 0);
            lblTotalImagesValue.Name = "lblTotalImagesValue";
            lblTotalImagesValue.TabIndex = 8;
            lblTotalImagesValue.Text = "- 장";
            // 
            // btnLoadStart
            // 
            btnLoadStart.Dock = DockStyle.Fill;
            btnLoadStart.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnLoadStart.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnLoadStart.FlatStyle = FlatStyle.Flat;
            btnLoadStart.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnLoadStart.ForeColor = SystemColors.Highlight;
            btnLoadStart.Margin = new Padding(4, 2, 4, 2);
            btnLoadStart.Name = "btnLoadStart";
            btnLoadStart.TabIndex = 7;
            btnLoadStart.Text = "데이터 로드";
            btnLoadStart.UseVisualStyleBackColor = true;
            btnLoadStart.Click += btnLoadStart_Click;
            // 
            // lblFileSizeTitle
            // 
            lblFileSizeTitle.AutoSize = false;
            lblFileSizeTitle.Dock = DockStyle.Fill;
            lblFileSizeTitle.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFileSizeTitle.ForeColor = Color.RoyalBlue;
            lblFileSizeTitle.Margin = new Padding(4, 4, 4, 0);
            lblFileSizeTitle.Name = "lblFileSizeTitle";
            lblFileSizeTitle.TabIndex = 5;
            lblFileSizeTitle.Text = "파일 크기";
            // 
            // lblResolutionTitle
            // 
            lblResolutionTitle.AutoSize = false;
            lblResolutionTitle.Dock = DockStyle.Fill;
            lblResolutionTitle.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblResolutionTitle.ForeColor = Color.RoyalBlue;
            lblResolutionTitle.Margin = new Padding(4, 4, 4, 0);
            lblResolutionTitle.Name = "lblResolutionTitle";
            lblResolutionTitle.TabIndex = 4;
            lblResolutionTitle.Text = "해상도";
            // 
            // lblFormatTitle
            // 
            lblFormatTitle.AutoSize = false;
            lblFormatTitle.Dock = DockStyle.Fill;
            lblFormatTitle.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFormatTitle.ForeColor = Color.RoyalBlue;
            lblFormatTitle.Margin = new Padding(4, 4, 4, 0);
            lblFormatTitle.Name = "lblFormatTitle";
            lblFormatTitle.TabIndex = 3;
            lblFormatTitle.Text = "이미지 형식";
            // 
            // lblTotalImagesTitle
            // 
            lblTotalImagesTitle.AutoSize = false;
            lblTotalImagesTitle.Dock = DockStyle.Fill;
            lblTotalImagesTitle.Font = new Font("맑은 고딕", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTotalImagesTitle.ForeColor = Color.RoyalBlue;
            lblTotalImagesTitle.Margin = new Padding(4, 4, 4, 0);
            lblTotalImagesTitle.Name = "lblTotalImagesTitle";
            lblTotalImagesTitle.TabIndex = 1;
            lblTotalImagesTitle.Text = "전체 이미지";
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
            groupBox1.ResumeLayout(false);
            tlpInfoBar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel pnlImageView;
        private Panel pnlFrameList;
        private Button btnSelectFolder;
        private Label lblFileSizeValue;
        private Label lblResolutionValue;
        private Label lblImageFormat;
        private Label lblTotalImagesValue;
        private Button btnLoadStart;
        private Label lblFileSizeTitle;
        private Label lblResolutionTitle;
        private Label lblFormatTitle;
        private Label lblTotalImagesTitle;
        private GroupBox groupBox1;
        private TableLayoutPanel tlpInfoBar;
    }
}
