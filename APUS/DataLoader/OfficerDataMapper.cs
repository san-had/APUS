namespace APUS.DataLoader
{
    using System.Collections.Generic;
    using System;
    using APUS.Models;
    using CommonDataAccess;
    using APUS.Logging;
    using System.Runtime.CompilerServices;

    public class OfficerDataMapper : IOfficerDataMapper, ILogging
    {
        private readonly IDateParser dateParser;

        public OfficerDataMapper(IDateParser dateParser)
        {
            this.dateParser = dateParser ?? throw new ArgumentNullException(nameof(dateParser));
        }

        public IEnumerable<Officer> Map(IEnumerable<CommonDbOfficer> dbOfficers)
        {
            foreach (var dbOfficer in dbOfficers)
            {
                var officer = new Officer();
                officer.FirstName = dbOfficer.FirstName;
                officer.LastName = dbOfficer.LastName;
                officer.TookOffice = this.dateParser.ParseDate(dbOfficer.TookOffice);
                officer.LeftOffice = this.dateParser.ParseDate(dbOfficer.LeftOffice);
                officer.OfficerType = dbOfficer.DataType;
                yield return officer;
            }
            WriteLog();
        }

        public void WriteLog([CallerMemberName] string callerName = null)
        {
            var record = new OfficerProcessingRecord();
            record.Parser = this.dateParser.GetType().Name;
            record.ParserCaller = callerName;
            record.ParserRecordingTime = DateTime.Now.ToString(Constants.LogDateTimeFormat);
            var recordCollector = RecordCollector.GetInstance();
            recordCollector.UpdateLastRecord(record);
        }
    }
}