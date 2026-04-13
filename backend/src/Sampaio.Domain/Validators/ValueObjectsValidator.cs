using FluentValidation;
using Sampaio.Shared.Notifications;
using Sampaio.Domain.ValueObjects;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Enums;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.Validators
{
    public static class ValueObjectsValidator
    {
        public static IRuleBuilderInitial<T, AddressInformation> Validate<T>(this IRuleBuilder<T, AddressInformation> ruleBuilder,
            bool required = false)
        {
            return ruleBuilder.Custom((value,
                context) =>
            {
                if (required)
                {
                    if (string.IsNullOrWhiteSpace(value?.Address))
                    {
                        context.AddFailure(CommonMessages.BuiltRequiredMessageError("Endereço"));
                    }

                    if (string.IsNullOrWhiteSpace(value?.Number))
                    {
                        context.AddFailure(CommonMessages.BuiltRequiredMessageError("Número do endereço"));
                    }

                    if (string.IsNullOrWhiteSpace(value?.Neighborhood))
                    {
                        context.AddFailure(CommonMessages.BuiltRequiredMessageError("Bairro do endereço"));
                    }

                    if (string.IsNullOrWhiteSpace(value?.ZipCode))
                    {
                        context.AddFailure(CommonMessages.BuiltRequiredMessageError("CEP do endereço"));
                    }
                }

                if (!string.IsNullOrWhiteSpace(value?.Address) && value?.Address?.Length > 255)
                {
                    context.AddFailure(CommonMessages.BuiltMaxLenghMessageError("Endereço", 255));
                }

                if (!string.IsNullOrWhiteSpace(value?.Number) && value?.Number?.Length > 100)
                {
                    context.AddFailure(CommonMessages.BuiltMaxLenghMessageError("Número do endereço", 100));
                }

                if (!string.IsNullOrWhiteSpace(value?.Neighborhood) && value?.Neighborhood?.Length > 100)
                {
                    context.AddFailure(CommonMessages.BuiltMaxLenghMessageError("Bairro do endereço", 100));
                }

                if (!string.IsNullOrWhiteSpace(value?.ZipCode) && value?.ZipCode?.Length > 8)
                {
                    context.AddFailure(CommonMessages.BuiltMaxLenghMessageError("CEP do endereço", 8));
                }

                // if (!string.IsNullOrWhiteSpace(value?.City) && value?.City?.Length > 100)
                // {
                //     context.AddFailure(CommonMessages.BuiltMaxLenghMessageError("Cidade do endereço", 100));
                // }
                //
                // if (!string.IsNullOrWhiteSpace(value?.State) && value?.State?.Length > 2)
                // {
                //     context.AddFailure(CommonMessages.BuiltMaxLenghMessageError("Estado do endereço", 2));
                // }
            });
        }

        public static IRuleBuilderInitial<T, Identification> ValidateIdentification<T>(this IRuleBuilder<T, Identification> ruleBuilder, bool required = false)
        {
            return ruleBuilder.Custom((value,
                context) =>
            {
                if (required)
                {
                    if (value?.Type.HasValue == false)
                    {
                        context.AddFailure(CommonMessages.BuiltRequiredMessageError("Tipo da identifiicação"));
                    }

                    if (string.IsNullOrWhiteSpace(value?.Number))
                    {
                        context.AddFailure(CommonMessages.BuiltRequiredMessageError("Número da identifiicação"));
                    }
                }

                if (!string.IsNullOrWhiteSpace(value?.Number) && value?.Number?.Length > 100)
                {
                    context.AddFailure(CommonMessages.BuiltMaxLenghMessageError("Número da identifiicação", 100));
                }

                if (!string.IsNullOrWhiteSpace(value?.Number) && value?.IsValid() == false && (value.Type == EIdentificationType.Cnpj || value.Type == EIdentificationType.Cpf))
                {
                    context.AddFailure($"Número do {value.Type.ToString().ToUpper()} da identificação é inválido");
                }
                
            });
            
        }

        public static IRuleBuilderInitial<T, Phone> ValidatePhone<T>(this IRuleBuilder<T, Phone> ruleBuilder, bool required = false)
        {
            return ruleBuilder.Custom((value, context) => {
                if (required)
                {
                    if (value?.Type.HasValue == false)
                    {
                        context.AddFailure(CommonMessages.BuiltRequiredMessageError("Tipo do telefone"));
                    }

                    if (string.IsNullOrWhiteSpace(value?.Number))
                    {
                        context.AddFailure(CommonMessages.BuiltRequiredMessageError("Número do telefone"));
                    }
                }

                if (!string.IsNullOrWhiteSpace(value?.Number) && value?.Number?.Length > 50)
                {
                    context.AddFailure(CommonMessages.BuiltMaxLenghMessageError("Número do telefone", 50));
                }
            });
        }
    }
}
