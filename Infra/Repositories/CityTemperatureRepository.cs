using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CrossCutting.Settings;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Options;

namespace Infra.Repositories
{
    public class CityTemperatureRepository : Repository, ICityTemperatureRepository
    {
        public CityTemperatureRepository(IOptions<ConnectionStrings> connectionString) : base(connectionString) { }

        public async Task AddAsync(CityTemperature cityTemperature)
        {
            const string insertCommand = @"insert into CityTemperature ([Key], [CityKey], [Temperature], [CreatedOn]) Values (@key, @cityKey, @temperature, @createdOn)";

            using (var connection = GetConnection())
            {
                try
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = insertCommand;
                        command.Parameters.Add(CreateParameter("@key", SqlDbType.UniqueIdentifier, cityTemperature.Key));
                        command.Parameters.Add(CreateParameter("@cityKey", SqlDbType.UniqueIdentifier, cityTemperature.CityKey));
                        command.Parameters.Add(CreateParameter("@temperature", SqlDbType.Int, cityTemperature.Temperature));
                        command.Parameters.Add(CreateParameter("@createdOn", SqlDbType.DateTime, cityTemperature.CreatedOn));
                        connection.Open();
                        command.Prepare();
                        await command.ExecuteNonQueryAsync();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Task<IList<CityTemperature>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IList<CityTemperature>> GetByCity(Guid cityKey)
        {
            throw new NotImplementedException();
        }
    }
}
