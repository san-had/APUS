﻿namespace APUS
{
    using APUS.OutputFormatters;
    using APUS.ViewModels;
    using System.Collections.Generic;

    public class ReportGenerator
    {
        private readonly DataAccess.IDataAccess dataAccess;

        private readonly IPresidentViewLoader presidentViewLoader;

        private readonly IOutputFormatter outputFormatter;

        public ReportGenerator()
        {
            this.dataAccess = new DataAccess.CsvDataAccess();
            this.presidentViewLoader = new PresidentViewLoader();
            this.outputFormatter = new StdOutputFormatter();
        }

        public void Run()
        {
            var dbPresidents = dataAccess.GetDbPresidents();

            var presidents = AutoMapper.Mapper.Map<IEnumerable<DataAccess.DbPresident>, IEnumerable<Models.President>>(dbPresidents);

            var presidentViewList = this.presidentViewLoader.UpdateViewPresidents(presidents);

            this.outputFormatter.RenderOutput(presidentViewList);
        }
    }
}