using SatelliteEmergencyHub.Domain.Enums;

namespace SatelliteEmergencyHub.Application.DTOs.Request
{
    public class CreateAlertRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public AlertLevel Level { get; set; }
        public int OccurrenceId { get; set; }
    }
}
