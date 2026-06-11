#!/usr/bin/env powershell
<#
SimpleDonkeyManager - automatic training environment setup

Run from an elevated PowerShell:
  powershell.exe -ExecutionPolicy Bypass -File .\resources\setup-environment.ps1

The script is intentionally usable both from the source tree and from the
WinForms app's copied resources folder under bin\...\resources.
#>

param(
    [switch]$NoPause,
    [switch]$SkipPackageInstall,
    [switch]$CheckOnly
)

$ErrorActionPreference = "Continue"

function Write-Header {
    param([string]$Text)
    Write-Host ""
    Write-Host "==============================================================================" -ForegroundColor Cyan
    Write-Host " $Text" -ForegroundColor Cyan
    Write-Host "==============================================================================" -ForegroundColor Cyan
}

function Write-Success {
    param([string]$Text)
    Write-Host "OK      $Text" -ForegroundColor Green
}

function Write-Warning-Custom {
    param([string]$Text)
    Write-Host "WARNING $Text" -ForegroundColor Yellow
}

function Write-Error-Custom {
    param([string]$Text)
    Write-Host "ERROR   $Text" -ForegroundColor Red
}

function Write-Info {
    param([string]$Text)
    Write-Host "INFO    $Text" -ForegroundColor Cyan
}

function Pause-IfNeeded {
    if (-not $NoPause) {
        Read-Host "Press Enter to exit"
    }
}

function Stop-WithError {
    param(
        [string]$Message,
        [int]$Code = 1
    )
    Write-Error-Custom $Message
    Pause-IfNeeded
    exit $Code
}

function Test-Admin {
    $currentUser = [Security.Principal.WindowsIdentity]::GetCurrent()
    $principal = New-Object Security.Principal.WindowsPrincipal($currentUser)
    return $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
}

function Invoke-CommandCapture {
    param(
        [string]$FilePath,
        [string[]]$Arguments = @()
    )

    try {
        $output = & $FilePath @Arguments 2>&1
        return @{
            Success = ($LASTEXITCODE -eq 0)
            ExitCode = $LASTEXITCODE
            Output = ($output -join [Environment]::NewLine)
        }
    } catch {
        return @{
            Success = $false
            ExitCode = -1
            Output = $_.Exception.Message
        }
    }
}

function Find-ProjectRoot {
    param([string]$StartDirectory)

    $current = Get-Item -LiteralPath $StartDirectory
    while ($null -ne $current) {
        $csproj = Join-Path $current.FullName "SimpleDonkeyManager.csproj"
        $trainPy = Join-Path $current.FullName "python\train.py"

        if ((Test-Path -LiteralPath $csproj) -or (Test-Path -LiteralPath $trainPy)) {
            return $current.FullName
        }

        $current = $current.Parent
    }

    # Published builds may not include the project file. In that case, use the
    # app directory that owns the copied resources folder.
    if ($StartDirectory -match "\\resources$") {
        return (Split-Path -Parent $StartDirectory)
    }

    return $StartDirectory
}

function Resolve-Python {
    $candidates = @()

    $pythonCommand = Get-Command "python.exe" -ErrorAction SilentlyContinue
    if ($pythonCommand) {
        $candidates += @{
            File = $pythonCommand.Source
            Args = @()
            Label = "python.exe from PATH"
        }
    }

    $pyLauncher = Get-Command "py.exe" -ErrorAction SilentlyContinue
    if ($pyLauncher) {
        $candidates += @{
            File = $pyLauncher.Source
            Args = @("-3.11")
            Label = "Python Launcher py -3.11"
        }
        $candidates += @{
            File = $pyLauncher.Source
            Args = @("-3")
            Label = "Python Launcher py -3"
        }
    }

    $localAppData = [Environment]::GetFolderPath("LocalApplicationData")
    if ($localAppData) {
        $commonPython = Join-Path $localAppData "Programs\Python\Python311\python.exe"
        if (Test-Path -LiteralPath $commonPython) {
            $candidates += @{
                File = $commonPython
                Args = @()
                Label = "Python 3.11 default user install"
            }
        }
    }

    foreach ($candidate in $candidates) {
        $result = Invoke-CommandCapture -FilePath $candidate.File -Arguments ($candidate.Args + @("--version"))
        if (-not $result.Success) {
            continue
        }

        $versionText = $result.Output.Trim()
        if ($versionText -match "Python\s+(\d+)\.(\d+)\.(\d+)") {
            $major = [int]$Matches[1]
            $minor = [int]$Matches[2]
            if ($major -eq 3 -and $minor -ge 10) {
                return @{
                    File = $candidate.File
                    Args = $candidate.Args
                    Label = $candidate.Label
                    Version = $versionText
                }
            }
        }
    }

    return $null
}

