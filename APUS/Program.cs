namespace APUS
{
    using System;

    internal class Program
    {
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
            DataAccess.IDataAccess dataAccess = new DataAccess.CsvDataAccess();

            ViewModels.IPresidentViewCalculator presidentViewCalculator = new ViewModels.PresidentViewCalculator();

            ViewModels.IPresidentViewLoader presidentViewLoader = new ViewModels.PresidentViewLoader(presidentViewCalculator);

            OutputFormatters.IOutputFormatter outputFormatter = new OutputFormatters.StdOutputFormatter();

            var reportGenerator = new ReportGenerator(dataAccess, presidentViewLoader, outputFormatter);

            reportGenerator.CreateReport();
        }
    }
}