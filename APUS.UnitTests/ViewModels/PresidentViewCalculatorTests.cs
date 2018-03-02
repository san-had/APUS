﻿namespace APUS.UnitTests.ViewModels
{
    using APUS;
    using APUS.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    public class PresidentViewCalculatorTests
    {
        [Theory]
        [MemberData(nameof(GetInputDataForCalculateNumberOfPresidencyDaysTest))]
        public void CalculateNumberOfPresidencyDays_ReturnsCalculatedDays(int expecteddays, DateTime? tookOffice, DateTime? leftOffice)
        {
            var presidentViewCalculator = new PresidentViewCalculator();

            var actualDays = presidentViewCalculator.CalculateNumberOfPresidencyDays(tookOffice, leftOffice);

            Assert.Equal(expecteddays, actualDays);
        }

        [ExcludeFromCodeCoverage]
        public static IEnumerable<object[]> GetInputDataForCalculateNumberOfPresidencyDaysTest()
        {
            DateTime? date11 = new DateTime(1789, 4, 3);
            DateTime? date12 = new DateTime(1793, 4, 10);

            DateTime? date21 = DateTime.Now;

            DateTime? date32 = new DateTime(1984, 4, 10);

            var list = new List<object[]>()
            {
                new object[] { 1468, date11, date12},
                new object[] { 0, date21, null},
                new object[] { 0, null, date32},
                new object[] { 0, null, null}
            };

            return list;
        }

        [Theory]
        [MemberData(nameof(GetInputDataForPresidencyRangeTest))]
        public void GetPresidencyRange_ReturnFromToString(string expectedString, DateTime? tookOffice, DateTime? leftOffice)
        {
            var presidentViewCalculator = new PresidentViewCalculator();

            var actualString = presidentViewCalculator.GetPresidencyRange(tookOffice, leftOffice);

            Assert.Equal(expectedString, actualString);
        }

        [ExcludeFromCodeCoverage]
        public static IEnumerable<object[]> GetInputDataForPresidencyRangeTest()
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
        [MemberData(nameof(GetLeftOfficeInputDataForLeftOfficeParserTests))]
        public void LeftOfficeParser_ReturnsRightDateTimeNullable(DateTime? expectedDate, DateTime? leftOfficeDate)
        {
            var presidentViewCalculator = new PresidentViewCalculator();

            var actualDate = presidentViewCalculator.LeftOfficeParser(leftOfficeDate);

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
