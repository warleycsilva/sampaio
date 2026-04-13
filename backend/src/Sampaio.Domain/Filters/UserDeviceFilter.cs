using System;

namespace Sampaio.Domain.Filters
{
    public class UserDeviceFilter
    {
        public string? DeviceId { get; set; }

        public string? DeviceToken { get; set; }

        public Guid? UserId { get; set; }
    }
}