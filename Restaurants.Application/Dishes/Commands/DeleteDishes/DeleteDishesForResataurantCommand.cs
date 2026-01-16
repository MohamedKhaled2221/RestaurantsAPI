using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes
{
    public class DeleteDishesForResataurantCommand(int RestaurantId ) : IRequest
    {
        public int RestaurantId { get; set; } = RestaurantId;


    }
    
    }

