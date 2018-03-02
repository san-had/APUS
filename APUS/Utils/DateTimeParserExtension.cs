namespace APUS.Utils
{
    using System;

    public static class DateTimeParserExtension
    {
        public static DateTime? ParseEnDateFormat(this string dateString)
        {
            if (dateString == null)
            {
                return null;
            }

            DateTime? parsedDate = null;

            string[] ymd = dateString.Split('/');

            if (ymd.Length < 3)
            {
                return null;
            }

            int day, month, year = 0;

            bool isParsedDay = int.TryParse(ymd[0], out day);

            if (day > 31)
            {
                isParsedDay = false;
            }

            bool isParsedMonth = int.TryParse(ymd[1], out month);

            if (month > 12)
            {
                isParsedMonth = false;
            }

            bool isParsedYear = int.TryParse(ymd[2], out year);

            bool isParsed = isParsedDay && isParsedMonth && isParsedYear;

            if (isParsed)
            {
                parsedDate = new DateTime(year, month, day);
            }

            return parsedDate;
        }
    }
}