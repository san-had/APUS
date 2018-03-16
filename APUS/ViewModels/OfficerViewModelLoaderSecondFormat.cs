namespace APUS.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using APUS.Models;
    using APUS.ViewModels.Calculation;

    public class OfficerViewModelLoaderSecondFormat : IOfficerViewModelDataMapper
    {
        public OfficerViewModel MapDomainData(IEnumerable<Officer> officers)
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

                    officerView.Col1 = officer.LastName.ToUpper();
                    officerView.Col2 = officer.FirstName;
                    officerView.Col3 = officer.TookOffice.HasValue ? officer.TookOffice.Value.Year.ToString() : Constants.NAString;
                    officerView.Col4 = officer.LeftOffice.HasValue ? officer.LeftOffice.Value.Year.ToString() : Constants.NALeftOfficeString;

                    officerViewList.Add(officerView);
                }

                officerViewModel.OfficerViewRows = officerViewList;
            }

            return officerViewModel;
        }

        public IEnumerable<string> CreateHeader()
        {
            yield return "Last Name";
            yield return "First Name";
            yield return "Took Office Year";
            yield return "Left Office Year";
        }
    }
}