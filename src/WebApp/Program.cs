using Application;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using WebApp;
using WebApp.Components;
using WebApp.Components.Account;
using WebApp.Services;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();
var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebAppServices();


builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");// Ajout de la localisation

builder.Services.AddRazorComponents() // Ajout des composants Razor
              .AddInteractiveServerComponents(); // Mode interactif côté serveur
builder.Services.AddControllers();

// Configuration des services d’Identity
builder.Services.AddCascadingAuthenticationState(); // Ajout de l’état d’authentification en cascade
builder.Services.AddScoped<IdentityRedirectManager>();// Ajout du gestionnaire de redirection d’identité
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();// Fournit l’état d’authentification

builder.Services.AddMudServices();
builder.Services.AddMudBlazorDialog();
builder.Services.AddMudBlazorSnackbar();
builder.Services.AddMudBlazorResizeListener();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
// Configuration des localizations
var locProvider = app.Services.GetRequiredService<LocalizationOptionsProvider>();
app.UseRequestLocalization(locProvider.GetLocalizationOptions());
app.UseAntiforgery();

// IMPORTANT pour Identity
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
await app.InitialiseDatabaseAsync();



app.Run();

