namespace APUS.UnitTests.Configuration
{
    using APUS.Configuration;
    using APUS.DataLoader;
    using APUS.OutputFormatters;
    using APUS.Utils;
    using APUS.ViewModels;
    using CommonDataAccess;
    using System;
    using System.Linq;
    using Unity;
    using Xunit;

    public class ReportConfiguratorTests
    {
        [Fact]
        public void ReportConfigurator_ConstructorParameterContainerNullReturnsException()
        {
            IUnityContainer fakeUnityContainer = null;

            string fakeFileName = "fakeFileName";

            IMenu fakeMenu = NSubstitute.Substitute.For<IMenu>();

            Assert.Throws<ArgumentNullException>(() => new ReportConfigurator(fakeUnityContainer, fakeFileName, fakeMenu));
        }

        [Fact]
        public void ReportConfigurator_ConstructorParameterFileNameNullReturnsException()
        {
            IUnityContainer fakeUnityContainer = NSubstitute.Substitute.For<IUnityContainer>();

            string fakeFileName = null;

            IMenu fakeMenu = NSubstitute.Substitute.For<IMenu>();

            Assert.Throws<ArgumentNullException>(() => new ReportConfigurator(fakeUnityContainer, fakeFileName, fakeMenu));
        }

        [Fact]
        public void ReportConfigurator_ConstructorParameterMenuNullReturnsException()
        {
            IUnityContainer fakeUnityContainer = NSubstitute.Substitute.For<IUnityContainer>();

            string fakeFileName = "fakeFileName";

            IMenu fakeMenu = null;

            Assert.Throws<ArgumentNullException>(() => new ReportConfigurator(fakeUnityContainer, fakeFileName, fakeMenu));
        }

        [Fact]
        public void ReportConfigurator_ConstructorParameterFileNameEmptyReturnsException()
        {
            IUnityContainer fakeUnityContainer = NSubstitute.Substitute.For<IUnityContainer>();

            string fakeFileName = string.Empty;

            IMenu fakeMenu = NSubstitute.Substitute.For<IMenu>();

            Assert.Throws<ArgumentNullException>(() => new ReportConfigurator(fakeUnityContainer, fakeFileName, fakeMenu));
        }

        [Fact]
        public void ReportConfigurator_Setup_CheckDataAccessInContainer()
        {
            IUnityContainer fakeUnityContainer = new UnityContainer();

            string fakeFileName = "helloen.csv";

            int fakeFormattedNumber = 1;

            IMenu fakeMenu = new FakeMenu(fakeFormattedNumber);

            var reportConfigurator = new ReportConfigurator(fakeUnityContainer, fakeFileName, fakeMenu);

            reportConfigurator.Setup();

            var numberOfRegistration = fakeUnityContainer.Registrations.Where(x => x.RegisteredType == typeof(ICommonDataAccess)).Count();

            var expectedNumberOfRegistrations = 1;

            Assert.Equal(expectedNumberOfRegistrations, numberOfRegistration);
        }

        [Fact]
        public void ReportConfigurator_Setup_CheckDataLoaderInContainer()
        {
            IUnityContainer fakeUnityContainer = new UnityContainer();

            string fakeFileName = "helloen.csv";

            int fakeFormattedNumber = 1;

            IMenu fakeMenu = new FakeMenu(fakeFormattedNumber);

            var reportConfigurator = new ReportConfigurator(fakeUnityContainer, fakeFileName, fakeMenu);

            reportConfigurator.Setup();

            var numberOfRegistration = fakeUnityContainer.Registrations.Where(x => x.RegisteredType == typeof(IDataLoader)).Count();

            var expectedNumberOfRegistrations = 1;

            Assert.Equal(expectedNumberOfRegistrations, numberOfRegistration);
        }

        [Fact]
        public void ReportConfigurator_Setup_CheckViewModelLoaderInContainer()
        {
            IUnityContainer fakeUnityContainer = new UnityContainer();

            string fakeFileName = "helloen.csv";

            int fakeFormattedNumber = 1;

            IMenu fakeMenu = new FakeMenu(fakeFormattedNumber);

            var reportConfigurator = new ReportConfigurator(fakeUnityContainer, fakeFileName, fakeMenu);

            reportConfigurator.Setup();

            var numberOfRegistration = fakeUnityContainer.Registrations.Where(x => x.RegisteredType == typeof(IOfficerViewModelDataMapper)).Count();

            var expectedNumberOfRegistrations = 1;

            Assert.Equal(expectedNumberOfRegistrations, numberOfRegistration);
        }

        [Fact]
        public void ReportConfigurator_Setup_CheckOutputFormatterInContainer()
        {
            IUnityContainer fakeUnityContainer = new UnityContainer();

            string fakeFileName = "helloen.csv";

            int fakeFormattedNumber = 1;

            IMenu fakeMenu = new FakeMenu(fakeFormattedNumber);

            var reportConfigurator = new ReportConfigurator(fakeUnityContainer, fakeFileName, fakeMenu);

            reportConfigurator.Setup();

            var numberOfRegistration = fakeUnityContainer.Registrations.Where(x => x.RegisteredType == typeof(IOutputFormatter)).Count();

            var expectedNumberOfRegistrations = 1;

            Assert.Equal(expectedNumberOfRegistrations, numberOfRegistration);
        }

        [Fact]
        public void ReportConfigurator_Setup_CheckReportGeneratorInContainer()
        {
            IUnityContainer fakeUnityContainer = new UnityContainer();

            string fakeFileName = "helloen.csv";

            int fakeFormattedNumber = 1;

            IMenu fakeMenu = new FakeMenu(fakeFormattedNumber);

            var reportConfigurator = new ReportConfigurator(fakeUnityContainer, fakeFileName, fakeMenu);

            reportConfigurator.Setup();

            var numberOfRegistration = fakeUnityContainer.Registrations.Where(x => x.RegisteredType == typeof(IReportGenerator)).Count();

            var expectedNumberOfRegistrations = 1;

            Assert.Equal(expectedNumberOfRegistrations, numberOfRegistration);
        }
    }
}