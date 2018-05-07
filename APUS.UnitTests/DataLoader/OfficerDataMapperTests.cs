namespace APUS.UnitTests.DataLoader
{
    using APUS.DataLoader;
    using CommonDataAccess;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Xunit;

    public class OfficerDataMapperTests
    {
        [Fact]
        public void OfficerDataMapper_ConstructorParameterDataParserIsNull_ReturnsException()
        {
            IDateParser fakeDateParser = null;

            Assert.Throws<ArgumentNullException>(() => new OfficerDataMapper(fakeDateParser));
        }

        [Fact]
        public void OfficerDataMapper_ConstructorParameterDataParserIsNotNull_ReturnsOfficerDataMapper()
        {
            IDateParser fakeDateParser = NSubstitute.Substitute.For<IDateParser>();

            var officerDataMapper = new OfficerDataMapper(fakeDateParser);

            Assert.NotNull(officerDataMapper);
        }

        [Fact]
        public void OfficerDataMapper_Map_InputNullCollectionCommonDbOfficerReturnsException()
        {
            var fakeDataParser = NSubstitute.Substitute.For<IDateParser>();

            var fakeOfficerDataMapper = new OfficerDataMapper(fakeDataParser);

            IEnumerable<CommonDbOfficer> fakeCommonDbOfficerCollection = null;

            var actualOfficerCollection = fakeOfficerDataMapper.Map(fakeCommonDbOfficerCollection);

            Assert.Throws<ArgumentNullException>(() => actualOfficerCollection.ToList());
        }

        [Fact]
        public void OfficerDataMapper_Map_InputEmptyCollectionCommonDbOfficerReturnsEmptyCollection()
        {
            var fakeDataParser = NSubstitute.Substitute.For<IDateParser>();

            var fakeOfficerDataMapper = new OfficerDataMapper(fakeDataParser);

            IEnumerable<CommonDbOfficer> fakeCommonDbOfficerCollection = Enumerable.Empty<CommonDbOfficer>();

            var actualOfficerCollection = fakeOfficerDataMapper.Map(fakeCommonDbOfficerCollection);

            Assert.Empty(actualOfficerCollection);
        }
    }
}