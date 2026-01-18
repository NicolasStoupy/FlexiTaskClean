using Application.Common.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using WebApp.Components.Account;
using WebApp.Services;

namespace WebApp
{
    public static class DependencyInjection
    {
        // Méthodes d’extension pour enregistrer les services de la couche "WebApp"
        // dans le conteneur d’injection de dépendances.
        public static void AddWebAppServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUser, CurrentUser>();
            builder.Services.AddSingleton<LocalizationOptionsProvider>();
            builder.Services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}