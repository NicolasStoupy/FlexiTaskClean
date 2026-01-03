using Application.Common.Interfaces;
using Domain.Entities;
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