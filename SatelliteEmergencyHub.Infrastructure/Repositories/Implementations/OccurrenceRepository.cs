using Microsoft.EntityFrameworkCore;
using SatelliteEmergencyHub.Domain.Entities;
using SatelliteEmergencyHub.Infrastructure.Data;
using SatelliteEmergencyHub.Infrastructure.Repositories.Interfaces;

namespace SatelliteEmergencyHub.Infrastructure.Repositories.Implementations
{
    public class OccurrenceRepository : Repository<Occurrence>, IOccurrenceRepository
    {
        public OccurrenceRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Occurrence>> GetAllWithRegionAsync() =>
            await _context.Occurrences.Include(o => o.Region).ToListAsync();

        public async Task<IEnumerable<Occurrence>> GetByRegionIdAsync(int regionId) =>
            await _context.Occurrences.Include(o => o.Region)
                .Where(o => o.RegionId == regionId).ToListAsync();

        public async Task<Occurrence?> GetByIdWithRegionAsync(int id) =>
            await _context.Occurrences.Include(o => o.Region)
                .FirstOrDefaultAsync(o => o.Id == id);
    }
}
