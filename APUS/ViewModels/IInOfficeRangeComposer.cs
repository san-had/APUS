namespace APUS.ViewModels
{
    using System;

    public interface IInOfficeRangeComposer
    {
        string GetInOfficeRange(DateTime? tookOffice, DateTime? leftOffice);
    }
}