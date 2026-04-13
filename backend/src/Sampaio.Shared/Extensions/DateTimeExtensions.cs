using System;
using System.Collections.Generic;
using System.Text;
using TimeZoneConverter;

namespace Sampaio.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToBrazilianTimezone(this DateTime value)
        {
            var timeZoneInfo = "E. South America Standard Time";

            return TimeZoneInfo.ConvertTime(value, TZConvert.GetTimeZoneInfo(timeZoneInfo));
        }
    }
}
