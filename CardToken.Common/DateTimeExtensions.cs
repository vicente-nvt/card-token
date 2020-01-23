using System;
using System.Globalization;

namespace CardToken.Common
{
    public static class DateTimeExtensions
    {
        public static string ToStringWithMiliseconds(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
    }
}
