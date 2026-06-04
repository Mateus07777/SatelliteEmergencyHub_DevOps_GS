using SatelliteEmergencyHub.Domain.Enums;
using SatelliteEmergencyHub.Domain.Enums.SatelliteEmergencyHub.Domain.Enums;

namespace SatelliteEmergencyHub.Domain.Entities
{
    public class Sensor : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public SensorType Type { get; set; }
        public SensorStatus Status { get; set; } = SensorStatus.Active;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // FK para Region (1:N)
        public int RegionId { get; set; }
        public Region Region { get; set; } = null!;
    }
}
