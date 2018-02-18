namespace APUS.DataAccess
{
    using System.Collections.Generic;

    public interface IDataAccess
    {
        IEnumerable<DbPresident> GetDbPresidents();
    }
}