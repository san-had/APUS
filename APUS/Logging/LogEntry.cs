namespace APUS.Logging
{
    public class LogEntry : ILogEntry
    {
        public int RecordNum { get; set; }

        public string FileName { get; set; }

        public string Parser { get; set; }

        public string OutputFormatter { get; set; }
    }
}