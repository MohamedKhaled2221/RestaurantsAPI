using FluentAssertions;
using Restaurants.Domain.Contants;
using Xunit;

namespace Restaurants.Application.Users.Tests
{
    public class CurrentUserTests
    {
        [Theory()]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.User)]

        public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
              // arrange
            var currentuser=new CurrentUser("1", "test@test.com", [UserRoles.Admin,UserRoles.User], null, null);
            // act
            var isInRole = currentuser.IsInRole(roleName);
           // assert

            isInRole.Should().BeTrue();
        }


        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
              // arrange
            var currentuser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);
            // act
            var isInRole = currentuser.IsInRole(UserRoles.Owner);
           // assert

            isInRole.Should().BeFalse();
        }

        [Fact()]
        public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
        {
              // arrange
            var currentuser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);
            // act
            var isInRole = currentuser.IsInRole(UserRoles.Admin.ToLower());
           // assert
            isInRole.Should().BeFalse();
        }
    }
}