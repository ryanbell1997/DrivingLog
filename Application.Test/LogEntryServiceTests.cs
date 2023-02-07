using Application.LogEntrys;
using Application.Settings;
using Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using Xunit;

namespace Application.Test
{
    public class LogEntryServiceTests
    {
        LogEntryService _subject;
        public Mock<AppDbContext> mockAppDbContext;
        public Mock<ISettingsService> mockSettingsService;

        public LogEntryServiceTests()
        {
            mockSettingsService = new();
        }

        [Fact]
        public void AddLogEntry_Successfully_Adds_To_Database()
        {
            var _dbContext = IntializeAppDbContext();

            _subject = new(_dbContext, mockSettingsService.Object);

            var finishTime = DateTime.UtcNow.AddHours(2);
            var startTime  = DateTime.UtcNow;
            var date = DateTime.Now;

            _subject.AddLogEntry(new() {
                Date = date,
                StartDateTime = startTime.TimeOfDay,
                FinishDateTime = finishTime.TimeOfDay 
            });

            var result = _dbContext.LogEntries.FirstOrDefault();
            result.Should().NotBeNull();
            result!.Date.ToLongDateString().Should().Be(date.ToLongDateString());
        }

        [Fact]
        public void DeleteLogEntry_Successfully_Removes_Log_Entry()
        {
            var _dbContext = IntializeAppDbContext();
            _subject = new(_dbContext, mockSettingsService.Object);

            LogEntry log = _mockLog;
            log.Id = 1;

            _subject.AddLogEntry(log);

            _dbContext.LogEntries.Should().HaveCount(1);

            _subject.DeleteLogEntry(1);

            _dbContext.LogEntries.Should().HaveCount(0);
        }
        

        private AppDbContext IntializeAppDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseInMemoryDatabase("Test");
            return new AppDbContext(optionsBuilder.Options);
        }

        private LogEntry _mockLog
            => new() { Date = new DateTime(2023, 02, 25), StartDateTime = new DateTime(2023, 02, 25, 10, 00, 30).TimeOfDay, FinishDateTime = new DateTime(2023, 02, 25, 11, 00, 30).TimeOfDay };
    }
}
