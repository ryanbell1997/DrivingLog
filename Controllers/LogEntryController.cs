using DrivingLog.Interfaces;
using DrivingLog.Models;
using DrivingLog.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
            logEntryListViewModel.DisplayDate =  logEntryListViewModel.MonthYear = DateTime.Now;
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

        [HttpPost]
        public IActionResult SubmitEditLogEntry(LogEntry eLogEntry)
        {
            if(eLogEntry != null)
            {
                _logEntryRepository.EditLogEntry(eLogEntry);
            }

            return RedirectToAction("LogEntry");
        }

        public IActionResult DownloadExcel(LogEntryListViewModel vm)
        {
            List<LogEntry> lstLogEntries = _logEntryRepository.GetLogEntriesByDate(vm.MonthYear).ToList();
            MemoryStream stream = new MemoryStream();

            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(lstLogEntries, true);
                workSheet.Column(2).Style.Numberformat.Format = "d-mmm-yy";
                workSheet.Column(3).Style.Numberformat.Format = "h:mm";
                workSheet.Column(4).Style.Numberformat.Format = "h:mm";
                workSheet.Column(5).Style.Numberformat.Format = "£#,##0.00";
                workSheet.Column(6).Style.Numberformat.Format = "h:mm";
                workSheet.Column(7).Style.Numberformat.Format = "0.00";
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells.AutoFitColumns();
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"{vm.MonthYear.Month}-{vm.MonthYear.Year}-driving-log.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

        }
    }
}
