#!/usr/bin/env powershell
# ============================================================================
# SimpleDonkeyManager - Auto Environment Setup Script
# ============================================================================
# Purpose: Automatically install and configure .NET 10, Python 3.11, Donkeycar
# Usage: powershell.exe -ExecutionPolicy Bypass -File setup-environment.ps1
# ============================================================================

$ErrorActionPreference = "Continue"

# ============================================================================
# 1. Utility Functions
# ============================================================================

function Write-Header {
	param([string]$Text)
	Write-Host ""
	Write-Host "╔════════════════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
	Write-Host "║ $($Text.PadRight(66)) ║" -ForegroundColor Cyan
	Write-Host "╚════════════════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
	Write-Host ""
}

function Write-Success {
	param([string]$Text)
	Write-Host "OK $Text" -ForegroundColor Green
}

function Write-Warning-Custom {
	param([string]$Text)
	Write-Host "WARNING $Text" -ForegroundColor Yellow
}

function Write-Error-Custom {
	param([string]$Text)
	Write-Host "ERROR $Text" -ForegroundColor Red
}

function Write-Info {
	param([string]$Text)
	Write-Host "INFO $Text" -ForegroundColor Cyan
}

function Check-Admin {
	$currentUser = [Security.Principal.WindowsIdentity]::GetCurrent()
	$principal = New-Object Security.Principal.WindowsPrincipal($currentUser)
	return $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
}

# ============================================================================
# 2. Admin Check
# ============================================================================

Write-Header "Admin Privilege Check"

if (-not (Check-Admin)) {
	Write-Error-Custom "This script requires administrator privileges!"
	Write-Info "Please run PowerShell as Administrator and try again."
	Read-Host "Press Enter to exit"
	exit 1
}

Write-Success "Running with administrator privileges"

# ============================================================================
# 3. .NET Version Check
# ============================================================================

Write-Header ".NET 10 Environment Check"

Write-Info "Checking .NET installation..."
$dotnetVersion = & dotnet --version 2>&1

if ($LASTEXITCODE -eq 0) {
	Write-Success ".NET is installed: $dotnetVersion"

	if ($dotnetVersion -like "10.*") {
		Write-Success ".NET 10 is installed - OK"
	} else {
		Write-Warning-Custom ".NET version is not 10 (current: $dotnetVersion)"
		Write-Warning-Custom "Please install .NET 10: https://dotnet.microsoft.com/download/dotnet/10.0"
	}
} else {
	Write-Error-Custom ".NET is not installed!"
	Write-Info "Installation link: https://dotnet.microsoft.com/download/dotnet/10.0"
	Write-Info ""
	Write-Warning-Custom "Please install .NET 10 before continuing."
	Write-Info ""
	Write-Info "Run this script again after installation."
	Read-Host "Press Enter to exit"
	exit 1
}

# ============================================================================
# 4. Python 3.11 Check
# ============================================================================

Write-Header "Python 3.11 Environment Check"

Write-Info "Checking Python installation..."
$pythonVersion = & python --version 2>&1

if ($LASTEXITCODE -eq 0) {
	Write-Success "Python is installed: $pythonVersion"

	if ($pythonVersion -like "*3.11*") {
		Write-Success "Python 3.11 is installed - OK"
	} else {
		Write-Warning-Custom "Python version is not 3.11 (current: $pythonVersion)"
		Write-Warning-Custom "Recommended: Python 3.11 from https://www.python.org/downloads/"
	}
} else {
	Write-Error-Custom "Python is not installed!"
	Write-Info "Please install Python 3.11 from: https://www.python.org/downloads/"
	Read-Host "Press Enter to exit"
	exit 1
}

# ============================================================================
# 5. Project Root Detection
# ============================================================================

Write-Header "Project Configuration"

# Get the directory where this script is located
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Write-Info "Script location: $scriptDir"

# Try to find the project root
# If script is in resources/, go up two levels to project root
# If script is in root/, stay at current location
if ($scriptDir -match "\\resources$") {
	$projectRoot = Split-Path -Parent $scriptDir
	Write-Info "Detected resources folder, moving up to: $projectRoot"
} else {
	$projectRoot = $scriptDir
	Write-Info "Using current directory as project root: $projectRoot"
}

Write-Success "Project root: $projectRoot"

# ============================================================================
# 6. Virtual Environment Setup
# ============================================================================

Write-Header "Virtual Environment Setup"

$venvPath = Join-Path $projectRoot "donkey_env"
$venvPython = Join-Path $venvPath "Scripts" "python.exe"

if (-not (Test-Path $venvPath)) {
	Write-Info "Creating virtual environment: $venvPath"
	& python -m venv $venvPath

	if ($LASTEXITCODE -eq 0) {
		Write-Success "Virtual environment created successfully"
	} else {
		Write-Error-Custom "Failed to create virtual environment"
		Read-Host "Press Enter to exit"
		exit 1
	}
} else {
	Write-Success "Virtual environment already exists: $venvPath"
}

# ============================================================================
# 7. Package Installation
# ============================================================================

Write-Header "Package Installation"

Write-Info "Upgrading pip..."
& $venvPython -m pip install --upgrade pip --quiet 2>&1 | Out-Null

if ($LASTEXITCODE -eq 0) {
	Write-Success "pip upgraded successfully"
} else {
	Write-Warning-Custom "pip upgrade encountered issues"
}

Write-Info ""
Write-Info "Installing required packages..."

