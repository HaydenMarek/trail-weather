using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trail_weather_data_access.Models
{
    public class SportCenter
    {
        [Key]
        public int SportCenterId { get; set; }
        public string Name { get; set; }
        [ForeignKey("GeoData")]
        public int GeoDataId { get; set; }
        public virtual GeoData GeoData { get; set; }
        [ForeignKey("SportCenterType")]
        public int SportCenterTypeId { get; set; }
        public virtual SportCenterType SportCenterType { get; set; }
    }
}
