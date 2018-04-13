namespace APUS.UnitTests
{
    using APUS.DataLoader;
    using APUS.OutputFormatters;
    using APUS.ViewModels;
    using System;
    using Xunit;

    public class ReportGeneratorTests
    {
        [Fact]
        public void ReportGeneratorConstructor_ConstructorParameterDataLoaderNull_ReturnsException()
        {
            IDataLoader fakeDataLoader = null;
            var fakeOfficerViewModel = NSubstitute.Substitute.For<IOfficerViewModelDataMapper>();
            var fakeOutputFormatter = NSubstitute.Substitute.For<IOutputFormatter>();

            Assert.Throws<ArgumentNullException>(() => new ReportGenerator(fakeDataLoader, fakeOfficerViewModel, fakeOutputFormatter));
        }

        [Fact]
        public void ReportGeneratorConstructor_ConstructorParameterOfficerViewModelNull_ReturnsException()
        {
            var fakeDataLoader = NSubstitute.Substitute.For<IDataLoader>();
            IOfficerViewModelDataMapper fakeOfficerViewModel = null;
            var fakeOutputFormatter = NSubstitute.Substitute.For<IOutputFormatter>();

            Assert.Throws<ArgumentNullException>(() => new ReportGenerator(fakeDataLoader, fakeOfficerViewModel, fakeOutputFormatter));
        }

        [Fact]
        public void ReportGeneratorConstructor_ConstructorParameterOutputFormatterNull_ReturnsException()
        {
            var fakeDataLoader = NSubstitute.Substitute.For<IDataLoader>(); ;
            var fakeOfficerViewModel = NSubstitute.Substitute.For<IOfficerViewModelDataMapper>(); ;
            IOutputFormatter fakeOutputFormatter = null;

            Assert.Throws<ArgumentNullException>(() => new ReportGenerator(fakeDataLoader, fakeOfficerViewModel, fakeOutputFormatter));
        }

        [Fact]
        public void ReportGeneratorConstructor_AllConstructorParameterNull_ReturnsException()
        {
            IDataLoader fakeDataLoader = null;
            IOfficerViewModelDataMapper fakeOfficerViewModel = null;
            IOutputFormatter fakeOutputFormatter = null;

            Assert.Throws<ArgumentNullException>(() => new ReportGenerator(fakeDataLoader, fakeOfficerViewModel, fakeOutputFormatter));
        }

        [Fact]
        public void ReportGeneratorConstructor_AllConstructorParameterNotNull_ReturnsReportGenerator()
        {
            var fakeDataLoader = NSubstitute.Substitute.For<IDataLoader>(); ;
            var fakeOfficerViewModel = NSubstitute.Substitute.For<IOfficerViewModelDataMapper>();
            var fakeOutputFormatter = NSubstitute.Substitute.For<IOutputFormatter>();

            var reportGenerator = new ReportGenerator(fakeDataLoader, fakeOfficerViewModel, fakeOutputFormatter);

            Assert.NotNull(reportGenerator);
        }
    }
}