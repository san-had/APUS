namespace APUS.DataLoader
{
    using System;

    public interface IDateParser
    {
        DateTime? ParseDate(string dateString);
    }
}