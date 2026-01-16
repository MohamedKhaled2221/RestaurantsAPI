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

namespace Restaurants.Application.Dishes.Commands.DeleteDishes
{
    public class DeleteDishesForResataurantCommandHandler(ILogger<DeleteDishesForResataurantCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository , IMapper mapper
        , IDishesRepository dishesRepository) : IRequestHandler<DeleteDishesForResataurantCommand>
    {
        public async Task Handle(DeleteDishesForResataurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogWarning("Deleting dishes for restaurant with ID {RestaurantId}", request.RestaurantId);
            var restaurant =await restaurantsRepository.GetById(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            await dishesRepository.Delete(restaurant.Dishes);
        }
    }
}
