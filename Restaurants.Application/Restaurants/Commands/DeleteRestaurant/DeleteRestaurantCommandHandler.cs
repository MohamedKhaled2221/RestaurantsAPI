using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Services;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{

    public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger
        , IRestaurantsRepository restaurantsRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
           logger.LogInformation("Deleting restaurant with Id: {RestaurantId}", request.Id);
            var restaurant = await restaurantsRepository.GetById(request.Id);
            if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant),request.Id.ToString());

            if(! restaurantAuthorizationService.Authorize(restaurant,ResourceOperation.Delete))
                throw new ForbidException();

            await restaurantsRepository.Delete(restaurant);
         
        }
    }
}
