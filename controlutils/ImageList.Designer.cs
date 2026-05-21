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
            txtSearch = new TextBox();
            listBoxImages = new ListBox();
            btnPrev = new Button();
            btn1 = new Button();
            btn2 = new Button();
            btn3 = new Button();
            btnDots = new Button();
            btnLast = new Button();
            btnNext = new Button();
            btnSearch = new Button();
            panel1 = new Panel();
            groupBox1 = new GroupBox();
            checkBox1 = new CheckBox();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("나눔고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            txtSearch.Location = new Point(8, 31);
            txtSearch.Margin = new Padding(2, 1, 2, 1);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "이미지 검색...";
            txtSearch.Size = new Size(250, 26);
            txtSearch.TabIndex = 1;
            // 
            // listBoxImages
            // 
            listBoxImages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxImages.Font = new Font("나눔고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            listBoxImages.FormattingEnabled = true;
            listBoxImages.Location = new Point(8, 86);
            listBoxImages.Margin = new Padding(2, 1, 2, 1);
            listBoxImages.Name = "listBoxImages";
            listBoxImages.Size = new Size(250, 429);
            listBoxImages.TabIndex = 2;
            // 
            // btnPrev
            // 
            btnPrev.FlatStyle = FlatStyle.Flat;
            btnPrev.Location = new Point(2, 2);
            btnPrev.Margin = new Padding(2, 1, 2, 1);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(32, 28);
            btnPrev.TabIndex = 0;
            btnPrev.Text = "<";
            btnPrev.UseVisualStyleBackColor = true;
            // 
            // btn1
            // 
            btn1.FlatStyle = FlatStyle.Flat;
            btn1.Location = new Point(36, 2);
            btn1.Margin = new Padding(2, 1, 2, 1);
            btn1.Name = "btn1";
            btn1.Size = new Size(33, 28);
            btn1.TabIndex = 1;
            btn1.Text = "1";
            btn1.UseVisualStyleBackColor = true;
            // 
            // btn2
            // 
            btn2.FlatStyle = FlatStyle.Flat;
            btn2.Location = new Point(72, 2);
            btn2.Margin = new Padding(2, 1, 2, 1);
            btn2.Name = "btn2";
            btn2.Size = new Size(34, 28);
            btn2.TabIndex = 2;
            btn2.Text = "2";
            btn2.UseVisualStyleBackColor = true;
            // 
            // btn3
            // 
            btn3.FlatStyle = FlatStyle.Flat;
            btn3.Location = new Point(109, 2);
            btn3.Margin = new Padding(2, 1, 2, 1);
            btn3.Name = "btn3";
            btn3.Size = new Size(29, 28);
            btn3.TabIndex = 3;
            btn3.Text = "3";
            btn3.UseVisualStyleBackColor = true;
            // 
            // btnDots
            // 
            btnDots.Enabled = false;
            btnDots.FlatStyle = FlatStyle.Flat;
            btnDots.Location = new Point(141, 2);
            btnDots.Margin = new Padding(2, 1, 2, 1);
            btnDots.Name = "btnDots";
            btnDots.Size = new Size(30, 28);
            btnDots.TabIndex = 4;
            btnDots.Text = "...";
            btnDots.UseVisualStyleBackColor = true;
            // 
            // btnLast
            // 
            btnLast.FlatStyle = FlatStyle.Flat;
            btnLast.Location = new Point(174, 2);
            btnLast.Margin = new Padding(2, 1, 2, 1);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(35, 28);
            btnLast.TabIndex = 5;
            btnLast.Text = "999";
            btnLast.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Location = new Point(212, 2);
            btnNext.Margin = new Padding(2, 1, 2, 1);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(30, 28);
            btnNext.TabIndex = 6;
            btnNext.Text = ">";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("나눔고딕", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSearch.Location = new Point(222, 33);
            btnSearch.Margin = new Padding(2, 1, 2, 1);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(32, 20);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "q";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(btnPrev);
            panel1.Controls.Add(btn1);
            panel1.Controls.Add(btn2);
            panel1.Controls.Add(btn3);
            panel1.Controls.Add(btnDots);
            panel1.Controls.Add(btnNext);
            panel1.Controls.Add(btnLast);
            panel1.Location = new Point(7, 523);
            panel1.Margin = new Padding(2, 1, 2, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 35);
            panel1.TabIndex = 7;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.ControlLightLight;
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(panel1);
            groupBox1.Controls.Add(btnSearch);
            groupBox1.Controls.Add(listBoxImages);
            groupBox1.Controls.Add(txtSearch);
            groupBox1.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            groupBox1.ForeColor = Color.RoyalBlue;
            groupBox1.Location = new Point(3, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(267, 569);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "이미지 선택";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            checkBox1.Location = new Point(6, 63);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(127, 19);
            checkBox1.TabIndex = 8;
            checkBox1.Text = "이미지 전체 선택";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // ImageList
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = SystemColors.ControlLightLight;
            Controls.Add(groupBox1);
            Name = "ImageList";
            Size = new Size(273, 577);
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TextBox txtSearch;
        private ListBox listBoxImages;
        private Button btnPrev;
        private Button btn1;
        private Button btn2;
        private Button btn3;
        private Button btnDots;
        private Button btnLast;
        private Button btnNext;
        private Button btnSearch;
        private Panel panel1;
        private GroupBox groupBox1;
        private CheckBox checkBox1;
    }
}
