namespace APUS.UnitTests.DataAccess
{
    using APUS.CommonDataAccess;
    using System.Collections.Generic;

    public class FakeCsvPresidentDataAccess : ICommonDataAccess
    {
        public IEnumerable<CommonDbOfficer> GetCommonDbOfficers()
        {
            var dbCommonOfficer1 = new CommonDbOfficer
            {
                FirstName = "George",
                LastName = "Washington",
                TookOffice = "30/04/1789",
                LeftOffice = "04/03/1797",
                Party = "Independent"
            };

            yield return dbCommonOfficer1;

            var dbCommonOfficer2 = new CommonDbOfficer
            {
                FirstName = "John",
                LastName = "Adams",
                TookOffice = "04/03/1797",
                LeftOffice = "04/03/1801",
                Party = "Federalist"
            };

            yield return dbCommonOfficer2;

            var dbCommonOfficer3 = new CommonDbOfficer
            {
                FirstName = "Donald",
                LastName = "Trump",
                TookOffice = "20/01/2017",
                LeftOffice = "Incumbent",
                Party = "Republican"
            };

            yield return dbCommonOfficer3;
        }
    }
}