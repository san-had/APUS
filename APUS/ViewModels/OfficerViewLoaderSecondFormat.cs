namespace APUS.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using APUS.Models;

    public class OfficerViewLoaderSecondFormat : IOfficerViewLoader
    {
        private IOfficerViewCalculator officerViewCalculator;

        public OfficerViewLoaderSecondFormat(IOfficerViewCalculator officerViewCalculator)
        {
            this.officerViewCalculator = officerViewCalculator ?? throw new ArgumentNullException(nameof(officerViewCalculator));
        }

        public IEnumerable<OfficerView> UpdateViewOfficers(IEnumerable<Officer> officers)
        {
            if (officers == null)
            {
                return Enumerable.Empty<OfficerView>();
            }

            var officerViewList = new List<OfficerView>();

            foreach (var officer in officers)
            {
                var officerView = new ViewModels.OfficerView();

                officerView.Col2 = officer.FirstName;
                officerView.Col1 = officer.LastName.ToUpper();
                officerView.Col3 = officer.TookOffice.HasValue ? officer.TookOffice.Value.Year.ToString() : "n.a";
                officerView.Col4 = officer.LeftOffice.HasValue ? officer.LeftOffice.Value.Year.ToString() : "n.a";

                officerViewList.Add(officerView);
            }
            return officerViewList;
        }
    }
}