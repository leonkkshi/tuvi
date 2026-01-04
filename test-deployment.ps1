# Script để test local deployment với Docker

Write-Host "🚀 Testing Tử Vi Application with Docker..." -ForegroundColor Green
Write-Host ""

# Check if Docker is running
Write-Host "1️⃣ Checking Docker..." -ForegroundColor Yellow
docker --version
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Docker is not installed or not running!" -ForegroundColor Red
    exit 1
}
Write-Host "✅ Docker is ready" -ForegroundColor Green
Write-Host ""

# Stop existing containers
Write-Host "2️⃣ Stopping existing containers..." -ForegroundColor Yellow
docker-compose down
Write-Host ""

# Build and start
Write-Host "3️⃣ Building and starting containers..." -ForegroundColor Yellow
docker-compose up --build -d
Write-Host ""

# Wait for services to be ready
Write-Host "4️⃣ Waiting for services to start..." -ForegroundColor Yellow
Start-Sleep -Seconds 10

# Check backend health
Write-Host "5️⃣ Checking backend health..." -ForegroundColor Yellow
$maxRetries = 10
$retryCount = 0
$backendReady = $false

while ($retryCount -lt $maxRetries -and -not $backendReady) {
    try {
        $response = Invoke-WebRequest -Uri "http://localhost:5000/api/tuvi/health" -UseBasicParsing -TimeoutSec 5
        if ($response.StatusCode -eq 200) {
            $backendReady = $true
            Write-Host "✅ Backend is healthy!" -ForegroundColor Green
            $response.Content | ConvertFrom-Json | ConvertTo-Json -Depth 10
        }
    }
    catch {
        $retryCount++
        Write-Host "⏳ Waiting for backend... (Attempt $retryCount/$maxRetries)" -ForegroundColor Yellow
        Start-Sleep -Seconds 3
    }
}

if (-not $backendReady) {
    Write-Host "❌ Backend failed to start!" -ForegroundColor Red
    Write-Host "📋 Backend logs:" -ForegroundColor Yellow
    docker-compose logs backend
    exit 1
}
Write-Host ""

# Check frontend
Write-Host "6️⃣ Checking frontend..." -ForegroundColor Yellow
try {
    $response = Invoke-WebRequest -Uri "http://localhost:4200" -UseBasicParsing -TimeoutSec 5
    if ($response.StatusCode -eq 200) {
        Write-Host "✅ Frontend is ready!" -ForegroundColor Green
    }
}
catch {
    Write-Host "⚠️ Frontend might take longer to build..." -ForegroundColor Yellow
}
Write-Host ""

# Show URLs
Write-Host "🎉 Application is running!" -ForegroundColor Green
Write-Host ""
Write-Host "📍 URLs:" -ForegroundColor Cyan
Write-Host "   Backend:  http://localhost:5000" -ForegroundColor White
Write-Host "   Health:   http://localhost:5000/api/tuvi/health" -ForegroundColor White
Write-Host "   Frontend: http://localhost:4200" -ForegroundColor White
Write-Host ""
Write-Host "📊 View logs:" -ForegroundColor Cyan
Write-Host "   docker-compose logs -f" -ForegroundColor White
Write-Host ""
Write-Host "🛑 Stop application:" -ForegroundColor Cyan
Write-Host "   docker-compose down" -ForegroundColor White
Write-Host ""

# Ask if user wants to view logs
$viewLogs = Read-Host "Do you want to view logs? (y/n)"
if ($viewLogs -eq 'y') {
    docker-compose logs -f
}
