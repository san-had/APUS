namespace APUS.ViewModels
{
    using APUS.Models;
    using System.Collections.Generic;

    public interface IOfficerViewModelLoader
    {
        OfficerViewModel UpdateViewOfficerModel(IEnumerable<Officer> officers);
    }
}