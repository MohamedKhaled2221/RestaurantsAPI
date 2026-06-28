using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization.Requirements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Restaurants.Infrastructure.Authorization.Requirements.Tests
{
    
    public class CreatedMultipleRestaurantsRequirementHandlerTests
    {
        [Fact()]
        public async Task HandleRequirementAsync_UserHasNotCreatedMultipleRestaurants_ShouldFail()
        {
            // Arrange
            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(uc => uc.GetCurrentUser()).Returns(currentUser);

            var restaurants = new List<Restaurant>
            {
                new(){  OwnerId = currentUser.Id },
               
                new (){ OwnerId = "2" }
            };
            var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
            restaurantRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

            var requirement = new CreatedMultipleRestaurantsRequirement(2);
            var handler = new CreatedMultipleRestaurantsRequirementHandler(restaurantRepositoryMock.Object, userContextMock.Object);

            var authorizationContext = new AuthorizationHandlerContext([requirement], null, null);
            //act 
            await handler.HandleAsync(authorizationContext);
            // Assert
            authorizationContext.HasSucceeded.Should().BeFalse();
            authorizationContext.HasFailed.Should().BeTrue();
        }
        [Fact()]
        public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceed()
        {
            // Arrange
            var currentUser =new CurrentUser("1", "test@test.com", [],null,null);
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(uc => uc.GetCurrentUser()).Returns(currentUser);

            var restaurants= new List<Restaurant>
            {
                new(){  OwnerId = currentUser.Id },
                new (){ OwnerId = currentUser.Id },
                new (){ OwnerId = "2" }
            };
            var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
            restaurantRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

            var requirement = new CreatedMultipleRestaurantsRequirement(2);
            var handler = new CreatedMultipleRestaurantsRequirementHandler( restaurantRepositoryMock.Object, userContextMock.Object);

            var authorizationContext = new AuthorizationHandlerContext([requirement ], null, null);
            //act 
           await handler.HandleAsync(authorizationContext);
            // Assert
            authorizationContext.HasSucceeded.Should().BeTrue();
        }
    }
}