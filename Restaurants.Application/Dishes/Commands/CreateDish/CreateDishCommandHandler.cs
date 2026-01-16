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

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IDishesRepository dishesRepository , IMapper mapper) : IRequestHandler<CreateDishCommand,int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new dish: {@DishRequest}", request);
            var restaurant = restaurantsRepository.GetById(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant),request.RestaurantId.ToString());

            var dish = mapper.Map<Dish>(request);
          return await dishesRepository.Create(dish);

        }
    }
}
