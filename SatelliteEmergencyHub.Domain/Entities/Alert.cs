using SatelliteEmergencyHub.Domain.Enums;

namespace SatelliteEmergencyHub.Domain.Entities
{
    public class Alert : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public AlertLevel Level { get; set; }
        public AlertStatus Status { get; set; } = AlertStatus.Sent;

        // FK para Occurrence (1:N)
        public int OccurrenceId { get; set; }
        public Occurrence Occurrence { get; set; } = null!;
    }
}
