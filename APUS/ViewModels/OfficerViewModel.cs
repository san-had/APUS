namespace APUS.ViewModels
{
    using System.Collections.Generic;

    public class OfficerViewModel
    {
        public IEnumerable<string> OfficerViewHeader { get; set; }

        public IEnumerable<OfficerView> OfficerViewRows { get; set; }
    }
}