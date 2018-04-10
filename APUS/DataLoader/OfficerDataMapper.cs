namespace APUS.DataLoader
{
    using System.Collections.Generic;
    using System;
    using APUS.Models;
    using CommonDataAccess;
    using APUS.Logging;

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

        public void WriteLog()
        {
            var logEntry = new LogEntry();
            logEntry.Parser = this.dateParser.GetType().Name;
            var logCollector = LogEntryCollector.GetInstance();
            logCollector.UpdateLastLogEntry(logEntry);
        }
    }
}