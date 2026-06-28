using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
    {
        private  int[] allowpagesizes = { 5, 10, 15, 30};
        private string[] allowedSortByColumnNames = [nameof(RestaurantDto.Name)
            , nameof(RestaurantDto.Description), nameof(RestaurantDto.Category)];
        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize)
                .Must(value=> allowpagesizes.Contains(value))
                .WithMessage($"PageSize must be in [{string.Join(", ", allowpagesizes)}]");

            RuleFor(x => x.SortBy)
               .Must(value => allowedSortByColumnNames.Contains(value))
               .When(q=>q.SortBy != null)
               .WithMessage($"Sort  iis optional, or  must be in [{string.Join(", ", allowedSortByColumnNames)}]");
        }
    }
}
