namespace APUS.Parsers
{
    using System;
    using System.Collections.Generic;
    using APUS.DataAccess;
    using APUS.Models;

    public class CsvParser : IParser
    {
        public IEnumerable<President> Parse(IEnumerable<DbPresident> dbPresidents)
        {
            var presidents = AutoMapper.Mapper.Map<IEnumerable<DataAccess.DbPresident>, IEnumerable<Models.President>>(dbPresidents);
            return presidents;
        }
    }
}