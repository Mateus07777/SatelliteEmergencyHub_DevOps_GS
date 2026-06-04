using SatelliteEmergencyHub.Application.DTOs.Request;
using SatelliteEmergencyHub.Application.DTOs.Response;

namespace SatelliteEmergencyHub.Application.Services.Interfaces
{
    public interface IEmergencyTeamService
    {
        Task<IEnumerable<EmergencyTeamResponse>> GetAllAsync();
        Task<EmergencyTeamResponse> GetByIdAsync(int id);
        Task<EmergencyTeamResponse> CreateAsync(CreateEmergencyTeamRequest request);
        Task<EmergencyTeamResponse> UpdateAsync(int id, UpdateEmergencyTeamRequest request);
        Task DeleteAsync(int id);
        Task AssignToOccurrenceAsync(int teamId, AssignTeamToOccurrenceRequest request);
        Task UnassignFromOccurrenceAsync(int teamId, int occurrenceId);
    }
}
