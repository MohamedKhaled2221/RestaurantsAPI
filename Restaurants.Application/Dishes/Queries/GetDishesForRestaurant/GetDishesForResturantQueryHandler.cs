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

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant
{
    public class GetDishesForResturantQueryHandler(ILogger<GetDishesForResturantQueryHandler> logger
        , IRestaurantsRepository restaurantsRepository, IMapper mapper ) : IRequestHandler<GetDishesForResturantQuery, IEnumerable<DishDto>>
    {
        public async Task<IEnumerable<DishDto>> Handle(GetDishesForResturantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving dishes for restaurant with id: {RestaurantId}",request.RestaurantId);
            var restaurant =await restaurantsRepository.GetById(request.RestaurantId);
            if(restaurant==null) throw new NotFoundException(nameof(Restaurant),request.RestaurantId.ToString());
            var results = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
            return results;
        }
    }
}