function Invoke-Python {
    param(
        [hashtable]$Python,
        [string[]]$Arguments
    )

    return Invoke-CommandCapture -FilePath $Python.File -Arguments ($Python.Args + $Arguments)
}

function Test-VenvPython {
    param([string]$VenvPython)

    if (-not (Test-Path -LiteralPath $VenvPython)) {
        return $false
    }

    $result = Invoke-CommandCapture -FilePath $VenvPython -Arguments @("--version")
    return $result.Success
}

function Install-Package {
    param(
        [string]$VenvPython,
        [string[]]$PackageArgs,
        [string]$DisplayName
    )

    Write-Info "Installing $DisplayName..."
    $result = Invoke-CommandCapture -FilePath $VenvPython -Arguments (@("-m", "pip", "install") + $PackageArgs + @("--quiet"))
    if ($result.Success) {
        Write-Success "$DisplayName installed"
        return $true
    }

    Write-Warning-Custom "$DisplayName installation failed"
    if ($result.Output) {
        Write-Warning-Custom $result.Output
    }
    return $false
}

Write-Header "Admin Privilege Check"
if (-not (Test-Admin)) {
    Stop-WithError "This script must be run as administrator. Right-click PowerShell and choose 'Run as administrator'."
}
Write-Success "Running with administrator privileges"

Write-Header ".NET 10 Environment Check"
$dotnet = Get-Command "dotnet.exe" -ErrorAction SilentlyContinue
if (-not $dotnet) {
    Stop-WithError ".NET SDK/Runtime was not found. Install .NET 10 from https://dotnet.microsoft.com/download/dotnet/10.0"
}

$dotnetCheck = Invoke-CommandCapture -FilePath $dotnet.Source -Arguments @("--version")
if (-not $dotnetCheck.Success) {
    Stop-WithError "dotnet --version failed: $($dotnetCheck.Output)"
}

$dotnetVersion = $dotnetCheck.Output.Trim()
Write-Success ".NET found: $dotnetVersion"
if ($dotnetVersion -notlike "10.*") {
    Write-Warning-Custom "This project targets .NET 10. Current dotnet version is $dotnetVersion."
}

Write-Header "Python Environment Check"
$python = Resolve-Python
if (-not $python) {
    Stop-WithError "Python 3.10+ was not found. Install Python 3.11 and enable either PATH or the Python Launcher (py.exe)."
}
Write-Success "$($python.Label): $($python.Version)"

Write-Header "Project Root Detection"
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$projectRoot = Find-ProjectRoot -StartDirectory $scriptDir
$pythonDir = Join-Path $projectRoot "python"
$trainPyPath = Join-Path $pythonDir "train.py"
$prepareScript = Join-Path $pythonDir "prepare_tub.py"

Write-Info "Script directory: $scriptDir"
Write-Success "Project root: $projectRoot"

if (-not (Test-Path -LiteralPath $trainPyPath)) {
    Stop-WithError "python\train.py was not found under project root: $trainPyPath"
}

$venvPath = Join-Path $projectRoot "donkey_env"
$venvPython = Join-Path $venvPath "Scripts\python.exe"

