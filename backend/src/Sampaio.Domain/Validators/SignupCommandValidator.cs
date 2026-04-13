using FluentValidation;
using Sampaio.Shared.Constants;
using Sampaio.Domain.Commands.Signup;
using Sampaio.Domain.CommandsMessages;

namespace Sampaio.Domain.Validators
{
    public class SignupCommandValidator : AbstractValidator<SignupCommand>
    {
        public SignupCommandValidator()
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

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(SignupCommandMessages.PasswordRequiredError);
            
            RuleFor(x => x.PasswordConfirmation)
                .NotEmpty()
                .WithMessage(SignupCommandMessages.PasswordConfirmationRequiredError);

            RuleFor(x => x)
                .Must(x => x.Password == x.PasswordConfirmation)
                .WithMessage(SignupCommandMessages.PasswordsNotMatchError)
                .When(x => !string.IsNullOrWhiteSpace(x.Password) || !string.IsNullOrWhiteSpace(x.PasswordConfirmation));
            
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(SignupCommandMessages.EmailInvalidError)
                .When(x =>!string.IsNullOrWhiteSpace(x.Email));
            
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(SignupCommandMessages.FirstNameRequiredError);
            
            RuleFor(x => x.FirstName)
                .MaximumLength(255)
                .WithMessage(SignupCommandMessages.FirstNameMaxLengthError)
                .When(x =>!string.IsNullOrWhiteSpace(x.FirstName));
            
            RuleFor(x => x.LastName)
                .MaximumLength(255)
                .WithMessage(SignupCommandMessages.LastNameMaxLengthError)
                .When(x =>!string.IsNullOrWhiteSpace(x.LastName));

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage(SignupCommandMessages.TypeInvalidError);
        }
    }
}
