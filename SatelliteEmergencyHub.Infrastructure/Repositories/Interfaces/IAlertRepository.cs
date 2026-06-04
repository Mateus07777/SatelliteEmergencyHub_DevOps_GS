using SatelliteEmergencyHub.Domain.Entities;

namespace SatelliteEmergencyHub.Infrastructure.Repositories.Interfaces
{
    public interface IAlertRepository : IRepository<Alert>
    {
        Task<IEnumerable<Alert>> GetAllWithOccurrenceAsync();
        Task<IEnumerable<Alert>> GetByOccurrenceIdAsync(int occurrenceId);
        Task<Alert?> GetByIdWithOccurrenceAsync(int id);
    }
}
