namespace APUS.DataLoader
{
    using System.Collections.Generic;

    public interface IMapper
    {
        IEnumerable<Models.Officer> Map(IEnumerable<DataAccess.DbModels.DbPresident> dbPresidents);
    }
}