using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantsService(IRestaurantsRepository restaurantsRepository,
        ILogger<RestaurantsService> logger, IMapper mapper) : IRestaurantsService
    {
        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            logger.LogInformation("Getting All Restaurants");
            var restaurants = await restaurantsRepository.GetAllAsync();
            var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);


            return restaurantsDtos!;

        }
        public async Task<RestaurantDto?>GetById(int id )
        {
            logger.LogInformation($"Getting Restaurant {id}");
            var restaurant = await restaurantsRepository.GetById(id);
            var restaurantDto= mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }

    }
}
