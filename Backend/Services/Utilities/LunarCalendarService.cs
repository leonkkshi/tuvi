namespace Backend.Services.Utilities;

using System.Diagnostics;
using System.Text.Json;

/// <summary>
/// Service chuyển đổi giữa âm lịch và dương lịch
/// Sử dụng vietnamese-lunar-calendar npm package cho độ chính xác cao
/// </summary>
public class LunarCalendarService
{
    public int[] ConvertSolar2Lunar(int day, int month, int year)
    {
        try
        {
            // Call Node.js script to convert using vietnamese-lunar-calendar library
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "node",
                    Arguments = $"convert-lunar.js {day} {month} {year}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception($"Node.js error: {error}");
            }

            // Parse JSON response
            var result = JsonSerializer.Deserialize<JsonElement>(output);
            int lunarDay = result.GetProperty("lunarDay").GetInt32();
            int lunarMonth = result.GetProperty("lunarMonth").GetInt32();
            int lunarYear = result.GetProperty("lunarYear").GetInt32();
            int leapMonth = result.GetProperty("leapMonth").GetInt32();

            return new[] { lunarDay, lunarMonth, lunarYear, leapMonth };
        }
        catch (Exception ex)
        {
            // Fallback: log error and return default
            Console.WriteLine($"Lunar conversion error: {ex.Message}");
            return new[] { day, month, year, 0 };
        }
    }
}
