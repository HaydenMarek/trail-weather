using System.Collections.Generic;
using System.Threading.Tasks;
using trail_weather_frontend.DTOs;

namespace trail_weather_frontend.Services.Interfaces
{
    public interface ITrailWeatherApiCaller
    {
        Task<List<ForecastDTO>> GetTrailWeather(int range, double lat, double lon);
    }
}
