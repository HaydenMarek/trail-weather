namespace trail_weather_frontend.DTOs
{
    public class GeoCoordinateDTO
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
