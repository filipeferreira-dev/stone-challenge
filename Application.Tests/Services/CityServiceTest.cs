using System;
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

        #region Add City

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
            result.Data.CreatedOn.Should().HaveLength(19);
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

        [Test(Description = "Should return false on try add a city with postalCodeService offline")]
        public async Task OnAddCityAsyncWithPostalCodeDown()
        {
            var postalCodeResponse = Builder<PostalCodeDto>
                                        .CreateNew()
                                        .With(p => p.HasFailed, true)
                                        .Build();

            PostalCodeService.GetCityNameByPostalCodeAsync(Arg.Any<string>()).Returns(postalCodeResponse);

            var result = await CityService.AddCityAsync("22743-011");

            result.Success.Should().BeFalse();
        }

        #endregion

        #region Remove City

        [Test(Description = "Should remove a city successfully")]
        public async Task OnRemoveCityAsync()
        {
            var city = Substitute.ForPartsOf<City>();

            CityRepository.GetByKeyAsync(Arg.Any<Guid>()).Returns(city);

            var result = await CityService.RemoveAsync(Guid.NewGuid());

            result.Success.Should().BeTrue();
        }

        [Test(Description = "Should return false when remove is called with a invalid key")]
        public async Task OnRemoveCityAsyncWithInvalidKey()
        {
            CityRepository.GetByKeyAsync(Arg.Any<Guid>()).Returns(Task.FromResult<City>(null));

            var result = await CityService.RemoveAsync(Guid.NewGuid());

            result.Success.Should().BeFalse();
        }

        #endregion

        #region Get All Cities

        [Test(Description = "Should return false when try get more than 100 records per page")]
        public async Task OnGetAllAsyncWithMoreThanMaxPerPage()
        {
            var paging = new PagingDto { RecordsPerPage = Faker.RandomNumber.Next(101, 1000) };
            var result = await CityService.GetAllAsync(paging);

            result.Success.Should().BeFalse();
        }

        [Test(Description = "Should return total of pages on getting cities when count is even")]
        public async Task OnGetAllAsyncCountEven()
        {
            var count = 10;
            var paging = new PagingDto { RecordsPerPage = 2 };
            var cities = Builder<City>.CreateListOfSize(paging.RecordsPerPage).Build();

            CityRepository.CountAsync().Returns(count);
            CityRepository.GetAllAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(cities);

            var result = await CityService.GetAllAsync(paging);

            result.Success.Should().BeNull();
            result.Data.Should().HaveCount(paging.RecordsPerPage);
            result.Paging.TotalPages.Should().Be(count / paging.RecordsPerPage);
        }

        [Test(Description = "Should return total of pages on getting cities when count is odd")]
        public async Task OnGetAllAsyncCountOdd()
        {
            var count = 11;
            var paging = new PagingDto { RecordsPerPage = 2 };
            var cities = Builder<City>.CreateListOfSize(paging.RecordsPerPage).Build();
            CityRepository.CountAsync().Returns(count);
            CityRepository.GetAllAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(cities);

            var result = await CityService.GetAllAsync(paging);

            result.Success.Should().BeNull();
            result.Data.Should().HaveCount(paging.RecordsPerPage);
            result.Paging.TotalPages.Should().Be((count / paging.RecordsPerPage) + 1);
        }

        #endregion

        #region Add Temperature

        [Test(Description = "Should add temperature successfully")]
        public async Task OnAddTemperatureAsync()
        {
            var city = Builder<City>
                        .CreateNew()
                        .With(p => p.Name, Faker.Address.City())
                        .Build();

            var weatherResult = Builder<WeatherResultDto>
                                .CreateNew()
                                .With(p => p.Temperature, Faker.RandomNumber.Next(1, 30))
                                .Build();

            var weather = Builder<WeatherDto>
                            .CreateNew()
                            .With(p => p.Results, weatherResult)
                            .Build();

            CityRepository.GetByKeyAsync(Arg.Any<Guid>()).Returns(city);
            CityTemperatureRepository.AddAsync(Arg.Any<CityTemperature>()).Returns(Task.CompletedTask);
            WeatherService.GetWeatherByCityAsync(Arg.Any<string>()).Returns(weather);

            var result = await CityService.AddTemperatureAsync(city.Key, new AddTemperatureRequestDto());

            result.Success.Should().BeTrue();
            result.Data.CreatedOn.Should().HaveLength(19);
        }

        #endregion
    }
}
