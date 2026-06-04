using Microsoft.EntityFrameworkCore;
using SatelliteEmergencyHub.Domain.Entities;
using SatelliteEmergencyHub.Infrastructure.Data;
using SatelliteEmergencyHub.Infrastructure.Repositories.Interfaces;

namespace SatelliteEmergencyHub.Infrastructure.Repositories.Implementations
{
    public class RegionRepository : Repository<Region>, IRegionRepository
    {
        public RegionRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Region>> GetAllWithDetailsAsync()
            => await _context.Regions
                .AsNoTracking()
                .ToListAsync();

        public async Task<Region?> GetWithDetailsAsync(int id)
            => await _context.Regions
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

        public async Task<bool> NameExistsAsync(string name, int? excludeId = null)
            => await _context.Regions
                .AnyAsync(r => r.Name == name && (!excludeId.HasValue || r.Id !=  excludeId.Value));
    }
}
