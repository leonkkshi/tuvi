namespace Backend.Services.StarPlacement;

/// <summary>
/// Service an 14 chính tinh theo bảng Tử Vi chuẩn.
/// </summary>
public class MainStarPlacementService : IStarPlacementService
{
    public Dictionary<int, List<int>> PlaceStars(StarPlacementContext context)
    {
        var positions = new Dictionary<int, List<int>>();
        for (int i = 1; i <= 12; i++) positions[i] = new List<int>();

        int nguHanhCuc = context.NguHanhCuc;
        int day = context.Day;

        // Tính Tử Vi theo công thức: Ngày sinh / Số cục = N dư X
        // X (số dư) = đi theo đường gấp khúc Tử Vi
        // N (thương số) = khởi thuận từ vị trí cuối cùng của X
        
        int N = day / nguHanhCuc;  // Thương số
        int X = day % nguHanhCuc;  // Số dư
        if (X == 0) { X = nguHanhCuc; N--; }  // Nếu chia hết thì số dư = số cục
        
        // Đường gấp khúc Tử Vi (theo thứ tự):
        // Lục 6 (Hỏa Lục) → Dậu(10)
        // Ngũ 5 (Thổ Ngũ) → Ngọ(7)
        // Tứ 4 (Kim Tứ) → Hợi(12)
        // Tam 3 (Mộc Tam) → Thìn(5)
        // Nhị 2 (Thủy Nhị) → Sửu(2)
        // Nhất 1 → Dần(3)
        int[] gapKhuc = { 10, 7, 12, 5, 2, 3 };  // Dậu, Ngọ, Hợi, Thìn, Sửu, Dần
        
        // Tìm vị trí bắt đầu dựa vào cục (6, 5, 4, 3, 2)
        int startIndex = 6 - nguHanhCuc;  // Lục=0, Ngũ=1, Tứ=2, Tam=3, Nhị=4
        
        // Bước 1: Đi theo đường gấp khúc X bước
        int currentPos = gapKhuc[startIndex];
        for (int i = 1; i < X; i++)
        {
            startIndex++;
            if (startIndex >= gapKhuc.Length) startIndex = 0;
            currentPos = gapKhuc[startIndex];
        }
        
        // Bước 2: Từ vị trí cuối, khởi thuận N bước
        int tuViPos = currentPos;
        for (int i = 0; i < N; i++)
        {
            tuViPos++;
            if (tuViPos > 12) tuViPos = 1;
        }

        // An 14 chính tinh theo quy tắc từ Tử Vi
        // Nhóm 1: Từ Tử Vi đi ngược chiều kim đồng hồ
        positions[tuViPos].Add(1); // Tử Vi
        
        int thienCoPos = Prev(tuViPos, 1); // Thiên Cơ: ngược 1 ô (liền kề)
        positions[thienCoPos].Add(2);
        
        int thaiDuongPos = Prev(thienCoPos, 2); // Thái Dương: cách Thiên Cơ 1 ô (bỏ qua 1 cung, ngược 2 ô)
        positions[thaiDuongPos].Add(3);
        
        int vuKhucPos = Prev(thaiDuongPos, 1); // Vũ Khúc: ngược thêm 1 ô (liền kề)
        positions[vuKhucPos].Add(4);
        
        int thienDongPos = Prev(vuKhucPos, 1); // Thiên Đồng: ngược thêm 1 ô (liền kề)
        positions[thienDongPos].Add(5);
        
        int liemTrinhPos = Prev(thienDongPos, 3); // Liêm Trinh: cách Thiên Đồng 2 ô (bỏ qua 2 cung, ngược 3 ô)
        positions[liemTrinhPos].Add(6);

        // Nhóm 2: Thiên Phủ đối xứng với Tử Vi qua trục Dần(3)-Thân(9)
        int thienPhuPos = GetSymmetricPosition(tuViPos);
        positions[thienPhuPos].Add(7); // Thiên Phủ
        
        // Từ Thiên Phủ đi thuận chiều kim đồng hồ
        int thaiAmPos = Next(thienPhuPos, 1); // Thái Âm
        positions[thaiAmPos].Add(8);
        
        int thamLangPos = Next(thaiAmPos, 1); // Tham Lang
        positions[thamLangPos].Add(9);
        
        int cuMonPos = Next(thamLangPos, 1); // Cự Môn
        positions[cuMonPos].Add(10);
        
        int thienTuongPos = Next(cuMonPos, 1); // Thiên Tướng
        positions[thienTuongPos].Add(11);
        
        int thienLuongPos = Next(thienTuongPos, 1); // Thiên Lương
        positions[thienLuongPos].Add(12);
        
        int thatSatPos = Next(thienLuongPos, 1); // Thất Sát
        positions[thatSatPos].Add(13);
        
        int phaQuanPos = Next(thatSatPos, 4); // Phá Quân: bỏ 3 cung (tức là +4)
        positions[phaQuanPos].Add(14);

        return positions;
    }

    // Helper: Đi ngược chiều kim đồng hồ
    private int Prev(int pos, int steps)
    {
        int result = pos - steps;
        while (result <= 0) result += 12;
        return result;
    }

    // Helper: Đi thuận chiều kim đồng hồ
    private int Next(int pos, int steps)
    {
        int result = pos + steps;
        while (result > 12) result -= 12;
        return result;
    }

    // Helper: Tính vị trí đối xứng qua trục Dần(3)-Thân(9)
    private int GetSymmetricPosition(int pos)
    {
        // Công thức đối xứng qua trục Dần(3)-Thân(9):
        // Nếu Tử Vi tại Dần(3) hoặc Thân(9) thì Thiên Phủ cũng tại đó (đồng cung)
        if (pos == 3 || pos == 9) return pos;
        
        // Các cặp đối xứng qua trục Dần-Thân (theo hướng dẫn):
        // Mão(4) <-> Sửu(2), Thìn(5) <-> Tý(1), Tỵ(6) <-> Hợi(12)
        // Ngọ(7) <-> Tuất(11), Mùi(8) <-> Dậu(10)
        return pos switch
        {
            1 => 5,   // Tý -> Thìn
            2 => 4,   // Sửu -> Mão
            4 => 2,   // Mão -> Sửu
            5 => 1,   // Thìn -> Tý
            6 => 12,  // Tỵ -> Hợi
            7 => 11,  // Ngọ -> Tuất
            8 => 10,  // Mùi -> Dậu
            10 => 8,  // Dậu -> Mùi
            11 => 7,  // Tuất -> Ngọ
            12 => 6,  // Hợi -> Tỵ
            _ => pos
        };
    }
}
