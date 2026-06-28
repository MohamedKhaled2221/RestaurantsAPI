using AutoMapper;
using FluentAssertions;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.Application.Restaurants.Dtos.Tests
{
    public class RestaurantsProfileTests
    {
        private IMapper _mapper;

        public RestaurantsProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantsProfile>();
            });

          _mapper = configuration.CreateMapper();
        }
        [Fact()]
        public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
        {
            // arrange
          
            var restaurant = new Restaurant()
            {
                Id = 1,
                Name = "Test Restaurant",
                Category = "Italian",
                HasDelivery = true,
                ContactNumber = "12345789",
                ContactEmail = "test@example.com",
                Description = "A test restaurant",
                Address = new Address()
                {
                    City = "Test City",
                    Street = "Test Street",
                    PostalCode = "12345"
                }
            };
            // act 
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            // assert
            restaurantDto.Should().NotBeNull();
            restaurantDto.Id.Should().Be(restaurant.Id);
            restaurantDto.Name.Should().Be(restaurant.Name);
            restaurantDto.Category.Should().Be(restaurant.Category);
            restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
            restaurantDto.City.Should().Be(restaurant.Address?.City);
            restaurantDto.Street.Should().Be(restaurant.Address?.Street);
            restaurantDto.PostalCode.Should().Be(restaurant.Address?.PostalCode);

        }
    }
}