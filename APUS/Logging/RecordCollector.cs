namespace APUS.Logging
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed class RecordCollector
    {
        private static readonly RecordCollector singletonRecordCollector = new RecordCollector();

        private IList<OfficerProcessingRecord> recordList;

        private RecordCollector()
        {
            recordList = new List<OfficerProcessingRecord>();
        }

        public static RecordCollector GetInstance()
        {
            return singletonRecordCollector;
        }

        public void AddRecord(OfficerProcessingRecord record)
        {
            recordList.Add(record);
        }

        public void UpdateLastRecord(OfficerProcessingRecord record)
        {
            if (recordList != null && recordList.Count > 0)
            {
                var lastIndex = recordList.Count - 1;
                var lastRecord = recordList[lastIndex];

                if (record.RecordNum != 0)
                {
                    lastRecord.RecordNum = record.RecordNum;
                }

                if (!string.IsNullOrWhiteSpace(record.FileName))
                {
                    lastRecord.FileName = record.FileName;
                }

                if (!string.IsNullOrWhiteSpace(record.FileNameCaller) && lastRecord.FileNameCaller != record.FileNameCaller)
                {
                    lastRecord.FileNameCaller = record.FileNameCaller;
                }

                if (!string.IsNullOrWhiteSpace(record.FileNameRecordingTime) && lastRecord.FileNameRecordingTime != record.FileNameRecordingTime)
                {
                    lastRecord.FileNameRecordingTime = record.FileNameRecordingTime;
                }

                if (!string.IsNullOrWhiteSpace(record.Parser))
                {
                    lastRecord.Parser = record.Parser;
                }

                if (!string.IsNullOrWhiteSpace(record.ParserCaller) && lastRecord.ParserCaller != record.ParserCaller)
                {
                    lastRecord.ParserCaller = record.ParserCaller;
                }

                if (!string.IsNullOrWhiteSpace(record.ParserRecordingTime) && lastRecord.ParserRecordingTime != record.ParserRecordingTime)
                {
                    lastRecord.ParserRecordingTime = record.ParserRecordingTime;
                }

                if (!string.IsNullOrWhiteSpace(record.ViewModelFormat))
                {
                    lastRecord.ViewModelFormat = record.ViewModelFormat;
                }

                if (!string.IsNullOrWhiteSpace(record.OutputFormatter))
                {
                    lastRecord.OutputFormatter = record.OutputFormatter;
                }

                if (!string.IsNullOrWhiteSpace(record.ReportGenCaller) && lastRecord.ReportGenCaller != record.ReportGenCaller)
                {
                    lastRecord.ReportGenCaller = record.ReportGenCaller;
                }

                if (!string.IsNullOrWhiteSpace(record.ReportGenRecordingTime) && lastRecord.ReportGenRecordingTime != record.ReportGenRecordingTime)
                {
                    lastRecord.ReportGenRecordingTime = record.ReportGenRecordingTime;
                }
            }
            else
            {
                AddRecord(record);
            }
        }

        public string WriteRecordListToString()
        {
            StringBuilder sb = new StringBuilder();

            var sumRecord = recordList.Select(x => x.RecordNum).Sum();

            sb.Append($"{sumRecord} records have been processed; ");

            foreach (var record in recordList)
            {
                sb.Append(
                    $"{record.RecordNum} records from {record.FileName} [caller:{record.FileNameCaller} at {record.FileNameRecordingTime}]," +
                    $" with parser: {record.Parser} [caller:{record.ParserCaller} at {record.ParserRecordingTime}]," +
                    $" with viewModelFormat: {record.ViewModelFormat} [caller:{record.ReportGenCaller} at {record.ReportGenRecordingTime}]," +
                    $" output formatter: {record.OutputFormatter} [caller:{record.ReportGenCaller} at {record.ReportGenRecordingTime}]; ");
            }
            return sb.ToString();
        }

        public void WriteRecordList()
        {
            var recordString = WriteRecordListToString();

            var logger = Logger.GetInstance();
            logger.WriteLog(recordString);
        }
    }
}