using SatelliteEmergencyHub.Domain.Entities;

namespace SatelliteEmergencyHub.Infrastructure.Repositories.Interfaces
{
    public interface IOccurrenceRepository : IRepository<Occurrence>
    {
        Task<IEnumerable<Occurrence>> GetAllWithRegionAsync();
        Task<IEnumerable<Occurrence>> GetByRegionIdAsync(int regionId);
        Task<Occurrence?> GetByIdWithRegionAsync(int id);
    }
}
