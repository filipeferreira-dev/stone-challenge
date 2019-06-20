using System.Collections.Generic;

namespace Application.DTO
{
    public class GetAllCityTemperaturesDto
    {
        public IList<CityWithTemperatureDto> Data { get; set; }
    }
}
