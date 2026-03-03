using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    internal class CreatedMultipleRestaurantsRequirementHandler (IRestaurantsRepository restaurantsRepository,
        IUserContext userContext)
        : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context
            , CreatedMultipleRestaurantsRequirement requirement)
        {
          var currentUser = userContext.GetCurrentUser();
            var restaurants = await restaurantsRepository.GetAllAsync();
            var createdRestaurantsCount = restaurants.Count(r => r.OwnerId == currentUser!.Id);
            if (createdRestaurantsCount >= requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
