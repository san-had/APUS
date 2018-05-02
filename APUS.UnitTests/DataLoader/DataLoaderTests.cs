namespace APUS.UnitTests.DataLoader
{
    using APUS.DataLoader;
    using CommonDataAccess;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xunit;

    public class DataLoaderTests
    {
        [Fact]
        public void DataLoader_ConstructorParameterDataAccessNull_ReturnsException()
        {
            ICommonDataAccess fakeDataAccess = null;

            IOfficerDataMapper fakeOfficerDataMapper = NSubstitute.Substitute.For<IOfficerDataMapper>();

            Assert.Throws<ArgumentNullException>(() => new DataLoader(fakeDataAccess, fakeOfficerDataMapper));
        }

        [Fact]
        public void DataLoader_ConstructorParameterOfficerDataMapperNull_ReturnsException()
        {
            ICommonDataAccess fakeDataAccess = NSubstitute.Substitute.For<ICommonDataAccess>();

            IOfficerDataMapper fakeOfficerDataMapper = null;

            Assert.Throws<ArgumentNullException>(() => new DataLoader(fakeDataAccess, fakeOfficerDataMapper));
        }

        [Fact]
        public void DataLoader_ConstructorParametersAreNotNull_ReturnsDataLoader()
        {
            ICommonDataAccess fakeDataAccess = NSubstitute.Substitute.For<ICommonDataAccess>();

            IOfficerDataMapper fakeOfficerDataMapper = NSubstitute.Substitute.For<IOfficerDataMapper>();

            var dataLoader = new DataLoader(fakeDataAccess, fakeOfficerDataMapper);

            Assert.NotNull(dataLoader);
        }
    }
}