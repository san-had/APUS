namespace APUS
{
    using APUS.OutputFormatters;
    using APUS.ViewModels;
    using System;

    public class ReportGenerator : IReportGenerator
    {
        private readonly DataLoader.IDataLoader dataLoader;

        private readonly IPresidentViewLoader presidentViewLoader;

        private readonly IOutputFormatter outputFormatter;

        public ReportGenerator(DataLoader.IDataLoader dataLoader, IPresidentViewLoader presidentViewLoader, IOutputFormatter outputFormatter)
        {
            this.dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader));
            this.presidentViewLoader = presidentViewLoader ?? throw new ArgumentNullException(nameof(presidentViewLoader));
            this.outputFormatter = outputFormatter ?? throw new ArgumentNullException(nameof(outputFormatter));
        }

        public void CreateReport()
        {
            var presidents = dataLoader.LoadData();

            var presidentViewList = this.presidentViewLoader.UpdateViewPresidents(presidents);

            this.outputFormatter.RenderOutput(presidentViewList);
        }
    }
}