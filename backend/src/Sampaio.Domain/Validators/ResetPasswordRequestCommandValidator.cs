using FluentValidation;
using Sampaio.Domain.Commands.Profiles;
using Sampaio.Domain.CommandsMessages;

namespace Sampaio.Domain.Validators
{
    public class ResetPasswordRequestCommandValidator : AbstractValidator<ResetPasswordRequestCommand>
    {
        public ResetPasswordRequestCommandValidator()
        {
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
        }
    }
}