using System;
using System.IO;

namespace Sampaio.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static bool NotNull(this string value) => !string.IsNullOrWhiteSpace(value?.Trim());
    }
}