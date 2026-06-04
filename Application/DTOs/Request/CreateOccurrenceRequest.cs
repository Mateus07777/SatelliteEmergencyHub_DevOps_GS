using SatelliteEmergencyHub.Domain.Enums;


namespace SatelliteEmergencyHub.Application.DTOs.Request
{
    public class CreateOccurrenceRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public OccurrenceType Type { get; set; }
        public OccurrenceSeverity Severity { get; set; }
        public OccurrenceStatus Status { get; set; } = OccurrenceStatus.Active;
        public int RegionId { get; set; }
    }
}
