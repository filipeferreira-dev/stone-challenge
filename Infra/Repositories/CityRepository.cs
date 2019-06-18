using CrossCutting.Settings;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class CityRepository : Repository, ICityRepository
    {
        public CityRepository(IOptions<ConnectionStrings> connectionStringsOptions) : base(connectionStringsOptions)
        {
        }

        public async Task AddAsync(City city)
        {
            //TODO : REMOVE TRY CATH
            try
            {
                const string insertCommand =
                            @"insert into City
                                ([Key], [Name], [PostalCode], [CreatedOn]) Values
                                (@key, @name, @postalCode, @createdOn)";

                using (var connection = new SqlConnection(ConnectionString.FullString))
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
    }
}
