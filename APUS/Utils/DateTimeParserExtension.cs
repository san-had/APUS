namespace APUS.Utils
{
    using System;

    public static class DateTimeParserExtension
    {
        public static DateTime ParseUsDateFormat(this string dateString)
        {
            string[] ymd = dateString.Split('/');

            int day = int.Parse(ymd[0]);

            int month = int.Parse(ymd[1]);

            int year = int.Parse(ymd[2]);

            return new DateTime(year, month, day);
        }
    }
}
