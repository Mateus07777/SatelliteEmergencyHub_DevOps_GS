using SatelliteEmergencyHub.Application.DTOs.Request;
using SatelliteEmergencyHub.Application.DTOs.Response;
using SatelliteEmergencyHub.Application.Services.Interfaces;
using SatelliteEmergencyHub.Domain.Entities;
using SatelliteEmergencyHub.Infrastructure.Repositories.Interfaces;

namespace SatelliteEmergencyHub.Application.Services.Implementations
{
    public class OccurrenceService : IOccurrenceService
    {
        private readonly IOccurrenceRepository _occurrenceRepository;
        private readonly IRegionRepository _regionRepository;

        public OccurrenceService(IOccurrenceRepository occurrenceRepository, IRegionRepository regionRepository)
        {
            _occurrenceRepository = occurrenceRepository;
            _regionRepository = regionRepository;
        }

        public async Task<IEnumerable<OccurrenceResponse>> GetAllAsync()
        {
            var occurrences = await _occurrenceRepository.GetAllWithRegionAsync();
            return occurrences.Select(MapToResponse);
        }

        public async Task<IEnumerable<OccurrenceResponse>> GetByRegionAsync(int regionId)
        {
            var occurrences = await _occurrenceRepository.GetByRegionIdAsync(regionId);
            return occurrences.Select(MapToResponse);
        }

        public async Task<OccurrenceResponse> GetByIdAsync(int id)
        {
            var occurrence = await _occurrenceRepository.GetByIdWithRegionAsync(id)
                ?? throw new NotFoundException($"Occurrence with id {id} not found.");
            return MapToResponse(occurrence);
        }

        public async Task<OccurrenceResponse> CreateAsync(CreateOccurrenceRequest request)
        {
            var region = await _regionRepository.GetByIdAsync(request.RegionId)
                ?? throw new NotFoundException($"Region with id {request.RegionId} not found.");

            var occurrence = new Occurrence
            {
                Title = request.Title,
                Description = request.Description,
                Type = request.Type,
                Severity = request.Severity,
                Status = request.Status,
                RegionId = request.RegionId
            };

            await _occurrenceRepository.CreateAsync(occurrence);

            return await GetByIdAsync(occurrence.Id);
        }

        public async Task<OccurrenceResponse> UpdateAsync(int id, UpdateOccurrenceRequest request)
        {
            var occurrence = await _occurrenceRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Occurrence with id {id} not found.");

            occurrence.Title = request.Title;
            occurrence.Description = request.Description;
            occurrence.Type = request.Type;
            occurrence.Severity = request.Severity;
            occurrence.Status = request.Status;
            occurrence.UpdatedAt = DateTime.UtcNow;

            _occurrenceRepository.UpdateAsync(occurrence);

            return await GetByIdAsync(occurrence.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var occurrence = await _occurrenceRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Occurrence with id {id} not found.");

            _occurrenceRepository.DeleteAsync(occurrence);
        }

        private static OccurrenceResponse MapToResponse(Occurrence o) => new()
        {
            Id = o.Id,
            Title = o.Title,
            Description = o.Description,
            Type = o.Type,
            Severity = o.Severity,
            Status = o.Status,
            RegionId = o.RegionId,
            RegionName = o.Region?.Name ?? string.Empty,
            CreatedAt = o.CreatedAt,
        };
    }
}
