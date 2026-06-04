using SatelliteEmergencyHub.Domain.Enums;

namespace SatelliteEmergencyHub.Application.DTOs.Response
{
    public class EmergencyTeamResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public TeamStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
