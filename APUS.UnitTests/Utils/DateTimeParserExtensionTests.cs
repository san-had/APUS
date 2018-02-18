namespace APUS.UnitTests.Utils
{
    using System;
    using Xunit;
    using APUS.Utils;

    public class DateTimeParserExtensionTests
    {
        [Theory]
        [InlineData(null, "a/b/c")]
        [InlineData(null, "03\04/2017")]
        public void ParseUsDateFormat_WrongStringFormat_ReturnsNull(DateTime? expectedDateTime, string dateTimeString)
        {
            var actualDate = dateTimeString.ParseUsDateFormat();

            Assert.Equal(expectedDateTime.HasValue, actualDate.HasValue);
        }

        [Fact]
        public void ParseUsDateFormat_RightStringFormat_ReturnsDateTime()
        {
            var actualDateString = "3/4/2017";
            var actualDateString2 = "03/04/2017";

            DateTime? expedtedDate = new DateTime(2017, 4, 3);

            var actualDate = actualDateString.ParseUsDateFormat();
            var actualDate2 = actualDateString2.ParseUsDateFormat();

            Assert.Equal(expedtedDate.Value, actualDate.Value);
            Assert.Equal(expedtedDate.Value, actualDate2.Value);
        }

    }
}
