namespace APUS.ViewModels
{
    using System;
    using System.Text;

    public class OfficerViewCalculator : IOfficerViewCalculator
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

        public string GetInOfficeRange(DateTime? tookOffice, DateTime? leftOffice)
        {
            var sb = new StringBuilder();

            if (tookOffice.HasValue || leftOffice.HasValue)
            {
                sb.Append("(");
                var tookYear = tookOffice.HasValue ? tookOffice.Value.Year.ToString() : Constants.NAString;
                sb.Append(tookYear);
                sb.Append("-");
                var leftYear = GetLeftYearString(leftOffice);
                sb.Append(leftYear).Append(")");
            }
            else
            {
                sb.Append(Constants.NAString);
            }

            return sb.ToString();
        }

        public DateTime? LeftOfficeParser(DateTime? leftOffice)
        {
            return leftOffice.HasValue ? leftOffice.Value : DateTime.Now;
        }

        public string GetLeftYearString(DateTime? leftOffice)
        {
            string leftOfficeString = string.Empty;

            var currentDate = DateTime.Now.Date;

            leftOfficeString = leftOffice.HasValue && leftOffice.Value.Date != currentDate
                ? leftOffice.Value.Year.ToString()
                : Constants.NALeftOfficeString;

            return leftOfficeString;
        }
    }
}