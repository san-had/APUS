namespace APUS.Utils
{
    using System;

    public static class DateTimeParserExtension
    {
        public static DateTime? ParseUsDateFormat(this string dateString)
        {
            string[] ymd = dateString.Split('/');

            if (ymd.Length < 3)
            {
                return null;
            }

            int day, month, year = 0;

            bool isParsed = int.TryParse(ymd[0], out day);

            isParsed = int.TryParse(ymd[1], out month);

            isParsed = int.TryParse(ymd[2], out year);

            DateTime? date = new DateTime(year, month, day);

            return isParsed ? date : null;
        }
    }
}
