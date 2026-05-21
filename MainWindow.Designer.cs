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
            tableLayoutPanelButtons = new TableLayoutPanel();
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
            tableLayoutPanelButtons.SuspendLayout();
            pnlConditionView.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMainContent
            // 
            pnlMainContent.BackColor = SystemColors.ControlLightLight;
            pnlMainContent.BackgroundImageLayout = ImageLayout.Stretch;
            pnlMainContent.Dock = DockStyle.Fill;
            pnlMainContent.Location = new Point(0, 60);
            pnlMainContent.Margin = new Padding(0);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.Size = new Size(1184, 799);
            pnlMainContent.TabIndex = 0;
            // 
            // splLogHelp
            // 
            splLogHelp.BackColor = Color.FromArgb(248, 248, 248);
            splLogHelp.Dock = DockStyle.Bottom;
            splLogHelp.Location = new Point(0, 733);
            splLogHelp.Name = "splLogHelp";
            splLogHelp.Size = new Size(1184, 103);
            splLogHelp.TabIndex = 1;
            splLogHelp.SplitterDistance = 584;
            splLogHelp.SplitterWidth = 8;
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
            // 
            // pnlSplitPanel1
            // 
            pnlSplitPanel1.BackColor = Color.FromArgb(248, 248, 248);
            pnlSplitPanel1.Controls.Add(richTxtLog);
            pnlSplitPanel1.Controls.Add(LblLog);
            pnlSplitPanel1.Dock = DockStyle.Fill;
            pnlSplitPanel1.Location = new Point(0, 0);
            pnlSplitPanel1.Margin = new Padding(0);
            pnlSplitPanel1.MinimumSize = new Size(300, 150);
            pnlSplitPanel1.Name = "pnlSplitPanel1";
            pnlSplitPanel1.Size = new Size(584, 178);
            pnlSplitPanel1.TabIndex = 0;
            // 
            // richTxtLog
            // 
            richTxtLog.BackColor = SystemColors.ControlLightLight;
            richTxtLog.BorderStyle = BorderStyle.FixedSingle;
            richTxtLog.Dock = DockStyle.Fill;
            richTxtLog.Location = new Point(8, 29);
            richTxtLog.Margin = new Padding(0);
            richTxtLog.Name = "richTxtLog";
            richTxtLog.ReadOnly = true;
            richTxtLog.Size = new Size(568, 141);
            richTxtLog.TabIndex = 1;
            richTxtLog.Text = "";
            // 
            // LblLog
            // 
            LblLog.AutoSize = true;
            LblLog.Dock = DockStyle.Top;
            LblLog.Font = new Font("나눔고딕", 12F, FontStyle.Bold);
            LblLog.ForeColor = Color.RoyalBlue;
            LblLog.Location = new Point(8, 7);
            LblLog.Name = "LblLog";
            LblLog.Size = new Size(78, 19);
            LblLog.TabIndex = 0;
            LblLog.Text = "실행 로그";
            // 
            // pnlSplitPanel2
            // 
            pnlSplitPanel2.BackColor = Color.FromArgb(248, 248, 248);
            pnlSplitPanel2.Controls.Add(richTxtHelp);
            pnlSplitPanel2.Controls.Add(LblHelp);
            pnlSplitPanel2.Dock = DockStyle.Fill;
            pnlSplitPanel2.Location = new Point(0, 0);
            pnlSplitPanel2.Margin = new Padding(0);
            pnlSplitPanel2.MinimumSize = new Size(300, 150);
            pnlSplitPanel2.Name = "pnlSplitPanel2";
            pnlSplitPanel2.Size = new Size(584, 178);
            pnlSplitPanel2.TabIndex = 0;
            // 
            // richTxtHelp
            // 
            richTxtHelp.BorderStyle = BorderStyle.FixedSingle;
            richTxtHelp.Dock = DockStyle.Fill;
            richTxtHelp.Location = new Point(13, 29);
            richTxtHelp.Margin = new Padding(0);
            richTxtHelp.Name = "richTxtHelp";
            richTxtHelp.Size = new Size(568, 141);
            richTxtHelp.TabIndex = 2;
            richTxtHelp.Text = "";
            // 
            // LblHelp
            // 
            LblHelp.AutoSize = true;
            LblHelp.Dock = DockStyle.Top;
            LblHelp.Font = new Font("나눔고딕", 12F, FontStyle.Bold);
            LblHelp.ForeColor = Color.RoyalBlue;
            LblHelp.Location = new Point(13, 7);
            LblHelp.Name = "LblHelp";
            LblHelp.Size = new Size(72, 19);
            LblHelp.TabIndex = 0;
            LblHelp.Text = "도움말 ?";
            // 
            // pnlButtons (이제 사용 안 함 - tableLayoutPanelButtons로 대체)
            // 
            // tableLayoutPanelButtons
            // 
            tableLayoutPanelButtons.BackColor = Color.FromArgb(248, 248, 248);
            tableLayoutPanelButtons.ColumnCount = 4;
            tableLayoutPanelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelButtons.Controls.Add(btnDataLoadCon, 0, 0);
            tableLayoutPanelButtons.Controls.Add(btnDataFilterCon, 1, 0);
            tableLayoutPanelButtons.Controls.Add(btnTraningCon, 2, 0);
            tableLayoutPanelButtons.Controls.Add(btnResultCon, 3, 0);
            tableLayoutPanelButtons.Dock = DockStyle.Top;
            tableLayoutPanelButtons.Location = new Point(0, 0);
            tableLayoutPanelButtons.Margin = new Padding(0);
            tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            tableLayoutPanelButtons.RowCount = 1;
            tableLayoutPanelButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanelButtons.Size = new Size(1184, 60);
            tableLayoutPanelButtons.TabIndex = 2;
            // 
            // btnDataLoadCon
            // 
            btnDataLoadCon.Dock = DockStyle.Fill;
            btnDataLoadCon.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnDataLoadCon.ForeColor = Color.RoyalBlue;
            btnDataLoadCon.Margin = new Padding(5);
            btnDataLoadCon.Name = "btnDataLoadCon";
            btnDataLoadCon.TabIndex = 2;
            btnDataLoadCon.Text = "📂 데이터 불러오기";
            btnDataLoadCon.UseVisualStyleBackColor = true;
            // 
            // btnTraningCon
            // 
            btnTraningCon.Dock = DockStyle.Fill;
            btnTraningCon.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnTraningCon.Margin = new Padding(5);
            btnTraningCon.Name = "btnTraningCon";
            btnTraningCon.TabIndex = 5;
            btnTraningCon.Text = "▶ 학습 실행";
            btnTraningCon.UseVisualStyleBackColor = true;
            // 
            // btnResultCon
            // 
            btnResultCon.Dock = DockStyle.Fill;
            btnResultCon.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnResultCon.Margin = new Padding(5);
            btnResultCon.Name = "btnResultCon";
            btnResultCon.TabIndex = 4;
            btnResultCon.Text = "📈 학습 결과 확인";
            btnResultCon.UseVisualStyleBackColor = true;
            // 
            // btnDataFilterCon
            // 
            btnDataFilterCon.Dock = DockStyle.Fill;
            btnDataFilterCon.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnDataFilterCon.ForeColor = Color.RoyalBlue;
            btnDataFilterCon.Margin = new Padding(5);
            btnDataFilterCon.Name = "btnDataFilterCon";
            btnDataFilterCon.TabIndex = 1;
            btnDataFilterCon.Text = "🔍 데이터 필터링";
            btnDataFilterCon.UseVisualStyleBackColor = true;
            // 
            // pnlConditionView
            // 
            pnlConditionView.BackColor = Color.FromArgb(230, 230, 230);
            pnlConditionView.Controls.Add(lblProgramCon);
            pnlConditionView.Dock = DockStyle.Bottom;
            pnlConditionView.Location = new Point(0, 836);
            pnlConditionView.Margin = new Padding(0);
            pnlConditionView.Name = "pnlConditionView";
            pnlConditionView.Size = new Size(1184, 37);
            pnlConditionView.TabIndex = 3;
            // 
            // lblProgramCon
            // 
            lblProgramCon.AutoSize = true;
            lblProgramCon.Dock = DockStyle.Left;
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
            ClientSize = new Size(1184, 873);
            Controls.Add(pnlMainContent);
            Controls.Add(tableLayoutPanelButtons);
            Controls.Add(splLogHelp);
            Controls.Add(pnlConditionView);
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
            tableLayoutPanelButtons.ResumeLayout(false);
            pnlConditionView.ResumeLayout(false);
            pnlConditionView.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMainContent;
        private SplitContainer splLogHelp;
        private TableLayoutPanel tableLayoutPanelButtons;
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
