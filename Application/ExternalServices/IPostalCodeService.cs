using System.Threading.Tasks;
using Application.DTO;

namespace Application.ExternalServices
{
    public interface IPostalCodeService
    {
        Task<PostalCodeDto> GetCityNameByPostalCodeAsync(string postalCode);
    }
}
