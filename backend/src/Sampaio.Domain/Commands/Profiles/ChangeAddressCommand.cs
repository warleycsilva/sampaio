using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Profiles;
using Sampaio.Domain.ValueObjects;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Commands.Profiles
{
    public class ChangeAddressCommand : IRequest<ChangeAddressResult>
    {
        [Required]
        public AddressInformation AddressInformation { get;  set; }
        
        [JsonIgnore]
        public SessionUser SessionUser { get; set; }
    }
}