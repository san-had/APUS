namespace APUS.DataAccess
{
    using APUS.Model;
    using System.Collections.Generic;

    public interface IDataAccess
    {
        IEnumerable<DbPresident> GetDbPresidents();
    }
}