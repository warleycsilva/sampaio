using Sampaio.Shared.Extensions;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Structs;

namespace Sampaio.Shared.ValueObjects
{
    public class Identification
    {
        public Identification()
        {
            Number = string.Empty;
            Type = default;
        }

        public Identification(string number,
            EIdentificationType type) => (Number, Type) = (number, type);

        public string Number { get; set; }

        public EIdentificationType? Type { get; set; }
        
        public void Modify(string number)
        {
            switch (Type)
            {
                case EIdentificationType.Cpf:
                case EIdentificationType.Cnpj:
                    Number = number?.ToNumber();
                    break;
                // case EIdentificationType.Other:
                //     Number = number;
                //     break;
                default:
                    Number = number;
                    break;
            }
        }

        public bool IsValid()
        {
            switch (Type)
            {
                case EIdentificationType.Cpf:
                    return new Cpf(Number).IsValid;
                case EIdentificationType.Cnpj:
                    return new Cnpj(Number).IsValid;
                // case EIdentificationType.Other:
                //     return !string.IsNullOrEmpty(Number);
                default:
                    return !string.IsNullOrEmpty(Number);
            }
        }

        public string Formatted
        {
            get
            {
                switch (Type)
                {
                    case EIdentificationType.Cpf:
                        return new Cpf(Number).Format();
                    case EIdentificationType.Cnpj:
                        return new Cnpj(Number).Format();
                    // case EIdentificationType.Other:
                    //     return Number;
                    default:
                        return Number;
                }
            }
        }

        public void Update(Identification identification)
        {
            Number = identification.Number ?? string.Empty;
            Type = identification?.Type;
        }
    }
}
