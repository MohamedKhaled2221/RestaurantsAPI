using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDishes;
using Restaurants.Application.Dishes.Commands.GetDishByIdForResturant;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/dishes")]
    [ApiController]
    [Authorize]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute]int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
           var dishId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId },null);

        }
        [HttpGet]
        [Authorize(Policy = PolicyNames.AtLeast20)]
        public async  Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
        {
        var dishes =  await mediator.Send(new GetDishesForResturantQuery(restaurantId));
            return Ok(dishes);
        }
        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = await mediator.Send(new GetDishByIdForResturantQuery(restaurantId,dishId));
            return Ok(dish);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDishForRestaurant([FromRoute] int restaurantId)
        {
            await mediator.Send(new DeleteDishesForResataurantCommand(restaurantId));
            return NoContent();
        }

    }
}
