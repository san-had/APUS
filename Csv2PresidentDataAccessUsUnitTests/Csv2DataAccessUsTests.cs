﻿namespace APUS.UnitTests.DataAccess
{
    using Csv2PresidentDataAccessUs;
    using System.Linq;
    using Xunit;

    public class Csv2PresidentDataAccessUsTests
    {
        [Fact]
        public void Csv2PresidentDataAccess_ReadsFile_ReturnsRightRowCount()
        {
            var csv2PresidentDataAccesUs = new Csv2PresidentDataAccessUs(Constants.Csv2DataFileName);

            var actualDbCommonOfficersRowCount = csv2PresidentDataAccesUs.GetCommonDbOfficers().Count();

            var expectedRowCount = 45;

            Assert.Equal(expectedRowCount, actualDbCommonOfficersRowCount);
        }

        [Fact]
        public void CsvDataAccess2_ReadsFile_ReturnsDbPresidentType()
        {
            var csv2PresidentDataAccesUs = new Csv2PresidentDataAccessUs(Constants.Csv2DataFileName);

            var actualDbCommonOfficersTypeName = csv2PresidentDataAccesUs.GetCommonDbOfficers().First().GetType().ToString();

            var expectedTypeName = "CommonDataAccess.CommonDbOfficer";

            Assert.Equal(expectedTypeName, actualDbCommonOfficersTypeName);
        }
    }
}