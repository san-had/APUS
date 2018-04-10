namespace APUS.Configuration
{
    using System;
    using Unity;
    using Unity.Injection;

    public class DataLoaderConfiguration : IConfiguration
    {
        private IUnityContainer container;

        public DataLoaderConfiguration(IUnityContainer container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public void Configure()
        {
            container.RegisterType<DataLoader.IOfficerDataMapper, DataLoader.OfficerDataMapper>(new InjectionConstructor(typeof(DataLoader.IDateParser)));
            container.RegisterType<DataLoader.IDataLoader, DataLoader.DataLoader>(new InjectionConstructor(typeof(CommonDataAccess.ICommonDataAccess), typeof(DataLoader.IOfficerDataMapper)));
        }
    }
}