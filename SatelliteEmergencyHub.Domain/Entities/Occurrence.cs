using SatelliteEmergencyHub.Domain.Enums;

namespace SatelliteEmergencyHub.Domain.Entities
{
    public class Occurrence : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public OccurrenceType Type { get; set; }
        public OccurrenceSeverity Severity { get; set; }
        public OccurrenceStatus Status { get; set; } = OccurrenceStatus.Active;

        // FK para Region (1:N)
        public int RegionId { get; set; }
        public Region Region { get; set; } = null!;

        // Navegação (1:N para Alert)
        public ICollection<Alert> Alerts { get; set; } = new List<Alert>();

        // Navegação (N:N para EmergencyTeam via pivot)
        public ICollection<EmergencyTeamOccurrence> EmergencyTeamOccurrences { get; set; } = new List<EmergencyTeamOccurrence>();
    }
}
