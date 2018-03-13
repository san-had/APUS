namespace APUS
{
    using System;
    using System.Collections.Generic;
    using Unity;
    using Unity.Injection;

    internal class Program
    {
        private static IUnityContainer container = new UnityContainer();

        private static void Main(string[] args)
        {
            Console.WriteLine(Constants.GreetingText);

            Setup();

            Run();
        }

        private static void Setup()
        {
            DataAccessConfiguration();

            DataLoaderConfiguration();

            ViewModelLoaderConfiguration();

            OutputFormatterConfiguration();

            ReportGeneratorConfiguration();
        }

        private static void DataAccessConfiguration()
        {
            int dataAccessTypeNumber = GetDataAccessType();

            switch (dataAccessTypeNumber)
            {
                case 1:
                    container.RegisterType<CommonDataAccess.ICommonDataAccess, DataAccess.CsvPresidentDataAccess>();
                    container.RegisterType<DataLoader.IDateParser, DataLoader.EnDateParser>();
                    break;

                case 2:
                    container.RegisterType<CommonDataAccess.ICommonDataAccess, DataAccess.Csv2PresidentDataAccess>();
                    container.RegisterType<DataLoader.IDateParser, DataLoader.UsDateParser>();
                    break;

                case 3:
                    container.RegisterType<CommonDataAccess.ICommonDataAccess, DataAccess.JsonMayorDataAccess>();
                    container.RegisterType<DataLoader.IDateParser, DataLoader.UTCDateTimeParser>();
                    break;

                default:
                    throw new NotImplementedException($"Invalid dataAccessTypeNumber: {dataAccessTypeNumber.ToString()}");
            }
        }

        private static void DataLoaderConfiguration()
        {
            container.RegisterType<DataLoader.IOfficerDataMapper, DataLoader.OfficerDataMapper>(new InjectionConstructor(typeof(DataLoader.IDateParser)));
            container.RegisterType<DataLoader.IDataLoader, DataLoader.DataLoader>(new InjectionConstructor(typeof(CommonDataAccess.ICommonDataAccess), typeof(DataLoader.IOfficerDataMapper)));
        }

        private static void ViewModelLoaderConfiguration()
        {
            container.RegisterType<ViewModels.IOfficerViewCalculator, ViewModels.OfficerViewCalculator>();

            int viewFormatNumber = GetViewFormat();

            switch (viewFormatNumber)
            {
                case 1:
                    container.RegisterType<ViewModels.IOfficerViewModelLoader, ViewModels.OfficerViewModelLoaderFirstFormat>(new InjectionProperty("OfficerViewCalculator"));
                    break;

                case 2:
                    container.RegisterType<ViewModels.IOfficerViewModelLoader, ViewModels.OfficerViewModelLoaderSecondFormat>();
                    break;

                default:
                    throw new NotImplementedException($"Invalid viewFormatNumber: {viewFormatNumber.ToString()}");
            }
        }

        private static void OutputFormatterConfiguration()
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

        private static void ReportGeneratorConfiguration()
        {
            container.RegisterType<IReportGenerator, ReportGenerator>(new InjectionConstructor(typeof(DataLoader.IDataLoader), typeof(ViewModels.IOfficerViewModelLoader), typeof(OutputFormatters.IOutputFormatter)));
        }

        private static void Run()
        {
            var reportGenerator = container.Resolve<IReportGenerator>();

            reportGenerator.CreateReport();
        }

        private static int GetDataAccessType()
        {
            var menuDictionary = new Dictionary<int, string>()
            {
                {1, "CsvDataAccess" },
                {2, "Csv2DataAccess" },
                {3, "JsonDataAccess" }
            };

            var menu = new Menu();
            menu.DisplayMenu(menuDictionary);
            return menu.GetChoise(menuDictionary);
        }

        private static int GetOutputFormat()
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

        private static int GetViewFormat()
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
    }
}