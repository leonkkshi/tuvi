# Script chạy Backend với Ngrok
# Sử dụng: .\start-backend-ngrok.ps1

Write-Host "=== Starting Backend with Ngrok ===" -ForegroundColor Green

# Kiểm tra ngrok đã cài chưa
if (-not (Get-Command ngrok -ErrorAction SilentlyContinue)) {
    Write-Host "ERROR: Ngrok chưa được cài đặt!" -ForegroundColor Red
    Write-Host "Vui lòng tải tại: https://ngrok.com/download" -ForegroundColor Yellow
    exit 1
}

# Kiểm tra Backend directory
if (-not (Test-Path "Backend")) {
    Write-Host "ERROR: Không tìm thấy thư mục Backend!" -ForegroundColor Red
    exit 1
}

# Start Backend trong background
Write-Host "Starting Backend..." -ForegroundColor Yellow
$backendJob = Start-Job -ScriptBlock {
    Set-Location $using:PWD
    cd Backend
    dotnet run
}

# Đợi Backend khởi động (khoảng 5 giây)
Write-Host "Waiting for Backend to start..." -ForegroundColor Yellow
Start-Sleep -Seconds 5

# Start Ngrok
Write-Host "`nStarting Ngrok tunnel on port 5015..." -ForegroundColor Yellow
Write-Host "Press Ctrl+C to stop all services`n" -ForegroundColor Cyan

try {
    # Chạy ngrok (sẽ hiển thị URL)
    ngrok http 5015
}
finally {
    # Cleanup khi thoát
    Write-Host "`nStopping Backend..." -ForegroundColor Yellow
    Stop-Job $backendJob
    Remove-Job $backendJob
    Write-Host "Done!" -ForegroundColor Green
}
