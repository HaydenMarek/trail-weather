namespace trail_weather_data_access.Models
{
    public class GeoData
    {
        public int GeoDataId { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public virtual SportCenter SportCenter { get; set; }
    }
}
