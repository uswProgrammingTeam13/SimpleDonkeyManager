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
            lblTitle.Location = new Point(15, 7);
            lblTitle.Margin = new Padding(2, 0, 2, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(90, 21);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "이미지목록";
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            txtSearch.Location = new Point(12, 39);
            txtSearch.Margin = new Padding(2, 1, 2, 1);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "이미지 검색...";
            txtSearch.Size = new Size(258, 29);
            txtSearch.TabIndex = 1;
            // 
            // listBoxImages
            // 
            listBoxImages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxImages.FormattingEnabled = true;
            listBoxImages.Location = new Point(12, 81);
            listBoxImages.Margin = new Padding(2, 1, 2, 1);
            listBoxImages.Name = "listBoxImages";
            listBoxImages.Size = new Size(255, 439);
            listBoxImages.TabIndex = 2;
            // 
            // btnPrev
            // 
            btnPrev.Location = new Point(8, 8);
            btnPrev.Margin = new Padding(2, 1, 2, 1);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(28, 26);
            btnPrev.TabIndex = 0;
            btnPrev.Text = "<";
            btnPrev.UseVisualStyleBackColor = true;
            // 
            // btn1
            // 
            btn1.Location = new Point(38, 6);
            btn1.Margin = new Padding(2, 1, 2, 1);
            btn1.Name = "btn1";
            btn1.Size = new Size(33, 28);
            btn1.TabIndex = 1;
            btn1.Text = "1";
            btn1.UseVisualStyleBackColor = true;
            // 
            // btn2
            // 
            btn2.Location = new Point(74, 6);
            btn2.Margin = new Padding(2, 1, 2, 1);
            btn2.Name = "btn2";
            btn2.Size = new Size(34, 28);
            btn2.TabIndex = 2;
            btn2.Text = "2";
            btn2.UseVisualStyleBackColor = true;
            // 
            // btn3
            // 
            btn3.Location = new Point(111, 6);
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
            btnDots.Location = new Point(143, 6);
            btnDots.Margin = new Padding(2, 1, 2, 1);
            btnDots.Name = "btnDots";
            btnDots.Size = new Size(30, 28);
            btnDots.TabIndex = 4;
            btnDots.Text = "...";
            btnDots.UseVisualStyleBackColor = true;
            // 
            // btnLast
            // 
            btnLast.Location = new Point(176, 6);
            btnLast.Margin = new Padding(2, 1, 2, 1);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(35, 28);
            btnLast.TabIndex = 5;
            btnLast.Text = "999";
            btnLast.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(214, 6);
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
            btnSearch.Location = new Point(233, 43);
            btnSearch.Margin = new Padding(2, 1, 2, 1);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(32, 20);
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
            panel1.Location = new Point(12, 532);
            panel1.Margin = new Padding(2, 1, 2, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 35);
            panel1.TabIndex = 7;
            // 
            // ImageList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = SystemColors.Control;
            Controls.Add(panel1);
            Controls.Add(btnSearch);
            Controls.Add(listBoxImages);
            Controls.Add(txtSearch);
            Controls.Add(lblTitle);
            Name = "ImageList";
            Size = new Size(279, 579);
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
