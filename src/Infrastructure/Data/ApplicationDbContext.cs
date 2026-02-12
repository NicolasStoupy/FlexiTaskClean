using Application.Common.Interfaces;
using Domain.Entities.MasterData;
using Domain.Entities.Tasks;
using Domain.Entities.Tasks.TaskSpecializations;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Plant> Plant => Set<Plant>();

        public DbSet<WorkArea> WorkAreas => Set<WorkArea>();

        public DbSet<WorkAreaType> WorkAreaTypes => Set<WorkAreaType>();

        public DbSet<TaskItem> TaskItem => Set<TaskItem>();

        public DbSet<TaskHeader> TaskHeader => Set<TaskHeader>();

        public DbSet<TransportTask> TransportTasks => Set<TransportTask>(); 

        public DbSet<WorkAreaTransport> WorkAreaTransports=> Set<WorkAreaTransport>();

        public DbSet<SupportType> SupportTypes => Set<SupportType>();

        public DbSet<TaskLog> TaskLogs => Set<TaskLog>();

        public DbSet<LoadingTask> LoadingTasks => Set<LoadingTask>();

        public DbSet<WorkAreaTransportSupport> workAreaTransportSupports => Set<WorkAreaTransportSupport>();

        /// <summary>
        /// Configure le modèle de données pour le contexte de base de données lors de sa création.
        /// </summary>
        /// <param name="builder">
        /// L’instance de <see cref="ModelBuilder"/> utilisée pour configurer les entités, relations,
        /// clés primaires et autres aspects du modèle.
        /// </param>
        /// <remarks>
        /// Cette méthode applique automatiquement toutes les configurations d’entités implémentées
        /// dans l’assembly courant à l’aide de <see cref="ModelBuilder.ApplyConfigurationsFromAssembly(Assembly)"/>.
        /// Cela permet une configuration centralisée et modulaire des entités via les classes
        /// implémentant <see cref="IEntityTypeConfiguration{TEntity}"/>.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}