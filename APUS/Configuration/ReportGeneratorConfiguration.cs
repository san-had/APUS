namespace APUS.Configuration
{
    using System;
    using Unity;
    using Unity.Injection;

    public class ReportGeneratorConfiguration : IConfiguration
    {
        private IUnityContainer container;

        public ReportGeneratorConfiguration(IUnityContainer container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public void Configure()
        {
            container.RegisterType<IReportGenerator, ReportGenerator>(
                new InjectionConstructor(
                    typeof(DataLoader.IDataLoader),
                    typeof(ViewModels.IOfficerViewModelDataMapper),
                    typeof(OutputFormatters.IOutputFormatter)));
        }
    }
}