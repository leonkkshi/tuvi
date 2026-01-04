# 🚀 Hướng Dẫn Deploy Tử Vi Application

## 📋 Tổng Quan

Project có 3 options deploy **MIỄN PHÍ**:
1. **Railway** - Tốt nhất cho .NET + tự động SSL
2. **Render** - Ổn định, nhiều region
3. **Vercel (Frontend) + Railway/Render (Backend)** - Hiệu suất tốt nhất

---

## 🎯 Option 1: RAILWAY (Khuyên dùng - Dễ nhất)

### ✅ Ưu điểm:
- ✨ Deploy cực dễ (1 click)
- 🚀 Tự động build từ GitHub
- 🔒 Free SSL certificate
- 💰 $5 credit/tháng (đủ dùng)
- 🔄 Auto deploy khi push code

### 📝 Các bước deploy:

#### 1️⃣ Push code lên GitHub
```bash
git init
git add .
git commit -m "Initial commit"
git branch -M main
git remote add origin <your-github-repo-url>
git push -u origin main
```

#### 2️⃣ Deploy Backend
1. Truy cập: https://railway.app/
2. Sign in với GitHub
3. Click **"New Project"** → **"Deploy from GitHub repo"**
4. Chọn repo `tuvi`
5. Railway tự động detect Dockerfile
6. Add Environment Variables:
   ```
   ASPNETCORE_ENVIRONMENT=Production
   AI__Provider=Gemini
   Gemini__ApiKey=<your-gemini-api-key>
   AI__MaxConcurrentRequests=3
   ```
7. Click **"Deploy"**
8. Copy **Backend URL** (vd: `https://tuvi-backend.railway.app`)

#### 3️⃣ Deploy Frontend
1. Trong cùng Railway project, click **"New"** → **"Service"**
2. Chọn repo, nhưng set **Root Directory** = `Frontend`
3. Railway auto-detect Angular
4. Add Environment Variable:
   ```
   API_URL=https://tuvi-backend.railway.app
   ```
5. Deploy và lấy **Frontend URL**

#### 4️⃣ Update CORS trong Backend
Vào Railway dashboard → Backend service → Add variable:
```
AllowedOrigins__0=https://<your-frontend-url>.railway.app
```

---

## 🎯 Option 2: RENDER

### ✅ Ưu điểm:
- 🌏 Region Singapore gần VN
- 📊 Dashboard rõ ràng
- 🆓 Hoàn toàn free (có giới hạn)

### 📝 Các bước deploy:

#### 1️⃣ Push code lên GitHub (nếu chưa)

#### 2️⃣ Deploy qua render.yaml
1. Truy cập: https://render.com/
2. Sign in với GitHub
3. Click **"New"** → **"Blueprint"**
4. Connect repo `tuvi`
5. Render đọc file `render.yaml` và tự động tạo 2 services:
   - `tuvi-backend` (Web Service)
   - `tuvi-frontend` (Static Site)
6. Set Environment Variables cho Backend:
   ```
   Gemini__ApiKey=<your-api-key>
   ```
7. Click **"Apply"**
8. Đợi 5-10 phút build

#### 3️⃣ Lấy URLs
- Backend: `https://tuvi-backend.onrender.com`
- Frontend: `https://tuvi-frontend.onrender.com`

⚠️ **Lưu ý**: Render free tier có sleep sau 15 phút không dùng. First request sẽ chậm (~30s).

---

## 🎯 Option 3: VERCEL (Frontend) + RAILWAY (Backend)

### ✅ Ưu điểm:
- ⚡ Frontend cực nhanh (Vercel CDN toàn cầu)
- 🎨 Best cho Angular/React
- 🔧 Backend ổn định (Railway)

### 📝 Các bước deploy:

#### 1️⃣ Deploy Backend trên Railway (xem Option 1)

#### 2️⃣ Deploy Frontend trên Vercel
1. Truy cập: https://vercel.com/
2. Sign in với GitHub
3. Click **"Add New"** → **"Project"**
4. Import `tuvi` repo
5. Set:
   - **Framework Preset**: Angular
   - **Root Directory**: `Frontend`
   - **Build Command**: `npm run build:prod`
   - **Output Directory**: `dist/frontend/browser`
6. Add Environment Variable:
   ```
   API_URL=https://tuvi-backend.railway.app
   ```
