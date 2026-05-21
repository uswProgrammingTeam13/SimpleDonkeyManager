namespace SimpleDonkeyManager
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlMainContent = new Panel();
            splLogHelp = new SplitContainer();
            pnlSplitPanel1 = new Panel();
            richTxtLog = new RichTextBox();
            LblLog = new Label();
            pnlSplitPanel2 = new Panel();
            richTxtHelp = new RichTextBox();
            LblHelp = new Label();
            pnlButtons = new Panel();
            btnDataLoadCon = new Button();
            btnTraningCon = new Button();
            btnResultCon = new Button();
            btnDataFilterCon = new Button();
            pnlConditionView = new Panel();
            lblProgramCon = new Label();
            ((System.ComponentModel.ISupportInitialize)splLogHelp).BeginInit();
            splLogHelp.Panel1.SuspendLayout();
            splLogHelp.Panel2.SuspendLayout();
            splLogHelp.SuspendLayout();
            pnlSplitPanel1.SuspendLayout();
            pnlSplitPanel2.SuspendLayout();
            pnlButtons.SuspendLayout();
            pnlConditionView.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMainContent
            // 
            pnlMainContent.BackColor = SystemColors.ControlLightLight;
            pnlMainContent.BackgroundImageLayout = ImageLayout.Stretch;
            pnlMainContent.Location = new Point(4, 69);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.Size = new Size(1176, 600);
            pnlMainContent.TabIndex = 0;
            // 
            // splLogHelp
            // 
            splLogHelp.BackColor = Color.FromArgb(248, 248, 248);
            splLogHelp.Location = new Point(4, 675);
            splLogHelp.Name = "splLogHelp";
            // 
            // splLogHelp.Panel1
            // 
            splLogHelp.Panel1.BackColor = SystemColors.ControlLight;
            splLogHelp.Panel1.Controls.Add(pnlSplitPanel1);
            splLogHelp.Panel1MinSize = 350;
            // 
            // splLogHelp.Panel2
            // 
            splLogHelp.Panel2.BackColor = SystemColors.ControlLight;
            splLogHelp.Panel2.Controls.Add(pnlSplitPanel2);
            splLogHelp.Panel2MinSize = 350;
            splLogHelp.Size = new Size(1176, 178);
            splLogHelp.SplitterDistance = 584;
            splLogHelp.SplitterWidth = 8;
            splLogHelp.TabIndex = 1;
            // 
            // pnlSplitPanel1
            // 
            pnlSplitPanel1.BackColor = Color.FromArgb(242, 242, 242);
            pnlSplitPanel1.Controls.Add(richTxtLog);
            pnlSplitPanel1.Controls.Add(LblLog);
            pnlSplitPanel1.Dock = DockStyle.Fill;
            pnlSplitPanel1.Location = new Point(0, 0);
            pnlSplitPanel1.MinimumSize = new Size(300, 150);
            pnlSplitPanel1.Name = "pnlSplitPanel1";
            pnlSplitPanel1.Size = new Size(584, 178);
            pnlSplitPanel1.TabIndex = 0;
            // 
            // richTxtLog
            // 
            richTxtLog.BorderStyle = BorderStyle.FixedSingle;
            richTxtLog.Location = new Point(8, 29);
            richTxtLog.Name = "richTxtLog";
            richTxtLog.Size = new Size(560, 137);
            richTxtLog.TabIndex = 1;
            richTxtLog.Text = "";
            // 
            // LblLog
            // 
            LblLog.AutoSize = true;
            LblLog.Font = new Font("나눔고딕", 12F, FontStyle.Bold);
            LblLog.ForeColor = SystemColors.Highlight;
            LblLog.Location = new Point(8, 7);
            LblLog.Name = "LblLog";
            LblLog.Size = new Size(78, 19);
            LblLog.TabIndex = 0;
            LblLog.Text = "실행 로그";
            // 
            // pnlSplitPanel2
            // 
            pnlSplitPanel2.BackColor = Color.FromArgb(242, 242, 242);
            pnlSplitPanel2.Controls.Add(richTxtHelp);
            pnlSplitPanel2.Controls.Add(LblHelp);
            pnlSplitPanel2.Dock = DockStyle.Fill;
            pnlSplitPanel2.Location = new Point(0, 0);
            pnlSplitPanel2.MinimumSize = new Size(300, 150);
            pnlSplitPanel2.Name = "pnlSplitPanel2";
            pnlSplitPanel2.Size = new Size(584, 178);
            pnlSplitPanel2.TabIndex = 0;
            // 
            // richTxtHelp
            // 
            richTxtHelp.BorderStyle = BorderStyle.FixedSingle;
            richTxtHelp.Location = new Point(13, 29);
            richTxtHelp.Name = "richTxtHelp";
            richTxtHelp.Size = new Size(560, 137);
            richTxtHelp.TabIndex = 2;
            richTxtHelp.Text = "";
            // 
            // LblHelp
            // 
            LblHelp.AutoSize = true;
            LblHelp.Font = new Font("나눔고딕", 12F, FontStyle.Bold);
            LblHelp.ForeColor = SystemColors.Highlight;
            LblHelp.Location = new Point(13, 7);
            LblHelp.Name = "LblHelp";
            LblHelp.Size = new Size(72, 19);
            LblHelp.TabIndex = 0;
            LblHelp.Text = "도움말 ?";
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.FromArgb(248, 248, 248);
            pnlButtons.Controls.Add(btnDataLoadCon);
            pnlButtons.Controls.Add(btnTraningCon);
            pnlButtons.Controls.Add(btnResultCon);
            pnlButtons.Controls.Add(btnDataFilterCon);
            pnlButtons.ForeColor = Color.RoyalBlue;
            pnlButtons.Location = new Point(0, 3);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(1184, 60);
            pnlButtons.TabIndex = 2;
            // 
            // btnDataLoadCon
            // 
            btnDataLoadCon.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnDataLoadCon.ForeColor = Color.RoyalBlue;
            btnDataLoadCon.Location = new Point(105, 8);
            btnDataLoadCon.Margin = new Padding(2);
            btnDataLoadCon.Name = "btnDataLoadCon";
            btnDataLoadCon.Size = new Size(200, 43);
            btnDataLoadCon.TabIndex = 2;
            btnDataLoadCon.Text = "📂 데이터 불러오기";
            btnDataLoadCon.UseVisualStyleBackColor = true;
            // 
            // btnTraningCon
            // 
            btnTraningCon.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnTraningCon.Location = new Point(605, 8);
            btnTraningCon.Margin = new Padding(2);
            btnTraningCon.Name = "btnTraningCon";
            btnTraningCon.Size = new Size(200, 43);
            btnTraningCon.TabIndex = 5;
            btnTraningCon.Text = "▶ 학습 실행";
            btnTraningCon.UseVisualStyleBackColor = true;
            // 
            // btnResultCon
            // 
            btnResultCon.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnResultCon.Location = new Point(855, 8);
            btnResultCon.Margin = new Padding(2);
            btnResultCon.Name = "btnResultCon";
            btnResultCon.Size = new Size(200, 43);
            btnResultCon.TabIndex = 4;
            btnResultCon.Text = "📈 학습 결과 확인";
            btnResultCon.UseVisualStyleBackColor = true;
            // 
            // btnDataFilterCon
            // 
            btnDataFilterCon.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnDataFilterCon.ForeColor = Color.RoyalBlue;
            btnDataFilterCon.Location = new Point(355, 8);
            btnDataFilterCon.Margin = new Padding(2);
            btnDataFilterCon.Name = "btnDataFilterCon";
            btnDataFilterCon.Size = new Size(200, 43);
            btnDataFilterCon.TabIndex = 1;
            btnDataFilterCon.Text = "🔍 데이터 필터링";
            btnDataFilterCon.UseVisualStyleBackColor = true;
            // 
            // pnlConditionView
            // 
            pnlConditionView.BackColor = Color.FromArgb(230, 230, 230);
            pnlConditionView.Controls.Add(lblProgramCon);
            pnlConditionView.Location = new Point(0, 859);
            pnlConditionView.Name = "pnlConditionView";
            pnlConditionView.Size = new Size(1184, 37);
            pnlConditionView.TabIndex = 3;
            // 
            // lblProgramCon
            // 
            lblProgramCon.AutoSize = true;
            lblProgramCon.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblProgramCon.ForeColor = SystemColors.ControlDarkDark;
            lblProgramCon.Location = new Point(12, 9);
            lblProgramCon.Name = "lblProgramCon";
            lblProgramCon.Size = new Size(536, 19);
            lblProgramCon.TabIndex = 0;
            lblProgramCon.Text = "📂 현재 폴더 : -    |    프레임 수 : - / -    |    상태 : 데이터 폴더 로드 대기";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1184, 896);
            Controls.Add(pnlConditionView);
            Controls.Add(pnlButtons);
            Controls.Add(splLogHelp);
            Controls.Add(pnlMainContent);
            Name = "MainWindow";
            Text = "SimpleDonkeyManager";
            splLogHelp.Panel1.ResumeLayout(false);
            splLogHelp.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splLogHelp).EndInit();
            splLogHelp.ResumeLayout(false);
            pnlSplitPanel1.ResumeLayout(false);
            pnlSplitPanel1.PerformLayout();
            pnlSplitPanel2.ResumeLayout(false);
            pnlSplitPanel2.PerformLayout();
            pnlButtons.ResumeLayout(false);
            pnlConditionView.ResumeLayout(false);
            pnlConditionView.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMainContent;
        private SplitContainer splLogHelp;
        private Panel pnlButtons;
        private Panel pnlConditionView;
        private Panel pnlSplitPanel1;
        private Panel pnlSplitPanel2;
        private Button btnResultCon;
        private Button btnDataLoadCon;
        private Button btnDataFilterCon;
        private Button btnTraningCon;
        private Label LblLog;
        private Label LblHelp;
        private RichTextBox richTxtLog;
        private RichTextBox richTxtHelp;
        private Label lblProgramCon;
    }
}
