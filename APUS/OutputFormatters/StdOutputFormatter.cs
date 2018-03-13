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

        public void RenderOutput(OfficerViewModel officerViewModel)
        {
            WriteHeader(officerViewModel.OfficerViewHeader);

            foreach (var officerView in officerViewModel.OfficerViewRows)
            {
                consoleWriter.WriteLine($"{officerView.Col1},{officerView.Col2},{officerView.Col3},{officerView.Col4}");
            }
        }

        public void WriteHeader(IEnumerable<string> header)
        {
            consoleWriter.WriteLine(string.Join(',', header));
        }
    }
}