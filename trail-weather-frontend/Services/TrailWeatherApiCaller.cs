using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using trail_weather_frontend.DTOs;
using trail_weather_frontend.Services.Interfaces;

namespace trail_weather_frontend.Services
{
    public class TrailWeatherApiCaller : ITrailWeatherApiCaller
    {
        private readonly HttpClient _httpClient;

        public TrailWeatherApiCaller(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ForecastDTO>> GetTrailWeather(int range, double lat, double lon)
        {            
            return await _httpClient.GetFromJsonAsync<List<ForecastDTO>>($"{range}, {lat}, {lon}");
        }
    }
}
