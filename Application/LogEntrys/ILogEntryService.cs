using Domain;

namespace Application.LogEntrys
{
    public interface ILogEntryService
    {
        public IEnumerable<LogEntry> AllLogEntries { get; }
        public IEnumerable<LogEntry> GetLogEntriesByDate(DateTime monthYear);
        public void AddLogEntry(LogEntry logEntry);
        public LogEntry GetLogEntryById(int id);
        public void DeleteLogEntry(int id);
        public void EditLogEntry(LogEntry eLogEntry);
        public TimeSpan CalculateTotalTime(TimeSpan startTime, TimeSpan endTime);
        public decimal CalculateTotalEarnings(TimeSpan totalTime);
        public decimal CalculateTotalTimeInDecimal(TimeSpan totalTime);
    }
}
