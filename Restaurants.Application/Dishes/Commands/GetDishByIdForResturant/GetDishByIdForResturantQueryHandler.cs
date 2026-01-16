using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.GetDishByIdForResturant
{
    public class GetDishByIdForResturantQueryHandler(ILogger<GetDishByIdForResturantQueryHandler> logger
     ,IRestaurantsRepository restaurantsRepository, IMapper mapper  ) : IRequestHandler<GetDishByIdForResturantQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetDishByIdForResturantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving dish with id: {DishId} for restaurant with id: {RestaurantId}",request.DishId,request.RestaurantId);
            var restaurant = await restaurantsRepository.GetById(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
            if (dish == null) throw new NotFoundException(nameof(Dish), request.DishId.ToString());
            var results = mapper.Map<DishDto>(dish);
            return results;
        }
    }
}
