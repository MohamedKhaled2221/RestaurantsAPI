using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users
{
    public record CurrentUser(string Id, string Email,
        IEnumerable<string> Roles,string? Nationality , DateOnly? DateofBirth)
    {
        public bool IsInRole(string role) => Roles.Contains(role);
    }
}
