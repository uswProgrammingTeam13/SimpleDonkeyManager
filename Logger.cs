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

    internal class Logger
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
        /// 로그 메시지를 추가합니다.
        public void AppendLog(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"[{timestamp}] {message}";
            logs.Add(logEntry);

            // LogAdded 이벤트 발생
            OnLogAdded(new LogAddedEventArgs(logEntry, DateTime.Now));
        }

        /// <summary>
        /// LogAdded 이벤트 발생 메서드
        /// </summary>
        protected virtual void OnLogAdded(LogAddedEventArgs e)
        {
            LogAdded?.Invoke(this, e);
        }

        /// 모든 로그를 List 형식으로 반환합니다.
        public List<string> GetLogs()
        {
            return new List<string>(logs);
        }

        /// 특정 인덱스의 로그를 반환합니다.
        public string GetLog(int index)
        {
            if (index >= 0 && index < logs.Count)
                return logs[index];
            return null;
        }
        public int GetLogCount()
        {
            return logs.Count;
        }
        /// 모든 로그를 지웁니다.
        public void ClearLogs()
        {
            logs.Clear();
        }
    }
}
