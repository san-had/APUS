namespace APUS.ViewModels
{
    using System;

    public interface IPresidentViewCalculator
    {
        int CalculateNumberOfPresidencyDays(DateTime? tookOffice, DateTime? leftOffice);

        string GetPresidencyRange(DateTime? tookOffice, DateTime? leftOffice);
    }
}
