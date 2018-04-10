namespace APUS.Logging
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed class LogEntryCollector
    {
        private static readonly LogEntryCollector singletonCollector = new LogEntryCollector();

        private IList<OfficerProcessingRecord> recordList;

        private LogEntryCollector()
        {
            recordList = new List<OfficerProcessingRecord>();
        }

        public static LogEntryCollector GetInstance()
        {
            return singletonCollector;
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

                if (!string.IsNullOrWhiteSpace(record.Parser))
                {
                    lastRecord.Parser = record.Parser;
                }

                if (!string.IsNullOrWhiteSpace(record.ViewModelFormat))
                {
                    lastRecord.ViewModelFormat = record.ViewModelFormat;
                }

                if (!string.IsNullOrWhiteSpace(record.OutputFormatter))
                {
                    lastRecord.OutputFormatter = record.OutputFormatter;
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
                    $"{record.RecordNum} records from {record.FileName}," +
                    $" with parser: {record.Parser}," +
                    $" with viewModelFormat: {record.ViewModelFormat}," +
                    $" output formatter: {record.OutputFormatter}; ");
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