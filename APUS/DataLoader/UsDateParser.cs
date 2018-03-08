namespace APUS.DataLoader
{
    using System;

    public class UsDateParser : IDateParser
    {
        public DateTime? ParseDate(string dateString)
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

            int month, day, year = 0;

            bool isParsedMonth = int.TryParse(ymd[0], out month);

            if (month > 12)
            {
                isParsedMonth = false;
            }

            bool isParsedDay = int.TryParse(ymd[1], out day);

            if (day > 31)
            {
                isParsedDay = false;
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