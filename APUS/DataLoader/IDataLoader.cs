namespace APUS.DataLoader
{
    using System.Collections.Generic;

    public interface IDataLoader
    {
        IEnumerable<Models.Officer> LoadData();
    }
}