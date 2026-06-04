using SatelliteEmergencyHub.Domain.Entities;

namespace SatelliteEmergencyHub.Infrastructure.Repositories.Interfaces
{
    public interface IEmergencyTeamRepository : IRepository<EmergencyTeam>
    {
        Task<bool> IsAssignedToOccurrenceAsync(int teamId, int occurrenceId);
        Task AssignToOccurrenceAsync(EmergencyTeamOccurrence pivot);
        Task<EmergencyTeamOccurrence?> GetAssignmentAsync(int teamId, int occurrenceId);
        void RemoveAssignment(EmergencyTeamOccurrence pivot);
    }
}
