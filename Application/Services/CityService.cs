using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.DTO;
using Application.ExternalServices;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class CityService : ICityService
    {
        IPostalCodeService PostalCodeService { get; }

        ICityRepository CityRepository { get; }

        public CityService(IPostalCodeService postalCodeService, ICityRepository cityRepository)
        {
            PostalCodeService = postalCodeService ?? throw new ArgumentNullException(nameof(IPostalCodeService));
            CityRepository = cityRepository ?? throw new ArgumentNullException(nameof(ICityRepository));
        }

        public async Task<AddCityResponseDto> AddCityAsync(string postalCode)
        {
            var regex = new Regex(@"^\d{5}-\d{3}$");

            if (!regex.IsMatch(postalCode)) return new AddCityResponseDto { Success = false, Message = "Invalid postal code." };

            var city = await CityRepository.GetByPostalCodeAsync(postalCode);

            if(city != null ) return new AddCityResponseDto { Success = false, Message = "Postal code already added." };
            var postalCodeResponse = await PostalCodeService.GetCityNameByPostalCodeAsync(postalCode);

            if (postalCodeResponse.HasFailed) return new AddCityResponseDto { Success = false, Message = "Failed on try get city name." };

            city = new City(postalCodeResponse.CityName, postalCode);

            await CityRepository.AddAsync(city);

            return new AddCityResponseDto
            {
                Data = new CityDto
                {
                    Key = city.Key.ToString(),
                    Name = city.Name,
                    PostalCode = city.PostalCode,
                    CreatedOn = city.CreatedOn.ToString("s")
                },
                Success = true
            };
        }

        public async Task<ResponseDto> RemoveAsync(string key)
        {
            if (!Guid.TryParse(key, out Guid keyGuid)) return new ResponseDto { Success = false, Message = "Invalid key." };

            var city = await CityRepository.GetByKeyAsync(keyGuid);

            if (city == null) return new ResponseDto { Success = false, Message = "City not found." };
            if (!city.Delete()) return new ResponseDto { Success = false, Message = "City is already removed." };

            await CityRepository.RemoveAsync(city);
            return new ResponseDto { Success = true };
        }
    }
}
