namespace trail_weather_data_access.Models
{
    public class SportCenter
    {
        public int SportCenterId { get; set; }
        public string Name { get; set; }
        public int GeoDataId { get; set; }
        public virtual GeoData GeoData { get; set; }
        public int SportCenterTypeId { get; set; }
        public virtual SportCenterType SportCenterType { get; set; }
    }
}
