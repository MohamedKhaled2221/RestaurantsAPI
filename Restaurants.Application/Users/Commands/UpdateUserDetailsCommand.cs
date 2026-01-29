using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Restaurants.Application.Users.Commands
{
    public class UpdateUserDetailsCommand : IRequest
    {
        public DateOnly? DataofBirth { get; set; }
        public string? Nationality { get; set; }
    }
}
