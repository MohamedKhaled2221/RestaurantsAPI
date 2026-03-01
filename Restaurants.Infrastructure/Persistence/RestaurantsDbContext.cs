using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence
{
    internal class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : IdentityDbContext<User>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>()
                        .OwnsOne(r => r.Address);

            // Ensure EF always creates an Address instance (prevents OptionalDependentWithoutIdentifyingPropertyWarning)
            modelBuilder.Entity<Restaurant>()
                        .Navigation(r => r.Address)
                        .IsRequired();

            modelBuilder.Entity<Restaurant>()
                        .HasMany(d => d.Dishes)
                        .WithOne()
                        .HasForeignKey(d => d.RestaurantId);

            // Make OwnerId optional to avoid NOT NULL constraint violations
            modelBuilder.Entity<Restaurant>()
                        .HasOne(r => r.Owner)
                        .WithMany(u => u.OwnedRestaurants)
                        .HasForeignKey(r => r.OwnerId)
                        .IsRequired(false);

            modelBuilder.Entity<Dish>()
                        .Property(d => d.Price)
                        .HasPrecision(18, 2);
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Dish> Dishes { get; set; }
    }
}
