namespace APUS.UnitTests.DataAccess
{
    using APUS.DataAccess;
    using System;
    using System.Linq;
    using Xunit;

    public class JsonDataAccessTests
    {
        [Fact]
        public void JsonDataAccess_ReadsFile_ReturnsRightRowNumber()
        {
            var jsonDataAcces = new JsonDataAccess();

            var actualDbPersonsRowCount = jsonDataAcces.GetDbPresidents().Count();

            var expectedRowCount = 4;

            Assert.Equal(expectedRowCount, actualDbPersonsRowCount);
        }

        [Fact]
        public void JsonDataAccess_ReadsFile_ReturnsDbPresidentType()
        {
            var jsonDataAcces = new JsonDataAccess();

            var actualDbPersonsTypeName = jsonDataAcces.GetDbPresidents().First().GetType().ToString();

            var expectedTypeName = "APUS.DataAccess.DbModels.DbPresident";

            Assert.Equal(expectedTypeName, actualDbPersonsTypeName);
        }
    }
}