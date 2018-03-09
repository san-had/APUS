namespace APUS.DataAccess
{
    using APUS.DataAccess.DbModels;
    using System.Collections.Generic;

    public interface IDataAccess
    {
        IEnumerable<DbPresident> GetDbPresidents();
    }
}