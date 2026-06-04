namespace SatelliteEmergencyHub.Domain.Entities
{
    public class EmergencyTeamOccurrence
    {
        public int EmergencyTeamId { get; set; }
        public EmergencyTeam EmergencyTeam { get; set; } = null!;

        public int OccurrenceId { get; set; }
        public Occurrence Occurrence { get; set; } = null!;

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }
    }
}
