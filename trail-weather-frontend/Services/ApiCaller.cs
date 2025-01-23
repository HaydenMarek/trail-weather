using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using trail_weather_frontend.DTOs;

namespace trail_weather_frontend.Services
{
    public class ApiCaller : IApiCaller
    {
        private readonly HttpClient _httpClient;

        public ApiCaller(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ForecastDTO>> GetTrailWeather(int range)
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            return await _httpClient.GetFromJsonAsync<List<ForecastDTO>>($"{range}");
        }
    }
}