Write-Header "Virtual Environment Setup"
if (-not (Test-Path -LiteralPath $venvPath)) {
    Write-Info "Creating virtual environment: $venvPath"
    $createResult = Invoke-Python -Python $python -Arguments @("-m", "venv", $venvPath)
    if (-not $createResult.Success) {
        Stop-WithError "Failed to create virtual environment: $($createResult.Output)"
    }
    Write-Success "Virtual environment created"
} elseif (-not (Test-VenvPython -VenvPython $venvPython)) {
    Write-Warning-Custom "Existing virtual environment is not runnable. Attempting venv repair with --upgrade."
    $repairResult = Invoke-Python -Python $python -Arguments @("-m", "venv", "--upgrade", $venvPath)
    if (-not $repairResult.Success -or -not (Test-VenvPython -VenvPython $venvPython)) {
        Stop-WithError "Failed to repair donkey_env. Rename or remove '$venvPath' and run this script again. Details: $($repairResult.Output)"
    }
    Write-Success "Virtual environment repaired"
} else {
    Write-Success "Virtual environment is ready: $venvPath"
}

$venvVersion = Invoke-CommandCapture -FilePath $venvPython -Arguments @("--version")
if ($venvVersion.Success) {
    Write-Success "venv Python: $($venvVersion.Output.Trim())"
}

if ($CheckOnly) {
    Write-Header "Check Complete"
    Write-Success "Required tools and project paths are valid."
    Pause-IfNeeded
    exit 0
}

Write-Header "Package Installation"
if ($SkipPackageInstall) {
    Write-Warning-Custom "Skipping package installation because -SkipPackageInstall was supplied."
} else {
    Write-Info "Upgrading pip..."
    $pipResult = Invoke-CommandCapture -FilePath $venvPython -Arguments @("-m", "pip", "install", "--upgrade", "pip", "--quiet")
    if ($pipResult.Success) {
        Write-Success "pip upgraded"
    } else {
        Write-Warning-Custom "pip upgrade failed: $($pipResult.Output)"
    }

    Write-Info "Installing pinned packages for donkeycar 5.3.0 / TensorFlow 2.15 / NumPy 1.x"
    $failedPackages = @()

    $packages = @(
        @{ Args = @("donkeycar==5.3.0"); DisplayName = "DonkeyCar 5.3.0" },
        @{ Args = @("numpy==1.26.4"); DisplayName = "NumPy 1.26.4" },
        @{ Args = @("tensorflow==2.15.1"); DisplayName = "TensorFlow 2.15.1" },
        @{ Args = @("albumentations==1.4.18"); DisplayName = "Albumentations 1.4.18" },
        @{ Args = @("opencv-python-headless==4.9.0.80"); DisplayName = "OpenCV headless 4.9.0.80" },
        @{ Args = @("Pillow"); DisplayName = "Pillow" },
        @{ Args = @("docopt"); DisplayName = "docopt" },
        @{ Args = @("h5py"); DisplayName = "h5py" },
        @{ Args = @("pyyaml"); DisplayName = "PyYAML" }
    )

    foreach ($pkg in $packages) {
        if (-not (Install-Package -VenvPython $venvPython -PackageArgs $pkg.Args -DisplayName $pkg.DisplayName)) {
            $failedPackages += $pkg.DisplayName
        }
    }

    if (-not (Install-Package -VenvPython $venvPython -PackageArgs @("numpy==1.26.4") -DisplayName "NumPy final pin 1.26.4")) {
        $failedPackages += "NumPy final pin"
    }

    if ($failedPackages.Count -gt 0) {
        Write-Warning-Custom "Some packages failed: $($failedPackages -join ', ')"
        Write-Warning-Custom "Check network access and rerun this script."
    }
}

Write-Header "Installation Verification"
$env:NO_ALBUMENTATIONS_UPDATE = "1"
$verifyCommands = @(
    @{ Name = "tensorflow"; Code = "import tensorflow as tf; print(tf.__version__)" },
    @{ Name = "keras"; Code = "import keras; print(keras.__version__)" },
    @{ Name = "donkeycar"; Code = "import donkeycar; print(donkeycar.__version__)" },
    @{ Name = "numpy"; Code = "import numpy; print(numpy.__version__)" },
    @{ Name = "albumentations"; Code = "import albumentations; print(albumentations.__version__)" },
    @{ Name = "Pillow"; Code = "from PIL import Image; print('Pillow OK')" },
    @{ Name = "docopt"; Code = "import docopt; print('docopt OK')" },
    @{ Name = "training pipeline"; Code = "from donkeycar.pipeline.training import train; print('donkeycar training pipeline OK')" }
)

