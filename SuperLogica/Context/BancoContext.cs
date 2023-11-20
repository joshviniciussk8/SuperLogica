using Microsoft.Data.SqlClient;
using System.Data;

namespace SuperLogica.Context
{
    public class BancoContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public BancoContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
