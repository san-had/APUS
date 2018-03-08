namespace APUS.DataLoader
{
    using System;

    public class DateParser
    {
        public DateTime? DateComposition(string[] ymd)
        {
            if (ymd.Length != 3)
            {
                return null;
            }

            DateTime? parsedDate = null;

            int day, month, year = 0;

            bool isParsedYear = int.TryParse(ymd[0], out year);

            bool isParsedMonth = int.TryParse(ymd[1], out month);

            if (month > 12)
            {
                isParsedMonth = false;
            }

            bool isParsedDay = int.TryParse(ymd[2], out day);

            if (day > 31)
            {
                isParsedDay = false;
            }

            bool isParsed = isParsedDay && isParsedMonth && isParsedYear;

            if (isParsed)
            {
                parsedDate = new DateTime(year, month, day);
            }

            return parsedDate;
        }
    }
}