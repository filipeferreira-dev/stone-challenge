using CrossCutting.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Infra.Repositories
{
    public abstract class Repository
    {
        protected ConnectionString ConnectionString { get; }

        public Repository(IOptions<ConnectionStrings> connectionString)
        {
            ConnectionString = connectionString?.Value?.St ?? throw new ArgumentException(nameof(ConnectionStrings));
        }

        protected SqlConnection GetConnection() => new SqlConnection(ConnectionString.FullString);

        protected SqlParameter CreateParameter(string name, SqlDbType type, object value, int size = 0)
            => new SqlParameter(name, type, size) { Value = value };
    }
}
