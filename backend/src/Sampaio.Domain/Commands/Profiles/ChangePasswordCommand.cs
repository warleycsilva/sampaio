using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Profiles;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Commands.Profiles
{
    public class ChangePasswordCommand : IRequest<ResetPasswordResult>
    {
        [Required]
        public string CurrentPassword { get; set; }
        
        [Required]
        public string NewPassword { get; set; }
        
        [Required]
        public string NewPasswordConfirmation { get; set; }
        
        [JsonIgnore]
        public SessionUser SessionUser { get; set; }
    }
}