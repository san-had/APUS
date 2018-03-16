namespace APUS.ViewModels.Calculation
{
    using System;

    public interface ILeftOfficeParser
    {
        DateTime? ParseLeftOffice(DateTime? leftOffice);
    }
}