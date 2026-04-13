using FluentValidation;
using Sampaio.Domain.Commands.Profiles;
using Sampaio.Domain.CommandsMessages;

namespace Sampaio.Domain.Validators
{
    public class ActiveAccountCommandValidator : AbstractValidator<ActiveAccountCommand>
    {
        public ActiveAccountCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage(SignupCommandMessages.EmptyToken);
        }
    }
}