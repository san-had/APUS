namespace APUS.DataLoader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using APUS.Logging;
    using APUS.Models;
    using CommonDataAccess;

    public class DataLoader : IDataLoader, ILogging
    {
        private readonly ICommonDataAccess dataAccess;
        private readonly IOfficerDataMapper mapper;

        public DataLoader(ICommonDataAccess dataAccess, IOfficerDataMapper mapper)
        {
            this.dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<Officer> LoadData()
        {
            WriteLog();

            return this.mapper.Map(dataAccess.GetCommonDbOfficers());
        }

        public void WriteLog()
        {
            var record = new OfficerProcessingRecord();
            record.RecordNum = dataAccess.GetCommonDbOfficers().ToList().Count();
            var recordCollector = RecordCollector.GetInstance();
            recordCollector.UpdateLastRecord(record);
        }
    }
}