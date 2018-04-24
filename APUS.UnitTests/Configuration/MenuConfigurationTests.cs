namespace APUS.UnitTests.Configuration
{
    using APUS.Configuration;
    using APUS.Utils;
    using System;
    using Unity;
    using Xunit;

    public class MenuConfigurationTests
    {
        [Fact]
        public void MenuConfiguration_ConstructorParameterContainerNullReturnsException()
        {
            IUnityContainer fakeUnityContainer = null;

            Assert.Throws<ArgumentNullException>(() => new MenuConfiguration(fakeUnityContainer));
        }

        [Fact]
        public void MenuConfiguration_ConstructorParameterContainerNotNullReturnsMenuConfiguration()
        {
            IUnityContainer fakeUnityContainer = new UnityContainer();

            var menuConfiguration = new MenuConfiguration(fakeUnityContainer);

            menuConfiguration.Configure();

            var isRegistered = fakeUnityContainer.IsRegistered<IMenu>();

            Assert.True(isRegistered);
        }
    }
}