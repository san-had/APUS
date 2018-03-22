namespace APUS.Configuration
{
    using APUS.Utils;
    using System;
    using Unity;

    public class LoggerConfiguration : IConfiguration
    {
        private IUnityContainer container;

        public LoggerConfiguration(IUnityContainer container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public void Configure()
        {
            container.RegisterType<ILogger, Logger>();
        }
    }
}