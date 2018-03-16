namespace APUS.ViewModels
{
    using APUS.Models;
    using System.Collections.Generic;

    public interface IOfficerViewModelDataMapper
    {
        OfficerViewModel MapDomainData(IEnumerable<Officer> officers);
    }
}