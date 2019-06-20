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
        public void OnCreateCityTemperature()
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
    }
}
