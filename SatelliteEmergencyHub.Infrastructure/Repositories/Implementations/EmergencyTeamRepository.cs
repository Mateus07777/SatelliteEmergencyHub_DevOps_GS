using Microsoft.EntityFrameworkCore;
using SatelliteEmergencyHub.Domain.Entities;
using SatelliteEmergencyHub.Infrastructure.Data;
using SatelliteEmergencyHub.Infrastructure.Repositories.Interfaces;

namespace SatelliteEmergencyHub.Infrastructure.Repositories.Implementations
{
    public class EmergencyTeamRepository : Repository<EmergencyTeam>, IEmergencyTeamRepository
    {
        public EmergencyTeamRepository(AppDbContext context) : base(context) { }

        public async Task<bool> IsAssignedToOccurrenceAsync(int teamId, int occurrenceId) =>
            await _context.EmergencyTeamOccurrences
                .AnyAsync(x => x.EmergencyTeamId == teamId && x.OccurrenceId == occurrenceId);


        public async Task AssignToOccurrenceAsync(EmergencyTeamOccurrence pivot) =>
            await _context.EmergencyTeamOccurrences.AddAsync(pivot);

        public async Task<EmergencyTeamOccurrence?> GetAssignmentAsync(int teamId, int occurrenceId) =>
            await _context.EmergencyTeamOccurrences
                .FirstOrDefaultAsync(x => x.EmergencyTeamId == teamId && x.OccurrenceId == occurrenceId);

        public void RemoveAssignment(EmergencyTeamOccurrence pivot) =>
            _context.EmergencyTeamOccurrences.Remove(pivot);
    }
}
