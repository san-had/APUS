namespace APUS
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;
    using System.Linq;

    internal class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static string dataAccess;

        private static void Main(string[] args)
        {
            Console.WriteLine(Constants.GreetingText);

            StartUp();

            Run();
        }

        private static void StartUp()
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperPresidentProfile>());

            AutoMapper.Mapper.AssertConfigurationIsValid();
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

            var presidentViewCalculatorTypeName = Configuration["presidentViewCalculator"];
            var presidentViewCalculatorType = Type.GetType(presidentViewCalculatorTypeName, true);

            var presidentViewLoaderTypeName = Configuration["presidentViewLoader"];
            var presidentViewLoaderType = Type.GetType(presidentViewLoaderTypeName, true);

            var consoleWriterTypeName = Configuration["consoleWriter"];
            var consoleWriterType = Type.GetType(consoleWriterTypeName, true);

            var outputFormatterTypeName = Configuration["outputFormatter"];
            var outputFormatterType = Type.GetType(outputFormatterTypeName, true);

            var reportGeneratorTypeName = Configuration["reportGenerator"];
            var reportGeneratorType = Type.GetType(reportGeneratorTypeName, true);

            DataAccess.IDataAccess dataAccess = (DataAccess.IDataAccess)Activator.CreateInstance(dataAccessType);

            ViewModels.IPresidentViewCalculator presidentViewCalculator = (ViewModels.IPresidentViewCalculator)Activator.CreateInstance(presidentViewCalculatorType);

            ViewModels.IPresidentViewLoader presidentViewLoader = (ViewModels.IPresidentViewLoader)Activator.CreateInstance(presidentViewLoaderType, new object[] { presidentViewCalculator });

            OutputFormatters.IConsoleWriter consoleWriter = (OutputFormatters.IConsoleWriter)Activator.CreateInstance(consoleWriterType);

            OutputFormatters.IOutputFormatter outputFormatter = (OutputFormatters.IOutputFormatter)Activator.CreateInstance(outputFormatterType, new object[] { consoleWriter });

            IReportGenerator reportGenerator = (IReportGenerator)Activator.CreateInstance(reportGeneratorType, new object[] { dataAccess, presidentViewLoader, outputFormatter });

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

            while (!validChoices.Contains(dataAccessNumber) && !isParsed)
            {
                var readString = Console.ReadLine();
                isParsed = int.TryParse(readString.Trim(), out dataAccessNumber);
            }

            return dataAccessNumber;
        }
    }
}