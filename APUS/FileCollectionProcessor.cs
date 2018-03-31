﻿namespace APUS
{
    using APUS.Configuration;
    using APUS.Logging;
    using System.IO;
    using Unity;

    public class FileCollectionProcessor
    {
        private IUnityContainer container;

        private IUnityContainer loggerContainer;

        private ILogger logger;

        private ILogger globalLogger;

        private static LogReportInfo logReportInfo;

        public void FilesProcessing()
        {
            using (loggerContainer = new UnityContainer())
            {
                logReportInfo = new LogReportInfo();

                string[] fileNames = Directory.GetFiles(Constants.DataFilesFolder);

                foreach (var fileName in fileNames)
                {
                    using (container = new UnityContainer())
                    {
                        var configurator = new ReportConfigurator(container, fileName);

                        configurator.Setup();

                        if (configurator.IsSuccesfulConfiguration)
                        {
                            Run();
                            //logger = container.Resolve<ILogger>();
                            //logger.Log($"Processed: {fileName}");
                        }
                    }
                }
                Logger.WriteLog("Hello from FileCollectionProcessor");
            }
        }

        private void Run()
        {
            var reportGenerator = container.Resolve<IReportGenerator>();

            reportGenerator.CreateReport();
        }
    }
}