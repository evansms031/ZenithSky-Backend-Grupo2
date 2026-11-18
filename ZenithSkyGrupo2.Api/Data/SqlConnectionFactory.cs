using Microsoft.Data.SqlClient;

namespace ZenithSkyGrupo2.Api.Data
{
    public class SqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration
                .GetConnectionString("ZenithSkyDb")
                ?? throw new InvalidOperationException(
                    "No se encontró la conexión ZenithSkyDb");
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
