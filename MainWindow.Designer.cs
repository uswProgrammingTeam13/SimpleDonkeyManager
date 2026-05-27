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
            tabControlHelp = new TabControl();
            tabPageInitial = new TabPage();
            richTxtHelpInitial = new RichTextBox();
            tabPageDataLoad = new TabPage();
            richTxtHelpDataLoad = new RichTextBox();
            tabPageDataFilter = new TabPage();
            richTxtHelpDataFilter = new RichTextBox();
            tabPageTraining = new TabPage();
            richTxtHelpTraining = new RichTextBox();
            tabPageResult = new TabPage();
            richTxtHelpResult = new RichTextBox();
            tableLayoutPanelButtons = new TableLayoutPanel();
            btnDataLoadCon = new Button();
            btnDataFilterCon = new Button();
            btnTraningCon = new Button();
            btnResultCon = new Button();
            pnlConditionView = new Panel();
            lblProgramCon = new Label();
            ((System.ComponentModel.ISupportInitialize)splLogHelp).BeginInit();
            splLogHelp.Panel1.SuspendLayout();
            splLogHelp.Panel2.SuspendLayout();
            splLogHelp.SuspendLayout();
            pnlSplitPanel1.SuspendLayout();
            pnlSplitPanel2.SuspendLayout();
            tabControlHelp.SuspendLayout();
            tabPageInitial.SuspendLayout();
            tabPageDataLoad.SuspendLayout();
            tabPageDataFilter.SuspendLayout();
            tabPageTraining.SuspendLayout();
            tabPageResult.SuspendLayout();
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
            pnlMainContent.Size = new Size(1184, 658);
            pnlMainContent.TabIndex = 0;
            // 
            // splLogHelp
            // 
            splLogHelp.BackColor = Color.FromArgb(248, 248, 248);
            splLogHelp.Dock = DockStyle.Bottom;
            splLogHelp.Location = new Point(0, 718);
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
            splLogHelp.Size = new Size(1184, 179);
            splLogHelp.SplitterDistance = 584;
            splLogHelp.SplitterWidth = 8;
            splLogHelp.TabIndex = 1;
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
            pnlSplitPanel1.Size = new Size(584, 179);
            pnlSplitPanel1.TabIndex = 0;
            // 
            // richTxtLog
            // 
            richTxtLog.BackColor = SystemColors.ControlLightLight;
            richTxtLog.BorderStyle = BorderStyle.FixedSingle;
            richTxtLog.Dock = DockStyle.Fill;
            richTxtLog.Location = new Point(0, 19);
            richTxtLog.Margin = new Padding(0);
            richTxtLog.Name = "richTxtLog";
            richTxtLog.ReadOnly = true;
            richTxtLog.Size = new Size(584, 160);
            richTxtLog.TabIndex = 1;
            richTxtLog.Text = "";
            // 
            // LblLog
            // 
            LblLog.AutoSize = true;
            LblLog.Dock = DockStyle.Top;
            LblLog.Font = new Font("나눔고딕", 12F, FontStyle.Bold);
            LblLog.ForeColor = Color.RoyalBlue;
            LblLog.Location = new Point(0, 0);
            LblLog.Name = "LblLog";
            LblLog.Size = new Size(78, 19);
            LblLog.TabIndex = 0;
            LblLog.Text = "실행 로그";
            // 
            // pnlSplitPanel2
            // 
            pnlSplitPanel2.BackColor = Color.FromArgb(248, 248, 248);
            pnlSplitPanel2.Controls.Add(tabControlHelp);
            pnlSplitPanel2.Dock = DockStyle.Fill;
            pnlSplitPanel2.Location = new Point(0, 0);
            pnlSplitPanel2.Margin = new Padding(0);
            pnlSplitPanel2.MinimumSize = new Size(300, 150);
            pnlSplitPanel2.Name = "pnlSplitPanel2";
            pnlSplitPanel2.Size = new Size(592, 179);
            pnlSplitPanel2.TabIndex = 0;
            // 
            // tabControlHelp
            // 
            tabControlHelp.Controls.Add(tabPageInitial);
            tabControlHelp.Controls.Add(tabPageDataLoad);
            tabControlHelp.Controls.Add(tabPageDataFilter);
            tabControlHelp.Controls.Add(tabPageTraining);
            tabControlHelp.Controls.Add(tabPageResult);
            tabControlHelp.Dock = DockStyle.Fill;
            tabControlHelp.Location = new Point(0, 0);
            tabControlHelp.Name = "tabControlHelp";
            tabControlHelp.SelectedIndex = 0;
            tabControlHelp.Size = new Size(592, 179);
            tabControlHelp.TabIndex = 0;
            // 
            // tabPageInitial
            // 
            tabPageInitial.BackColor = SystemColors.ControlLightLight;
            tabPageInitial.Controls.Add(richTxtHelpInitial);
            tabPageInitial.Location = new Point(4, 24);
            tabPageInitial.Name = "tabPageInitial";
            tabPageInitial.Padding = new Padding(3);
            tabPageInitial.Size = new Size(584, 151);
            tabPageInitial.TabIndex = 0;
            tabPageInitial.Text = "시작 가이드";
            // 
            // richTxtHelpInitial
            // 
            richTxtHelpInitial.BorderStyle = BorderStyle.FixedSingle;
            richTxtHelpInitial.Dock = DockStyle.Fill;
            richTxtHelpInitial.Location = new Point(3, 3);
            richTxtHelpInitial.Margin = new Padding(0);
            richTxtHelpInitial.Name = "richTxtHelpInitial";
            richTxtHelpInitial.ReadOnly = true;
            richTxtHelpInitial.Size = new Size(578, 145);
            richTxtHelpInitial.TabIndex = 0;
            richTxtHelpInitial.Text = "";
            // 
            // tabPageDataLoad
            // 
            tabPageDataLoad.BackColor = SystemColors.ControlLightLight;
            tabPageDataLoad.Controls.Add(richTxtHelpDataLoad);
            tabPageDataLoad.Location = new Point(4, 24);
            tabPageDataLoad.Name = "tabPageDataLoad";
            tabPageDataLoad.Padding = new Padding(3);
            tabPageDataLoad.Size = new Size(584, 151);
            tabPageDataLoad.TabIndex = 1;
            tabPageDataLoad.Text = "데이터 불러오기";
            // 
            // richTxtHelpDataLoad
            // 
            richTxtHelpDataLoad.BorderStyle = BorderStyle.FixedSingle;
            richTxtHelpDataLoad.Dock = DockStyle.Fill;
            richTxtHelpDataLoad.Location = new Point(3, 3);
            richTxtHelpDataLoad.Margin = new Padding(0);
            richTxtHelpDataLoad.Name = "richTxtHelpDataLoad";
            richTxtHelpDataLoad.ReadOnly = true;
            richTxtHelpDataLoad.Size = new Size(578, 145);
            richTxtHelpDataLoad.TabIndex = 0;
            richTxtHelpDataLoad.Text = "";
            // 
            // tabPageDataFilter
            // 
            tabPageDataFilter.BackColor = SystemColors.ControlLightLight;
            tabPageDataFilter.Controls.Add(richTxtHelpDataFilter);
            tabPageDataFilter.Location = new Point(4, 24);
            tabPageDataFilter.Name = "tabPageDataFilter";
            tabPageDataFilter.Padding = new Padding(3);
            tabPageDataFilter.Size = new Size(584, 151);
            tabPageDataFilter.TabIndex = 2;
            tabPageDataFilter.Text = "데이터 필터링";
            // 
            // richTxtHelpDataFilter
            // 
            richTxtHelpDataFilter.BorderStyle = BorderStyle.FixedSingle;
            richTxtHelpDataFilter.Dock = DockStyle.Fill;
            richTxtHelpDataFilter.Location = new Point(3, 3);
            richTxtHelpDataFilter.Margin = new Padding(0);
            richTxtHelpDataFilter.Name = "richTxtHelpDataFilter";
            richTxtHelpDataFilter.ReadOnly = true;
            richTxtHelpDataFilter.Size = new Size(578, 145);
            richTxtHelpDataFilter.TabIndex = 0;
            richTxtHelpDataFilter.Text = "";
            // 
            // tabPageTraining
            // 
            tabPageTraining.BackColor = SystemColors.ControlLightLight;
            tabPageTraining.Controls.Add(richTxtHelpTraining);
            tabPageTraining.Location = new Point(4, 24);
            tabPageTraining.Name = "tabPageTraining";
            tabPageTraining.Padding = new Padding(3);
            tabPageTraining.Size = new Size(584, 151);
            tabPageTraining.TabIndex = 3;
            tabPageTraining.Text = "학습 실행";
            // 
            // richTxtHelpTraining
            // 
            richTxtHelpTraining.BorderStyle = BorderStyle.FixedSingle;
            richTxtHelpTraining.Dock = DockStyle.Fill;
            richTxtHelpTraining.Location = new Point(3, 3);
            richTxtHelpTraining.Margin = new Padding(0);
            richTxtHelpTraining.Name = "richTxtHelpTraining";
            richTxtHelpTraining.ReadOnly = true;
            richTxtHelpTraining.Size = new Size(578, 145);
            richTxtHelpTraining.TabIndex = 0;
            richTxtHelpTraining.Text = "";
            // 
            // tabPageResult
            // 
            tabPageResult.BackColor = SystemColors.ControlLightLight;
            tabPageResult.Controls.Add(richTxtHelpResult);
            tabPageResult.Location = new Point(4, 24);
            tabPageResult.Name = "tabPageResult";
            tabPageResult.Padding = new Padding(3);
            tabPageResult.Size = new Size(584, 151);
            tabPageResult.TabIndex = 4;
            tabPageResult.Text = "학습 결과 확인";
            // 
            // richTxtHelpResult
            // 
            richTxtHelpResult.BorderStyle = BorderStyle.FixedSingle;
            richTxtHelpResult.Dock = DockStyle.Fill;
            richTxtHelpResult.Location = new Point(3, 3);
            richTxtHelpResult.Margin = new Padding(0);
            richTxtHelpResult.Name = "richTxtHelpResult";
            richTxtHelpResult.ReadOnly = true;
            richTxtHelpResult.Size = new Size(578, 145);
            richTxtHelpResult.TabIndex = 0;
            richTxtHelpResult.Text = "";
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
            btnDataLoadCon.Location = new Point(5, 5);
            btnDataLoadCon.Margin = new Padding(5);
            btnDataLoadCon.Name = "btnDataLoadCon";
            btnDataLoadCon.Size = new Size(286, 50);
            btnDataLoadCon.TabIndex = 2;
            btnDataLoadCon.Text = "① 데이터 불러오기";
            btnDataLoadCon.UseVisualStyleBackColor = true;
            // 
            // btnDataFilterCon
            // 
            btnDataFilterCon.Dock = DockStyle.Fill;
            btnDataFilterCon.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnDataFilterCon.ForeColor = Color.RoyalBlue;
            btnDataFilterCon.Location = new Point(301, 5);
            btnDataFilterCon.Margin = new Padding(5);
            btnDataFilterCon.Name = "btnDataFilterCon";
            btnDataFilterCon.Size = new Size(286, 50);
            btnDataFilterCon.TabIndex = 1;
            btnDataFilterCon.Text = "② 데이터 필터링";
            btnDataFilterCon.UseVisualStyleBackColor = true;
            // 
            // btnTraningCon
            // 
            btnTraningCon.Dock = DockStyle.Fill;
            btnTraningCon.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnTraningCon.ForeColor = Color.RoyalBlue;
            btnTraningCon.Location = new Point(597, 5);
            btnTraningCon.Margin = new Padding(5);
            btnTraningCon.Name = "btnTraningCon";
            btnTraningCon.Size = new Size(286, 50);
            btnTraningCon.TabIndex = 5;
            btnTraningCon.Text = "③ 학습 실행";
            btnTraningCon.UseVisualStyleBackColor = true;
            // 
            // btnResultCon
            // 
            btnResultCon.Dock = DockStyle.Fill;
            btnResultCon.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnResultCon.ForeColor = Color.RoyalBlue;
            btnResultCon.Location = new Point(893, 5);
            btnResultCon.Margin = new Padding(5);
            btnResultCon.Name = "btnResultCon";
            btnResultCon.Size = new Size(286, 50);
            btnResultCon.TabIndex = 4;
            btnResultCon.Text = "④ 학습 결과 확인";
            btnResultCon.UseVisualStyleBackColor = true;
            // 
            // pnlConditionView
            // 
            pnlConditionView.BackColor = Color.FromArgb(230, 230, 230);
            pnlConditionView.Controls.Add(lblProgramCon);
            pnlConditionView.Dock = DockStyle.Bottom;
            pnlConditionView.Location = new Point(0, 897);
            pnlConditionView.Margin = new Padding(0);
            pnlConditionView.Name = "pnlConditionView";
            pnlConditionView.Size = new Size(1184, 20);
            pnlConditionView.TabIndex = 3;
            // 
            // lblProgramCon
            // 
            lblProgramCon.AutoSize = true;
            lblProgramCon.Dock = DockStyle.Bottom;
            lblProgramCon.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblProgramCon.ForeColor = SystemColors.ControlDarkDark;
            lblProgramCon.Location = new Point(0, 1);
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
            ClientSize = new Size(1184, 917);
            MinimumSize = new Size(900, 700);
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
            tabControlHelp.ResumeLayout(false);
            tabPageInitial.ResumeLayout(false);
            tabPageDataLoad.ResumeLayout(false);
            tabPageDataFilter.ResumeLayout(false);
            tabPageTraining.ResumeLayout(false);
            tabPageResult.ResumeLayout(false);
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
        private RichTextBox richTxtLog;
        private Label lblProgramCon;
        private TabControl tabControlHelp;
        private TabPage tabPageInitial;
        private TabPage tabPageDataLoad;
        private TabPage tabPageDataFilter;
        private TabPage tabPageTraining;
        private TabPage tabPageResult;
        private RichTextBox richTxtHelpInitial;
        private RichTextBox richTxtHelpDataLoad;
        private RichTextBox richTxtHelpDataFilter;
        private RichTextBox richTxtHelpTraining;
        private RichTextBox richTxtHelpResult;
    }
}
