namespace APUS.UnitTests.DataAccess
{
    using APUS.DataAccess;
    using APUS.DataAccess.DbModels;
    using System.Collections.Generic;

    public class FakeCsvDataAccess : IDataAccess
    {
        public IEnumerable<DbPresident> GetDbPresidents()
        {
            var dbPresident1 = new DbPresident
            {
                FirstName = "George",
                LastName = "Washington",
                TookOffice = "30/04/1789",
                LeftOffice = "04/03/1797",
                Party = "Independent"
            };

            yield return dbPresident1;

            var dbPresident2 = new DbPresident
            {
                FirstName = "John",
                LastName = "Adams",
                TookOffice = "04/03/1797",
                LeftOffice = "04/03/1801",
                Party = "Federalist"
            };

            yield return dbPresident2;

            var dbPresident3 = new DbPresident
            {
                FirstName = "Donald",
                LastName = "Trump",
                TookOffice = "20/01/2017",
                LeftOffice = "Incumbent",
                Party = "Republican"
            };

            yield return dbPresident3;
        }
    }
}