using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Commands.GetDishByIdForResturant
{
    public class GetDishByIdForResturantQuery(int restaurantId ,int dishId) : IRequest<DishDto>
    {
       
        public int RestaurantId { get; set; } = restaurantId;
        public int DishId { get; set; } = dishId;
    
    }
}
