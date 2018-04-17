namespace APUS.UnitTests.Configuration
{
    using APUS.Configuration;
    using APUS.OutputFormatters;
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

            Assert.Throws<ArgumentNullException>(() => new OutputFormatterConfiguration(fakeUnityContainer));
        }

        [Fact]
        public void OutputFormatterConfiguration_ConstructorParameterContainerNotNullReturnsOutputFormatterConfiguration()
        {
            var fakeUnityContainer = new UnityContainer();

            var outputFormatterConfiguration = new OutputFormatterConfiguration(fakeUnityContainer);

            Assert.NotNull(outputFormatterConfiguration);
        }

        [Fact]
        public void OutputFormatterConfiguration_ConfigureAddOutputFormatter()
        {
            var fakeUnityContainer = new UnityContainer();

            var outputFormatterConfiguration = new OutputFormatterConfiguration(fakeUnityContainer);

            outputFormatterConfiguration.Configure();

            var isRegistered = fakeUnityContainer.IsRegistered<IOutputFormatter>();

            Assert.True(isRegistered);
        }
    }
}