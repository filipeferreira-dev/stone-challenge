using System.Threading.Tasks;
using Application.DTO;

namespace Application.ExternalServices
{
    public interface IWeatherService
    {
        Task<WeatherDto> GetWeatherByCityAsync(string cityName);
    }
}
