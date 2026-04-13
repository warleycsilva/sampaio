using FluentValidation;
using Sampaio.Domain.Commands.Profiles;
using Sampaio.Shared.Constants;

namespace Sampaio.Domain.Validators
{
    public class AcceptTermsCommandValidator : AbstractValidator<AcceptTermsCommand>
    {
        public AcceptTermsCommandValidator()
        {
            RuleFor(x => x.SessionUser)
                .NotEmpty()
                .WithMessage(CommonMessages.Unauthorized);
        }
    }
}