namespace APUS.UnitTests.Utils
{
    using System;
    using Xunit;
    using APUS.Utils;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class DateTimeParserExtensionTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "")]
        [InlineData(null, "a/b/c")]
        [InlineData(null, "03\04/2017")]
        [InlineData(null, "03/a/2017")]
        [InlineData(null, "03/13/2017")]
        [InlineData(null, "32/03/2017")]
        public void ParseEnDateFormat_WrongStringFormat_ReturnsNull(DateTime? expectedDateTime, string dateTimeString)
        {
            var actualDate = dateTimeString.ParseEnDateFormat();

            Assert.Equal(expectedDateTime.HasValue, actualDate.HasValue);
        }

        [Theory]
        [MemberData(nameof(GetInputDataForParseEnDateFormatTests))]
        public void ParseEnDateFormat_RightStringFormat_ReturnsDateTime(string inputString, DateTime? expectedDate)
        {
            var actualDate = inputString.ParseEnDateFormat();

            Assert.Equal(expectedDate.Value, actualDate.Value);
        }

        [ExcludeFromCodeCoverage]
        public static IEnumerable<object[]> GetInputDataForParseEnDateFormatTests()
        {
            DateTime? expedtedDate = new DateTime(2017, 4, 3);

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