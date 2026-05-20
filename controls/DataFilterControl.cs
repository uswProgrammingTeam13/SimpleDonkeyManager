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
        private controlutils.ImageList imageList= new controlutils.ImageList();
        private controlutils.ImageViewer imageViewer = new controlutils.ImageViewer();
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

            SetSummaryData("12,345", "9,876", "2,469 (20.0%)", "80.0%");

            imageList.Dock = DockStyle.Fill;
            imageList.Visible = true;
            pnlFrameList.Controls.Add(imageList);

            imageViewer.Dock = DockStyle.Fill;
            imageViewer.Visible = true;
            pnlImageView.Controls.Add(imageViewer);

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
    }
}
