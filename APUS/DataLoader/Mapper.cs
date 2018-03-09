namespace APUS.DataLoader
{
    using System.Collections.Generic;
    using System;
    using APUS.Models;
    using APUS.DataAccess.DbModels;

    public class Mapper : IMapper
    {
        private readonly IDateParser dateParser;

        public Mapper(IDateParser dateParser)
        {
            this.dateParser = dateParser ?? throw new ArgumentNullException(nameof(dateParser));
        }

        public IEnumerable<President> Mapping(IEnumerable<DbPresident> dbPresidents)
        {
            foreach (var dbPresident in dbPresidents)
            {
                var president = new President();
                president.FirstName = dbPresident.FirstName;
                president.LastName = dbPresident.LastName;
                president.TookOffice = this.dateParser.ParseDate(dbPresident.TookOffice);
                president.LeftOffice = this.dateParser.ParseDate(dbPresident.LeftOffice);

                yield return president;
            }
        }
    }
}