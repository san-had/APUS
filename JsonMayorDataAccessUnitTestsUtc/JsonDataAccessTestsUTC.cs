namespace APUS.UnitTests.DataAccess
{
    using JsonMayorDataAccessUtc;
    using System.Linq;
    using Xunit;

    public class JsonDataAccessTestsUtc
    {
        [Fact]
        public void JsonMayorDataAccessUtc_ReadsFile_ReturnsRightRowNumber()
        {
            var jsonMayorDataAccesUtc = new JsonMayorDataAccessUtc(Constants.JsonDataFileName);

            var actualDbPersonsRowCount = jsonMayorDataAccesUtc.GetCommonDbOfficers().Count();

            var expectedRowCount = 4;

            Assert.Equal(expectedRowCount, actualDbPersonsRowCount);
        }

        [Fact]
        public void JsonMayorDataAccessUtc_ReadsFile_ReturnsDbPresidentType()
        {
            var jsonMayorDataAccesUtc = new JsonMayorDataAccessUtc(Constants.JsonDataFileName);

            var actualDbPersonsTypeName = jsonMayorDataAccesUtc.GetCommonDbOfficers().First().GetType().ToString();

            var expectedTypeName = "CommonDataAccess.CommonDbOfficer";

            Assert.Equal(expectedTypeName, actualDbPersonsTypeName);
        }
    }
}