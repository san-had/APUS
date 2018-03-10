namespace APUS.UnitTests.DataAccess
{
    using System.Linq;
    using Xunit;

    public class FakeCsvDataAccessTests
    {
        [Fact]
        public void FakeCsvPresidentDataAccess_ReadsFile_ReturnsRightRowCount()
        {
            var fakeCsvPresidentDataAcces = new FakeCsvPresidentDataAccess();

            var actualDbPersonsRowCount = fakeCsvPresidentDataAcces.GetCommonDbOfficers().Count();

            var expectedRowCount = 3;

            Assert.Equal(expectedRowCount, actualDbPersonsRowCount);
        }

        [Fact]
        public void FakeCsvPresidentDataAccess_ReadsFile_ReturnsDbPresidentType()
        {
            var fakeCsvPresidentDataAcces = new FakeCsvPresidentDataAccess();

            var actualDbPersonsTypeName = fakeCsvPresidentDataAcces.GetCommonDbOfficers().First().GetType().ToString();

            var expectedTypeName = "APUS.CommonDataAccess.CommonDbOfficer";

            Assert.Equal(expectedTypeName, actualDbPersonsTypeName);
        }
    }
}