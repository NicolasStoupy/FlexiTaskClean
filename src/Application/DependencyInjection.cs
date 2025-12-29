using Application.Common.Behaviours;
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
        /// <example>
        /// Exemple d’utilisation :
        /// <code>
        /// var builder = Host.CreateApplicationBuilder(args);
        /// builder.AddApplicationServices();
        /// </code>
        /// </example>
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {         
            builder.Services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            var licenseKey = builder.Configuration["MediatR:LicenseKey"];
           
            builder.Services.AddMediatR(cfg => {
                cfg.LicenseKey = licenseKey;
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenRequestPreProcessor(typeof(LoggingBehaviour<>));
                cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
                //cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                //cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
            });
        }
    }
}
