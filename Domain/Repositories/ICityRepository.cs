using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICityRepository
    {
        Task AddAsync(City city);

        Task RemoveAsync(City city);

        Task<City> GetByKey(Guid key);
    }
}
