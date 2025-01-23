using GeoCoordinatePortable;
using Microsoft.AspNetCore.Mvc;
using trail_weather_api.DTOs;
using trail_weather_api.Services.Interfaces;
using trail_weather_data_access.Models;
using trail_weather_data_access.Repositories;
namespace trail_weather_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ISportCenterRepository _sportCenterRepository;
        private readonly IForecastService _forecastService;

        public WeatherForecastController(ISportCenterRepository sportCenterRepository, IForecastService forecastService)
        {
            _sportCenterRepository = sportCenterRepository;
            _forecastService = forecastService;
        }

        [HttpGet("{range}, {lat}, {lon}", Name = "GetByRangeFromLocation")]
        public async Task<List<ForecastDTO>> Get(int range, double lat, double lon)
        {            
            GeoCoordinate location = new GeoCoordinate { Latitude = lat, Longitude = lon };
            List<SportCenter> sportCenters = _sportCenterRepository.GetSportCenters();

            var filteredByDistance = sportCenters
                .Where(sc => CalculateDistance(location, new GeoCoordinate { Latitude = sc.GeoData.Lat, Longitude = sc.GeoData.Lon }) <= range)
                .Select(sc => new ForecastDTO
                {
                    Name = sc.Name,
                    DistanceTo = CalculateDistance(location, new GeoCoordinate { Latitude = sc.GeoData.Lat, Longitude = sc.GeoData.Lon }),
                    Coordinate = new GeoCoordinateDTO { Lat = sc.GeoData.Lat, Lon = sc.GeoData.Lon },
                    DailyData = new DailyDataDTO()
                }).ToList();

            if (filteredByDistance.Count == 0)
                return new List<ForecastDTO>();

            return await _forecastService.GetForecast(filteredByDistance);
        }

        private static int CalculateDistance(GeoCoordinate start, GeoCoordinate end)
        {            
            return (int)start.GetDistanceTo(end)/1000;
        }
    }
}
