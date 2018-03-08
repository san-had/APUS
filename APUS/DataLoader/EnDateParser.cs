namespace APUS.DataLoader
{
    using System;

    public class EnDateParser : DateParser, IDateParser
    {
        public DateTime? ParseDate(string dateString)
        {
            if (dateString == null)
            {
                return null;
            }

            string[] dmy = dateString.Split('/');

            if (dmy.Length != 3)
            {
                return null;
            }

            string[] ymd = new string[3] { dmy[2], dmy[1], dmy[0] };

            DateTime? parsedDate = DateComposition(ymd);

            return parsedDate;
        }
    }
}