using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Sampaio.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string FormatPhone(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            
            var number = Regex.Replace(value, @"[^0-9]", "");

            value = number.Length == 10
                ? Convert.ToUInt64(number).ToString(@"00\-00000000")
                : number.Length == 11
                    ? Convert.ToUInt64(number).ToString(@"00\-000000000")
                    : value;

            return value;
        }

        public static string ToNumber(this string value) =>
            string.IsNullOrWhiteSpace(value?.Trim())
                ? ""
                : Regex.Replace(value, @"[^0-9]", "");


        public static string ToBase64Hash(this string value)
        {
            using (var hashData = SHA256.Create())
            {
                var data = hashData.ComputeHash(Encoding.ASCII.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }

//        public static string ToSha256(this string value)
//        {
//            using (var hashData = SHA256.Create())
//            {
//                var hash = new StringBuilder();
//
//                foreach (var theByte in hashData.ComputeHash(Encoding.ASCII.GetBytes(value)))
//                {
//                    hash.Append(theByte.ToString("x2"));
//                }
//
//                return hash.ToString();
//            }
//        }

        public static string FormatCpfCnpj(this string value)
        {
            var result = "";

            if (string.IsNullOrWhiteSpace(value)) return result;

            var number = value.ToNumber();

            result = number.Length == 11
                ? Convert.ToUInt64(number).ToString(@"000\.000\.000\-00")
                : number.Length == 14
                    ? Convert.ToUInt64(number).ToString(@"00\.000\.000\/0000\-00")
                    : result;

            return result;
        }

        public static string FromBase64(this string value) => Encoding.UTF8.GetString(Convert.FromBase64String(value));
        public static string ToBase64(this string value) => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        public static string ReplaceSpecial(this string value) => Regex.Replace(value, @"[\\/]", "_");

        public static string ToBase64Key(this string value) => value.ToBase64().ReplaceSpecial();

        public static string GetExtension(this string value) => Path.GetExtension(value);

        public static string RemoveSpaces(this string value)
        {
            value = value.Replace(" ", "").Trim();
            return value;
        }

        public static T ToEnum<T>(this string value)
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }

        public static bool IsNull(this string value)
        {
            return string.IsNullOrWhiteSpace(value?.Trim());
        }
        
        public static string GenerateCode(this string value, int length)
        {
            var random = new Random();
            var characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }

            return result.ToString();
        }

        public static string HandleNullOrEmpty(this string value,
            string subValue)
        {
            value = string.IsNullOrWhiteSpace(value) ? subValue : value;
            return value;
        }

        public static string GetnerateRandonValue(this string value,
            int length,
            string toContact = "") => $"{string.Join("", Enumerable.Range(0, 255).Select(x => x.ToString())).Substring(0, length)}{toContact}";
        
        public static string HandleContentType(this string fileName)
        {
            var extension = Path.GetExtension(fileName);

            if (extension.Equals(".js", StringComparison.OrdinalIgnoreCase))
            {
                return "application/javascript";
            }

            if (extension.Equals(".png", StringComparison.OrdinalIgnoreCase))
            {
                return "image/png";
            }

            if (extension.Equals(".svg", StringComparison.OrdinalIgnoreCase))
            {
                return "image/svg+xml";
            }

            if (extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                return "image/jpeg";
            }

            if (extension.Equals(".gif", StringComparison.OrdinalIgnoreCase))
            {
                return "image/gif";
            }

            if (extension.Equals(".ico", StringComparison.OrdinalIgnoreCase))
            {
                return "image/x-icon";
            }

            if (extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                return "application/pdf";
            }

            if (extension.Equals(".doc", StringComparison.OrdinalIgnoreCase))
            {
                return "application/msword";
            }

            if (extension.Equals(".docx", StringComparison.OrdinalIgnoreCase))
            {
                return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            }

            return "application/octet-stream";
        }
        public static string RemoveNumbersSpecialCharactersAndAccents(this string input)
        {
            var stringWithoutSpecialChars = Regex.Replace(input, "[^a-zA-Z ]", "");

            var stringWithoutAccents = RemoveAccents(stringWithoutSpecialChars);

            return stringWithoutAccents;
        }

        public static string RemoveAccents(string input)
        {
            var normalizedString = input.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString();
        }
    }
}
