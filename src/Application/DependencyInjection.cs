using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Common.Interfaces.Tasks;
using Domain.Entities.Tasks;
using Domain.Factories;
using Domain.Factories.Requests;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Application
{
    /// <summary>
    /// Fournit des méthodes d’extension pour enregistrer les services de l’application
    /// dans le conteneur d’injection de dépendances.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Enregistre tous les services de la couche "Application" dans le conteneur d’injection
        /// via le <see cref="IHostApplicationBuilder"/>.  
        /// Cette méthode est généralement appelée au démarrage de l’application dans le fichier <c>Program.cs</c>.
        /// </summary>
        /// <param name="builder">
        /// L’instance du <see cref="IHostApplicationBuilder"/> utilisée pour configurer les services.
        /// </param>
        /// <remarks>
        /// Comportement et enregistrements effectués :
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// AutoMapper : enregistre automatiquement tous les profils de mapping définis dans l'assembly courant
        /// (utilisé pour transformer des DTOs/commandes vers des modèles/domaines et inversement).
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// FluentValidation : scanne et enregistre toutes les règles de validation (<c>IValidator{T}</c>)
        /// présentes dans l'assembly courant.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// MediatR : configure MediatR en lisant la clé de licence (si nécessaire), en enregistrant les handlers
        /// depuis l'assembly courant, et en ajoutant les comportements (pipeline behaviours) suivants :
        /// <list type="bullet">
        /// <item><description><c>LoggingBehaviour&lt;TRequest&gt;</c> : pré-processeur pour journaliser les requêtes.</description></item>
        /// <item><description><c>UnhandledExceptionBehaviour&lt;TRequest,TResponse&gt;</c> : capture et normalise les exceptions non gérées.</description></item>
        /// <item><description><c>AuthorizationBehaviour&lt;TRequest,TResponse&gt;</c> : applique les règles d'autorisation basées sur les attributs/sécurité.</description></item>
        /// <item><description><c>ValidationBehaviour&lt;TRequest,TResponse&gt;</c> : exécute les validateurs FluentValidation avant l'exécution des handlers.</description></item>
        /// </list>
        /// Un comportement optionnel <c>PerformanceBehaviour&lt;TRequest,TResponse&gt;</c> est commenté et peut être activé
        /// pour mesurer les temps d'exécution des requêtes.
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Exemple d’utilisation :
        /// <code>
        /// var builder = Host.CreateApplicationBuilder(args);
        /// builder.AddApplicationServices();
        /// </code>
        /// </example>
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            // Enregistre AutoMapper et scanne les profils dans l'assembly courant.
            builder.Services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

            // Enregistre tous les validateurs FluentValidation trouvés dans l'assembly courant.
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Lecture de la clé de licence MediatR (peut être null si non configurée).
            var licenseKey = builder.Configuration["MediatR:LicenseKey"];

            // Configuration de MediatR :
            // - définit la licence si fournie,
            // - enregistre handlers, pré-processeurs et behaviours depuis l'assembly courant,
            // - ajoute les behaviors utilisés par l'application (logging, validation, autorisation, gestion d'exceptions).
            builder.Services.AddMediatR(cfg => {
                cfg.LicenseKey = licenseKey;
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenRequestPreProcessor(typeof(LoggingBehaviour<>));
                cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
                cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                //cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
            });
            builder.Services.Configure<TaskLockOptions>(builder.Configuration.GetSection("TaskLock"));

            builder.Services.AddScoped<ITaskCreationFacade, TaskCreationFacade>();

            builder.Services.AddScoped<ITaskCreator<CreateOneWayTransportTask>, TransportTaskCreator>();
            builder.Services.AddScoped<ITaskCreator<CreateMultiStageTransportTask>, MultiStageTransportTaskCreator>();
            builder.Services.AddScoped<ITaskCreator<CreateLoadingTaskRequests>, LoadingTaskCreation>();

            builder.Services.AddScoped<ITaskCreator<EmptySupportTaskRequest>, EmptySupportTaskCreator>();
        }
    }
}
