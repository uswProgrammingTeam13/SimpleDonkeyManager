using System;
using System.Collections.Generic;

namespace SimpleDonkeyManager
{
    /// <summary>
    /// 데이터 필터링의 한 시점(스냅샷)을 표현하는 모델입니다.
    /// Git의 커밋과 유사하게, 특정 시점에 "삭제(제외)된 프레임 번호 집합"과
    /// 작성자/메모/시각 정보를 함께 보관합니다.
    /// </summary>
    public class FilterSnapshot
    {
        /// <summary>
        /// 스냅샷 고유 식별자 (생성 시 자동 부여).
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 이 스냅샷 시점에 삭제(제외) 상태인 프레임 번호 집합.
        /// </summary>
        public List<int> DeletedFrameNumbers { get; set; } = new List<int>();

        /// <summary>
        /// 스냅샷을 저장한 사용자 ID.
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        /// 변경 내역 메모 (저장 시 입력, 불러오기에는 사용하지 않음).
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 저장 시각.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        public FilterSnapshot()
        {
        }

        public FilterSnapshot(string authorId, string memo, IEnumerable<int> deletedFrameNumbers)
        {
            Id = Guid.NewGuid().ToString("N");
            AuthorId = authorId ?? string.Empty;
            Memo = memo ?? string.Empty;
            CreatedAt = DateTime.Now;
            DeletedFrameNumbers = deletedFrameNumbers != null
                ? new List<int>(deletedFrameNumbers)
                : new List<int>();
        }

        /// <summary>
        /// 이 스냅샷 시점에 삭제된 프레임 개수.
        /// </summary>
        public int DeletedCount
        {
            get { return DeletedFrameNumbers != null ? DeletedFrameNumbers.Count : 0; }
        }
    }
}
