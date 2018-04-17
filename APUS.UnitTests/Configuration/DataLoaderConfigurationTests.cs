namespace APUS.UnitTests.Configuration
{
    using APUS.Configuration;
    using APUS.DataLoader;
    using System;
    using Unity;
    using Xunit;

    public class DataLoaderConfigurationTests
    {
        [Fact]
        public void DataLoaderConfiguration_ConstructorParameterContainerNullReturnsException()
        {
            IUnityContainer fakeUnityContainer = null;

            Assert.Throws<ArgumentNullException>(() => new DataLoaderConfiguration(fakeUnityContainer));
        }

        [Fact]
        public void DataLoaderConfiguration_ConstructorParameterContainerNotNullReturnsDataLoaderConfiguration()
        {
            var fakeUnityContainer = NSubstitute.Substitute.For<IUnityContainer>();

            var dataLoaderConfiguration = new DataLoaderConfiguration(fakeUnityContainer);

            Assert.NotNull(dataLoaderConfiguration);
        }

        [Fact]
        public void DataLoaderConfiguration_ConfigureAddOfficerDataMapperToConfiguration()
        {
            var fakeUnityContainer = new UnityContainer();

            var dataLoaderConfiguration = new DataLoaderConfiguration(fakeUnityContainer);

            dataLoaderConfiguration.Configure();

            var isRegistered = fakeUnityContainer.IsRegistered<IOfficerDataMapper>();

            Assert.True(isRegistered);
        }

        [Fact]
        public void DataLoaderConfiguration_ConfigureAddDataLoaderToConfiguration()
        {
            var fakeUnityContainer = new UnityContainer();

            var dataLoaderConfiguration = new DataLoaderConfiguration(fakeUnityContainer);

            dataLoaderConfiguration.Configure();

            var isRegistered = fakeUnityContainer.IsRegistered<IDataLoader>();

            Assert.True(isRegistered);
        }
    }
}