namespace Csv2PresidentDataAccess
{
    using CommonDataAccess;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Csv2PresidentDataAccess : ICommonDataAccess
    {
        public IEnumerable<CommonDbOfficer> GetCommonDbOfficers()
        {
            return File.ReadAllLines(Constants.Csv2DataFileName)
                .Skip(1)
                .Select(line => line.Split(';'))
                .Select(x => new CommonDbOfficer
                {
                    Party = x[0].Trim(),
                    FirstName = x[1].Trim(),
                    LastName = x[2].Trim(),
                    TookOffice = x[3].Trim(),
                    LeftOffice = x[4].Trim(),
                    DataType = "P"
                });
        }
    }
}