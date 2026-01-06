using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator()
        {
            RuleFor(x => x.Price)
               .GreaterThanOrEqualTo(0)
               .WithMessage("Price must be a non-negative number.");


            RuleFor(x => x.KiloCalories)
             .GreaterThanOrEqualTo(0)
             .WithMessage("KiloCalories must be a non-negative number."); ;
        }
    }
}
