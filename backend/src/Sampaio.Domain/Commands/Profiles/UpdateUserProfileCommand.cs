using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Shared.ValueObjects;
using Sampaio.Domain.Results.Profiles;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Commands.Profiles
{
    public class UpdateUserProfileCommand : IRequest<UpdateUserProfileResult>
    {
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public SessionUser SessionUser { get; set; }
    }
}