namespace SimpleDonkeyManager
{
    partial class TrainingControl
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
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            checkBox1 = new CheckBox();
            comboBox6 = new ComboBox();
            comboBox5 = new ComboBox();
            comboBox4 = new ComboBox();
            comboBox3 = new ComboBox();
            comboBox2 = new ComboBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            comboBox1 = new ComboBox();
            label2 = new Label();
            groupBox2 = new GroupBox();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            panel2 = new Panel();
            tableLayoutPanel3 = new TableLayoutPanel();
            groupBox3 = new GroupBox();
            label15 = new Label();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            progressBar1 = new ProgressBar();
            groupBox4 = new GroupBox();
            richTextBox1 = new RichTextBox();
            panel3 = new Panel();
            groupBox5 = new GroupBox();
            imageViewerUpper1 = new SimpleDonkeyManager.controlutils.ImageViewerUpper();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1133, 576);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 0;
            label1.Text = "Training";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = SystemColors.ControlLightLight;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel2, 1, 0);
            tableLayoutPanel1.Controls.Add(panel3, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1176, 600);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(tableLayoutPanel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(323, 594);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel2.Controls.Add(groupBox2, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 65F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tableLayoutPanel2.Size = new Size(323, 594);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.FromArgb(248, 248, 248);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(comboBox6);
            groupBox1.Controls.Add(comboBox5);
            groupBox1.Controls.Add(comboBox4);
            groupBox1.Controls.Add(comboBox3);
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(label2);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold);
            groupBox1.ForeColor = Color.RoyalBlue;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(317, 380);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "학습 설정";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            checkBox1.ForeColor = SystemColors.ControlText;
            checkBox1.Location = new Point(31, 292);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(83, 19);
            checkBox1.TabIndex = 13;
            checkBox1.Text = "조기 종료";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox6
            // 
            comboBox6.FormattingEnabled = true;
            comboBox6.Location = new Point(135, 322);
            comboBox6.Name = "comboBox6";
            comboBox6.Size = new Size(121, 29);
            comboBox6.TabIndex = 12;
            // 
            // comboBox5
            // 
            comboBox5.FormattingEnabled = true;
            comboBox5.Location = new Point(135, 239);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(121, 29);
            comboBox5.TabIndex = 11;
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(135, 208);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(121, 29);
            comboBox4.TabIndex = 10;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(135, 177);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(121, 29);
            comboBox3.TabIndex = 9;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(135, 142);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(121, 29);
            comboBox2.TabIndex = 8;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label8.ForeColor = SystemColors.ControlText;
            label8.Location = new Point(31, 330);
            label8.Name = "label8";
            label8.Size = new Size(66, 15);
            label8.TabIndex = 7;
            label8.Text = "patience";
            label8.Click += label8_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label7.ForeColor = SystemColors.ControlText;
            label7.Location = new Point(29, 246);
            label7.Name = "label7";
            label7.Size = new Size(46, 15);
            label7.TabIndex = 6;
            label7.Text = "학습률";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label6.ForeColor = SystemColors.ControlText;
            label6.Location = new Point(31, 216);
            label6.Name = "label6";
            label6.Size = new Size(46, 15);
            label6.TabIndex = 5;
            label6.Text = "에포크";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label5.ForeColor = SystemColors.ControlText;
            label5.Location = new Point(31, 185);
            label5.Name = "label5";
            label5.Size = new Size(64, 15);
            label5.TabIndex = 4;
            label5.Text = "배치 크기";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label4.ForeColor = SystemColors.ControlText;
            label4.Location = new Point(31, 150);
            label4.Name = "label4";
            label4.Size = new Size(77, 15);
            label4.TabIndex = 3;
            label4.Text = "이미지 크기";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label3.ForeColor = SystemColors.ControlText;
            label3.Location = new Point(31, 105);
            label3.Name = "label3";
            label3.Size = new Size(77, 15);
            label3.TabIndex = 2;
            label3.Text = "데이터 경로";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Behavior Cloning", "Linear", "Categorical" });
            comboBox1.Location = new Point(135, 55);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 29);
            comboBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(31, 61);
            label2.Name = "label2";
            label2.Size = new Size(64, 15);
            label2.TabIndex = 0;
            label2.Text = "모델 선택";
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.FromArgb(248, 248, 248);
            groupBox2.Controls.Add(button4);
            groupBox2.Controls.Add(button3);
            groupBox2.Controls.Add(button2);
            groupBox2.Controls.Add(button1);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold);
            groupBox2.ForeColor = Color.RoyalBlue;
            groupBox2.Location = new Point(3, 389);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(317, 202);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "학습 제어";
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(242, 242, 242);
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("나눔고딕", 12F, FontStyle.Bold);
            button4.ForeColor = Color.DodgerBlue;
            button4.Location = new Point(165, 122);
            button4.Name = "button4";
            button4.Size = new Size(121, 39);
            button4.TabIndex = 3;
            button4.Text = "↻ 초기화";
            button4.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(242, 242, 242);
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("나눔고딕", 12F, FontStyle.Bold);
            button3.ForeColor = Color.DodgerBlue;
            button3.Location = new Point(29, 122);
            button3.Name = "button3";
            button3.Size = new Size(121, 39);
            button3.TabIndex = 2;
            button3.Text = "■ 중지";
            button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(242, 242, 242);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("나눔고딕", 12F, FontStyle.Bold);
            button2.ForeColor = Color.DodgerBlue;
            button2.Location = new Point(165, 59);
            button2.Name = "button2";
            button2.Size = new Size(121, 39);
            button2.TabIndex = 1;
            button2.Text = "⏸ 일시정지";
            button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(242, 242, 242);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("나눔고딕", 12F, FontStyle.Bold);
            button1.ForeColor = Color.DodgerBlue;
            button1.Location = new Point(29, 59);
            button1.Name = "button1";
            button1.Size = new Size(121, 39);
            button1.TabIndex = 0;
            button1.Text = "▶ 시작";
            button1.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(tableLayoutPanel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(332, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(440, 594);
            panel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(groupBox3, 0, 0);
            tableLayoutPanel3.Controls.Add(groupBox4, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            tableLayoutPanel3.Size = new Size(440, 594);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // groupBox3
            // 
            groupBox3.BackColor = Color.FromArgb(248, 248, 248);
            groupBox3.Controls.Add(label15);
            groupBox3.Controls.Add(label14);
            groupBox3.Controls.Add(label13);
            groupBox3.Controls.Add(label12);
            groupBox3.Controls.Add(label11);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(progressBar1);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold);
            groupBox3.ForeColor = Color.RoyalBlue;
            groupBox3.Location = new Point(3, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(434, 261);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "학습 진행 상태";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label15.ForeColor = SystemColors.ControlText;
            label15.Location = new Point(20, 220);
            label15.Name = "label15";
            label15.Size = new Size(95, 15);
            label15.TabIndex = 7;
            label15.Text = "예상 완료 시간";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label14.ForeColor = SystemColors.ControlText;
            label14.Location = new Point(20, 193);
            label14.Name = "label14";
            label14.Size = new Size(64, 15);
            label14.TabIndex = 6;
            label14.Text = "결과 시간";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label13.ForeColor = SystemColors.ControlText;
            label13.Location = new Point(20, 168);
            label13.Name = "label13";
            label13.Size = new Size(64, 15);
            label13.TabIndex = 5;
            label13.Text = "남은 시간";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label12.ForeColor = SystemColors.ControlText;
            label12.Location = new Point(20, 141);
            label12.Name = "label12";
            label12.Size = new Size(90, 15);
            label12.TabIndex = 4;
            label12.Text = "데이터 사용량";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label11.ForeColor = SystemColors.ControlText;
            label11.Location = new Point(20, 113);
            label11.Name = "label11";
            label11.Size = new Size(64, 15);
            label11.TabIndex = 3;
            label11.Text = "현재 단계";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label10.ForeColor = SystemColors.ControlText;
            label10.Location = new Point(20, 87);
            label10.Name = "label10";
            label10.Size = new Size(80, 15);
            label10.TabIndex = 2;
            label10.Text = "현재 epoch";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label9.ForeColor = SystemColors.ControlText;
            label9.Location = new Point(20, 34);
            label9.Name = "label9";
            label9.Size = new Size(46, 15);
            label9.TabIndex = 1;
            label9.Text = "진행률";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(20, 52);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(392, 23);
            progressBar1.TabIndex = 0;
            // 
            // groupBox4
            // 
            groupBox4.BackColor = Color.FromArgb(248, 248, 248);
            groupBox4.Controls.Add(richTextBox1);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold);
            groupBox4.ForeColor = Color.RoyalBlue;
            groupBox4.Location = new Point(3, 270);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(434, 321);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            groupBox4.Text = "학습 로그";
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = SystemColors.ControlLightLight;
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Location = new Point(3, 25);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(428, 293);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Controls.Add(groupBox5);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(778, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(395, 594);
            panel3.TabIndex = 2;
            // 
            // groupBox5
            // 
            groupBox5.BackColor = Color.FromArgb(248, 248, 248);
            groupBox5.Controls.Add(imageViewerUpper1);
            groupBox5.Dock = DockStyle.Fill;
            groupBox5.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold);
            groupBox5.ForeColor = Color.RoyalBlue;
            groupBox5.Location = new Point(0, 0);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(395, 594);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Text = "이미지 미리보기";
            // 
            // imageViewerUpper1
            // 
            imageViewerUpper1.Dock = DockStyle.Fill;
            imageViewerUpper1.Location = new Point(3, 25);
            imageViewerUpper1.Name = "imageViewerUpper1";
            imageViewerUpper1.Size = new Size(389, 566);
            imageViewerUpper1.TabIndex = 0;
            // 
            // TrainingControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(label1);
            Name = "TrainingControl";
            Size = new Size(1176, 600);
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            panel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label5;
        private Label label4;
        private Label label3;
        private ComboBox comboBox1;
        private Label label2;
        private Panel panel2;
        private Panel panel3;
        private ComboBox comboBox6;
        private ComboBox comboBox5;
        private ComboBox comboBox4;
        private ComboBox comboBox3;
        private ComboBox comboBox2;
        private Label label8;
        private Label label7;
        private Label label6;
        private CheckBox checkBox1;
        private TableLayoutPanel tableLayoutPanel3;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private ProgressBar progressBar1;
        private RichTextBox richTextBox1;
        private GroupBox groupBox5;
        private SimpleDonkeyManager.controlutils.ImageViewerUpper imageViewerUpper1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button1;
    }
}
