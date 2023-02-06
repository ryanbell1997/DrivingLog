using Domain;
using System;
using System.Collections.Generic;

namespace Client.ViewModels
{


    public class LogEntryListViewModel
    {
        public IEnumerable<LogEntry> LogEntries { get; set; }
        public DateTime MonthYear { get; set; }
        public DateTime DisplayDate { get; set; }
    }
}
