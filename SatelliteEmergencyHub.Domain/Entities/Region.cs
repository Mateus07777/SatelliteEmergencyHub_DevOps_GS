using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteEmergencyHub.Domain.Entities
{
    public class Region : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public String Country { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double RadiusKm { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();
        public ICollection<Occurrence> Occurrences { get; set; } = new List<Occurrence>();
    }
}
