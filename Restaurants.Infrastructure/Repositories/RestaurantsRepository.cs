using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
    {
        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants =await dbContext.Restaurants.ToListAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetById(int id)
        {
            var restaurant = await dbContext.Restaurants
                .Include(x => x.Dishes)
                .FirstOrDefaultAsync(x => x.Id == id);
            return restaurant;
        }
    }
}
