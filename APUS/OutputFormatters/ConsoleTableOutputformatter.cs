namespace APUS.OutputFormatters
{
    using System;
    using System.Collections.Generic;
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

        public void RenderOutput(IEnumerable<OfficerView> officerViewList)
        {
            PrintTable(officerViewList);
        }

        public void PrintTable(IEnumerable<OfficerView> officerViewList)
        {
            consoleWriter.WriteLine(string.Empty);
            consoleWriter.WriteLine("Officer list in table format");
            var tableBuilder = ConsoleTableBuilder.From(OfficerTableData(officerViewList));
            tableBuilder.ExportAndWriteLine();
        }

        private DataTable OfficerTableData(IEnumerable<OfficerView> officerViewList)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Last Name", typeof(string));
            table.Columns.Add("First Name", typeof(string));
            table.Columns.Add("In-Office Range", typeof(string));
            table.Columns.Add("# of In-Office Days", typeof(int));

            foreach (var officer in officerViewList)
            {
                table.Rows.Add(officer.Col1, officer.Col2, officer.Col3, officer.Col4);
            }

            return table;
        }
    }
}