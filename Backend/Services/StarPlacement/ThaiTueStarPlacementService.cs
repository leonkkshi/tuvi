namespace Backend.Services.StarPlacement;

/// <summary>
/// Service an vòng Thái Tuế (12 sao + 5 sao đi cùng + 4 sao phụ)
/// </summary>
public class ThaiTueStarPlacementService : IStarPlacementService
{
    public Dictionary<int, List<int>> PlaceStars(StarPlacementContext context)
    {
        var positions = new Dictionary<int, List<int>>();
        for (int i = 1; i <= 12; i++) positions[i] = new List<int>();

        // Tính Chi năm
        int yearBranch = ((context.Year - 3) % 12);
        if (yearBranch <= 0) yearBranch += 12;

        // 12 sao vòng Thái Tuế: 54-65
        int[] thaiTueStars = { 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65 };

        // An 12 sao theo chi năm, thuận chiều
        for (int i = 0; i < 12; i++)
        {
            int palacePos = yearBranch + i;
            if (palacePos > 12) palacePos -= 12;
            
            positions[palacePos].Add(thaiTueStars[i]);
        }

        // Các sao đi cùng vòng Thái Tuế
        
        // Thiên Không(66): cùng Thiếu Dương
        int thieuDuongPos = yearBranch + 1;
        if (thieuDuongPos > 12) thieuDuongPos -= 12;
        positions[thieuDuongPos].Add(66);

        // Long Trì(67): cùng Quan Phù
        int quanPhuPos = yearBranch + 4;
        if (quanPhuPos > 12) quanPhuPos -= 12;
        positions[quanPhuPos].Add(67);

        // Nguyệt Đức(68): cùng Tử Phù
        int tuPhuPos = yearBranch + 5;
        if (tuPhuPos > 12) tuPhuPos -= 12;
        positions[tuPhuPos].Add(68);

        // Thiên Hư(69): cùng Tuế Phá
        int tuePhaPos = yearBranch + 6;
        if (tuePhaPos > 12) tuePhaPos -= 12;
        positions[tuePhaPos].Add(69);

        // Thiên Đức(70): cùng Phúc Đức
        int phucDucPos = yearBranch + 9;
        if (phucDucPos > 12) phucDucPos -= 12;
        positions[phucDucPos].Add(70);

        // Thiên Khốc(71): Ngọ là Tý đi ngược
        int thienKhocPos = 7 - (yearBranch - 1);
        if (thienKhocPos <= 0) thienKhocPos += 12;
        positions[thienKhocPos].Add(71);

        // Hoa Cái(72), Đào Hoa(73), Kiếp Sát(74) theo Tam Hội
        int hoaCaiPos = 0, daoHoaPos = 0, kiepSatPos = 0;
        
        if (yearBranch == 3 || yearBranch == 7 || yearBranch == 11) // Dần-Ngọ-Tuất
        {
            hoaCaiPos = 11; daoHoaPos = 4; kiepSatPos = 12;
        }
        else if (yearBranch == 9 || yearBranch == 1 || yearBranch == 5) // Thân-Tý-Thìn
        {
            hoaCaiPos = 5; daoHoaPos = 10; kiepSatPos = 6;
        }
        else if (yearBranch == 6 || yearBranch == 10 || yearBranch == 2) // Tỵ-Dậu-Sửu
        {
            hoaCaiPos = 2; daoHoaPos = 7; kiepSatPos = 3;
        }
        else // Hợi-Mão-Mùi
        {
            hoaCaiPos = 8; daoHoaPos = 1; kiepSatPos = 9;
        }
        
        positions[hoaCaiPos].Add(72);
        positions[daoHoaPos].Add(73);
        positions[kiepSatPos].Add(74);

        return positions;
    }
}
