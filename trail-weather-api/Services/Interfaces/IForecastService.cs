using trail_weather_api.DTOs;
namespace trail_weather_api.Services.Interfaces
{
    public interface IForecastService
    {
        Task<List<ForecastDTO>> GetForecast(List<ForecastDTO> forecastDTOs);
    }
}
