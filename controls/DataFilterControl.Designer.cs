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
            chkHighlightDel = new CheckBox();
            lblFilterAdditional = new Label();
            chkDelFrames = new CheckBox();
            pnlFilterSetBasic = new Panel();
            comboBox1 = new ComboBox();
            lblFilterSize = new Label();
            lblFilterThrottlenum = new Label();
            numFilterThrottle2 = new NumericUpDown();
            numFilterThrottle1 = new NumericUpDown();
            lblFilterThrottle = new Label();
            lblFilterAnglenum = new Label();
            numFilterAngle2 = new NumericUpDown();
            numFilterAngle1 = new NumericUpDown();
            lblFilterAngle = new Label();
            chkDisable = new CheckBox();
            chkThrottle = new CheckBox();
            lblFilterBasic = new Label();
            lblFilterSetting = new Label();
            pnlImageView = new Panel();
            pnlFilterResult = new Panel();
            lblFilterSummary = new Label();
            lstFilterSummary = new ListView();
            btnFilterPreview = new Button();
            btnFilterReset = new Button();
            btnFilterStart = new Button();
            pnlFilterSet.SuspendLayout();
            pnlFilterAdditional.SuspendLayout();
            pnlFilterSetBasic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numFilterThrottle2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numFilterThrottle1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numFilterAngle2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numFilterAngle1).BeginInit();
            pnlFilterResult.SuspendLayout();
            SuspendLayout();
            // 
            // pnlFrameList
            // 
            pnlFrameList.BackColor = Color.FromArgb(248, 248, 248);
            pnlFrameList.BorderStyle = BorderStyle.FixedSingle;
            pnlFrameList.Location = new Point(3, 3);
            pnlFrameList.Margin = new Padding(4);
            pnlFrameList.Name = "pnlFrameList";
            pnlFrameList.Size = new Size(265, 594);
            pnlFrameList.TabIndex = 1;
            // 
            // pnlFilterSet
            // 
            pnlFilterSet.BackColor = Color.FromArgb(248, 248, 248);
            pnlFilterSet.BorderStyle = BorderStyle.FixedSingle;
            pnlFilterSet.Controls.Add(pnlFilterAdditional);
            pnlFilterSet.Controls.Add(pnlFilterSetBasic);
            pnlFilterSet.Controls.Add(lblFilterSetting);
            pnlFilterSet.Location = new Point(276, 3);
            pnlFilterSet.Margin = new Padding(4);
            pnlFilterSet.Name = "pnlFilterSet";
            pnlFilterSet.Size = new Size(239, 431);
            pnlFilterSet.TabIndex = 0;
            // 
            // pnlFilterAdditional
            // 
            pnlFilterAdditional.BackColor = Color.FromArgb(242, 242, 242);
            pnlFilterAdditional.BorderStyle = BorderStyle.FixedSingle;
            pnlFilterAdditional.Controls.Add(chkHighlightDel);
            pnlFilterAdditional.Controls.Add(lblFilterAdditional);
            pnlFilterAdditional.Controls.Add(chkDelFrames);
            pnlFilterAdditional.ForeColor = SystemColors.ControlText;
            pnlFilterAdditional.Location = new Point(9, 334);
            pnlFilterAdditional.Name = "pnlFilterAdditional";
            pnlFilterAdditional.Size = new Size(218, 84);
            pnlFilterAdditional.TabIndex = 3;
            // 
            // chkHighlightDel
            // 
            chkHighlightDel.AutoSize = true;
            chkHighlightDel.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold);
            chkHighlightDel.Location = new Point(7, 60);
            chkHighlightDel.Name = "chkHighlightDel";
            chkHighlightDel.Size = new Size(150, 18);
            chkHighlightDel.TabIndex = 18;
            chkHighlightDel.Text = "조향 값 급변 구간 제거";
            chkHighlightDel.UseVisualStyleBackColor = true;
            // 
            // lblFilterAdditional
            // 
            lblFilterAdditional.AutoSize = true;
            lblFilterAdditional.Font = new Font("나눔고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFilterAdditional.ForeColor = Color.RoyalBlue;
            lblFilterAdditional.Location = new Point(3, 6);
            lblFilterAdditional.Name = "lblFilterAdditional";
            lblFilterAdditional.Size = new Size(73, 17);
            lblFilterAdditional.TabIndex = 6;
            lblFilterAdditional.Text = "추가 필터";
            // 
            // chkDelFrames
            // 
            chkDelFrames.AutoSize = true;
            chkDelFrames.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold);
            chkDelFrames.Location = new Point(7, 33);
            chkDelFrames.Name = "chkDelFrames";
            chkDelFrames.Size = new Size(118, 18);
            chkDelFrames.TabIndex = 17;
            chkDelFrames.Text = "중복 프레임 제거";
            chkDelFrames.UseVisualStyleBackColor = true;
            // 
            // pnlFilterSetBasic
            // 
            pnlFilterSetBasic.BackColor = Color.FromArgb(242, 242, 242);
            pnlFilterSetBasic.BorderStyle = BorderStyle.FixedSingle;
            pnlFilterSetBasic.Controls.Add(comboBox1);
            pnlFilterSetBasic.Controls.Add(lblFilterSize);
            pnlFilterSetBasic.Controls.Add(lblFilterThrottlenum);
            pnlFilterSetBasic.Controls.Add(numFilterThrottle2);
            pnlFilterSetBasic.Controls.Add(numFilterThrottle1);
            pnlFilterSetBasic.Controls.Add(lblFilterThrottle);
            pnlFilterSetBasic.Controls.Add(lblFilterAnglenum);
            pnlFilterSetBasic.Controls.Add(numFilterAngle2);
            pnlFilterSetBasic.Controls.Add(numFilterAngle1);
            pnlFilterSetBasic.Controls.Add(lblFilterAngle);
            pnlFilterSetBasic.Controls.Add(chkDisable);
            pnlFilterSetBasic.Controls.Add(chkThrottle);
            pnlFilterSetBasic.Controls.Add(lblFilterBasic);
            pnlFilterSetBasic.ForeColor = SystemColors.ControlText;
            pnlFilterSetBasic.Location = new Point(10, 44);
            pnlFilterSetBasic.Name = "pnlFilterSetBasic";
            pnlFilterSetBasic.Size = new Size(217, 279);
            pnlFilterSetBasic.TabIndex = 1;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(7, 237);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 16;
            comboBox1.Text = "(전체)";
            // 
            // lblFilterSize
            // 
            lblFilterSize.AutoSize = true;
            lblFilterSize.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFilterSize.Location = new Point(7, 214);
            lblFilterSize.Name = "lblFilterSize";
            lblFilterSize.Size = new Size(71, 14);
            lblFilterSize.TabIndex = 15;
            lblFilterSize.Text = "해상도 필터";
            // 
            // lblFilterThrottlenum
            // 
            lblFilterThrottlenum.AutoSize = true;
            lblFilterThrottlenum.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFilterThrottlenum.Location = new Point(70, 183);
            lblFilterThrottlenum.Name = "lblFilterThrottlenum";
            lblFilterThrottlenum.Size = new Size(16, 14);
            lblFilterThrottlenum.TabIndex = 14;
            lblFilterThrottlenum.Text = "~";
            // 
            // numFilterThrottle2
            // 
            numFilterThrottle2.DecimalPlaces = 2;
            numFilterThrottle2.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numFilterThrottle2.Location = new Point(93, 178);
            numFilterThrottle2.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numFilterThrottle2.Name = "numFilterThrottle2";
            numFilterThrottle2.Size = new Size(55, 23);
            numFilterThrottle2.TabIndex = 13;
            numFilterThrottle2.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // numFilterThrottle1
            // 
            numFilterThrottle1.DecimalPlaces = 2;
            numFilterThrottle1.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numFilterThrottle1.Location = new Point(7, 178);
            numFilterThrottle1.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numFilterThrottle1.Name = "numFilterThrottle1";
            numFilterThrottle1.Size = new Size(55, 23);
            numFilterThrottle1.TabIndex = 12;
            numFilterThrottle1.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // lblFilterThrottle
            // 
            lblFilterThrottle.AutoSize = true;
            lblFilterThrottle.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFilterThrottle.Location = new Point(7, 154);
            lblFilterThrottle.Name = "lblFilterThrottle";
            lblFilterThrottle.Size = new Size(86, 14);
            lblFilterThrottle.TabIndex = 11;
            lblFilterThrottle.Text = "Throttle 범위";
            // 
            // lblFilterAnglenum
            // 
            lblFilterAnglenum.AutoSize = true;
            lblFilterAnglenum.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFilterAnglenum.Location = new Point(70, 123);
            lblFilterAnglenum.Name = "lblFilterAnglenum";
            lblFilterAnglenum.Size = new Size(16, 14);
            lblFilterAnglenum.TabIndex = 10;
            lblFilterAnglenum.Text = "~";
            // 
            // numFilterAngle2
            // 
            numFilterAngle2.DecimalPlaces = 2;
            numFilterAngle2.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numFilterAngle2.Location = new Point(93, 118);
            numFilterAngle2.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numFilterAngle2.Name = "numFilterAngle2";
            numFilterAngle2.Size = new Size(55, 23);
            numFilterAngle2.TabIndex = 9;
            numFilterAngle2.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // numFilterAngle1
            // 
            numFilterAngle1.DecimalPlaces = 2;
            numFilterAngle1.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numFilterAngle1.Location = new Point(7, 118);
            numFilterAngle1.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numFilterAngle1.Name = "numFilterAngle1";
            numFilterAngle1.Size = new Size(55, 23);
            numFilterAngle1.TabIndex = 8;
            numFilterAngle1.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // lblFilterAngle
            // 
            lblFilterAngle.AutoSize = true;
            lblFilterAngle.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFilterAngle.Location = new Point(7, 94);
            lblFilterAngle.Name = "lblFilterAngle";
            lblFilterAngle.Size = new Size(111, 14);
            lblFilterAngle.TabIndex = 7;
            lblFilterAngle.Text = "Angle 범위 (Rad)";
            // 
            // chkDisable
            // 
            chkDisable.AutoSize = true;
            chkDisable.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold);
            chkDisable.Location = new Point(7, 64);
            chkDisable.Name = "chkDisable";
            chkDisable.Size = new Size(146, 18);
            chkDisable.TabIndex = 6;
            chkDisable.Text = "기본 반전 이미지 제외";
            chkDisable.UseVisualStyleBackColor = true;
            // 
            // chkThrottle
            // 
            chkThrottle.AutoSize = true;
            chkThrottle.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold);
            chkThrottle.Location = new Point(7, 37);
            chkThrottle.Name = "chkThrottle";
            chkThrottle.Size = new Size(117, 18);
            chkThrottle.TabIndex = 5;
            chkThrottle.Text = "Throttle 0 제외";
            chkThrottle.UseVisualStyleBackColor = true;
            // 
            // lblFilterBasic
            // 
            lblFilterBasic.AutoSize = true;
            lblFilterBasic.Font = new Font("나눔고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFilterBasic.ForeColor = Color.RoyalBlue;
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
            lblFilterSetting.ForeColor = Color.RoyalBlue;
            lblFilterSetting.Location = new Point(9, 12);
            lblFilterSetting.Name = "lblFilterSetting";
            lblFilterSetting.Size = new Size(155, 21);
            lblFilterSetting.TabIndex = 0;
            lblFilterSetting.Text = "필터링 조건 설정";
            // 
            // pnlImageView
            // 
            pnlImageView.BackColor = Color.FromArgb(248, 248, 248);
            pnlImageView.BorderStyle = BorderStyle.FixedSingle;
            pnlImageView.Location = new Point(523, 3);
            pnlImageView.Margin = new Padding(4);
            pnlImageView.Name = "pnlImageView";
            pnlImageView.Size = new Size(649, 431);
            pnlImageView.TabIndex = 0;
            // 
            // pnlFilterResult
            // 
            pnlFilterResult.BackColor = Color.FromArgb(248, 248, 248);
            pnlFilterResult.BorderStyle = BorderStyle.FixedSingle;
            pnlFilterResult.Controls.Add(lblFilterSummary);
            pnlFilterResult.Controls.Add(lstFilterSummary);
            pnlFilterResult.Location = new Point(721, 442);
            pnlFilterResult.Margin = new Padding(4);
            pnlFilterResult.Name = "pnlFilterResult";
            pnlFilterResult.Size = new Size(452, 155);
            pnlFilterResult.TabIndex = 3;
            // 
            // lblFilterSummary
            // 
            lblFilterSummary.AutoSize = true;
            lblFilterSummary.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblFilterSummary.ForeColor = Color.RoyalBlue;
            lblFilterSummary.Location = new Point(12, 8);
            lblFilterSummary.Name = "lblFilterSummary";
            lblFilterSummary.Size = new Size(155, 21);
            lblFilterSummary.TabIndex = 4;
            lblFilterSummary.Text = "필터링 결과 요약";
            // 
            // lstFilterSummary
            // 
            lstFilterSummary.BorderStyle = BorderStyle.FixedSingle;
            lstFilterSummary.FullRowSelect = true;
            lstFilterSummary.GridLines = true;
            lstFilterSummary.HeaderStyle = ColumnHeaderStyle.None;
            lstFilterSummary.HideSelection = true;
            lstFilterSummary.Location = new Point(14, 38);
            lstFilterSummary.Name = "lstFilterSummary";
            lstFilterSummary.Size = new Size(420, 104);
            lstFilterSummary.TabIndex = 0;
            lstFilterSummary.UseCompatibleStateImageBehavior = false;
            lstFilterSummary.View = View.Details;
            // 
            // btnFilterPreview
            // 
            btnFilterPreview.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnFilterPreview.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnFilterPreview.FlatStyle = FlatStyle.Flat;
            btnFilterPreview.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnFilterPreview.ForeColor = SystemColors.Highlight;
            btnFilterPreview.Location = new Point(275, 491);
            btnFilterPreview.Name = "btnFilterPreview";
            btnFilterPreview.Size = new Size(149, 50);
            btnFilterPreview.TabIndex = 4;
            btnFilterPreview.Text = "◎ 필터 미리보기";
            btnFilterPreview.UseVisualStyleBackColor = true;
            // 
            // btnFilterReset
            // 
            btnFilterReset.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnFilterReset.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnFilterReset.FlatStyle = FlatStyle.Flat;
            btnFilterReset.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnFilterReset.ForeColor = SystemColors.Highlight;
            btnFilterReset.Location = new Point(575, 491);
            btnFilterReset.Name = "btnFilterReset";
            btnFilterReset.Size = new Size(139, 50);
            btnFilterReset.TabIndex = 5;
            btnFilterReset.Text = "필터 초기화";
            btnFilterReset.UseVisualStyleBackColor = true;
            // 
            // btnFilterStart
            // 
            btnFilterStart.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnFilterStart.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnFilterStart.FlatStyle = FlatStyle.Flat;
            btnFilterStart.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnFilterStart.ForeColor = SystemColors.Highlight;
            btnFilterStart.Location = new Point(430, 491);
            btnFilterStart.Name = "btnFilterStart";
            btnFilterStart.Size = new Size(139, 50);
            btnFilterStart.TabIndex = 6;
            btnFilterStart.Text = "▷ 필터 적용";
            btnFilterStart.UseVisualStyleBackColor = true;
            // 
            // DataFilterControl
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            Controls.Add(btnFilterStart);
            Controls.Add(btnFilterReset);
            Controls.Add(btnFilterPreview);
            Controls.Add(pnlFilterResult);
            Controls.Add(pnlImageView);
            Controls.Add(pnlFilterSet);
            Controls.Add(pnlFrameList);
            Name = "DataFilterControl";
            Size = new Size(1176, 600);
            pnlFilterSet.ResumeLayout(false);
            pnlFilterSet.PerformLayout();
            pnlFilterAdditional.ResumeLayout(false);
            pnlFilterAdditional.PerformLayout();
            pnlFilterSetBasic.ResumeLayout(false);
            pnlFilterSetBasic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numFilterThrottle2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numFilterThrottle1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numFilterAngle2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numFilterAngle1).EndInit();
            pnlFilterResult.ResumeLayout(false);
            pnlFilterResult.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlFrameList;
        private Panel pnlFilterSet;
        private Panel pnlImageView;
        private Panel pnlFilterResult;
        private Label lblFilterSetting;
        private Panel pnlFilterSetBasic;
        private Panel pnlFilterAdditional;
        private Label lblFilterAdditional;
        private Label lblFilterBasic;
        private CheckBox chkThrottle;
        private Label lblFilterAngle;
        private CheckBox chkDisable;
        private NumericUpDown numFilterAngle1;
        private Label lblFilterAnglenum;
        private NumericUpDown numFilterAngle2;
        private ComboBox comboBox1;
        private Label lblFilterSize;
        private Label lblFilterThrottlenum;
        private NumericUpDown numFilterThrottle2;
        private NumericUpDown numFilterThrottle1;
        private Label lblFilterThrottle;
        private Button btnFilterPreview;
        private Button btnFilterReset;
        private Button btnFilterStart;
        private Label lblFilterSummary;
        private ListView lstFilterSummary;
        private CheckBox chkHighlightDel;
        private CheckBox chkDelFrames;
    }
}
