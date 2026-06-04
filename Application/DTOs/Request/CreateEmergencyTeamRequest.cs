using SatelliteEmergencyHub.Domain.Enums;

namespace SatelliteEmergencyHub.Application.DTOs.Request
{
    public class CreateEmergencyTeamRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public TeamStatus Status { get; set; } = TeamStatus.Available;
    }
}
