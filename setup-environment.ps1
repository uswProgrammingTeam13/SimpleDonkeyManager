#!/usr/bin/env powershell
<#
Compatibility launcher for DEPLOYMENT_GUIDE.md.
The real setup script lives in resources\setup-environment.ps1 so it can be
copied with the WinForms app resources.
#>

$scriptPath = Join-Path $PSScriptRoot "resources\setup-environment.ps1"

if (-not (Test-Path -LiteralPath $scriptPath)) {
    Write-Error "Could not find setup script: $scriptPath"
    exit 1
}

& $scriptPath @args
exit $LASTEXITCODE
