﻿namespace CsvPresidentDataAccessUnitTests
{
    using CsvPresidentDataAccess;
    using System.Linq;
    using Xunit;

    public class CsvDataAccessTests
    {
        [Fact]
        public void CsvPresidentDataAccess_ReadsFile_ReturnsRightRowNumber()
        {
            var csvPresidentDataAcces = new CsvPresidentDataAccess();

            var actualDbPersonsRowCount = csvPresidentDataAcces.GetCommonDbOfficers().Count();

            var expectedRowCount = 45;

            Assert.Equal(expectedRowCount, actualDbPersonsRowCount);
        }

        [Fact]
        public void CsvPresidentDataAccess_ReadsFile_ReturnsDbPresidentType()
        {
            var csvPresidentDataAcces = new CsvPresidentDataAccess();

            var actualDbPersonsTypeName = csvPresidentDataAcces.GetCommonDbOfficers().First().GetType().ToString();

            var expectedTypeName = "CommonDataAccess.CommonDbOfficer";

            Assert.Equal(expectedTypeName, actualDbPersonsTypeName);
        }
    }
}