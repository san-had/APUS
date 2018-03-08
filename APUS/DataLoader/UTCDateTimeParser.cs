namespace APUS.DataLoader
{
    using System;

    public class UTCDateTimeParser : DateParser, IDateParser
    {
        public DateTime? ParseDate(string dateString)
        {
            if (dateString == null)
            {
                return null;
            }

            string[] ymd = dateString.Split('T')[0].Split('-');

            if (ymd.Length != 3)
            {
                return null;
            }

            DateTime? parsedDate = DateComposition(ymd);

            return parsedDate;
        }
    }
}