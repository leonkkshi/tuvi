# Script deploy lên Railway/Render
Write-Host "🚀 Deployment Helper for Tử Vi Application" -ForegroundColor Green
Write-Host ""

# Menu
Write-Host "Select deployment option:" -ForegroundColor Cyan
Write-Host "1. Railway (Recommended)" -ForegroundColor White
Write-Host "2. Render" -ForegroundColor White
Write-Host "3. Vercel (Frontend only)" -ForegroundColor White
Write-Host "4. Test local with Docker" -ForegroundColor White
Write-Host "5. Push to GitHub" -ForegroundColor White
Write-Host ""

$choice = Read-Host "Enter your choice (1-5)"

switch ($choice) {
    "1" {
        Write-Host ""
        Write-Host "📦 Railway Deployment" -ForegroundColor Green
        Write-Host "1. Make sure your code is pushed to GitHub" -ForegroundColor Yellow
        Write-Host "2. Go to: https://railway.app/" -ForegroundColor Cyan
        Write-Host "3. Click 'New Project' → 'Deploy from GitHub repo'" -ForegroundColor Yellow
        Write-Host "4. Select your repository" -ForegroundColor Yellow
        Write-Host "5. Set these Environment Variables:" -ForegroundColor Yellow
        Write-Host ""
        Write-Host "   ASPNETCORE_ENVIRONMENT=Production" -ForegroundColor White
        Write-Host "   AI__Provider=Gemini" -ForegroundColor White
        Write-Host "   Gemini__ApiKey=<your-api-key>" -ForegroundColor White
        Write-Host "   AI__MaxConcurrentRequests=3" -ForegroundColor White
        Write-Host ""
        Write-Host "6. Deploy and copy the Backend URL" -ForegroundColor Yellow
        Write-Host "7. Create another service for Frontend with Root Directory = Frontend" -ForegroundColor Yellow
        Write-Host ""
        
        $openBrowser = Read-Host "Open Railway in browser? (y/n)"
        if ($openBrowser -eq 'y') {
            Start-Process "https://railway.app/"
        }
    }
    
    "2" {
        Write-Host ""
        Write-Host "📦 Render Deployment" -ForegroundColor Green
        Write-Host "1. Make sure your code is pushed to GitHub" -ForegroundColor Yellow
        Write-Host "2. Make sure render.yaml exists in root" -ForegroundColor Yellow
        Write-Host "3. Go to: https://render.com/" -ForegroundColor Cyan
        Write-Host "4. Click 'New' → 'Blueprint'" -ForegroundColor Yellow
        Write-Host "5. Connect your repository" -ForegroundColor Yellow
        Write-Host "6. Render will auto-detect render.yaml" -ForegroundColor Yellow
        Write-Host "7. Set Gemini__ApiKey in Backend service settings" -ForegroundColor Yellow
        Write-Host ""
        
        $openBrowser = Read-Host "Open Render in browser? (y/n)"
        if ($openBrowser -eq 'y') {
            Start-Process "https://render.com/"
        }
    }
    
    "3" {
        Write-Host ""
        Write-Host "📦 Vercel Deployment (Frontend only)" -ForegroundColor Green
        Write-Host "1. Make sure Frontend code is committed" -ForegroundColor Yellow
        Write-Host "2. Go to: https://vercel.com/" -ForegroundColor Cyan
        Write-Host "3. Click 'Add New' → 'Project'" -ForegroundColor Yellow
        Write-Host "4. Import your repository" -ForegroundColor Yellow
        Write-Host "5. Set Root Directory to 'Frontend'" -ForegroundColor Yellow
        Write-Host "6. Framework: Angular" -ForegroundColor Yellow
        Write-Host "7. Build Command: npm run build:prod" -ForegroundColor Yellow
        Write-Host "8. Output Directory: dist/frontend/browser" -ForegroundColor Yellow
        Write-Host "9. Add Environment Variable: API_URL=<your-backend-url>" -ForegroundColor Yellow
        Write-Host ""
        
        $openBrowser = Read-Host "Open Vercel in browser? (y/n)"
        if ($openBrowser -eq 'y') {
            Start-Process "https://vercel.com/"
        }
    }
    
    "4" {
        Write-Host ""
        Write-Host "🐳 Running local Docker test..." -ForegroundColor Green
        Write-Host ""
        & "$PSScriptRoot\test-deployment.ps1"
    }
    
    "5" {
        Write-Host ""
        Write-Host "📤 Pushing to GitHub..." -ForegroundColor Green
        Write-Host ""
        
        $repoUrl = Read-Host "Enter GitHub repository URL (or press Enter to skip)"
        
        if ($repoUrl) {
            git init
            git add .
            $commitMsg = Read-Host "Enter commit message (default: 'Update deployment config')"
            if (-not $commitMsg) {
                $commitMsg = "Update deployment config"
            }
            git commit -m $commitMsg
            git branch -M main
            git remote remove origin 2>$null
            git remote add origin $repoUrl
            git push -u origin main
            
            Write-Host ""
            Write-Host "✅ Code pushed successfully!" -ForegroundColor Green
        }
        else {
            Write-Host "Commands to push manually:" -ForegroundColor Yellow
            Write-Host ""
            Write-Host "git add ." -ForegroundColor White
            Write-Host "git commit -m 'Add deployment config'" -ForegroundColor White
            Write-Host "git push" -ForegroundColor White
        }
    }
    
    default {
        Write-Host "Invalid choice!" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "📖 For detailed instructions, see DEPLOYMENT.md" -ForegroundColor Cyan
