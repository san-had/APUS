namespace APUS.UnitTests.Configuration
{
    using APUS.Configuration;
    using APUS.DataLoader;
    using CommonDataAccess;
    using System;
    using System.Linq;
    using Unity;
    using Xunit;

    public class DataAccessConfigurationTests
    {
        [Fact]
        public void DataAccessConfiguration_ConstructorParameterContainerNullReturnsException()
        {
            IUnityContainer fakeUnityContainer = null;

            string fakeFileName = "dataen.csv";

            Assert.Throws<ArgumentNullException>(() => new DataAccessConfiguration(fakeUnityContainer, fakeFileName));
        }

        [Fact]
        public void DataAccessConfiguration_ConstructorParameterFileNameNullReturnsException()
        {
            var fakeUnityContainer = NSubstitute.Substitute.For<IUnityContainer>();

            string fakeFileName = null;

            Assert.Throws<ArgumentNullException>(() => new DataAccessConfiguration(fakeUnityContainer, fakeFileName));
        }

        [Fact]
        public void DataAccessConfiguration_ConstructorParameterFileNameEmptyReturnsException()
        {
            var fakeUnityContainer = NSubstitute.Substitute.For<IUnityContainer>();

            string fakeFileName = string.Empty;

            Assert.Throws<ArgumentNullException>(() => new DataAccessConfiguration(fakeUnityContainer, fakeFileName));
        }

        [Theory]
        [InlineData("dataen.csv")]
        [InlineData("hello.xyz")]
        public void DataAccessConfiguration_ConstructorParametersNotNullReturnsDataAccessConfiguration(string fakeFileName)
        {
            var fakeUnityContainer = NSubstitute.Substitute.For<IUnityContainer>();

            var dataAccessConfiguration = new DataAccessConfiguration(fakeUnityContainer, fakeFileName);

            Assert.NotNull(dataAccessConfiguration);
        }

        [Theory]
        [InlineData("helloen.csv")]
        [InlineData("hellous.csv")]
        [InlineData("helloutc.json")]
        public void DataAccessConfiguration_ConfigureRightFileNamesAddParsersToContainer(string fakeFileName)
        {
            var fakeUnityContainer = new UnityContainer();

            var dataAccessConfiguration = new DataAccessConfiguration(fakeUnityContainer, fakeFileName);

            dataAccessConfiguration.Configure();

            var numberOfRegistration = fakeUnityContainer.Registrations.Where(x => x.RegisteredType == typeof(IDateParser)).Count();

            var expectedNumberOfRegistrations = 1;

            Assert.Equal(expectedNumberOfRegistrations, numberOfRegistration);
        }

        [Theory]
        [InlineData("helloen.xyz")]
        [InlineData("hellous.zcd")]
        [InlineData("helloutc.nosj")]
        [InlineData("helloetn.csv")]
        [InlineData("hellouts.csv")]
        [InlineData("hellouttc.json")]
        public void DataAccessConfiguration_ConfigureBadFileNamesNotAddParsersToContainer(string fakeFileName)
        {
            var fakeUnityContainer = new UnityContainer();

            var dataAccessConfiguration = new DataAccessConfiguration(fakeUnityContainer, fakeFileName);

            dataAccessConfiguration.Configure();

            var numberOfRegistration = fakeUnityContainer.Registrations.Where(x => x.RegisteredType == typeof(IDateParser)).Count();

            var expectedNumberOfRegistrations = 0;

            Assert.Equal(expectedNumberOfRegistrations, numberOfRegistration);
        }

        [Theory]
        [InlineData("helloen.csv")]
        [InlineData("hellous.csv")]
        [InlineData("helloutc.json")]
        public void DataAccessConfiguration_ConfigureRightFileNamesAddDataAccessToContainer(string fakeFileName)
        {
            var fakeUnityContainer = new UnityContainer();

            var dataAccessConfiguration = new DataAccessConfiguration(fakeUnityContainer, fakeFileName);

            dataAccessConfiguration.Configure();

            var numberOfRegistration = fakeUnityContainer.Registrations.Where(x => x.RegisteredType == typeof(ICommonDataAccess)).Count();

            var expectedNumberOfRegistrations = 1;

            Assert.Equal(expectedNumberOfRegistrations, numberOfRegistration);
        }

        [Theory]
        [InlineData("helloen.xyz")]
        [InlineData("hellous.zcd")]
        [InlineData("helloutc.nosj")]
        [InlineData("helloetn.csv")]
        [InlineData("hellouts.csv")]
        [InlineData("hellouttc.json")]
        public void DataAccessConfiguration_ConfigureBadFileNamesNotAddDataAccessToContainer(string fakeFileName)
        {
            var fakeUnityContainer = new UnityContainer();

            var dataAccessConfiguration = new DataAccessConfiguration(fakeUnityContainer, fakeFileName);

            dataAccessConfiguration.Configure();

            var numberOfRegistration = fakeUnityContainer.Registrations.Where(x => x.RegisteredType == typeof(ICommonDataAccess)).Count();

            var expectedNumberOfRegistrations = 0;

            Assert.Equal(expectedNumberOfRegistrations, numberOfRegistration);
        }
    }
}