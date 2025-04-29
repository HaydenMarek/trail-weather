using trail_weather_data_access.Models;

namespace trail_weather_data_access.Repositories.Interfaces
{
    public interface ISportCenterRepository
    {
        List<SportCenter> GetSportCenters();
    }
}
