using System.Collections.Generic;

namespace trail_weather_data_access.Models
{
    public class SportCenterType
    {
        public int SportCenterTypeId { get; set; }
        public string Name { get; set; }
        public virtual List<SportCenter> SportCenter { get; set; }
    }
}
