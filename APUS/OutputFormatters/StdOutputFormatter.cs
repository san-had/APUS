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

        public void RenderOutput(IEnumerable<OfficerView> officerViewList)
        {
            consoleWriter.WriteLine(Constants.CsvOutputHeader);

            foreach (var officerView in officerViewList)
            {
                consoleWriter.WriteLine($"{officerView.LastName.ToUpper()},{officerView.FirstName},{officerView.InOfficeRange},{officerView.NumberOfInOfficeDays} days");
            }
        }
    }
}