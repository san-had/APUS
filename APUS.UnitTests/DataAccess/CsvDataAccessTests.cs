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
            var fakeCsvDataAcces = new FakeCsvDataAccess();

            var actualDbPersonsRowCount = fakeCsvDataAcces.GetDbPresidents().Count();

            var expectedRowCount = 3;

            Assert.Equal(expectedRowCount, actualDbPersonsRowCount);
        }
    }
}