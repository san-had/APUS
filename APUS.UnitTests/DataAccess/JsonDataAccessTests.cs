﻿namespace APUS.UnitTests.DataAccess
{
    using APUS.DataAccess;
    using System.Linq;
    using Xunit;

    public class JsonDataAccessTests
    {
        [Fact]
        public void JsonMayorDataAccess_ReadsFile_ReturnsRightRowNumber()
        {
            var jsonMayorDataAcces = new JsonMayorDataAccess();

            var actualDbPersonsRowCount = jsonMayorDataAcces.GetCommonDbOfficers().Count();

            var expectedRowCount = 4;

            Assert.Equal(expectedRowCount, actualDbPersonsRowCount);
        }

        [Fact]
        public void JsonMayorDataAccess_ReadsFile_ReturnsDbPresidentType()
        {
            var jsonMayorDataAcces = new JsonMayorDataAccess();

            var actualDbPersonsTypeName = jsonMayorDataAcces.GetCommonDbOfficers().First().GetType().ToString();

            var expectedTypeName = "APUS.CommonDataAccess.CommonDbOfficer";

            Assert.Equal(expectedTypeName, actualDbPersonsTypeName);
        }
    }
}