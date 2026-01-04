using Backend.Models;

namespace Backend.Services
{
    public interface ITuViService
    {
        List<Palace> GetAllPalaces();
        List<Star> GetAllStars();
        TuViChart GenerateChart(ChartRequest request);
        string InterpretPalace(int palaceId, List<StarInPalace> stars);
        int[] TestConvertSolar2Lunar(int day, int month, int year);
    }
}
