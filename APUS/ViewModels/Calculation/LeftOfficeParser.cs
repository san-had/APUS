namespace APUS.ViewModels.Calculation
{
    using System;

    public class LeftOfficeParser : ILeftOfficeParser
    {
        public DateTime? ParseLeftOffice(DateTime? leftOffice)
        {
            return leftOffice.HasValue ? leftOffice.Value : DateTime.Now;
        }
    }
}