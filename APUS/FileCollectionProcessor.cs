namespace APUS
{
    using APUS.Configuration;
    using APUS.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Unity;

    public class FileCollectionProcessor
    {
        private IUnityContainer container;

        private ILogger logger;

        public void FilesProcessing()
        {
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
                        logger = container.Resolve<ILogger>();
                        logger.Log($"Processed: {fileName}");
                    }
                }
            }
        }

        private void Run()
        {
            var reportGenerator = container.Resolve<IReportGenerator>();

            reportGenerator.CreateReport();
        }
    }
}