using Domain.Entities;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;

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
            var deletedAt = DateTime.Now.AddMonths(-1);

            var city = new City(key, cityName, postalCode, createdOn, deletedAt);

            city.Key.Should().Be(key);
            city.Name.Should().Be(cityName);
            city.PostalCode.Should().Be(postalCode);
            city.CreatedOn.Should().Be(createdOn);
            city.DeletedAt.Should().Be(deletedAt);
            city.IsDeleted.Should().BeTrue();
        }

        [Test(Description = "On delete city should set deleteAt date")]
        public void OnDelete()
        {
            var city = Substitute.ForPartsOf<City>();
            city.Delete().Should().BeTrue();
            city.DeletedAt.Should().BeBefore(DateTime.UtcNow);
        }

        [Test(Description = "Should return false on try delete a city twice")]
        public void OnDeleteTwice()
        {
            var city = Substitute.ForPartsOf<City>();
            city.Delete();
            city.Delete().Should().BeFalse();
        }
    }
}
