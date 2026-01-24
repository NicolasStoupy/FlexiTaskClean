using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mappings
{
    /// <summary>
    /// Extensions utilitaires pour les opérations de mapping avec AutoMapper.
    /// Fournit des méthodes d'extension pour projeter des requêtes IQueryable vers des types de destination (DTO).
    /// </summary>
    public static class MappingExtensions
    {
        /// <summary>
        /// Projette une source <see cref="IQueryable"/> vers une liste asynchrone de <typeparamref name="TDestination"/>
        /// en utilisant <see cref="AutoMapper.QueryableExtensions.ProjectTo{TDestination}(IQueryable, IConfigurationProvider)"/>.
        /// La requête résultante est exécutée en lecture seule via <see cref="EntityFrameworkCoreQueryableExtensions.AsNoTracking{TEntity}(IQueryable{TEntity})"/>
        /// puis matérialisée avec <see cref="Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync{TSource}(IQueryable{TSource}, System.Threading.CancellationToken)"/>.
        /// </summary>
        /// <typeparam name="TDestination">Le type de destination (généralement un DTO). Doit être une classe.</typeparam>
        /// <param name="queryable">La source <see cref="IQueryable"/> à projeter.</param>
        /// <param name="configuration">La configuration AutoMapper (<see cref="IConfigurationProvider"/>) contenant les maps nécessaires.</param>
        /// <param name="cancellationToken">Jeton d'annulation optionnel pour l'opération asynchrone.</param>
        /// <returns>
        /// Une tâche représentant l'opération asynchrone, dont le résultat est une <see cref="List{TDestination}"/>
        /// contenant les éléments projetés.
        /// </returns>
        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration, CancellationToken cancellationToken = default) where TDestination : class
            => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync(cancellationToken);
    }
}
