namespace APUS.Configuration
{
    using APUS.Logging;
    using APUS.Utils;
    using System;
    using System.Linq;
    using Unity;

    public class ReportConfigurator : IReportConfigurator
    {
        private IUnityContainer container;

        private string fileName;

        private ILogger logger;

        public bool IsSuccesfulConfiguration { get; set; } = false;

        public ReportConfigurator(IUnityContainer container, string fileName)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));

            if (!string.IsNullOrEmpty(fileName))
            {
                this.fileName = fileName;
            }
            else
            {
                throw new ArgumentNullException(nameof(fileName));
            }
        }

        public void Setup()
        {
            var loggerConfiguration = new LoggerConfiguration(container);
            loggerConfiguration.Configure();

            var dataAccessConfiguration = new DataAccessConfiguration(container, fileName);
            dataAccessConfiguration.Configure();

            IsSuccesfulConfiguration = dataAccessConfiguration.IsSuccesfulDataConfiguration;

            if (!IsSuccesfulConfiguration)
            {
                return;
            }

            var dataLoaderConfiguration = new DataLoaderConfiguration(container);
            dataLoaderConfiguration.Configure();

            var viewModelLoaderConfiguration = new ViewModelLoaderConfiguration(container);
            viewModelLoaderConfiguration.Configure();

            var outputFormatterConfiguration = new OutputFormatterConfiguration(container);
            outputFormatterConfiguration.Configure();

            var reportGeneratorConfiguration = new ReportGeneratorConfiguration(container);
            reportGeneratorConfiguration.Configure();
        }

        private void DisplayContainerRegistrations()
        {
            Console.WriteLine($"Container has {container.Registrations.ToList().Count()} Registrations: ");
            foreach (var registration in container.Registrations)
            {
                Console.WriteLine(registration.GetMappingAsString());
            }
        }
    }
}