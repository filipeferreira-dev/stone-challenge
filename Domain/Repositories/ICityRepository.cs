using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICityRepository
    {
        Task AddAsync(City city);

        Task RemoveAsync(City city);

        Task<City> GetByKeyAsync(Guid key);

        Task<City> GetByPostalCodeAsync(string postalCode);
    }
}
