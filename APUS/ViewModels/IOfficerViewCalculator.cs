namespace APUS.ViewModels
{
    using System;

    public interface IOfficerViewCalculator
    {
        int CalculateNumberOfInOfficeDays(DateTime? tookOffice, DateTime? leftOffice);

        string GetInOfficeRange(DateTime? tookOffice, DateTime? leftOffice);

        DateTime? LeftOfficeParser(DateTime? leftOffice);
    }
}