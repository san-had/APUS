namespace APUS.OutputFormatters
{
    using System;
    using System.Data;
    using APUS.ViewModels;
    using ConsoleTableExt;

    public class ConsoleTableOutputFormatter : IOutputFormatter
    {
        private readonly IConsoleWriter consoleWriter;

        public ConsoleTableOutputFormatter(IConsoleWriter consoleWriter)
        {
            this.consoleWriter = consoleWriter ?? throw new ArgumentNullException(nameof(consoleWriter));
        }

        public void RenderOutput(OfficerViewModel officerViewModel)
        {
            PrintTable(officerViewModel);
        }

        public void PrintTable(OfficerViewModel officerViewModel)
        {
            consoleWriter.WriteLine(string.Empty);
            consoleWriter.WriteLine("Officer list in table format");
            var tableBuilder = ConsoleTableBuilder.From(OfficerTableData(officerViewModel));
            tableBuilder.ExportAndWriteLine();
        }

        private DataTable OfficerTableData(OfficerViewModel officerViewModel)
        {
            DataTable table = new DataTable();
            foreach (var col in officerViewModel.OfficerViewHeader)
            {
                table.Columns.Add(col, typeof(string));
            }

            foreach (var officer in officerViewModel.OfficerViewRows)
            {
                table.Rows.Add(officer.Col1, officer.Col2, officer.Col3, officer.Col4);
            }

            return table;
        }
    }
}