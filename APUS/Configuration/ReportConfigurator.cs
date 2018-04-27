namespace APUS.Configuration
{
    using APUS.Logging;
    using APUS.Utils;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Unity;

    public class ReportConfigurator : IReportConfigurator
    {
        private IUnityContainer container;

        private string fileName;

        private IMenu menu;

        private ILogging logger;

        [ExcludeFromCodeCoverage]
        public bool IsSuccesfulConfiguration { get; set; } = false;

        public ReportConfigurator(IUnityContainer container, string fileName, IMenu menu)
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

            this.menu = menu ?? throw new ArgumentNullException(nameof(menu));
        }

        public void Setup()
        {
            var dataAccessConfiguration = new DataAccessConfiguration(container, fileName);
            dataAccessConfiguration.Configure();

            IsSuccesfulConfiguration = dataAccessConfiguration.IsSuccesfulDataConfiguration;

            if (!IsSuccesfulConfiguration)
            {
                return;
            }

            var dataLoaderConfiguration = new DataLoaderConfiguration(container);
            dataLoaderConfiguration.Configure();

            var viewModelLoaderConfiguration = new ViewModelLoaderConfiguration(container, menu);
            viewModelLoaderConfiguration.Configure();

            var outputFormatterConfiguration = new OutputFormatterConfiguration(container, menu);
            outputFormatterConfiguration.Configure();

            var reportGeneratorConfiguration = new ReportGeneratorConfiguration(container);
            reportGeneratorConfiguration.Configure();
        }

        [ExcludeFromCodeCoverage]
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