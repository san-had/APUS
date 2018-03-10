namespace APUS.DataLoader
{
    using System.Collections.Generic;

    public interface IOfficerDataMapper
    {
        IEnumerable<Models.Officer> Map(IEnumerable<CommonDataAccess.CommonDbOfficer> commonDbOfficers);
    }
}