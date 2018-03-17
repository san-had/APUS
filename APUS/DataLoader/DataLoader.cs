namespace APUS.DataLoader
{
    using System;
    using System.Collections.Generic;
    using CommonDataAccess;
    using APUS.Models;

    public class DataLoader : IDataLoader
    {
        private readonly ICommonDataAccess dataAccess;
        private readonly IOfficerDataMapper mapper;

        public DataLoader(ICommonDataAccess dataAccess, IOfficerDataMapper mapper)
        {
            this.dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<Officer> LoadData()
        {
            return this.mapper.Map(dataAccess.GetCommonDbOfficers());
        }
    }
}