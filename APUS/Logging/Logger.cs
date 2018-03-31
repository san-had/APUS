namespace APUS.Logging
{
    using log4net;
    using log4net.Config;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public sealed class Logger
    {
        private ILog logger;

        private static readonly Logger singletonLogger = new Logger();

        private List<ILogEntry> logEntryList;

        private Logger()
        {
            LoggerSetup();
            logEntryList = new List<ILogEntry>();
        }

        public static Logger GetInstance()
        {
            return singletonLogger;
        }

        private void LoggerSetup()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            logger = LogManager.GetLogger(typeof(Logger));
        }

        public void WriteLog(string message)
        {
            logger.Info(message);
        }

        public void WriteLogEntry(ILogEntry logEntry)
        {
            logger.Info("Writing logEntry");
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

        public void WriteLogEntryList()
        {
            StringBuilder sb = new StringBuilder();

            var sumRecord = logEntryList.Select(x => x.RecordNum).Sum();

            sb.Append($"{sumRecord} records have been processed; ");

            foreach (var logEntry in logEntryList)
            {
                sb.Append(
                    $"{logEntry.RecordNum} from {logEntry.FileName}," +
                    $" with parser: {logEntry.Parser}," +
                    $" with viewModelFormat: {logEntry.ViewModelFormat}," +
                    $" output formatter: {logEntry.OutputFormatter}; ");
            }

            logger.Info(sb.ToString());
        }
    }
}