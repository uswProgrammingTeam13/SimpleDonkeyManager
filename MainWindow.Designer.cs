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
            btnDebugControlChanger = new Button();
            pnlConditionView = new Panel();
            ((System.ComponentModel.ISupportInitialize)splLogHelp).BeginInit();
            splLogHelp.Panel1.SuspendLayout();
            splLogHelp.Panel2.SuspendLayout();
            splLogHelp.SuspendLayout();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMainContent
            // 
            pnlMainContent.BackColor = SystemColors.ControlLight;
            pnlMainContent.Location = new Point(4, 69);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.Size = new Size(1176, 600);
            pnlMainContent.TabIndex = 0;
            // 
            // splLogHelp
            // 
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
            splLogHelp.SplitterDistance = 591;
            splLogHelp.SplitterWidth = 8;
            splLogHelp.TabIndex = 1;
            // 
            // pnlSplitPanel1
            // 
            pnlSplitPanel1.Dock = DockStyle.Fill;
            pnlSplitPanel1.Location = new Point(0, 0);
            pnlSplitPanel1.MinimumSize = new Size(300, 150);
            pnlSplitPanel1.Name = "pnlSplitPanel1";
            pnlSplitPanel1.Size = new Size(591, 178);
            pnlSplitPanel1.TabIndex = 0;
            // 
            // pnlSplitPanel2
            // 
            pnlSplitPanel2.Dock = DockStyle.Fill;
            pnlSplitPanel2.Location = new Point(0, 0);
            pnlSplitPanel2.MinimumSize = new Size(300, 150);
            pnlSplitPanel2.Name = "pnlSplitPanel2";
            pnlSplitPanel2.Size = new Size(577, 178);
            pnlSplitPanel2.TabIndex = 0;
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.LightGray;
            pnlButtons.Controls.Add(btnDebugControlChanger);
            pnlButtons.Location = new Point(0, 3);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(1184, 60);
            pnlButtons.TabIndex = 2;
            // 
            // btnDebugControlChanger
            // 
            btnDebugControlChanger.Location = new Point(1124, 9);
            btnDebugControlChanger.Name = "btnDebugControlChanger";
            btnDebugControlChanger.Size = new Size(43, 43);
            btnDebugControlChanger.TabIndex = 0;
            btnDebugControlChanger.Text = "다음";
            btnDebugControlChanger.UseVisualStyleBackColor = true;
            // 
            // pnlConditionView
            // 
            pnlConditionView.BackColor = Color.LightGray;
            pnlConditionView.Location = new Point(0, 859);
            pnlConditionView.Name = "pnlConditionView";
            pnlConditionView.Size = new Size(1184, 28);
            pnlConditionView.TabIndex = 3;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 885);
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
        private Button btnDebugControlChanger;
    }
}
