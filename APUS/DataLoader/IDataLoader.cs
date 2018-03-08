namespace APUS.DataLoader
{
    using System.Collections.Generic;

    public interface IDataLoader
    {
        IEnumerable<Models.President> LoadData(IEnumerable<DataAccess.DbPresident> dbPresidents);
    }
}