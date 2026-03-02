using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Contants;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger,
        IUserContext userContext) : IRestaurantAuthorizationService
    {
        public bool Authorize(Restaurant restaurant, ResourceOperation resourceoperation)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Authorizing user {UserEmail} to{Operation} on restaurant {RestaurantName}",
                user.Email, resourceoperation, restaurant.Name);
            if (resourceoperation == ResourceOperation.Read || resourceoperation == ResourceOperation.Create)
            {
                logger.LogInformation("Create/read operations - Sucessful authorization");
                return true;
            }
            if (resourceoperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("Admin user , delete operation - Sucessful authorization");
                return true;
            }
            if (resourceoperation == ResourceOperation.Delete || resourceoperation == ResourceOperation.Update)
            {
                logger.LogInformation("Restaurant owner -  Successful authorization");
                return true;
            }
            return false;

        }
    }
}
