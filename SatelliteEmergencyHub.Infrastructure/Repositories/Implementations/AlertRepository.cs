using Microsoft.EntityFrameworkCore;
using SatelliteEmergencyHub.Domain.Entities;
using SatelliteEmergencyHub.Infrastructure.Data;
using SatelliteEmergencyHub.Infrastructure.Repositories.Interfaces;

namespace SatelliteEmergencyHub.Infrastructure.Repositories.Implementations
{
    public class AlertRepository : Repository<Alert>, IAlertRepository
    {
        public AlertRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Alert>> GetAllWithOccurrenceAsync() =>
            await _context.Alerts.Include(a => a.Occurrence).ToListAsync();

        public async Task<IEnumerable<Alert>> GetByOccurrenceIdAsync(int occurrenceId) =>
            await _context.Alerts.Include(a => a.Occurrence)
                .Where(a => a.OccurrenceId == occurrenceId).ToListAsync();

        public async Task<Alert?> GetByIdWithOccurrenceAsync(int id) =>
            await _context.Alerts.Include(a => a.Occurrence)
                .FirstOrDefaultAsync(a => a.Id == id);
    }
}
