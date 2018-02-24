﻿namespace APUS.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using APUS.Models;

    public class PresidentViewLoader : IPresidentViewLoader, IPresidentViewCalculator
    {
        public IEnumerable<ViewModels.PresidentView> UpdateViewPresidents(IEnumerable<President> presidents)
        {
            if (presidents == null)
            {
                return Enumerable.Empty<PresidentView>();
            }

            var presidentViewList = new List<ViewModels.PresidentView>();

            foreach (var president in presidents)
            {
                var viewPresident = new ViewModels.PresidentView();

                DateTime? leftOffice = LeftOfficeParser(president.LeftOffice);

                viewPresident.FirstName = president.FirstName;
                viewPresident.LastName = president.LastName;
                viewPresident.PresidencyRange = GetPresidencyRange(president.TookOffice, leftOffice);
                viewPresident.NumberOfPresidencyDays = CalculateNumberOfPresidencyDays(president.TookOffice, leftOffice);

                presidentViewList.Add(viewPresident);
            }
            return presidentViewList;
        }

        public DateTime? LeftOfficeParser(DateTime? leftOffice)
        {
            return leftOffice.HasValue ? leftOffice.Value : DateTime.Now;
        }

        public int CalculateNumberOfPresidencyDays(DateTime? tookOffice, DateTime? leftOffice)
        {
            int presidencyDays = 0;

            if (tookOffice.HasValue && leftOffice.HasValue)
            {
                TimeSpan offset = leftOffice.Value.Subtract(tookOffice.Value);

                presidencyDays = offset.Days;
            }
 
            return presidencyDays;
        }

        public string GetPresidencyRange(DateTime? tookOffice, DateTime? leftOffice)
        {
            var sb = new StringBuilder();

            if (tookOffice.HasValue || leftOffice.HasValue)
            {
                sb.Append("(");
                var tookYear = tookOffice.HasValue ? tookOffice.Value.Year.ToString() : Constants.NAString;
                sb.Append(tookYear);
                sb.Append("-");
                var leftYear = leftOffice.HasValue ? leftOffice.Value.Year.ToString() : Constants.NALeftOfficeString;
                sb.Append(leftYear).Append(")");
            }
            else
            {
                sb.Append(Constants.NAString);
            }

            return sb.ToString();
        }
    }
}