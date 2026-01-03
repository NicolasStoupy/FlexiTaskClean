using Application.FunctionalTests.Interfaces;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Respawn;
using System.Data.Common;

using Microsoft.EntityFrameworkCore.Diagnostics;
using Testcontainers.MsSql;

//
// PSEUDOCODE (plan détaillé en français) :
//
// 1. Définir la classe de test de base de données qui gère un conteneur MSSQL pour les tests fonctionnels.
// 2. Champs privés : nom de la base par défaut, instance du conteneur, connexion, chaîne de connexion et Respawner.
// 3. Constructeur : construire le conteneur MSSQL avec mot de passe et option d'auto-suppression.
// 4. InitialiseAsync :
//    a. Démarrer le conteneur.
//    b. Créer une base de données dédiée (DefaultDatabase) dans le conteneur.
//    c. Construire une chaîne de connexion qui pointe vers DefaultDatabase.
//    d. Initialiser une DbConnection (SqlConnection) pour l'accès direct si nécessaire.
//    e. Construire DbContextOptions pour ApplicationDbContext en spécifiant la chaîne de connexion.
//    f. Instancier ApplicationDbContext et forcer la recréation de la base (EnsureDeleted puis EnsureCreated)
//       pour garantir un état propre.
//    g. Initialiser Respawner pour permettre la restauration de l'état entre tests.
// 5. GetConnection : retourner l'objet DbConnection initialisé.
// 6. GetConnectionString : retourner la chaîne de connexion utilisée.
// 7. ResetAsync : utiliser Respawner pour réinitialiser l'état de la base de données entre tests.
// 8. DisposeAsync : libérer la connexion et arrêter/éliminer le conteneur.
// 9. Documenter chaque membre public et les champs importants avec des commentaires XML en français.
// 10. Ne pas modifier la logique fonctionnelle — uniquement ajouter de la documentation et des commentaires explicatifs.
//
// Fin du pseudocode.
//

namespace Application.FunctionalTests.Databases
{
    /// <summary>
    /// Fournit une base de données SQL Server isolée pour les tests fonctionnels,
    /// basée sur Testcontainers (MsSqlContainer). Cette classe gère le cycle de vie
    /// du conteneur, la création de la base de données de test, la connexion et
    /// la réinitialisation via Respawn.
    /// </summary>
    public class SqlTestcontainersTestDatabase : ITestDatabase
    {

        public bool Dispose = false;
        
        /// <summary>
        /// Nom de la base de données utilisée à l'intérieur du conteneur pour les tests.
        /// </summary>
        private const string DefaultDatabase = "FlexiTaskDev";

        /// <summary>
        /// Instance du conteneur MSSQL fournie par Testcontainers.
        /// </summary>
        private readonly MsSqlContainer _container;

        /// <summary>
        /// Connexion ADO.NET vers la base de test (InitialCatalog = <see cref="DefaultDatabase"/>).
        /// Initialisée dans <see cref="InitialiseAsync"/>.
        /// </summary>
        private DbConnection _connection = null!;

        /// <summary>
        /// Chaîne de connexion complète pointant vers la base de test. Initialisée dans <see cref="InitialiseAsync"/>.
        /// </summary>
        private string _connectionString = null!;

        /// <summary>
        /// Respawner utilisé pour remettre la base dans un état propre entre les tests.
        /// </summary>
        private Respawner _respawner = null!;

        /// <summary>
        /// Construit une nouvelle instance et configure le conteneur MSSQL.
        /// Le mot de passe et l'option d'auto-suppression sont définis ici.
        /// </summary>
        public SqlTestcontainersTestDatabase()
        {
            _container = new MsSqlBuilder()
                .WithPassword("NIOLAb2024!")
                .WithAutoRemove(false)
                .Build();
        }

        /// <summary>
        /// Démarre le conteneur, crée la base de données de test, construit la chaîne
        /// de connexion, initialise le <see cref="ApplicationDbContext"/> et configure Respawn.
        /// Appeler avant d'exécuter des tests qui utilisent la base de données.
        /// </summary>
        /// <returns>Une tâche asynchrone représentant l'opération d'initialisation.</returns>
        public async Task InitialiseAsync()
        {
            // Démarrage du conteneur MSSQL
            await _container.StartAsync();

            // Création explicite d'une base de données dédiée à l'intérieur du conteneur
            await _container.ExecScriptAsync($"CREATE DATABASE {DefaultDatabase}");

            // Construire une chaîne de connexion qui cible la base nouvellement créée
            var builder = new SqlConnectionStringBuilder(_container.GetConnectionString())
            {
                InitialCatalog = DefaultDatabase
            };

            _connectionString = builder.ConnectionString;

            // Préparer une connexion ADO.NET si besoin d'accès direct (transactions, Respawn, etc.)
            _connection = new SqlConnection(_connectionString);

            // Construire les options du DbContext afin d'exécuter EnsureDeleted/EnsureCreated
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_connectionString)
                .ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning))
                .Options;

            var context = new ApplicationDbContext(options);

            // Forcer une base propre : supprimer si existe puis créer à partir du modèle EF
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            // Log basique pour aider au débogage (affiche la chaîne de connexion du conteneur)
            Console.WriteLine("CONTAINER CS = " + _container.GetConnectionString());

            // Initialiser Respawner pour permettre la réinitialisation entre tests
            _respawner = await Respawner.CreateAsync(_connectionString);
        }

        /// <summary>
        /// Retourne la connexion ADO.NET initialisée vers la base de test.
        /// </summary>
        /// <returns>Une instance de <see cref="DbConnection"/> (généralement <see cref="SqlConnection"/>).</returns>
        public DbConnection GetConnection() => _connection;

        /// <summary>
        /// Retourne la chaîne de connexion complète utilisée pour accéder à la base de test.
        /// </summary>
        /// <returns>La chaîne de connexion SQL Server.</returns>
        public string GetConnectionString() => _connectionString;

        /// <summary>
        /// Réinitialise l'état de la base de données en utilisant Respawn.
        /// Permet d'obtenir un état propre entre deux tests sans redémarrer le conteneur.
        /// </summary>
        /// <returns>Une tâche asynchrone représentant l'opération de réinitialisation.</returns>
        public async Task ResetAsync() => await _respawner.ResetAsync(_connectionString);

        /// <summary>
        /// Libère les ressources : ferme la connexion et supprime/arrête le conteneur.
        /// Appeler une fois tous les tests terminés.
        /// </summary>
        /// <returns>Une tâche asynchrone représentant l'opération de nettoyage.</returns>
        public async Task DisposeAsync()
        {

            await _connection.DisposeAsync();

            // Si KEEP_TESTCONTAINERS=1 => on ne détruit pas le container
            if (!Dispose)
                return;

            await _container.DisposeAsync();
            await _connection.DisposeAsync();
            await _container.DisposeAsync();
        }
    }
}