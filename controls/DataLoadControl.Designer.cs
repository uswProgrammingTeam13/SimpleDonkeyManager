namespace SimpleDonkeyManager
{
    partial class DataLoadControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataLoadControl));
            label1 = new Label();
            imgListPan = new Panel();
            mainPan = new Panel();
            btnAftPic = new Button();
            btnPrePic = new Button();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            lblImage = new Label();
            panelBottomInfo = new Panel();
            lblSummary = new Label();
            lblTotalImages = new Label();
            lblFolderPath = new Label();
            lblImageFormat = new Label();
            lblResolution = new Label();
            lblFileSize = new Label();
            btnRefresh = new Button();
            btnData = new Button();
            mainPan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelBottomInfo.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2266, 1229);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(115, 32);
            label1.TabIndex = 0;
            label1.Text = "DataLoad";
            // 
            // imgListPan
            // 
            imgListPan.Location = new Point(17, 29);
            imgListPan.Name = "imgListPan";
            imgListPan.Size = new Size(438, 849);
            imgListPan.TabIndex = 1;
            // 
            // mainPan
            // 
            mainPan.Controls.Add(btnAftPic);
            mainPan.Controls.Add(btnPrePic);
            mainPan.Controls.Add(pictureBox4);
            mainPan.Controls.Add(pictureBox3);
            mainPan.Controls.Add(pictureBox2);
            mainPan.Controls.Add(pictureBox1);
            mainPan.Controls.Add(lblImage);
            mainPan.Location = new Point(489, 29);
            mainPan.Name = "mainPan";
            mainPan.Size = new Size(1353, 849);
            mainPan.TabIndex = 0;
            // 
            // btnAftPic
            // 
            btnAftPic.Location = new Point(1255, 650);
            btnAftPic.Name = "btnAftPic";
            btnAftPic.Size = new Size(62, 46);
            btnAftPic.TabIndex = 7;
            btnAftPic.Text = ">";
            btnAftPic.UseVisualStyleBackColor = true;
            // 
            // btnPrePic
            // 
            btnPrePic.Location = new Point(51, 650);
            btnPrePic.Name = "btnPrePic";
            btnPrePic.Size = new Size(62, 46);
            btnPrePic.TabIndex = 6;
            btnPrePic.Text = "<";
            btnPrePic.UseVisualStyleBackColor = true;
            // 
            // pictureBox4
            // 
            pictureBox4.Location = new Point(889, 570);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(342, 185);
            pictureBox4.TabIndex = 5;
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(516, 570);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(342, 185);
            pictureBox3.TabIndex = 4;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(144, 570);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(342, 185);
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(34, 98);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1299, 390);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.Location = new Point(34, 35);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(190, 32);
            lblImage.TabIndex = 1;
            lblImage.Text = "이미지 미리보기";
            // 
            // panelBottomInfo
            // 
            panelBottomInfo.Controls.Add(btnData);
            panelBottomInfo.Controls.Add(btnRefresh);
            panelBottomInfo.Controls.Add(lblFileSize);
            panelBottomInfo.Controls.Add(lblResolution);
            panelBottomInfo.Controls.Add(lblImageFormat);
            panelBottomInfo.Controls.Add(lblFolderPath);
            panelBottomInfo.Controls.Add(lblTotalImages);
            panelBottomInfo.Controls.Add(lblSummary);
            panelBottomInfo.Location = new Point(17, 898);
            panelBottomInfo.Name = "panelBottomInfo";
            panelBottomInfo.Size = new Size(1834, 284);
            panelBottomInfo.TabIndex = 1;
            // 
            // lblSummary
            // 
            lblSummary.AutoSize = true;
            lblSummary.Location = new Point(41, 26);
            lblSummary.Name = "lblSummary";
            lblSummary.Size = new Size(174, 32);
            lblSummary.TabIndex = 0;
            lblSummary.Text = "로드 요약 정보";
            // 
            // lblTotalImages
            // 
            lblTotalImages.AutoSize = true;
            lblTotalImages.Location = new Point(102, 83);
            lblTotalImages.Name = "lblTotalImages";
            lblTotalImages.Size = new Size(134, 32);
            lblTotalImages.TabIndex = 1;
            lblTotalImages.Text = "전체이미지";
            lblTotalImages.Click += label2_Click;
            // 
            // lblFolderPath
            // 
            lblFolderPath.AutoSize = true;
            lblFolderPath.Location = new Point(405, 83);
            lblFolderPath.Name = "lblFolderPath";
            lblFolderPath.Size = new Size(142, 32);
            lblFolderPath.TabIndex = 2;
            lblFolderPath.Text = "선택된 폴더";
            // 
            // lblImageFormat
            // 
            lblImageFormat.AutoSize = true;
            lblImageFormat.Location = new Point(659, 83);
            lblImageFormat.Name = "lblImageFormat";
            lblImageFormat.Size = new Size(142, 32);
            lblImageFormat.TabIndex = 3;
            lblImageFormat.Text = "이미지 형식";
            // 
            // lblResolution
            // 
            lblResolution.AutoSize = true;
            lblResolution.Location = new Point(928, 83);
            lblResolution.Name = "lblResolution";
            lblResolution.Size = new Size(86, 32);
            lblResolution.TabIndex = 4;
            lblResolution.Text = "해상도";
            // 
            // lblFileSize
            // 
            lblFileSize.AutoSize = true;
            lblFileSize.Location = new Point(1147, 83);
            lblFileSize.Name = "lblFileSize";
            lblFileSize.Size = new Size(118, 32);
            lblFileSize.TabIndex = 5;
            lblFileSize.Text = "파일 크기";
            lblFileSize.Click += label6_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Image = (Image)resources.GetObject("btnRefresh.Image");
            btnRefresh.ImageAlign = ContentAlignment.TopCenter;
            btnRefresh.Location = new Point(1361, 66);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(184, 115);
            btnRefresh.TabIndex = 6;
            btnRefresh.Text = "새로고침";
            btnRefresh.TextAlign = ContentAlignment.BottomCenter;
            btnRefresh.TextImageRelation = TextImageRelation.ImageAboveText;
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnData
            // 
            btnData.Location = new Point(1560, 66);
            btnData.Name = "btnData";
            btnData.Size = new Size(213, 115);
            btnData.TabIndex = 7;
            btnData.Text = "데이터 로드 시작";
            btnData.UseVisualStyleBackColor = true;
            // 
            // DataLoadControl
            // 
            AutoScaleDimensions = new SizeF(14F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelBottomInfo);
            Controls.Add(mainPan);
            Controls.Add(imgListPan);
            Controls.Add(label1);
            Margin = new Padding(6);
            Name = "DataLoadControl";
            Size = new Size(2352, 1280);
            Load += DataLoadControl_Load;
            mainPan.ResumeLayout(false);
            mainPan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelBottomInfo.ResumeLayout(false);
            panelBottomInfo.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel imgListPan;
        private Panel mainPan;
        private Panel panelBottomInfo;
        private Label lblSummary;
        private Label lblImage;
        private Button btnAftPic;
        private Button btnPrePic;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Label lblFileSize;
        private Label lblResolution;
        private Label lblImageFormat;
        private Label lblFolderPath;
        private Label lblTotalImages;
        private Button btnData;
        private Button btnRefresh;
    }
}
