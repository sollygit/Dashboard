using System;

namespace Dashboard.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static bool SameWeek(this DateTimeOffset date, DateTimeOffset compareTo)
        {
            var calendar = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            var d1 = date.Date.AddDays(-1 * (int)calendar.GetDayOfWeek(date.DateTime));
            var d2 = compareTo.Date.AddDays(-1 * (int)calendar.GetDayOfWeek(compareTo.DateTime));
            return d1 == d2;
        }
    }
}
