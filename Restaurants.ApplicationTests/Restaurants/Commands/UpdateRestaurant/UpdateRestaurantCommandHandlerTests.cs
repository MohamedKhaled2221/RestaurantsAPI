using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Services;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        private readonly Mock<IRestaurantsRepository> _restaurantRepositoryMock;
        private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
        private readonly Mock<IRestaurantAuthorizationService> _restaurantauthorizationServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UpdateRestaurantCommandHandler _handler;

        public UpdateRestaurantCommandHandlerTests(Mock<IRestaurantsRepository> restaurantRepositoryMock,
            Mock<ILogger<UpdateRestaurantCommandHandler>> loggerMock, Mock<IRestaurantAuthorizationService> restaurantauthorizationServiceMock, Mock<IMapper> mapperMock)
        {
            _restaurantRepositoryMock = restaurantRepositoryMock;
            _loggerMock = loggerMock;
            _restaurantauthorizationServiceMock = restaurantauthorizationServiceMock;
            _mapperMock = mapperMock;
            _handler = new UpdateRestaurantCommandHandler(_loggerMock.Object,
                _mapperMock.Object, _restaurantRepositoryMock.Object
                , _restaurantauthorizationServiceMock.Object);
        }

        [Fact()]
        public async Task Handle_WithValidRequest_ShouldUpdateRestaurants()
        {
            //arrange
            var restaurantId = 1;
            var command = new UpdateRestaurantCommand()
            {
                Id = restaurantId,
                Name = "Test Restaurant",
                Description = "Test Description",
                HasDelivery = true
            };
            var restaurant = new Restaurant()
            {
                Id = restaurantId,
                Name = "Old Name",
                Description = "Old Description",

            };
            _restaurantRepositoryMock.Setup(x => x.GetById(restaurantId)).ReturnsAsync(restaurant);
            _restaurantauthorizationServiceMock.Setup(x => x.Authorize(restaurant, ResourceOperation.Update)).Returns(true);

            //act
            await _handler.Handle(command, CancellationToken.None);
            //assert
            _restaurantRepositoryMock.Verify(x => x.SaveChanges(), Times.Once);
            _mapperMock.Verify(x => x.Map(command, restaurant), Times.Once);
        }
        [Fact()]
        public async Task Handle_WithNonExistingValidRestaurant_ShouldUpdateRestaurants()
        {
            //arrange
            var restaurantId = 2;
            var command = new UpdateRestaurantCommand()
            {
                Id = restaurantId,

            };
            _restaurantRepositoryMock.Setup(x => x.GetById(restaurantId)).ReturnsAsync((Restaurant?)null);

            //act
           Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            //assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Restaurant with id {restaurantId} not found.");
        }
    }
}