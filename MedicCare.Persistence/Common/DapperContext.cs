using System.Data;
using Npgsql;

namespace MedicCare.Persistence.Common
{
    public class DapperContext : IDbContext
    {
        private readonly string _connectionString;

        public DapperContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
    }
}
