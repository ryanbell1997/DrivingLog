using DrivingLog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingLog.ViewModels
{


    public class LogEntryListViewModel
    {
        public IEnumerable<LogEntry> LogEntries { get; set; }
        public DateTime MonthYear { get; set; }
        public DateTime DisplayDate { get; set; }
    }
}
