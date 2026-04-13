using System;
using Sampaio.Shared.Extensions;
using Sampaio.Shared.Enums;

namespace Sampaio.Shared.ValueObjects
{
    public class Phone
    {
        public Phone()
        {
            Number = string.Empty;
        }

        public Phone(string number = null, EPhoneType? type = null)
        {
            Number = number?.ToNumber();
            Type = type;
        }

        public string Number { get; set; }

        public EPhoneType? Type { get; set; }

        public void Modify(string number, EPhoneType type) => (Number, Type) = (number, type);

        public string Formatted
        {
            get
            {
                if (string.IsNullOrEmpty(Number))
                {
                    return string.Empty;
                }

                var value = Convert.ToUInt64(Number.ToNumber());

                return Number.Length == 8
                    ? value.ToString(@"####\-####")
                    : Number.Length == 9
                    ? value.ToString(@"#\-####\-####")
                    : Number.Length == 10
                    ? value.ToString(@"(##) ####\-####")
                    : Number.Length == 11
                    ? value.ToString(@"(##) #\-####\-####")
                    : value.ToString();
            }
        }

        public void Update(Phone phone)
        {
            Number = phone?.Number;
        }
    }
}
