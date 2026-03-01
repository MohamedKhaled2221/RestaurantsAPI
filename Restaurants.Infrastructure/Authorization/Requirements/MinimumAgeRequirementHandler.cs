using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    internal class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger
        , IUserContext userContext)
        : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context
            , MinimumAgeRequirement requirement)
        {
            var currentuser = userContext.GetCurrentUser();



            logger.LogInformation("User: {Email},  date of birth {DoB} - Handling MinimumAgeRequirement",
                currentuser.Email,
                currentuser.DateofBirth);

            if (currentuser.DateofBirth == null)
            {
                logger.LogWarning("User date of birth is null");
                context.Fail();
                return Task.CompletedTask;
            }

            if (currentuser.DateofBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
            {
                logger.LogInformation("Authorization Succeeded");
                context.Succeed(requirement);
            }
            else
            {

                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
