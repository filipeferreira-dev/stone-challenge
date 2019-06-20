using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICityRepository
    {
        Task AddAsync(City city);

        Task RemoveAsync(City city);

        Task<City> GetByKeyAsync(Guid key);

        Task<City> GetByPostalCodeAsync(string postalCode);

        Task<IList<City>> GetAllAsync(int recordsPerPage, int page);

        Task<IList<City>> GetAllAsync();

        Task<int> CountAsync(); 
    }
}
