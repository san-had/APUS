namespace APUS.DataLoader
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using APUS.DataAccess;
    using APUS.Models;

    public class DataLoader : IDataLoader
    {
        public IEnumerable<President> LoadData(IEnumerable<DbPresident> dbPresidents)
        {
            throw new NotImplementedException();
        }
    }
}