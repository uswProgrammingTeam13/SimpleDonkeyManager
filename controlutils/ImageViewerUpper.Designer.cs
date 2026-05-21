namespace SimpleDonkeyManager.controlutils
{
    partial class ImageViewerUpper
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
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            panel2 = new Panel();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            panel3 = new Panel();
            button1 = new Button();
            button2 = new Button();
            button5 = new Button();
            button6 = new Button();
            trackBar2 = new TrackBar();
            label9 = new Label();
            comboBox1 = new ComboBox();
            panel4 = new Panel();
            label8 = new Label();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(trackBar2, 0, 3);
            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(panel3, 0, 5);
            tableLayoutPanel1.Controls.Add(panel2, 0, 4);
            tableLayoutPanel1.Controls.Add(panel4, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 48.913044F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.304348F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.695652F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.695652F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.521739F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.869565F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(556, 990);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(10, 10);
            pictureBox1.Margin = new Padding(10);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(536, 464);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 487);
            panel1.Name = "panel1";
            panel1.Size = new Size(550, 155);
            panel1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label2.Location = new Point(12, 33);
            label2.Name = "label2";
            label2.Size = new Size(233, 23);
            label2.TabIndex = 0;
            label2.Text = "Frame: 0001 / 12,345";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label3.Location = new Point(12, 66);
            label3.Name = "label3";
            label3.Size = new Size(170, 23);
            label3.TabIndex = 1;
            label3.Text = "Angle: 0.24 rad";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label4.Location = new Point(12, 101);
            label4.Name = "label4";
            label4.Size = new Size(150, 23);
            label4.TabIndex = 2;
            label4.Text = "Throttle: 0.18";
            // 
            // panel2
            // 
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label5);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 820);
            panel2.Name = "panel2";
            panel2.Size = new Size(550, 58);
            panel2.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label5.Location = new Point(3, 0);
            label5.Name = "label5";
            label5.Size = new Size(23, 23);
            label5.TabIndex = 0;
            label5.Text = "1";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label6.Location = new Point(195, 0);
            label6.Name = "label6";
            label6.Size = new Size(150, 23);
            label6.TabIndex = 1;
            label6.Text = "0001 / 12345";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label7.Location = new Point(472, -3);
            label7.Name = "label7";
            label7.Size = new Size(75, 23);
            label7.TabIndex = 2;
            label7.Text = "12345";
            // 
            // panel3
            // 
            panel3.Controls.Add(comboBox1);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(button6);
            panel3.Controls.Add(button5);
            panel3.Controls.Add(button2);
            panel3.Controls.Add(button1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(3, 884);
            panel3.Name = "panel3";
            panel3.Size = new Size(550, 103);
            panel3.TabIndex = 5;
            panel3.Paint += panel3_Paint;
            // 
            // button1
            // 
            button1.Font = new Font("맑은 고딕", 13F);
            button1.Location = new Point(43, 28);
            button1.Name = "button1";
            button1.Size = new Size(45, 45);
            button1.TabIndex = 0;
            button1.Text = "⏮";
            button1.TextAlign = ContentAlignment.TopCenter;
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Font = new Font("맑은 고딕", 13F);
            button2.Location = new Point(200, 28);
            button2.Name = "button2";
            button2.Size = new Size(45, 45);
            button2.TabIndex = 4;
            button2.Text = "⏭";
            button2.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.BackColor = Color.DodgerBlue;
            button5.Font = new Font("맑은 고딕", 13F);
            button5.ForeColor = SystemColors.ControlLightLight;
            button5.Location = new Point(149, 28);
            button5.Name = "button5";
            button5.Size = new Size(45, 45);
            button5.TabIndex = 5;
            button5.Text = "▶";
            button5.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            button6.Font = new Font("맑은 고딕", 13F);
            button6.Location = new Point(94, 28);
            button6.Name = "button6";
            button6.Size = new Size(45, 45);
            button6.TabIndex = 6;
            button6.Text = "◀";
            button6.UseVisualStyleBackColor = true;
            // 
            // trackBar2
            // 
            trackBar2.Dock = DockStyle.Fill;
            trackBar2.Location = new Point(3, 734);
            trackBar2.Maximum = 12345;
            trackBar2.Minimum = 1;
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(550, 80);
            trackBar2.TabIndex = 6;
            trackBar2.Value = 1;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label9.Location = new Point(352, 44);
            label9.Name = "label9";
            label9.Size = new Size(50, 23);
            label9.TabIndex = 7;
            label9.Text = "배속";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "0.5x", "1.0x", "2.0x" });
            comboBox1.Location = new Point(408, 38);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(86, 33);
            comboBox1.TabIndex = 8;
            comboBox1.Text = "1.0x";
            // 
            // panel4
            // 
            panel4.Controls.Add(label8);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(3, 648);
            panel4.Name = "panel4";
            panel4.Size = new Size(550, 80);
            panel4.TabIndex = 7;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.None;
            label8.AutoSize = true;
            label8.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label8.Location = new Point(12, 48);
            label8.Name = "label8";
            label8.Size = new Size(117, 23);
            label8.TabIndex = 8;
            label8.Text = "프레임 이동";
            // 
            // ImageViewerUpper
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "ImageViewerUpper";
            Size = new Size(556, 990);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBox1;
        private Panel panel1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Panel panel2;
        private Label label7;
        private Label label6;
        private Label label5;
        private Panel panel3;
        private Button button6;
        private Button button5;
        private Button button2;
        private Button button1;
        private TrackBar trackBar2;
        private ComboBox comboBox1;
        private Label label9;
        private Panel panel4;
        private Label label8;
    }
}
