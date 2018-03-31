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

        private readonly ILogEntry logEntry;

        public OfficerDataMapper(IDateParser dateParser, ILogEntry logEntry)
        {
            this.dateParser = dateParser ?? throw new ArgumentNullException(nameof(dateParser));
            this.logEntry = logEntry ?? throw new ArgumentNullException(nameof(logEntry));
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
            logEntry.Parser = this.dateParser.GetType().Name;
            var logger = Logger.GetInstance();
            logger.UpdateLastLogEntry(logEntry);
        }
    }
}