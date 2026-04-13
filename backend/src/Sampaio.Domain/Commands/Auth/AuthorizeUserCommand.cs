using System.ComponentModel.DataAnnotations;
using MediatR;
using Sampaio.Domain.Results.Auth;

namespace Sampaio.Domain.Commands.Auth
{
    
    public class AuthorizeUserCommand : IRequest<AuthorizeUserResult>
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}