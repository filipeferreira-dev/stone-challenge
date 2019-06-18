using Domain.Entities;
using Domain.Repositories;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class CityRepository : ICityRepository
    {
        string StringConnection { get; }

        public CityRepository()
        {
            StringConnection = "Server=localhost;Database=ST;User Id=sa;Password=St#1234567890;";
        }

        public async Task AddAsync(City city)
        {
            try
            {
                const string insertCommand =
                            @"insert into City
                    ([Key], [Name], [PostalCode], [CreatedOn]) Values
                    (@key, @name, @postalCode, @createdOn)";

                using (var connection = new SqlConnection(StringConnection))
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
            catch (Exception e)
            {

                throw;
            }
        }

        private SqlParameter CreateParameter(string name, SqlDbType type, object value, int size = 0)
            => new SqlParameter(name, type, size) { Value = value };
    }
}
