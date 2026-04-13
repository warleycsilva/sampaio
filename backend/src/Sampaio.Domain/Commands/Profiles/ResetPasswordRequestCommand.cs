using System.ComponentModel.DataAnnotations;
using MediatR;
using Sampaio.Domain.Results.Profiles;

namespace Sampaio.Domain.Commands.Profiles
{
    public class ResetPasswordRequestCommand : IRequest<ResetPasswordRequestResult>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}