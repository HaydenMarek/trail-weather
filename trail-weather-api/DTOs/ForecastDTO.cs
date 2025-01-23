namespace trail_weather_api.DTOs
{
    public class ForecastDTO
    {
        public string Name { get; set; }
        public int DistanceTo { get; set; }
        public GeoCoordinateDTO Coordinate { get; set; }
        public DailyDataDTO DailyData { get; set; }
    }

    public class DailyDataDTO
    {
        public List<string> TimeList { get; set; }
        public List<int> WeatherCodeList { get; set; }
    }
}
