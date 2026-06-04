using SatelliteEmergencyHub.Application.DTOs.Request;
using SatelliteEmergencyHub.Application.DTOs.Response;

namespace SatelliteEmergencyHub.Application.Services.Interfaces
{
    public interface IOccurrenceService
    {
        Task<IEnumerable<OccurrenceResponse>> GetAllAsync();
        Task<IEnumerable<OccurrenceResponse>> GetByRegionAsync(int regionId);
        Task<OccurrenceResponse> GetByIdAsync(int id);
        Task<OccurrenceResponse> CreateAsync(CreateOccurrenceRequest request);
        Task<OccurrenceResponse> UpdateAsync(int id, UpdateOccurrenceRequest request);
        Task DeleteAsync(int id);
    }
}
