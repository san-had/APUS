namespace APUS.DataAccess
{
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
                    FirstName = x[0],
                    LastName = x[1],
                    TookOffice = x[2],
                    LeftOffice = x[3],
                    Party = x[4]                    
                });
        }
    }
}