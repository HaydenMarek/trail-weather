using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using trail_weather_api.DTOs;
using trail_weather_api.Services.Interfaces;

namespace trail_weather_api.Services
{
    public class ForecastService : IForecastService
    {
        private readonly HttpClient _httpClient;
        const int PAST_DAYS = 2;
        const int FORECAST_DAYS = 4;        

        public ForecastService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ForecastDTO>> GetForecast(List<ForecastDTO> forecastDTOs)
        {
            UriBuilder uriBuilder = new UriBuilder();
            var query = HttpUtility.ParseQueryString(string.Empty);

            query["latitude"] = string.Join(",", forecastDTOs.Select(f => f.Coordinate.Lat.ToString(CultureInfo.InvariantCulture)));
            query["longitude"] = string.Join(",", forecastDTOs.Select(f => f.Coordinate.Lon.ToString(CultureInfo.InvariantCulture)));
            query["daily"] = "weather_code";
            query["past_days"] = PAST_DAYS.ToString();
            query["forecast_days"] = FORECAST_DAYS.ToString();
            uriBuilder.Query = query.ToString();

            var response = await _httpClient.GetAsync(uriBuilder.Query);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Error while fetching data from the API");

            var parsedResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            List<WeatherResponseDTO> weatherData = new();

            if (forecastDTOs.Count == 1)
            {
                WeatherResponseDTO? singleResponse = JsonConvert.DeserializeObject<WeatherResponseDTO>(parsedResponse);
                if (singleResponse is null)                
                    throw new NullReferenceException();
                
                weatherData.Add(singleResponse);
            }                          
            
            if (forecastDTOs.Count > 1)
            {
                List<WeatherResponseDTO>? multipleResponse = JsonConvert.DeserializeObject<List<WeatherResponseDTO>>(parsedResponse);
                if (multipleResponse is null)
                    throw new NullReferenceException();

                weatherData.AddRange(multipleResponse);
            }
                
            if (weatherData is null)
                throw new Exception("Error while fetching data from the API no data received");

            for (int i = 0; i < forecastDTOs.Count; i++)
            {
                forecastDTOs[i].DailyData.TimeList = weatherData[i].Daily.TimeList;
                forecastDTOs[i].DailyData.WeatherCodeList = weatherData[i].Daily.WeatherCodeList;
            }

            return forecastDTOs;
        }
    }
}
