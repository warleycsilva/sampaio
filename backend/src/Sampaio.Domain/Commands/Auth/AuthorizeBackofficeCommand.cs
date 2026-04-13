using System.ComponentModel.DataAnnotations;
using MediatR;
using Sampaio.Domain.Results.Auth;

namespace Sampaio.Domain.Commands.Auth
{
    public class AuthorizeBackofficeCommand: IRequest<AuthorizeBackofficeResult>
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}