namespace APUS
{
    using APUS.OutputFormatters;
    using APUS.ViewModels;
    using System;
    using System.Collections.Generic;

    public class ReportGenerator : IReportGenerator
    {
        private readonly DataAccess.IDataAccess dataAccess;

        private readonly IPresidentViewLoader presidentViewLoader;

        private readonly IOutputFormatter outputFormatter;

        public ReportGenerator(DataAccess.IDataAccess dataAccess, IPresidentViewLoader presidentViewLoader, IOutputFormatter outputFormatter)
        {
            if (dataAccess == null)
            {
                throw new ArgumentNullException(nameof(dataAccess));
            }

            if (presidentViewLoader == null)
            {
                throw new ArgumentNullException(nameof(presidentViewLoader));
            }

            if (outputFormatter == null)
            {
                throw new ArgumentNullException(nameof(outputFormatter));
            }

            this.dataAccess = dataAccess;
            this.presidentViewLoader = presidentViewLoader;
            this.outputFormatter = outputFormatter;
        }

        public void CreateReport()
        {
            var dbPresidents = dataAccess.GetDbPresidents();

            var presidents = AutoMapper.Mapper.Map<IEnumerable<DataAccess.DbPresident>, IEnumerable<Models.President>>(dbPresidents);

            var presidentViewList = this.presidentViewLoader.UpdateViewPresidents(presidents);

            this.outputFormatter.RenderOutput(presidentViewList);
        }
    }
}