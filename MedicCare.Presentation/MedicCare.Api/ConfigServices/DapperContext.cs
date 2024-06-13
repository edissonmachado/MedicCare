using System.Data;
using MedicCare.Persistence.Common;
using Npgsql;

namespace MedicCare.Api.ConfigServices
{
    public class DapperContext : IDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MedicCareDb");
        }
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_connectionString);
    }
}
