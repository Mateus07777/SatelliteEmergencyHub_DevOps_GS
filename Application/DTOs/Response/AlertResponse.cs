using SatelliteEmergencyHub.Domain.Enums;


namespace SatelliteEmergencyHub.Application.DTOs.Response
{
    public class AlertResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public AlertLevel Level { get; set; }
        public AlertStatus Status { get; set; }
        public int OccurrenceId { get; set; }
        public string OccurrenceTitle { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
