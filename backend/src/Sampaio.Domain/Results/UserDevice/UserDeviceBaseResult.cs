using System;

namespace Sampaio.Domain.Results.UserDevice
{
    public class UserDeviceBaseResult
    {
        public string Message { get; set; }
        public string Error { get; set; }
        public bool Success { get; set; } = false;
        public Guid UserDeviceId { get; set; }
    }
}