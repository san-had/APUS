namespace APUS.ViewModels
{
    using APUS.Models;
    using System;
    using System.Collections.Generic;

    public interface IPresidentViewLoader
    {
        IEnumerable<ViewModels.PresidentView> UpdateViewPresidents(IEnumerable<Officer> presidents);
    }
}