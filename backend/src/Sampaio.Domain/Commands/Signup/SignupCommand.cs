using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Signup;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.Commands.Signup
{
    public class SignupCommand : IRequest<SignupResult>
    {
        [EmailAddress, JsonRequired, MaxLength(255)]
        public string Email { get; set; }

        [PasswordPropertyText, JsonRequired]
        public string Password { get; set; }

        [PasswordPropertyText, JsonRequired]
        public string PasswordConfirmation { get; set; }

        [JsonRequired, MaxLength(255)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public ESignupType Type { get; set; }
    }
}

