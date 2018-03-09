namespace APUS.UnitTests.DataAccess
{
    using APUS.DataAccess;
    using System.Linq;
    using Xunit;

    public class CsvDataAccessTests
    {
        [Fact]
        public void CsvDataAccess_ReadsFile_ReturnsRightRowNumber()
        {
            var csvDataAcces = new CsvDataAccess();

            var actualDbPersonsRowCount = csvDataAcces.GetDbPresidents().Count();

            var expectedRowCount = 45;

            Assert.Equal(expectedRowCount, actualDbPersonsRowCount);
        }

        [Fact]
        public void CsvDataAccess_ReadsFile_ReturnsDbPresidentType()
        {
            var csvDataAcces = new CsvDataAccess();

            var actualDbPersonsTypeName = csvDataAcces.GetDbPresidents().First().GetType().ToString();

            var expectedTypeName = "APUS.DataAccess.DbModels.DbPresident";

            Assert.Equal(expectedTypeName, actualDbPersonsTypeName);
        }
    }
}