namespace APUS
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;

    internal class Program
    {
        public static IConfiguration Configuration { get; set; }

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

            var dataAccessTypeName = Configuration["dataAccess"];
            var dataAccessType = Type.GetType(dataAccessTypeName, true);

            var presidentViewCalculatorTypeName = Configuration["presidentViewCalculator"];
            var presidentViewCalculatorType = Type.GetType(presidentViewCalculatorTypeName, true);

            var presidentViewLoaderTypeName = Configuration["presidentViewLoader"];
            var presidentViewLoaderType = Type.GetType(presidentViewLoaderTypeName, true);

            var outputFormatterTypeName = Configuration["outputFormatter"];
            var outputFormatterType = Type.GetType(outputFormatterTypeName, true);

            DataAccess.IDataAccess dataAccess = (DataAccess.IDataAccess)Activator.CreateInstance(dataAccessType);

            ViewModels.IPresidentViewCalculator presidentViewCalculator = (ViewModels.IPresidentViewCalculator)Activator.CreateInstance(presidentViewCalculatorType);

            ViewModels.IPresidentViewLoader presidentViewLoader = (ViewModels.IPresidentViewLoader)Activator.CreateInstance(presidentViewLoaderType, new object[] { presidentViewCalculator });

            OutputFormatters.IOutputFormatter outputFormatter = (OutputFormatters.IOutputFormatter)Activator.CreateInstance(outputFormatterType);

            var reportGenerator = new ReportGenerator(dataAccess, presidentViewLoader, outputFormatter);

            reportGenerator.CreateReport();
        }
    }
}