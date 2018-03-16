namespace APUS.ViewModels.Calculation
{
    using System;

    public class InOfficeDaysCalculator : IInOfficeDaysCalculator
    {
        public int CalculateNumberOfInOfficeDays(DateTime? tookOffice, DateTime? leftOffice)
        {
            int presidencyDays = 0;

            if (tookOffice.HasValue && leftOffice.HasValue)
            {
                TimeSpan offset = leftOffice.Value.Subtract(tookOffice.Value);

                presidencyDays = offset.Days;
            }

            return presidencyDays;
        }
    }
}