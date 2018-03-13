namespace APUS.ViewModels
{
    using System;

    public interface ILeftOfficeParser
    {
        DateTime? ParseLeftOffice(DateTime? leftOffice);
    }
}