using System;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Domain.Tests.Entities
{
    [TestFixture]
    public class CityTemperatureTest
    {
        [Test(Description = "Should create a cityTemperature successfully")]
        public void OnCreateCityTemperatureWithCityKeyAndTemperature()
        {
            var cityKey = Guid.NewGuid();
            var temperature = Faker.RandomNumber.Next(0, 30);

            var cityTemperature = new CityTemperature(cityKey, temperature);

            cityTemperature.CreatedOn.Should().BeBefore(DateTime.UtcNow);
            cityTemperature.DeletedAt.Should().BeNull();
            cityTemperature.IsDeleted.Should().BeFalse();
            cityTemperature.CityKey.Should().Be(cityKey);
            cityTemperature.Temperature.Should().Be(temperature);
        }

        [Test(Description = "Should create a cityTemperature successfully")]
        public void OnCreateCityTemperature()
        {
            var key = Guid.NewGuid();
            var cityKey = Guid.NewGuid();
            var temperature = Faker.RandomNumber.Next(0, 30);
            var createdOn = DateTime.UtcNow.AddDays(-24);

            var cityTemperature = new CityTemperature(key, cityKey, temperature, createdOn);

            cityTemperature.CreatedOn.Should().Be(createdOn);
            cityTemperature.DeletedAt.Should().BeNull();
            cityTemperature.IsDeleted.Should().BeFalse();
            cityTemperature.CityKey.Should().Be(cityKey);
            cityTemperature.Temperature.Should().Be(temperature);
            cityTemperature.Key.Should().Be(key);
        }
    }
}
