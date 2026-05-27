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
        private SimpleDonkeyManager.Logger logger;

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

            logger = null;
        }

        /// <summary>
        /// Logger를 설정합니다.
        /// </summary>
        public void SetLogger(SimpleDonkeyManager.Logger log)
        {
            logger = log;
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
            try
            {
                if (frames == null)
                {
                    frameDataList = new List<SimpleDonkeyManager.FrameData>();
                    ShowDefaultMessage();
                    return;
                }

                frameDataList = new List<SimpleDonkeyManager.FrameData>(frames);
                listBoxImages.Items.Clear();

                foreach (var frame in frameDataList)
                {
                    try
                    {
                        if (frame == null)
                            continue;

                        // 단순한 형식으로 표시: "Frame 390"
                        string displayText = $"Frame {frame.FrameNumber}";
                        listBoxImages.Items.Add(displayText);
                    }
                    catch
                    {
                        // 개별 항목 추가 실패 시 계속
                        continue;
                    }
                }

                LogInfo($"{frameDataList.Count}개 프레임 로드 완료");
            }
            catch (Exception ex)
            {
                LogWarning($"프레임 로드 예외: {ex.Message}");
                frameDataList = new List<SimpleDonkeyManager.FrameData>();
                ShowDefaultMessage();
            }
        }

        /// <summary>
        /// 특정 인덱스의 프레임을 선택합니다.
        /// </summary>
        public void SelectFrame(int index)
        {
            try
            {
                if (listBoxImages == null)
                {
                    LogWarning("SelectFrame: listBoxImages가 null입니다");
                    return;
                }

                if (index >= 0 && index < listBoxImages.Items.Count)
                {
                    listBoxImages.SelectedIndex = index;
                }
                else if (index >= 0)
                {
                    LogWarning($"SelectFrame: 인덱스 {index}가 범위를 벗어났습니다 (총 {listBoxImages.Items.Count}개)");
                }
            }
            catch (Exception ex)
            {
                LogWarning($"프레임 선택 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 프레임 순번으로 검색하여 선택합니다.
        /// </summary>
        private void SearchAndSelectFrame()
        {
            try
            {
                if (frameDataList == null || frameDataList.Count == 0)
                {
                    MessageBox.Show("로드된 프레임 데이터가 없습니다.", "검색 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogWarning("프레임 검색 실패: 프레임 데이터 없음");
                    return;
                }

                string searchText = txtFrameSearch?.Text?.Trim();

                if (string.IsNullOrEmpty(searchText))
                {
                    MessageBox.Show("프레임 순번을 입력하세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 정수 파싱 시도
                if (!int.TryParse(searchText, out int frameNumber))
                {
                    MessageBox.Show("유효한 프레임 순번을 입력하세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogWarning($"프레임 검색 실패: 잘못된 입력 '{searchText}'");
                    return;
                }

                // 해당 프레임 순번 찾기
                var matchingFrames = frameDataList.Where(f => f != null && f.FrameNumber == frameNumber).ToList();

                if (matchingFrames.Count == 0)
                {
                    MessageBox.Show($"프레임 {frameNumber}을(를) 찾을 수 없습니다.", "검색 결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogWarning($"프레임 검색 실패: 프레임 {frameNumber} 없음");
                    return;
                }

                if (matchingFrames.Count == 1)
                {
                    // 유일한 매치 - 선택
                    try
                    {
                        int index = frameDataList.IndexOf(matchingFrames[0]);
                        if (index >= 0)
                        {
                            SelectFrame(index);
                            if (txtFrameSearch != null)
                            {
                                txtFrameSearch.Clear();
                            }
                            LogInfo($"프레임 {frameNumber} 검색 및 선택 완료");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"프레임 선택 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LogWarning($"프레임 선택 오류: {ex.Message}");
                    }
                }
                else
                {
                    // 여러 개 매치 - 선택 안 함
                    MessageBox.Show($"프레임 {frameNumber}과 일치하는 항목이 {matchingFrames.Count}개 있습니다.\n선택은 불가능합니다.", 
                        "검색 결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogWarning($"프레임 검색 실패: 프레임 {frameNumber} 중복 ({matchingFrames.Count}개)");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"검색 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"프레임 검색 예외: {ex.Message}");
            }
        }

        private void BtnFrameSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchAndSelectFrame();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"검색 버튼 클릭 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWarning($"검색 버튼 클릭 예외: {ex.Message}");
            }
        }

        private void TxtFrameSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    SearchAndSelectFrame();
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                LogWarning($"키다운 이벤트 예외: {ex.Message}");
                e.Handled = true;
            }
        }

        private void listBoxImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBoxImages == null)
                    return;

                int selectedIndex = listBoxImages.SelectedIndex;

                if (selectedIndex >= 0 && selectedIndex < frameDataList.Count)
                {
                    var selectedFrame = frameDataList[selectedIndex];
                    if (selectedFrame != null && !string.IsNullOrEmpty(selectedFrame.ImagePath))
                    {
                        try
                        {
                            ImageSelected?.Invoke(this, selectedFrame.ImagePath);
                        }
                        catch (Exception ex)
                        {
                            LogWarning($"ImageSelected 이벤트 처리 오류: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogWarning($"선택 인덱스 변경 예외: {ex.Message}");
            }
        }

        /// <summary>
        /// 정보 로그를 기록합니다.
        /// </summary>
        private void LogInfo(string message)
        {
            if (logger != null)
            {
                logger.AppendLog($"[이미지리스트] {message}");
            }
        }

        /// <summary>
        /// 경고 로그를 기록합니다.
        /// </summary>
        private void LogWarning(string message)
        {
            if (logger != null)
            {
                logger.AppendLog($"[이미지리스트 경고] {message}");
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


