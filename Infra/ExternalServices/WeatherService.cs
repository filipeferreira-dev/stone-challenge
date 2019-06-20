using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.DTO;
using Application.ExternalServices;
using CrossCutting.Settings;
using Microsoft.Extensions.Options;

namespace Infra.ExternalServices
{
    public class WeatherService : IWeatherService
    {
        WeatherServiceSettings Settings { get; }

        IHttpClientFactory ClientFactory { get; }

        public WeatherService(IHttpClientFactory clientFactory, IOptions<WeatherServiceSettings> options)
        {
            ClientFactory = clientFactory ?? throw new ArgumentException(nameof(IHttpClientFactory));
            Settings = options?.Value ?? throw new ArgumentException(nameof(WeatherServiceSettings));
        }

        public async Task<WeatherDto> GetWeatherByCityAsync(string cityName)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{Settings.Uri}?key={Settings.Key}&city_name={cityName}");

                var client = ClientFactory.CreateClient();

                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode) return new WeatherDto { HasFailed = true };
                return await response.Content.ReadAsAsync<WeatherDto>();
            }
            catch
            {
                return new WeatherDto { HasFailed = true };
            }
        }
    }
}
