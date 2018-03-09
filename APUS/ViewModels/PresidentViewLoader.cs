namespace APUS.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using APUS.Models;

    public class PresidentViewLoader : IPresidentViewLoader
    {
        private IPresidentViewCalculator presidentViewCalculator;

        public PresidentViewLoader(IPresidentViewCalculator presidentViewCalculator)
        {
            if (presidentViewCalculator == null)
            {
                throw new ArgumentNullException(nameof(presidentViewCalculator));
            }

            this.presidentViewCalculator = presidentViewCalculator;
        }

        public IEnumerable<ViewModels.PresidentView> UpdateViewPresidents(IEnumerable<Officer> presidents)
        {
            if (presidents == null)
            {
                return Enumerable.Empty<PresidentView>();
            }

            var presidentViewList = new List<ViewModels.PresidentView>();

            foreach (var president in presidents)
            {
                var viewPresident = new ViewModels.PresidentView();

                DateTime? leftOffice = this.presidentViewCalculator.LeftOfficeParser(president.LeftOffice);

                viewPresident.FirstName = president.FirstName;
                viewPresident.LastName = president.LastName;
                viewPresident.PresidencyRange = this.presidentViewCalculator.GetPresidencyRange(president.TookOffice, leftOffice);
                viewPresident.NumberOfPresidencyDays = this.presidentViewCalculator.CalculateNumberOfPresidencyDays(president.TookOffice, leftOffice);

                presidentViewList.Add(viewPresident);
            }
            return presidentViewList;
        }
    }
}