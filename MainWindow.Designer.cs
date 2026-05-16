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
            pnlSplitPanel2 = new Panel();
            pnlButtons = new Panel();
            btnDataLoadCon = new Button();
            btnTraningCon = new Button();
            btnResultCon = new Button();
            btnDataFilterCon = new Button();
            pnlConditionView = new Panel();
            checkedListBox1 = new CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)splLogHelp).BeginInit();
            splLogHelp.Panel1.SuspendLayout();
            splLogHelp.Panel2.SuspendLayout();
            splLogHelp.SuspendLayout();
            pnlSplitPanel1.SuspendLayout();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMainContent
            // 
            pnlMainContent.BackColor = SystemColors.ControlLight;
            pnlMainContent.Location = new Point(5, 92);
            pnlMainContent.Margin = new Padding(4);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.Size = new Size(1512, 800);
            pnlMainContent.TabIndex = 0;
            // 
            // splLogHelp
            // 
            splLogHelp.Location = new Point(5, 900);
            splLogHelp.Margin = new Padding(4);
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
            splLogHelp.Size = new Size(1512, 237);
            splLogHelp.SplitterDistance = 752;
            splLogHelp.SplitterWidth = 10;
            splLogHelp.TabIndex = 1;
            // 
            // pnlSplitPanel1
            // 
            pnlSplitPanel1.Controls.Add(checkedListBox1);
            pnlSplitPanel1.Dock = DockStyle.Fill;
            pnlSplitPanel1.Location = new Point(0, 0);
            pnlSplitPanel1.Margin = new Padding(4);
            pnlSplitPanel1.MinimumSize = new Size(386, 200);
            pnlSplitPanel1.Name = "pnlSplitPanel1";
            pnlSplitPanel1.Size = new Size(752, 237);
            pnlSplitPanel1.TabIndex = 0;
            // 
            // pnlSplitPanel2
            // 
            pnlSplitPanel2.Dock = DockStyle.Fill;
            pnlSplitPanel2.Location = new Point(0, 0);
            pnlSplitPanel2.Margin = new Padding(4);
            pnlSplitPanel2.MinimumSize = new Size(386, 200);
            pnlSplitPanel2.Name = "pnlSplitPanel2";
            pnlSplitPanel2.Size = new Size(750, 237);
            pnlSplitPanel2.TabIndex = 0;
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.LightGray;
            pnlButtons.Controls.Add(btnDataLoadCon);
            pnlButtons.Controls.Add(btnTraningCon);
            pnlButtons.Controls.Add(btnResultCon);
            pnlButtons.Controls.Add(btnDataFilterCon);
            pnlButtons.Location = new Point(0, 4);
            pnlButtons.Margin = new Padding(4);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(1522, 80);
            pnlButtons.TabIndex = 2;
            // 
            // btnDataLoadCon
            // 
            btnDataLoadCon.Location = new Point(40, 12);
            btnDataLoadCon.Name = "btnDataLoadCon";
            btnDataLoadCon.Size = new Size(197, 57);
            btnDataLoadCon.TabIndex = 2;
            btnDataLoadCon.Text = "파일 로드";
            btnDataLoadCon.UseVisualStyleBackColor = true;
            // 
            // btnTraningCon
            // 
            btnTraningCon.Location = new Point(514, 12);
            btnTraningCon.Name = "btnTraningCon";
            btnTraningCon.Size = new Size(197, 57);
            btnTraningCon.TabIndex = 5;
            btnTraningCon.Text = "트레이닝";
            btnTraningCon.UseVisualStyleBackColor = true;
            // 
            // btnResultCon
            // 
            btnResultCon.Location = new Point(767, 12);
            btnResultCon.Name = "btnResultCon";
            btnResultCon.Size = new Size(197, 57);
            btnResultCon.TabIndex = 4;
            btnResultCon.Text = "결과";
            btnResultCon.UseVisualStyleBackColor = true;
            // 
            // btnDataFilterCon
            // 
            btnDataFilterCon.Location = new Point(276, 12);
            btnDataFilterCon.Name = "btnDataFilterCon";
            btnDataFilterCon.Size = new Size(197, 57);
            btnDataFilterCon.TabIndex = 1;
            btnDataFilterCon.Text = "파일 필터";
            btnDataFilterCon.UseVisualStyleBackColor = true;
            // 
            // pnlConditionView
            // 
            pnlConditionView.BackColor = Color.LightGray;
            pnlConditionView.Location = new Point(0, 1145);
            pnlConditionView.Margin = new Padding(4);
            pnlConditionView.Name = "pnlConditionView";
            pnlConditionView.Size = new Size(1522, 37);
            pnlConditionView.TabIndex = 3;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(513, 69);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(8, 4);
            checkedListBox1.TabIndex = 0;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1522, 1055);
            Controls.Add(pnlConditionView);
            Controls.Add(pnlButtons);
            Controls.Add(splLogHelp);
            Controls.Add(pnlMainContent);
            Margin = new Padding(4);
            Name = "MainWindow";
            Text = "SimpleDonkeyManager";
            splLogHelp.Panel1.ResumeLayout(false);
            splLogHelp.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splLogHelp).EndInit();
            splLogHelp.ResumeLayout(false);
            pnlSplitPanel1.ResumeLayout(false);
            pnlButtons.ResumeLayout(false);
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
        private CheckedListBox checkedListBox1;
    }
}
