using System.Threading.Tasks;
using Application.DTO;

namespace Application.Services.Interfaces
{
    public interface ICityService
    {
        Task<ResponseDto> AddCityAsync(string postalCode);
    }
}
