using System;
using System.Linq;
using System.Text.RegularExpressions;
using Sampaio.Domain.Contracts.Services;
using Sampaio.Shared.Notifications;

namespace Sampaio.Domain.Services
{
    public class PasswordPolicyService : IPasswordPolicyService
    {
        private readonly IDomainNotification _notifications;

        public PasswordPolicyService(IDomainNotification notifications)
        {
            _notifications = notifications;
        }

        public string GeneratePassword(int minLength)
        {
            const int maxIdenticalChars = 2;
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string specials = @"!#$%&*@\";

            var random = new Random();
            var lengthOfPassword = minLength < 6 ? 6 : minLength;
            var characterSet = $"{chars}{chars.ToUpper()}{numbers}{specials}";

            var password = new char[lengthOfPassword];
            var temp = ShuffleArray(random, new[]
            {
                GetPasswordElement(random, chars, true),
                GetPasswordElement(random, chars),
                GetPasswordElement(random, numbers),
                GetPasswordElement(random, specials)
            });

            for (var i = 0; i < temp.Length; i++)
            {
                password[i] = temp[i];
            }

            for (var i = 4; i < password.Length; i++)
            {
                password[i] = characterSet[random.Next(characterSet.Length - 1)];

                var moreThanTwoIdenticalInARow =
                    i > maxIdenticalChars
                    && password[i] == password[i - 1]
                    && password[i - 1] == password[i - 2];

                if (moreThanTwoIdenticalInARow)
                {
                    i--;
                }
            }

            return string.Join(null, password);
        }

        private char GetPasswordElement(Random random,
            string chars,
            bool toUpper = false)
        {
            var result = chars[random.Next(chars.Length - 1)];
            return toUpper ? Char.ToUpper(result) : result;
        }

        private char[] ShuffleArray(Random random,
            char[] array)
        {
            for (var i = array.Length; i > 0; i--)
            {
                var j = random.Next(i);
                var k = array[j];
                array[j] = array[i - 1];
                array[i - 1] = k;
            }

            return array;
        }

        public bool PasswordIsValid(int minLength,
            string name,
            string email,
            string password)
        {
            // var localizer = ResourceLocalization.Read<PasswordPolicyLocalization>();
            //
            // if (!HasCapitalChar(password) || !HasLowerChar(password) || !HasNumbers(password) ||
            //     !HasSpecialChar(password))
            // {
            //     _notifications.Handle(localizer[PasswordPolicyLocalization.PwdMainValidation]);
            // }
            //
            // if (password.Length < minLength)
            // {
            //     _notifications.Handle(string.Format(localizer[PasswordPolicyLocalization.PwdLengthValidation], minLength));
            // }
            //
            // if (HasRepeatedCharactersInSequence(password))
            // {
            //     _notifications.Handle(localizer[PasswordPolicyLocalization.PwdRepeatedCharactersInSequenceValidation]);
            //    
            // }
            //
            // if (HasUserNameOrUserEmailParts(password, name, email))
            // {
            //     _notifications.Handle(localizer[PasswordPolicyLocalization.PwdUserNameOrUserEmailPartsValidation]);
            // }

            return !_notifications.HasNotifications();
        }

        private bool HasCapitalChar(string value) => Regex.IsMatch(value, "[A-Z]");


        private bool HasLowerChar(string value) => Regex.IsMatch(value, "[a-z]");


        private bool HasNumbers(string value) => Regex.IsMatch(value, "[0-9]");

        private bool HasSpecialChar(string value) => value.Any(ch => !char.IsLetterOrDigit(ch));

        private bool HasRepeatedCharactersInSequence(string value)
        {
            if (value.Length < 2) return false;

            for (var i = 1; i < value.Length; i++)
            {
                if (value[i - 1].Equals(value[i])) return true;
            }

            return false;
        }

        private bool HasUserNameOrUserEmailParts(string value,
            string name,
            string email)
        {
            var emailSeparatorIndex = email.IndexOf("@", StringComparison.OrdinalIgnoreCase);

            return
            (
                emailSeparatorIndex > 0 &&
                value.IndexOf(email.Substring(0, emailSeparatorIndex), StringComparison.OrdinalIgnoreCase) >= 0
            ) || name.Split(" ").Any(x => value.IndexOf(x, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}