using System.Collections.Generic;

namespace Application.DTO
{
    public class GetAllCityResponseDto : ResponseDto
    {
        public IList<CityDto> Data { get; set; }

        public PagingDto Paging { get; set; }
    }
}
