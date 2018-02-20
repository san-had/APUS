namespace APUS.ViewModels
{
    using APUS.Models;
    using System;
    using System.Collections.Generic;

    public interface IPresidentViewLoader
    {
        IEnumerable<ViewModels.PresidentView> UpdateViewPresidents(IEnumerable<President> presidents);

        int CalculateNumberOfPresidencyDays(DateTime? tookOffice, DateTime? leftOffice);

        string GetPresidencyRange(DateTime? tookOffice, DateTime? leftOffice);
    }
}