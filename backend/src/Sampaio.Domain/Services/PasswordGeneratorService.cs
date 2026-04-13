using System;
using Sampaio.Domain.Contracts.Services;

namespace Sampaio.Domain.Services
{
    public class PasswordGeneratorService : IPasswordGeneratorService
   {
        public string GeneratePassword()
        {
            const int maxIdenticalChars = 2;
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string specials = @"!#$%&*@\";
            var random = new Random();
            var lengthOfPassword = 6;
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
        
        private char GetPasswordElement(Random random, string chars, bool toUpper = false)
        {
            var result = chars[random.Next(chars.Length - 1)];
            return toUpper ? Char.ToUpper(result) : result;
        }
        private char[] ShuffleArray(Random random, char[] array)
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
    }
}