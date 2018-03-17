namespace CommonDataAccess
{
    using System.Collections.Generic;

    public interface ICommonDataAccess
    {
        IEnumerable<CommonDbOfficer> GetCommonDbOfficers();
    }
}