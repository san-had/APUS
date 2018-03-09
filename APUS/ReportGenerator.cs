namespace APUS
{
    using APUS.OutputFormatters;
    using APUS.ViewModels;
    using System;

    public class ReportGenerator : IReportGenerator
    {
        private readonly DataLoader.IDataLoader dataLoader;

        private readonly IOfficerViewLoader officerViewLoader;

        private readonly IOutputFormatter outputFormatter;

        public ReportGenerator(DataLoader.IDataLoader dataLoader, IOfficerViewLoader officerViewLoader, IOutputFormatter outputFormatter)
        {
            this.dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader));
            this.officerViewLoader = officerViewLoader ?? throw new ArgumentNullException(nameof(officerViewLoader));
            this.outputFormatter = outputFormatter ?? throw new ArgumentNullException(nameof(outputFormatter));
        }

        public void CreateReport()
        {
            var officers = dataLoader.LoadData();

            var officerViewList = this.officerViewLoader.UpdateViewOfficers(officers);

            this.outputFormatter.RenderOutput(officerViewList);
        }
    }
}