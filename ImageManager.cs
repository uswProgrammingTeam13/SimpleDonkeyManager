using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace SimpleDonkeyManager
{
    /// <summary>
    /// 이미지와 JSON 데이터를 관리하는 클래스.
    /// 데이터를 메모리에 사본으로 보관하지 않고 폴더 경로를 기준으로 실제 파일을 다룹니다.
    /// </summary>
    public class ImageManager
    {
        private List<FrameData> frameDataList = new List<FrameData>();

        /// <summary>
        /// 사용자가 선택한 폴더 경로 (data 루트 또는 images 폴더).
        /// </summary>
        private string selectedFolderPath;

        /// <summary>
        /// 이미지 및 identifier 파일이 실제로 들어 있는 폴더 경로.
        /// data 폴더를 선택한 경우 하위 images 폴더, images 폴더를 선택한 경우 자기 자신.
        /// </summary>
        private string imagesFolderPath;

        /// <summary>
        /// 대표 catalog 파일 경로 (다중 catalog 중 첫 번째, 호환성 유지용. 없을 수 있음).
        /// </summary>
        private string catalogPath;

        /// <summary>
        /// 폴더 내 모든 catalog 파일 경로 (catalog_0.catalog, catalog_1.catalog ...).
        /// </summary>
        private readonly List<string> catalogPaths = new List<string>();

        /// <summary>
        /// tub v2 manifest.json 경로 (없으면 null). 삭제는 이 파일의 deleted_indexes로 반영됩니다.
        /// </summary>
        private string manifestPath;

        /// <summary>
        /// 이미지 파일명 -> 전역 catalog _index 매핑. manifest deleted_indexes 갱신에 사용합니다.
        /// </summary>
        private Dictionary<string, long> indexByImageName = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 삭제된 프레임을 백업하는 filtered 폴더 경로.
        /// </summary>
        private string filteredFolderPath;

        /// <summary>
        /// 스냅샷/백업 등 프로그램 관리 데이터를 보관하는 SimpleDonkeyManager 폴더 경로.
        /// 이미지 폴더 하위에 생성됩니다. (예: {images}/SimpleDonkeyManager)
        /// </summary>
        private string managerFolderPath;

        /// <summary>
        /// 직전 삭제(RemoveFrames) 때 filtered 폴더로 이동된 파일들의 파일명 목록.
        /// "삭제 되돌리기"(UndoLastRemove)에서 이 파일들만 원위치로 복구합니다.
        /// </summary>
        private readonly List<string> lastRemovedFileNames = new List<string>();

        /// <summary>
        /// 직전 삭제 때 manifest.json deleted_indexes에 새로 추가된 전역 _index 목록.
        /// "삭제 되돌리기"에서 이 인덱스들만 deleted_indexes에서 제거합니다.
        /// </summary>
        private readonly List<long> lastRemovedManifestIndexes = new List<long>();

        /// <summary>
        /// identifier 파일 확장자 (.json 또는 .identifier 등). 폴더 내 규칙은 일관됨.
        /// </summary>
        private string identifierExtension;

        /// <summary>
        /// 이미지 파일명 -> identifier 파일 경로 매핑 (identifier 내부 cam/image_array 기반).
        /// </summary>
        private Dictionary<string, string> identifierByImageName = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 프레임 번호 -> identifier 파일 경로 매핑 (파일명 숫자 기반 폴백용).
        /// </summary>
        private Dictionary<int, string> identifierByFrameNumber = new Dictionary<int, string>();

        /// <summary>
        /// identifier 후보에서 제외할 보조 파일명 (메타/로그 등).
        /// </summary>
        private static readonly string[] NonFrameIdentifierNames = { "meta", "manifest" };

        /// <summary>
        /// 선택한 폴더 경로 (호환성 유지).
        /// </summary>
        public string SelectedFolderPath
        {
            get { return selectedFolderPath; }
        }

        /// <summary>
        /// 이미지 및 identifier 파일이 위치한 폴더 경로.
        /// </summary>
        public string ImagesFolderPath
        {
            get { return imagesFolderPath; }
        }

        /// <summary>
        /// catalog 파일 경로 (없으면 null).
        /// </summary>
        public string CatalogPath
        {
            get { return catalogPath; }
        }

        /// <summary>
        /// catalog 파일이 존재하는지 여부.
        /// </summary>
        public bool HasCatalog
        {
            get { return catalogPaths.Count > 0 && catalogPaths.Any(File.Exists); }
        }

        /// <summary>
        /// manifest.json(tub v2)이 존재하는지 여부.
        /// </summary>
        public bool HasManifest
        {
            get { return !string.IsNullOrEmpty(manifestPath) && File.Exists(manifestPath); }
        }

        /// <summary>
        /// 삭제된 프레임 백업 폴더 경로.
        /// </summary>
        public string FilteredFolderPath
        {
            get { return filteredFolderPath; }
        }

        /// <summary>
        /// 스냅샷/백업 등 프로그램 관리 데이터를 보관하는 SimpleDonkeyManager 폴더 경로.
        /// 폴더가 아직 없으면 생성합니다. 데이터 폴더가 로드되지 않은 경우 null을 반환합니다.
        /// </summary>
        public string ManagerFolderPath
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(managerFolderPath))
                    {
                        if (string.IsNullOrEmpty(imagesFolderPath))
                            return null;
                        managerFolderPath = Path.Combine(imagesFolderPath, "SimpleDonkeyManager");
                    }

                    if (!Directory.Exists(managerFolderPath))
                        Directory.CreateDirectory(managerFolderPath);

                    return managerFolderPath;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"ManagerFolderPath 오류: {ex.Message}");
                    return managerFolderPath;
                }
            }
        }

        /// <summary>
        /// 로드된 프레임 데이터 목록.
        /// </summary>
        public List<FrameData> FrameDataList
        {
            get { return frameDataList; }
        }

        /// <summary>
        /// 선택된 폴더에서 이미지 및 identifier/catalog 파일을 스캔합니다.
        /// data 폴더(하위 images + catalog) 또는 images 폴더(이미지 + identifier)를 모두 지원합니다.
        /// 이미지 + identifier를 찾을 수 없으면 false를 반환합니다.
        /// </summary>
        public bool ScanFolder(string folderPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(folderPath))
                    return false;

                if (!Directory.Exists(folderPath))
                    return false;

                selectedFolderPath = folderPath;
                frameDataList.Clear();
                catalogPath = null;
                identifierExtension = null;
                identifierByImageName.Clear();
                identifierByFrameNumber.Clear();

                // 폴더 구조 판별: 이미지가 들어 있는 폴더와 catalog 경로 결정
                ResolveFolderStructure(folderPath);

                // images 폴더를 결정하지 못하면 실패
                if (string.IsNullOrEmpty(imagesFolderPath) || !Directory.Exists(imagesFolderPath))
                    return false;

                // 관리용 폴더(SimpleDonkeyManager)와 그 하위 filtered 백업 폴더는 이미지 폴더 기준으로 둔다.
                // 기존 {images}/filtered 폴더가 있으면 새 위치로 마이그레이션한다.
                managerFolderPath = Path.Combine(imagesFolderPath, "SimpleDonkeyManager");
                filteredFolderPath = Path.Combine(managerFolderPath, "Filtered");
                MigrateLegacyFilteredFolder();

                try
                {
                    // .jpg 이미지 파일 찾기 (Zone.Identifier 파생 파일 제외)
                    string[] imageFiles = Directory
                        .GetFiles(imagesFolderPath, "*.jpg")
                        .Where(p => !IsZoneIdentifierArtifact(p))
                        .ToArray();

                    if (imageFiles.Length == 0)
                        return false; // 이미지가 없으면 유효한 데이터 폴더가 아님

                    // catalog 메타데이터 미리 로드 (이미지 파일명 -> 메타데이터)
                    Dictionary<string, Dictionary<string, object>> catalogByImage = LoadCatalogMetadata();

                    // manifest.json에 기록된 삭제 인덱스 (학습 시 제외되는 프레임) 로드
                    HashSet<long> deletedIndexes = HasManifest ? ReadDeletedIndexes() : new HashSet<long>();

                    // identifier 파일 인덱스 구축 (이미지명/프레임번호 -> identifier 경로)
                    BuildIdentifierIndex();

                    bool anyIdentifierFound = false;

                    foreach (string imagePath in imageFiles)
                    {
                        try
                        {
                            if (!File.Exists(imagePath))
                                continue;

                            // manifest deleted_indexes에 포함된 프레임은 목록에서 제외 (학습 상태와 동기화)
                            if (deletedIndexes.Count > 0)
                            {
                                string imgKey = Path.GetFileName(imagePath);
                                if (indexByImageName.TryGetValue(imgKey, out long gidx) && deletedIndexes.Contains(gidx))
                                    continue;
                            }

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
                                    FileSize = 0
                                };

                                // 파일 크기 안전하게 가져오기
                                try
                                {
                                    frameData.FileSize = new FileInfo(imagePath).Length;
                                }
                                catch
                                {
                                    frameData.FileSize = 0;
                                }

                                // 대응하는 identifier 파일 찾기 (.json 또는 .identifier 등)
                                string identifierPath = FindIdentifierFile(frameNumber, frameData.ImageFileName);
                                if (!string.IsNullOrEmpty(identifierPath))
                                {
                                    frameData.JsonPath = identifierPath;
                                    LoadFrameMetadata(frameData);
                                    anyIdentifierFound = true;
                                }

                                // catalog 메타데이터 병합 (identifier에 없는 키 보완)
                                MergeCatalogMetadata(frameData, catalogByImage);

                                // 이미지 해상도 가져오기 (JPEG 헤더만 읽어 전체 디코딩 비용 회피)
                                try
                                {
                                    var size = ReadJpegDimensions(imagePath);
                                    frameData.Resolution = size.HasValue
                                        ? $"{size.Value.Width}x{size.Value.Height}"
                                        : "Unknown";
                                }
                                catch
                                {
                                    frameData.Resolution = "Unknown";
                                }

                                frameDataList.Add(frameData);
                            }
                        }
                        catch (Exception ex)
                        {
                            // 개별 이미지 처리 실패 시 계속 진행
                            System.Diagnostics.Debug.WriteLine($"이미지 처리 오류: {imagePath}, {ex.Message}");
                            continue;
                        }
                    }

                    // identifier 파일이 하나도 없고 catalog도 없으면 식별 데이터가 없는 폴더로 간주
                    if (!anyIdentifierFound && !HasCatalog)
                        return false;

                    // 프레임 번호 순서로 정렬
                    if (frameDataList.Count > 0)
                        frameDataList = frameDataList.OrderBy(f => f.FrameNumber).ToList();

                    return true;
                }
                catch (UnauthorizedAccessException)
                {
                    // 접근 권한 없음
                    return false;
                }
                catch (IOException)
                {
                    // I/O 오류
                    return false;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"폴더 스캔 내부 오류: {ex.Message}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"폴더 스캔 오류: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 선택한 폴더의 구조를 판별하여 imagesFolderPath와 catalogPath를 결정합니다.
        /// - data 루트(catalog + 하위 images): imagesFolderPath = ./images, catalogPath = catalog 파일
        /// - images 폴더(이미지 + identifier): imagesFolderPath = 자기 자신, 상위에 catalog가 있으면 연결
        /// </summary>
        private void ResolveFolderStructure(string folderPath)
        {
            imagesFolderPath = folderPath;

            // 현재 폴더에 직접 이미지가 있는지 확인
            bool hasImagesHere = Directory
                .GetFiles(folderPath, "*.jpg")
                .Any(p => !IsZoneIdentifierArtifact(p));

            // 하위 images 폴더 확인
            string subImages = Path.Combine(folderPath, "images");
            bool hasSubImages = Directory.Exists(subImages) &&
                Directory.GetFiles(subImages, "*.jpg").Any(p => !IsZoneIdentifierArtifact(p));

            if (hasImagesHere)
            {
                // 이미지가 현재 폴더에 있음 -> 현재 폴더가 images 폴더 역할
                imagesFolderPath = folderPath;
                // catalog/manifest는 현재 폴더 또는 상위 폴더에서 탐색
                if (!CollectCatalogAndManifest(folderPath))
                    CollectCatalogAndManifest(Path.GetDirectoryName(folderPath));
            }
            else if (hasSubImages)
            {
                // data 루트 폴더 -> 하위 images 폴더 사용
                imagesFolderPath = subImages;
                CollectCatalogAndManifest(folderPath);
            }
            else
            {
                // 이미지를 찾지 못함 -> 그대로 두고 ScanFolder에서 실패 처리
                imagesFolderPath = folderPath;
                CollectCatalogAndManifest(folderPath);
            }
        }

        /// <summary>
        /// 지정한 폴더에서 모든 catalog 파일과 manifest.json 경로를 수집합니다.
        /// catalog를 하나 이상 찾으면 true를 반환합니다.
        /// </summary>
        private bool CollectCatalogAndManifest(string folderPath)
        {
            catalogPaths.Clear();
            catalogPath = null;
            manifestPath = null;

            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
                return false;

            try
            {
                // 1) manifest.json 우선 확인 (tub v2)
                string manifest = Path.Combine(folderPath, "manifest.json");
                if (File.Exists(manifest))
                    manifestPath = manifest;

                // 2) manifest의 catalog paths가 있으면 그 순서를 신뢰
                var fromManifest = ReadCatalogPathsFromManifest(folderPath);
                if (fromManifest.Count > 0)
                {
                    catalogPaths.AddRange(fromManifest);
                }
                else
                {
                    // 3) 폴더에서 *.catalog 파일을 이름 순으로 수집 (catalog_0, catalog_1 ...)
                    var found = Directory.GetFiles(folderPath, "*.catalog")
                        .Where(p => !IsZoneIdentifierArtifact(p))
                        .OrderBy(p => GetCatalogFileOrder(p))
                        .ToList();

                    // .catalog 확장자가 없으면 catalog 이름 포함 파일도 허용
                    if (found.Count == 0)
                    {
                        var alt = Directory.GetFiles(folderPath)
                            .FirstOrDefault(p =>
                            {
                                string name = Path.GetFileNameWithoutExtension(p);
                                return name.IndexOf("catalog", StringComparison.OrdinalIgnoreCase) >= 0
                                    && !IsZoneIdentifierArtifact(p)
                                    && !p.EndsWith(".catalog_manifest", StringComparison.OrdinalIgnoreCase);
                            });
                        if (!string.IsNullOrEmpty(alt))
                            found.Add(alt);
                    }

                    catalogPaths.AddRange(found);
                }

                if (catalogPaths.Count > 0)
                    catalogPath = catalogPaths[0];

                return catalogPaths.Count > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"catalog/manifest 수집 오류: {ex.Message}");
                return catalogPaths.Count > 0;
            }
        }

        /// <summary>
        /// manifest.json의 catalog metadata(5번째 줄)에서 paths 목록을 읽어 절대 경로로 반환합니다.
        /// </summary>
        private List<string> ReadCatalogPathsFromManifest(string folderPath)
        {
            var result = new List<string>();
            try
            {
                string manifest = Path.Combine(folderPath, "manifest.json");
                if (!File.Exists(manifest))
                    return result;

                string[] lines = File.ReadAllLines(manifest);
                if (lines.Length < 5)
                    return result;

                using (JsonDocument doc = JsonDocument.Parse(lines[4]))
                {
                    if (doc.RootElement.ValueKind == JsonValueKind.Object &&
                        doc.RootElement.TryGetProperty("paths", out var pathsEl) &&
                        pathsEl.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var p in pathsEl.EnumerateArray())
                        {
                            string rel = p.GetString();
                            if (string.IsNullOrEmpty(rel))
                                continue;
                            string full = Path.Combine(folderPath, rel);
                            if (File.Exists(full))
                                result.Add(full);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"manifest catalog paths 읽기 오류: {ex.Message}");
            }
            return result;
        }

        /// <summary>
        /// catalog 파일명에서 순번을 추출합니다 (catalog_3.catalog -> 3). 없으면 큰 값.
        /// </summary>
        private int GetCatalogFileOrder(string path)
        {
            try
            {
                string name = Path.GetFileNameWithoutExtension(path);
                string digits = new string(name.Where(char.IsDigit).ToArray());
                if (int.TryParse(digits, out int n))
                    return n;
            }
            catch { }
            return int.MaxValue;
        }

        /// <summary>
        /// 폴더 내 identifier 후보 파일(.json/.identifier 등)을 한 번 스캔하여
        /// 이미지 파일명 및 프레임 번호 기준 인덱스를 구축합니다.
        /// identifier 파일명 규칙(예: record_0.json, 0_cam-image_array_.json)에 무관하게 매칭됩니다.
        /// </summary>
        private void BuildIdentifierIndex()
        {
            try
            {
                identifierByImageName.Clear();
                identifierByFrameNumber.Clear();

                if (string.IsNullOrEmpty(imagesFolderPath) || !Directory.Exists(imagesFolderPath))
                    return;

                // 후보 확장자 (이미지가 아닌 메타데이터 파일)
                string[] extensions = { "*.json", "*.identifier" };

                foreach (string pattern in extensions)
                {
                    string[] candidates;
                    try
                    {
                        candidates = Directory.GetFiles(imagesFolderPath, pattern)
                            .Where(p => !IsZoneIdentifierArtifact(p))
                            .ToArray();
                    }
                    catch
                    {
                        continue;
                    }

                    foreach (string candidate in candidates)
                    {
                        try
                        {
                            string nameNoExt = Path.GetFileNameWithoutExtension(candidate);

                            // meta.json, manifest 등 프레임 identifier가 아닌 보조 파일 제외
                            if (NonFrameIdentifierNames.Any(n => nameNoExt.Equals(n, StringComparison.OrdinalIgnoreCase)))
                                continue;

                            // 1) 파일명에서 프레임 번호 추출하여 매핑 (가장 빠름)
                            string num = ExtractFrameNumberAnywhere(nameNoExt);
                            bool hasFrameNumber = int.TryParse(num, out int frameNumber);
                            if (hasFrameNumber && !identifierByFrameNumber.ContainsKey(frameNumber))
                            {
                                identifierByFrameNumber[frameNumber] = candidate;
                            }

                            // 2) 파일명으로 번호를 얻지 못한 경우에만 내부 cam/image_array 값을 읽어 이미지명 매핑
                            //    (대량 파일에서 불필요한 디스크 I/O 방지)
                            if (!hasFrameNumber)
                            {
                                string imageName = ReadImageNameFromIdentifier(candidate);
                                if (!string.IsNullOrEmpty(imageName))
                                {
                                    string key = Path.GetFileName(imageName);
                                    if (!identifierByImageName.ContainsKey(key))
                                        identifierByImageName[key] = candidate;
                                }
                            }

                            if (string.IsNullOrEmpty(identifierExtension))
                                identifierExtension = Path.GetExtension(candidate);
                        }
                        catch
                        {
                            // 개별 파일 처리 실패 시 계속
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"identifier 인덱스 구축 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// identifier 파일 내부의 cam/image_array 값을 읽어 이미지 파일명을 반환합니다.
        /// </summary>
        private string ReadImageNameFromIdentifier(string identifierPath)
        {
            try
            {
                string content = File.ReadAllText(identifierPath);
                if (string.IsNullOrWhiteSpace(content))
                    return null;

                using (JsonDocument doc = JsonDocument.Parse(content))
                {
                    if (doc.RootElement.ValueKind != JsonValueKind.Object)
                        return null;

                    foreach (var property in doc.RootElement.EnumerateObject())
                    {
                        if (property.Name.Equals("cam/image_array", StringComparison.OrdinalIgnoreCase))
                        {
                            return property.Value.ValueKind == JsonValueKind.String
                                ? property.Value.GetString()
                                : property.Value.GetRawText().Trim('"');
                        }
                    }
                }
            }
            catch
            {
                // 파싱 실패 시 null (파일명 기반 폴백 사용)
            }

            return null;
        }

        /// <summary>
        /// 프레임에 대응하는 identifier 파일을 인덱스에서 찾습니다.
        /// 1순위: identifier 내부 cam/image_array == 이미지 파일명
        /// 2순위: identifier 파일명에 포함된 프레임 번호
        /// </summary>
        private string FindIdentifierFile(int frameNumber, string imageFileName)
        {
            try
            {
                // 1순위: 이미지 파일명으로 직접 매칭
                if (!string.IsNullOrEmpty(imageFileName) &&
                    identifierByImageName.TryGetValue(imageFileName, out string byImage) &&
                    File.Exists(byImage))
                {
                    return byImage;
                }

                // 2순위: 프레임 번호로 폴백 매칭
                if (identifierByFrameNumber.TryGetValue(frameNumber, out string byNumber) &&
                    File.Exists(byNumber))
                {
                    return byNumber;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Zone.Identifier 대체 데이터 스트림에서 파생된 잡 파일인지 확인합니다.
        /// </summary>
        private static bool IsZoneIdentifierArtifact(string path)
        {
            string name = Path.GetFileName(path) ?? "";
            return name.IndexOf("Zone.Identifier", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// JPEG 파일의 SOF 마커만 파싱하여 해상도를 빠르게 읽습니다.
        /// 전체 픽셀을 디코딩하지 않으므로 대량 이미지 스캔 시 훨씬 빠릅니다.
        /// 파싱에 실패하면 null을 반환합니다.
        /// </summary>
        private static Size? ReadJpegDimensions(string path)
        {
            try
            {
                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan);
                using var reader = new BinaryReader(fs);

                // SOI 마커(0xFFD8) 확인
                if (reader.ReadByte() != 0xFF || reader.ReadByte() != 0xD8)
                    return null;

                while (fs.Position < fs.Length)
                {
                    // 마커 시작(0xFF) 찾기
                    byte b = reader.ReadByte();
                    if (b != 0xFF)
                        continue;

                    // 0xFF 패딩 스킵
                    byte marker = reader.ReadByte();
                    while (marker == 0xFF)
                        marker = reader.ReadByte();

                    // SOF0~SOF15 (0xC0~0xCF), 단 0xC4(DHT), 0xC8(JPG), 0xCC(DAC) 제외
                    if (marker >= 0xC0 && marker <= 0xCF &&
                        marker != 0xC4 && marker != 0xC8 && marker != 0xCC)
                    {
                        reader.ReadByte(); reader.ReadByte(); // 세그먼트 길이
                        reader.ReadByte();                    // 정밀도(precision)
                        int height = (reader.ReadByte() << 8) | reader.ReadByte();
                        int width = (reader.ReadByte() << 8) | reader.ReadByte();
                        return new Size(width, height);
                    }

                    // SOS(0xDA)에 도달하면 이미지 데이터 시작 -> 중단
                    if (marker == 0xDA)
                        break;

                    // 그 외 세그먼트는 길이만큼 건너뛰기
                    int segLen = (reader.ReadByte() << 8) | reader.ReadByte();
                    if (segLen < 2)
                        break;
                    fs.Seek(segLen - 2, SeekOrigin.Current);
                }
            }
            catch
            {
                // 파싱 실패 시 호출부에서 Unknown 처리
            }

            return null;
        }

        /// <summary>
        /// 모든 catalog 파일(JSON Lines)을 읽어 이미지 파일명 -> 메타데이터 사전을 만듭니다.
        /// 동시에 이미지 파일명 -> 전역 _index 매핑(indexByImageName)도 구축합니다.
        /// </summary>
        private Dictionary<string, Dictionary<string, object>> LoadCatalogMetadata()
        {
            var result = new Dictionary<string, Dictionary<string, object>>(StringComparer.OrdinalIgnoreCase);
            indexByImageName.Clear();

            try
            {
                if (!HasCatalog)
                    return result;

                foreach (string catalog in catalogPaths)
                {
                    if (string.IsNullOrEmpty(catalog) || !File.Exists(catalog))
                        continue;

                    string[] lines = File.ReadAllLines(catalog);
                    foreach (string line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        try
                        {
                            using (JsonDocument doc = JsonDocument.Parse(line))
                            {
                                var meta = new Dictionary<string, object>();
                                string imageName = null;
                                long? recordIndex = null;

                                foreach (var property in doc.RootElement.EnumerateObject())
                                {
                                    meta[property.Name] = property.Value.GetRawText();
                                    if (property.Name.Equals("cam/image_array", StringComparison.OrdinalIgnoreCase))
                                    {
                                        imageName = property.Value.ValueKind == JsonValueKind.String
                                            ? property.Value.GetString()
                                            : property.Value.GetRawText().Trim('"');
                                    }
                                    else if (property.Name.Equals("_index", StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (property.Value.ValueKind == JsonValueKind.Number && property.Value.TryGetInt64(out long idx))
                                            recordIndex = idx;
                                        else if (long.TryParse(property.Value.GetRawText().Trim('"'), out long parsed))
                                            recordIndex = parsed;
                                    }
                                }

                                if (!string.IsNullOrEmpty(imageName))
                                {
                                    string key = Path.GetFileName(imageName);
                                    result[key] = meta;
                                    if (recordIndex.HasValue)
                                        indexByImageName[key] = recordIndex.Value;
                                }
                            }
                        }
                        catch
                        {
                            // 개별 줄 파싱 실패 시 계속
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"catalog 로드 오류: {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// catalog 메타데이터를 프레임에 병합합니다. identifier에 이미 있는 키는 유지합니다.
        /// </summary>
        private void MergeCatalogMetadata(FrameData frameData, Dictionary<string, Dictionary<string, object>> catalogByImage)
        {
            try
            {
                if (frameData == null || catalogByImage == null || catalogByImage.Count == 0)
                    return;

                if (frameData.Metadata == null)
                    frameData.Metadata = new Dictionary<string, object>();

                if (catalogByImage.TryGetValue(frameData.ImageFileName, out var meta) && meta != null)
                {
                    foreach (var kv in meta)
                    {
                        if (!frameData.Metadata.ContainsKey(kv.Key))
                            frameData.Metadata[kv.Key] = kv.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"catalog 병합 오류: {ex.Message}");
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
        /// 파일명 내 어느 위치에 있든 첫 번째 숫자 그룹을 프레임 번호로 추출합니다.
        /// 예: "record_0" -> "0", "0_cam-image_array_" -> "0"
        /// </summary>
        private string ExtractFrameNumberAnywhere(string fileName)
        {
            var match = System.Text.RegularExpressions.Regex.Match(fileName ?? "", @"(\d+)");
            return match.Success ? match.Groups[1].Value : "";
        }

        /// <summary>
        /// JSON 파일에서 프레임 메타데이터를 로드합니다.
        /// </summary>
        private void LoadFrameMetadata(FrameData frameData)
        {
            if (frameData == null)
                return;

            try
            {
                if (string.IsNullOrEmpty(frameData.JsonPath) || !File.Exists(frameData.JsonPath))
                    return;

                string jsonContent = File.ReadAllText(frameData.JsonPath);

                if (string.IsNullOrWhiteSpace(jsonContent))
                    return;

                using (JsonDocument doc = JsonDocument.Parse(jsonContent))
                {
                    if (doc?.RootElement != null)
                    {
                        foreach (var property in doc.RootElement.EnumerateObject())
                        {
                            try
                            {
                                if (frameData.Metadata != null)
                                {
                                    frameData.Metadata[property.Name] = property.Value.GetRawText();
                                }
                            }
                            catch
                            {
                                // 개별 속성 파싱 실패 시 계속
                                continue;
                            }
                        }
                    }
                }
            }
            catch (JsonException)
            {
                // JSON 파싱 오류 - 메타데이터 없이 계속
            }
            catch (IOException)
            {
                // 파일 읽기 오류
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"메타데이터 로드 오류: {ex.Message}");
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
            try
            {
                if (frameDataList == null || frameDataList.Count == 0)
                    return null;

                if (index >= 0 && index < frameDataList.Count)
                    return frameDataList[index];

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"프레임 데이터 조회 오류: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 전체 프레임 데이터를 반환합니다.
        /// </summary>
        public List<FrameData> GetAllFrameData()
        {
            try
            {
                if (frameDataList == null)
                    return new List<FrameData>();

                return new List<FrameData>(frameDataList);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"프레임 데이터 전체 조회 오류: {ex.Message}");
                return new List<FrameData>();
            }
        }

        /// <summary>
        /// 지정한 프레임 번호 하나를 실제 폴더에서 삭제하고 filtered 폴더로 백업합니다.
        /// </summary>
        public bool RemoveFrame(int frameNumber)
        {
            return RemoveFrames(new List<int> { frameNumber }) > 0;
        }

        /// <summary>
        /// 지정한 프레임 번호들을 실제 폴더에서 삭제합니다.
        /// 이미지/identifier 파일을 filtered 폴더로 이동하고, catalog에서 해당 줄을 제거하여
        /// filtered 폴더의 백업 catalog에 누적 저장합니다.
        /// 반환값은 실제로 삭제된 프레임 수입니다.
        /// </summary>
        public int RemoveFrames(IEnumerable<int> frameNumbers)
        {
            int removed = 0;

            try
            {
                if (frameNumbers == null)
                    return 0;

                var targetSet = new HashSet<int>(frameNumbers);
                if (targetSet.Count == 0)
                    return 0;

                if (string.IsNullOrEmpty(imagesFolderPath) || !Directory.Exists(imagesFolderPath))
                    return 0;

                EnsureFilteredFolder();

                // 직전 삭제 내역 초기화 (이번 삭제만 되돌릴 수 있도록)
                lastRemovedFileNames.Clear();
                lastRemovedManifestIndexes.Clear();

                // 삭제 대상 프레임의 이미지 파일명 수집 (manifest 인덱스 매칭용)
                var removedImageNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach (int frameNumber in targetSet)
                {
                    var frame = frameDataList.FirstOrDefault(f => f != null && f.FrameNumber == frameNumber);

                    // 이미지 파일 이동
                    string imagePath = frame?.ImagePath;
                    if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
                    {
                        // 메모리에 없으면 폴더에서 직접 탐색
                        imagePath = Directory.GetFiles(imagesFolderPath, "*.jpg")
                            .Where(p => !IsZoneIdentifierArtifact(p))
                            .FirstOrDefault(p => int.TryParse(ExtractFrameNumber(Path.GetFileNameWithoutExtension(p)), out int n) && n == frameNumber);
                    }

                    bool movedAny = false;

                    if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                    {
                        removedImageNames.Add(Path.GetFileName(imagePath));
                        MoveToFiltered(imagePath);
                        lastRemovedFileNames.Add(Path.GetFileName(imagePath));
                        movedAny = true;
                    }

                    // identifier 파일 이동
                    string identifierPath = frame?.JsonPath;
                    if (string.IsNullOrEmpty(identifierPath) || !File.Exists(identifierPath))
                        identifierPath = FindIdentifierFile(frameNumber, frame?.ImageFileName ?? (imagePath != null ? Path.GetFileName(imagePath) : null));

                    if (!string.IsNullOrEmpty(identifierPath) && File.Exists(identifierPath))
                    {
                        MoveToFiltered(identifierPath);
                        lastRemovedFileNames.Add(Path.GetFileName(identifierPath));
                        movedAny = true;
                    }

                    if (movedAny)
                    {
                        // 메모리 목록에서도 제거
                        frameDataList.RemoveAll(f => f != null && f.FrameNumber == frameNumber);
                        removed++;
                    }
                }

                // tub v2: catalog 파일은 건드리지 않고 manifest.json의 deleted_indexes에
                // 삭제된 프레임의 전역 _index를 추가하여 학습 시 해당 레코드를 건너뛰게 합니다.
                if (HasManifest && removedImageNames.Count > 0)
                {
                    var existing = ReadDeletedIndexes();

                    foreach (string imgName in removedImageNames)
                    {
                        if (indexByImageName.TryGetValue(imgName, out long gidx) && existing.Add(gidx))
                        {
                            // 이번 삭제로 새로 추가된 인덱스만 되돌리기용으로 추적
                            lastRemovedManifestIndexes.Add(gidx);
                        }
                    }

                    if (lastRemovedManifestIndexes.Count > 0)
                        WriteDeletedIndexes(existing);
                }

                return removed;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"프레임 삭제 오류: {ex.Message}");
                return removed;
            }
        }

        /// <summary>
        /// filtered 폴더에 백업된 모든 파일(이미지/identifier)을 원래 위치로 복구하고,
        /// 백업된 이미지에 해당하는 전역 _index를 manifest.json의 deleted_indexes에서 제거합니다.
        /// 복구 후 폴더를 재스캔하여 메모리 목록을 갱신합니다.
        /// </summary>
        public bool RestoreAllFrames()
        {
            try
            {
                if (string.IsNullOrEmpty(filteredFolderPath) || !Directory.Exists(filteredFolderPath))
                    return false;

                if (string.IsNullOrEmpty(imagesFolderPath) || !Directory.Exists(imagesFolderPath))
                    return false;

                // 1) 백업된 이미지 파일명을 수집 (deleted_indexes 복구용)
                var restoredImageNames = Directory.GetFiles(filteredFolderPath, "*.jpg")
                    .Where(p => !IsZoneIdentifierArtifact(p))
                    .Select(Path.GetFileName)
                    .ToList();

                // 2) 이미지/identifier 파일을 원위치로 이동
                foreach (string filePath in Directory.GetFiles(filteredFolderPath))
                {
                    try
                    {
                        string fileName = Path.GetFileName(filePath);

                        string destPath = Path.Combine(imagesFolderPath, fileName);
                        if (File.Exists(destPath))
                            File.Delete(destPath);

                        File.Move(filePath, destPath);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"파일 복구 오류: {filePath}, {ex.Message}");
                    }
                }

                // 3) manifest.json의 deleted_indexes에서 복구된 이미지의 전역 _index 제거
                //    (인덱스 매핑이 최신 상태가 되도록 catalog 메타데이터를 먼저 다시 로드)
                if (HasManifest)
                {
                    LoadCatalogMetadata();
                    var deleted = ReadDeletedIndexes();
                    bool changed = false;
                    foreach (string imgName in restoredImageNames)
                    {
                        if (indexByImageName.TryGetValue(imgName, out long gidx) && deleted.Remove(gidx))
                            changed = true;
                    }
                    if (changed)
                        WriteDeletedIndexes(deleted);
                }

                // 4) 폴더 재스캔
                ScanFolder(selectedFolderPath);

                // 전체 복구되었으므로 직전 삭제 되돌리기 내역도 무효화
                lastRemovedFileNames.Clear();
                lastRemovedManifestIndexes.Clear();

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"전체 복구 오류: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 직전 삭제(RemoveFrame/RemoveFrames)로 filtered 폴더에 백업된 파일을 원위치로 복구하고,
        /// 직전 삭제로 추가된 전역 _index를 manifest.json의 deleted_indexes에서 제거합니다.
        /// 직전 1회 삭제만 되돌립니다. 복구 후 폴더를 재스캔하여 메모리 목록을 갱신합니다.
        /// 되돌릴 직전 삭제 내역이 없으면 false를 반환합니다.
        /// </summary>
        public bool UndoLastRemove()
        {
            try
            {
                if (lastRemovedFileNames.Count == 0 && lastRemovedManifestIndexes.Count == 0)
                    return false;

                if (string.IsNullOrEmpty(imagesFolderPath) || !Directory.Exists(imagesFolderPath))
                    return false;

                // 1) 직전 삭제된 이미지/identifier 파일을 원위치로 이동
                if (!string.IsNullOrEmpty(filteredFolderPath) && Directory.Exists(filteredFolderPath))
                {
                    foreach (string fileName in lastRemovedFileNames)
                    {
                        try
                        {
                            string sourcePath = Path.Combine(filteredFolderPath, fileName);
                            if (!File.Exists(sourcePath))
                                continue;

                            string destPath = Path.Combine(imagesFolderPath, fileName);
                            if (File.Exists(destPath))
                                File.Delete(destPath);

                            File.Move(sourcePath, destPath);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"되돌리기 파일 복구 오류: {fileName}, {ex.Message}");
                        }
                    }
                }

                // 2) 직전 삭제로 추가된 전역 _index를 manifest deleted_indexes에서 제거
                if (HasManifest && lastRemovedManifestIndexes.Count > 0)
                {
                    var deleted = ReadDeletedIndexes();
                    bool changed = false;
                    foreach (long gidx in lastRemovedManifestIndexes)
                    {
                        if (deleted.Remove(gidx))
                            changed = true;
                    }
                    if (changed)
                        WriteDeletedIndexes(deleted);
                }

                // 3) 직전 삭제 내역 비우기 (한 번만 되돌릴 수 있음)
                lastRemovedFileNames.Clear();
                lastRemovedManifestIndexes.Clear();

                // 4) 폴더 재스캔
                ScanFolder(selectedFolderPath);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"직전 삭제 되돌리기 오류: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 현재 filtered 폴더에 백업되어 있는(=현재 삭제 상태인) 프레임 번호 집합을 반환합니다.
        /// 스냅샷 저장 시 현재 상태를 표현하는 기준이 됩니다.
        /// </summary>
        public List<int> GetCurrentDeletedFrameNumbers()
        {
            var result = new List<int>();
            try
            {
                if (string.IsNullOrEmpty(filteredFolderPath) || !Directory.Exists(filteredFolderPath))
                    return result;

                var set = new HashSet<int>();
                foreach (string filePath in Directory.GetFiles(filteredFolderPath, "*.jpg"))
                {
                    if (IsZoneIdentifierArtifact(filePath))
                        continue;

                    string num = ExtractFrameNumber(Path.GetFileNameWithoutExtension(filePath));
                    if (int.TryParse(num, out int n))
                        set.Add(n);
                }

                result = set.OrderBy(x => x).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"현재 삭제 프레임 집합 조회 오류: {ex.Message}");
            }
            return result;
        }

        /// <summary>
        /// 데이터셋을 목표 삭제 프레임 집합 상태로 전환합니다.
        /// 현재 삭제된 모든 프레임을 원위치로 복구한 뒤, 목표 프레임들을 다시 삭제합니다.
        /// 스냅샷(특정 시점)으로 되돌릴 때 사용합니다.
        /// </summary>
        public bool ApplyDeletedFrameNumbers(IEnumerable<int> targetFrameNumbers)
        {
            try
            {
                if (string.IsNullOrEmpty(imagesFolderPath) || !Directory.Exists(imagesFolderPath))
                    return false;

                var target = new HashSet<int>(targetFrameNumbers ?? Enumerable.Empty<int>());

                // 1) 현재 삭제 상태를 모두 복구 (filtered 폴더 비우기)
                if (!string.IsNullOrEmpty(filteredFolderPath) && Directory.Exists(filteredFolderPath)
                    && Directory.GetFiles(filteredFolderPath).Length > 0)
                {
                    RestoreAllFrames(); // 내부에서 ScanFolder 호출
                }
                else
                {
                    // 복구할 게 없으면 메모리 일관성을 위해 재스캔
                    ScanFolder(selectedFolderPath);
                }

                // 2) 목표 프레임 삭제 적용
                if (target.Count > 0)
                    RemoveFrames(target);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"삭제 프레임 집합 적용 오류: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 되돌릴 수 있는 직전 삭제 내역이 있는지 여부.
        /// </summary>
        public bool CanUndoLastRemove
        {
            get { return lastRemovedFileNames.Count > 0 || lastRemovedManifestIndexes.Count > 0; }
        }

        /// <summary>
        /// filtered 백업 폴더를 생성합니다.
        /// </summary>
        private void EnsureFilteredFolder()
        {
            try
            {
                if (string.IsNullOrEmpty(managerFolderPath) && !string.IsNullOrEmpty(imagesFolderPath))
                    managerFolderPath = Path.Combine(imagesFolderPath, "SimpleDonkeyManager");

                if (string.IsNullOrEmpty(filteredFolderPath) && !string.IsNullOrEmpty(managerFolderPath))
                    filteredFolderPath = Path.Combine(managerFolderPath, "Filtered");

                if (!string.IsNullOrEmpty(managerFolderPath) && !Directory.Exists(managerFolderPath))
                    Directory.CreateDirectory(managerFolderPath);

                if (!string.IsNullOrEmpty(filteredFolderPath) && !Directory.Exists(filteredFolderPath))
                    Directory.CreateDirectory(filteredFolderPath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"filtered 폴더 생성 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 기존 버전에서 사용하던 {images}/filtered 폴더가 있으면
        /// 새 위치({images}/SimpleDonkeyManager/Filtered)로 파일을 이전합니다.
        /// </summary>
        private void MigrateLegacyFilteredFolder()
        {
            try
            {
                if (string.IsNullOrEmpty(imagesFolderPath))
                    return;

                string legacyPath = Path.Combine(imagesFolderPath, "filtered");
                if (!Directory.Exists(legacyPath))
                    return;

                // 새 위치가 기존 위치와 동일하면(이론상 없음) 건너뛴다.
                if (string.Equals(Path.GetFullPath(legacyPath), Path.GetFullPath(filteredFolderPath ?? ""), StringComparison.OrdinalIgnoreCase))
                    return;

                EnsureFilteredFolder();

                foreach (string filePath in Directory.GetFiles(legacyPath))
                {
                    try
                    {
                        string destPath = Path.Combine(filteredFolderPath, Path.GetFileName(filePath));
                        if (File.Exists(destPath))
                            File.Delete(destPath);
                        File.Move(filePath, destPath);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"filtered 마이그레이션 파일 이동 오류: {filePath}, {ex.Message}");
                    }
                }

                // 비워진 기존 폴더 삭제 시도
                try
                {
                    if (Directory.GetFiles(legacyPath).Length == 0 && Directory.GetDirectories(legacyPath).Length == 0)
                        Directory.Delete(legacyPath);
                }
                catch { }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"filtered 폴더 마이그레이션 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 파일을 filtered 폴더로 이동합니다. 동일 이름이 있으면 덮어씁니다.
        /// </summary>
        private void MoveToFiltered(string sourcePath)
        {
            try
            {
                EnsureFilteredFolder();

                string fileName = Path.GetFileName(sourcePath);
                string destPath = Path.Combine(filteredFolderPath, fileName);

                if (File.Exists(destPath))
                    File.Delete(destPath);

                File.Move(sourcePath, destPath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"filtered 이동 오류: {sourcePath}, {ex.Message}");
            }
        }

        /// <summary>
        /// manifest.json의 catalog metadata(5번째 줄)에서 deleted_indexes 집합을 읽어옵니다.
        /// </summary>
        private HashSet<long> ReadDeletedIndexes()
        {
            var result = new HashSet<long>();
            try
            {
                if (!HasManifest)
                    return result;

                string[] lines = File.ReadAllLines(manifestPath);
                if (lines.Length < 5)
                    return result;

                using (JsonDocument doc = JsonDocument.Parse(lines[4]))
                {
                    if (doc.RootElement.ValueKind == JsonValueKind.Object &&
                        doc.RootElement.TryGetProperty("deleted_indexes", out var delEl) &&
                        delEl.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var item in delEl.EnumerateArray())
                        {
                            if (item.ValueKind == JsonValueKind.Number && item.TryGetInt64(out long idx))
                                result.Add(idx);
                            else if (long.TryParse(item.GetRawText().Trim('"'), out long parsed))
                                result.Add(parsed);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"deleted_indexes 읽기 오류: {ex.Message}");
            }
            return result;
        }

        /// <summary>
        /// manifest.json의 catalog metadata(5번째 줄) deleted_indexes를 주어진 집합으로 교체합니다.
        /// 다른 메타데이터 줄(inputs/types/metadata/manifest_metadata)은 그대로 보존합니다.
        /// </summary>
        private bool WriteDeletedIndexes(IEnumerable<long> deletedIndexes)
        {
            try
            {
                if (!HasManifest)
                    return false;

                string[] lines = File.ReadAllLines(manifestPath);
                if (lines.Length < 5)
                    return false;

                // 5번째 줄(catalog metadata) JSON 파싱 후 deleted_indexes만 교체
                using (JsonDocument doc = JsonDocument.Parse(lines[4]))
                {
                    if (doc.RootElement.ValueKind != JsonValueKind.Object)
                        return false;

                    var sorted = deletedIndexes.Distinct().OrderBy(x => x).ToList();

                    // 원본 속성 순서를 유지하면서 deleted_indexes만 치환
                    var sb = new System.Text.StringBuilder();
                    sb.Append('{');
                    bool first = true;
                    foreach (var prop in doc.RootElement.EnumerateObject())
                    {
                        if (!first) sb.Append(',');
                        first = false;
                        sb.Append(JsonSerializer.Serialize(prop.Name));
                        sb.Append(':');
                        if (prop.Name.Equals("deleted_indexes", StringComparison.OrdinalIgnoreCase))
                        {
                            sb.Append('[');
                            sb.Append(string.Join(",", sorted));
                            sb.Append(']');
                        }
                        else
                        {
                            sb.Append(prop.Value.GetRawText());
                        }
                    }
                    sb.Append('}');
                    lines[4] = sb.ToString();
                }

                File.WriteAllLines(manifestPath, lines);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"deleted_indexes 쓰기 오류: {ex.Message}");
                return false;
            }
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
