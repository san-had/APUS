namespace APUS
{
    using APUS.Utils;
    using log4net;
    using log4net.Config;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Reflection;
    using Unity;

    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static IUnityContainer container;

        private static void Main(string[] args)
        {
            Console.WriteLine(Constants.GreetingText);

            LoggerSetup();

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
                    }
                }
            }
        }

        private static void LoggerSetup()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            var logger = LogManager.GetLogger(typeof(Program));

            logger.Info("Hello World");
        }

        private static void Run()
        {
            var reportGenerator = container.Resolve<IReportGenerator>();

            reportGenerator.CreateReport();
        }
    }
}