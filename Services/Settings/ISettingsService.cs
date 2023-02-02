using DrivingLog.Types;
using DrivingLog.Types.DTO;
using System.Threading.Tasks;

namespace DrivingLog.Services.Settings
{
    public interface ISettingsService
    {
        public decimal? GetHourlyRate();
        public decimal HourlyRate { get; set; }
        public Task<Result> SaveSettings(SaveSettingsDTO saveSettingsDTO);
    }
}
