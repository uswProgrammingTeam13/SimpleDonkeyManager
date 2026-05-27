namespace SimpleDonkeyManager.controlutils
{
    partial class ImageList
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
            listBoxImages = new ListBox();
            groupBox1 = new GroupBox();
            btnFrameSearch = new Button();
            txtFrameSearch = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // listBoxImages
            // 
            listBoxImages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxImages.Font = new Font("나눔고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            listBoxImages.FormattingEnabled = true;
            listBoxImages.Location = new Point(8, 56);
            listBoxImages.Margin = new Padding(2, 1, 2, 1);
            listBoxImages.Name = "listBoxImages";
            listBoxImages.Size = new Size(250, 497);
            listBoxImages.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.BackColor = SystemColors.ControlLightLight;
            groupBox1.Controls.Add(btnFrameSearch);
            groupBox1.Controls.Add(txtFrameSearch);
            groupBox1.Controls.Add(listBoxImages);
            groupBox1.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            groupBox1.ForeColor = Color.RoyalBlue;
            groupBox1.Location = new Point(3, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(267, 569);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "이미지 선택";
            // 
            // btnFrameSearch
            // 
            btnFrameSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFrameSearch.FlatStyle = FlatStyle.Flat;
            btnFrameSearch.Font = new Font("나눔고딕", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnFrameSearch.ForeColor = Color.RoyalBlue;
            btnFrameSearch.Location = new Point(222, 26);
            btnFrameSearch.Margin = new Padding(2, 1, 2, 1);
            btnFrameSearch.Name = "btnFrameSearch";
            btnFrameSearch.Size = new Size(39, 25);
            btnFrameSearch.TabIndex = 1;
            btnFrameSearch.Text = "검색";
            btnFrameSearch.UseVisualStyleBackColor = true;
            // 
            // txtFrameSearch
            // 
            txtFrameSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFrameSearch.BorderStyle = BorderStyle.FixedSingle;
            txtFrameSearch.Font = new Font("나눔고딕", 11F, FontStyle.Regular, GraphicsUnit.Point, 129);
            txtFrameSearch.Location = new Point(8, 26);
            txtFrameSearch.Margin = new Padding(2, 1, 2, 1);
            txtFrameSearch.Name = "txtFrameSearch";
            txtFrameSearch.PlaceholderText = "프레임 순번 입력";
            txtFrameSearch.Size = new Size(210, 24);
            txtFrameSearch.TabIndex = 0;
            // 
            // ImageList
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            BackColor = SystemColors.ControlLightLight;
            Controls.Add(groupBox1);
            Name = "ImageList";
            Size = new Size(273, 577);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ListBox listBoxImages;
        private GroupBox groupBox1;
        private TextBox txtFrameSearch;
        private Button btnFrameSearch;
    }
}

