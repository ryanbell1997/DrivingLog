using System;
using System.ComponentModel.DataAnnotations;

namespace DrivingLog.Models
{
    public class LogEntry
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public TimeSpan StartDateTime { get; set; }
        public TimeSpan FinishDateTime { get; set; }
        public decimal QuantityCharged { get; set; }
        public TimeSpan TotalTime { get; set; }
        public Nullable<decimal> FormattedTotalTime { get; set; }
    }
}
