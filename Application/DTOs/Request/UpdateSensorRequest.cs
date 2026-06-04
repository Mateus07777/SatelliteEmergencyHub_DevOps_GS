using SatelliteEmergencyHub.Domain.Enums;
using SatelliteEmergencyHub.Domain.Enums.SatelliteEmergencyHub.Domain.Enums;

namespace SatelliteEmergencyHub.Application.DTOs.Request
{
    public class UpdateSensorRequest
    {
        public string Name { get; set; } = string.Empty;
        public SensorType Type { get; set; }
        public SensorStatus Status { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
