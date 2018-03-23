namespace APUS.Logging
{
    using System.Collections.Generic;

    public class LogReportInfo : ILogReportInfo
    {
        public int AllRecordNum { get; set; }

        public IEnumerable<LogEntry> LogEntries { get; set; }
    }
}