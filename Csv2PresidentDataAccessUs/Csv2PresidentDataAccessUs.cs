namespace Csv2PresidentDataAccessUs
{
    using CommonDataAccess;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Csv2PresidentDataAccessUs : ICommonDataAccess
    {
        private readonly string fileName;

        public Csv2PresidentDataAccessUs(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException($"Null or empty fileName.");
            }

            this.fileName = fileName;
        }

        public bool CanDo()
        {
            bool isCanDo = false;

            if (fileName.ToUpper().EndsWith(Constants.MatchingFiles))
            {
                isCanDo = true;
            }

            return isCanDo;
        }

        public IEnumerable<CommonDbOfficer> GetCommonDbOfficers()
        {
            if (!File.Exists(this.fileName))
            {
                throw new FileNotFoundException($"File is not found: {this.fileName}");
            }

            return File.ReadAllLines(fileName)
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