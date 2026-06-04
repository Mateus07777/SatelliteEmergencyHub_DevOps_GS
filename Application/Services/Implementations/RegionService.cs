using SatelliteEmergencyHub.Application.DTOs.Request;
using SatelliteEmergencyHub.Application.DTOs.Response;
using SatelliteEmergencyHub.Application.Services.Interfaces;
using SatelliteEmergencyHub.Domain.Entities;
using SatelliteEmergencyHub.Infrastructure.Repositories.Interfaces;

namespace SatelliteEmergencyHub.Application.Services.Implementations
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _repository;

        public RegionService(IRegionRepository repository) 
        { 
            _repository = repository;
        }
      
        public async Task<IEnumerable<RegionResponse>> GetAllAsync()
        {
            var regions = await _repository.GetAllWithDetailsAsync();
            return regions.Select(MapToResponse);
        }
      
        public async Task<RegionResponse> GetByIdAsync(int id)
        {
            var region = await _repository.GetWithDetailsAsync(id)
                ?? throw new NotFoundException("Region", id);

            return MapToResponse(region);
        }

        public async Task<RegionResponse> CreateAsync(CreateRegionRequest request)
        {
            if (await _repository.NameExistsAsync(request.Name))
                throw new ConflictException($"Region '{request.Name}' already exists.");

            var region = new Region
            {
                Name = request.Name,
                Country = request.Country,
                State = request.State,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                RadiusKm = request.RadiusKm
            };

            var created = await _repository.CreateAsync(region);
            return MapToResponse(created);
        }

        public async Task<RegionResponse> UpdateAsync(int id, UpdateRegionRequest request)
        {
            var region = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Region", id);

            if (await _repository.NameExistsAsync(request.Name, excludeId: id))
                throw new ConflictException($"Region '{request.Name}' already exists.");

            region.Name = request.Name;
            region.Country = request.Country;
            region.State = request.State;
            region.Latitude = request.Latitude;
            region.Longitude = request.Longitude;
            region.RadiusKm = request.RadiusKm;
            region.IsActive = request.IsActive;

            var updated = await _repository.UpdateAsync(region);
            return MapToResponse(updated);
        }


        public async Task DeleteAsync(int id)
        {
            var region = await _repository.GetByIdAsync(id)
                        ?? throw new NotFoundException("Region", id);

            await _repository.DeleteAsync(region);
        }




        private static RegionResponse MapToResponse(Region r) => new()
        {
            Id = r.Id,
            Name = r.Name,
            Country = r.Country,
            State = r.State,
            Latitude = r.Latitude,
            Longitude = r.Longitude,
            RadiusKm = r.RadiusKm,
            IsActive = r.IsActive,
            CreatedAt = r.CreatedAt,
        };
    }
}
