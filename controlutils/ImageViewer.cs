using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SimpleDonkeyManager.controlutils
{
    public partial class ImageViewer : UserControl
    {
        private SimpleDonkeyManager.ImageManager imageManager;
        private List<SimpleDonkeyManager.FrameData> frameDataList = new List<SimpleDonkeyManager.FrameData>();
        private int currentFrameIndex = 0;
        private System.Windows.Forms.Timer playTimer;
        private bool isPlaying = false;
        private double playbackSpeed = 1.0;
        private const int FRAMES_PER_SECOND = 20;

        public ImageViewer()
        {
            InitializeComponent();

            // pictureBox1은 이미 Designer에서 pnlCenterMain에 추가됨
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            InitializeListView();

            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
            button4.Click += Button4_Click;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            trackBar1.ValueChanged += TrackBar1_ValueChanged;

            // 리사이즈 이벤트 추가 - 썸네일 적응형 표시
            this.Resize += ImageViewer_Resize;
            pnlLeftThumbnail.Resize += PnlThumbnail_Resize;
            pnlRightThumbnail.Resize += PnlThumbnail_Resize;

            playTimer = new System.Windows.Forms.Timer();
            playTimer.Interval = (int)(1000.0 / (FRAMES_PER_SECOND * playbackSpeed));
            playTimer.Tick += PlayTimer_Tick;

            UpdateCurrentFrameDisplay();
        }

        private void InitializeListView()
        {
            lstJSONSummary.Columns.Clear();
            lstJSONSummary.Columns.Add("항목", 80);
            lstJSONSummary.Columns.Add("값", 120);
        }

        public void SetImageManager(SimpleDonkeyManager.ImageManager manager)
        {
            imageManager = manager;
            frameDataList = new List<SimpleDonkeyManager.FrameData>(imageManager.GetAllFrameData());

            if (frameDataList.Count > 0)
            {
                trackBar1.Maximum = frameDataList.Count - 1;
                currentFrameIndex = 0;
                UpdateCurrentFrameDisplay();
            }
        }

        public void DisplayImage(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                for (int i = 0; i < frameDataList.Count; i++)
                {
                    if (frameDataList[i].ImagePath == imagePath)
                    {
                        currentFrameIndex = i;
                        break;
                    }
                }

                DisplayFrameAtIndex(currentFrameIndex);
            }
        }

        private void DisplayFrameAtIndex(int index)
        {
            if (index < 0 || index >= frameDataList.Count)
                return;

            currentFrameIndex = index;
            var frameData = frameDataList[index];

            if (File.Exists(frameData.ImagePath))
            {
                if (pictureBox1.Image != null)
                {
                    var oldImage = pictureBox1.Image;
                    pictureBox1.Image = null;
                    oldImage.Dispose();
                }

                using (var stream = new FileStream(frameData.ImagePath, FileMode.Open, FileAccess.Read))
                {
                    pictureBox1.Image = Image.FromStream(stream);
                }
            }

            DisplayThumbnails();
            UpdateJSONInfo(frameData);
            UpdateCurrentFrameDisplay();
            trackBar1.Value = index;
        }

        private void DisplayThumbnails()
        {
            if (currentFrameIndex > 0)
            {
                var prevFrame = frameDataList[currentFrameIndex - 1];
                if (File.Exists(prevFrame.ImagePath))
                {
                    if (pictureBox2.Image != null)
                    {
                        pictureBox2.Image.Dispose();
                    }
                    pictureBox2.Image = Image.FromFile(prevFrame.ImagePath);
                }
            }
            else
            {
                pictureBox2.Image = null;
            }

            if (currentFrameIndex < frameDataList.Count - 1)
            {
                var nextFrame = frameDataList[currentFrameIndex + 1];
                if (File.Exists(nextFrame.ImagePath))
                {
                    if (pictureBox3.Image != null)
                    {
                        pictureBox3.Image.Dispose();
                    }
                    pictureBox3.Image = Image.FromFile(nextFrame.ImagePath);
                }
            }
            else
            {
                pictureBox3.Image = null;
            }
        }

        private void UpdateJSONInfo(SimpleDonkeyManager.FrameData frameData)
        {
            lstJSONSummary.Items.Clear();

            // 스로틀 정보
            if (frameData.Metadata.ContainsKey("user/throttle"))
            {
                var item = new ListViewItem("스로틀");
                item.SubItems.Add(frameData.Metadata["user/throttle"].ToString());
                lstJSONSummary.Items.Add(item);
            }

            // 앵글 정보
            if (frameData.Metadata.ContainsKey("user/angle"))
            {
                var item = new ListViewItem("앵글");
                item.SubItems.Add(frameData.Metadata["user/angle"].ToString());
                lstJSONSummary.Items.Add(item);
            }

            // 이미지 이름
            var imgItem = new ListViewItem("이미지");
            imgItem.SubItems.Add(frameData.ImageFileName);
            lstJSONSummary.Items.Add(imgItem);
        }

        private void UpdateCurrentFrameDisplay()
        {
            if (currentFrameIndex >= 0 && currentFrameIndex < frameDataList.Count)
            {
                var currentFrame = frameDataList[currentFrameIndex];
                label5.Text = $"현재 : Frame {currentFrame.FrameNumber}";
            }
        }

        /// <summary>
        /// 이미지 크기 변경 이벤트 - 썸네일 크기를 패널에 맞춤
        /// </summary>
        private void ImageViewer_Resize(object sender, EventArgs e)
        {
            // pnlCenterMain의 크기에 따라 pictureBox1 크기 조정
            if (pnlCenterMain != null && pictureBox1 != null)
            {
                pictureBox1.Width = pnlCenterMain.Width;
                pictureBox1.Height = pnlCenterMain.Height;
            }
        }

        /// <summary>
        /// 썸네일 패널 리사이즈 이벤트
        /// </summary>
        private void PnlThumbnail_Resize(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null && panel.Controls.Count > 0)
            {
                // 패널의 모든 PictureBox 크기 조정
                foreach (Control control in panel.Controls)
                {
                    if (control is PictureBox picBox)
                    {
                        // 패널 크기에 맞춰 PictureBox 크기 자동 조정
                        picBox.Width = panel.Width;
                        picBox.Height = panel.Height - (panel.Height > 50 ? 50 : 0);
                    }
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (currentFrameIndex > 0)
            {
                DisplayFrameAtIndex(currentFrameIndex - 1);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (currentFrameIndex < frameDataList.Count - 1)
            {
                DisplayFrameAtIndex(currentFrameIndex + 1);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (!isPlaying)
            {
                isPlaying = true;
                playTimer.Start();
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            isPlaying = false;
            playTimer.Stop();
            currentFrameIndex = 0;
            DisplayFrameAtIndex(0);
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            if (isPlaying && currentFrameIndex < frameDataList.Count - 1)
            {
                DisplayFrameAtIndex(currentFrameIndex + 1);
            }
            else if (isPlaying)
            {
                isPlaying = false;
                playTimer.Stop();
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string speedText = comboBox1.SelectedItem?.ToString() ?? "1.0";
            if (double.TryParse(speedText, out double speed))
            {
                playbackSpeed = speed;
                playTimer.Interval = (int)(1000.0 / (FRAMES_PER_SECOND * playbackSpeed));
            }
        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (!isPlaying)
            {
                DisplayFrameAtIndex(trackBar1.Value);
            }
        }
    }
}
