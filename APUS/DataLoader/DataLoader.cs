namespace APUS.DataLoader
{
    using System;
    using System.Collections.Generic;
    using APUS.DataAccess;
    using APUS.Models;

    public class DataLoader : IDataLoader
    {
        private readonly IDataAccess dataAccess;
        private readonly IMapper mapper;

        public DataLoader(IDataAccess dataAccess, IMapper mapper)
        {
            this.dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<Officer> LoadData()
        {
            return this.mapper.Map(dataAccess.GetDbPresidents());
        }
    }
}