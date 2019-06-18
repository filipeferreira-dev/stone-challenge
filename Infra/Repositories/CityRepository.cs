using System.Data.SqlClient;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class CityRepository : ICityRepository
    {
        string StringConnection { get; }

        public CityRepository()
        {
            StringConnection = "";
        }

        public async Task AddAsync(City city)
        {
            const string insertCommand =
                    @"insert into City
                    (Key, Name, PostalCode, CreatedOn) Values
                    (@key, @name, @postalCode, @createdOn)";

            using (var connection = new SqlConnection(StringConnection))
            {
                var command = connection.CreateCommand();
                command.CommandText = insertCommand;
                command.Parameters.Add(new SqlParameter("@key", city.Key));
                command.Parameters.Add(new SqlParameter("@name", city.Name));
                command.Parameters.Add(new SqlParameter("@postalCode", city.PostalCode));
                command.Parameters.Add(new SqlParameter("@createdOn", city.CreatedOn));
                command.Prepare();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
