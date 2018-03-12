namespace APUS.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using APUS.Models;

    public class OfficerViewLoaderFirstFormat : IOfficerViewLoader
    {
        private IOfficerViewCalculator officerViewCalculator;

        public OfficerViewLoaderFirstFormat(IOfficerViewCalculator officerViewCalculator)
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

            foreach (var officer in officers)
            {
                var officerView = new ViewModels.OfficerView();

                DateTime? leftOffice = this.officerViewCalculator.LeftOfficeParser(officer.LeftOffice);

                officerView.Col2 = officer.FirstName;
                officerView.Col1 = officer.LastName.ToUpper();
                officerView.Col3 = this.officerViewCalculator.GetInOfficeRange(officer.TookOffice, leftOffice);
                officerView.Col4 = $"{this.officerViewCalculator.CalculateNumberOfInOfficeDays(officer.TookOffice, leftOffice).ToString()} days";

                officerViewList.Add(officerView);
            }
            return officerViewList;
        }
    }
}