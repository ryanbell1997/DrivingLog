using Application.DTO;
using Application.Types;
using Persistence;

namespace Application.Settings
{
    public class SettingsService : ISettingsService
    {
        private readonly AppDbContext _appDbContext;
        private decimal hourlyRate;

        public SettingsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public decimal HourlyRate 
        { 
            get
            {
                if(hourlyRate == 0)
                {
                   var settings = _appDbContext.Settings.FirstOrDefault();

                   return settings is null ? 12 : settings.HourlyRate;
                }
                else
                {
                    return hourlyRate;
                }
            }
            set => hourlyRate = value;
        }

        public decimal? GetHourlyRate() => _appDbContext.Settings.FirstOrDefault()?.HourlyRate;

        public async Task<Result> SaveSettings(SaveSettingsDTO saveSettingsDTO)
        {
            var settings = _appDbContext.Settings.FirstOrDefault();

            if (settings is null) return Result.Error("Settings missing from database");

            settings.HourlyRate = saveSettingsDTO.CurrentHourlyRate;
            
            _appDbContext.Settings.Update(settings);
            var result = await _appDbContext.SaveChangesAsync();

            if (result > 0) return Result.Success();

            return Result.Error("Error occured while saving changes");
        }
    }
}
