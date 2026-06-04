namespace SatelliteEmergencyHub.Application.DTOs.Request
{
    public class AssignTeamToOccurrenceRequest
    {
        public int OccurrenceId { get; set; }
        public string? Notes { get; set; }
    }
}
