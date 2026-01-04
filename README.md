# Tử Vi Đẩu Số - Ứng Dụng Xem Lá Số Tử Vi

Ứng dụng web full-stack cho phép người dùng tạo và xem lá số Tử Vi Đẩu Số dựa trên ngày giờ sinh.

## 🚀 Quick Start - Deployment

### Deploy lên Production (FREE)

```powershell
# Option 1: Deploy helper script
.\deploy.ps1

# Option 2: Test local với Docker
.\test-deployment.ps1
```

📖 **Chi tiết deployment**: Xem [DEPLOYMENT.md](DEPLOYMENT.md)

**3 options FREE:**
- 🥇 **Railway** - Dễ nhất, tự động SSL
- 🥈 **Render** - Ổn định, region Singapore
- 🥉 **Vercel + Railway** - Frontend siêu nhanh

---

## Công Nghệ

### Backend
- **ASP.NET Core 10** - Web API
- **C#** - Ngôn ngữ lập trình
- 12 cung: Mệnh, Phụ Mẫu, Phúc Đức, Điền Trạch, Quan Lộc, Nô Bộc, Thiên Di, Tật Ách, Tài Bạch, Tử Nữ, Phu Thê, Huynh Đệ
- 14+ sao chính: Tử Vi, Thiên Cơ, Thái Dương, Vũ Khúc, Thiên Đồng, Liêm Trinh, Thiên Phủ, Thái Âm, Tham Lang, Cự Môn, Thiên Tướng, Thiên Lương, Thất Sát, Phá Quân

### Frontend
- **Angular 19** - Framework
- **TypeScript** - Ngôn ngữ lập trình
- **CSS** - Styling

## Cấu Trúc Dự Án

```
tuvi/
├── Backend/                 # ASP.NET Core Web API
│   ├── Controllers/        # API Controllers
│   │   └── TuViController.cs
│   ├── Models/            # Data models
│   │   ├── Palace.cs      # 12 Cung
│   │   ├── Horoscope.cs   # Sao và Lá số
│   │   └── BirthInfo.cs   # Thông tin sinh
│   ├── Services/          # Business logic
│   │   ├── ITuViService.cs
│   │   └── TuViService.cs
│   └── Program.cs         # Entry point
│
└── Frontend/              # Angular Application
    ├── src/app/
    │   ├── components/    # UI Components
    │   │   ├── birth-form/
    │   │   └── tu-vi-chart/
    │   ├── models/        # TypeScript interfaces
    │   ├── services/      # API services
    │   └── app.component.*
    └── package.json
```

## Cài Đặt và Chạy

### Chạy Local (Cùng một máy)

#### Backend (ASP.NET Core)

1. Di chuyển vào thư mục Backend:
```bash
cd Backend
dotnet run
```

API sẽ chạy tại: http://localhost:5015

#### Frontend (Angular)

1. Di chuyển vào thư mục Frontend:
```bash
cd Frontend
npm install  # Chỉ cần chạy lần đầu
npm start
```

Ứng dụng sẽ chạy tại: http://localhost:4200

### Chạy từ Máy Khác (Qua Ngrok) 🌐

Để chia sẻ ứng dụng cho người khác truy cập từ máy khác/mạng khác:

#### 1. Cài đặt Ngrok
- Tải tại: https://ngrok.com/download
- Đăng ký và lấy authtoken

#### 2. Sử dụng Script tự động (Khuyến nghị)
```powershell
# Chạy Backend với Ngrok
.\start-backend-ngrok.ps1
```

Script sẽ tự động:
- Khởi động Backend
- Tạo tunnel ngrok
- Hiển thị URL công khai

#### 3. Copy URL ngrok và cập nhật Frontend
```powershell
# Cập nhật API URL (thay YOUR_NGROK_URL bằng URL thực tế)
.\update-api-url.ps1 https://abc123.ngrok-free.app
```

#### 4. Chạy Frontend
```bash
cd Frontend
npm start
```

**Chi tiết đầy đủ**: Xem file [NGROK_SETUP.md](NGROK_SETUP.md)

### Backend (ASP.NET Core)

1. Di chuyển vào thư mục Backend:
```bash
cd Backend
```

2. Khôi phục dependencies:
```bash
dotnet restore
```

3. Chạy backend:
```bash
dotnet run
```

Backend sẽ chạy tại: `https://localhost:7296`

### Frontend (Angular)

1. Di chuyển vào thư mục Frontend:
```bash
cd Frontend
```

2. Cài đặt dependencies:
```bash
npm install
```

3. Chạy frontend:
```bash
ng serve
```

Frontend sẽ chạy tại: `http://localhost:4200`

## Sử Dụng

1. Mở trình duyệt và truy cập `http://localhost:4200`
2. Nhập thông tin sinh:
   - Năm sinh (Âm lịch)
   - Tháng sinh
   - Ngày sinh
   - Giờ sinh
   - Phút sinh
   - Giới tính
3. Chọn "Âm lịch" nếu ngày sinh theo âm lịch
4. Nhấn "An Sao Tử Vi" để xem lá số
5. Hệ thống sẽ hiển thị 12 cung với các sao tương ứng

## API Endpoints

### GET /api/TuVi/palaces
Lấy danh sách 12 cung

### GET /api/TuVi/stars
Lấy danh sách các sao

### POST /api/TuVi/generate-chart
Tạo lá số Tử Vi
```json
{
  "year": 1990,
  "month": 1,
  "day": 15,
  "hour": 12,
  "minute": 0,
  "isMale": true,
  "isLunar": true
}
```

## Lưu Ý

- Đây là phiên bản đơn giản hóa của Tử Vi Đẩu Số
- Thuật toán an sao hiện tại sử dụng phương pháp ngẫu nhiên có hạt giống (seeded random) dựa trên thông tin sinh
- Để có độ chính xác cao hơn, cần implement thuật toán an sao truyền thống đầy đủ dựa trên Can Chi, Âm Dương Ngũ Hành

## Phát Triển Tiếp

- [ ] Implement thuật toán an sao chính xác theo Tử Vi Đẩu Số truyền thống
- [ ] Thêm chuyển đổi Dương lịch - Âm lịch
- [ ] Thêm giải đoán chi tiết cho từng cung
- [ ] Thêm xem vận hạn theo năm
- [ ] Thêm in lá số và xuất PDF
- [ ] Lưu lịch sử các lá số đã xem

## License

MIT License
