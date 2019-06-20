using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICityTemperatureRepository
    {
        Task<IList<CityTemperature>> GetByCityAsync(Guid cityKey);

        Task<IList<CityTemperature>> GetAll();

        Task AddAsync(CityTemperature cityTemperature);
    }
}
