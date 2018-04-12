namespace APUS
{
    using APUS.Logging;
    using APUS.OutputFormatters;
    using APUS.ViewModels;
    using System;
    using System.Runtime.CompilerServices;

    public class ReportGenerator : IReportGenerator, ILogging
    {
        private readonly DataLoader.IDataLoader dataLoader;

        private readonly IOfficerViewModelDataMapper officerViewModelLoader;

        private readonly IOutputFormatter outputFormatter;

        public ReportGenerator(DataLoader.IDataLoader dataLoader, IOfficerViewModelDataMapper officerViewModelLoader, IOutputFormatter outputFormatter)
        {
            this.dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader));
            this.officerViewModelLoader = officerViewModelLoader ?? throw new ArgumentNullException(nameof(officerViewModelLoader));
            this.outputFormatter = outputFormatter ?? throw new ArgumentNullException(nameof(outputFormatter));
        }

        public void CreateReport()
        {
            var officers = dataLoader.LoadData();

            var officerViewModel = this.officerViewModelLoader.MapDomainData(officers);

            this.outputFormatter.RenderOutput(officerViewModel);

            WriteLog();
        }

        public void WriteLog([CallerMemberName] string callerName = null)
        {
            var record = new OfficerProcessingRecord();
            record.OutputFormatter = outputFormatter.GetType().Name;
            record.ViewModelFormat = officerViewModelLoader.GetType().Name;
            record.ReportGenCaller = callerName;
            record.ReportGenRecordingTime = DateTime.Now.ToString(Constants.LogDateTimeFormat);
            var recordCollector = RecordCollector.GetInstance();
            recordCollector.UpdateLastRecord(record);
        }
    }
}