using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteEmergencyHub.Application
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string? message) : base(message)
        {
        }

        public NotFoundException(string resource, int id)
            : base($"{resource} with id {id} not found.") { }
    }

    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }
    }


    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }
}
