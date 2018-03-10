namespace APUS
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;
    using System.Linq;

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

            var officerViewLoaderTypeName = Configuration["officerViewLoader"];
            var officerViewLoaderType = Type.GetType(officerViewLoaderTypeName, true);

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

            ViewModels.IOfficerViewLoader officerViewLoader = (ViewModels.IOfficerViewLoader)Activator.CreateInstance(officerViewLoaderType, new object[] { officerViewCalculator });

            OutputFormatters.IConsoleWriter consoleWriter = (OutputFormatters.IConsoleWriter)Activator.CreateInstance(consoleWriterType);

            OutputFormatters.IOutputFormatter outputFormatter = (OutputFormatters.IOutputFormatter)Activator.CreateInstance(outputFormatterType, new object[] { consoleWriter });

            IReportGenerator reportGenerator = (IReportGenerator)Activator.CreateInstance(reportGeneratorType, new object[] { dataLoader, officerViewLoader, outputFormatter });

            reportGenerator.CreateReport();
        }

        private static int GetDataAccessType()
        {
            int dataAccessNumber = 0;
            var validChoices = new int[] { 1, 2, 3 };
            bool isParsed = false;

            Console.WriteLine();
            Console.WriteLine("1. CsvDataAccess");
            Console.WriteLine("2. Csv2DataAccess");
            Console.WriteLine("3. JsonDataAccess");
            Console.WriteLine();
            Console.Write("Your choice: ");

            while (!validChoices.Contains(dataAccessNumber) || !isParsed)
            {
                var readString = Console.ReadLine();
                isParsed = int.TryParse(readString.Trim(), out dataAccessNumber);
            }

            return dataAccessNumber;
        }

        private static int GetOutputFormat()
        {
            int formatNumber = 0;
            var validChoices = new int[] { 1, 2 };
            bool isParsed = false;

            Console.WriteLine();
            Console.WriteLine("1. Standard");
            Console.WriteLine("2. Table");
            Console.WriteLine();
            Console.Write("Your choice: ");

            while (!validChoices.Contains(formatNumber) || !isParsed)
            {
                var readString = Console.ReadLine();
                isParsed = int.TryParse(readString.Trim(), out formatNumber);
            }

            return formatNumber;
        }
    }
}