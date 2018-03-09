namespace APUS.DataLoader
{
    using System.Collections.Generic;

    public interface IMapper
    {
        IEnumerable<Models.President> Mapping(IEnumerable<DataAccess.DbModels.DbPresident> dbPresidents);
    }
}