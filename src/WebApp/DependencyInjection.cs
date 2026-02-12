    using Application.Common.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MudBlazor.Services;
using MudBlazor.Translations;
using WebApp.Services;

namespace WebApp
{
    /// <summary>
    /// Fournit des méthodes d'extension pour enregistrer les services spécifiques à la couche WebApp
    /// dans le conteneur d'injection de dépendances de l'application.
    /// </summary>
    /// <remarks>
    /// Cette classe centralise l'enregistrement des services utilisés par l'interface Blazor,
    /// notamment les services d'identité, de localisation, de thème et les services MudBlazor.
    /// Appeler <see cref="AddWebAppServices(IHostApplicationBuilder)"/> depuis <c>Program.Main</c>
    /// ou lors de la configuration de l'hôte pour s'assurer que tous les services nécessaires
    /// sont disponibles via l'injection de dépendances.
    /// </remarks>
    public static class DependencyInjection
    {
        // Méthodes d’extension pour enregistrer les services de la couche "WebApp"
        // dans le conteneur d’injection de dépendances.
        /// <summary>
        /// Enregistre les services requis par l'application Web (Blazor) dans le conteneur DI.
        /// </summary>
        /// <param name="builder">Le <see cref="IHostApplicationBuilder"/> utilisé pour configurer les services.</param>
        /// <remarks>
        /// Services enregistrés (exemples) :
        /// - <c>IUser</c> implémenté par <c>CurrentUser</c> (scoped)
        /// - <c>LocalizationOptionsProvider</c> (singleton)
        /// - <c>IIdentityService</c> implémenté par <c>IdentityService</c> (scoped)
        /// - Services UI: <c>ThemeService</c>, <c>UiMediator</c>, <c>FlagService</c> (scoped)
        /// - Intégration MudBlazor : services principaux, dialogues, snackbar config et resize listener
        /// </remarks>
        public static void AddWebAppServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUser, CurrentUser>();
            builder.Services.AddSingleton<LocalizationOptionsProvider>();
            builder.Services.AddScoped<IIdentityService, IdentityService>();
            builder.Services.AddScoped<ThemeService>();
            builder.Services.AddScoped<UiMediator>();
            builder.Services.AddScoped<FlagService>();  
            builder.Services.AddScoped<HistoryService>();
            builder.Services.AddMudServices();
            builder.Services.AddMudBlazorDialog();
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
                config.SnackbarConfiguration.PreventDuplicates = true;
                config.SnackbarConfiguration.NewestOnTop = true;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 3500;
                config.SnackbarConfiguration.HideTransitionDuration = 250;
                config.SnackbarConfiguration.ShowTransitionDuration = 250;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
            });
            builder.Services.AddMudBlazorResizeListener();
            builder.Services.AddMudTranslations();
          
        }
    }
}