namespace APUS.ViewModels.Calculation
{
    using System;

    public interface IInOfficeRangeComposer
    {
        string GetInOfficeRange(DateTime? tookOffice, DateTime? leftOffice);
    }
}