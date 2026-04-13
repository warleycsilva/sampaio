using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.UserDevice;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Commands.UserDevice
{
    public class AddUserDeviceCommand : IRequest<UserDeviceBaseResult>
    {
        [Required]
        public string DeviceId { get;  set; }
        [Required]
        public string DeviceToken { get;  set; }
        [JsonIgnore]
        public SessionUser SessionUser { get;  set; }
    }
}