using SatelliteEmergencyHub.Domain.Entities;

namespace SatelliteEmergencyHub.Infrastructure.Repositories.Interfaces
{
    public interface IRegionRepository : IRepository<Region>
    {
        Task<IEnumerable<Region>> GetAllWithDetailsAsync();
        Task<Region?> GetWithDetailsAsync(int id);
        Task<bool> NameExistsAsync(string name, int? excludeId = null);
    }
}
