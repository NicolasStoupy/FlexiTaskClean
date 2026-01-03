using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp;
using WebApp.Components;
using WebApp.Components.Account;


var builder = WebApplication.CreateBuilder(args);


builder.AddApplicationServices();
builder.AddInfrastructureServices();

builder.Services.AddScoped<IUser, CurrentUser>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// IMPORTANT pour Identity
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
await app.InitialiseDatabaseAsync();

app.Run();
