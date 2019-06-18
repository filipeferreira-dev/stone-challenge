namespace Application.DTO
{
    public class AddCityResponseDto : ResponseDto
    {
        public CityDto Data { get; set; }
    }

    public class CityDto
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string PostalCode { get; set; }

        public string CreatedOn { get; set; }
    }
}
