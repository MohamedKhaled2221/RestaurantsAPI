using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantsCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> validCategories = new List<string>
        {
            "Italian",
            "Chinese",
            "Mexican",
            "Indian",
            "French",
            "Japanese"
        };

        public CreateRestaurantsCommandValidator()
        {
            RuleFor(x => x.Name)
                .Length(3, 100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required");
            RuleFor(x => x.Category)
                .Must(validCategories.Contains)
                .WithMessage($"Invalid category. Please Choose from the valid Categories");
            //.Custom((value, context) =>
            //{
            //    if (!validCategories.Contains(value))
            //    {
            //        context.AddFailure("Category", $"Category must be one of the following: {string.Join(", ", validCategories)}");
            //    }
            //});



            RuleFor(x => x.ContactEmail)
                .EmailAddress() 
                .WithMessage("Please Provide a valid email address ");
            RuleFor(x => x.PostalCode)
                 .Matches(@"^\d{2}-\d{3}$")
                 .WithMessage("Please provide a valid a postal code (XX-XXX).");


        }
    }
}
