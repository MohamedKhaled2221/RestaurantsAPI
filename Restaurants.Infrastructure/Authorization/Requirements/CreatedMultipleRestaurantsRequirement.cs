using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class CreatedMultipleRestaurantsRequirement(int minimumRestaurantsCreated) : IAuthorizationRequirement
    {
        public int MinimumRestaurantsCreated { get; } = minimumRestaurantsCreated;
    }
}
