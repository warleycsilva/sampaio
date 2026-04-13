using FluentValidation;
using Sampaio.Domain.Commands.Profiles;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Shared.Constants;

namespace Sampaio.Domain.Validators
{
    public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
    {
        public UpdateUserProfileCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(ProfileCommandMessages.FirstnameRequiredError);
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(SignupCommandMessages.EmailRequiredError);
            
            RuleFor(x => x.Email)
                .MaximumLength(255)
                .WithMessage(SignupCommandMessages.EmailMaxLengthError)
                .When(x =>!string.IsNullOrWhiteSpace(x.Email));
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(SignupCommandMessages.EmailInvalidError)
                .When(x =>!string.IsNullOrWhiteSpace(x.Email));
            
            RuleFor(x => x.SessionUser)
                .NotEmpty()
                .WithMessage(CommonMessages.Unauthorized);
        }
    }
}