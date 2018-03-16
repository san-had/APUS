namespace APUS.UnitTests.ViewModels.Calculation
{
    using APUS.ViewModels.Calculation;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    public class LeftOfficeParserTests
    {
        [Theory]
        [MemberData(nameof(GetLeftOfficeInputDataForLeftOfficeParserTests))]
        public void LeftOfficeParser_ReturnsRightDateTimeNullable(DateTime? expectedDate, DateTime? leftOfficeDate)
        {
            var leftOfficeParser = new LeftOfficeParser();

            var actualDate = leftOfficeParser.ParseLeftOffice(leftOfficeDate);

            Assert.Equal(expectedDate.Value.Year, actualDate.Value.Year);
            Assert.Equal(expectedDate.Value.Month, actualDate.Value.Month);
            Assert.Equal(expectedDate.Value.Day, actualDate.Value.Day);
        }

        [ExcludeFromCodeCoverage]
        public static IEnumerable<object[]> GetLeftOfficeInputDataForLeftOfficeParserTests()
        {
            DateTime? date1 = null;
            DateTime? date1Expected = DateTime.Now;

            DateTime? date2 = new DateTime(1789, 4, 3);
            DateTime? date2Expected = new DateTime(1789, 4, 3);

            var list = new List<object[]>()
            {
                new object[] { date1Expected, date1 },
                new object[] { date2Expected, date2 }
            };

            return list;
        }
    }
}