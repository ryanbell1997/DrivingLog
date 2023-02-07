using Application.Helpers;
using Application.Settings;
using Domain;
using Persistence;

namespace Application.LogEntrys
{
    public class LogEntryService : ILogEntryService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ISettingsService _settingsService;

        public LogEntryService(AppDbContext appDbContext, ISettingsService settingsService)
        {
            _appDbContext = appDbContext;
            _settingsService = settingsService;
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

        public void AddLogEntry(LogEntry logEntry)
        {

            if (logEntry != null)
            {
                LogEntry eLogEntry = logEntry;
                eLogEntry.TotalTime = CalculateTotalTime(eLogEntry.StartDateTime, eLogEntry.FinishDateTime);

                eLogEntry.QuantityCharged = CalculateTotalEarnings(eLogEntry.TotalTime);

                eLogEntry.FormattedTotalTime = CalculateTotalTimeInDecimal(eLogEntry.TotalTime);

                _appDbContext.Add(eLogEntry);

                _appDbContext.SaveChanges();
            }
        }

        public LogEntry GetLogEntryById(int id)
        {
            LogEntry returnLogEntry = new();

            returnLogEntry = _appDbContext.LogEntries.FirstOrDefault(le => le.Id == id);

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
            LogEntry eLogEntryToUpdate = _appDbContext.LogEntries.FirstOrDefault(le => le.Id == eLogEntry.Id);

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
            decimal hourlyRate = _settingsService.HourlyRate;
            decimal returnEarnings;

            decimal minutes = totalTime.Minutes;
            decimal hours = totalTime.Hours;

            decimal hoursToMinutes = hours * 60;
            decimal totalMinutes = minutes + hoursToMinutes;
            decimal amountToCharge = totalMinutes / 60;

            decimal amountToChargeRounded = RoundingHelper.RoundEarnings(amountToCharge);
            
            returnEarnings = hourlyRate * amountToChargeRounded;

            return returnEarnings;
        }

        public decimal CalculateTotalTimeInDecimal(TimeSpan totalTime)
        {
            int totalMinutes;
            int hoursInMinutes = totalTime.Hours * 60;

            totalMinutes = hoursInMinutes + totalTime.Minutes;

            decimal returnDecimal = totalMinutes / 60m;

            return returnDecimal;
        }
    }
}
