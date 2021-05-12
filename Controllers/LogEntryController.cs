using DrivingLog.Interfaces;
using DrivingLog.Models;
using DrivingLog.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

            if(id != 0)
            {
               eLogEntry = _logEntryRepository.GetLogEntryById(id);
            }

            return View(eLogEntry);
        }

        [HttpPost]
        public IActionResult SubmitEditLogEntry(LogEntry eLogEntry)
        {

            _logEntryRepository.EditLogEntry(eLogEntry);

            return RedirectToAction("LogEntry");
        }

    }
}
