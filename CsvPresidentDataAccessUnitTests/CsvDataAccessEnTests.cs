namespace CsvPresidentDataAccessUnitTests
{
    using CsvPresidentDataAccess;
    using System.Linq;
    using Xunit;

    public class CsvDataAccessEnTests
    {
        [Fact]
        public void CsvPresidentDataAccessEn_ReadsFile_ReturnsRightRowNumber()
        {
            var csvPresidentDataAccesEn = new CsvPresidentDataAccessEn();

            var actualDbPersonsRowCount = csvPresidentDataAccesEn.GetCommonDbOfficers().Count();

            var expectedRowCount = 45;

            Assert.Equal(expectedRowCount, actualDbPersonsRowCount);
        }

        [Fact]
        public void CsvPresidentDataAccessEn_ReadsFile_ReturnsDbPresidentType()
        {
            var csvPresidentDataAccesEn = new CsvPresidentDataAccessEn();

            var actualDbPersonsTypeName = csvPresidentDataAccesEn.GetCommonDbOfficers().First().GetType().ToString();

            var expectedTypeName = "CommonDataAccess.CommonDbOfficer";

            Assert.Equal(expectedTypeName, actualDbPersonsTypeName);
        }
    }
}