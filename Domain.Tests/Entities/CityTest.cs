using System;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Domain.Tests.Entities
{
    [TestFixture]
    public class CityTest
    {
        [Test(Description = "On create a new city")]
        public void OnCreateCityWithNameAndPostalCode()
        {
            var cityName = Faker.Address.City();
            var postalCode = Faker.Address.ZipCode();
            var city = new City(cityName, postalCode);

            city.CreatedOn.Should().BeOnOrBefore(DateTime.UtcNow);
            city.PostalCode.Should().Be(postalCode);
            city.Name.Should().Be(cityName);
            city.IsDeleted.Should().BeFalse();
        }

        [Test(Description = "On create a new city")]
        public void OnCreateCity()
        {
            var key = Guid.NewGuid();
            var cityName = Faker.Address.City();
            var postalCode = Faker.Address.ZipCode();
            var createdOn = DateTime.Now.AddMonths(-10);

            var city = new City(key, cityName, postalCode, createdOn);

            city.Key.Should().Be(key);
            city.Name.Should().Be(cityName);
            city.PostalCode.Should().Be(postalCode);
            city.CreatedOn.Should().Be(createdOn);
        }
    }
}
