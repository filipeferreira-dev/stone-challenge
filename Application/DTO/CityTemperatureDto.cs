using System.Collections.Generic;

namespace Application.DTO
{
    public class CityTemperatureDto
    {
        public string City { get; set; }

        public int Temperature { get; set; }

        public string CreatedOn { get; set; }
    }

    public class CityWithTemperatureDto
    {
        public string City { get; set; }

        public IList<CityTemperatureDto> Temperatures { get; set; }
    }
}
