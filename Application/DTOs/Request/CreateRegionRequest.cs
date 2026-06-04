using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SatelliteEmergencyHub.Application.DTOs.Request
{
    public class CreateRegionRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string State { get; set; } = string.Empty;

        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Range(-180, 180)]
        public double Longitude { get; set; }

        [Range(1, 10000)]
        public double RadiusKm { get; set; }
    }

    public class UpdateRegionRequest : CreateRegionRequest
    {
        public bool IsActive { get; set; } = true;
    }
}
