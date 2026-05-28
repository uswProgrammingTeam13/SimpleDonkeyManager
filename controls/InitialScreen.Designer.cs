namespace SimpleDonkeyManager.controls
{
    partial class InitialScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitialScreen));
            btnAutoSetupEnvironment = new Button();
            SuspendLayout();
            // 
            // btnAutoSetupEnvironment
            // 
            btnAutoSetupEnvironment.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAutoSetupEnvironment.BackColor = Color.FromArgb(41, 128, 185);
            btnAutoSetupEnvironment.FlatStyle = FlatStyle.Flat;
            btnAutoSetupEnvironment.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAutoSetupEnvironment.ForeColor = Color.White;
            btnAutoSetupEnvironment.Location = new Point(1020, 15);
            btnAutoSetupEnvironment.Name = "btnAutoSetupEnvironment";
            btnAutoSetupEnvironment.Size = new Size(141, 32);
            btnAutoSetupEnvironment.TabIndex = 0;
            btnAutoSetupEnvironment.Text = "⚙ 자동 환경 설정";
            btnAutoSetupEnvironment.UseVisualStyleBackColor = false;
            btnAutoSetupEnvironment.Click += BtnAutoSetupEnvironment_Click;
            // 
            // InitialScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(btnAutoSetupEnvironment);
            Name = "InitialScreen";
            Size = new Size(1176, 600);
            ResumeLayout(false);
        }

        #endregion

        private Button btnAutoSetupEnvironment;
    }
}
