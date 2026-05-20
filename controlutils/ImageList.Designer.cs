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
            lblTitle = new Label();
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
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lblTitle.Location = new Point(30, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(180, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "이미지목록";
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            txtSearch.Location = new Point(24, 83);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "이미지 검색...";
            txtSearch.Size = new Size(510, 50);
            txtSearch.TabIndex = 1;
            // 
            // listBoxImages
            // 
            listBoxImages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxImages.FormattingEnabled = true;
            listBoxImages.Location = new Point(24, 173);
            listBoxImages.Name = "listBoxImages";
            listBoxImages.Size = new Size(510, 772);
            listBoxImages.TabIndex = 2;
            // 
            // btnPrev
            // 
            btnPrev.Location = new Point(15, 16);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(55, 56);
            btnPrev.TabIndex = 0;
            btnPrev.Text = "<";
            btnPrev.UseVisualStyleBackColor = true;
            // 
            // btn1
            // 
            btn1.Location = new Point(76, 13);
            btn1.Name = "btn1";
            btn1.Size = new Size(66, 59);
            btn1.TabIndex = 1;
            btn1.Text = "1";
            btn1.UseVisualStyleBackColor = true;
            // 
            // btn2
            // 
            btn2.Location = new Point(148, 13);
            btn2.Name = "btn2";
            btn2.Size = new Size(68, 59);
            btn2.TabIndex = 2;
            btn2.Text = "2";
            btn2.UseVisualStyleBackColor = true;
            // 
            // btn3
            // 
            btn3.Location = new Point(222, 13);
            btn3.Name = "btn3";
            btn3.Size = new Size(58, 59);
            btn3.TabIndex = 3;
            btn3.Text = "3";
            btn3.UseVisualStyleBackColor = true;
            // 
            // btnDots
            // 
            btnDots.Enabled = false;
            btnDots.Location = new Point(286, 13);
            btnDots.Name = "btnDots";
            btnDots.Size = new Size(60, 59);
            btnDots.TabIndex = 4;
            btnDots.Text = "...";
            btnDots.UseVisualStyleBackColor = true;
            // 
            // btnLast
            // 
            btnLast.Location = new Point(352, 13);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(70, 59);
            btnLast.TabIndex = 5;
            btnLast.Text = "999";
            btnLast.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(428, 13);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(59, 59);
            btnNext.TabIndex = 6;
            btnNext.Text = ">";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnSearch.Location = new Point(459, 91);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(64, 42);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "⌕";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.White;
            panel1.Controls.Add(btnPrev);
            panel1.Controls.Add(btn1);
            panel1.Controls.Add(btn2);
            panel1.Controls.Add(btn3);
            panel1.Controls.Add(btnDots);
            panel1.Controls.Add(btnNext);
            panel1.Controls.Add(btnLast);
            panel1.Location = new Point(24, 983);
            panel1.Name = "panel1";
            panel1.Size = new Size(503, 75);
            panel1.TabIndex = 7;
            // 
            // ImageList
            // 
            AutoScaleDimensions = new SizeF(14F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = SystemColors.Control;
            Controls.Add(panel1);
            Controls.Add(btnSearch);
            Controls.Add(listBoxImages);
            Controls.Add(txtSearch);
            Controls.Add(lblTitle);
            Margin = new Padding(6);
            Name = "ImageList";
            Size = new Size(558, 1225);
            Load += ImageList_Load;
            Layout += ImageList_Layout;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
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
    }
}
