using Newtonsoft.Json;

namespace trail_weather_api.DTOs
{
    public class WeatherResponseDTO
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("generationtime_ms")]
        public double GenerationTimeMs { get; set; }

        [JsonProperty("utc_offset_seconds")]
        public int UtcOffsetSeconds { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("timezone_abbreviation")]
        public string TimezoneAbbreviation { get; set; }

        [JsonProperty("elevation")]
        public double Elevation { get; set; }

        [JsonProperty("daily_units")]
        public DailyUnits DailyUnits { get; set; }

        [JsonProperty("daily")]
        public DailyData Daily { get; set; }
    }

    public class DailyUnits
    {
        [JsonProperty("time")]
        public string TimeUnit { get; set; }

        [JsonProperty("weather_code")]
        public string WeatherCodeUnit { get; set; }
    }

    public class DailyData
    {
        [JsonProperty("time")]
        public List<string> TimeList { get; set; }

        [JsonProperty("weather_code")]
        public List<int> WeatherCodeList { get; set; }
    }
}
