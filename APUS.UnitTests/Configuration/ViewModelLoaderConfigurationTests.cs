namespace APUS.UnitTests.Configuration
{
    using APUS.Configuration;
    using APUS.Utils;
    using APUS.ViewModels;
    using APUS.ViewModels.Calculation;
    using System;
    using System.Linq;
    using Unity;
    using Xunit;

    public class ViewModelLoaderConfigurationTests
    {
        [Fact]
        public void ViewModelLoaderConfiguration_ConstructorParameterContainerNullReturnsException()
        {
            IUnityContainer fakeUnityContainer = null;

            IMenu fakeMenu = NSubstitute.Substitute.For<IMenu>();

            Assert.Throws<ArgumentNullException>(() => new ViewModelLoaderConfiguration(fakeUnityContainer, fakeMenu));
        }

        [Fact]
        public void ViewModelLoaderConfiguration_ConstructorParameterMenuNullReturnsException()
        {
            IUnityContainer fakeUnityContainer = NSubstitute.Substitute.For<IUnityContainer>();

            IMenu fakeMenu = null;

            Assert.Throws<ArgumentNullException>(() => new ViewModelLoaderConfiguration(fakeUnityContainer, fakeMenu));
        }

        [Fact]
        public void ViewModelLoaderConfiguration_ConstructorParametersNotNullReturnsViewModelLoaderConfiguration()
        {
            IUnityContainer fakeUnityContainer = NSubstitute.Substitute.For<IUnityContainer>();

            var fakeMenu = NSubstitute.Substitute.For<IMenu>();

            var viewModelLoaderConfiguration = new ViewModelLoaderConfiguration(fakeUnityContainer, fakeMenu);

            Assert.NotNull(viewModelLoaderConfiguration);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void ViewModelLoaderConfiguration_ConfigureViewLoaderWithValidFormatNumbers(int formatNumber)
        {
            var fakeUnityContainer = new UnityContainer();

            var fakeMenu = new FakeMenu(formatNumber);

            var viewModelLoaderConfiguration = new ViewModelLoaderConfiguration(fakeUnityContainer, fakeMenu);

            viewModelLoaderConfiguration.Configure();

            var numberOfRegistration = fakeUnityContainer.Registrations.Where(x => x.RegisteredType == typeof(IOfficerViewModelDataMapper)).Count();

            var expectedNumberOfRegistrations = 1;

            Assert.Equal(expectedNumberOfRegistrations, numberOfRegistration);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(8)]
        public void ViewModelLoaderConfiguration_ConfigureViewLoaderWithInValidFormatNumbers(int formatNumber)
        {
            var fakeUnityContainer = new UnityContainer();

            var fakeMenu = new FakeMenu(formatNumber);

            var viewModelLoaderConfiguration = new ViewModelLoaderConfiguration(fakeUnityContainer, fakeMenu);

            Assert.Throws<NotImplementedException>(() => viewModelLoaderConfiguration.Configure());
        }

        [Fact]
        public void ViewModelLoaderConfiguration_ConfigureViewLoader_CheckInOfficeDaysCalculatorInContainer()
        {
            var fakeUnityContainer = new UnityContainer();

            var fakeMenu = new FakeMenu(1);

            var viewModelLoaderConfiguration = new ViewModelLoaderConfiguration(fakeUnityContainer, fakeMenu);

            viewModelLoaderConfiguration.Configure();

            var numberOfRegistration = fakeUnityContainer.Registrations.Where(x => x.RegisteredType == typeof(IInOfficeDaysCalculator)).Count();

            var expectedNumberOfRegistrations = 1;

            Assert.Equal(expectedNumberOfRegistrations, numberOfRegistration);
        }

        [Fact]
        public void ViewModelLoaderConfiguration_ConfigureViewLoader_CheckInOfficeRangeComposerInContainer()
        {
            var fakeUnityContainer = new UnityContainer();

            var fakeMenu = new FakeMenu(1);

            var viewModelLoaderConfiguration = new ViewModelLoaderConfiguration(fakeUnityContainer, fakeMenu);

            viewModelLoaderConfiguration.Configure();

            var numberOfRegistration = fakeUnityContainer.Registrations.Where(x => x.RegisteredType == typeof(IInOfficeRangeComposer)).Count();

            var expectedNumberOfRegistrations = 1;

            Assert.Equal(expectedNumberOfRegistrations, numberOfRegistration);
        }

        [Fact]
        public void ViewModelLoaderConfiguration_ConfigureViewLoader_CheckLeftOfficeParserInContainer()
        {
            var fakeUnityContainer = new UnityContainer();

            var fakeMenu = new FakeMenu(1);

            var viewModelLoaderConfiguration = new ViewModelLoaderConfiguration(fakeUnityContainer, fakeMenu);

            viewModelLoaderConfiguration.Configure();

            var numberOfRegistration = fakeUnityContainer.Registrations.Where(x => x.RegisteredType == typeof(ILeftOfficeParser)).Count();

            var expectedNumberOfRegistrations = 1;

            Assert.Equal(expectedNumberOfRegistrations, numberOfRegistration);
        }
    }
}