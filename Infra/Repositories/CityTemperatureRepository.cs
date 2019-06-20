using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public async Task<IList<CityTemperature>> GetByCityAsync(Guid cityKey)
        {
            const string getByCityCommand =
                @"select [Key], [CityKey], [Temperature], [CreatedOn] from CityTemperature where [CityKey] = @cityKey and [DeletedAt] is null and [CreatedOn] > @dateFilter order by [CreatedOn]";

            using (var connection = GetConnection())
            {
                try
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = getByCityCommand;
                        command.Parameters.Add(CreateParameter("@cityKey", SqlDbType.UniqueIdentifier, cityKey));
                        command.Parameters.Add(CreateParameter("@dateFilter", SqlDbType.DateTime, DateTime.UtcNow.AddHours(-24)));
                        connection.Open();
                        command.Prepare();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var result = new List<CityTemperature>();

                            while (await reader.ReadAsync()) result.Add(MapToCityTemperature(reader));

                            return result;
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private CityTemperature MapToCityTemperature(SqlDataReader reader)
            => new CityTemperature
            (
                reader.GetGuid(reader.GetOrdinal("Key")),
                reader.GetGuid(reader.GetOrdinal("CityKey")),
                reader.GetInt32(reader.GetOrdinal("Temperature")),
                reader.GetDateTime(reader.GetOrdinal("CreatedOn"))
            );

    }
}
