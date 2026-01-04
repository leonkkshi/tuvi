namespace Backend.Services.StarPlacement;

/// <summary>
/// Service an 12 sao Trường Sinh
/// </summary>
public class TruongSinhStarPlacementService : IStarPlacementService
{
    public Dictionary<int, List<int>> PlaceStars(StarPlacementContext context)
    {
        var positions = new Dictionary<int, List<int>>();
        for (int i = 1; i <= 12; i++) positions[i] = new List<int>();

        // 12 sao Trường Sinh: Trường Sinh(42) -> Dưỡng(53)
        int[] truongSinhStars = { 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53 };

        // Cung khởi Trường Sinh theo Ngũ Hành Cục
        int startPalace = context.NguHanhCuc switch
        {
            2 => 9,  // Thủy Nhị Cục - Trường Sinh tại Thân
            3 => 12, // Mộc Tam Cục - Trường Sinh tại Hợi
            4 => 6,  // Kim Tứ Cục - Trường Sinh tại Tỵ
            5 => 9,  // Thổ Ngũ Cục - Trường Sinh tại Thân
            6 => 3,  // Hỏa Lục Cục - Trường Sinh tại Dần
            _ => 9
        };

        // Dương Nam và Âm Nữ thuận, Âm Nam và Dương Nữ nghịch
        bool thuanChieu = context.AmDuong == "Dương Nam" || context.AmDuong == "Âm Nữ";

        for (int i = 0; i < 12; i++)
        {
            int palacePos;
            if (thuanChieu)
            {
                palacePos = startPalace + i;
                if (palacePos > 12) palacePos -= 12;
            }
            else
            {
                palacePos = startPalace - i;
                if (palacePos <= 0) palacePos += 12;
            }

            positions[palacePos].Add(truongSinhStars[i]);
        }

        return positions;
    }
}
