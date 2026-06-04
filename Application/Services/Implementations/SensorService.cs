using SatelliteEmergencyHub.Application.DTOs.Request;
using SatelliteEmergencyHub.Application.DTOs.Response;
using SatelliteEmergencyHub.Application.Services.Interfaces;
using SatelliteEmergencyHub.Domain.Entities;
using SatelliteEmergencyHub.Infrastructure.Repositories.Interfaces;

namespace SatelliteEmergencyHub.Application.Services.Implementations
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;
        private readonly IRegionRepository _regionRepository;

        public SensorService(ISensorRepository sensorRepository, IRegionRepository regionRepository)
        {
            _sensorRepository = sensorRepository;
            _regionRepository = regionRepository;
        }

        public async Task<IEnumerable<SensorResponse>> GetAllAsync()
        {
            var sensors = await _sensorRepository.GetAllWithRegionAsync();
            return sensors.Select(MapToResponse);
        }

        public async Task<IEnumerable<SensorResponse>> GetByRegionAsync(int regionId)
        {
            var sensors = await _sensorRepository.GetByRegionIdAsync(regionId);
            return sensors.Select(MapToResponse);
        }

        public async Task<SensorResponse> GetByIdAsync(int id)
        {
            var sensor = await _sensorRepository.GetByIdWithRegionAsync(id)
                ?? throw new NotFoundException($"Sensor with id {id} not found.");
            return MapToResponse(sensor);
        }

        public async Task<SensorResponse> CreateAsync(CreateSensorRequest request)
        {
            var regionExists = await _regionRepository.GetByIdAsync(request.RegionId);
            if (regionExists is null)
                throw new NotFoundException($"Region with id {request.RegionId} not found.");

            var sensor = new Sensor
            {
                Name = request.Name,
                Type = request.Type,
                Status = request.Status,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                RegionId = request.RegionId
            };

            await _sensorRepository.CreateAsync(sensor);

            return await GetByIdAsync(sensor.Id);
        }

        public async Task<SensorResponse> UpdateAsync(int id, UpdateSensorRequest request)
        {
            var sensor = await _sensorRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Sensor with id {id} not found.");

            sensor.Name = request.Name;
            sensor.Type = request.Type;
            sensor.Status = request.Status;
            sensor.Latitude = request.Latitude;
            sensor.Longitude = request.Longitude;
            sensor.UpdatedAt = DateTime.UtcNow;

            _sensorRepository.UpdateAsync(sensor);

            return await GetByIdAsync(sensor.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var sensor = await _sensorRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Sensor with id {id} not found.");

            _sensorRepository.DeleteAsync(sensor);
        }

        private static SensorResponse MapToResponse(Sensor s) => new()
        {
            Id = s.Id,
            Name = s.Name,
            Type = s.Type,
            Status = s.Status,
            Latitude = s.Latitude,
            Longitude = s.Longitude,
            RegionId = s.RegionId,
            RegionName = s.Region?.Name ?? string.Empty,
            CreatedAt = s.CreatedAt,
        };
    }
}
