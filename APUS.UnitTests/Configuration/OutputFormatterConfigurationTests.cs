namespace APUS.UnitTests.Configuration
{
    using APUS.Configuration;
    using APUS.OutputFormatters;
    using APUS.Utils;
    using System;
    using System.Linq;
    using Unity;
    using Xunit;

    public class OutputFormatterConfigurationTests
    {
        [Fact]
        public void OutputFormatterConfiguration_ConstructorParameterContainerNullReturnsException()
        {
            IUnityContainer fakeUnityContainer = null;

            IMenu fakeMenu = NSubstitute.Substitute.For<IMenu>();

            Assert.Throws<ArgumentNullException>(() => new OutputFormatterConfiguration(fakeUnityContainer, fakeMenu));
        }

        [Fact]
        public void OutputFormatterConfiguration_ConstructorParameterMenuNullReturnsException()
        {
            IUnityContainer fakeUnityContainer = NSubstitute.Substitute.For<IUnityContainer>();

            IMenu fakeMenu = null;

            Assert.Throws<ArgumentNullException>(() => new OutputFormatterConfiguration(fakeUnityContainer, fakeMenu));
        }

        [Fact]
        public void OutputFormatterConfiguration_ConstructorParametersNotNullReturnsOutputFormatterConfiguration()
        {
            var fakeUnityContainer = NSubstitute.Substitute.For<IUnityContainer>();

            var fakeMenu = NSubstitute.Substitute.For<IMenu>();

            var outputFormatterConfiguration = new OutputFormatterConfiguration(fakeUnityContainer, fakeMenu);

            Assert.NotNull(outputFormatterConfiguration);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void OutputFormatterConfiguration_ConfigureOutputFormatterWithValidFormatNumbers(int formatNumber)
        {
            var fakeUnityContainer = new UnityContainer();

            var fakeMenu = new FakeMenu(formatNumber);

            var outputFormatterConfiguration = new OutputFormatterConfiguration(fakeUnityContainer, fakeMenu);

            outputFormatterConfiguration.Configure();

            var numberOfRegistration = fakeUnityContainer.Registrations.Where(x => x.RegisteredType == typeof(IOutputFormatter)).Count();

            var expectedNumberOfRegistrations = 1;

            Assert.Equal(expectedNumberOfRegistrations, numberOfRegistration);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(8)]
        public void OutputFormatterConfiguration_ConfigureOutputFormatterWithInValidFormatNumbers(int formatNumber)
        {
            var fakeUnityContainer = new UnityContainer();

            var fakeMenu = new FakeMenu(formatNumber);

            var outputFormatterConfiguration = new OutputFormatterConfiguration(fakeUnityContainer, fakeMenu);

            Assert.Throws<NotImplementedException>(() => outputFormatterConfiguration.Configure());
        }
    }
}