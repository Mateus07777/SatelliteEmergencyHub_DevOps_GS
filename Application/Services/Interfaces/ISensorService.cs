using SatelliteEmergencyHub.Application.DTOs.Request;
using SatelliteEmergencyHub.Application.DTOs.Response;

namespace SatelliteEmergencyHub.Application.Services.Interfaces
{
    public interface ISensorService
    {
        Task<IEnumerable<SensorResponse>> GetAllAsync();
        Task<IEnumerable<SensorResponse>> GetByRegionAsync(int regionId);
        Task<SensorResponse> GetByIdAsync(int id);
        Task<SensorResponse> CreateAsync(CreateSensorRequest request);
        Task<SensorResponse> UpdateAsync(int id, UpdateSensorRequest request);
        Task DeleteAsync(int id);
    }
}
