namespace APUS
{    
    using APUS.OutputFormatters;
    using APUS.ViewModels;
    using System.Collections.Generic;

    public class ReportGenerator
    {
        private readonly DataAccess.IDataAccess dataAccess;

        private readonly IOutputFormatter outputFormatter;

        private readonly ViewModelLoader viewModelLoader;

        public ReportGenerator()
        {
            this.dataAccess = new DataAccess.CsvDataAccess();
            this.outputFormatter = new StdOutputFormatter();
            this.viewModelLoader = new ViewModelLoader();
        }

        public void Run()
        {
            var dbPresidents = dataAccess.GetDbPresidents();

            var presidents = AutoMapper.Mapper.Map<IEnumerable<DataAccess.DbPresident>, IEnumerable<Models.President>>(dbPresidents);

            var presidentViewList = this.viewModelLoader.UpdateViewPresidents(presidents);

            this.outputFormatter.RenderOutput(presidentViewList);
        }
    }
}
