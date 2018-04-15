namespace CsvPresidentDataAccessEn
{
    using CommonDataAccess;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CsvPresidentDataAccessEn : ICommonDataAccess
    {
        private readonly string fileName;

        public CsvPresidentDataAccessEn(string fileName)
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
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"File is not found: {fileName}");
            }

            return File.ReadAllLines(fileName)
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