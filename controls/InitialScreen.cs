using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

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

        private void BtnAutoSetupEnvironment_Click(object? sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "관리자 권한 PowerShell로 DonkeyCar 학습 실행 환경을 자동 구성합니다.\n\n진행하시겠습니까?",
                    "자동 환경 설정 확인",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;

                string? scriptPath = FindSetupScript();

                if (string.IsNullOrEmpty(scriptPath) || !File.Exists(scriptPath))
                {
                    MessageBox.Show(
                        "setup-environment.ps1 파일을 찾을 수 없습니다.\n\nresources 폴더 또는 프로젝트 루트에 setup-environment.ps1 파일이 있는지 확인하세요.",
                        "파일 없음",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                RunPowerShellScriptAsAdmin(scriptPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"자동 환경 설정 실행 중 오류가 발생했습니다:\n{ex.Message}",
                    "오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private string? FindSetupScript()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string scriptInBinResources = Path.Combine(baseDirectory, "resources", "setup-environment.ps1");
            if (File.Exists(scriptInBinResources))
                return scriptInBinResources;

            string scriptInBin = Path.Combine(baseDirectory, "setup-environment.ps1");
            if (File.Exists(scriptInBin))
                return scriptInBin;

            DirectoryInfo currentDir = new DirectoryInfo(baseDirectory);
            for (int i = 0; i < 8 && currentDir != null; i++)
            {
                string scriptInProjectResources = Path.Combine(currentDir.FullName, "resources", "setup-environment.ps1");
                if (File.Exists(scriptInProjectResources))
                    return scriptInProjectResources;

                string scriptInProjectRoot = Path.Combine(currentDir.FullName, "setup-environment.ps1");
                if (File.Exists(scriptInProjectRoot))
                    return scriptInProjectRoot;

                currentDir = currentDir.Parent;
            }

            return null;
        }

        private void RunPowerShellScriptAsAdmin(string scriptPath)
        {
            try
            {
                string command = $"-NoExit -ExecutionPolicy Bypass -File \"{scriptPath}\"";

                ProcessStartInfo psi = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = command,
                    UseShellExecute = true,
                    Verb = "runas",
                    WorkingDirectory = Path.GetDirectoryName(scriptPath),
                    CreateNoWindow = false
                };

                using (Process? process = Process.Start(psi))
                {
                }

                MessageBox.Show(
                    "자동 환경 설정 스크립트를 실행했습니다.\n\nPowerShell 창에서 설치 진행 상황을 확인하세요.",
                    "자동 환경 설정 시작",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show(
                    "자동 환경 설정에는 관리자 권한이 필요합니다.\n\nPowerShell을 '관리자 권한으로 실행'하거나 다시 시도해 주세요.",
                    "관리자 권한 필요",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"PowerShell 스크립트 실행 중 오류가 발생했습니다:\n{ex.Message}",
                    "오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
