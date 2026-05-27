using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;

namespace SimpleDonkeyManager.controls
{
    public partial class ResultControl : UserControl
    {
        private PlotView plotView = null;
        private PlotModel plotModel = null;
        private ChartDataModel trainingMetrics = null;
        private List<FrameData> trainingData = null;

        public ResultControl()
        {
            InitializeComponent();
            InitializeChartView();
            InitializeImageViewer();

            // Resize 이벤트 핸들러
            this.Resize += ResultControl_Resize;
            this.Load += ResultControl_Load;
        }

        private void ResultControl_Load(object sender, EventArgs e)
        {
            // 로드 시 초기 레이아웃 조정
            AdjustLayoutForWindowSize();
        }

        private void ResultControl_Resize(object sender, EventArgs e)
        {
            // 창 크기 변경 시 레이아웃 조정
            AdjustLayoutForWindowSize();
        }

        private void InitializeImageViewer()
        {
            try
            {
                if (imageViewerUpper1 != null)
                {
                    // ImageViewerUpper는 Designer에서 이미 추가됨
                    // 필요하면 Logger 설정
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"이미지 뷰어 초기화 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 창 크기에 따라 레이아웃을 동적으로 조정합니다.
        /// </summary>
        private void AdjustLayoutForWindowSize()
        {
            try
            {
                if (tlpResultMain == null || tlpResultMain.IsDisposed)
                    return;

                if (pnlLeft == null || pnlRight == null)
                    return;

                int controlWidth = this.Width;

                tlpResultMain.SuspendLayout();
                try
                {
                    if (controlWidth <= 900)
                    {
                        // 상하 레이아웃
                        tlpResultMain.ColumnCount = 1;
                        tlpResultMain.RowCount = 2;

                        tlpResultMain.ColumnStyles.Clear();
                        tlpResultMain.RowStyles.Clear();
                        tlpResultMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                        tlpResultMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                        tlpResultMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

                        tlpResultMain.SetCellPosition(pnlLeft, new TableLayoutPanelCellPosition(0, 0));
                        tlpResultMain.SetCellPosition(pnlRight, new TableLayoutPanelCellPosition(0, 1));
                    }
                    else
                    {
                        // 좌우 레이아웃
                        tlpResultMain.ColumnCount = 2;
                        tlpResultMain.RowCount = 1;

                        tlpResultMain.ColumnStyles.Clear();
                        tlpResultMain.RowStyles.Clear();
                        tlpResultMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67F));
                        tlpResultMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
                        tlpResultMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

                        tlpResultMain.SetCellPosition(pnlLeft, new TableLayoutPanelCellPosition(0, 0));
                        tlpResultMain.SetCellPosition(pnlRight, new TableLayoutPanelCellPosition(1, 0));
                    }
                }
                finally
                {
                    tlpResultMain.ResumeLayout(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"레이아웃 조정 오류: {ex.Message}");
            }
        }

        private void InitializeChartView()
        {
            try
            {
                // OxyPlot PlotView 생성
                plotView = new PlotView();
                plotView.Dock = DockStyle.Fill;
                plotView.Name = "plotViewResultChart";

                // 플롯 모델 초기화
                CreateResultPlotModel();
                plotView.Model = plotModel;

                // pnlResultChart에 추가
                pnlResultChart.Controls.Clear();
                pnlResultChart.Controls.Add(plotView);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"결과 그래프 초기화 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 결과 표시용 그래프 모델 생성
        /// </summary>
        private void CreateResultPlotModel()
        {
            plotModel = new PlotModel
            {
                Title = "Training Results",
                TitleFontSize = 12,
                Background = OxyColors.White,
                PlotAreaBorderColor = OxyColors.Black,
                PlotAreaBorderThickness = new OxyThickness(1)
            };

            // X축 (에포크)
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Epoch",
                TitleFontSize = 11,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColor.FromArgb(200, 200, 200, 200)
            };
            plotModel.Axes.Add(xAxis);

            // Y축 (손실값)
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Loss",
                TitleFontSize = 11,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColor.FromArgb(200, 200, 200, 200)
            };
            plotModel.Axes.Add(yAxis);

            // 훈련 손실 라인
            var trainSeries = new LineSeries
            {
                Title = "Train Loss",
                Color = OxyColors.Blue,
                StrokeThickness = 2,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };
            plotModel.Series.Add(trainSeries);

            // 검증 손실 라인
            var valSeries = new LineSeries
            {
                Title = "Validation Loss",
                Color = OxyColors.Red,
                StrokeThickness = 2,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };
            plotModel.Series.Add(valSeries);
        }

        /// <summary>
        /// 학습 메트릭을 결과 화면에 표시
        /// </summary>
        public void DisplayTrainingResults(ChartDataModel metrics)
        {
            try
            {
                if (metrics == null)
                    return;

                trainingMetrics = metrics;

                // 그래프 표시
                if (plotModel == null || plotModel.Series.Count < 2)
                {
                    CreateResultPlotModel();
                    if (plotView != null)
                        plotView.Model = plotModel;
                }

                // 기존 데이터 제거
                ((LineSeries)plotModel.Series[0]).Points.Clear();
                ((LineSeries)plotModel.Series[1]).Points.Clear();

                // 훈련 손실 데이터 추가
                var trainLosses = metrics.GetTrainLosses();
                var epochs = metrics.GetEpochs();
                for (int i = 0; i < Math.Min(trainLosses.Length, epochs.Length); i++)
                {
                    ((LineSeries)plotModel.Series[0]).Points.Add(
                        new DataPoint(epochs[i], trainLosses[i]));
                }

                // 검증 손실 데이터 추가
                var allMetrics = metrics.GetAllMetrics();
                var valEpochs = new List<double>();
                var valLosses = new List<double>();

                foreach (var metric in allMetrics)
                {
                    if (metric.ValidationLoss.HasValue)
                    {
                        valEpochs.Add(metric.Epoch);
                        valLosses.Add(metric.ValidationLoss.Value);
                    }
                }

                for (int i = 0; i < valLosses.Count; i++)
                {
                    ((LineSeries)plotModel.Series[1]).Points.Add(
                        new DataPoint(valEpochs[i], valLosses[i]));
                }

                // 플롯 새로고침
                plotModel.InvalidatePlot(true);

                // 결과 요약 표시
                UpdateResultSummary(metrics);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"그래프 표시 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateResultSummary(ChartDataModel metrics)
        {
            try
            {
                if (metrics == null)
                    return;

                // 총 에포크
                if (lblTotalEpochs != null)
                {
                    lblTotalEpochs.Text = $"총 에포크: {metrics.GetMetricCount()}";
                }

                // 최소 손실값
                if (lblMinLoss != null)
                {
                    float minLoss = metrics.GetMinimumLoss();
                    lblMinLoss.Text = $"최소 손실값: {minLoss:F4}";
                }

                // 최고 정확도
                if (lblMaxAccuracy != null)
                {
                    float maxAccuracy = metrics.GetMaximumAccuracy();
                    lblMaxAccuracy.Text = $"최고 정확도: {maxAccuracy:F4}";
                }

                // 소요 시간
                if (lblTrainingTime != null)
                {
                    TimeSpan elapsed = metrics.GetElapsedTime();
                    string timeStr;
                    if (elapsed.TotalSeconds < 60)
                    {
                        timeStr = $"{elapsed.TotalSeconds:F0}초";
                    }
                    else if (elapsed.TotalMinutes < 60)
                    {
                        timeStr = $"{elapsed.TotalMinutes:F1}분";
                    }
                    else
                    {
                        timeStr = $"{elapsed.TotalHours:F1}시간";
                    }
                    lblTrainingTime.Text = $"소요 시간: {timeStr}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"결과 요약 표시 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 학습 데이터를 설정합니다.
        /// </summary>
        public void SetTrainingData(List<FrameData> data)
        {
            if (data != null)
            {
                this.trainingData = new List<FrameData>(data);
            }

            try
            {
                if (imageViewerUpper1 != null)
                {
                    imageViewerUpper1.LoadFrames(this.trainingData ?? new List<FrameData>());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"이미지 뷰어 로드 오류: {ex.Message}");
            }
        }
    }
}
