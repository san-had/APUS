namespace APUS
{
    using APUS.OutputFormatters;
    using APUS.ViewModels;
    using System;

    public class ReportGenerator : IReportGenerator
    {
        private readonly DataLoader.IDataLoader dataLoader;

        private readonly IOfficerViewModelLoader officerViewModelLoader;

        private readonly IOutputFormatter outputFormatter;

        public ReportGenerator(DataLoader.IDataLoader dataLoader, IOfficerViewModelLoader officerViewModelLoader, IOutputFormatter outputFormatter)
        {
            this.dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader));
            this.officerViewModelLoader = officerViewModelLoader ?? throw new ArgumentNullException(nameof(officerViewModelLoader));
            this.outputFormatter = outputFormatter ?? throw new ArgumentNullException(nameof(outputFormatter));
        }

        public void CreateReport()
        {
            var officers = dataLoader.LoadData();

            var officerViewModel = this.officerViewModelLoader.UpdateViewOfficerModel(officers);

            this.outputFormatter.RenderOutput(officerViewModel);
        }
    }
}