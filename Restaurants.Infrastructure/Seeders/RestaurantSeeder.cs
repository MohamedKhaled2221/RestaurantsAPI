using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Contants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder : IRestaurantSeeder
    {
        private readonly RestaurantsDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RestaurantSeeder(
            RestaurantsDbContext dbContext,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            // Ensure roles exist first
            var roles = GetRoles();
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role.Name!))
                {
                    var createRoleResult = await _roleManager.CreateAsync(role);
                    // consider logging createRoleResult.Errors if not succeeded
                }
            }

            // Now create sample users and assign roles
            await EnsureUserAsync("admin@local", UserRoles.Admin, "Admin", "US", new DateTime(1985, 1, 1));
            await EnsureUserAsync("owner@local", UserRoles.Owner, "Owner", "UK", new DateTime(1990, 6, 15));
            await EnsureUserAsync("user@local", UserRoles.User, "User", "CA", new DateTime(1995, 3, 20));

            /*
            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                await _dbContext.Database.MigrateAsync();
            }

            if (!await _dbContext.Database.CanConnectAsync())
            {
                return;
            }*/

            // Seed restaurants
            // Ensure role & owner first
            await EnsureUserAsync("owner@local", UserRoles.Owner, "Owner", "UK", new DateTime(1990, 6, 15));
            var owner = await _userManager.FindByEmailAsync("owner@local");

            // Seed restaurants with OwnerId
            if (!_dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants().Select(r =>
                {
                    r.OwnerId = owner?.Id;
                    return r;
                }).ToList();
                _dbContext.Restaurants.AddRange(restaurants);
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task EnsureUserAsync(string email, string role, string userName, string nationality, DateTime dob)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null) return;

            user = new User
            {
                UserName = userName,
                Email = email,
                Nationality = nationality,
                DateOfBirth =dob
            };

            var result = await _userManager.CreateAsync(user, "Pa$$w0rd!");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            // In production consider logging/failing on errors instead of ignoring
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            return new List<IdentityRole>
            {
                new IdentityRole(UserRoles.User) { NormalizedName = UserRoles.User.ToUpperInvariant() },
                new IdentityRole(UserRoles.Owner) { NormalizedName = UserRoles.Owner.ToUpperInvariant() },
                new IdentityRole(UserRoles.Admin) { NormalizedName = UserRoles.Admin.ToUpperInvariant() }
            };
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            return new List<Restaurant>
            {
                new Restaurant
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC (short for Kentucky Fried Chicken) ...",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "Nashville Hot Chicken", Description = "Nashville Hot Chicken (10 pcs.)", Price = 10.30M },
                        new Dish { Name = "Chicken Nuggets", Description = "Chicken Nuggets (5 pcs.)", Price = 5.30M }
                    },
                    Address = new Address { City = "London", Street = "Cork St 5", PostalCode = "WC2N 5DU" }
                },
                new Restaurant
                {
                    Name = "McDonald",
                    Category = "Fast Food",
                    Description = "McDonald's Corporation (McDonald's) ...",
                    ContactEmail = "contact@mcdonald.com",
                    HasDelivery = true,
                    Address = new Address { City = "London", Street = "Boots 193", PostalCode = "W1F 8SR" }
                }
            };
        }
    }
}