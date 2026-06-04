using SatelliteEmergencyHub.Domain.Entities;

namespace SatelliteEmergencyHub.Infrastructure.Repositories.Interfaces
{
    public interface ISensorRepository : IRepository<Sensor>
    {
        Task<IEnumerable<Sensor>> GetAllWithRegionAsync();
        Task<IEnumerable<Sensor>> GetByRegionIdAsync(int regionId);
        Task<Sensor?> GetByIdWithRegionAsync(int id);
    }
}
