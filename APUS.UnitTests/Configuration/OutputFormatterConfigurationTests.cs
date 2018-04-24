namespace APUS.UnitTests.Configuration
{
    using APUS.Configuration;
    using APUS.OutputFormatters;
    using APUS.Utils;
    using System;
    using System.Collections.Generic;
    using System.Text;
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
        public void OutputFormatterConfiguration_ConstructorParameterContainerNotNullReturnsOutputFormatterConfiguration()
        {
            var fakeUnityContainer = NSubstitute.Substitute.For<IUnityContainer>();

            var fakeMenu = NSubstitute.Substitute.For<IMenu>();

            var outputFormatterConfiguration = new OutputFormatterConfiguration(fakeUnityContainer, fakeMenu);

            Assert.NotNull(outputFormatterConfiguration);
        }

        [Fact]
        public void OutputFormatterConfiguration_ConfigureAddOutputFormatter()
        {
            var fakeUnityContainer = new UnityContainer();

            var fakeMenu = NSubstitute.Substitute.For<IMenu>();

            var outputFormatterConfiguration = new OutputFormatterConfiguration(fakeUnityContainer, fakeMenu);

            outputFormatterConfiguration.Configure();

            var isRegistered = fakeUnityContainer.IsRegistered<IOutputFormatter>();

            Assert.True(isRegistered);
        }
    }
}