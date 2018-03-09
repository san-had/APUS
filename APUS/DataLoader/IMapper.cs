namespace APUS.DataLoader
{
    using System.Collections.Generic;

    public interface IMapper
    {
        IEnumerable<Models.President> Map(IEnumerable<DataAccess.DbModels.DbPresident> dbPresidents);
    }
}