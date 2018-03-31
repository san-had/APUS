﻿namespace APUS.DataLoader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using APUS.Logging;
    using APUS.Models;
    using CommonDataAccess;

    public class DataLoader : IDataLoader, ILogging
    {
        private readonly ICommonDataAccess dataAccess;
        private readonly IOfficerDataMapper mapper;
        private readonly ILogEntry logEntry;

        public DataLoader(ICommonDataAccess dataAccess, IOfficerDataMapper mapper, ILogEntry logEntry)
        {
            this.dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logEntry = logEntry ?? throw new ArgumentNullException(nameof(logEntry));
        }

        public IEnumerable<Officer> LoadData()
        {
            WriteLog();

            return this.mapper.Map(dataAccess.GetCommonDbOfficers());
        }

        public void WriteLog()
        {
            logEntry.RecordNum = dataAccess.GetCommonDbOfficers().ToList().Count();
            var logger = Logger.GetInstance();
            logger.UpdateLastLogEntry(logEntry);
        }
    }
}