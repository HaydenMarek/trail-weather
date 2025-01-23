using System.Threading.Tasks;
using trail_weather_frontend.DTOs;

namespace trail_weather_frontend.Services.Interfaces
{
    public interface INominatimApiCaller
    {
        Task<LocationDetailsDTO> GetLocationDetailsAsync(double latitude, double longitude);
    }
}
