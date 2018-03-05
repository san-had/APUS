namespace APUS.Parsers
{
    using System.Collections.Generic;

    public interface IParser
    {
        IEnumerable<Models.President> Parse(IEnumerable<DataAccess.DbPresident> dbPresidents);
    }
}