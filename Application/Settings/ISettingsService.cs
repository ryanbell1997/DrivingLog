using Application.DTO;
using Application.Types;

namespace Application.Settings
{
    public interface ISettingsService
    {
        public decimal? GetHourlyRate();
        public decimal HourlyRate { get; set; }
        public Task<Result> SaveSettings(SaveSettingsDTO saveSettingsDTO);
    }
}
