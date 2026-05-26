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

        public override string ToString()
        {
            return $"Frame {FrameNumber}: {ImageFileName}";
        }
    }
}
