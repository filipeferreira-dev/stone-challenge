using Application.DTO;
using System;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICityService
    {
        Task<AddCityResponseDto> AddCityAsync(string postalCode);

        Task<ResponseDto> RemoveAsync(Guid key);

    }
}
