namespace APUS.ViewModels
{
    using System;

    public interface IInOfficeDaysCalculator
    {
        int CalculateNumberOfInOfficeDays(DateTime? tookOffice, DateTime? leftOffice);
    }
}