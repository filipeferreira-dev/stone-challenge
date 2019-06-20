﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.DTO;
using Application.ExternalServices;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class CityService : ICityService
    {
        IPostalCodeService PostalCodeService { get; }

        ICityRepository CityRepository { get; }

        IWeatherService WeatherService { get; }

        ICityTemperatureRepository CityTemperatureRepository { get; }

        public CityService
            (
              IPostalCodeService postalCodeService
            , ICityRepository cityRepository
            , IWeatherService weatherService
            , ICityTemperatureRepository cityTemperatureRepository
            )
        {
            PostalCodeService = postalCodeService ?? throw new ArgumentNullException(nameof(IPostalCodeService));
            CityRepository = cityRepository ?? throw new ArgumentNullException(nameof(ICityRepository));
            WeatherService = weatherService ?? throw new ArgumentNullException(nameof(IWeatherService));
            CityTemperatureRepository = cityTemperatureRepository ?? throw new ArgumentNullException(nameof(ICityTemperatureRepository));
        }

        public async Task<AddCityResponseDto> AddCityAsync(string postalCode)
        {
            var regex = new Regex(@"^\d{5}-\d{3}$");

            if (!regex.IsMatch(postalCode)) return new AddCityResponseDto { Success = false, Message = "Invalid postal code." };

            var city = await CityRepository.GetByPostalCodeAsync(postalCode);

            if (city != null) return new AddCityResponseDto { Success = false, Message = "Postal code already added." };
            var postalCodeResponse = await PostalCodeService.GetCityNameByPostalCodeAsync(postalCode);

            if (postalCodeResponse.HasFailed) return new AddCityResponseDto { Success = false, Message = "Failed on try get city name." };

            city = new City(postalCodeResponse.CityName, postalCode);

            await CityRepository.AddAsync(city);

            return new AddCityResponseDto
            {
                Data = new CityDto
                {
                    Key = city.Key.ToString(),
                    Name = city.Name,
                    PostalCode = city.PostalCode,
                    CreatedOn = city.CreatedOn.ToString("s")
                },
                Success = true
            };
        }

        public async Task<ResponseDto> RemoveAsync(Guid key)
        {
            var city = await CityRepository.GetByKeyAsync(key);

            if (city == null) return new ResponseDto { Success = false, Message = "City not found." };
            if (!city.Delete()) return new ResponseDto { Success = false, Message = "City is already removed." };

            await CityRepository.RemoveAsync(city);
            return new ResponseDto { Success = true };
        }

        public async Task<GetAllCityResponseDto> GetAllAsync(PagingDto pagingDto)
        {
            if (pagingDto.RecordsPerPage > 100) return new GetAllCityResponseDto { Success = false, Message = "The maximum number of records per page is 100." };

            var citiesTask = CityRepository.GetAllAsync(pagingDto.RecordsPerPage, pagingDto.Page);
            var countTask = CityRepository.CountAsync();

            await Task.WhenAll(citiesTask, countTask);

            var totalPages = Math.DivRem(countTask.Result, pagingDto.RecordsPerPage, out int remainder);
            pagingDto.TotalRecords = countTask.Result;
            pagingDto.TotalPages = remainder > 0 ? totalPages + 1 : totalPages;

            return new GetAllCityResponseDto
            {
                Data = citiesTask.Result.Select(city => new CityDto
                {
                    Key = city.Key.ToString(),
                    Name = city.Name,
                    PostalCode = city.PostalCode,
                    CreatedOn = city.CreatedOn.ToString("s")
                }).ToList(),
                Paging = pagingDto
            };
        }

        public async Task<AddTemperatureResponseDto> AddTemperatureAsync(Guid cityKey, AddTemperatureRequestDto addTemperatureRequestDto)
        {
            var city = await CityRepository.GetByKeyAsync(cityKey);
            if (city == null) return new AddTemperatureResponseDto { Success = false, Message = "City not found." };

            var weather = await WeatherService.GetWeatherByCityAsync(city.Name);
            if (weather.HasFailed) return new AddTemperatureResponseDto { Success = false, Message = "Failed on try get city temperature." };

            var temperature = new CityTemperature(city.Key, weather.Results.Temperature);

            await CityTemperatureRepository.AddAsync(temperature);

            return new AddTemperatureResponseDto
            {
                Data = new CityTemperatureDto
                {
                    City = city.Name,
                    Temperature = temperature.Temperature,
                    CreatedOn = temperature.CreatedOn.ToString("s")
                },

                Success = true
            };
        }
    }
}
