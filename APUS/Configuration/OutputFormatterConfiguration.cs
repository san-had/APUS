namespace APUS.Configuration
{
    using APUS.Utils;
    using System;
    using System.Collections.Generic;
    using Unity;
    using Unity.Injection;

    public class OutputFormatterConfiguration : IConfiguration
    {
        private IUnityContainer container;

        private IMenu menu;

        public OutputFormatterConfiguration(IUnityContainer container, IMenu menu)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));

            this.menu = menu ?? throw new ArgumentNullException(nameof(menu));
        }

        public void Configure()
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

        private int GetOutputFormat()
        {
            var menuDictionary = new Dictionary<int, string>()
            {
                {1, "Standard" },
                {2, "Table" }
            };

            menu.DisplayMenu(menuDictionary);
            return menu.GetChoise(menuDictionary);
        }
    }
}