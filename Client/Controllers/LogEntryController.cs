using Application.Excel;
using Application.LogEntrys;
using Client.ViewModels;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Client.Controllers
{
    [Authorize]
    public class LogEntryController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        private readonly ILogEntryService _logEntryRepository;
        private readonly IExcelService _excelService;

        public LogEntryController(ILogEntryService logEntryRepository, IExcelService excelService)
        {
            _logEntryRepository = logEntryRepository;
            _excelService = excelService;
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
            if (vm.MonthYear == DateTime.MinValue) return BadRequest();

            var result = _excelService.GetExcelDownloadByDate(vm.MonthYear);
            return File(result.stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.Name);
        }
    }
}
