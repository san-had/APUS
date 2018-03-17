namespace APUS.UnitTests.DataAccess
{
    using APUS.DataAccess;
    using System.Linq;
    using Xunit;

    public class Csv2PresidentDataAccessTests
    {
        [Fact]
        public void Csv2PresidentDataAccess_ReadsFile_ReturnsRightRowCount()
        {
            var csv2PresidentDataAcces = new Csv2PresidentDataAccess();

            var actualDbCommonOfficersRowCount = csv2PresidentDataAcces.GetCommonDbOfficers().Count();

            var expectedRowCount = 45;

            Assert.Equal(expectedRowCount, actualDbCommonOfficersRowCount);
        }

        [Fact]
        public void CsvDataAccess2_ReadsFile_ReturnsDbPresidentType()
        {
            var csv2PresidentDataAcces = new Csv2PresidentDataAccess();

            var actualDbCommonOfficersTypeName = csv2PresidentDataAcces.GetCommonDbOfficers().First().GetType().ToString();

            var expectedTypeName = "CommonDataAccess.CommonDbOfficer";

            Assert.Equal(expectedTypeName, actualDbCommonOfficersTypeName);
        }
    }
}