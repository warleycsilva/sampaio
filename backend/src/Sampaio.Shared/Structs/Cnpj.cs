using System;
using System.Text.RegularExpressions;
using Sampaio.Shared.Extensions;

namespace Sampaio.Shared.Structs
{

    [Serializable]
    public struct Cnpj
    {
        private readonly string _value;

        public readonly bool IsValid;
        static readonly int[] Multiplier1 = {5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};
        static readonly int[] Multiplier2 = {6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};

        public Cnpj(string value)
        {
            value = value.ToNumber();
            _value = value;
           
            var identicalDigits = true;
            var lastDigit = -1;
            var position = 0;
            var totalDigit1 = 0;
            var totalDigit2 = 0;

            foreach (var c in _value)
            {
                if (char.IsDigit(c))
                {
                    var digit = c - '0';
                    if (position != 0 && lastDigit != digit)
                    {
                        identicalDigits = false;
                    }

                    lastDigit = digit;
                    if (position < 12)
                    {
                        totalDigit1 += digit * Multiplier1[position];
                        totalDigit2 += digit * Multiplier2[position];
                    }
                    else if (position == 12)
                    {
                        var dv1 = (totalDigit1 % 11);
                        dv1 = dv1 < 2
                            ? 0
                            : 11 - dv1;

                        if (digit != dv1)
                        {
                            IsValid = false;
                            return;
                        }

                        totalDigit2 += dv1 * Multiplier2[12];
                    }
                    else if (position == 13)
                    {
                        var dv2 = (totalDigit2 % 11);

                        dv2 = dv2 < 2
                            ? 0
                            : 11 - dv2;

                        if (digit != dv2)
                        {
                            IsValid = false;
                            return;
                        }
                    }

                    position++;
                }
            }

            IsValid = (position == 14) && !identicalDigits;
        }

        public static implicit operator Cnpj(string value) => new Cnpj(value);

        public override string ToString() => _value;
        
        public string Format()
        {
            if (string.IsNullOrWhiteSpace(_value)) return string.Empty;

            var number = Regex.Replace(_value, @"[^0-9]", string.Empty);

            return Convert.ToUInt64(number).ToString(@"##\.###\.###\/####\-##");
        }
    }
}
