namespace APUS.UnitTests.DataAccess
{
    using APUS.DataAccess;
    using System.Linq;
    using Xunit;

    public class Csv2DataAccessTests
    {
        [Fact]
        public void CsvDataAccess2_ReadsFile_ReturnsRightRowCount()
        {
            var csv2DataAcces = new Csv2DataAccess();

            var actualDbPersonsRowCount = csv2DataAcces.GetDbPresidents().Count();

            var expectedRowCount = 45;

            Assert.Equal(expectedRowCount, actualDbPersonsRowCount);
        }

        [Fact]
        public void CsvDataAccess2_ReadsFile_ReturnsDbPresidentType()
        {
            var csv2DataAcces = new Csv2DataAccess();

            var actualDbPersonsTypeName = csv2DataAcces.GetDbPresidents().First().GetType().ToString();

            var expectedTypeName = "APUS.DataAccess.DbPresident";

            Assert.Equal(expectedTypeName, actualDbPersonsTypeName);
        }
    }
}