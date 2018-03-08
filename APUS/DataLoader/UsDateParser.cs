namespace APUS.DataLoader
{
    using System;

    public class UsDateParser : DateParser, IDateParser
    {
        public DateTime? ParseDate(string dateString)
        {
            if (dateString == null)
            {
                return null;
            }

            string[] mdy = dateString.Split('/');

            if (mdy.Length != 3)
            {
                return null;
            }

            string[] ymd = new string[3] { mdy[2], mdy[0], mdy[1] };

            DateTime? parsedDate = DateComposition(ymd);

            return parsedDate;
        }
    }
}