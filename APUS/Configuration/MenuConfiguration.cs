namespace APUS.Configuration
{
    using APUS.Utils;
    using System;
    using Unity;

    public class MenuConfiguration : IConfiguration
    {
        private IUnityContainer container;

        public MenuConfiguration(IUnityContainer container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public void Configure()
        {
            container.RegisterType<IMenu, Menu>();
        }
    }
}