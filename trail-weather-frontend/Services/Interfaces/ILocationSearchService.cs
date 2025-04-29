using System.Collections.Generic;
using System.Threading.Tasks;
using trail_weather_frontend.DTOs;

namespace trail_weather_frontend.Services.Interfaces
{
    public interface ILocationSearchService
    {
        Task<List<NominatimResult>> SearchLocationsAsync(string query);
        Task<GeoCoordinateDTO> GetLocationDetailsAsync(double latitude, double longitude);
    }
}
