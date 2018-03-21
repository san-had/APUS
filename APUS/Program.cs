namespace APUS
{
    using APUS.ViewModels.Calculation;
    using log4net;
    using log4net.Config;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Unity;
    using Unity.Injection;

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
                    bool isSuccess = Setup(fileName);

                    if (isSuccess)
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

        private static bool Setup(string fileName)
        {
            bool isSuccess = DataAccessConfiguration(fileName);

            if (!isSuccess)
            {
                return false;
            }

            DataLoaderConfiguration();

            ViewModelLoaderConfiguration();

            OutputFormatterConfiguration();

            ReportGeneratorConfiguration();

            return isSuccess;
        }

        private static bool DataAccessConfiguration(string fileName)
        {
            var dictionary = GetDataAccessType(fileName);

            if (dictionary.Count > 0)
            {
                container.RegisterType(dictionary.Keys.First(), dictionary.Values.First(), new InjectionConstructor(fileName));
            }
            else
            {
                return false;
            }

            var dataAccessPluginName = dictionary.Values.First().Assembly.FullName.Split(',')[0].Trim();

            if (dataAccessPluginName.EndsWith("En"))
            {
                container.RegisterType<DataLoader.IDateParser, DataLoader.EnDateParser>();
            }
            else if (dataAccessPluginName.EndsWith("Us"))
            {
                container.RegisterType<DataLoader.IDateParser, DataLoader.UsDateParser>();
            }
            else if (dataAccessPluginName.EndsWith("Utc"))
            {
                container.RegisterType<DataLoader.IDateParser, DataLoader.UTCDateTimeParser>();
            }
            else
            {
                throw new NotImplementedException($"No available DateTime parser for this dataAccessType: {dataAccessPluginName}");
            }

            DisplayContainerRegistrations();

            return true;
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

        private static Dictionary<Type, Type> GetDataAccessType(string fileName)
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

            var pluginSelector = new PluginSelector();
            typeTo = pluginSelector.GetSelected(fileName, types);

            var mapping = new Dictionary<Type, Type>();

            if (typeFrom != null && typeTo != null)
            {
                mapping.Add(typeFrom, typeTo);
            }

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