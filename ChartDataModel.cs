using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleDonkeyManager
{
    /// <summary>
    /// 학습 과정의 메트릭 데이터를 저장하는 클래스
    /// </summary>
    public class TrainingMetric
    {
        public int Epoch { get; set; }
        public float TrainLoss { get; set; }
        public float? ValidationLoss { get; set; }
        public float? Accuracy { get; set; }
        public float? ValidationAccuracy { get; set; }
        public DateTime Timestamp { get; set; }

        public TrainingMetric()
        {
            Timestamp = DateTime.Now;
        }
    }

    /// <summary>
    /// 전체 학습 과정의 그래프 데이터를 관리하는 클래스
    /// Donkey UI와 유사한 형식의 메트릭을 추적합니다.
    /// </summary>
    public class ChartDataModel
    {
        private List<TrainingMetric> metrics = new List<TrainingMetric>();

        public string ModelType { get; set; }
        public int TotalEpochs { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string DataFolderPath { get; set; }
        public int TotalFrames { get; set; }

        public ChartDataModel()
        {
            StartTime = DateTime.Now;
        }

        /// <summary>
        /// 에포크별 메트릭을 추가합니다.
        /// </summary>
        public void AddMetric(int epoch, float trainLoss, float? validationLoss = null, 
                            float? accuracy = null, float? validationAccuracy = null)
        {
            // 동일 에포크가 이미 있으면 갱신합니다.
            // Keras는 한 에포크 동안 진행 줄을 여러 번 출력하므로(중간 loss → 최종 loss/val_loss),
            // 같은 에포크에 대해 최신 값으로 덮어써서 그래프에 중복 점이 쌓이지 않도록 합니다.
            var existing = metrics.FirstOrDefault(m => m.Epoch == epoch);
            if (existing != null)
            {
                existing.TrainLoss = trainLoss;
                if (validationLoss.HasValue) existing.ValidationLoss = validationLoss;
                if (accuracy.HasValue) existing.Accuracy = accuracy;
                if (validationAccuracy.HasValue) existing.ValidationAccuracy = validationAccuracy;
                existing.Timestamp = DateTime.Now;
                return;
            }

            var metric = new TrainingMetric
            {
                Epoch = epoch,
                TrainLoss = trainLoss,
                ValidationLoss = validationLoss,
                Accuracy = accuracy,
                ValidationAccuracy = validationAccuracy
            };

            metrics.Add(metric);
        }

        /// <summary>
        /// 모든 메트릭을 반환합니다.
        /// </summary>
        public List<TrainingMetric> GetAllMetrics()
        {
            return new List<TrainingMetric>(metrics);
        }

        /// <summary>
        /// 특정 범위의 메트릭을 반환합니다.
        /// </summary>
        public List<TrainingMetric> GetMetrics(int startEpoch, int endEpoch)
        {
            return metrics.Where(m => m.Epoch >= startEpoch && m.Epoch <= endEpoch)
                         .ToList();
        }

        /// <summary>
        /// 에포크 배열을 반환합니다.
        /// </summary>
        public double[] GetEpochs()
        {
            return metrics.Select(m => (double)m.Epoch).ToArray();
        }

        /// <summary>
        /// 훈련 손실값 배열을 반환합니다.
        /// </summary>
        public double[] GetTrainLosses()
        {
            return metrics.Select(m => (double)m.TrainLoss).ToArray();
        }

        /// <summary>
        /// 검증 손실값 배열을 반환합니다.
        /// </summary>
        public double[] GetValidationLosses()
        {
            return metrics.Where(m => m.ValidationLoss.HasValue)
                         .Select(m => (double)m.ValidationLoss.Value).ToArray();
        }

        /// <summary>
        /// 정확도 배열을 반환합니다.
        /// </summary>
        public double[] GetAccuracies()
        {
            return metrics.Where(m => m.Accuracy.HasValue)
                         .Select(m => (double)m.Accuracy.Value).ToArray();
        }

        /// <summary>
        /// 검증 정확도 배열을 반환합니다.
        /// </summary>
        public double[] GetValidationAccuracies()
        {
            return metrics.Where(m => m.ValidationAccuracy.HasValue)
                         .Select(m => (double)m.ValidationAccuracy.Value).ToArray();
        }

        /// <summary>
        /// 최저 손실값(최고 성능)을 반환합니다.
        /// </summary>
        public float GetMinimumLoss()
        {
            return metrics.Count > 0 ? metrics.Min(m => m.TrainLoss) : 0f;
        }

        /// <summary>
        /// 최고 정확도를 반환합니다.
        /// </summary>
        public float GetMaximumAccuracy()
        {
            return metrics.Where(m => m.Accuracy.HasValue)
                         .Select(m => m.Accuracy.Value)
                         .DefaultIfEmpty(0f)
                         .Max();
        }

        /// <summary>
        /// 메트릭 개수를 반환합니다.
        /// </summary>
        public int GetMetricCount()
        {
            return metrics.Count;
        }

        /// <summary>
        /// 메트릭을 초기화합니다.
        /// </summary>
        public void Clear()
        {
            metrics.Clear();
            StartTime = DateTime.Now;
            EndTime = null;
        }

        /// <summary>
        /// 학습 종료를 표시합니다.
        /// </summary>
        public void MarkAsCompleted()
        {
            EndTime = DateTime.Now;
        }

        /// <summary>
        /// 학습 소요 시간을 반환합니다.
        /// </summary>
        public TimeSpan GetElapsedTime()
        {
            if (EndTime.HasValue)
                return EndTime.Value - StartTime;
            else
                return DateTime.Now - StartTime;
        }

        /// <summary>
        /// 데이터를 JSON 형식으로 직렬화합니다.
        /// </summary>
        public string ToJson()
        {
            var json = new System.Text.StringBuilder();
            json.AppendLine("{");
            json.AppendLine($"  \"modelType\": \"{ModelType}\",");
            json.AppendLine($"  \"totalEpochs\": {TotalEpochs},");
            json.AppendLine($"  \"totalFrames\": {TotalFrames},");
            json.AppendLine($"  \"startTime\": \"{StartTime:O}\",");
            json.AppendLine($"  \"endTime\": \"{(EndTime?.ToString("O") ?? "null")}\",");
            json.AppendLine($"  \"elapsedSeconds\": {GetElapsedTime().TotalSeconds},");
            json.AppendLine("  \"metrics\": [");

            for (int i = 0; i < metrics.Count; i++)
            {
                var m = metrics[i];
                json.Append($"    {{\"epoch\": {m.Epoch}, \"trainLoss\": {m.TrainLoss:F6}");

                if (m.ValidationLoss.HasValue)
                    json.Append($", \"validationLoss\": {m.ValidationLoss.Value:F6}");

                if (m.Accuracy.HasValue)
                    json.Append($", \"accuracy\": {m.Accuracy.Value:F6}");

                if (m.ValidationAccuracy.HasValue)
                    json.Append($", \"validationAccuracy\": {m.ValidationAccuracy.Value:F6}");

                json.AppendLine(i < metrics.Count - 1 ? "}," : "}");
            }

            json.AppendLine("  ]");
            json.AppendLine("}");

            return json.ToString();
        }

        /// <summary>
        /// JSON 문자열에서 데이터를 로드합니다.
        /// </summary>
        public static ChartDataModel FromJson(string json)
        {
            try
            {
                var model = new ChartDataModel();

                // 간단한 JSON 파싱 (정규식 사용)
                var modelTypeMatch = System.Text.RegularExpressions.Regex.Match(json, @"""modelType"":\s*""([^""]*)""\s*[,}]");
                if (modelTypeMatch.Success)
                    model.ModelType = modelTypeMatch.Groups[1].Value;

                var totalEpochsMatch = System.Text.RegularExpressions.Regex.Match(json, @"""totalEpochs"":\s*(\d+)");
                if (totalEpochsMatch.Success)
                    model.TotalEpochs = int.Parse(totalEpochsMatch.Groups[1].Value);

                var totalFramesMatch = System.Text.RegularExpressions.Regex.Match(json, @"""totalFrames"":\s*(\d+)");
                if (totalFramesMatch.Success)
                    model.TotalFrames = int.Parse(totalFramesMatch.Groups[1].Value);

                // 메트릭 데이터 파싱
                var metricsMatch = System.Text.RegularExpressions.Regex.Match(json, @"""metrics"":\s*\[(.*?)\]", System.Text.RegularExpressions.RegexOptions.Singleline);
                if (metricsMatch.Success)
                {
                    var metricsStr = metricsMatch.Groups[1].Value;
                    var metricMatches = System.Text.RegularExpressions.Regex.Matches(metricsStr, @"\{[^}]+\}");

                    foreach (System.Text.RegularExpressions.Match metricMatch in metricMatches)
                    {
                        var metricStr = metricMatch.Value;
                        int epoch = 0;
                        float trainLoss = 0f;
                        float? valLoss = null;
                        float? accuracy = null;
                        float? valAccuracy = null;

                        var epochMatch = System.Text.RegularExpressions.Regex.Match(metricStr, @"""epoch"":\s*(\d+)");
                        if (epochMatch.Success)
                            epoch = int.Parse(epochMatch.Groups[1].Value);

                        var trainLossMatch = System.Text.RegularExpressions.Regex.Match(metricStr, @"""trainLoss"":\s*([\d.]+)");
                        if (trainLossMatch.Success)
                            trainLoss = float.Parse(trainLossMatch.Groups[1].Value);

                        var valLossMatch = System.Text.RegularExpressions.Regex.Match(metricStr, @"""validationLoss"":\s*([\d.]+)");
                        if (valLossMatch.Success)
                            valLoss = float.Parse(valLossMatch.Groups[1].Value);

                        var accMatch = System.Text.RegularExpressions.Regex.Match(metricStr, @"""accuracy"":\s*([\d.]+)");
                        if (accMatch.Success)
                            accuracy = float.Parse(accMatch.Groups[1].Value);

                        var valAccMatch = System.Text.RegularExpressions.Regex.Match(metricStr, @"""validationAccuracy"":\s*([\d.]+)");
                        if (valAccMatch.Success)
                            valAccuracy = float.Parse(valAccMatch.Groups[1].Value);

                        model.AddMetric(epoch, trainLoss, valLoss, accuracy, valAccuracy);
                    }
                }

                return model;
            }
            catch
            {
                return new ChartDataModel();
            }
        }
    }
}
