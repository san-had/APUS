namespace APUS.ViewModels
{
    using System;
    using System.Text;

    public class InOfficeRangeComposer : IInOfficeRangeComposer
    {
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