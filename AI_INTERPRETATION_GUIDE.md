# Tích Hợp AI Luận Giải Lá Số Tử Vi

## Tổng Quan

Hệ thống đã được tích hợp AI (OpenAI GPT-4) để luận giải lá số tử vi một cách tự động và chuyên nghiệp. AI sẽ phân tích các sao trong 12 cung và đưa ra những nhận định sâu sắc về cuộc đời, tính cách, sự nghiệp, tình duyên, sức khỏe và tài lộc.

## Cấu Hình

### 1. Cài Đặt OpenAI API Key

Mở file `Backend/appsettings.json` hoặc `Backend/appsettings.Development.json` và thêm API key của bạn:

```json
{
  "OpenAI": {
    "ApiKey": "sk-your-openai-api-key-here",
    "Model": "gpt-4"
  }
}
```

**Lưu ý**: 
- Bạn cần đăng ký tài khoản OpenAI tại https://platform.openai.com/
- Tạo API key tại https://platform.openai.com/api-keys
- Có thể sử dụng model khác như `gpt-3.5-turbo` để tiết kiệm chi phí

### 2. Cài Đặt Azure OpenAI (Tùy Chọn)

Nếu bạn muốn sử dụng Azure OpenAI thay vì OpenAI trực tiếp, cần chỉnh sửa file `Backend/Services/OpenAIInterpretationService.cs`:

```csharp
// Thay đổi URL từ
var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

// Thành Azure OpenAI endpoint
var response = await _httpClient.PostAsync("https://YOUR-RESOURCE.openai.azure.com/openai/deployments/YOUR-DEPLOYMENT/chat/completions?api-version=2024-02-01", content);
```

## Cách Sử Dụng

### 1. Chạy Backend

```bash
cd Backend
dotnet run
```

Backend sẽ chạy tại `http://localhost:5015`

### 2. Chạy Frontend

```bash
cd Frontend
npm install
npm start
```

Frontend sẽ chạy tại `http://localhost:4200`

### 3. Sử Dụng Tính Năng AI

1. **Nhập thông tin sinh** trong form
2. **Xem lá số** được tạo ra với đầy đủ các sao trong 12 cung
3. **Cuộn xuống phần "🤖 Luận Giải AI"**
4. **Chọn lĩnh vực** muốn tập trung:
   - Tổng quan
   - Sự nghiệp
   - Tình duyên
   - Sức khỏe
   - Tài lộc
5. **Click "Luận giải bằng AI"**
6. **Đợi 10-30 giây** để AI phân tích
7. **Xem kết quả** bao gồm:
   - Tổng quan về lá số
   - Điểm nổi bật
   - Cảnh báo
   - Khuyến nghị
   - Chi tiết 12 cung (nếu có)

## Cấu Trúc File

### Backend

```
Backend/
├── Models/
│   └── InterpretationModels.cs          # Models cho AI interpretation
├── Services/
│   ├── IAIInterpretationService.cs      # Interface
│   └── OpenAIInterpretationService.cs   # Implementation với OpenAI
├── Controllers/
│   └── ZodiacController.cs              # Endpoint /api/tuvi/ai-interpret
└── appsettings.json                     # Cấu hình API key
```

### Frontend

```
Frontend/src/app/
├── models/
│   └── interpretation.models.ts         # TypeScript models
├── services/
│   └── tu-vi.service.ts                 # Service gọi API
└── components/
    └── tu-vi-chart/
        ├── tu-vi-chart.component.ts     # Logic component
        ├── tu-vi-chart.component.html   # Template với UI
        └── tu-vi-chart.component.css    # Styling đẹp mắt
```

## API Endpoint

### POST /api/tuvi/ai-interpret

**Request Body:**
```json
{
  "chart": {
    // TuViChart object với đầy đủ thông tin lá số
  },
  "focusArea": "general" // hoặc "career", "love", "health", "wealth"
}
```

