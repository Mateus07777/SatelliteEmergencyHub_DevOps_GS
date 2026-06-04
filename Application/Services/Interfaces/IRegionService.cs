using SatelliteEmergencyHub.Application.DTOs.Request;
using SatelliteEmergencyHub.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteEmergencyHub.Application.Services.Interfaces
{
    public interface IRegionService
    {
        Task<IEnumerable<RegionResponse>> GetAllAsync();
        Task<RegionResponse> GetByIdAsync(int id);
        Task<RegionResponse> CreateAsync(CreateRegionRequest request);
        Task<RegionResponse> UpdateAsync(int id, UpdateRegionRequest request);
        Task DeleteAsync(int id);
    }
}
