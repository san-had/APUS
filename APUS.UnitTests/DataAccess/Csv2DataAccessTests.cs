namespace APUS.UnitTests.DataAccess
{
    using APUS.DataAccess;
    using System.Linq;
    using Xunit;

    public class Csv2DataAccessTests
    {
        [Fact]
        public void CsvDataAccess2_ReadsFile_ReturnsDbPersons()
        {
            var csv2DataAcces = new Csv2DataAccess();

            var actualDbPersonsRowCount = csv2DataAcces.GetDbPresidents().Count();

            var expectedRowCount = 45;

            Assert.Equal(expectedRowCount, actualDbPersonsRowCount);
        }
    }
}