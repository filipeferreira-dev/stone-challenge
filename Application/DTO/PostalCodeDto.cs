using Newtonsoft.Json;

namespace Application.DTO
{
    public class PostalCodeDto
    {
        [JsonProperty(PropertyName = "cep")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "localidade")]
        public string CityName { get; set; }

        [JsonProperty(PropertyName = "erro")]
        public bool HasFailed { get; set; }
    }
}
