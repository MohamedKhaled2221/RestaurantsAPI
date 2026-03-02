using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration )
        {
            var connectionstring = configuration.GetConnectionString("RestaurantsDb");
            services.AddDbContext<RestaurantsDbContext>(options=>
            options.UseSqlServer(connectionstring)
                    .EnableSensitiveDataLogging());

            services.AddIdentityApiEndpoints<User>()
                    .AddRoles<IdentityRole>()
                    .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipleFactory>()
                    .AddEntityFrameworkStores<RestaurantsDbContext>();

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantsRepository , RestaurantsRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
            services.AddAuthorizationBuilder()
             .AddPolicy(PolicyNames.HasNationality, policy => policy.RequireClaim( AppClaimTypes.Nationality, "German", "Polish"))
              .AddPolicy(PolicyNames.AtLeast20, policy => policy.AddRequirements(new MinimumAgeRequirement(20)));
            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            services.AddScoped<IRestaurantAuthorizationService,RestaurantAuthorizationService >();
        }
    }
}

