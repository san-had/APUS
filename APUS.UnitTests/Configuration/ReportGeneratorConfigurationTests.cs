namespace APUS.UnitTests.Configuration
{
    using APUS.Configuration;
    using System;
    using System.Linq;
    using Unity;
    using Xunit;

    public class ReportGeneratorConfigurationTests
    {
        [Fact]
        public void ReportGeneratorConfiguration_ConstructorParameterContainerNullReturnsException()
        {
            IUnityContainer fakeUnityContainer = null;

            Assert.Throws<ArgumentNullException>(() => new ReportGeneratorConfiguration(fakeUnityContainer));
        }

        [Fact]
        public void ReportGeneratorConfiguration_ConstructorParameterContainerNotNullReturnsReportGenerationConfiguration()
        {
            IUnityContainer fakeUnityContainer = NSubstitute.Substitute.For<IUnityContainer>();

            var reportGenerationConfiguration = new ReportGeneratorConfiguration(fakeUnityContainer);

            Assert.NotNull(reportGenerationConfiguration);
        }

        [Fact]
        public void ReportGeneratorConfiguration_Configure_ReportGeneratorInContainer()
        {
            IUnityContainer fakeUnityContainer = new UnityContainer();

            var reportGeneratorConfiguration = new ReportGeneratorConfiguration(fakeUnityContainer);

            reportGeneratorConfiguration.Configure();

            var numberOfRegistration = fakeUnityContainer.Registrations.Where(x => x.RegisteredType == typeof(IReportGenerator)).Count();

            var expectedNumberOfRegistrations = 1;

            Assert.Equal(expectedNumberOfRegistrations, numberOfRegistration);
        }
    }
}