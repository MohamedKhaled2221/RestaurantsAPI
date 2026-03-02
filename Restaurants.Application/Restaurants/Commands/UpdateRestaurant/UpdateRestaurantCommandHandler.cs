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

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger, IMapper mapper,
        IRestaurantsRepository restaurantsRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<UpdateRestaurantCommand>
    {
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating restaurant with Id: {RestaurantId} with {@UpdateRestaurant}", request.Id,request);
            var restaurant = await restaurantsRepository.GetById(request.Id);

            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
                throw new ForbidException();
            mapper.Map(request, restaurant);
           

        await restaurantsRepository.SaveChanges();
           

        }
    }
}
