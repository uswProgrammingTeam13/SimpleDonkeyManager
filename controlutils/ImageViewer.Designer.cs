namespace SimpleDonkeyManager.controlutils
{
    partial class ImageViewer
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
            pnlLeftThumbnail = new Panel();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            button1 = new Button();
            pnlCenterMain = new Panel();
            pictureBox1 = new PictureBox();
            pnlRightThumbnail = new Panel();
            label2 = new Label();
            pictureBox3 = new PictureBox();
            button2 = new Button();
            pnlControlBar = new Panel();
            comboBox1 = new ComboBox();
            frameTimeline = new FrameTimeline();
            button3 = new Button();
            button4 = new Button();
            label5 = new Label();
            label6 = new Label();
            lstJSONSummary = new ListView();
            tableLayoutPanel3 = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            btnLargeView = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            pnlLeftThumbnail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            pnlCenterMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlRightThumbnail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            pnlControlBar.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(pnlControlBar, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 69.97519F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 30.0248146F));
            tableLayoutPanel1.Size = new Size(647, 403);
            tableLayoutPanel1.TabIndex = 20;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F));
            tableLayoutPanel2.Controls.Add(pnlLeftThumbnail, 0, 0);
            tableLayoutPanel2.Controls.Add(pnlCenterMain, 1, 0);
            tableLayoutPanel2.Controls.Add(pnlRightThumbnail, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(641, 276);
            tableLayoutPanel2.TabIndex = 15;
            // 
            // pnlLeftThumbnail
            // 
            pnlLeftThumbnail.Controls.Add(label1);
            pnlLeftThumbnail.Controls.Add(pictureBox2);
            pnlLeftThumbnail.Controls.Add(button1);
            pnlLeftThumbnail.Dock = DockStyle.Fill;
            pnlLeftThumbnail.Location = new Point(3, 3);
            pnlLeftThumbnail.Name = "pnlLeftThumbnail";
            pnlLeftThumbnail.Size = new Size(109, 270);
            pnlLeftThumbnail.TabIndex = 16;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label1.ForeColor = Color.RoyalBlue;
            label1.Location = new Point(16, 8);
            label1.Name = "label1";
            label1.Size = new Size(77, 15);
            label1.TabIndex = 6;
            label1.Text = "이전 프레임";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Location = new Point(0, 25);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(109, 195);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Bottom;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button1.ForeColor = Color.RoyalBlue;
            button1.Location = new Point(0, 243);
            button1.Name = "button1";
            button1.Size = new Size(109, 27);
            button1.TabIndex = 4;
            button1.Text = "<";
            button1.UseVisualStyleBackColor = true;
            // 
            // pnlCenterMain
            // 
            pnlCenterMain.Controls.Add(pictureBox1);
            pnlCenterMain.Dock = DockStyle.Fill;
            pnlCenterMain.Location = new Point(118, 3);
            pnlCenterMain.Name = "pnlCenterMain";
            pnlCenterMain.Size = new Size(405, 270);
            pnlCenterMain.TabIndex = 17;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(405, 270);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pnlRightThumbnail
            // 
            pnlRightThumbnail.Controls.Add(label2);
            pnlRightThumbnail.Controls.Add(pictureBox3);
            pnlRightThumbnail.Controls.Add(button2);
            pnlRightThumbnail.Dock = DockStyle.Fill;
            pnlRightThumbnail.Location = new Point(529, 3);
            pnlRightThumbnail.Name = "pnlRightThumbnail";
            pnlRightThumbnail.Size = new Size(109, 270);
            pnlRightThumbnail.TabIndex = 18;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label2.ForeColor = Color.RoyalBlue;
            label2.Location = new Point(17, 8);
            label2.Name = "label2";
            label2.Size = new Size(77, 15);
            label2.TabIndex = 7;
            label2.Text = "다음 프레임";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox3.BorderStyle = BorderStyle.FixedSingle;
            pictureBox3.Location = new Point(0, 25);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(109, 195);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 3;
            pictureBox3.TabStop = false;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Bottom;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button2.ForeColor = Color.RoyalBlue;
            button2.Location = new Point(0, 243);
            button2.Name = "button2";
            button2.Size = new Size(109, 27);
            button2.TabIndex = 5;
            button2.Text = ">";
            button2.UseVisualStyleBackColor = true;
            // 
            // pnlControlBar
            // 
            pnlControlBar.Controls.Add(comboBox1);
            pnlControlBar.Controls.Add(frameTimeline);
            pnlControlBar.Controls.Add(button3);
            pnlControlBar.Controls.Add(button4);
            pnlControlBar.Controls.Add(label5);
            pnlControlBar.Controls.Add(label6);
            pnlControlBar.Controls.Add(lstJSONSummary);
            pnlControlBar.Controls.Add(btnLargeView);
            pnlControlBar.Dock = DockStyle.Fill;
            pnlControlBar.ForeColor = Color.RoyalBlue;
            pnlControlBar.Location = new Point(3, 285);
            pnlControlBar.Name = "pnlControlBar";
            pnlControlBar.Size = new Size(641, 115);
            pnlControlBar.TabIndex = 19;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "0.25", "0.5", "1.0", "2.0", "4.0" });
            comboBox1.Location = new Point(150, 50);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(75, 27);
            comboBox1.TabIndex = 15;
            comboBox1.Text = "1.0";
            // 
            // frameTimeline
            // 
            frameTimeline.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            frameTimeline.BackColor = Color.FromArgb(248, 248, 248);
            frameTimeline.Location = new Point(10, 5);
            frameTimeline.Name = "frameTimeline";
            frameTimeline.Size = new Size(620, 40);
            frameTimeline.TabIndex = 1;
            // 
            // button3
            // 
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button3.ForeColor = Color.RoyalBlue;
            button3.Location = new Point(10, 50);
            button3.Name = "button3";
            button3.Size = new Size(64, 26);
            button3.TabIndex = 11;
            button3.Text = "재생";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button4.ForeColor = Color.RoyalBlue;
            button4.Location = new Point(80, 50);
            button4.Name = "button4";
            button4.Size = new Size(64, 26);
            button4.TabIndex = 12;
            button4.Text = "■ 정지";
            button4.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.Font = new Font("나눔고딕", 10F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label5.ForeColor = Color.RoyalBlue;
            label5.Location = new Point(10, 80);
            label5.Name = "label5";
            label5.Size = new Size(300, 22);
            label5.TabIndex = 10;
            label5.Text = "현재 : 123";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            label6.Font = new Font("나눔고딕", 10F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label6.ForeColor = Color.OrangeRed;
            label6.Location = new Point(10, 104);
            label6.Name = "label6";
            label6.Size = new Size(400, 22);
            label6.TabIndex = 17;
            label6.Text = "";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lstJSONSummary
            // 
            lstJSONSummary.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lstJSONSummary.BorderStyle = BorderStyle.FixedSingle;
            lstJSONSummary.FullRowSelect = true;
            lstJSONSummary.GridLines = true;
            lstJSONSummary.HeaderStyle = ColumnHeaderStyle.None;
            lstJSONSummary.HideSelection = true;
            lstJSONSummary.Location = new Point(426, 50);
            lstJSONSummary.Name = "lstJSONSummary";
            lstJSONSummary.Size = new Size(212, 62);
            lstJSONSummary.TabIndex = 14;
            lstJSONSummary.UseCompatibleStateImageBehavior = false;
            lstJSONSummary.View = View.Details;
            // 
            // btnLargeView
            // 
            btnLargeView.FlatStyle = FlatStyle.Flat;
            btnLargeView.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnLargeView.ForeColor = Color.RoyalBlue;
            btnLargeView.Location = new Point(230, 50);
            btnLargeView.Name = "btnLargeView";
            btnLargeView.Size = new Size(100, 26);
            btnLargeView.TabIndex = 16;
            btnLargeView.Text = "🔍 크게 보기";
            btnLargeView.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.Size = new Size(200, 100);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 100);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // ImageViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 248, 248);
            Controls.Add(tableLayoutPanel1);
            Name = "ImageViewer";
            Size = new Size(647, 403);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            pnlLeftThumbnail.ResumeLayout(false);
            pnlLeftThumbnail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            pnlCenterMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlRightThumbnail.ResumeLayout(false);
            pnlRightThumbnail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            pnlControlBar.ResumeLayout(false);
            pnlControlBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel pnlLeftThumbnail;
        private Panel pnlCenterMain;
        private Panel pnlRightThumbnail;
        private Panel pnlControlBar;
        private GroupBox groupBox1;
        private PictureBox pictureBox1;
        private Button button1;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private FrameTimeline frameTimeline;
        private Label label2;
        private Label label1;
        private Button button2;
        private Button button4;
        private Button button3;
        private Label label5;
        private Label label6;
        private ListView lstJSONSummary;
        private ComboBox comboBox1;
        private Button btnLargeView;
    }
}
