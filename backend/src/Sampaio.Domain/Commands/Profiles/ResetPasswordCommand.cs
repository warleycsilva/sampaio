using System.ComponentModel.DataAnnotations;
using MediatR;
using Sampaio.Domain.Results.Profiles;

namespace Sampaio.Domain.Commands.Profiles
{
    public class ResetPasswordCommand : IRequest<ResetPasswordResult>
    {
        [Required]
        public string NewPassword { get; set; }
        
        [Required]
        public string NewPasswordConfirmation { get; set; }
        
        [Required]
        public string Token { get; set; }
    }
}