namespace APUS
{
    using APUS.Configuration;
    using APUS.Logging;
    using System.IO;
    using Unity;

    public class FileCollectionProcessor
    {
        private IUnityContainer container;

        public void FilesProcessing()
        {
            string[] fileNames = Directory.GetFiles(Constants.DataFilesFolder);

            var recordCollector = RecordCollector.GetInstance();

            foreach (var fileName in fileNames)
            {
                using (container = new UnityContainer())
                {
                    var configurator = new ReportConfigurator(container, fileName);

                    configurator.Setup();

                    if (configurator.IsSuccesfulConfiguration)
                    {
                        var record = new OfficerProcessingRecord();

                        record.FileName = fileName;

                        recordCollector.AddRecord(record);

                        Run();
                    }
                }
            }
            recordCollector.WriteRecordList();
        }

        private void Run()
        {
            var reportGenerator = container.Resolve<IReportGenerator>();

            reportGenerator.CreateReport();
        }
    }
}