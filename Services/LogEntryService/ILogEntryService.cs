using DrivingLog.Models;
using System;
using System.Collections.Generic;

namespace DrivingLog.Services.LogEntryService
{
    public interface ILogEntryService
    {
        public IEnumerable<LogEntry> AllLogEntries { get; }
        public IEnumerable<LogEntry> GetLogEntriesByDate(DateTime monthYear);
        public LogEntry GetPieById(int logEntryId);
        public void AddLogEntry(LogEntry logEntry);
        public LogEntry GetLogEntryById(int id);
        public void DeleteLogEntry(int id);
        public void EditLogEntry(LogEntry eLogEntry);
        public TimeSpan CalculateTotalTime(TimeSpan startTime, TimeSpan endTime);
        public decimal CalculateTotalEarnings(TimeSpan totalTime);
        public decimal CalculateTotalTimeInDecimal(TimeSpan totalTime);
    }
}
