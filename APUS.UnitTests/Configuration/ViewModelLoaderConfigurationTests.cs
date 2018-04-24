namespace APUS.UnitTests.Configuration
{
    using APUS.Configuration;
    using APUS.Utils;
    using APUS.ViewModels;
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
    }
}