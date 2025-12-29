using Application.FunctionalTests.Interfaces;
using Ardalis.GuardClauses;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Respawn;
using System.Data.Common;

namespace Application.FunctionalTests.Databases
{
    /// <summary>
    /// Fournit une base de données SQL Server pour les tests fonctionnels.
    /// </summary>
    /// <remarks>
    /// Cette classe lit la chaîne de connexion depuis la configuration, ouvre une connexion
    /// SQL partagée, initialise la base (suppression/création) via Entity Framework et
    /// utilise <see cref="Respawner"/> pour permettre une réinitialisation rapide de l'état
    /// de la base entre les tests.
    /// 
    /// Remarques importantes :
    /// - L'initialisation de <see cref="_connection"/> et de <see cref="_respawner"/> se fait
    ///   dans <see cref="InitialiseAsync"/> ; les méthodes publiques supposent donc que cette
    ///   initialisation a été effectuée au préalable.
    /// - La gestion des ressources (ouverture/asynchrone, disposition de contextes EF, etc.)
    ///   doit être effectuée avec précaution dans le code de test appelant.
    /// </remarks>
    public class SqlTestDatabase : ITestDatabase
    {
        private readonly string _connectionString;
        private SqlConnection _connection;
        private Respawner _respawner = null!;

        public SqlTestDatabase()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

            var connectionString = configuration.GetConnectionString("DB");

            Guard.Against.Null(connectionString);

            _connectionString = connectionString;
        }

        public async Task DisposeAsync()
        {
            await _connection.DisposeAsync();
        }

        public DbConnection GetConnection()
        {
            return _connection;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public async Task InitialiseAsync()
        {
            _connection = new SqlConnection(_connectionString);
         
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_connectionString)
                .Options;

            var context = new ApplicationDbContext(options);

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            _connection.Open();
            _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.SqlServer,
                WithReseed = true
            });
        }

        public Task ResetAsync()
        {
            return _respawner.ResetAsync(_connection);
        }
    }
}