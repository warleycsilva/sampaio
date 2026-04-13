using FluentValidation;
using Sampaio.Domain.Commands.Product;
using Sampaio.Domain.CommandsMessages;

namespace Sampaio.Domain.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.DiscountPrice)
                .NotEmpty()
                .WithMessage(ProductCommandMessages.DiscountPriceRequired);
        }
    }
}