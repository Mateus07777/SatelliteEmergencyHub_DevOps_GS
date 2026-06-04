using SatelliteEmergencyHub.Domain.Enums;
using SatelliteEmergencyHub.Domain.Enums.SatelliteEmergencyHub.Domain.Enums;

namespace SatelliteEmergencyHub.Application.DTOs.Response
{
    public class SensorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public SensorType Type { get; set; }
        public SensorStatus Status { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
