using System.Net.Http;
using System.Threading.Tasks;
using Application.DTO;
using Application.ExternalServices;

namespace Infra.ExternalServices
{
    public class PostalCodeService : IPostalCodeService
    {
        IHttpClientFactory ClientFactory { get; }

        //TODO: Inject configuration
        string RequestUri { get; } = "https://viacep.com.br/ws";

        public PostalCodeService(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        public async Task<PostalCodeDto> GetCityNameByPostalCodeAsync(string postalCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{RequestUri}/{postalCode}/json");

            var client = ClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode) return new PostalCodeDto { HasFailed = true };

            return await response.Content.ReadAsAsync<PostalCodeDto>();
        }
    }
}
