
using Domain.Constants;
using Domain.Entities.MasterData;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

            await initialiser.InitialiseAsync();
            await initialiser.SeedAsync();
        }
    }

    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                var dbConnection = _context.Database.GetDbConnection();
                _logger.LogWarning("EF RAW CS = {cs}", dbConnection.ConnectionString);
                _logger.LogWarning("EF DataSource={ds} Database={db}", dbConnection.DataSource, dbConnection.Database);
                _logger.LogWarning(
                    "EF is connecting to: DataSource='{DataSource}', Database='{Database}', ConnectionString='{ConnectionString}'",
                    dbConnection.DataSource,
                    dbConnection.Database,
                    dbConnection.ConnectionString);

                //await _context.Database.EnsureDeletedAsync();
                //await _context.Database.EnsureCreatedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }


        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Default roles
            var administratorRole = new IdentityRole(Roles.Administrator);

            if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await _roleManager.CreateAsync(administratorRole);
            }
            var userRole = new IdentityRole(Roles.Users);
            if(_roleManager.Roles.All(r=>r.Name != userRole.Name))
            {
                await _roleManager.CreateAsync(userRole);
            }

            // Default users
            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (_userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await _userManager.CreateAsync(administrator, "Administrator1!");
                if (!string.IsNullOrWhiteSpace(administratorRole.Name))
                {
                    await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
                }
            }
            //user 
            var user = new ApplicationUser { UserName = "nicolas@localhost", Email = "user@localhost" };

            if (_userManager.Users.All(u => u.UserName != user.UserName))
            {
                var result =await _userManager.CreateAsync(user, "Admin1!");
                if (!string.IsNullOrWhiteSpace(userRole.Name))
                {
                    await _userManager.AddToRolesAsync(user, new[] { userRole.Name });
                }
            }
            // Default data
            // Seed, if necessary
            if (!_context.Plant.Any())
            {
                _context.Plant.Add(new Plant()
                {
                    Code = "MOU1",
                    CommonName = "Moustier",
                    Language = Domain.Enums.PlantLanguage.FR
                });
               

                await _context.SaveChangesAsync();
            }
        }
    }
}