$packages = @(
	@{ name = "tensorflow"; displayName = "TensorFlow" },
	@{ name = "donkeycar"; displayName = "DonkeyCar" },
	@{ name = "numpy"; displayName = "NumPy" },
	@{ name = "Pillow"; displayName = "Pillow" },
	@{ name = "docopt"; displayName = "docopt" },
	@{ name = "h5py"; displayName = "h5py" },
	@{ name = "pyyaml"; displayName = "PyYAML" }
)

$failedPackages = @()

foreach ($pkg in $packages) {
	Write-Info "Installing $($pkg.displayName)..."

	try {
		& $venvPython -m pip install $pkg.name --quiet 2>&1 | Out-Null

		if ($LASTEXITCODE -eq 0) {
			Write-Success "$($pkg.displayName) installed successfully"
		} else {
			Write-Warning-Custom "$($pkg.displayName) installation error"
			$failedPackages += $pkg.displayName
		}
	} catch {
		Write-Warning-Custom "$($pkg.displayName) failed: $_"
		$failedPackages += $pkg.displayName
	}
}

if ($failedPackages.Count -gt 0) {
	Write-Warning-Custom "Failed to install: $($failedPackages -join ', ')"
	Write-Info "Check network connection and run again."
}

# ============================================================================
# 8. Installation Verification
# ============================================================================

Write-Header "Installation Verification"

Write-Info "Verifying installed packages..."

$verifyCommands = @(
	@{ name = "tensorflow"; cmd = "import tensorflow as tf; print(tf.__version__)" },
	@{ name = "donkeycar"; cmd = "import donkeycar; print(donkeycar.__version__)" },
	@{ name = "numpy"; cmd = "import numpy; print(numpy.__version__)" },
	@{ name = "PIL"; cmd = "from PIL import Image; print('Pillow OK')" },
	@{ name = "docopt"; cmd = "import docopt; print('docopt OK')" }
)

$installedPackages = @()
$missingPackages = @()

foreach ($verify in $verifyCommands) {
	$result = & $venvPython -c $verify.cmd 2>&1

	if ($LASTEXITCODE -eq 0) {
		Write-Success "$($verify.name): $result"
		$installedPackages += $verify.name
	} else {
		Write-Warning-Custom "$($verify.name): Not installed or error"
		$missingPackages += $verify.name
	}
}

# ============================================================================
# 9. train.py Verification
# ============================================================================

Write-Header "Training Script Verification"

$trainPyPath = Join-Path $projectRoot "python\train.py"

if (Test-Path $trainPyPath) {
	Write-Success "Found train.py: $trainPyPath"

	Write-Info "Checking train.py syntax..."
	& $venvPython -m py_compile $trainPyPath 2>&1 | Out-Null

	if ($LASTEXITCODE -eq 0) {
		Write-Success "train.py syntax is OK"
	} else {
		Write-Warning-Custom "train.py has syntax errors"
	}
} else {
	Write-Error-Custom "train.py not found: $trainPyPath"
}

# ============================================================================
# 10. config.py Verification
# ============================================================================

Write-Header "Configuration File Verification"

$configPath = Join-Path $projectRoot "config.py"

if (Test-Path $configPath) {
	Write-Success "Found config.py: $configPath"
} else {
	Write-Warning-Custom "config.py not found: $configPath"
	Write-Info "Create config.py in project root if needed."
}

# ============================================================================
# 11. Environment Test (Optional)
# ============================================================================

Write-Header "Environment Test"

Write-Info ""
Write-Info "Run these commands to test the environment:"
Write-Host ""
Write-Host "  1. Activate virtual environment:" -ForegroundColor Yellow
Write-Host "     .\donkey_env\Scripts\Activate.ps1" -ForegroundColor White
Write-Host ""
Write-Host "  2. Check train.py help:" -ForegroundColor Yellow
Write-Host "     python python\train.py --help" -ForegroundColor White
Write-Host ""
Write-Host "  3. Check installed packages:" -ForegroundColor Yellow
Write-Host "     pip list" -ForegroundColor White
Write-Host ""

# ============================================================================
# 12. Completion Summary
# ============================================================================

Write-Header "Setup Complete"

Write-Info "Next steps:"
Write-Info ""
Write-Host "  1. Open SimpleDonkeyManager.sln in Visual Studio" -ForegroundColor Green
Write-Host "  2. Build project: Ctrl+Shift+B" -ForegroundColor Green
Write-Host "  3. Run app: F5 (or SimpleDonkeyManager.exe)" -ForegroundColor Green
Write-Host "  4. Select training data folder in Data tab" -ForegroundColor Green
Write-Host "  5. Start training!" -ForegroundColor Green
Write-Info ""

Write-Info "Setup Summary:"
Write-Host "  - .NET: $dotnetVersion" -ForegroundColor White
Write-Host "  - Python: $pythonVersion" -ForegroundColor White
Write-Host "  - Virtual env: $venvPath" -ForegroundColor White
Write-Host "  - Installed packages: $($installedPackages.Count)" -ForegroundColor White

if ($missingPackages.Count -gt 0) {
	Write-Warning-Custom "Missing packages: $($missingPackages -join ', ')"
	Write-Info "Install manually:"
	Write-Host "  $venvPython -m pip install $($missingPackages -join ' ')" -ForegroundColor Yellow
}

Write-Info ""
Write-Info "See DEPLOYMENT_GUIDE.md for troubleshooting."
Write-Info ""

# ============================================================================
# 13. Exit
# ============================================================================

Read-Host "Press Enter to exit"
