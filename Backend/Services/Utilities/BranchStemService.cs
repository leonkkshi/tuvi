namespace Backend.Services.Utilities;

/// <summary>
/// Service xử lý Can Chi (Thiên Can và Địa Chi)
/// </summary>
public class BranchStemService
{
    private static readonly string[] Cans = { "Giáp", "Ất", "Bính", "Đinh", "Mậu", "Kỷ", "Canh", "Tân", "Nhâm", "Quý" };
    private static readonly string[] Branches = { "Tý", "Sửu", "Dần", "Mão", "Thìn", "Tỵ", "Ngọ", "Mùi", "Thân", "Dậu", "Tuất", "Hợi" };

    /// <summary>
    /// Lấy Thiên Can từ năm (1-10)
    /// </summary>
    public int GetYearCan(int year)
    {
        int yearCan = (year - 3) % 10;
        if (yearCan <= 0) yearCan += 10;
        return yearCan;
    }

    /// <summary>
    /// Lấy Địa Chi từ năm (1-12)
    /// </summary>
    public int GetYearBranch(int year)
    {
        int yearBranch = (year - 3) % 12;
        if (yearBranch <= 0) yearBranch += 12;
        return yearBranch;
    }

    /// <summary>
    /// Xây dựng năm Can Chi (ví dụ: "Giáp Tý")
    /// </summary>
    public string BuildCanChiYear(int lunarYear)
    {
        int can = GetYearCan(lunarYear);
        int branch = GetYearBranch(lunarYear);
        return $"{Cans[can - 1]} {Branches[branch - 1]}";
    }

    /// <summary>
    /// Lấy tên Can từ số (1-10)
    /// </summary>
    public string GetCanName(int can)
    {
        if (can < 1 || can > 10) return "";
        return Cans[can - 1];
    }

    /// <summary>
    /// Lấy tên Chi từ số (1-12)
    /// </summary>
    public string GetBranchName(int branch)
    {
        if (branch < 1 || branch > 12) return "";
        return Branches[branch - 1];
    }
}
