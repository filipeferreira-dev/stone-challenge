using CrossCutting.Settings;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class CityRepository : Repository, ICityRepository
    {
        public CityRepository(IOptions<ConnectionStrings> connectionStringsOptions) : base(connectionStringsOptions) { }

        public async Task AddAsync(City city)
        {
            const string insertCommand = @"insert into City ([Key], [Name], [PostalCode], [CreatedOn]) Values (@key, @name, @postalCode, @createdOn)";

            using (var connection = GetConnection())
            {
                try
                {
                    using (var command = connection.CreateCommand())
                    {
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
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task RemoveAsync(City city)
        {
            const string softDeleteCommand = @"update City set [DeletedAt] = @deletedAt where [Key] = @key";

            using (var connection = GetConnection())
            {
                try
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = softDeleteCommand;
                        command.Parameters.Add(CreateParameter("@key", SqlDbType.UniqueIdentifier, city.Key));
                        command.Parameters.Add(CreateParameter("@deletedAt", SqlDbType.DateTime, city.DeletedAt));
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

        public async Task<City> GetByKeyAsync(Guid key)
        {
            const string getByKeyCommand =
                @"select [Key], [Name], [PostalCode], [CreatedOn] from City where [Key] = @key and [DeletedAt] is null";

            using (var connection = GetConnection())
            {
                try
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = getByKeyCommand;
                        command.Parameters.Add(CreateParameter("@key", SqlDbType.UniqueIdentifier, key));
                        connection.Open();
                        command.Prepare();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            try
                            {
                                if (await reader.ReadAsync()) return MapToCity(reader);
                            }
                            finally
                            {
                                reader.Close();
                            }
                        }

                        return null;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<City> GetByPostalCodeAsync(string postalCode)
        {
            const string getByPostalCodeCommand =
                @"select [Key], [Name], [PostalCode], [CreatedOn] from City where [PostalCode] = @postalCode and [DeletedAt] is null";

            using (var connection = GetConnection())
            {
                try
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = getByPostalCodeCommand;
                        command.Parameters.Add(CreateParameter("@postalCode", SqlDbType.VarChar, postalCode, 9));
                        connection.Open();
                        command.Prepare();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            try
                            {
                                if (await reader.ReadAsync()) return MapToCity(reader);
                            }
                            finally
                            {
                                reader.Close();
                            }
                        }

                        return null;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<IList<City>> GetAllAsync(int recordsPerPage, int page)
        {
            string getByAllCommand =
                $"select [Key], [Name], [PostalCode], [CreatedOn] from City where [DeletedAt] is null order by [CreatedOn] desc offset {(page - 1) * recordsPerPage} rows fetch next {recordsPerPage} rows only";

            using (var connection = GetConnection())
            {
                try
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = getByAllCommand;
                        connection.Open();
                        command.Prepare();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            try
                            {
                                var list = new List<City>();
                                while (await reader.ReadAsync()) list.Add(MapToCity(reader));
                                return list;
                            }
                            finally
                            {
                                reader.Close();
                            }
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<IList<City>> GetAllAsync()
        {
            string getByAllCommand = $"select [Key], [Name], [PostalCode], [CreatedOn] from City where [DeletedAt] is null";

            using (var connection = GetConnection())
            {
                try
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = getByAllCommand;
                        connection.Open();
                        command.Prepare();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            try
                            {
                                var list = new List<City>();
                                while (await reader.ReadAsync()) list.Add(MapToCity(reader));
                                return list;
                            }
                            finally
                            {
                                reader.Close();
                            }
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<int> CountAsync()
        {
            const string countCities = @"select count(1) from City where [DeletedAt] is null";

            using (var connection = GetConnection())
            {
                try
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = countCities;
                        connection.Open();
                        command.Prepare();

                        return Convert.ToInt32(await command.ExecuteScalarAsync());
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private City MapToCity(SqlDataReader reader)
           => new City(
                       reader.GetGuid(reader.GetOrdinal("Key")),
                       reader.GetString(reader.GetOrdinal("Name")),
                       reader.GetString(reader.GetOrdinal("PostalCode")),
                       reader.GetDateTime(reader.GetOrdinal("CreatedOn"))
                   );
    }
}

