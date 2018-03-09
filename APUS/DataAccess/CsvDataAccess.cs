namespace APUS.DataAccess
{
    using APUS.DataAccess.DbModels;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CsvDataAccess : IDataAccess
    {
        public IEnumerable<DbPresident> GetDbPresidents()
        {
            return File.ReadAllLines(Constants.CsvDataFileName)
                .Skip(1)
                .Select(line => line.Split(','))
                .Select(x => new DbPresident
                {
                    FirstName = x[0].Trim(),
                    LastName = x[1].Trim(),
                    TookOffice = x[2].Trim(),
                    LeftOffice = x[3].Trim(),
                    Party = x[4].Trim()
                });
        }
    }
}