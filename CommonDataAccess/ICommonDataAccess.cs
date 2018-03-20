namespace CommonDataAccess
{
    using System.Collections.Generic;

    public interface ICommonDataAccess
    {
        bool CanDo();

        IEnumerable<CommonDbOfficer> GetCommonDbOfficers();
    }
}