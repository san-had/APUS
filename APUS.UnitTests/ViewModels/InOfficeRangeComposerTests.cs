namespace APUS.UnitTests.ViewModels
{
    using APUS.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    public class InOfficeRangeComposerTests
    {
        [Theory]
        [MemberData(nameof(GetInputDataForInOfficeRangeTest))]
        public void GetInOfficeRange_ReturnFromToString(string expectedString, DateTime? tookOffice, DateTime? leftOffice)
        {
            var inOfficeRangeComposer = new InOfficeRangeComposer();

            var actualString = inOfficeRangeComposer.GetInOfficeRange(tookOffice, leftOffice);

            Assert.Equal(expectedString, actualString);
        }

        [ExcludeFromCodeCoverage]
        public static IEnumerable<object[]> GetInputDataForInOfficeRangeTest()
        {
            DateTime? date11 = new DateTime(1789, 4, 3);
            DateTime? date12 = new DateTime(1793, 4, 10);

            var list = new List<object[]>()
            {
                new object[] { "(1789-1793)", date11, date12},
                new object[] { $"(1789-{Constants.NALeftOfficeString})", date11, null},
                new object[] { $"({Constants.NAString}-1789)", null, date11},
                new object[] { $"{Constants.NAString}", null, null}
            };

            return list;
        }

        [Theory]
        [MemberData(nameof(GetLeftYearInputDataForGetLeftYearStringTests))]
        public void GetLeftYearString_ReturnsRightString(string expectedString, DateTime? leftOfficeDate)
        {
            var inOfficeRangeComposer = new InOfficeRangeComposer();

            var actualString = inOfficeRangeComposer.GetLeftYearString(leftOfficeDate);

            Assert.Equal(expectedString, actualString);
        }

        [ExcludeFromCodeCoverage]
        public static IEnumerable<object[]> GetLeftYearInputDataForGetLeftYearStringTests()
        {
            DateTime? date1 = null;
            DateTime? date2 = new DateTime(1789, 4, 3);
            DateTime? date3 = DateTime.Now;

            string expected1 = Constants.NALeftOfficeString;
            string expected2 = "1789";
            string expected3 = Constants.NALeftOfficeString;

            var list = new List<object[]>()
            {
                new object[] {expected1, date1},
                new object[] {expected2, date2},
                new object[] {expected3, date3}
            };

            return list;
        }
    }
}