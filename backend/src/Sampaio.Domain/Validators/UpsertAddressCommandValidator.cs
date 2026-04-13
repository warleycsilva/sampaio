using Sampaio.Domain.CommandsMessages;
using FluentValidation;
using Sampaio.Domain.Commands.Commerce;

namespace Sampaio.Domain.Validators
{
    public class UpsertAddressCommandValidator : AbstractValidator<UpsertAddressCommand>
    {
        public UpsertAddressCommandValidator()
        {
            RuleFor(x => x.Address.Number)
                .NotEmpty()
                .NotNull()
                .WithMessage("Número deve ser preenchido");
        }
    }
}