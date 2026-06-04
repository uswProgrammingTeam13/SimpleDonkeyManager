using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SimpleDonkeyManager.controlutils
{
    /// <summary>
    /// 영상 편집기 스타일의 프레임 타임라인 컨트롤입니다.
    /// - 클릭: 해당 지점 프레임 확인 (FramePreviewed)
    /// - 클릭 & 드래그: 미리보기만 따라감(구간 선택 안 함) (FramePreviewed)
    /// - 더블클릭: 해당 지점 프레임 선택/앵커 지정 (FrameAnchored) — 기존 구간 선택 해제
    /// - 더블클릭 & 드래그: 클릭 지점부터 구간 선택 (RangeSelected)
    /// - 앵커가 있을 때 클릭: 앵커와 클릭 지점 사이 구간 선택 (RangeSelected)
    /// - 휠(가운데) 클릭: 현재 표시 중인 지점을 앵커로 선택 (FrameAnchored)
    /// - 우클릭: 프레임/구간 선택 취소 (SelectionCleared)
    /// </summary>
    public class FrameTimeline : Control
    {
        private int frameCount = 0;
        private int currentIndex = 0;

        // 앵커(선택된 단일 프레임) 인덱스. -1이면 없음.
        private int anchorIndex = -1;

        // 구간 선택. 둘 다 -1이 아니면 구간이 존재.
        private int rangeStart = -1;
        private int rangeEnd = -1;

        // 드래그 상태
        // isPreviewDragging: 일반 클릭&드래그 → 미리보기만(구간 선택 안 함)
        // isRangeDragging: 더블클릭&드래그 → 클릭 지점부터 구간 선택
        private bool isPreviewDragging = false;
        private bool isRangeDragging = false;
        private int dragStartIndex = -1;
        private bool draggedSincePress = false;

        // 레이아웃 여백
        private const int TrackMarginX = 8;
        private const int TrackHeight = 14;

        public FrameTimeline()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.ResizeRedraw
                | ControlStyles.UserPaint, true);
            UpdateStyles();

            BackColor = Color.FromArgb(248, 248, 248);
            Height = 45;
        }

        // ────────────────────────────────────────────────────────────
        // 이벤트
        // ────────────────────────────────────────────────────────────

        /// <summary>프레임을 확인(클릭/드래그)했을 때 발생합니다. 인자는 프레임 인덱스입니다.</summary>
        public event EventHandler<int> FramePreviewed;

        /// <summary>프레임을 선택(더블클릭)하여 앵커로 지정했을 때 발생합니다. 인자는 프레임 인덱스입니다.</summary>
        public event EventHandler<int> FrameAnchored;

        /// <summary>구간이 선택되었을 때 발생합니다. 인자는 (시작 인덱스, 끝 인덱스)입니다.</summary>
        public event EventHandler<RangeSelectedEventArgs> RangeSelected;

        /// <summary>프레임/구간 선택이 취소되었을 때 발생합니다.</summary>
        public event EventHandler SelectionCleared;

        // ────────────────────────────────────────────────────────────
        // 공개 속성/메서드
        // ────────────────────────────────────────────────────────────

        /// <summary>총 프레임 개수입니다.</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        [System.ComponentModel.Browsable(false)]
        public int FrameCount
        {
            get => frameCount;
            set
            {
                frameCount = Math.Max(0, value);
                if (currentIndex >= frameCount) currentIndex = Math.Max(0, frameCount - 1);
                ClearSelectionInternal(false);
                Invalidate();
            }
        }

        /// <summary>현재 표시 중인 프레임 인덱스입니다.</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        [System.ComponentModel.Browsable(false)]
        public int CurrentIndex
        {
            get => currentIndex;
            set
            {
                int clamped = Clamp(value);
                if (clamped != currentIndex)
                {
                    currentIndex = clamped;
                    Invalidate();
                }
            }
        }

        /// <summary>앵커(선택된 단일 프레임) 인덱스입니다. 없으면 -1.</summary>
        public int AnchorIndex => anchorIndex;

        /// <summary>구간이 선택되어 있으면 true입니다.</summary>
        public bool HasRange => rangeStart >= 0 && rangeEnd >= 0;

        /// <summary>구간 시작 인덱스입니다. 구간이 없으면 -1.</summary>
        public int RangeStart => rangeStart;

        /// <summary>구간 끝 인덱스입니다. 구간이 없으면 -1.</summary>
        public int RangeEnd => rangeEnd;

        /// <summary>
        /// 외부(예: 정지 버튼)에서 선택 및 구간을 모두 해제합니다.
        /// </summary>
        public void ClearSelection()
        {
            ClearSelectionInternal(true);
        }

        private void ClearSelectionInternal(bool raiseEvent)
        {
            bool had = anchorIndex >= 0 || HasRange;
            anchorIndex = -1;
            rangeStart = -1;
            rangeEnd = -1;
            Invalidate();
            if (raiseEvent && had)
            {
                SelectionCleared?.Invoke(this, EventArgs.Empty);
            }
        }

        // ────────────────────────────────────────────────────────────
        // 좌표 ↔ 인덱스 매핑
        // ────────────────────────────────────────────────────────────

        private Rectangle GetTrackRect()
        {
            int y = (Height - TrackHeight) / 2;
            return new Rectangle(TrackMarginX, y, Math.Max(1, Width - TrackMarginX * 2), TrackHeight);
        }

        private int IndexFromX(int x)
        {
            if (frameCount <= 1) return 0;
            Rectangle track = GetTrackRect();
            double ratio = (double)(x - track.Left) / track.Width;
            ratio = Math.Max(0.0, Math.Min(1.0, ratio));
            int idx = (int)Math.Round(ratio * (frameCount - 1));
            return Clamp(idx);
        }

        private int XFromIndex(int index)
        {
            Rectangle track = GetTrackRect();
            if (frameCount <= 1) return track.Left;
            double ratio = (double)index / (frameCount - 1);
            return track.Left + (int)Math.Round(ratio * track.Width);
        }

        private int Clamp(int index)
        {
            if (frameCount <= 0) return 0;
            if (index < 0) return 0;
            if (index > frameCount - 1) return frameCount - 1;
            return index;
        }

        // ────────────────────────────────────────────────────────────
        // 렌더링
        // ────────────────────────────────────────────────────────────

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle track = GetTrackRect();

            // 트랙 배경
            using (var bg = new SolidBrush(Color.FromArgb(225, 228, 232)))
            using (var border = new Pen(Color.FromArgb(190, 195, 200)))
            {
                g.FillRectangle(bg, track);
                g.DrawRectangle(border, track);
            }

            if (frameCount <= 0)
                return;

            // 구간 하이라이트
            if (HasRange)
            {
                int x1 = XFromIndex(Math.Min(rangeStart, rangeEnd));
                int x2 = XFromIndex(Math.Max(rangeStart, rangeEnd));
                var rangeRect = new Rectangle(x1, track.Top, Math.Max(1, x2 - x1), track.Height);
                using (var rangeBrush = new SolidBrush(Color.FromArgb(120, 100, 149, 237)))
                {
                    g.FillRectangle(rangeBrush, rangeRect);
                }
                using (var rangePen = new Pen(Color.RoyalBlue))
                {
                    g.DrawRectangle(rangePen, rangeRect);
                }
            }

            // 앵커(선택 프레임) 강조 표시
            if (anchorIndex >= 0)
            {
                int ax = XFromIndex(anchorIndex);
                using (var anchorPen = new Pen(Color.OrangeRed, 2f))
                {
                    g.DrawLine(anchorPen, ax, track.Top - 4, ax, track.Bottom + 4);
                }
                using (var anchorBrush = new SolidBrush(Color.OrangeRed))
                {
                    g.FillPolygon(anchorBrush, new[]
                    {
                        new Point(ax - 5, track.Top - 9),
                        new Point(ax + 5, track.Top - 9),
                        new Point(ax, track.Top - 3)
                    });
                }
            }

            // 현재 위치 마커
            int cx = XFromIndex(currentIndex);
            using (var curPen = new Pen(Color.RoyalBlue, 2f))
            {
                g.DrawLine(curPen, cx, track.Top - 6, cx, track.Bottom + 6);
            }
            using (var curBrush = new SolidBrush(Color.RoyalBlue))
            {
                g.FillEllipse(curBrush, cx - 5, track.Bottom + 4, 10, 10);
            }
        }

        // ────────────────────────────────────────────────────────────
        // 마우스 상호작용
        // ────────────────────────────────────────────────────────────

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (frameCount <= 0)
                return;

            if (e.Button == MouseButtons.Right)
            {
                // 우클릭: 프레임/구간 선택 취소
                ClearSelectionInternal(true);
                return;
            }

            if (e.Button == MouseButtons.Middle)
            {
                // 휠(가운데) 클릭: 현재 표시 중인 지점을 앵커로 선택
                Focus();
                anchorIndex = currentIndex;
                rangeStart = -1;
                rangeEnd = -1;
                Invalidate();
                FrameAnchored?.Invoke(this, currentIndex);
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                Focus();
                dragStartIndex = IndexFromX(e.X);
                currentIndex = dragStartIndex;

                if (anchorIndex >= 0)
                {
                    // 앵커가 있으면 클릭&드래그를 구간 선택 모드로 처리
                    // (드래그 시작점은 앵커 지점으로 고정)
                    isPreviewDragging = false;
                    isRangeDragging = true;
                    draggedSincePress = false;
                    dragStartIndex = anchorIndex;
                }
                else
                {
                    isPreviewDragging = true;
                    draggedSincePress = false;
                }

                // 드래그 시작 지점 프레임 미리보기
                FramePreviewed?.Invoke(this, currentIndex);
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (frameCount <= 0)
                return;

            if ((isPreviewDragging || isRangeDragging) && (e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                int idx = IndexFromX(e.X);
                if (idx != currentIndex)
                {
                    currentIndex = idx;
                    draggedSincePress = true;
                    // 드래그 도중 마우스 위치 프레임 미리보기 (두 모드 공통)
                    FramePreviewed?.Invoke(this, currentIndex);

                    // 더블클릭&드래그(구간 모드)일 때만 임시 구간 표시
                    if (isRangeDragging && idx != dragStartIndex)
                    {
                        rangeStart = dragStartIndex;
                        rangeEnd = idx;
                    }
                    Invalidate();
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (frameCount <= 0)
            {
                isPreviewDragging = false;
                isRangeDragging = false;
                return;
            }

            if (e.Button != MouseButtons.Left)
                return;

            if (isRangeDragging)
            {
                // 더블클릭&드래그로 구간 선택 완료
                isRangeDragging = false;
                int endIdx = IndexFromX(e.X);

                if (endIdx != dragStartIndex)
                {
                    rangeStart = dragStartIndex;
                    rangeEnd = endIdx;
                    anchorIndex = -1;
                    currentIndex = endIdx;
                    Invalidate();
                    RaiseRangeSelected();
                }
                return;
            }

            if (isPreviewDragging)
            {
                isPreviewDragging = false;

                // 드래그가 발생했으면 미리보기만 하고 종료(구간 선택 안 함)
                if (draggedSincePress)
                    return;

                // 드래그 없이 제자리 클릭 → 클릭 처리
                HandleClick(dragStartIndex);
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (frameCount <= 0)
                return;

            if (e.Button == MouseButtons.Left)
            {
                // 더블클릭 시점부터 드래그하면 구간 선택 모드로 진입
                isPreviewDragging = false;
                isRangeDragging = true;
                draggedSincePress = false;
                int idx = IndexFromX(e.X);
                dragStartIndex = idx;

                // 더블클릭: 프레임 선택(앵커) — 기존 구간 선택은 해제
                anchorIndex = idx;
                rangeStart = -1;
                rangeEnd = -1;
                currentIndex = idx;
                Invalidate();
                FrameAnchored?.Invoke(this, idx);
            }
        }

        /// <summary>
        /// 단일 클릭 처리: 앵커가 있으면 구간 선택, 없으면 프레임 확인.
        /// </summary>
        private void HandleClick(int idx)
        {
            if (anchorIndex >= 0)
            {
                // 앵커가 선택된 상태에서 클릭 → 구간 선택
                rangeStart = anchorIndex;
                rangeEnd = idx;
                currentIndex = idx;
                Invalidate();
                RaiseRangeSelected();
            }
            else
            {
                // 단순 프레임 확인
                currentIndex = idx;
                Invalidate();
                FramePreviewed?.Invoke(this, idx);
            }
        }

        private void RaiseRangeSelected()
        {
            int start = Math.Min(rangeStart, rangeEnd);
            int end = Math.Max(rangeStart, rangeEnd);
            RangeSelected?.Invoke(this, new RangeSelectedEventArgs(start, end));
        }
    }

    /// <summary>
    /// 구간 선택 이벤트 인자입니다.
    /// </summary>
    public class RangeSelectedEventArgs : EventArgs
    {
        public int StartIndex { get; }
        public int EndIndex { get; }

        public RangeSelectedEventArgs(int startIndex, int endIndex)
        {
            StartIndex = startIndex;
            EndIndex = endIndex;
        }
    }
}
