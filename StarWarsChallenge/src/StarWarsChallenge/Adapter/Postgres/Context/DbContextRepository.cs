using Npgsql;
using System.Data;

namespace StarWarsChallenge.Adapter.Postgres.Context
{
    public class DbContext
    {
        public DbContext(IConfiguration configuration) 
        {
            ConnectionString = configuration.GetConnectionString("Postgres");
        }
        public string ConnectionString { get; private set; }
        public IDbConnection CreateConnection() => new NpgsqlConnection(ConnectionString);
    }
}
