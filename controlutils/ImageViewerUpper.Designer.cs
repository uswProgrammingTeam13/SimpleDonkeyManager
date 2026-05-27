namespace SimpleDonkeyManager.controlutils
{
    partial class ImageViewerUpper
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
            tableLayoutPanel1 = new TableLayoutPanel();
            pnlImage = new Panel();
            pictureBox1 = new PictureBox();
            pnlControls = new Panel();
            btnFirst = new Button();
            btnPlay = new Button();
            btnNext = new Button();
            lblSpeed = new Label();
            comboBox1 = new ComboBox();
            pnlTrackBar = new Panel();
            trackBar2 = new TrackBar();
            pnlInfo = new Panel();
            lblFrame = new Label();
            lblAngle = new Label();
            lblThrottle = new Label();
            tableLayoutPanel1.SuspendLayout();
            pnlImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlControls.SuspendLayout();
            pnlTrackBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            pnlInfo.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(pnlImage, 0, 0);
            tableLayoutPanel1.Controls.Add(pnlControls, 0, 1);
            tableLayoutPanel1.Controls.Add(pnlTrackBar, 0, 2);
            tableLayoutPanel1.Controls.Add(pnlInfo, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.Size = new Size(357, 600);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlImage
            // 
            pnlImage.BackColor = Color.White;
            pnlImage.BorderStyle = BorderStyle.FixedSingle;
            pnlImage.Controls.Add(pictureBox1);
            pnlImage.Dock = DockStyle.Fill;
            pnlImage.Location = new Point(3, 3);
            pnlImage.Name = "pnlImage";
            pnlImage.Size = new Size(351, 364);
            pnlImage.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(349, 362);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pnlControls
            // 
            pnlControls.BackColor = Color.FromArgb(248, 248, 248);
            pnlControls.Controls.Add(btnFirst);
            pnlControls.Controls.Add(btnPlay);
            pnlControls.Controls.Add(btnNext);
            pnlControls.Controls.Add(lblSpeed);
            pnlControls.Controls.Add(comboBox1);
            pnlControls.Dock = DockStyle.Fill;
            pnlControls.Location = new Point(3, 373);
            pnlControls.Name = "pnlControls";
            pnlControls.Size = new Size(351, 64);
            pnlControls.TabIndex = 1;
            // 
            // btnFirst
            // 
            btnFirst.BackColor = Color.DodgerBlue;
            btnFirst.FlatStyle = FlatStyle.Flat;
            btnFirst.Font = new Font("나눔고딕", 24F, FontStyle.Bold);
            btnFirst.ForeColor = Color.White;
            btnFirst.Location = new Point(15, 8);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(50, 50);
            btnFirst.TabIndex = 0;
            btnFirst.Text = "⏮";
            btnFirst.TextAlign = ContentAlignment.MiddleLeft;
            btnFirst.UseVisualStyleBackColor = false;
            // 
            // btnPlay
            // 
            btnPlay.BackColor = Color.DodgerBlue;
            btnPlay.FlatStyle = FlatStyle.Flat;
            btnPlay.Font = new Font("나눔고딕", 24F, FontStyle.Bold);
            btnPlay.ForeColor = Color.White;
            btnPlay.Location = new Point(75, 8);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(50, 50);
            btnPlay.TabIndex = 1;
            btnPlay.Text = "▶";
            btnPlay.TextAlign = ContentAlignment.MiddleLeft;
            btnPlay.UseVisualStyleBackColor = false;
            // 
            // btnNext
            // 
            btnNext.BackColor = Color.DodgerBlue;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Font = new Font("나눔고딕", 24F, FontStyle.Bold);
            btnNext.ForeColor = Color.White;
            btnNext.Location = new Point(135, 8);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(50, 50);
            btnNext.TabIndex = 2;
            btnNext.Text = "⏭";
            btnNext.UseVisualStyleBackColor = false;
            // 
            // lblSpeed
            // 
            lblSpeed.AutoSize = true;
            lblSpeed.Font = new Font("나눔고딕", 10F, FontStyle.Bold);
            lblSpeed.Location = new Point(208, 15);
            lblSpeed.Name = "lblSpeed";
            lblSpeed.Size = new Size(40, 16);
            lblSpeed.TabIndex = 3;
            lblSpeed.Text = "배속:";
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("나눔고딕", 10F, FontStyle.Bold);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "0.5x", "1.0x", "1.5x", "2.0x" });
            comboBox1.Location = new Point(254, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(80, 23);
            comboBox1.TabIndex = 4;
            comboBox1.Text = "1.0x";
            // 
            // pnlTrackBar
            // 
            pnlTrackBar.BackColor = Color.FromArgb(242, 242, 242);
            pnlTrackBar.Controls.Add(trackBar2);
            pnlTrackBar.Dock = DockStyle.Fill;
            pnlTrackBar.Location = new Point(3, 443);
            pnlTrackBar.Name = "pnlTrackBar";
            pnlTrackBar.Padding = new Padding(10);
            pnlTrackBar.Size = new Size(351, 54);
            pnlTrackBar.TabIndex = 2;
            // 
            // trackBar2
            // 
            trackBar2.BackColor = Color.White;
            trackBar2.Dock = DockStyle.Fill;
            trackBar2.Location = new Point(10, 10);
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(331, 34);
            trackBar2.TabIndex = 0;
            // 
            // pnlInfo
            // 
            pnlInfo.BackColor = Color.FromArgb(248, 248, 248);
            pnlInfo.BorderStyle = BorderStyle.FixedSingle;
            pnlInfo.Controls.Add(lblFrame);
            pnlInfo.Controls.Add(lblAngle);
            pnlInfo.Controls.Add(lblThrottle);
            pnlInfo.Dock = DockStyle.Fill;
            pnlInfo.Location = new Point(3, 503);
            pnlInfo.Name = "pnlInfo";
            pnlInfo.Size = new Size(351, 94);
            pnlInfo.TabIndex = 3;
            // 
            // lblFrame
            // 
            lblFrame.AutoSize = true;
            lblFrame.Font = new Font("나눔고딕", 11F, FontStyle.Bold);
            lblFrame.Location = new Point(10, 10);
            lblFrame.Name = "lblFrame";
            lblFrame.Size = new Size(134, 17);
            lblFrame.TabIndex = 0;
            lblFrame.Text = "Frame: 0000 / 0";
            // 
            // lblAngle
            // 
            lblAngle.AutoSize = true;
            lblAngle.Font = new Font("나눔고딕", 11F, FontStyle.Bold);
            lblAngle.Location = new Point(10, 35);
            lblAngle.Name = "lblAngle";
            lblAngle.Size = new Size(131, 17);
            lblAngle.TabIndex = 1;
            lblAngle.Text = "Angle: 0.00 rad";
            // 
            // lblThrottle
            // 
            lblThrottle.AutoSize = true;
            lblThrottle.Font = new Font("나눔고딕", 11F, FontStyle.Bold);
            lblThrottle.Location = new Point(10, 60);
            lblThrottle.Name = "lblThrottle";
            lblThrottle.Size = new Size(116, 17);
            lblThrottle.TabIndex = 2;
            lblThrottle.Text = "Throttle: 0.00";
            // 
            // ImageViewerUpper
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "ImageViewerUpper";
            Size = new Size(357, 600);
            tableLayoutPanel1.ResumeLayout(false);
            pnlImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlControls.ResumeLayout(false);
            pnlControls.PerformLayout();
            pnlTrackBar.ResumeLayout(false);
            pnlTrackBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
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
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblFrame;
        private System.Windows.Forms.Label lblAngle;
        private System.Windows.Forms.Label lblThrottle;
    }
}
