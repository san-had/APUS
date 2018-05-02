namespace APUS.UnitTests.DataLoader
{
    using APUS.DataLoader;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    public class DateParserTests
    {
        [Theory]
        [MemberData(nameof(GetRightInputDataForDateParserInputTests))]
        public void DateParser_RightInputString_ReturnsExpected(string[] ymd, DateTime? expectedDate)
        {
            var actualdate = new DateParser().DateComposition(ymd);

            Assert.Equal(expectedDate.Value, actualdate.Value);
        }

        [ExcludeFromCodeCoverage]
        public static IEnumerable<object[]> GetRightInputDataForDateParserInputTests()
        {
            DateTime? expectedDate = new DateTime(2017, 4, 3);

            var ymd1 = new string[3] { "2017", "4", "3" };
            var ymd2 = new string[3] { "2017", "04", "03" };
            var ymd3 = new string[3] { " 2017 ", " 04 ", " 03 " };

            var list = new List<object[]>()
            {
                new object[] { ymd1, expectedDate},
                new object[] { ymd2, expectedDate},
                new object[] { ymd3, expectedDate}
            };

            return list;
        }

        [Theory]
        [MemberData(nameof(GetFalseInputDataForDateParserFalseInputTests))]
        public void DateParser_FalseInputString_ReturnsNull(string[] ymd, DateTime? expectedDate)
        {
            var actualdate = new DateParser().DateComposition(ymd);

            Assert.Equal(expectedDate.HasValue, actualdate.HasValue);
        }

        [ExcludeFromCodeCoverage]
        public static IEnumerable<object[]> GetFalseInputDataForDateParserFalseInputTests()
        {
            DateTime? expectedDate = new DateTime(2017, 4, 3);

            var ymd1 = new string[4] { "2017", "4", "3", "12" };
            var ymd2 = new string[2] { "2017", "4" };
            var ymd3 = new string[3] { "2017", "13", "03" };
            var ymd4 = new string[3] { "2017", "4", "31" };

            var list = new List<object[]>()
            {
                new object[] { null, null},
                new object[] { ymd1, null},
                new object[] { ymd2, null},
                new object[] { ymd3, null},
                new object[] { ymd4, null}
            };

            return list;
        }

        [Theory]
        [InlineData(13, 0)]
        [InlineData(0, 0)]
        [InlineData(-1, 0)]
        public void DateParser_MonthMaxDays_InvalidMonth_Returns0(int inputMonth, int expectedMonth)
        {
            var actualMonth = new DateParser().MonthMaxDays(inputMonth);

            Assert.Equal(expectedMonth, actualMonth);
        }
    }
}