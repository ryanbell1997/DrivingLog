using DrivingLog.Interfaces;
using DrivingLog.Models;
using DrivingLog.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingLog.Controllers
{
    public class LogEntryController : Controller
    {

        public ViewResult Index()
        {
            return View();
        }

        private readonly LogEntryRepository _logEntryRepository;

        public LogEntryController(LogEntryRepository logEntryRepository)
        {
            _logEntryRepository = logEntryRepository;
        }

        public IActionResult LogEntry()
        {
            LogEntryListViewModel logEntryListViewModel = new LogEntryListViewModel();
            logEntryListViewModel.LogEntries = _logEntryRepository.AllLogEntries;
            logEntryListViewModel.DisplayDate = DateTime.Now;
            return View(logEntryListViewModel);
        }

        [HttpPost]
        public IActionResult LogEntry(LogEntryListViewModel vm)
        {
            vm.LogEntries = _logEntryRepository.GetLogEntriesByDate(vm.MonthYear);
            vm.DisplayDate = vm.MonthYear;
            return View(vm);
        }

        public ViewResult AddLogEntry()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddLogEntry(LogEntry logEntry)
        {
            if (ModelState.IsValid)
            {
                _logEntryRepository.AddLogEntry(logEntry);
                return RedirectToAction("LogEntry");
            }

            return View(logEntry);
        }

        [HttpPost]
        public IActionResult DeleteLogEntry(int id)
        {
            if (id != 0)
            {
                _logEntryRepository.DeleteLogEntry(id);
            }

            return RedirectToAction("LogEntry");
        }

        [HttpPost]
        public IActionResult EditLogEntry(int id)
        {
            LogEntry eLogEntry = new();

            if (id != 0)
            {
                eLogEntry = _logEntryRepository.GetLogEntryById(id);
            }

            return View(eLogEntry);
        }

        public IActionResult DownloadExcel(LogEntryListViewModel vm)
        {
            List<LogEntry> lstLogEntries = _logEntryRepository.GetLogEntriesByDate(vm.MonthYear).ToList();
            MemoryStream stream = new MemoryStream();

            using (ExcelPackage Ep = new ExcelPackage(stream))
            {
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
                
                int row = 2;
                foreach (LogEntry eLogEntry in lstLogEntries)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = eLogEntry.Date;
                    Sheet.Cells[string.Format("B{0}", row)].Value = eLogEntry.StartDateTime;
                    Sheet.Cells[string.Format("C{0}", row)].Value = eLogEntry.FinishDateTime;
                    Sheet.Cells[string.Format("D{0}", row)].Value = eLogEntry.TotalTime;
                    Sheet.Cells[string.Format("E{0}", row)].Value = eLogEntry.QuantityCharged;
                    row++;

                    
                    Ep.Save();
                }

                Sheet.Cells["A:AZ"].AutoFitColumns();
            };  
            
            stream.Position = 0;
            string excelName = $"{vm.MonthYear}-driving-log.csv";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheet.sheet", excelName);

        }
    }
}
