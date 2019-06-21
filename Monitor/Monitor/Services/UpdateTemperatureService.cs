using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Monitor.Models;
using Newtonsoft.Json;

namespace Monitor.Services
{
    public class UpdateTemperatureService
    {
        ApiSettings Settings { get; }

        HttpClient HttpClient { get; }

        public UpdateTemperatureService(ApiSettings settings)
        {
            Settings = settings;

            HttpClient = new HttpClient();
        }

        public void UpdateTemperatures()
        {
            var cities = GetCities();

            if (!cities.Data.Any()) return;

            UpdateTemperatureByCity(cities.Data);
        }

        GetCityResponse GetCities()
        {
            try
            {
                var response = HttpClient.GetAsync($"{Settings.Uri}/api/cities?page=1&recordsPerPage=100").GetAwaiter().GetResult();

                var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                return JsonConvert.DeserializeObject<GetCityResponse>(responseContent);
            }
            catch
            {
                return new GetCityResponse { Data = new List<City>() };
            }
        }

        void UpdateTemperatureByCity(List<City> cities)
        {
            var content = JsonConvert.SerializeObject(new { Temperature = 1 });
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            foreach (var city in cities)
            {
                try
                {
                    HttpClient.PostAsync($"{Settings.Uri}/api/cities/{city.Key.ToString()}/temperatures", byteContent).GetAwaiter().GetResult();
                }
                catch
                {

                }
            }
        }
    }
}
