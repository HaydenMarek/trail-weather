using Microsoft.EntityFrameworkCore;
using trail_weather_data_access.Enums;
using trail_weather_data_access.Models;

namespace trail_weather_data_access.Repositories
{
    public class SportCenterRepository : ISportCenterRepository
    {
        private readonly TrailWeatherDbContext _db;

        public SportCenterRepository(string connectionString, DbProviders dbProvider)
        {
            _db = new TrailWeatherDbContext(connectionString, dbProvider);
        }

        public List<SportCenter> GetSportCenters()
        {
            return _db.SportCenter
                .Include(Repositories => Repositories.GeoData)
                .Include(Repositories => Repositories.SportCenterType)
                .ToList();
        }
    }
}
