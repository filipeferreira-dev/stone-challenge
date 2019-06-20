using System;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Services.Interfaces
{
    public interface ICityService
    {
        Task<AddCityResponseDto> AddCityAsync(string postalCode);

        Task<ResponseDto> RemoveAsync(Guid key);

        Task<GetAllCityResponseDto> GetAllAsync(PagingDto paging);

        Task<AddTemperatureResponseDto> AddTemperatureAsync(Guid cityKey, AddTemperatureRequestDto addTemperatureRequestDto);

        Task<GetCityTemperaturesDto> GetCityByKeyWithTemperaturesAsync(Guid cityKey);

        Task<GetAllCityTemperaturesDto> GetAllWithTemperaturesAsync();
    }
}
