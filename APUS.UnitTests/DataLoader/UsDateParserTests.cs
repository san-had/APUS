namespace APUS.UnitTests.DataLoader
{
    using System;
    using Xunit;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using APUS.DataLoader;

    public class UsDateParserTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "")]
        [InlineData(null, "a/b/c")]
        [InlineData(null, "03\04/2017")]
        [InlineData(null, "03/a/2017")]
        [InlineData(null, "13/03/2017")]
        [InlineData(null, "32/03/2017")]
        public void ParseUsDateFormat_WrongStringFormat_ReturnsNull(DateTime? expectedDateTime, string dateTimeString)
        {
            var actualDate = new UsDateParser().ParseDate(dateTimeString);

            Assert.Equal(expectedDateTime.HasValue, actualDate.HasValue);
        }

        [Theory]
        [MemberData(nameof(GetInputDataForParseUsDateFormatTests))]
        public void ParseUsDateFormat_RightStringFormat_ReturnsDateTime(string inputString, DateTime? expectedDate)
        {
            var actualDate = new UsDateParser().ParseDate(inputString);

            Assert.Equal(expectedDate.Value, actualDate.Value);
        }

        [ExcludeFromCodeCoverage]
        public static IEnumerable<object[]> GetInputDataForParseUsDateFormatTests()
        {
            DateTime? expedtedDate = new DateTime(2017, 3, 4);

            var list = new List<object[]>()
            {
                new object[] { "3/4/2017", expedtedDate},
                new object[] { "03/04/2017", expedtedDate},
                new object[] { " 03 / 04 / 2017 ", expedtedDate},
            };

            return list;
        }
    }
}