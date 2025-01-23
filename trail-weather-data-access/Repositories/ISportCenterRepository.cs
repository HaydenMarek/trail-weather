using trail_weather_data_access.Models;

namespace trail_weather_data_access.Repositories
{
    public interface ISportCenterRepository
    {
        List<SportCenter> GetSportCenters();
    }
}
