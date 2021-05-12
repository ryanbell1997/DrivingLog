using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingLog.Models
{
    public class LogEntryRepository
    {
        private readonly AppDbContext _appDbContext;

        public LogEntryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<LogEntry> AllLogEntries
        {
            get
            {
                DateTime dateNow = DateTime.Now;
                return _appDbContext.LogEntries.Where(le => le.Date.Month == dateNow.Month && le.Date.Year == dateNow.Year).OrderBy(le => le.Date);
            }
        }

        public IEnumerable<LogEntry> GetLogEntriesByDate(DateTime monthYear) 
        {
            return _appDbContext.LogEntries.Where(le => le.Date.Month == monthYear.Month && le.Date.Year == monthYear.Year).OrderBy(le => le.Date);
        }

        public LogEntry GetPieById(int logEntryId)
        {
            return _appDbContext.LogEntries.FirstOrDefault(le => le.Id == logEntryId);
        }

        public void AddLogEntry(LogEntry logEntry)
        {

            if (logEntry != null)
            {
                LogEntry eLogEntry = logEntry;
                eLogEntry.TotalTime = CalculateTotalTime(eLogEntry.StartDateTime, eLogEntry.FinishDateTime);

                eLogEntry.QuantityCharged = CalculateTotalEarnings(eLogEntry.TotalTime);

                _appDbContext.Add(eLogEntry);

                _appDbContext.SaveChanges();
            }
        }

        public LogEntry GetLogEntryById(int id)
        {
            LogEntry returnLogEntry = new();

            returnLogEntry = _appDbContext.LogEntries.Where(le => le.Id == id).FirstOrDefault();

            return returnLogEntry;
        }

        public void DeleteLogEntry(int id)
        {
            LogEntry logEntryToDelete = _appDbContext.LogEntries.FirstOrDefault(le => le.Id == id);

            _appDbContext.Remove(logEntryToDelete);

            _appDbContext.SaveChanges();
        }

        public void EditLogEntry(LogEntry eLogEntry)
        {
            LogEntry eLogEntryToUpdate = _appDbContext.LogEntries.Where(le => le.Id == eLogEntry.Id).FirstOrDefault();

            if(eLogEntryToUpdate != null)
            {
                eLogEntry.TotalTime = CalculateTotalTime(eLogEntry.StartDateTime, eLogEntry.FinishDateTime);

                eLogEntry.QuantityCharged = CalculateTotalEarnings(eLogEntry.TotalTime);

                _appDbContext.Entry(eLogEntryToUpdate).CurrentValues.SetValues(eLogEntry);
            }

            _appDbContext.SaveChanges();
        }

        public TimeSpan CalculateTotalTime(TimeSpan startTime, TimeSpan endTime)
        {
            TimeSpan returnTime = endTime - startTime;

            return returnTime;
        }

        public decimal CalculateTotalEarnings(TimeSpan totalTime)
        {
            decimal hourlyRate = 9;

            decimal returnEarnings;

            int hoursToMinutes = totalTime.Hours * 60;
            decimal amountToCharge = (hoursToMinutes + totalTime.Minutes) / 60;
            
            returnEarnings = hourlyRate * amountToCharge;

            return returnEarnings;
        }
    }
}
