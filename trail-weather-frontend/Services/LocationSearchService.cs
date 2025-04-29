using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using trail_weather_frontend.DTOs;
using trail_weather_frontend.Services.Interfaces;

namespace trail_weather_frontend.Services
{
    public class LocationSearchService : ILocationSearchService
    {
        private readonly HttpClient _httpClient;

        public LocationSearchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<NominatimResult>> SearchLocationsAsync(string query)
        {
            var url = $"https://nominatim.openstreetmap.org/search?format=json&q={Uri.EscapeDataString(query)}";
            var result = await _httpClient.GetAsync(url);
            var content = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<NominatimResult>>(content) ?? new();
        }

        public async Task<GeoCoordinateDTO> GetLocationDetailsAsync(double latitude, double longitude)
        {
            var query = $"/reverse.php?lat={latitude}&lon={longitude}&zoom=18&layer=address&format=jsonv2";
            var url = $"https://nominatim.openstreetmap.org{query}";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var locationDetailsJson = await response.Content.ReadAsStringAsync();
                var locationDetails = JsonSerializer.Deserialize<NominatimLocationDetailsDTO>(locationDetailsJson);
                return new GeoCoordinateDTO
                {
                    Lat = Double.Parse(locationDetails.lat),
                    Lon = Double.Parse(locationDetails.lon),
                    Address = locationDetails.display_name
                };
            }

            Console.WriteLine($"Failed to fetch location details: {response.ReasonPhrase}");
            return null;
        }
    }

    public class NominatimResult
    {
        public long PlaceId { get; set; }
        public string Licence { get; set; }
        public string OsmType { get; set; }
        public long OsmId { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string Class { get; set; }
        public string Type { get; set; }
        public int PlaceRank { get; set; }
        public double Importance { get; set; }
        public string AddressType { get; set; }
        public string Name { get; set; }
        public string display_name { get; set; }
        public List<double> BoundingBox { get; set; }
    }

    public class NominatimLocationDetailsDTO
    {
        public string lat { get; set; }
        public string lon { get; set; }
        public string display_name { get; set; }
    }
}