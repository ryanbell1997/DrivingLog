using DrivingLog.Services.Settings;

namespace DrivingLog.ViewModels
{
    public class SettingsViewModel
    {
        private readonly ISettingsService _settingsService;

        public SettingsViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            CurrentHourlyRate = _settingsService.HourlyRate;
        }

        public decimal CurrentHourlyRate { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
