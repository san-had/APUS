namespace APUS.Logging
{
    public class OfficerProcessingRecord
    {
        public int RecordNum { get; set; }

        public string FileName { get; set; }

        public string FileNameCaller { get; set; }

        public string FileNameRecordingTime { get; set; }

        public string Parser { get; set; }

        public string ParserCaller { get; set; }

        public string ParserRecordingTime { get; set; }

        public string ViewModelFormat { get; set; }

        public string OutputFormatter { get; set; }

        public string ReportGenCaller { get; set; }

        public string ReportGenRecordingTime { get; set; }
    }
}