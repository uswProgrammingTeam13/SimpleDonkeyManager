using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDonkeyManager
{
    /// <summary>
    /// 로그가 추가되었을 때 발생하는 이벤트 인자
    /// </summary>
    public class LogAddedEventArgs : EventArgs
    {
        public string LogMessage { get; set; }
        public DateTime Timestamp { get; set; }

        public LogAddedEventArgs(string logMessage, DateTime timestamp)
        {
            LogMessage = logMessage;
            Timestamp = timestamp;
        }
    }

    public class Logger
    {
        private List<string> logs;

        /// <summary>
        /// 로그가 추가되었을 때 발생하는 이벤트
        /// </summary>
        public event EventHandler<LogAddedEventArgs> LogAdded;

        public Logger()
        {
            logs = new List<string>();
        }
        /// <summary>
        /// 로그 메시지를 추가합니다.
        /// </summary>
        public void AppendLog(string message)
        {
            try
            {
                if (logs == null)
                {
                    logs = new List<string>();
                }

                if (string.IsNullOrEmpty(message))
                    return;

                try
                {
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string logEntry = $"[{timestamp}] {message}";
                    logs.Add(logEntry);
                }
                catch (ArgumentException)
                {
                    // 로그 추가 실패 시 기본 메시지 추가
                    logs.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [로그 추가 오류]");
                }

                // LogAdded 이벤트 발생 (이벤트 호출 실패는 무시)
                try
                {
                    OnLogAdded(new LogAddedEventArgs(logs[logs.Count - 1], DateTime.Now));
                }
                catch
                {
                    // 이벤트 호출 실패는 무시
                }
            }
            catch (Exception ex)
            {
                // 최상위 예외도 무시 (로깅 자체가 실패하면 할 수 있는 게 없음)
                System.Diagnostics.Debug.WriteLine($"Logger 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// LogAdded 이벤트 발생 메서드
        /// </summary>
        protected virtual void OnLogAdded(LogAddedEventArgs e)
        {
            try
            {
                if (e == null)
                    return;

                LogAdded?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                // 이벤트 핸들러 예외는 무시 (로깅 시스템이 중단되면 안 됨)
                System.Diagnostics.Debug.WriteLine($"LogAdded 이벤트 처리 오류: {ex.Message}");
            }
        }

        /// <summary>
        /// 모든 로그를 List 형식으로 반환합니다.
        /// </summary>
        public List<string> GetLogs()
        {
            try
            {
                if (logs == null)
                    return new List<string>();

                return new List<string>(logs);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"로그 조회 오류: {ex.Message}");
                return new List<string>();
            }
        }

        /// <summary>
        /// 특정 인덱스의 로그를 반환합니다.
        /// </summary>
        public string GetLog(int index)
        {
            try
            {
                if (logs == null || logs.Count == 0)
                    return null;

                if (index >= 0 && index < logs.Count)
                    return logs[index];

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"로그 항목 조회 오류: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 로그 개수를 반환합니다.
        /// </summary>
        public int GetLogCount()
        {
            try
            {
                return logs?.Count ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 모든 로그를 지웁니다.
        /// </summary>
        public void ClearLogs()
        {
            try
            {
                if (logs != null)
                {
                    logs.Clear();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"로그 삭제 오류: {ex.Message}");
            }
        }
    }
}
