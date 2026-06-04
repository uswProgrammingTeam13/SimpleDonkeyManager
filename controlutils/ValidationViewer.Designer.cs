namespace SimpleDonkeyManager.controlutils
{
    partial class ValidationViewer
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            pnlImage = new System.Windows.Forms.Panel();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            pnlControls = new System.Windows.Forms.Panel();
            btnFirst = new System.Windows.Forms.Button();
            btnPlay = new System.Windows.Forms.Button();
            btnNext = new System.Windows.Forms.Button();
            lblSpeed = new System.Windows.Forms.Label();
            comboBox1 = new System.Windows.Forms.ComboBox();
            pnlTrackBar = new System.Windows.Forms.Panel();
            trackBar1 = new System.Windows.Forms.TrackBar();
            pnlInfo = new System.Windows.Forms.Panel();
            lblFrame = new System.Windows.Forms.Label();
            lblAngle = new System.Windows.Forms.Label();
            lblThrottle = new System.Windows.Forms.Label();
            lblError = new System.Windows.Forms.Label();
            tableLayoutPanel1.SuspendLayout();
            pnlImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlControls.SuspendLayout();
            pnlTrackBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            pnlInfo.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(pnlImage, 0, 0);
            tableLayoutPanel1.Controls.Add(pnlControls, 0, 1);
            tableLayoutPanel1.Controls.Add(pnlTrackBar, 0, 2);
            tableLayoutPanel1.Controls.Add(pnlInfo, 0, 3);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            tableLayoutPanel1.Size = new System.Drawing.Size(357, 600);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlImage
            // 
            pnlImage.BackColor = System.Drawing.Color.White;
            pnlImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlImage.Controls.Add(pictureBox1);
            pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlImage.Location = new System.Drawing.Point(3, 3);
            pnlImage.Name = "pnlImage";
            pnlImage.Size = new System.Drawing.Size(351, 324);
            pnlImage.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(349, 322);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pnlControls
            // 
            pnlControls.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            pnlControls.Controls.Add(btnFirst);
            pnlControls.Controls.Add(btnPlay);
            pnlControls.Controls.Add(btnNext);
            pnlControls.Controls.Add(lblSpeed);
            pnlControls.Controls.Add(comboBox1);
            pnlControls.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlControls.Location = new System.Drawing.Point(3, 333);
            pnlControls.Name = "pnlControls";
            pnlControls.Size = new System.Drawing.Size(351, 64);
            pnlControls.TabIndex = 1;
            // 
            // btnFirst
            // 
            btnFirst.BackColor = System.Drawing.Color.DodgerBlue;
            btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnFirst.Font = new System.Drawing.Font("나눔고딕", 24F, System.Drawing.FontStyle.Bold);
            btnFirst.ForeColor = System.Drawing.Color.White;
            btnFirst.Location = new System.Drawing.Point(15, 8);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new System.Drawing.Size(50, 50);
            btnFirst.TabIndex = 0;
            btnFirst.Text = "⏮";
            btnFirst.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnFirst.UseVisualStyleBackColor = false;
            // 
            // btnPlay
            // 
            btnPlay.BackColor = System.Drawing.Color.DodgerBlue;
            btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPlay.Font = new System.Drawing.Font("나눔고딕", 24F, System.Drawing.FontStyle.Bold);
            btnPlay.ForeColor = System.Drawing.Color.White;
            btnPlay.Location = new System.Drawing.Point(75, 8);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new System.Drawing.Size(50, 50);
            btnPlay.TabIndex = 1;
            btnPlay.Text = "▶";
            btnPlay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnPlay.UseVisualStyleBackColor = false;
            // 
            // btnNext
            // 
            btnNext.BackColor = System.Drawing.Color.DodgerBlue;
            btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNext.Font = new System.Drawing.Font("나눔고딕", 24F, System.Drawing.FontStyle.Bold);
            btnNext.ForeColor = System.Drawing.Color.White;
            btnNext.Location = new System.Drawing.Point(135, 8);
            btnNext.Name = "btnNext";
            btnNext.Size = new System.Drawing.Size(50, 50);
            btnNext.TabIndex = 2;
            btnNext.Text = "⏭";
            btnNext.UseVisualStyleBackColor = false;
            // 
            // lblSpeed
            // 
            lblSpeed.AutoSize = true;
            lblSpeed.Font = new System.Drawing.Font("나눔고딕", 10F, System.Drawing.FontStyle.Bold);
            lblSpeed.Location = new System.Drawing.Point(208, 15);
            lblSpeed.Name = "lblSpeed";
            lblSpeed.Size = new System.Drawing.Size(40, 16);
            lblSpeed.TabIndex = 3;
            lblSpeed.Text = "배속:";
            // 
            // comboBox1
            // 
            comboBox1.Font = new System.Drawing.Font("나눔고딕", 10F, System.Drawing.FontStyle.Bold);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "0.5x", "1.0x", "1.5x", "2.0x" });
            comboBox1.Location = new System.Drawing.Point(254, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(80, 23);
            comboBox1.TabIndex = 4;
            comboBox1.Text = "1.0x";
            // 
            // pnlTrackBar
            // 
            pnlTrackBar.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
            pnlTrackBar.Controls.Add(trackBar1);
            pnlTrackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlTrackBar.Location = new System.Drawing.Point(3, 403);
            pnlTrackBar.Name = "pnlTrackBar";
            pnlTrackBar.Padding = new System.Windows.Forms.Padding(10);
            pnlTrackBar.Size = new System.Drawing.Size(351, 54);
            pnlTrackBar.TabIndex = 2;
            // 
            // trackBar1
            // 
            trackBar1.BackColor = System.Drawing.Color.White;
            trackBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            trackBar1.Location = new System.Drawing.Point(10, 10);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new System.Drawing.Size(331, 34);
            trackBar1.TabIndex = 0;
            // 
            // pnlInfo
            // 
            pnlInfo.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            pnlInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlInfo.Controls.Add(lblFrame);
            pnlInfo.Controls.Add(lblAngle);
            pnlInfo.Controls.Add(lblThrottle);
            pnlInfo.Controls.Add(lblError);
            pnlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlInfo.Location = new System.Drawing.Point(3, 463);
            pnlInfo.Name = "pnlInfo";
            pnlInfo.Size = new System.Drawing.Size(351, 134);
            pnlInfo.TabIndex = 3;
            // 
            // lblFrame
            // 
            lblFrame.AutoSize = true;
            lblFrame.Font = new System.Drawing.Font("나눔고딕", 11F, System.Drawing.FontStyle.Bold);
            lblFrame.ForeColor = System.Drawing.Color.RoyalBlue;
            lblFrame.Location = new System.Drawing.Point(10, 8);
            lblFrame.Name = "lblFrame";
            lblFrame.Size = new System.Drawing.Size(120, 17);
            lblFrame.TabIndex = 0;
            lblFrame.Text = "현재 프레임: -";
            // 
            // lblAngle
            // 
            lblAngle.AutoSize = true;
            lblAngle.Font = new System.Drawing.Font("나눔고딕", 10F, System.Drawing.FontStyle.Bold);
            lblAngle.Location = new System.Drawing.Point(10, 36);
            lblAngle.Name = "lblAngle";
            lblAngle.Size = new System.Drawing.Size(180, 16);
            lblAngle.TabIndex = 1;
            lblAngle.Text = "실제 조향값: -    AI 예측 조향값: -";
            // 
            // lblThrottle
            // 
            lblThrottle.AutoSize = true;
            lblThrottle.Font = new System.Drawing.Font("나눔고딕", 10F, System.Drawing.FontStyle.Bold);
            lblThrottle.Location = new System.Drawing.Point(10, 64);
            lblThrottle.Name = "lblThrottle";
            lblThrottle.Size = new System.Drawing.Size(180, 16);
            lblThrottle.TabIndex = 2;
            lblThrottle.Text = "실제 속도값: -    AI 예측 속도값: -";
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new System.Drawing.Font("나눔고딕", 11F, System.Drawing.FontStyle.Bold);
            lblError.ForeColor = System.Drawing.Color.OrangeRed;
            lblError.Location = new System.Drawing.Point(10, 96);
            lblError.Name = "lblError";
            lblError.Size = new System.Drawing.Size(80, 17);
            lblError.TabIndex = 3;
            lblError.Text = "오차: -";
            // 
            // ValidationViewer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "ValidationViewer";
            Size = new System.Drawing.Size(357, 600);
            tableLayoutPanel1.ResumeLayout(false);
            pnlImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlControls.ResumeLayout(false);
            pnlControls.PerformLayout();
            pnlTrackBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            pnlInfo.ResumeLayout(false);
            pnlInfo.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel pnlTrackBar;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblFrame;
        private System.Windows.Forms.Label lblAngle;
        private System.Windows.Forms.Label lblThrottle;
        private System.Windows.Forms.Label lblError;
    }
}
