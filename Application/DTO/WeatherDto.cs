using Newtonsoft.Json;

namespace Application.DTO
{
    public class WeatherDto
    {
        public string By { get; set; }

        [JsonProperty(PropertyName = "valid_key")]
        public string ValidKey { get; set; }

        public WeatherResultDto Results { get; set; }

        [JsonProperty(PropertyName = "from_cache")]
        public bool FromCache { get; set; }

        public bool HasFailed { get; set; }
    }

    public class WeatherResultDto
    {
        [JsonProperty(PropertyName = "temp")]
        public int Temperature { get; set; }

        public string City { get; set; }

        public string Description { get; set; }
    }
}