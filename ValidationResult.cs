using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleDonkeyManager
{
    /// <summary>
    /// 검증(추론) 결과 — 프레임별 실제값/AI 예측값/오차를 보관합니다.
    /// </summary>
    public class ValidationResult
    {
        public int Frame { get; set; }
        public string ImagePath { get; set; }
        public double ActualAngle { get; set; }
        public double PredAngle { get; set; }
        public double ActualThrottle { get; set; }
        public double PredThrottle { get; set; }
        public double AngleError { get; set; }
        public double ThrottleError { get; set; }
    }

    /// <summary>
    /// 검증 결과 전체 요약 정보입니다.
    /// </summary>
    public class ValidationSummary
    {
        public int Count { get; set; }
        public double AvgAngleError { get; set; }
        public double MaxAngleError { get; set; }
        public double AvgThrottleError { get; set; }
        public double MaxThrottleError { get; set; }
        public string Verdict { get; set; }

        public static ValidationSummary FromResults(List<ValidationResult> results)
        {
            var summary = new ValidationSummary();
            if (results == null || results.Count == 0)
            {
                summary.Verdict = "데이터 없음";
                return summary;
            }

            summary.Count = results.Count;
            summary.AvgAngleError = results.Average(r => r.AngleError);
            summary.MaxAngleError = results.Max(r => r.AngleError);
            summary.AvgThrottleError = results.Average(r => r.ThrottleError);
            summary.MaxThrottleError = results.Max(r => r.ThrottleError);

            if (summary.AvgAngleError <= 0.05)
                summary.Verdict = "양호";
            else if (summary.AvgAngleError <= 0.12)
                summary.Verdict = "보통";
            else
                summary.Verdict = "미흡";

            return summary;
        }
    }
}
