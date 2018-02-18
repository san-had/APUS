namespace APUS.UnitTests.DataAccess
{
    using APUS.DataAccess;
    using System.Linq;
    using Xunit;

    public class CsvDataAccessTests
    {
        [Fact]
        public void CsvDataAccess_ReadsFile_ReturnsDbPersons()
        {
            var csvDataAcces = new CsvDataAccess();

            var actualDbPersonsRowCount = csvDataAcces.GetDbPresidents().Count();

            var expectedRowCount = 45;

            Assert.Equal(expectedRowCount, actualDbPersonsRowCount);

        }
    }
}
