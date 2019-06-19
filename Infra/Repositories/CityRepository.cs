using System;
using System.Data;
using System.Threading.Tasks;
using CrossCutting.Settings;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Options;

namespace Infra.Repositories
{
    public class CityRepository : Repository, ICityRepository
    {
        public CityRepository(IOptions<ConnectionStrings> connectionStringsOptions) : base(connectionStringsOptions)
        {
        }

        public async Task AddAsync(City city)
        {
            const string insertCommand = @"insert into City ([Key], [Name], [PostalCode], [CreatedOn]) Values (@key, @name, @postalCode, @createdOn)";

            using (var connection = GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = insertCommand;
                command.Parameters.Add(CreateParameter("@key", SqlDbType.UniqueIdentifier, city.Key));
                command.Parameters.Add(CreateParameter("@name", SqlDbType.VarChar, city.Name, 500));
                command.Parameters.Add(CreateParameter("@postalCode", SqlDbType.VarChar, city.PostalCode, 9));
                command.Parameters.Add(CreateParameter("@createdOn", SqlDbType.DateTime, city.CreatedOn));
                connection.Open();
                command.Prepare();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task RemoveAsync(City city)
        {
            const string softDeleteCommand = @"update City set [DeletedAt] = @deletedAt where [Key] = @key";

            using (var connection = GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = softDeleteCommand;
                command.Parameters.Add(CreateParameter("@key", SqlDbType.UniqueIdentifier, city.Key));
                command.Parameters.Add(CreateParameter("@deletedAt", SqlDbType.DateTime, city.DeletedAt));
                connection.Open();
                command.Prepare();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<City> GetByKey(Guid key)
        {
            const string softDeleteCommand = @"select [Key], [Name], [PostalCode], [CreatedOn], [DeletedAt] from City where [Key] = @key";

            using (var connection = GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = softDeleteCommand;
                command.Parameters.Add(CreateParameter("@key", SqlDbType.UniqueIdentifier, key));
                connection.Open();
                command.Prepare();
                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new City(
                                    reader.GetGuid(reader.GetOrdinal("Key")),
                                    reader.GetString(reader.GetOrdinal("Name")),
                                    reader.GetString(reader.GetOrdinal("PostalCode")),
                                    reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                                    await reader.IsDBNullAsync(reader.GetOrdinal("DeletedAt")) ? new DateTime?() : reader.GetDateTime(reader.GetOrdinal("DeletedAt"))
                                    );
                }

                return null;
            }
        }
    }
}
