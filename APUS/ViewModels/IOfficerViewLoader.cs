namespace APUS.ViewModels
{
    using APUS.Models;
    using System.Collections.Generic;

    public interface IOfficerViewLoader
    {
        IEnumerable<OfficerView> UpdateViewOfficers(IEnumerable<Officer> presidents);
    }
}