﻿using System;
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

        public CityService(IPostalCodeService postalCodeService, ICityRepository cityRepository)
        {
            PostalCodeService = postalCodeService ?? throw new ArgumentNullException(nameof(IPostalCodeService));
            CityRepository = cityRepository ?? throw new ArgumentNullException(nameof(ICityRepository));
        }

        public async Task<ResponseDto> AddCityAsync(string postalCode)
        {
            var regex = new Regex(@"^\d{5}-\d{3}$");

            if (!regex.IsMatch(postalCode)) return new ResponseDto { Success = false, Message = "Invalid postal code." };

            var postalCodeResponse = await PostalCodeService.GetCityNameByPostalCodeAsync(postalCode);

            if (postalCodeResponse.HasFailed) return new ResponseDto { Success = false, Message = "Failed on try get city name." };

            var city = new City(postalCodeResponse.CityName, postalCode);

            await CityRepository.AddAsync(city);

            return new ResponseDto { Success = true };
        }
    }
}