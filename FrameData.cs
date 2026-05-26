using System;
using System.Collections.Generic;

namespace SimpleDonkeyManager
{
    /// <summary>
    /// 프레임의 이미지와 메타데이터를 관리하는 클래스
    /// </summary>
    public class FrameData
    {
        /// <summary>
        /// 프레임 번호
        /// </summary>
        public int FrameNumber { get; set; }

        /// <summary>
        /// 이미지 파일 경로
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// JSON 파일 경로
        /// </summary>
        public string JsonPath { get; set; }

        /// <summary>
        /// 이미지 파일명
        /// </summary>
        public string ImageFileName { get; set; }

        /// <summary>
        /// JSON 메타데이터 (Dictionary로 저장)
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; }

        /// <summary>
        /// 이미지 해상도 (가로x세로 포맷)
        /// </summary>
        public string Resolution { get; set; }

        /// <summary>
        /// 이미지 파일 크기 (바이트)
        /// </summary>
        public long FileSize { get; set; }

        public FrameData()
        {
            Metadata = new Dictionary<string, object>();
        }

        /// <summary>
        /// Metadata에서 throttle 값을 추출합니다.
        /// </summary>
        public double GetThrottle()
        {
            if (Metadata.TryGetValue("throttle", out var throttleObj))
            {
                if (double.TryParse(throttleObj.ToString(), out double throttle))
                    return throttle;
            }
            return 0.0;
        }

        /// <summary>
        /// Metadata에서 angle 값을 추출합니다.
        /// </summary>
        public double GetAngle()
        {
            if (Metadata.TryGetValue("angle", out var angleObj))
            {
                if (double.TryParse(angleObj.ToString(), out double angle))
                    return angle;
            }
            return 0.0;
        }

        /// <summary>
        /// Metadata에서 disable 값을 추출합니다.
        /// </summary>
        public bool GetDisable()
        {
            if (Metadata.TryGetValue("disable", out var disableObj))
            {
                if (bool.TryParse(disableObj.ToString(), out bool disable))
                    return disable;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Frame {FrameNumber}: {ImageFileName}";
        }
    }
}
