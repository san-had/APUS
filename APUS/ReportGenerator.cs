namespace APUS
{
    using APUS.Logging;
    using APUS.OutputFormatters;
    using APUS.ViewModels;
    using System;

    public class ReportGenerator : IReportGenerator, ILogging
    {
        private readonly DataLoader.IDataLoader dataLoader;

        private readonly IOfficerViewModelDataMapper officerViewModelLoader;

        private readonly IOutputFormatter outputFormatter;

        private readonly ILogEntry logEntry;

        public ReportGenerator(DataLoader.IDataLoader dataLoader, IOfficerViewModelDataMapper officerViewModelLoader, IOutputFormatter outputFormatter, ILogEntry logEntry)
        {
            this.dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader));
            this.officerViewModelLoader = officerViewModelLoader ?? throw new ArgumentNullException(nameof(officerViewModelLoader));
            this.outputFormatter = outputFormatter ?? throw new ArgumentNullException(nameof(outputFormatter));
            this.logEntry = logEntry ?? throw new ArgumentNullException(nameof(logEntry));
        }

        public void CreateReport()
        {
            var officers = dataLoader.LoadData();

            var officerViewModel = this.officerViewModelLoader.MapDomainData(officers);

            this.outputFormatter.RenderOutput(officerViewModel);

            WriteLog();
        }

        public void WriteLog()
        {
            this.logEntry.OutputFormatter = outputFormatter.GetType().Name;
            this.logEntry.ViewModelFormat = officerViewModelLoader.GetType().Name;
            var logger = Logger.GetInstance();
            logger.UpdateLastLogEntry(logEntry);
        }
    }
}