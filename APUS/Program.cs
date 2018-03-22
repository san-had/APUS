namespace APUS
{
    using APUS.Configuration;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Unity;

    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static IUnityContainer container;

        private static void Main(string[] args)
        {
            Console.WriteLine(Constants.GreetingText);

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

        private static void Run()
        {
            var reportGenerator = container.Resolve<IReportGenerator>();

            reportGenerator.CreateReport();
        }
    }
}