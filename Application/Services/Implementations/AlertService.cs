using SatelliteEmergencyHub.Application.DTOs.Request;
using SatelliteEmergencyHub.Application.DTOs.Response;
using SatelliteEmergencyHub.Application.Services.Interfaces;
using SatelliteEmergencyHub.Domain.Entities;
using SatelliteEmergencyHub.Infrastructure.Repositories.Interfaces;

namespace SatelliteEmergencyHub.Application.Services.Implementations
{
    public class AlertService : IAlertService
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IOccurrenceRepository _occurrenceRepository;

        public AlertService(IAlertRepository alertRepository, IOccurrenceRepository occurrenceRepository)
        {
            _alertRepository = alertRepository;
            _occurrenceRepository = occurrenceRepository;
        }

        public async Task<IEnumerable<AlertResponse>> GetAllAsync()
        {
            var alerts = await _alertRepository.GetAllWithOccurrenceAsync();
            return alerts.Select(MapToResponse);
        }

        public async Task<IEnumerable<AlertResponse>> GetByOccurrenceAsync(int occurrenceId)
        {
            var alerts = await _alertRepository.GetByOccurrenceIdAsync(occurrenceId);
            return alerts.Select(MapToResponse);
        }

        public async Task<AlertResponse> GetByIdAsync(int id)
        {
            var alert = await _alertRepository.GetByIdWithOccurrenceAsync(id)
                ?? throw new NotFoundException($"Alert with id {id} not found.");
            return MapToResponse(alert);
        }

        public async Task<AlertResponse> CreateAsync(CreateAlertRequest request)
        {
            var occurrence = await _occurrenceRepository.GetByIdAsync(request.OccurrenceId)
                ?? throw new NotFoundException($"Occurrence with id {request.OccurrenceId} not found.");

            var alert = new Alert
            {
                Title = request.Title,
                Message = request.Message,
                Level = request.Level,
                Status = Domain.Enums.AlertStatus.Sent,
                OccurrenceId = request.OccurrenceId
            };

            await _alertRepository.CreateAsync(alert);

            return await GetByIdAsync(alert.Id);
        }

        public async Task<AlertResponse> UpdateAsync(int id, UpdateAlertRequest request)
        {
            var alert = await _alertRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Alert with id {id} not found.");

            alert.Title = request.Title;
            alert.Message = request.Message;
            alert.Level = request.Level;
            alert.Status = request.Status;
            alert.UpdatedAt = DateTime.UtcNow;

            _alertRepository.UpdateAsync(alert);

            return await GetByIdAsync(alert.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var alert = await _alertRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Alert with id {id} not found.");

            _alertRepository.DeleteAsync(alert);
        }

        private static AlertResponse MapToResponse(Alert a) => new()
        {
            Id = a.Id,
            Title = a.Title,
            Message = a.Message,
            Level = a.Level,
            Status = a.Status,
            OccurrenceId = a.OccurrenceId,
            OccurrenceTitle = a.Occurrence?.Title ?? string.Empty,
            CreatedAt = a.CreatedAt
        };
    }
}
