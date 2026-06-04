using SatelliteEmergencyHub.Domain.Enums;
using SatelliteEmergencyHub.Domain.Enums.SatelliteEmergencyHub.Domain.Enums;

namespace SatelliteEmergencyHub.Application.DTOs.Request
{
    public class CreateSensorRequest
    {
        public string Name { get; set; } = string.Empty;
        public SensorType Type { get; set; }
        public SensorStatus Status { get; set; } = SensorStatus.Active;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int RegionId { get; set; }
    }
}
