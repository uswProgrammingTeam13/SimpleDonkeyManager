namespace SimpleDonkeyManager
{
    partial class DataFilterControl
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
            pnlFrameList = new Panel();
            pnlFilterSet = new Panel();
            pnlFilterAdditional = new Panel();
            lblFilterAdditional = new Label();
            pnlFilterAutoDel = new Panel();
            label2 = new Label();
            pnlFilterSetBasic = new Panel();
            checkBox1 = new CheckBox();
            lblFilterBasic = new Label();
            lblFilterSetting = new Label();
            pnlDataPreview = new Panel();
            pnlImageView = new Panel();
            pnlFilterResult = new Panel();
            checkBox2 = new CheckBox();
            label1 = new Label();
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            label3 = new Label();
            label4 = new Label();
            numericUpDown3 = new NumericUpDown();
            numericUpDown4 = new NumericUpDown();
            label5 = new Label();
            label6 = new Label();
            comboBox1 = new ComboBox();
            pnlFilterSet.SuspendLayout();
            pnlFilterAdditional.SuspendLayout();
            pnlFilterAutoDel.SuspendLayout();
            pnlFilterSetBasic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            SuspendLayout();
            // 
            // pnlFrameList
            // 
            pnlFrameList.BackColor = SystemColors.Control;
            pnlFrameList.BorderStyle = BorderStyle.FixedSingle;
            pnlFrameList.Location = new Point(3, 3);
            pnlFrameList.Margin = new Padding(4);
            pnlFrameList.Name = "pnlFrameList";
            pnlFrameList.Size = new Size(265, 594);
            pnlFrameList.TabIndex = 1;
            // 
            // pnlFilterSet
            // 
            pnlFilterSet.BackColor = SystemColors.Control;
            pnlFilterSet.BorderStyle = BorderStyle.FixedSingle;
            pnlFilterSet.Controls.Add(pnlFilterAdditional);
            pnlFilterSet.Controls.Add(pnlFilterAutoDel);
            pnlFilterSet.Controls.Add(pnlFilterSetBasic);
            pnlFilterSet.Controls.Add(lblFilterSetting);
            pnlFilterSet.Location = new Point(276, 3);
            pnlFilterSet.Margin = new Padding(4);
            pnlFilterSet.Name = "pnlFilterSet";
            pnlFilterSet.Size = new Size(341, 431);
            pnlFilterSet.TabIndex = 0;
            // 
            // pnlFilterAdditional
            // 
            pnlFilterAdditional.BackColor = SystemColors.Control;
            pnlFilterAdditional.BorderStyle = BorderStyle.FixedSingle;
            pnlFilterAdditional.Controls.Add(lblFilterAdditional);
            pnlFilterAdditional.ForeColor = SystemColors.ControlText;
            pnlFilterAdditional.Location = new Point(173, 234);
            pnlFilterAdditional.Name = "pnlFilterAdditional";
            pnlFilterAdditional.Size = new Size(157, 183);
            pnlFilterAdditional.TabIndex = 3;
            pnlFilterAdditional.Paint += pnlFilterAdditional_Paint;
            // 
            // lblFilterAdditional
            // 
            lblFilterAdditional.AutoSize = true;
            lblFilterAdditional.Font = new Font("나눔고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFilterAdditional.ForeColor = Color.Blue;
            lblFilterAdditional.Location = new Point(3, 6);
            lblFilterAdditional.Name = "lblFilterAdditional";
            lblFilterAdditional.Size = new Size(73, 17);
            lblFilterAdditional.TabIndex = 6;
            lblFilterAdditional.Text = "추가 필터";
            // 
            // pnlFilterAutoDel
            // 
            pnlFilterAutoDel.BackColor = SystemColors.Control;
            pnlFilterAutoDel.BorderStyle = BorderStyle.FixedSingle;
            pnlFilterAutoDel.Controls.Add(label2);
            pnlFilterAutoDel.ForeColor = SystemColors.ControlText;
            pnlFilterAutoDel.Location = new Point(173, 44);
            pnlFilterAutoDel.Name = "pnlFilterAutoDel";
            pnlFilterAutoDel.Size = new Size(157, 184);
            pnlFilterAutoDel.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("나눔고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label2.ForeColor = Color.Blue;
            label2.Location = new Point(3, 6);
            label2.Name = "label2";
            label2.Size = new Size(88, 17);
            label2.TabIndex = 5;
            label2.Text = "이상치 제거";
            // 
            // pnlFilterSetBasic
            // 
            pnlFilterSetBasic.BackColor = SystemColors.Control;
            pnlFilterSetBasic.BorderStyle = BorderStyle.FixedSingle;
            pnlFilterSetBasic.Controls.Add(comboBox1);
            pnlFilterSetBasic.Controls.Add(label6);
            pnlFilterSetBasic.Controls.Add(label4);
            pnlFilterSetBasic.Controls.Add(numericUpDown3);
            pnlFilterSetBasic.Controls.Add(numericUpDown4);
            pnlFilterSetBasic.Controls.Add(label5);
            pnlFilterSetBasic.Controls.Add(label3);
            pnlFilterSetBasic.Controls.Add(numericUpDown2);
            pnlFilterSetBasic.Controls.Add(numericUpDown1);
            pnlFilterSetBasic.Controls.Add(label1);
            pnlFilterSetBasic.Controls.Add(checkBox2);
            pnlFilterSetBasic.Controls.Add(checkBox1);
            pnlFilterSetBasic.Controls.Add(lblFilterBasic);
            pnlFilterSetBasic.ForeColor = SystemColors.ControlText;
            pnlFilterSetBasic.Location = new Point(10, 44);
            pnlFilterSetBasic.Name = "pnlFilterSetBasic";
            pnlFilterSetBasic.Size = new Size(157, 372);
            pnlFilterSetBasic.TabIndex = 1;
            pnlFilterSetBasic.Paint += pnlFilterSetBasic_Paint;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold);
            checkBox1.Location = new Point(7, 37);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(117, 18);
            checkBox1.TabIndex = 5;
            checkBox1.Text = "Throttle 0 제외";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // lblFilterBasic
            // 
            lblFilterBasic.AutoSize = true;
            lblFilterBasic.Font = new Font("나눔고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFilterBasic.ForeColor = Color.Blue;
            lblFilterBasic.Location = new Point(7, 7);
            lblFilterBasic.Name = "lblFilterBasic";
            lblFilterBasic.Size = new Size(73, 17);
            lblFilterBasic.TabIndex = 4;
            lblFilterBasic.Text = "기본 필터";
            // 
            // lblFilterSetting
            // 
            lblFilterSetting.AutoSize = true;
            lblFilterSetting.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFilterSetting.ForeColor = Color.Blue;
            lblFilterSetting.Location = new Point(9, 12);
            lblFilterSetting.Name = "lblFilterSetting";
            lblFilterSetting.Size = new Size(155, 21);
            lblFilterSetting.TabIndex = 0;
            lblFilterSetting.Text = "필터링 조건 설정";
            // 
            // pnlDataPreview
            // 
            pnlDataPreview.BackColor = SystemColors.Control;
            pnlDataPreview.BorderStyle = BorderStyle.FixedSingle;
            pnlDataPreview.Location = new Point(625, 3);
            pnlDataPreview.Margin = new Padding(4);
            pnlDataPreview.Name = "pnlDataPreview";
            pnlDataPreview.Size = new Size(239, 431);
            pnlDataPreview.TabIndex = 2;
            // 
            // pnlImageView
            // 
            pnlImageView.BackColor = SystemColors.Control;
            pnlImageView.BorderStyle = BorderStyle.FixedSingle;
            pnlImageView.Location = new Point(872, 3);
            pnlImageView.Margin = new Padding(4);
            pnlImageView.Name = "pnlImageView";
            pnlImageView.Size = new Size(300, 400);
            pnlImageView.TabIndex = 0;
            // 
            // pnlFilterResult
            // 
            pnlFilterResult.BackColor = SystemColors.Control;
            pnlFilterResult.BorderStyle = BorderStyle.FixedSingle;
            pnlFilterResult.Location = new Point(625, 442);
            pnlFilterResult.Margin = new Padding(4);
            pnlFilterResult.Name = "pnlFilterResult";
            pnlFilterResult.Size = new Size(548, 155);
            pnlFilterResult.TabIndex = 3;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold);
            checkBox2.Location = new Point(7, 64);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(146, 18);
            checkBox2.TabIndex = 6;
            checkBox2.Text = "기본 반전 이미지 제외";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label1.Location = new Point(7, 112);
            label1.Name = "label1";
            label1.Size = new Size(111, 14);
            label1.TabIndex = 7;
            label1.Text = "Angle 범위 (Rad)";
            label1.Click += label1_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.DecimalPlaces = 2;
            numericUpDown1.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown1.Location = new Point(7, 136);
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(55, 23);
            numericUpDown1.TabIndex = 8;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // numericUpDown2
            // 
            numericUpDown2.DecimalPlaces = 2;
            numericUpDown2.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown2.Location = new Point(93, 136);
            numericUpDown2.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(55, 23);
            numericUpDown2.TabIndex = 9;
            numericUpDown2.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label3.Location = new Point(70, 141);
            label3.Name = "label3";
            label3.Size = new Size(16, 14);
            label3.TabIndex = 10;
            label3.Text = "~";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label4.Location = new Point(70, 218);
            label4.Name = "label4";
            label4.Size = new Size(16, 14);
            label4.TabIndex = 14;
            label4.Text = "~";
            // 
            // numericUpDown3
            // 
            numericUpDown3.DecimalPlaces = 2;
            numericUpDown3.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown3.Location = new Point(93, 213);
            numericUpDown3.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(55, 23);
            numericUpDown3.TabIndex = 13;
            numericUpDown3.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // numericUpDown4
            // 
            numericUpDown4.DecimalPlaces = 2;
            numericUpDown4.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown4.Location = new Point(7, 213);
            numericUpDown4.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(55, 23);
            numericUpDown4.TabIndex = 12;
            numericUpDown4.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label5.Location = new Point(7, 189);
            label5.Name = "label5";
            label5.Size = new Size(86, 14);
            label5.TabIndex = 11;
            label5.Text = "Throttle 범위";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label6.Location = new Point(7, 269);
            label6.Name = "label6";
            label6.Size = new Size(71, 14);
            label6.TabIndex = 15;
            label6.Text = "해상도 필터";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(7, 292);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 16;
            comboBox1.Text = "(전체)";
            // 
            // DataFilterControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            Controls.Add(pnlFilterResult);
            Controls.Add(pnlImageView);
            Controls.Add(pnlDataPreview);
            Controls.Add(pnlFilterSet);
            Controls.Add(pnlFrameList);
            Name = "DataFilterControl";
            Size = new Size(1176, 600);
            pnlFilterSet.ResumeLayout(false);
            pnlFilterSet.PerformLayout();
            pnlFilterAdditional.ResumeLayout(false);
            pnlFilterAdditional.PerformLayout();
            pnlFilterAutoDel.ResumeLayout(false);
            pnlFilterAutoDel.PerformLayout();
            pnlFilterSetBasic.ResumeLayout(false);
            pnlFilterSetBasic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlFrameList;
        private Panel pnlFilterSet;
        private Panel pnlDataPreview;
        private Panel pnlImageView;
        private Panel pnlFilterResult;
        private Label lblFilterSetting;
        private Panel pnlFilterAutoDel;
        private Panel pnlFilterSetBasic;
        private Panel pnlFilterAdditional;
        private Label lblFilterAdditional;
        private Label label2;
        private Label lblFilterBasic;
        private CheckBox checkBox1;
        private Label label1;
        private CheckBox checkBox2;
        private NumericUpDown numericUpDown1;
        private Label label3;
        private NumericUpDown numericUpDown2;
        private ComboBox comboBox1;
        private Label label6;
        private Label label4;
        private NumericUpDown numericUpDown3;
        private NumericUpDown numericUpDown4;
        private Label label5;
    }
}
