namespace APUS.UnitTests.ViewModels.Calculation
{
    using APUS.ViewModels.Calculation;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    public class InOfficeDaysCalculatorTests
    {
        [Theory]
        [MemberData(nameof(GetInputDataForInOfficeDaysCalculatorTests))]
        public void CalculateNumberOfInOfficeDays_ReturnsCalculatedDays(int expecteddays, DateTime? tookOffice, DateTime? leftOffice)
        {
            var inOfficeDaysCalculator = new InOfficeDaysCalculator();

            var actualDays = inOfficeDaysCalculator.CalculateNumberOfInOfficeDays(tookOffice, leftOffice);

            Assert.Equal(expecteddays, actualDays);
        }

        [ExcludeFromCodeCoverage]
        public static IEnumerable<object[]> GetInputDataForInOfficeDaysCalculatorTests()
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
    }
}