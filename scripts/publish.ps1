# Build portable single-file exe (no installer)
# Costura.Fody embeds dependency DLLs into the main exe

$ErrorActionPreference = "Stop"
Set-Location (Split-Path -Parent $PSScriptRoot)

$nuget = Join-Path $env:TEMP "nuget.exe"
if (-not (Test-Path $nuget)) {
    Write-Host "Downloading nuget.exe..." -ForegroundColor Cyan
    Invoke-WebRequest -Uri "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" -OutFile $nuget
}

$msbuild = & "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" `
    -latest -requires Microsoft.Component.MSBuild `
    -find "MSBuild\**\Bin\MSBuild.exe" | Select-Object -First 1

if (-not $msbuild) {
    throw "MSBuild not found. Install Visual Studio or Build Tools."
}

$solution = (Get-ChildItem -Filter "*.sln" | Select-Object -First 1).FullName
if (-not $solution) {
    throw "No .sln file found in $(Get-Location)"
}

Write-Host "Restoring NuGet packages..." -ForegroundColor Cyan
& $nuget restore $solution

Write-Host "Building Release (single-file)..." -ForegroundColor Cyan
& $msbuild $solution /p:Configuration=Release /v:minimal

$exe = Get-ChildItem -Recurse -Filter "*.exe" |
    Where-Object { $_.FullName -match "\\bin\\Release\\" -and $_.Name -notmatch "vshost" } |
    Select-Object -First 1

if (-not $exe) {
    throw "Build failed: Release exe not found"
}

$outDir = Join-Path (Get-Location) "publish"
New-Item -ItemType Directory -Force -Path $outDir | Out-Null
$dest = Join-Path $outDir "OpenCopy.exe"
Copy-Item $exe.FullName $dest -Force

Write-Host ""
Write-Host "Done." -ForegroundColor Green
Write-Host "Portable exe: $dest"
Write-Host "Copy this single file anywhere and run. No installer required."
Write-Host "Requires .NET Framework 4.7.2+ (preinstalled on Windows 10/11)."
