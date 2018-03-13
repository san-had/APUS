namespace APUS.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using APUS.Models;

    public class OfficerViewModelLoaderFirstFormat : IOfficerViewModelLoader
    {
        private IOfficerViewCalculator officerViewCalculator;

        public IOfficerViewCalculator OfficerViewCalculator
        {
            get
            {
                if (officerViewCalculator == null)
                {
                    throw new NullReferenceException(nameof(officerViewCalculator));
                }

                return officerViewCalculator;
            }
            set { officerViewCalculator = value; }
        }

        public OfficerViewModel UpdateViewOfficerModel(IEnumerable<Officer> officers)
        {
            var officerViewModel = new OfficerViewModel();

            officerViewModel.OfficerViewHeader = CreateHeader();

            var officerViewList = new List<OfficerView>();

            if (officers == null)
            {
                officerViewModel.OfficerViewRows = Enumerable.Empty<OfficerView>();
            }
            else
            {
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

                officerViewModel.OfficerViewRows = officerViewList;
            }

            return officerViewModel;
        }

        public IEnumerable<string> CreateHeader()
        {
            yield return "last name";
            yield return "first name";
            yield return "in-office range";
            yield return "# of in-office days";
        }
    }
}