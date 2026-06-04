using SatelliteEmergencyHub.Application.DTOs.Request;
using SatelliteEmergencyHub.Application.DTOs.Response;
using SatelliteEmergencyHub.Application.Services.Interfaces;
using SatelliteEmergencyHub.Domain.Entities;
using SatelliteEmergencyHub.Infrastructure.Repositories.Interfaces;

namespace SatelliteEmergencyHub.Application.Services.Implementations
{
    public class EmergencyTeamService : IEmergencyTeamService
    {
        private readonly IEmergencyTeamRepository _teamRepository;
        private readonly IOccurrenceRepository _occurrenceRepository;

        public EmergencyTeamService(IEmergencyTeamRepository teamRepository, IOccurrenceRepository occurrenceRepository)
        {
            _teamRepository = teamRepository;
            _occurrenceRepository = occurrenceRepository;
        }

        public async Task<IEnumerable<EmergencyTeamResponse>> GetAllAsync()
        {
            var teams = await _teamRepository.GetAllAsync();
            return teams.Select(MapToResponse);
        }

        public async Task<EmergencyTeamResponse> GetByIdAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Emergency team with id {id} not found.");
            return MapToResponse(team);
        }

        public async Task<EmergencyTeamResponse> CreateAsync(CreateEmergencyTeamRequest request)
        {
            var team = new EmergencyTeam
            {
                Name = request.Name,
                Specialization = request.Specialization,
                ContactPhone = request.ContactPhone,
                Status = request.Status
            };

            await _teamRepository.CreateAsync(team);

            return MapToResponse(team);
        }

        public async Task<EmergencyTeamResponse> UpdateAsync(int id, UpdateEmergencyTeamRequest request)
        {
            var team = await _teamRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Emergency team with id {id} not found.");

            team.Name = request.Name;
            team.Specialization = request.Specialization;
            team.ContactPhone = request.ContactPhone;
            team.Status = request.Status;
            team.UpdatedAt = DateTime.UtcNow;

            _teamRepository.UpdateAsync(team);

            return MapToResponse(team);
        }

        public async Task DeleteAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Emergency team with id {id} not found.");

            _teamRepository.DeleteAsync(team);
        }

        public async Task AssignToOccurrenceAsync(int teamId, AssignTeamToOccurrenceRequest request)
        {
            var team = await _teamRepository.GetByIdAsync(teamId)
                ?? throw new NotFoundException($"Emergency team with id {teamId} not found.");

            var occurrence = await _occurrenceRepository.GetByIdAsync(request.OccurrenceId)
                ?? throw new NotFoundException($"Occurrence with id {request.OccurrenceId} not found.");

            var alreadyAssigned = await _teamRepository.IsAssignedToOccurrenceAsync(teamId, request.OccurrenceId);
            if (alreadyAssigned)
                throw new InvalidOperationException("Team is already assigned to this occurrence.");

            var pivot = new EmergencyTeamOccurrence
            {
                EmergencyTeamId = teamId,
                OccurrenceId = request.OccurrenceId,
                AssignedAt = DateTime.UtcNow,
                Notes = request.Notes
            };

            await _teamRepository.AssignToOccurrenceAsync(pivot);
        }

        public async Task UnassignFromOccurrenceAsync(int teamId, int occurrenceId)
        {
            var pivot = await _teamRepository.GetAssignmentAsync(teamId, occurrenceId)
                ?? throw new NotFoundException("Assignment not found.");

            _teamRepository.RemoveAssignment(pivot);
        }

        private static EmergencyTeamResponse MapToResponse(EmergencyTeam t) => new()
        {
            Id = t.Id,
            Name = t.Name,
            Specialization = t.Specialization,
            ContactPhone = t.ContactPhone,
            Status = t.Status,
            CreatedAt = t.CreatedAt
        };
    }
}
