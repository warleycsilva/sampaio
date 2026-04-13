using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Profiles;
using Sampaio.Shared.Security;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.Commands.Profiles
{
    public class ChangeProfilePhotoCommand : IRequest<ChangeProfilePhotoResult>
    {
        [Required]
        public FileInput FileInput { get; set; }
        
        [JsonIgnore]
        public SessionUser SessionUser { get; set; }
    }
}