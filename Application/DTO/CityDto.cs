using System.Collections.Generic;

namespace Application.DTO
{
    public class CityDto
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string PostalCode { get; set; }

        public string CreatedOn { get; set; }

        public IList<CityTemperatureDto> Temperatures { get; set; }
    }
}
