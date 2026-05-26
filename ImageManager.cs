using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SimpleDonkeyManager
{
    /// <summary>
    /// 이미지와 JSON 데이터를 관리하는 클래스
    /// </summary>
    public class ImageManager
    {
        private List<FrameData> frameDataList = new List<FrameData>();
        private string selectedFolderPath;

        /// <summary>
        /// 선택한 폴더 경로
        /// </summary>
        public string SelectedFolderPath
        {
            get { return selectedFolderPath; }
        }

        /// <summary>
        /// 로드된 프레임 데이터 목록
        /// </summary>
        public List<FrameData> FrameDataList
        {
            get { return frameDataList; }
        }

        /// <summary>
        /// 선택된 폴더에서 이미지 및 JSON 파일을 스캔합니다.
        /// </summary>
        public bool ScanFolder(string folderPath)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                    return false;

                selectedFolderPath = folderPath;
                frameDataList.Clear();

                // .jpg 이미지 파일 찾기
                string[] imageFiles = Directory.GetFiles(folderPath, "*.jpg");

                foreach (string imagePath in imageFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(imagePath);

                    // 파일명에서 프레임 번호 추출 (예: 390_cam-image_array_ -> 390)
                    string frameNumberStr = ExtractFrameNumber(fileName);

                    if (int.TryParse(frameNumberStr, out int frameNumber))
                    {
                        FrameData frameData = new FrameData
                        {
                            FrameNumber = frameNumber,
                            ImagePath = imagePath,
                            ImageFileName = Path.GetFileName(imagePath),
                            FileSize = new FileInfo(imagePath).Length
                        };

                        // 대응하는 JSON 파일 찾기
                        string jsonPath = Path.Combine(folderPath, $"record_{frameNumber}.json");
                        if (File.Exists(jsonPath))
                        {
                            frameData.JsonPath = jsonPath;
                            LoadFrameMetadata(frameData);
                        }

                        // 이미지 해상도 가져오기
                        try
                        {
                            using (var img = Image.FromFile(imagePath))
                            {
                                frameData.Resolution = $"{img.Width}x{img.Height}";
                            }
                        }
                        catch
                        {
                            frameData.Resolution = "Unknown";
                        }

                        frameDataList.Add(frameData);
                    }
                }

                // 프레임 번호 순서로 정렬
                frameDataList = frameDataList.OrderBy(f => f.FrameNumber).ToList();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 파일명에서 프레임 번호를 추출합니다.
        /// </summary>
        private string ExtractFrameNumber(string fileName)
        {
            // "390_cam-image_array_" 형식에서 숫자 부분만 추출
            var match = System.Text.RegularExpressions.Regex.Match(fileName, @"^(\d+)");
            return match.Success ? match.Groups[1].Value : "";
        }

        /// <summary>
        /// JSON 파일에서 프레임 메타데이터를 로드합니다.
        /// </summary>
        private void LoadFrameMetadata(FrameData frameData)
        {
            try
            {
                if (string.IsNullOrEmpty(frameData.JsonPath) || !File.Exists(frameData.JsonPath))
                    return;

                string jsonContent = File.ReadAllText(frameData.JsonPath);
                using (JsonDocument doc = JsonDocument.Parse(jsonContent))
                {
                    foreach (var property in doc.RootElement.EnumerateObject())
                    {
                        frameData.Metadata[property.Name] = property.Value.GetRawText();
                    }
                }
            }
            catch
            {
                // JSON 파일 읽기 실패 시 무시
            }
        }

        /// <summary>
        /// 폴더의 통계 정보를 반환합니다.
        /// </summary>
        public FolderStatistics GetFolderStatistics()
        {
            var stats = new FolderStatistics();

            if (frameDataList.Count == 0)
                return stats;

            stats.TotalImageCount = frameDataList.Count;
            stats.TotalFileSize = frameDataList.Sum(f => f.FileSize);

            // 이미지 형식 (모두 jpg)
            stats.ImageFormats = new[] { "JPG" };

            // 해상도 정보
            var resolutions = frameDataList.Select(f => f.Resolution).Distinct().ToList();
            stats.Resolutions = resolutions;

            return stats;
        }

        /// <summary>
        /// 특정 프레임의 데이터를 가져옵니다.
        /// </summary>
        public FrameData GetFrameData(int index)
        {
            if (index >= 0 && index < frameDataList.Count)
                return frameDataList[index];
            return null;
        }

        /// <summary>
        /// 전체 프레임 데이터를 반환합니다.
        /// </summary>
        public List<FrameData> GetAllFrameData()
        {
            return new List<FrameData>(frameDataList);
        }
    }

    /// <summary>
    /// 폴더 통계 정보
    /// </summary>
    public class FolderStatistics
    {
        public int TotalImageCount { get; set; }
        public long TotalFileSize { get; set; }
        public string[] ImageFormats { get; set; }
        public List<string> Resolutions { get; set; }

        public FolderStatistics()
        {
            ImageFormats = new string[] { };
            Resolutions = new List<string>();
        }

        /// <summary>
        /// 파일 크기를 포맷팅합니다 (MB, GB 등)
        /// </summary>
        public string GetFormattedFileSize()
        {
            return FormatBytes(TotalFileSize);
        }

        private static string FormatBytes(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }
}
