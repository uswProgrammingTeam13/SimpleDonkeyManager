using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SimpleDonkeyManager
{
    /// <summary>
    /// 데이터 필터 스냅샷(버전) 내역을 표시하는 Non-modal 창입니다.
    /// 저장된 스냅샷 목록과 직전 스냅샷 대비 프레임 증감을 보여주고,
    /// 선택한 시점으로 되돌리기(불러오기) 및 이력 삭제를 지원합니다.
    /// </summary>
    public class SnapshotHistoryForm : Form
    {
        private readonly DataFilterControl owner;

        private ListView lstSnapshots;
        private Button btnLoad;
        private Button btnDelete;
        private Button btnClose;
        private Label lblMemoTitle;
        private TextBox txtMemo;
        private Label lblEmpty;

        public SnapshotHistoryForm(DataFilterControl ownerControl)
        {
            owner = ownerControl;
            InitializeComponentManual();
            RefreshList();
        }

        private void InitializeComponentManual()
        {
            Text = "필터 스냅샷 내역";
            Font = new Font("나눔고딕", 9F);
            Size = new Size(460, 560);
            MinimumSize = new Size(360, 400);
            ShowInTaskbar = false;
            MaximizeBox = false;
            StartPosition = FormStartPosition.Manual;

            // 스냅샷 목록
            lstSnapshots = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                MultiSelect = false,
                HideSelection = false
            };
            lstSnapshots.Columns.Add("저장 시각", 130);
            lstSnapshots.Columns.Add("작성자", 90);
            lstSnapshots.Columns.Add("제외 수", 70);
            lstSnapshots.Columns.Add("증감", 90);
            lstSnapshots.SelectedIndexChanged += LstSnapshots_SelectedIndexChanged;
            lstSnapshots.DoubleClick += (s, e) => BtnLoad_Click(s, e);

            // 저장 내역이 없을 때 표시할 안내 라벨 (목록 위에 겹쳐 배치)
            lblEmpty = new Label
            {
                Text = "저장된 스냅샷이 없습니다.\n필터를 적용한 뒤 '필터 저장'으로 스냅샷을 만들어 보세요.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Gray,
                Font = new Font("나눔고딕", 10F),
                Visible = false
            };

            var listPanel = new Panel { Dock = DockStyle.Fill };
            listPanel.Controls.Add(lblEmpty);
            listPanel.Controls.Add(lstSnapshots);
            lblEmpty.BringToFront();

            // 변경 내역(메모) 표시 영역
            lblMemoTitle = new Label
            {
                Text = "변경 내역",
                Dock = DockStyle.Top,
                Height = 22,
                Font = new Font("나눔고딕", 9F, FontStyle.Bold),
                ForeColor = Color.DimGray,
                Padding = new Padding(4, 4, 0, 0)
            };

            txtMemo = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.FromArgb(248, 248, 248)
            };

            var memoPanel = new Panel { Dock = DockStyle.Bottom, Height = 110 };
            memoPanel.Controls.Add(txtMemo);
            memoPanel.Controls.Add(lblMemoTitle);

            // 버튼 영역
            btnLoad = new Button
            {
                Text = "이 시점으로 불러오기",
                Dock = DockStyle.Left,
                Width = 160,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.RoyalBlue,
                Font = new Font("나눔고딕", 9F, FontStyle.Bold),
                Enabled = false
            };
            btnLoad.FlatAppearance.BorderColor = Color.RoyalBlue;
            btnLoad.Click += BtnLoad_Click;

            btnDelete = new Button
            {
                Text = "이력 삭제",
                Dock = DockStyle.Left,
                Width = 100,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.Firebrick,
                Font = new Font("나눔고딕", 9F, FontStyle.Bold),
                Enabled = false
            };
            btnDelete.FlatAppearance.BorderColor = Color.Firebrick;
            btnDelete.Click += BtnDelete_Click;

            btnClose = new Button
            {
                Text = "닫기",
                Dock = DockStyle.Right,
                Width = 90,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.DimGray,
                Font = new Font("나눔고딕", 9F)
            };
            btnClose.FlatAppearance.BorderColor = Color.Silver;
            btnClose.Click += (s, e) => Close();

            var buttonPanel = new Panel { Dock = DockStyle.Bottom, Height = 44, Padding = new Padding(6) };
            buttonPanel.Controls.Add(btnLoad);
            buttonPanel.Controls.Add(btnDelete);
            buttonPanel.Controls.Add(btnClose);

            Controls.Add(listPanel);
            Controls.Add(memoPanel);
            Controls.Add(buttonPanel);
        }

        /// <summary>
        /// 저장소에서 스냅샷 목록을 다시 읽어 화면을 갱신합니다.
        /// </summary>
        public void RefreshList()
        {
            try
            {
                if (lstSnapshots == null)
                    return;

                string selectedId = GetSelectedSnapshotId();

                lstSnapshots.BeginUpdate();
                lstSnapshots.Items.Clear();

                var store = owner?.GetSnapshotStore();
                if (store != null)
                {
                    var deltas = store.ComputeDeltas();

                    foreach (var snapshot in store.Snapshots)
                    {
                        if (snapshot == null)
                            continue;

                        int delta = deltas.TryGetValue(snapshot.Id, out int d) ? d : 0;

                        var item = new ListViewItem(snapshot.CreatedAt.ToString("yyyy-MM-dd HH:mm"))
                        {
                            Tag = snapshot.Id
                        };
                        item.SubItems.Add(string.IsNullOrEmpty(snapshot.AuthorId) ? "-" : snapshot.AuthorId);
                        item.SubItems.Add(snapshot.DeletedCount.ToString());

                        string deltaText = delta > 0 ? $"+{delta}" : delta.ToString();
                        var deltaSub = item.SubItems.Add(deltaText);
                        if (delta > 0)
                            deltaSub.ForeColor = Color.Firebrick;
                        else if (delta < 0)
                            deltaSub.ForeColor = Color.SeaGreen;
                        else
                            deltaSub.ForeColor = Color.Gray;

                        lstSnapshots.Items.Add(item);
                    }
                }

                lstSnapshots.EndUpdate();

                // 선택 복원
                if (!string.IsNullOrEmpty(selectedId))
                    SelectSnapshotById(selectedId);

                // 빈 상태 안내 라벨 토글
                if (lblEmpty != null)
                    lblEmpty.Visible = lstSnapshots.Items.Count == 0;

                UpdateButtonState();
                UpdateMemo();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"스냅샷 목록 갱신 오류: {ex.Message}");
            }
        }

        private string GetSelectedSnapshotId()
        {
            if (lstSnapshots != null && lstSnapshots.SelectedItems.Count > 0)
                return lstSnapshots.SelectedItems[0].Tag as string;
            return null;
        }

        private void SelectSnapshotById(string snapshotId)
        {
            foreach (ListViewItem item in lstSnapshots.Items)
            {
                if ((item.Tag as string) == snapshotId)
                {
                    item.Selected = true;
                    item.EnsureVisible();
                    return;
                }
            }
        }

        private void LstSnapshots_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
            UpdateMemo();
        }

        private void UpdateButtonState()
        {
            bool hasSelection = lstSnapshots != null && lstSnapshots.SelectedItems.Count > 0;
            if (btnLoad != null) btnLoad.Enabled = hasSelection;
            if (btnDelete != null) btnDelete.Enabled = hasSelection;
        }

        private void UpdateMemo()
        {
            if (txtMemo == null)
                return;

            string id = GetSelectedSnapshotId();
            var store = owner?.GetSnapshotStore();
            var snapshot = (id != null && store != null) ? store.GetById(id) : null;
            txtMemo.Text = snapshot?.Memo ?? string.Empty;
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            string id = GetSelectedSnapshotId();
            if (string.IsNullOrEmpty(id))
                return;

            var confirm = MessageBox.Show(
                "선택한 시점으로 데이터를 되돌립니다.\n현재 필터 상태가 이 스냅샷 상태로 변경됩니다. 계속하시겠습니까?",
                "스냅샷 불러오기", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;

            bool ok = owner != null && owner.LoadSnapshot(id);
            if (!ok)
            {
                MessageBox.Show("스냅샷을 불러오지 못했습니다.",
                    "스냅샷 불러오기", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string id = GetSelectedSnapshotId();
            if (string.IsNullOrEmpty(id))
                return;

            var confirm = MessageBox.Show(
                "선택한 스냅샷을 이력에서 삭제합니다.\n(실제 데이터는 변경되지 않습니다.) 계속하시겠습니까?",
                "스냅샷 삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
                return;

            bool ok = owner != null && owner.DeleteSnapshot(id);
            if (ok)
            {
                // 삭제 후 목록을 다시 로드해 증감을 다시 계산
                RefreshList();
            }
            else
            {
                MessageBox.Show("스냅샷을 삭제하지 못했습니다.",
                    "스냅샷 삭제", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
