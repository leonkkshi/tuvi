namespace Backend.Models
{
    // 12 Cung trong Tử Vi Đẩu Số
    public class Palace
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Mệnh, Phụ Mẫu, Phúc Đức, etc.
        public string Description { get; set; } = string.Empty;
        public int Position { get; set; } // Vị trí cung (1-12)
    }
}
