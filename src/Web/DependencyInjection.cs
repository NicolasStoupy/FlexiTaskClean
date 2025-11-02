using Application.Common.Interfaces;
using Azure.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web
{
    public static class DependencyInjection
    {
        public static void AddWebServices(this IHostApplicationBuilder builder)
        {
          

            builder.Services.AddScoped<IUser, FakeUser>();

            builder.Services.AddHttpContextAccessor();


            // Customise default API behaviour
            builder.Services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            builder.Services.AddEndpointsApiExplorer();

           
        }

    }
}
