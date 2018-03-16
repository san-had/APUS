namespace APUS.ViewModels.Calculation
{
    using System;

    public interface IInOfficeDaysCalculator
    {
        int CalculateNumberOfInOfficeDays(DateTime? tookOffice, DateTime? leftOffice);
    }
}