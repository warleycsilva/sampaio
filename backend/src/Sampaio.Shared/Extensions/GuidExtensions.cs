using System;

namespace Sampaio.Shared.Extensions
{
    public static class GuidExtensions
    {
        public static string HasDigits(this Guid value) => value.ToString("N");
    }
}