﻿namespace APUS.DataAccess
{
    using CommonDataAccess;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CsvPresidentDataAccess : ICommonDataAccess
    {
        public IEnumerable<CommonDbOfficer> GetCommonDbOfficers()
        {
            return File.ReadAllLines(Constants.CsvDataFileName)
                .Skip(1)
                .Select(line => line.Split(','))
                .Select(x => new CommonDbOfficer
                {
                    FirstName = x[0].Trim(),
                    LastName = x[1].Trim(),
                    TookOffice = x[2].Trim(),
                    LeftOffice = x[3].Trim(),
                    Party = x[4].Trim(),
                    DataType = "P"
                });
        }
    }
}