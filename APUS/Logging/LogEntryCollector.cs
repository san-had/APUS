namespace APUS.Logging
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed class LogEntryCollector
    {
        private static readonly LogEntryCollector singletonCollector = new LogEntryCollector();

        private IList<ILogEntry> logEntryList;

        private LogEntryCollector()
        {
            logEntryList = new List<ILogEntry>();
        }

        public static LogEntryCollector GetInstance()
        {
            return singletonCollector;
        }

        public void AddLogEntry(ILogEntry logEntry)
        {
            logEntryList.Add(logEntry);
        }

        public void UpdateLastLogEntry(ILogEntry logEntry)
        {
            if (logEntryList != null && logEntryList.Count > 0)
            {
                var lastIndex = logEntryList.Count - 1;
                var lastLogEntry = logEntryList[lastIndex];

                if (logEntry.RecordNum != 0)
                {
                    lastLogEntry.RecordNum = logEntry.RecordNum;
                }

                if (!string.IsNullOrWhiteSpace(logEntry.FileName))
                {
                    lastLogEntry.FileName = logEntry.FileName;
                }

                if (!string.IsNullOrWhiteSpace(logEntry.Parser))
                {
                    lastLogEntry.Parser = logEntry.Parser;
                }

                if (!string.IsNullOrWhiteSpace(logEntry.ViewModelFormat))
                {
                    lastLogEntry.ViewModelFormat = logEntry.ViewModelFormat;
                }

                if (!string.IsNullOrWhiteSpace(logEntry.OutputFormatter))
                {
                    lastLogEntry.OutputFormatter = logEntry.OutputFormatter;
                }
            }
            else
            {
                AddLogEntry(logEntry);
            }
        }

        public string WriteLogEntryListToString()
        {
            StringBuilder sb = new StringBuilder();

            var sumRecord = logEntryList.Select(x => x.RecordNum).Sum();

            sb.Append($"{sumRecord} records have been processed; ");

            foreach (var logEntry in logEntryList)
            {
                sb.Append(
                    $"{logEntry.RecordNum} records from {logEntry.FileName}," +
                    $" with parser: {logEntry.Parser}," +
                    $" with viewModelFormat: {logEntry.ViewModelFormat}," +
                    $" output formatter: {logEntry.OutputFormatter}; ");
            }
            return sb.ToString();
        }

        public void WriteLogEntryList()
        {
            var logEntryString = WriteLogEntryListToString();

            var logger = Logger.GetInstance();
            logger.WriteLog(logEntryString);
        }
    }
}