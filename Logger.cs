using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDonkeyManager
{
    internal class Logger
    {
        private List<string> logs;

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
