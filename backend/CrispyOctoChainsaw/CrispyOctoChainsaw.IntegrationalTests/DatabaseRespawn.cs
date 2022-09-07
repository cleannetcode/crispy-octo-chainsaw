using Microsoft.Extensions.Configuration;
using Npgsql;
using Respawn;
using Microsoft.EntityFrameworkCore;

namespace CrispyOctoChainsaw.IntegrationalTests
{
    public class DatabaseRespawn : IAsyncLifetime
    {
        public DatabaseRespawn()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets(typeof(DatabaseRespawn).Assembly)
                .Build();

            ConnectionString = builder.GetConnectionString("CrispyOctoChainsawDbContext");
        }

        public string ConnectionString { get; set; }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            var respawner = await CreateRespawner();

            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                await conn.OpenAsync();
                await respawner.ResetAsync(conn);
            }
        }

        private async Task<Respawner> CreateRespawner()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            await connection.OpenAsync();

            var respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.Postgres
            });

            return respawner;
        }
    }
}
