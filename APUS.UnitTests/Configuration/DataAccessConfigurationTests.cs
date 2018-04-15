namespace APUS.UnitTests.Configuration
{
    using APUS.Configuration;
    using APUS.DataLoader;
    using System;
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
        //[InlineData("dataen.csv")]
        [InlineData("hello.xyz")]
        public void DataAccessConfiguration_ConfigureRightFileNamesAddParsersToContainer(string fakeFileName)
        {
            var fakeUnityContainer = new UnityContainer();

            var dataAccessConfiguration = new DataAccessConfiguration(fakeUnityContainer, fakeFileName);

            dataAccessConfiguration.Configure();

            Assert.True(fakeUnityContainer.IsRegistered<IDateParser>());
        }
    }
}