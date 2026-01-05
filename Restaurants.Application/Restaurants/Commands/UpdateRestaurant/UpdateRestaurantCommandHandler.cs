using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger, IMapper mapper,
        IRestaurantsRepository restaurantsRepository) : IRequestHandler<UpdateRestaurantCommand,bool>
    {
        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating restaurant with Id: {RestaurantId} with {@UpdateRestaurant}", request.Id,request);
            var restaurant = await restaurantsRepository.GetById(request.Id);

            if (restaurant is null)
                return false;
            mapper.Map(request, restaurant);
           

        await restaurantsRepository.SaveChanges();
            return true;

        }
    }
}
