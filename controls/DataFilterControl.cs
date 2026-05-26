using SimpleDonkeyManager.controls;
using SimpleDonkeyManager.controlutils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ImageList = System.Windows.Forms.ImageList;

namespace SimpleDonkeyManager
{
    public partial class DataFilterControl : UserControl
    {
        private controlutils.ImageList imageList = new controlutils.ImageList();
        private controlutils.ImageViewer imageViewer = new controlutils.ImageViewer();
        private ImageManager imageManager;
        private List<FrameData> filteredFrameDataList = new List<FrameData>();

        public DataFilterControl()
        {
            InitializeComponent();

            lstFilterSummary.Columns.Clear();
            lstFilterSummary.Columns.Add("항목", 200);
            lstFilterSummary.Columns.Add("값", 220);

            ImageList rowHeight = new ImageList();
            rowHeight.ImageSize = new Size(1, 25);
            lstFilterSummary.SmallImageList = rowHeight;

            lstFilterSummary.Font = new Font("나눔고딕", 10F);

            SetSummaryData("0", "0", "0 (0.0%)", "0.0%");

            // ImageList 설정
            imageList.Dock = DockStyle.Fill;
            imageList.AutoSize = false;
            imageList.Visible = true;
            pnlFrameList.Controls.Clear();
            pnlFrameList.Controls.Add(imageList);

            // ImageViewer 설정
            imageViewer.Dock = DockStyle.Fill;
            imageViewer.AutoSize = false;
            imageViewer.Visible = true;
            pnlImageView.Controls.Clear();
            pnlImageView.Controls.Add(imageViewer);

            // 이미지 선택 이벤트 구독
            imageList.ImageSelected += ImageList_ImageSelected;

            // 버튼 이벤트
            btnFilterStart.Click += BtnFilterStart_Click;
            btnFilterPreview.Click += BtnFilterPreview_Click;
            btnFilterReset.Click += BtnFilterReset_Click;
        }

        private void ImageList_ImageSelected(object sender, string imagePath)
        {
            imageViewer.DisplayImage(imagePath);
        }

        /// <summary>
        /// DataLoadControl에서 데이터를 받아옵니다.
        /// </summary>
        public void SetFrameData(ImageManager manager, List<FrameData> frameDataList)
        {
            this.imageManager = manager;
            this.filteredFrameDataList = new List<FrameData>(frameDataList);

            // ImageList와 ImageViewer에 데이터 설정
            imageList.SetImageManager(manager);
            imageList.LoadFrames(frameDataList);

            imageViewer.SetImageManager(manager);

            // 첫 번째 프레임 선택
            if (frameDataList.Count > 0)
            {
                imageList.SelectFrame(0);
            }

            // 통계 업데이트
            UpdateStatistics();
        }

        /// <summary>
        /// 필터링된 데이터로 통계를 업데이트합니다.
        /// </summary>
        private void UpdateStatistics()
        {
            if (imageManager == null || filteredFrameDataList.Count == 0)
            {
                SetSummaryData("0", "0", "0 (0.0%)", "0.0%");
                return;
            }

            int totalFrames = imageManager.GetAllFrameData().Count;
            int filteredFrames = filteredFrameDataList.Count;
            int deletedFrames = totalFrames - filteredFrames;
            double activeRatio = (double)filteredFrames / totalFrames * 100;

            SetSummaryData(
                totalFrames.ToString("N0"),
                filteredFrames.ToString("N0"),
                $"{deletedFrames} ({(double)deletedFrames / totalFrames * 100:F1}%)",
                $"{activeRatio:F1}%"
            );
        }

        private void SetSummaryData(string frame, string filterframe, string delframe, string activeframe)
        {
            lstFilterSummary.Items.Clear();

            AddSummaryRow("총 프레임 수", frame);
            AddSummaryRow("필터링 후 프레임 수", filterframe);
            AddSummaryRow("제거된 프레임 수", delframe);
            AddSummaryRow("활성 프레임 비율", activeframe);
        }

        private void AddSummaryRow(string title, string value)
        {
            ListViewItem item = new ListViewItem(title);
            item.SubItems.Add(value);
            lstFilterSummary.Items.Add(item);
        }

        private void BtnFilterStart_Click(object sender, EventArgs e)
        {
            // 필터 적용 - 필터링된 데이터로 학습 진행
            if (filteredFrameDataList.Count == 0)
            {
                MessageBox.Show("필터링된 데이터가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"{filteredFrameDataList.Count}개의 프레임으로 학습을 시작합니다.", "학습 시작", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // TODO: Python 학습 코드 연동
        }

        private void BtnFilterPreview_Click(object sender, EventArgs e)
        {
            // 필터 미리보기
            UpdateStatistics();
            MessageBox.Show("필터가 적용되었습니다.", "미리보기", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnFilterReset_Click(object sender, EventArgs e)
        {
            // 필터 초기화
            if (imageManager != null)
            {
                filteredFrameDataList = new List<FrameData>(imageManager.GetAllFrameData());
                imageList.LoadFrames(filteredFrameDataList);
                UpdateStatistics();
                MessageBox.Show("필터가 초기화되었습니다.", "초기화", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lstFilterSummary_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void DisplayImage(string imagePath)
        {
            if (imageViewer != null)
            {
                imageViewer.DisplayImage(imagePath);
            }
        }
    }
}
