using Microsoft.Data.SqlClient;
using System.Data;

namespace SeguimientoTramites.Data;

public class DbContext
{
    private readonly string _connectionString;

    public DbContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}
