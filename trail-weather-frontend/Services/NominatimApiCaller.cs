using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using trail_weather_frontend.DTOs;
using trail_weather_frontend.Services.Interfaces;

namespace trail_weather_frontend.Services
{
    public class NominatimApiCaller : INominatimApiCaller
    {
        private readonly HttpClient _httpClient;

        public NominatimApiCaller (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<LocationDetailsDTO> GetLocationDetailsAsync(double latitude, double longitude)
        {            
            var url = $"/reverse.php?lat={latitude}&lon={longitude}&zoom=18&layer=address&format=jsonv2";
            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.GetAsync(url);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            if (response.IsSuccessStatusCode)
            {
                var locationDetailsJson = await response.Content.ReadAsStringAsync();
                var locationDetails = JsonSerializer.Deserialize<LocationDetailsDTO>(locationDetailsJson);
                return locationDetails;
            }
            else
            {
                Console.WriteLine($"Failed to fetch location details: {response.ReasonPhrase}");
                return null;
            }
        }
    }
}
