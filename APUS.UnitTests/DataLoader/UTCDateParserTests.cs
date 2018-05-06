namespace APUS.UnitTests.DataLoader
{
    using System;
    using Xunit;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using APUS.DataLoader;

    public class UTCDateParserTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "")]
        [InlineData(null, "a-b-cT00:00:00Z")]
        [InlineData(null, "2017-03--04T00:00:00KU")]
        [InlineData(null, "2017-a-04T00:00:00Z")]
        [InlineData(null, "2017-13-03T00:00:00Z")]
        [InlineData(null, "2017-03-32T00:00:00Z")]
        public void ParseUTCDateFormat_WrongStringFormat_ReturnsNull(DateTime? expectedDateTime, string dateTimeString)
        {
            var actualDate = new UTCDateTimeParser().ParseDate(dateTimeString);

            Assert.Equal(expectedDateTime.HasValue, actualDate.HasValue);
        }

        [Theory]
        [MemberData(nameof(GetInputDataForParseUTCDateFormatTests))]
        public void ParseUTCDateFormat_RightStringFormat_ReturnsDateTime(string inputString, DateTime? expectedDate)
        {
            var actualDate = new UTCDateTimeParser().ParseDate(inputString);

            Assert.Equal(expectedDate.Value, actualDate.Value);
        }

        [ExcludeFromCodeCoverage]
        public static IEnumerable<object[]> GetInputDataForParseUTCDateFormatTests()
        {
            DateTime? expedtedDate = new DateTime(2017, 4, 3);

            var list = new List<object[]>()
            {
                new object[] { "2017-04-03T00:00:00Z", expedtedDate},
                new object[] { "2017-4-3T50:00:00Z", expedtedDate},
                new object[] { " 2017 - 04 - 03 T00:00:00Z ", expedtedDate},
            };

            return list;
        }
    }
}