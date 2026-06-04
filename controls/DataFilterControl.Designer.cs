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
            lstFilterSummary = new ListView();
            btnFilterReset = new Button();
            btnFilterStart = new Button();
            panelButtons = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            pnlFilterSetBasic = new Panel();
            btnUndoRemove = new Button();
            btnRemoveSelectedFrame = new Button();
            btnFilterUnselected = new Button();
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
            pnlImageView = new Panel();
            tableLayoutPanel4 = new TableLayoutPanel();
            pnlFilterResult = new GroupBox();
            pnlFrameList = new Panel();
            panelButtons.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            groupBox1.SuspendLayout();
            pnlFilterSetBasic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numFilterThrottle2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numFilterThrottle1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numFilterAngle2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numFilterAngle1).BeginInit();
            tableLayoutPanel4.SuspendLayout();
            pnlFilterResult.SuspendLayout();
            SuspendLayout();
            // 
            // lstFilterSummary
            // 
            lstFilterSummary.BorderStyle = BorderStyle.FixedSingle;
            lstFilterSummary.Dock = DockStyle.Fill;
            lstFilterSummary.FullRowSelect = true;
            lstFilterSummary.GridLines = true;
            lstFilterSummary.HeaderStyle = ColumnHeaderStyle.None;
            lstFilterSummary.HideSelection = true;
            lstFilterSummary.Location = new Point(3, 25);
            lstFilterSummary.Name = "lstFilterSummary";
            lstFilterSummary.Size = new Size(882, 139);
            lstFilterSummary.TabIndex = 0;
            lstFilterSummary.UseCompatibleStateImageBehavior = false;
            lstFilterSummary.View = View.Details;
            lstFilterSummary.SelectedIndexChanged += lstFilterSummary_SelectedIndexChanged;
            // 
            // btnFilterReset
            // 
            btnFilterReset.Dock = DockStyle.Fill;
            btnFilterReset.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnFilterReset.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnFilterReset.FlatStyle = FlatStyle.Flat;
            btnFilterReset.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnFilterReset.ForeColor = SystemColors.Highlight;
            btnFilterReset.Location = new Point(785, 3);
            btnFilterReset.Name = "btnFilterReset";
            btnFilterReset.Size = new Size(388, 44);
            btnFilterReset.TabIndex = 12;
            btnFilterReset.Text = "필터 초기화";
            btnFilterReset.UseVisualStyleBackColor = true;
            // 
            // btnFilterStart
            // 
            btnFilterStart.Dock = DockStyle.Fill;
            btnFilterStart.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnFilterStart.FlatAppearance.MouseOverBackColor = Color.Azure;
            btnFilterStart.FlatStyle = FlatStyle.Flat;
            btnFilterStart.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnFilterStart.ForeColor = SystemColors.Highlight;
            btnFilterStart.Location = new Point(394, 3);
            btnFilterStart.Name = "btnFilterStart";
            btnFilterStart.Size = new Size(385, 44);
            btnFilterStart.TabIndex = 13;
            btnFilterStart.Text = "▷ 필터 적용";
            btnFilterStart.UseVisualStyleBackColor = true;
            // 
            // panelButtons
            // 
            panelButtons.ColumnCount = 2;
            panelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panelButtons.Controls.Add(btnFilterStart, 0, 0);
            panelButtons.Controls.Add(btnFilterReset, 1, 0);
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Location = new Point(0, 550);
            panelButtons.Name = "panelButtons";
            panelButtons.RowCount = 1;
            panelButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            panelButtons.Size = new Size(1176, 50);
            panelButtons.TabIndex = 15;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 77F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
            tableLayoutPanel1.Controls.Add(pnlFrameList, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1176, 600);
            tableLayoutPanel1.TabIndex = 14;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(273, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel2.Size = new Size(900, 594);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73F));
            tableLayoutPanel3.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel3.Controls.Add(pnlImageView, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(894, 409);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.FromArgb(248, 248, 248);
            groupBox1.Controls.Add(pnlFilterSetBasic);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold);
            groupBox1.ForeColor = Color.RoyalBlue;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(235, 403);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "필터링 조건 설정";
            // 
            // pnlFilterSetBasic
            // 
            pnlFilterSetBasic.BackColor = Color.FromArgb(242, 242, 242);
            pnlFilterSetBasic.BorderStyle = BorderStyle.FixedSingle;
            pnlFilterSetBasic.Controls.Add(btnFilterUnselected);
            pnlFilterSetBasic.Controls.Add(btnUndoRemove);
            pnlFilterSetBasic.Controls.Add(btnRemoveSelectedFrame);
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
            pnlFilterSetBasic.Dock = DockStyle.Fill;
            pnlFilterSetBasic.ForeColor = SystemColors.ControlText;
            pnlFilterSetBasic.Location = new Point(3, 25);
            pnlFilterSetBasic.Name = "pnlFilterSetBasic";
            pnlFilterSetBasic.Size = new Size(229, 375);
            pnlFilterSetBasic.TabIndex = 1;
            // 
            // btnRemoveSelectedFrame
            // 
            btnRemoveSelectedFrame.BackColor = Color.White;
            btnRemoveSelectedFrame.FlatAppearance.BorderColor = Color.IndianRed;
            btnRemoveSelectedFrame.FlatAppearance.MouseOverBackColor = Color.MistyRose;
            btnRemoveSelectedFrame.FlatStyle = FlatStyle.Flat;
            btnRemoveSelectedFrame.Font = new Font("나눔고딕", 9F, FontStyle.Bold);
            btnRemoveSelectedFrame.ForeColor = Color.IndianRed;
            btnRemoveSelectedFrame.Location = new Point(7, 268);
            btnRemoveSelectedFrame.Name = "btnRemoveSelectedFrame";
            btnRemoveSelectedFrame.Size = new Size(215, 32);
            btnRemoveSelectedFrame.TabIndex = 17;
            btnRemoveSelectedFrame.Text = "✖ 선택 프레임 제거";
            btnRemoveSelectedFrame.UseVisualStyleBackColor = false;
            // 
            // btnUndoRemove
            // 
            btnUndoRemove.BackColor = Color.White;
            btnUndoRemove.Enabled = false;
            btnUndoRemove.FlatAppearance.BorderColor = Color.SeaGreen;
            btnUndoRemove.FlatAppearance.MouseOverBackColor = Color.Honeydew;
            btnUndoRemove.FlatStyle = FlatStyle.Flat;
            btnUndoRemove.Font = new Font("나눔고딕", 9F, FontStyle.Bold);
            btnUndoRemove.ForeColor = Color.SeaGreen;
            btnUndoRemove.Location = new Point(7, 305);
            btnUndoRemove.Name = "btnUndoRemove";
            btnUndoRemove.Size = new Size(215, 32);
            btnUndoRemove.TabIndex = 18;
            btnUndoRemove.Text = "↺ 이전 삭제 되돌리기";
            btnUndoRemove.UseVisualStyleBackColor = false;
            // 
            // btnFilterUnselected
            // 
            btnFilterUnselected.BackColor = Color.White;
            btnFilterUnselected.FlatAppearance.BorderColor = Color.RoyalBlue;
            btnFilterUnselected.FlatAppearance.MouseOverBackColor = Color.AliceBlue;
            btnFilterUnselected.FlatStyle = FlatStyle.Flat;
            btnFilterUnselected.Font = new Font("나눔고딕", 9F, FontStyle.Bold);
            btnFilterUnselected.ForeColor = Color.RoyalBlue;
            btnFilterUnselected.Location = new Point(7, 342);
            btnFilterUnselected.Name = "btnFilterUnselected";
            btnFilterUnselected.Size = new Size(215, 32);
            btnFilterUnselected.TabIndex = 19;
            btnFilterUnselected.Text = "✂ 미선택 프레임 필터";
            btnFilterUnselected.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("맑은 고딕", 9F);
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
            numFilterThrottle2.Font = new Font("맑은 고딕", 9F);
            numFilterThrottle2.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numFilterThrottle2.Location = new Point(93, 178);
            numFilterThrottle2.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numFilterThrottle2.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numFilterThrottle2.Name = "numFilterThrottle2";
            numFilterThrottle2.Size = new Size(55, 23);
            numFilterThrottle2.TabIndex = 13;
            numFilterThrottle2.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numFilterThrottle1
            // 
            numFilterThrottle1.DecimalPlaces = 2;
            numFilterThrottle1.Font = new Font("맑은 고딕", 9F);
            numFilterThrottle1.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numFilterThrottle1.Location = new Point(7, 178);
            numFilterThrottle1.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
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
            numFilterAngle2.Font = new Font("맑은 고딕", 9F);
            numFilterAngle2.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numFilterAngle2.Location = new Point(93, 118);
            numFilterAngle2.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numFilterAngle2.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numFilterAngle2.Name = "numFilterAngle2";
            numFilterAngle2.Size = new Size(55, 23);
            numFilterAngle2.TabIndex = 9;
            numFilterAngle2.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numFilterAngle2.ValueChanged += numFilterAngle2_ValueChanged;
            // 
            // numFilterAngle1
            // 
            numFilterAngle1.DecimalPlaces = 2;
            numFilterAngle1.Font = new Font("맑은 고딕", 9F);
            numFilterAngle1.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numFilterAngle1.Location = new Point(7, 118);
            numFilterAngle1.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
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
            chkThrottle.CheckedChanged += chkThrottle_CheckedChanged;
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
            // pnlImageView
            // 
            pnlImageView.BackColor = Color.FromArgb(248, 248, 248);
            pnlImageView.Dock = DockStyle.Fill;
            pnlImageView.Location = new Point(244, 3);
            pnlImageView.Name = "pnlImageView";
            pnlImageView.Size = new Size(647, 403);
            pnlImageView.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(pnlFilterResult, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 418);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(894, 173);
            tableLayoutPanel4.TabIndex = 1;
            // 
            // pnlFilterResult
            // 
            pnlFilterResult.BackColor = Color.FromArgb(248, 248, 248);
            pnlFilterResult.Controls.Add(lstFilterSummary);
            pnlFilterResult.Dock = DockStyle.Fill;
            pnlFilterResult.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold);
            pnlFilterResult.ForeColor = Color.RoyalBlue;
            pnlFilterResult.Location = new Point(3, 3);
            pnlFilterResult.Name = "pnlFilterResult";
            pnlFilterResult.Size = new Size(888, 167);
            pnlFilterResult.TabIndex = 0;
            pnlFilterResult.TabStop = false;
            pnlFilterResult.Text = "필터링 결과 요약";
            // 
            // pnlFrameList
            // 
            pnlFrameList.BackColor = Color.FromArgb(248, 248, 248);
            pnlFrameList.Dock = DockStyle.Fill;
            pnlFrameList.Location = new Point(3, 3);
            pnlFrameList.Name = "pnlFrameList";
            pnlFrameList.Size = new Size(264, 594);
            pnlFrameList.TabIndex = 1;
            // 
            // DataFilterControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            Controls.Add(panelButtons);
            Controls.Add(tableLayoutPanel1);
            Name = "DataFilterControl";
            Size = new Size(1176, 600);
            panelButtons.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            pnlFilterSetBasic.ResumeLayout(false);
            pnlFilterSetBasic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numFilterThrottle2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numFilterThrottle1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numFilterAngle2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numFilterAngle1).EndInit();
            tableLayoutPanel4.ResumeLayout(false);
            pnlFilterResult.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ListView lstFilterSummary;
        private Button btnFilterReset;
        private Button btnFilterStart;
        private TableLayoutPanel panelButtons;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel4;
        private GroupBox pnlFilterResult;
        private Panel pnlFilterSetBasic;
        private ComboBox comboBox1;
        private Button btnRemoveSelectedFrame;
        private Button btnUndoRemove;
        private Button btnFilterUnselected;
        private Label lblFilterSize;
        private Label lblFilterThrottlenum;
        private NumericUpDown numFilterThrottle2;
        private NumericUpDown numFilterThrottle1;
        private Label lblFilterThrottle;
        private Label lblFilterAnglenum;
        private NumericUpDown numFilterAngle2;
        private NumericUpDown numFilterAngle1;
        private Label lblFilterAngle;
        private CheckBox chkDisable;
        private CheckBox chkThrottle;
        private Label lblFilterBasic;
        private Panel pnlImageView;
        private Panel pnlFrameList;
    }
}
