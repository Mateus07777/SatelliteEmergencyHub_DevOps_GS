using Microsoft.EntityFrameworkCore;
using SatelliteEmergencyHub.Domain.Entities;
using SatelliteEmergencyHub.Infrastructure.Data;
using SatelliteEmergencyHub.Infrastructure.Repositories.Interfaces;

namespace SatelliteEmergencyHub.Infrastructure.Repositories.Implementations
{
    public class SensorRepository : Repository<Sensor>, ISensorRepository
    {
        public SensorRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Sensor>> GetAllWithRegionAsync() =>
            await _context.Sensors.Include(s => s.Region).ToListAsync();

        public async Task<IEnumerable<Sensor>> GetByRegionIdAsync(int regionId) =>
            await _context.Sensors.Include(s => s.Region)
                .Where(s => s.RegionId == regionId).ToListAsync();

        public async Task<Sensor?> GetByIdWithRegionAsync(int id) =>
            await _context.Sensors.Include(s => s.Region)
                .FirstOrDefaultAsync(s => s.Id == id);
    }
}
