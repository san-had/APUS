namespace APUS
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class Program
    {
        public static IConfiguration Configuration { get; set; }

        private static void Main(string[] args)
        {
            Console.WriteLine(Constants.GreetingText);

            Run();
        }

        private static void Run()
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            int dataAccessTypeNumber = GetDataAccessType();

            string dataAccessString = ((DataAccesType)dataAccessTypeNumber).ToString();

            var dataAccessTypeName = Configuration[dataAccessString];
            var dataAccessType = Type.GetType(dataAccessTypeName, true);

            string dateParserString;
            switch (dataAccessTypeNumber)
            {
                case 1:
                    dateParserString = "enDateParser";
                    break;

                case 2:
                    dateParserString = "usDateParser";
                    break;

                case 3:
                    dateParserString = "utcDateTimeParser";
                    break;

                default:
                    throw new NotImplementedException(dataAccessTypeNumber.ToString());
            }

            var dateParserTypeName = Configuration[dateParserString];
            var dateParserType = Type.GetType(dateParserTypeName, true);

            var mapperTypeName = Configuration["mapper"];
            var mapperType = Type.GetType(mapperTypeName, true);

            var dataLoaderTypeName = Configuration["dataLoader"];
            var dataLoaderType = Type.GetType(dataLoaderTypeName, true);

            var officerViewCalculatorTypeName = Configuration["officerViewCalculator"];
            var officerViewCalculatorType = Type.GetType(officerViewCalculatorTypeName, true);

            int viewFormatNumber = GetViewFormat();
            string viewFormatString = ((ViewFormatType)viewFormatNumber).ToString();

            var officerViewModelLoaderTypeName = Configuration[viewFormatString];
            var officerViewModelLoaderType = Type.GetType(officerViewModelLoaderTypeName, true);

            var consoleWriterTypeName = Configuration["consoleWriter"];
            var consoleWriterType = Type.GetType(consoleWriterTypeName, true);

            int outputFormatTypeNumber = GetOutputFormat();

            string outputFormatString = ((OutputFormatterType)outputFormatTypeNumber).ToString();

            var outputFormatterTypeName = Configuration[outputFormatString];
            var outputFormatterType = Type.GetType(outputFormatterTypeName, true);

            var reportGeneratorTypeName = Configuration["reportGenerator"];
            var reportGeneratorType = Type.GetType(reportGeneratorTypeName, true);

            CommonDataAccess.ICommonDataAccess dataAccess = (CommonDataAccess.ICommonDataAccess)Activator.CreateInstance(dataAccessType);

            DataLoader.IDateParser dateParser = (DataLoader.IDateParser)Activator.CreateInstance(dateParserType);

            DataLoader.IOfficerDataMapper mapper = (DataLoader.IOfficerDataMapper)Activator.CreateInstance(mapperType, new object[] { dateParser });

            DataLoader.IDataLoader dataLoader = (DataLoader.IDataLoader)Activator.CreateInstance(dataLoaderType, new object[] { dataAccess, mapper });

            ViewModels.IOfficerViewCalculator officerViewCalculator = (ViewModels.IOfficerViewCalculator)Activator.CreateInstance(officerViewCalculatorType);

            ViewModels.IOfficerViewModelLoader officerViewModelLoader = (ViewModels.IOfficerViewModelLoader)Activator.CreateInstance(officerViewModelLoaderType, new object[] { officerViewCalculator });

            OutputFormatters.IConsoleWriter consoleWriter = (OutputFormatters.IConsoleWriter)Activator.CreateInstance(consoleWriterType);

            OutputFormatters.IOutputFormatter outputFormatter = (OutputFormatters.IOutputFormatter)Activator.CreateInstance(outputFormatterType, new object[] { consoleWriter });

            IReportGenerator reportGenerator = (IReportGenerator)Activator.CreateInstance(reportGeneratorType, new object[] { dataLoader, officerViewModelLoader, outputFormatter });

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