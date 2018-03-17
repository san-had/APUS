namespace APUS.DataLoader
{
    using System.Collections.Generic;
    using System;
    using APUS.Models;
    using CommonDataAccess;

    public class OfficerDataMapper : IOfficerDataMapper
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
        }
    }
}