# Script update Frontend API URL để trỏ đến Ngrok
# Sử dụng: .\update-api-url.ps1 <ngrok-url>
# Ví dụ: .\update-api-url.ps1 https://abc123.ngrok-free.app

param(
    [Parameter(Mandatory=$true)]
    [string]$NgrokUrl
)

$envFile = "Frontend\src\environments\environment.development.ts"

if (-not (Test-Path $envFile)) {
    Write-Host "ERROR: Không tìm thấy file $envFile" -ForegroundColor Red
    exit 1
}

# Loại bỏ trailing slash nếu có
$NgrokUrl = $NgrokUrl.TrimEnd('/')

# Tạo nội dung mới
$newContent = @"
export const environment = {
  production: false,
  apiUrl: '$NgrokUrl/api/TuVi'
};
"@

# Ghi vào file
Set-Content -Path $envFile -Value $newContent -Encoding UTF8

Write-Host "✓ Updated API URL to: $NgrokUrl/api/TuVi" -ForegroundColor Green
Write-Host "`nBây giờ bạn có thể chạy Frontend:" -ForegroundColor Yellow
Write-Host "  cd Frontend" -ForegroundColor Cyan
Write-Host "  npm start" -ForegroundColor Cyan
