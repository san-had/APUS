namespace APUS.UnitTests.DataAccess
{
    using System.Linq;
    using Xunit;

    public class FakeCsvDataAccessTests
    {
        [Fact]
        public void FakeCsvDataAccess_ReadsFile_ReturnsRightRowCount()
        {
            var fakeCsvDataAcces = new FakeCsvDataAccess();

            var actualDbPersonsRowCount = fakeCsvDataAcces.GetDbPresidents().Count();

            var expectedRowCount = 3;

            Assert.Equal(expectedRowCount, actualDbPersonsRowCount);
        }

        [Fact]
        public void FakeCsvDataAccess_ReadsFile_ReturnsDbPresidentType()
        {
            var fakeCsvDataAcces = new FakeCsvDataAccess();

            var actualDbPersonsTypeName = fakeCsvDataAcces.GetDbPresidents().First().GetType().ToString();

            var expectedTypeName = "APUS.DataAccess.DbModels.DbPresident";

            Assert.Equal(expectedTypeName, actualDbPersonsTypeName);
        }
    }
}