7. Click **"Deploy"**

#### 3️⃣ Update API URL trong Frontend
Sau khi deploy, bạn cần update API endpoint trong Angular environment:

```typescript
// Frontend/src/environments/environment.ts
export const environment = {
  production: true,
  apiUrl: 'https://tuvi-backend.railway.app/api'
};
```

Push changes, Vercel tự động redeploy.

---

## 🧪 Test Local với Docker

Trước khi deploy, test local:

```bash
# Build và run
docker-compose up --build

# Test:
# - Backend: http://localhost:5000/api/tuvi/health
# - Frontend: http://localhost:4200
```

---

## 🔧 Cấu hình cho từng môi trường

### Railway Environment Variables:
```env
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:8080
AI__Provider=Gemini
Gemini__ApiKey=AIzaSyD1Jm0qr-tdD015UsK7oOajq00cFxa_WNQ
AI__MaxConcurrentRequests=3
```

### Render Environment Variables:
```env
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:8080
AI__Provider=Gemini
Gemini__ApiKey=AIzaSyD1Jm0qr-tdD015UsK7oOajq00cFxa_WNQ
AI__MaxConcurrentRequests=2
```
*(Giảm xuống 2 vì Render free tier có ít RAM hơn)*

---

## 📊 So sánh Free Tiers

| Feature | Railway | Render | Vercel |
|---------|---------|--------|--------|
| Credit/tháng | $5 | Unlimited | Unlimited |
| Build time | ~2-3 phút | ~5-7 phút | ~1-2 phút |
| Auto-sleep | ❌ Không | ✅ Sau 15 phút | ❌ Không |
| SSL | ✅ Tự động | ✅ Tự động | ✅ Tự động |
| Region | US/EU | Singapore | Global CDN |
| Best cho | Backend .NET | Full-stack | Frontend |

---

## 🔍 Troubleshooting

### Backend không start được:
```bash
# Check logs trên Railway/Render dashboard
# Thường do:
# - Thiếu environment variables
# - Port mapping sai (phải dùng port 8080)
# - .NET SDK version không đúng
```

### Frontend không connect được Backend:
```bash
# 1. Check CORS trong Backend Program.cs
# 2. Verify API_URL environment variable
# 3. Check Network tab trong browser DevTools
```

### Memory/Performance issues:
```bash
# Giảm AI__MaxConcurrentRequests:
# - Railway: 3 (2GB RAM)
# - Render: 2 (512MB RAM)
```

---

## 🎓 Các lệnh hữu ích

```bash
# Build local
dotnet build Backend/Backend.csproj
cd Frontend && npm run build:prod

# Test Docker local
docker build -t tuvi-backend ./Backend
docker run -p 8080:8080 tuvi-backend

# Check health
curl http://localhost:8080/api/tuvi/health

# View logs (Railway)
railway logs

# View logs (Docker)
docker-compose logs -f
```

---

## 🚀 Workflow Tối Ưu

1. **Develop local** → Test với `docker-compose`
2. **Push to GitHub** → Trigger auto-deploy
3. **Monitor** → Check health endpoint
4. **Scale** → Upgrade plan nếu cần

---

## 💡 Tips & Best Practices

✅ **DO:**
- Luôn test với Docker local trước
- Set timeout cho AI requests
- Monitor memory qua `/api/tuvi/health`
- Use environment variables cho secrets
- Enable rate limiting trong production

❌ **DON'T:**
- Hard-code API keys trong code
- Deploy mà không test CORS
- Quên set ASPNETCORE_URLS=http://+:8080
- Dùng .NET 10 (chưa stable, dùng .NET 9)

---

## 📞 Support

Nếu gặp vấn đề:
1. Check logs trên dashboard
2. Test health endpoint: `/api/tuvi/health`
3. Verify environment variables
4. Check GitHub Actions (nếu dùng CI/CD)

---

## 🎉 Kết Luận

**Khuyến nghị cho bạn:**
- 🥇 **RAILWAY** - Nếu muốn dễ, nhanh, ổn định
- 🥈 **RENDER** - Nếu lo ngại về credit limit
- 🥉 **VERCEL + RAILWAY** - Nếu muốn frontend siêu nhanh

Chúc bạn deploy thành công! 🚀
