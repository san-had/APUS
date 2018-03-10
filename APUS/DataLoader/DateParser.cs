namespace APUS.DataLoader
{
    using System;
    using System.Collections.Generic;

    public class DateParser
    {
        public DateTime? DateComposition(string[] ymd)
        {
            if (ymd == null || ymd.Length != 3)
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

            if (day > MonthMaxDays(month))
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

        public int MonthMaxDays(int month)
        {
            if (month > 12)
            {
                return 0;
            }
            var maxDayDictionary = new Dictionary<int, int>()
            {
                {0,0},
                {1,31},
                {2,29},
                {3,31},
                {4,30},
                {5,31},
                {6,30},
                {7,31},
                {8,31},
                {9,30},
                {10,31},
                {11,30},
                {12,31}
            };
            return maxDayDictionary[month];
        }
    }
}