**Response:**
```json
{
  "overallInterpretation": "Tổng quan về lá số...",
  "palaceInterpretations": [
    {
      "palaceName": "Mệnh",
      "interpretation": "Cung Mệnh có sao Tử Vi...",
      "influencingStars": ["Tử Vi", "Thiên Phủ"]
    }
  ],
  "keyInsights": ["Điểm nổi bật 1", "Điểm nổi bật 2"],
  "warnings": ["Cảnh báo 1", "Cảnh báo 2"],
  "recommendations": ["Khuyến nghị 1", "Khuyến nghị 2"]
}
```

## Chi Phí

### OpenAI API
- GPT-4: ~$0.03/1K tokens input, ~$0.06/1K tokens output
- GPT-3.5-turbo: ~$0.001/1K tokens (rẻ hơn nhiều)
- Mỗi lần luận giải: ~2000-4000 tokens (~$0.10-0.30 với GPT-4)

### Tối Ưu Chi Phí
1. Sử dụng `gpt-3.5-turbo` thay vì `gpt-4`
2. Cache kết quả cho những lá số giống nhau
3. Giảm `max_tokens` trong request
4. Sử dụng Azure OpenAI với pricing tốt hơn

## Tùy Chỉnh

### Thay Đổi Prompt

Chỉnh sửa method `BuildPrompt()` trong `OpenAIInterpretationService.cs` để tùy chỉnh cách AI phân tích.

### Thay Đổi Model

Trong `appsettings.json`:
```json
{
  "OpenAI": {
    "Model": "gpt-3.5-turbo"  // hoặc "gpt-4-turbo-preview", "gpt-4o"
  }
}
```

### Cải Thiện Parsing

Method `ParseAIResponse()` có thể được cải thiện để:
- Yêu cầu AI trả về JSON structured
- Parse chính xác hơn các section
- Trích xuất thông tin từ markdown format

## Lỗi Thường Gặp

### 1. "Không thể luận giải lá số"
- Kiểm tra API key có đúng không
- Kiểm tra có đủ credits trong tài khoản OpenAI không
- Xem console log để biết lỗi cụ thể

### 2. CORS Error
- Đảm bảo backend đang chạy
- Kiểm tra CORS configuration trong `Program.cs`

### 3. Kết quả không như mong đợi
- Điều chỉnh prompt để cụ thể hơn
- Tăng `temperature` (0.7-0.9) để sáng tạo hơn
- Tăng `max_tokens` để được phản hồi dài hơn

## Phát Triển Tiếp

### Tính Năng Có Thể Thêm

1. **Cache kết quả**: Lưu interpretation đã phân tích vào database
2. **History**: Lưu lịch sử các lần luận giải
3. **Export PDF**: Xuất kết quả ra file PDF
4. **Multiple AI providers**: Hỗ trợ Anthropic Claude, Google Gemini
5. **Streaming response**: Hiển thị kết quả từng phần khi AI đang phân tích
6. **Voice reading**: Đọc kết quả bằng text-to-speech
7. **Comparison**: So sánh 2 lá số với nhau

### Cải Thiện Chất Lượng

1. **Fine-tuning**: Train model riêng với dữ liệu Tử Vi chuyên sâu
2. **RAG (Retrieval Augmented Generation)**: Kết hợp với knowledge base Tử Vi
3. **Prompt engineering**: Tối ưu prompt để có kết quả tốt hơn
4. **Validation**: Kiểm tra tính chính xác của AI interpretation

## Bảo Mật

⚠️ **QUAN TRỌNG**:
- **KHÔNG** commit API key lên Git
- Sử dụng Environment Variables hoặc Azure Key Vault
- Thêm `appsettings.Development.json` vào `.gitignore`
- Rate limiting để tránh lạm dụng

## Hỗ Trợ

Nếu có vấn đề, vui lòng:
1. Kiểm tra log trong browser console (F12)
2. Kiểm tra log trong terminal backend
3. Đọc kỹ error message
4. Tham khảo OpenAI documentation: https://platform.openai.com/docs

---

**Chúc bạn thành công với tính năng AI Luận Giải Lá Số Tử Vi!** 🎉
