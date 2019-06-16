using NUnit.Framework;
using FluentAssertions;
using Domain.Entities;
using System;

namespace Domain.Tests.Entities
{
    [TestFixture]
    public class CityTest
    {
        [Test(Description = "On create a new city")]
        public void OnCreateCity()
        {
            var key = Guid.NewGuid();
            var cityName = Faker.Address.City();
            var postalCode = Faker.Address.ZipCode();

            var city = new City(key, cityName, postalCode);

            city.CreatedOn.Should().BeOnOrBefore(DateTime.Now);
            city.Key.Should().Be(key);
            city.PostalCode.Should().Be(postalCode);
            city.Name.Should().Be(cityName);
        }

    }
}
