namespace APUS.Configuration
{
    using APUS.Utils;
    using APUS.ViewModels.Calculation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Unity;
    using Unity.Injection;

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

            DataLoaderConfiguration();

            ViewModelLoaderConfiguration();

            OutputFormatterConfiguration();

            ReportGeneratorConfiguration();

            logger = container.Resolve<ILogger>();

            logger.Log(fileName);
        }

        private void DataLoaderConfiguration()
        {
            container.RegisterType<DataLoader.IOfficerDataMapper, DataLoader.OfficerDataMapper>(new InjectionConstructor(typeof(DataLoader.IDateParser)));
            container.RegisterType<DataLoader.IDataLoader, DataLoader.DataLoader>(new InjectionConstructor(typeof(CommonDataAccess.ICommonDataAccess), typeof(DataLoader.IOfficerDataMapper)));
        }

        private void ViewModelLoaderConfiguration()
        {
            container.RegisterType<IInOfficeDaysCalculator, InOfficeDaysCalculator>();
            container.RegisterType<IInOfficeRangeComposer, InOfficeRangeComposer>();
            container.RegisterType<ILeftOfficeParser, LeftOfficeParser>();

            int viewFormatNumber = GetViewFormat();

            switch (viewFormatNumber)
            {
                case 1:
                    container.RegisterType<ViewModels.IOfficerViewModelDataMapper, ViewModels.OfficerViewModelLoaderFirstFormat>
                        (
                            new InjectionProperty("InOfficeDaysCalculator"),
                            new InjectionProperty("InOfficeRangeComposer"),
                            new InjectionProperty("LeftOfficeParser"));
                    break;

                case 2:
                    container.RegisterType<ViewModels.IOfficerViewModelDataMapper, ViewModels.OfficerViewModelLoaderSecondFormat>();
                    break;

                default:
                    throw new NotImplementedException($"Invalid viewFormatNumber: {viewFormatNumber.ToString()}");
            }
        }

        private void OutputFormatterConfiguration()
        {
            int outputFormatTypeNumber = GetOutputFormat();

            switch (outputFormatTypeNumber)
            {
                case 1:
                    container.RegisterType<OutputFormatters.IOutputFormatter, OutputFormatters.StdOutputFormatter>(new InjectionConstructor(typeof(OutputFormatters.IConsoleWriter)));
                    break;

                case 2:
                    container.RegisterType<OutputFormatters.IOutputFormatter, OutputFormatters.ConsoleTableOutputFormatter>(new InjectionConstructor(typeof(OutputFormatters.IConsoleWriter)));
                    break;

                default:
                    throw new NotImplementedException($"Invalid outputFormatTypeNumber: {outputFormatTypeNumber.ToString()}");
            }

            container.RegisterType<OutputFormatters.IConsoleWriter, OutputFormatters.ConsoleWriter>();
        }

        private void ReportGeneratorConfiguration()
        {
            container.RegisterType<IReportGenerator, ReportGenerator>(new InjectionConstructor(typeof(DataLoader.IDataLoader), typeof(ViewModels.IOfficerViewModelDataMapper), typeof(OutputFormatters.IOutputFormatter)));
        }

        private int GetOutputFormat()
        {
            var menuDictionary = new Dictionary<int, string>()
            {
                {1, "Standard" },
                {2, "Table" }
            };

            var menu = new Menu();
            menu.DisplayMenu(menuDictionary);
            return menu.GetChoise(menuDictionary);
        }

        private int GetViewFormat()
        {
            var menuDictionary = new Dictionary<int, string>()
            {
                {1, "First Format" },
                {2, "Second Format" }
            };

            var menu = new Menu();
            menu.DisplayMenu(menuDictionary);
            return menu.GetChoise(menuDictionary);
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