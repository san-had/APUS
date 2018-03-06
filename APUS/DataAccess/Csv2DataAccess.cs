namespace APUS.DataAccess
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Csv2DataAccess : IDataAccess
    {
        public IEnumerable<DbPresident> GetDbPresidents()
        {
            return File.ReadAllLines(Constants.Csv2DataFileName)
                .Skip(1)
                .Select(line => line.Split(';'))
                .Select(x => new DbPresident
                {
                    Party = x[0].Trim(),
                    FirstName = x[1].Trim(),
                    LastName = x[2].Trim(),
                    TookOffice = x[3].Trim(),
                    LeftOffice = x[4].Trim()
                });
        }
    }
}