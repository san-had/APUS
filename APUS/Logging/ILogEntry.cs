namespace APUS.Logging
{
    public interface ILogEntry
    {
        int RecordNum { get; set; }

        string FileName { get; set; }

        string Parser { get; set; }

        string OutputFormatter { get; set; }
    }
}