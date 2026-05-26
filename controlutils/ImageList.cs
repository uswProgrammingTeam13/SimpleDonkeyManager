using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace SimpleDonkeyManager.controlutils
{
    public partial class ImageList : UserControl
    {
        private List<SimpleDonkeyManager.FrameData> frameDataList = new List<SimpleDonkeyManager.FrameData>();
        private SimpleDonkeyManager.ImageManager imageManager;

        public ImageList()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.AutoSize = false;

            listBoxImages.SelectedIndexChanged += listBoxImages_SelectedIndexChanged;
            btnFrameSearch.Click += BtnFrameSearch_Click;
            txtFrameSearch.KeyDown += TxtFrameSearch_KeyDown;

            // 초기 상태 - 데이터 폴더 로드 필요 표시
            ShowDefaultMessage();
        }

        // 이미지 선택 이벤트 정의
        public event EventHandler<string> ImageSelected;

        /// <summary>
        /// 초기 상태 메시지 표시
        /// </summary>
        private void ShowDefaultMessage()
        {
            listBoxImages.Items.Clear();
            listBoxImages.Items.Add("데이터 폴더 로드 필요");
        }

        /// <summary>
        /// ImageManager를 설정합니다.
        /// </summary>
        public void SetImageManager(SimpleDonkeyManager.ImageManager manager)
        {
            imageManager = manager;
        }

        /// <summary>
        /// 레거시 호환성을 위한 메서드 - 경로 배열로 로드
        /// </summary>
        public void LoadImages(string[] imageFiles)
        {
            listBoxImages.Items.Clear();
            foreach (string file in imageFiles)
            {
                listBoxImages.Items.Add(file);
            }
        }

        /// <summary>
        /// 프레임 데이터 목록을 로드합니다.
        /// </summary>
        public void LoadFrames(List<SimpleDonkeyManager.FrameData> frames)
        {
            frameDataList = new List<SimpleDonkeyManager.FrameData>(frames);
            listBoxImages.Items.Clear();

            foreach (var frame in frameDataList)
            {
                // 단순한 형식으로 표시: "Frame 390"
                string displayText = $"Frame {frame.FrameNumber}";
                listBoxImages.Items.Add(displayText);
            }
        }

        /// <summary>
        /// 특정 인덱스의 프레임을 선택합니다.
        /// </summary>
        public void SelectFrame(int index)
        {
            if (index >= 0 && index < listBoxImages.Items.Count)
            {
                listBoxImages.SelectedIndex = index;
            }
        }

        /// <summary>
        /// 프레임 순번으로 검색하여 선택합니다.
        /// </summary>
        private void SearchAndSelectFrame()
        {
            string searchText = txtFrameSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("프레임 순번을 입력하세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 정수 파싱 시도
            if (!int.TryParse(searchText, out int frameNumber))
            {
                MessageBox.Show("유효한 프레임 순번을 입력하세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 해당 프레임 순번 찾기
            var matchingFrames = frameDataList.Where(f => f.FrameNumber == frameNumber).ToList();

            if (matchingFrames.Count == 0)
            {
                MessageBox.Show($"프레임 {frameNumber}을(를) 찾을 수 없습니다.", "검색 결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (matchingFrames.Count == 1)
            {
                // 유일한 매치 - 선택
                int index = frameDataList.IndexOf(matchingFrames[0]);
                SelectFrame(index);
                txtFrameSearch.Clear();
            }
            else
            {
                // 여러 개 매치 - 선택 안 함
                MessageBox.Show($"프레임 {frameNumber}과 일치하는 항목이 {matchingFrames.Count}개 있습니다.\n선택은 불가능합니다.", 
                    "검색 결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnFrameSearch_Click(object sender, EventArgs e)
        {
            SearchAndSelectFrame();
        }

        private void TxtFrameSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SearchAndSelectFrame();
                e.Handled = true;
            }
        }

        private void listBoxImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxImages.SelectedIndex >= 0 && listBoxImages.SelectedIndex < frameDataList.Count)
            {
                var selectedFrame = frameDataList[listBoxImages.SelectedIndex];
                ImageSelected?.Invoke(this, selectedFrame.ImagePath);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void ImageList_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void ImageList_Load(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}


