namespace APUS.Logging
{
    using System.Collections.Generic;

    public interface ILogReportInfo
    {
        int AllRecordNum { get; set; }

        IEnumerable<LogEntry> LogEntries { get; set; }
    }
}