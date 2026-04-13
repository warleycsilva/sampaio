using FluentValidation;
using Sampaio.Domain.Commands.Profiles;
using Sampaio.Domain.CommandsMessages;

namespace Sampaio.Domain.Validators
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {

            RuleFor(x => x.CurrentPassword)
                .NotEmpty()
                .WithMessage(ProfileCommandMessages.PasswordRequiredError);

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage(ProfileCommandMessages.NewPasswordRequiredError);
            
            RuleFor(x => x.NewPasswordConfirmation)
                .NotEmpty()
                .WithMessage(ProfileCommandMessages.NewPasswordConfirmationRequiredError);
            
            RuleFor(x => x.NewPassword)
                .Matches(x => x.NewPasswordConfirmation)
                .WithMessage(ProfileCommandMessages.NewPassMatchError);
        }
    }
}