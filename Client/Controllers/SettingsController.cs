﻿using Application.DTO;
using Application.Settings;
using Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public ViewResult Index()
        {
            SettingsViewModel viewModel = new(_settingsService);
            return View(viewModel);
        }

        public async Task<IActionResult> SaveSettings(SaveSettingsDTO saveSettingsDTO)
        {
            var result = await _settingsService.SaveSettings(saveSettingsDTO);

            if(result.IsSuccess)
            {
                return RedirectToAction("LogEntry", "LogEntry");
            }
            else
            {
                return View("Index", new SettingsViewModel(_settingsService) { ErrorMessage = result.Message, CurrentHourlyRate = saveSettingsDTO.CurrentHourlyRate });
            }
        }
    }
}
