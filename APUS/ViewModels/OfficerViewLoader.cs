namespace APUS.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using APUS.Models;

    public class OfficerViewLoader : IOfficerViewLoader
    {
        private IOfficerViewCalculator officerViewCalculator;

        public OfficerViewLoader(IOfficerViewCalculator officerViewCalculator)
        {
            this.officerViewCalculator = officerViewCalculator ?? throw new ArgumentNullException(nameof(officerViewCalculator));
        }

        public IEnumerable<ViewModels.OfficerView> UpdateViewOfficers(IEnumerable<Officer> officers)
        {
            if (officers == null)
            {
                return Enumerable.Empty<OfficerView>();
            }

            var officerViewList = new List<OfficerView>();

            foreach (var president in officers)
            {
                var viewPresident = new ViewModels.OfficerView();

                DateTime? leftOffice = this.officerViewCalculator.LeftOfficeParser(president.LeftOffice);

                viewPresident.FirstName = president.FirstName;
                viewPresident.LastName = president.LastName;
                viewPresident.InOfficeRange = this.officerViewCalculator.GetInOfficeRange(president.TookOffice, leftOffice);
                viewPresident.NumberOfInOfficeDays = this.officerViewCalculator.CalculateNumberOfInOfficeDays(president.TookOffice, leftOffice);

                officerViewList.Add(viewPresident);
            }
            return officerViewList;
        }
    }
}