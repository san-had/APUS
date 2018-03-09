namespace APUS.OutputFormatters
{
    using System.Collections.Generic;
    using APUS.ViewModels;

    public class StdOutputFormatter : IOutputFormatter
    {
        private readonly IConsoleWriter consoleWriter;

        public StdOutputFormatter(IConsoleWriter consoleWriter)
        {
            this.consoleWriter = consoleWriter;
        }

        public void RenderOutput(IEnumerable<OfficerView> presidentViewList)
        {
            consoleWriter.WriteLine(Constants.CsvOutputHeader);

            foreach (var presidentView in presidentViewList)
            {
                consoleWriter.WriteLine($"{presidentView.LastName.ToUpper()},{presidentView.FirstName},{presidentView.InOfficeRange},{presidentView.NumberOfInOfficeDays} days");
            }
        }
    }
}