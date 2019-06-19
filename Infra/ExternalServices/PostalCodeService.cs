using Application.DTO;
using Application.ExternalServices;
using CrossCutting.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infra.ExternalServices
{
    public class PostalCodeService : IPostalCodeService
    {
        IHttpClientFactory ClientFactory { get; }

        PostalCodeServiceSettings Settings { get; }

        public PostalCodeService(IHttpClientFactory clientFactory, IOptions<PostalCodeServiceSettings> postalCodeServiceSettings)
        {
            ClientFactory = clientFactory ?? throw new ArgumentException(nameof(IHttpClientFactory));
            Settings = postalCodeServiceSettings?.Value ?? throw new ArgumentException(nameof(PostalCodeServiceSettings));
        }

        public async Task<PostalCodeDto> GetCityNameByPostalCodeAsync(string postalCode)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{Settings.Uri}/{postalCode}/json");

                var client = ClientFactory.CreateClient();

                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode) return new PostalCodeDto { HasFailed = true };
                return await response.Content.ReadAsAsync<PostalCodeDto>();
            }
            catch
            {
                return new PostalCodeDto { HasFailed = true };
            }
        }
    }
}
