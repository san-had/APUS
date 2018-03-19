namespace APUS
{
    using APUS.ViewModels.Calculation;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Unity;
    using Unity.Injection;

    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static IUnityContainer container;

        private static void Main(string[] args)
        {
            Console.WriteLine(Constants.GreetingText);

            using (container = new UnityContainer())
            {
                Setup();

                Run();
            }
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
            var dictionary = GetDataAccessType();

            container.RegisterType(dictionary.Keys.First(), dictionary.Values.First());

            var dataAccessPluginName = dictionary.Values.First().Assembly.FullName.Split(',')[0].Trim();

            switch (dataAccessPluginName)
            {
                case "CsvPresidentDataAccess":
                    container.RegisterType<DataLoader.IDateParser, DataLoader.EnDateParser>();
                    break;

                case "Csv2PresidentDataAccess":
                    container.RegisterType<DataLoader.IDateParser, DataLoader.UsDateParser>();
                    break;

                case "JsonMayorDataAccess":
                    container.RegisterType<DataLoader.IDateParser, DataLoader.UTCDateTimeParser>();
                    break;

                default:
                    throw new NotImplementedException($"Invalid dataAccessType: {dataAccessPluginName}");
            }

            DisplayContainerRegistrations();
        }

        private static void DataLoaderConfiguration()
        {
            container.RegisterType<DataLoader.IOfficerDataMapper, DataLoader.OfficerDataMapper>(new InjectionConstructor(typeof(DataLoader.IDateParser)));
            container.RegisterType<DataLoader.IDataLoader, DataLoader.DataLoader>(new InjectionConstructor(typeof(CommonDataAccess.ICommonDataAccess), typeof(DataLoader.IOfficerDataMapper)));
        }

        private static void ViewModelLoaderConfiguration()
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
            container.RegisterType<IReportGenerator, ReportGenerator>(new InjectionConstructor(typeof(DataLoader.IDataLoader), typeof(ViewModels.IOfficerViewModelDataMapper), typeof(OutputFormatters.IOutputFormatter)));
        }

        private static void Run()
        {
            var reportGenerator = container.Resolve<IReportGenerator>();

            reportGenerator.CreateReport();
        }

        private static Dictionary<Type, Type> GetDataAccessType()
        {
            Type typeFrom = null;

            Type typeTo = null;

            var pluginExplorer = new PluginExplorer();

            var typeExploreredCollection = pluginExplorer.GetPlugins(Constants.DataAccessPluginFolder);

            var types = new List<Type>();

            foreach (var items in typeExploreredCollection)
            {
                typeFrom = items.Key;

                foreach (var item in items.Value)
                {
                    types.Add(item);
                }
            }

            var pluginMenu = new PluginMenu();
            pluginMenu.DisplayMenu(types);

            typeTo = pluginMenu.GetChoise(types);

            var mapping = new Dictionary<Type, Type>();

            mapping.Add(typeFrom, typeTo);

            return mapping;
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

        private static void DisplayContainerRegistrations()
        {
            Console.WriteLine($"Container has {container.Registrations.ToList().Count()} Registrations: ");
            foreach (var registration in container.Registrations)
            {
                Console.WriteLine(registration.GetMappingAsString());
            }
        }
    }
}