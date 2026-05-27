using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleDonkeyManager
{
    /// <summary>
    /// 도움말 텍스트 파일을 로드하고 관리하는 클래스
    /// </summary>
    public class HelpManager
    {
        private Dictionary<int, string> helpTexts = new Dictionary<int, string>();
        private string helpFolderPath;

        // 탭 인덱스 상수
        public const int HELP_INITIAL = 0;
        public const int HELP_DATA_LOAD = 1;
        public const int HELP_DATA_FILTER = 2;
        public const int HELP_TRAINING = 3;
        public const int HELP_RESULT = 4;

        private static readonly string[] HelpFileNames = new string[]
        {
            "InitialHelp.txt",
            "DataLoadHelp.txt",
            "DataFilterHelp.txt",
            "TrainingHelp.txt",
            "ResultHelp.txt"
        };

        public HelpManager(string helpFolderPath = null)
        {
            // 도움말 폴더 경로 설정
            if (string.IsNullOrEmpty(helpFolderPath))
            {
                // 기본값: 현재 실행 파일이 있는 디렉토리의 helptexts 폴더
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                this.helpFolderPath = Path.Combine(baseDirectory, "helptexts");
            }
            else
            {
                this.helpFolderPath = helpFolderPath;
            }

            LoadAllHelpFiles();
        }

        /// <summary>
        /// 모든 도움말 파일을 로드합니다.
        /// </summary>
        private void LoadAllHelpFiles()
        {
            helpTexts.Clear();

            for (int i = 0; i < HelpFileNames.Length; i++)
            {
                string filePath = Path.Combine(helpFolderPath, HelpFileNames[i]);
                LoadHelpFile(i, filePath);
            }
        }

        /// <summary>
        /// 특정 도움말 파일을 로드합니다.
        /// </summary>
        private void LoadHelpFile(int index, string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string content = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
                    helpTexts[index] = content;
                }
                else
                {
                    // 파일을 찾을 수 없는 경우 상세한 오류 메시지 표시
                    helpTexts[index] = $"⚠️ 도움말을 찾을 수 없습니다.\n\n" +
                        $"파일: {HelpFileNames[index]}\n" +
                        $"경로: {filePath}\n\n" +
                        $"해결 방법:\n" +
                        $"1. 프로젝트의 helptexts 폴더가 있는지 확인하세요.\n" +
                        $"2. Visual Studio에서 프로젝트를 다시 빌드하세요 (Ctrl+Shift+B).\n" +
                        $"3. 실행 파일 폴더(bin\\Debug\\net10.0-windows)에 helptexts 폴더가 있는지 확인하세요.";
                }
            }
            catch (Exception ex)
            {
                helpTexts[index] = $"❌ 도움말 로드 중 오류가 발생했습니다.\n\n" +
                    $"파일: {HelpFileNames[index]}\n" +
                    $"경로: {filePath}\n\n" +
                    $"오류: {ex.Message}\n\n" +
                    $"자세한 정보: {ex.StackTrace}";
            }
        }

        /// <summary>
        /// 지정된 탭의 도움말 텍스트를 반환합니다.
        /// </summary>
        public string GetHelpText(int tabIndex)
        {
            if (helpTexts.ContainsKey(tabIndex))
            {
                return helpTexts[tabIndex];
            }
            return "도움말이 없습니다.";
        }

        /// <summary>
        /// 초기 화면 도움말을 반환합니다.
        /// </summary>
        public string GetInitialHelp()
        {
            return GetHelpText(HELP_INITIAL);
        }

        /// <summary>
        /// 데이터 불러오기 도움말을 반환합니다.
        /// </summary>
        public string GetDataLoadHelp()
        {
            return GetHelpText(HELP_DATA_LOAD);
        }

        /// <summary>
        /// 데이터 필터링 도움말을 반환합니다.
        /// </summary>
        public string GetDataFilterHelp()
        {
            return GetHelpText(HELP_DATA_FILTER);
        }

        /// <summary>
        /// 학습 실행 도움말을 반환합니다.
        /// </summary>
        public string GetTrainingHelp()
        {
            return GetHelpText(HELP_TRAINING);
        }

        /// <summary>
        /// 학습 결과 확인 도움말을 반환합니다.
        /// </summary>
        public string GetResultHelp()
        {
            return GetHelpText(HELP_RESULT);
        }

        /// <summary>
        /// 도움말 폴더 경로를 반환합니다.
        /// </summary>
        public string GetHelpFolderPath()
        {
            return helpFolderPath;
        }

        /// <summary>
        /// 도움말이 올바르게 로드되었는지 확인합니다.
        /// </summary>
        public bool IsHelpLoaded(int tabIndex)
        {
            return helpTexts.ContainsKey(tabIndex) && 
                   !helpTexts[tabIndex].Contains("도움말을 찾을 수 없습니다") &&
                   !helpTexts[tabIndex].Contains("도움말 로드 중 오류");
        }

        /// <summary>
        /// 모든 도움말이 정상적으로 로드되었는지 확인합니다.
        /// </summary>
        public bool AllHelpLoaded()
        {
            for (int i = 0; i < HelpFileNames.Length; i++)
            {
                if (!IsHelpLoaded(i))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
