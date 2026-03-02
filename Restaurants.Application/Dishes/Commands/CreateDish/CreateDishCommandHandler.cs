using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Services;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IDishesRepository dishesRepository , IMapper mapper,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<CreateDishCommand,int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new dish: {@DishRequest}", request);
            var restaurant = await restaurantsRepository.GetById(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
                throw new ForbidException();

            var dish = mapper.Map<Dish>(request);
            return await dishesRepository.Create(dish);
        }
    }
}
