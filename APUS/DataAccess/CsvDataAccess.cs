namespace APUS.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using APUS.Model;

    public class CsvDataAccess : IDataAccess
    {
        public IEnumerable<DbPresident> GetDbPresidents()
        {
            return File.ReadAllLines(@"D:\APUS_DATA\data.csv")
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