using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SimpleDonkeyManager
{
    /// <summary>
    /// 데이터 필터 스냅샷 목록을 snapshots.json 파일로 영속화하는 저장소입니다.
    /// 파일은 데이터 폴더의 SimpleDonkeyManager 폴더 하위에 저장됩니다.
    /// </summary>
    public class SnapshotStore
    {
        private const string FileName = "snapshots.json";

        private readonly string storeFolderPath;
        private List<FilterSnapshot> snapshots = new List<FilterSnapshot>();

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        /// <summary>
        /// 지정한 관리 폴더(SimpleDonkeyManager) 경로를 기준으로 저장소를 생성하고 즉시 로드합니다.
        /// </summary>
        public SnapshotStore(string managerFolderPath)
        {
            storeFolderPath = managerFolderPath;
            Load();
        }

        /// <summary>
        /// snapshots.json 파일의 전체 경로.
        /// </summary>
        public string FilePath
        {
            get
            {
                return string.IsNullOrEmpty(storeFolderPath)
                    ? null
                    : Path.Combine(storeFolderPath, FileName);
            }
        }

        /// <summary>
        /// 저장된 스냅샷 목록 (시간순, 오래된 것부터).
        /// </summary>
        public IReadOnlyList<FilterSnapshot> Snapshots
        {
            get { return snapshots; }
        }

        /// <summary>
        /// 가장 최근 스냅샷. 없으면 null.
        /// </summary>
        public FilterSnapshot Latest
        {
            get { return snapshots.Count > 0 ? snapshots[snapshots.Count - 1] : null; }
        }

        /// <summary>
        /// 가장 최근 스냅샷의 작성자 ID. 없으면 빈 문자열.
        /// </summary>
        public string LastAuthorId
        {
            get { return Latest?.AuthorId ?? string.Empty; }
        }

        /// <summary>
        /// snapshots.json 파일을 읽어 메모리 목록을 갱신합니다.
        /// </summary>
        public void Load()
        {
            try
            {
                snapshots = new List<FilterSnapshot>();

                string path = FilePath;
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                    return;

                string json = File.ReadAllText(path);
                if (string.IsNullOrWhiteSpace(json))
                    return;

                var loaded = JsonSerializer.Deserialize<List<FilterSnapshot>>(json, JsonOptions);
                if (loaded != null)
                {
                    snapshots = loaded
                        .Where(s => s != null)
                        .OrderBy(s => s.CreatedAt)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"스냅샷 로드 오류: {ex.Message}");
                snapshots = new List<FilterSnapshot>();
            }
        }

        /// <summary>
        /// 현재 메모리 목록을 snapshots.json 파일로 저장합니다.
        /// </summary>
        public bool Save()
        {
            try
            {
                string path = FilePath;
                if (string.IsNullOrEmpty(path))
                    return false;

                string folder = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string json = JsonSerializer.Serialize(snapshots, JsonOptions);
                File.WriteAllText(path, json);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"스냅샷 저장 오류: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 새 스냅샷을 추가하고 즉시 파일에 저장합니다.
        /// </summary>
        public FilterSnapshot Add(string authorId, string memo, IEnumerable<int> deletedFrameNumbers)
        {
            var snapshot = new FilterSnapshot(authorId, memo, deletedFrameNumbers);
            snapshots.Add(snapshot);
            Save();
            return snapshot;
        }

        /// <summary>
        /// 지정한 ID의 스냅샷을 이력에서 제거하고 즉시 파일에 저장합니다.
        /// </summary>
        public bool Remove(string snapshotId)
        {
            try
            {
                if (string.IsNullOrEmpty(snapshotId))
                    return false;

                int removed = snapshots.RemoveAll(s => s != null && s.Id == snapshotId);
                if (removed > 0)
                {
                    Save();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"스냅샷 삭제 오류: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 지정한 ID의 스냅샷을 반환합니다. 없으면 null.
        /// </summary>
        public FilterSnapshot GetById(string snapshotId)
        {
            if (string.IsNullOrEmpty(snapshotId))
                return null;
            return snapshots.FirstOrDefault(s => s != null && s.Id == snapshotId);
        }

        /// <summary>
        /// 각 스냅샷의 직전 스냅샷 대비 프레임 증감(삭제 수 변화)을 계산합니다.
        /// 첫 스냅샷은 원본 대비이므로 해당 스냅샷의 삭제 수 자체가 증감이 됩니다.
        /// 반환 값은 스냅샷 ID -> (삭제 수 증가량) 매핑이며, 양수는 추가 삭제, 음수는 복구를 의미합니다.
        /// </summary>
        public Dictionary<string, int> ComputeDeltas()
        {
            var deltas = new Dictionary<string, int>();
            int previousDeleted = 0;
            foreach (var snapshot in snapshots)
            {
                if (snapshot == null)
                    continue;

                int current = snapshot.DeletedCount;
                deltas[snapshot.Id] = current - previousDeleted;
                previousDeleted = current;
            }
            return deltas;
        }
    }
}
