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
            try
            {
                // 다양한 키 형식으로 throttle 값을 찾아봅니다
                string[] possibleKeys = { "throttle", "user/throttle", "user_throttle", "Throttle" };

                foreach (var key in possibleKeys)
                {
                    if (Metadata != null && Metadata.TryGetValue(key, out var throttleObj))
                    {
                        if (throttleObj != null)
                        {
                            string valueStr = throttleObj.ToString().Trim();

                            // JSON 리터럴 형식 제거 (큰따옴표 제거)
                            valueStr = valueStr.Trim('"');

                            // 숫자 파싱 시도
                            if (double.TryParse(valueStr, out double throttle))
                                return throttle;
                        }
                    }
                }

                return 0.0;
            }
            catch
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Metadata에서 angle 값을 추출합니다.
        /// </summary>
        public double GetAngle()
        {
            try
            {
                // 다양한 키 형식으로 angle 값을 찾아봅니다
                string[] possibleKeys = { "angle", "user/angle", "user_angle", "Angle", "steering" };

                foreach (var key in possibleKeys)
                {
                    if (Metadata != null && Metadata.TryGetValue(key, out var angleObj))
                    {
                        if (angleObj != null)
                        {
                            string valueStr = angleObj.ToString().Trim();

                            // JSON 리터럴 형식 제거 (큰따옴표 제거)
                            valueStr = valueStr.Trim('"');

                            // 숫자 파싱 시도
                            if (double.TryParse(valueStr, out double angle))
                                return angle;
                        }
                    }
                }

                return 0.0;
            }
            catch
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Metadata에서 disable 값을 추출합니다.
        /// </summary>
        public bool GetDisable()
        {
            try
            {
                // 다양한 키 형식으로 disable 값을 찾아봅니다
                string[] possibleKeys = { "disable", "user/disable", "user_disable", "Disable" };

                foreach (var key in possibleKeys)
                {
                    if (Metadata != null && Metadata.TryGetValue(key, out var disableObj))
                    {
                        if (disableObj != null)
                        {
                            string valueStr = disableObj.ToString().Trim();

                            // JSON 리터럴 형식 제거 (큰따옴표 제거)
                            valueStr = valueStr.Trim('"');

                            // 불린 파싱 시도
                            if (bool.TryParse(valueStr, out bool disable))
                                return disable;
                        }
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"Frame {FrameNumber}: {ImageFileName}";
        }
    }
}
