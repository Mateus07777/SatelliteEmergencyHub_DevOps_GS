using SatelliteEmergencyHub.Domain.Enums;

namespace SatelliteEmergencyHub.Application.DTOs.Response
{
    public class OccurrenceResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public OccurrenceType Type { get; set; }
        public OccurrenceSeverity Severity { get; set; }
        public OccurrenceStatus Status { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
