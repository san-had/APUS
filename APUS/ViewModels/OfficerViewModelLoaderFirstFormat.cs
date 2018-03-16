namespace APUS.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using APUS.Models;
    using APUS.ViewModels.Calculation;

    public class OfficerViewModelLoaderFirstFormat : IOfficerViewModelDataMapper
    {
        private IInOfficeDaysCalculator inOfficeDaysCalculator;

        public IInOfficeDaysCalculator InOfficeDaysCalculator
        {
            get
            {
                if (inOfficeDaysCalculator == null)
                {
                    throw new NullReferenceException(nameof(inOfficeDaysCalculator));
                }

                return inOfficeDaysCalculator;
            }
            set { inOfficeDaysCalculator = value; }
        }

        private IInOfficeRangeComposer inOfficeRangeComposer;

        public IInOfficeRangeComposer InOfficeRangeComposer
        {
            get
            {
                if (inOfficeRangeComposer == null)
                {
                    throw new NullReferenceException(nameof(inOfficeRangeComposer));
                }

                return inOfficeRangeComposer;
            }
            set { inOfficeRangeComposer = value; }
        }

        private ILeftOfficeParser leftOfficeParser;

        public ILeftOfficeParser LeftOfficeParser
        {
            get
            {
                if (leftOfficeParser == null)
                {
                    throw new NullReferenceException(nameof(leftOfficeParser));
                }

                return leftOfficeParser;
            }
            set { leftOfficeParser = value; }
        }

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

                    DateTime? leftOffice = this.leftOfficeParser.ParseLeftOffice(officer.LeftOffice);

                    officerView.Col2 = officer.FirstName;
                    officerView.Col1 = officer.LastName.ToUpper();
                    officerView.Col3 = this.inOfficeRangeComposer.GetInOfficeRange(officer.TookOffice, leftOffice);
                    officerView.Col4 = $"{this.inOfficeDaysCalculator.CalculateNumberOfInOfficeDays(officer.TookOffice, leftOffice).ToString()} days";

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