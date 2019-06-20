using System.Threading.Tasks;
using Application.DTO;
using Application.ExternalServices;
using Application.Services;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Application.Tests.Services
{
    public class CityServiceTest
    {
        ICityRepository CityRepository { get; set; }
        IPostalCodeService PostalCodeService { get; set; }
        ICityService CityService { get; set; }
        IWeatherService WeatherService { get; set; }
        ICityTemperatureRepository CityTemperatureRepository { get; set; }

        [SetUp]
        public void SetUp()
        {
            CityRepository = Substitute.For<ICityRepository>();
            PostalCodeService = Substitute.For<IPostalCodeService>();
            WeatherService = Substitute.For<IWeatherService>();
            CityTemperatureRepository = Substitute.For<ICityTemperatureRepository>();

            CityService = new CityService(PostalCodeService, CityRepository, WeatherService, CityTemperatureRepository);
        }

        [Test(Description = "Should add a city successfully")]
        public async Task OnAddCityAsync()
        {
            var postalCodeResponse = Builder<PostalCodeDto>
                                        .CreateNew()
                                        .With(p => p.HasFailed, false)
                                        .With(p => p.CityName, Faker.Address.City())
                                        .With(p => p.PostalCode, Faker.Address.ZipCode())
                                        .Build();

            PostalCodeService.GetCityNameByPostalCodeAsync(Arg.Any<string>()).Returns(postalCodeResponse);
            CityRepository.AddAsync(Arg.Any<City>()).Returns(Task.CompletedTask);

            var result = await CityService.AddCityAsync("22743-011");

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.CreatedOn.Length.Should().Be(19);
        }

        [Test(Description = "Should return false on try add a city twice")]
        public async Task OnAddCityAsyncTwice()
        {
            var city = Builder<City>.CreateNew().Build();
            CityRepository.GetByPostalCodeAsync(Arg.Any<string>()).Returns(city);

            var result = await CityService.AddCityAsync("22743-011");

            result.Success.Should().BeFalse();
        }

        [Test(Description = "Should return false on try add a city with invalid postalCode")]
        public async Task OnAddCityAsyncWithInvalidPostalCode()
        {
            var result = await CityService.AddCityAsync(Faker.Lorem.GetFirstWord());

            result.Success.Should().BeFalse();
        }
    }
}
