using Backend.Models;

namespace Backend.Services
{
    public interface IAIInterpretationService
    {
        Task<InterpretationResponse> InterpretChartAsync(InterpretationRequest request);
    }
}
