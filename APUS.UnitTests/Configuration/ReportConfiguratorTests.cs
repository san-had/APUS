namespace APUS.UnitTests.Configuration
{
    using APUS.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Unity;
    using Xunit;

    public class ReportConfiguratorTests
    {
        [Fact]
        public void ReportConfigurator_ConstructorParameterContainerNullReturnsException()
        {
            IUnityContainer fakeUnityContainer = null;

            string fakeFileName = "fakeFileName";

            Assert.Throws<ArgumentNullException>(() => new ReportConfigurator(fakeUnityContainer, fakeFileName));
        }

        [Fact]
        public void ReportConfigurator_ConstructorParameterFileNameNullReturnsException()
        {
            IUnityContainer fakeUnityContainer = NSubstitute.Substitute.For<IUnityContainer>();

            string fakeFileName = null;

            Assert.Throws<ArgumentNullException>(() => new ReportConfigurator(fakeUnityContainer, fakeFileName));
        }

        [Fact]
        public void ReportConfigurator_ConstructorParameterFileNameEmptyReturnsException()
        {
            IUnityContainer fakeUnityContainer = NSubstitute.Substitute.For<IUnityContainer>();

            string fakeFileName = string.Empty;

            Assert.Throws<ArgumentNullException>(() => new ReportConfigurator(fakeUnityContainer, fakeFileName));
        }
    }
}