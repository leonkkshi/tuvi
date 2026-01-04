# Hướng dẫn chạy ứng dụng với Ngrok

## 1. Cài đặt Ngrok
- Tải tại: https://ngrok.com/download
- Đăng ký tài khoản miễn phí tại: https://dashboard.ngrok.com/signup
- Lấy authtoken từ: https://dashboard.ngrok.com/get-started/your-authtoken

## 2. Xác thực Ngrok (chỉ cần làm 1 lần)
```bash
ngrok config add-authtoken <YOUR_AUTHTOKEN>
```

## 3. Chạy Backend
Mở terminal thứ nhất tại thư mục Backend:
```bash
cd C:\tuvi\Backend
dotnet run
```
Backend sẽ chạy tại: http://localhost:5015

## 4. Expose Backend qua Ngrok
Mở terminal thứ hai:
```bash
ngrok http 5015
```

Ngrok sẽ hiển thị một URL công khai, ví dụ:
```
Forwarding  https://abc123.ngrok-free.app -> http://localhost:5015
```

## 5. Cập nhật Frontend API URL
Mở file: `Frontend/src/environments/environment.development.ts`

Thay đổi `apiUrl` thành URL ngrok của bạn:
```typescript
export const environment = {
  production: false,
  apiUrl: 'https://abc123.ngrok-free.app/api/TuVi'  // Thay abc123 bằng URL ngrok của bạn
};
```

## 6. Chạy Frontend
Mở terminal thứ ba tại thư mục Frontend:
```bash
cd C:\tuvi\Frontend
npm start
```

Hoặc để cho phép truy cập từ mọi thiết bị trong mạng:
```bash
ng serve --host 0.0.0.0
```

## 7. Truy cập ứng dụng
- **Trên máy chủ**: http://localhost:4200
- **Từ máy khác trong mạng**: http://[IP_MÁY_CHỦ]:4200
- **Từ máy khác khác mạng**: Có thể expose Frontend qua ngrok riêng

## Expose cả Frontend qua Ngrok (tùy chọn)
Nếu muốn chia sẻ cả Frontend:

Mở terminal thứ tư:
```bash
ngrok http 4200
```

Bạn sẽ nhận được URL Frontend ngrok, ví dụ: https://xyz789.ngrok-free.app

Chia sẻ URL này cho người khác để họ truy cập!

## Lưu ý
- URL ngrok miễn phí sẽ thay đổi mỗi lần restart ngrok
- Ngrok miễn phí có giới hạn 40 connections/phút
- Backend CORS đã được cấu hình để chấp nhận request từ ngrok