$installedPackages = @()
$missingPackages = @()
foreach ($verify in $verifyCommands) {
    $result = Invoke-CommandCapture -FilePath $venvPython -Arguments @("-c", $verify.Code)
    if ($result.Success) {
        $lastLine = (($result.Output -split "(`r`n|`n|`r)") | Where-Object { $_.Trim() -ne "" } | Select-Object -Last 1)
        Write-Success "$($verify.Name): $lastLine"
        $installedPackages += $verify.Name
    } else {
        Write-Warning-Custom "$($verify.Name): missing or failed"
        if ($result.Output) {
            Write-Warning-Custom $result.Output
        }
        $missingPackages += $verify.Name
    }
}

Write-Header "Training Script Verification"
$compileResult = Invoke-CommandCapture -FilePath $venvPython -Arguments @("-m", "py_compile", $trainPyPath)
if ($compileResult.Success) {
    Write-Success "train.py syntax is OK"
} else {
    Write-Warning-Custom "train.py syntax check failed: $($compileResult.Output)"
}

Write-Header "Configuration File Setup"
$configPath = Join-Path $pythonDir "config.py"
if (Test-Path -LiteralPath $configPath) {
    Write-Success "Found config.py: $configPath"
} elseif (Test-Path -LiteralPath $prepareScript) {
    Write-Info "config.py not found. Generating it from donkeycar's cfg_complete.py template."
    $configCode = @"
import os, shutil, donkeycar
src = os.path.join(os.path.dirname(donkeycar.__file__), 'templates', 'cfg_complete.py')
dst = r'$configPath'
shutil.copyfile(src, dst)
with open(dst, 'a', encoding='utf-8') as f:
    f.write('\n# ----- setup-environment.ps1 auto settings -----\n')
    f.write('SHOW_PLOT = False\n')
    f.write('PRINT_MODEL_SUMMARY = True\n')
    f.write('CREATE_TF_LITE = False\n')
    f.write('CREATE_TENSOR_RT = False\n')
print('config.py created')
"@
    $configResult = Invoke-CommandCapture -FilePath $venvPython -Arguments @("-c", $configCode)
    if ($configResult.Success -and (Test-Path -LiteralPath $configPath)) {
        Write-Success "config.py generated: $configPath"
    } else {
        Write-Warning-Custom "config.py generation failed: $($configResult.Output)"
        Write-Warning-Custom "prepare_tub.py will try to generate config.py before training."
    }
} else {
    Write-Warning-Custom "prepare_tub.py not found. config.py cannot be auto-generated now."
}

Write-Header "Environment Test"
$helpResult = Invoke-CommandCapture -FilePath $venvPython -Arguments @($trainPyPath, "--help")
if ($helpResult.Success) {
    Write-Success "train.py --help runs successfully"
} else {
    Write-Warning-Custom "train.py --help failed: $($helpResult.Output)"
}

Write-Header "Setup Complete"
Write-Info "Project root: $projectRoot"
Write-Info "Virtual env:  $venvPath"
Write-Info ".NET:         $dotnetVersion"
Write-Info "Python:       $($python.Version)"
Write-Info "Verified:     $($installedPackages.Count) package checks passed"

if ($missingPackages.Count -gt 0) {
    Write-Warning-Custom "Missing checks: $($missingPackages -join ', ')"
}

Write-Info ""
Write-Info "Next steps:"
Write-Host "  1. Build/run the WinForms app." -ForegroundColor Green
Write-Host "  2. Load DonkeyCar tub data." -ForegroundColor Green
Write-Host "  3. Start training from the Training screen." -ForegroundColor Green
Write-Info ""
Write-Info "Manual test command:"
Write-Host "  .\donkey_env\Scripts\python.exe python\train.py --help" -ForegroundColor White

Pause-IfNeeded
