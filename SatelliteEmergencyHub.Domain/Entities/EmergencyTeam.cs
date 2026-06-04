using SatelliteEmergencyHub.Domain.Enums;

namespace SatelliteEmergencyHub.Domain.Entities
{
    public class EmergencyTeam : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public TeamStatus Status { get; set; } = TeamStatus.Available;

        // Navegação (N:N para Occurrence via pivot)
        public ICollection<EmergencyTeamOccurrence> EmergencyTeamOccurrences { get; set; } = new List<EmergencyTeamOccurrence>();
    }
}
