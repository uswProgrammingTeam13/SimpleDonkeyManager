using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System;

namespace SimpleDonkeyManager.controls
{
    public partial class InitialScreen : UserControl
    {
        public InitialScreen()
        {
            InitializeComponent();
            InitializeEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            btnAutoSetupEnvironment.Click += BtnAutoSetupEnvironment_Click;
        }

        private void BtnAutoSetupEnvironment_Click(object sender, EventArgs e)
        {
            try
            {
                // 확인 메시지 표시
                DialogResult result = MessageBox.Show(
                    "관리자 권한으로 Data Manager 실행에 필요한 파일을 자동으로 설치합니다.\n\n진행하시겠습니까?",
                    "자동 환경 설정 확인",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;

                string scriptPath = FindSetupScript();

                if (string.IsNullOrEmpty(scriptPath) || !File.Exists(scriptPath))
                {
                    MessageBox.Show(
                        "setup-environment.ps1 파일을 찾을 수 없습니다.\n\n" +
                        "resources 폴더에 setup-environment.ps1 파일이 있는지 확인하세요.",
                        "파일 없음",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // 관리자 권한으로 PowerShell 스크립트 실행
                RunPowerShellScriptAsAdmin(scriptPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"자동 환경 설정 실행 중 오류 발생:\n{ex.Message}",
                    "오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private string FindSetupScript()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // 1️⃣ bin\Debug\net10\resources\ 또는 bin\Release\net10\resources\ 에서 찾기
            //    (Release 빌드 배포 시 여기서 발견 - 권장)
            string scriptInBinResources = Path.Combine(baseDirectory, "resources", "setup-environment.ps1");
            if (File.Exists(scriptInBinResources))
                return scriptInBinResources;

            // 2️⃣ bin\Debug\net10\ 에서 직접 찾기 (호환성)
            string scriptInBin = Path.Combine(baseDirectory, "setup-environment.ps1");
            if (File.Exists(scriptInBin))
                return scriptInBin;

            // 3️⃣ 프로젝트 루트\resources에서 찾기 (개발 환경)
            string projectRoot = Directory.GetParent(Directory.GetParent(baseDirectory).FullName).FullName;
            string scriptInProjectResources = Path.Combine(projectRoot, "resources", "setup-environment.ps1");
            if (File.Exists(scriptInProjectResources))
                return scriptInProjectResources;

            // 4️⃣ 프로젝트 루트에서 찾기 (개발 환경 - 호환성)
            string scriptInProjectRoot = Path.Combine(projectRoot, "setup-environment.ps1");
            if (File.Exists(scriptInProjectRoot))
                return scriptInProjectRoot;

            return null;
        }

        private void RunPowerShellScriptAsAdmin(string scriptPath)
        {
            try
            {
                // PowerShell 스크립트 실행 명령어
                string command = $"-NoExit -ExecutionPolicy Bypass -File \"{scriptPath}\"";

                // ProcessStartInfo 설정
                ProcessStartInfo psi = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = command,
                    UseShellExecute = true,
                    Verb = "runas",  // 관리자 권한으로 실행
                    WorkingDirectory = Path.GetDirectoryName(scriptPath),
                    CreateNoWindow = false
                };

                // 프로세스 시작
                using (Process process = Process.Start(psi))
                {
                    // 필요시 프로세스 완료 대기 (선택 사항)
                    // process.WaitForExit();
                }

                MessageBox.Show(
                    "자동 환경 설정 스크립트가 실행되었습니다.\n\n" +
                    "PowerShell 창에서 설치 진행 상황을 확인하세요.",
                    "자동 환경 설정 시작",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (System.ComponentModel.Win32Exception)
            {
                // 사용자가 관리자 권한 승인을 거부한 경우
                MessageBox.Show(
                    "자동 환경 설정을 위해서는 관리자 권한이 필요합니다.\n\n" +
                    "PowerShell을 '관리자로 실행'한 후 다시 시도해주세요.",
                    "관리자 권한 필요",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"PowerShell 스크립트 실행 중 오류 발생:\n{ex.Message}",
                    "오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
