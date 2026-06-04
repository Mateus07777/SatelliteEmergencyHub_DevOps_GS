using SatelliteEmergencyHub.Application.DTOs.Request;
using SatelliteEmergencyHub.Application.DTOs.Response;

namespace SatelliteEmergencyHub.Application.Services.Interfaces
{
    public interface IAlertService
    {
        Task<IEnumerable<AlertResponse>> GetAllAsync();
        Task<IEnumerable<AlertResponse>> GetByOccurrenceAsync(int occurrenceId);
        Task<AlertResponse> GetByIdAsync(int id);
        Task<AlertResponse> CreateAsync(CreateAlertRequest request);
        Task<AlertResponse> UpdateAsync(int id, UpdateAlertRequest request);
        Task DeleteAsync(int id);
    }
}
