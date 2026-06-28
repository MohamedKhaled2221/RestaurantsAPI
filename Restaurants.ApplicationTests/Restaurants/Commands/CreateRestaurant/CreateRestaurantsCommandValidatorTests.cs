using FluentValidation.TestHelper;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantsCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            // arrange
            var command = new CreateRestaurantCommand
            {
                Name = "Test",
                Category = "Italian",
                ContactEmail = "test@test.com",
                PostalCode = "12-345"
            };
            var validator = new CreateRestaurantsCommandValidator();

            //act 
            var result = validator.TestValidate(command);
            // assert
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact()]
        public void Validator_ForInValidCommand_ShouldHaveValidationErrors()
        {
            // arrange
            var command = new CreateRestaurantCommand
            {
                Name = "Te",
                Category = "Ita",
                ContactEmail = "@test.com",
                PostalCode = "12345"
            };
            var validator = new CreateRestaurantsCommandValidator();

            //act 
            var result = validator.TestValidate(command);
            // assert
            result.ShouldHaveValidationErrorFor(c => c.Name);
            result.ShouldHaveValidationErrorFor(c => c.Category);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);

        }

        [Theory()]
        [InlineData("Italian")]
        [InlineData("Mexican")]
        [InlineData("American")]
        [InlineData("Indian")]
        public void Validator_ForvalidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
        {
            // arrange
            var validator = new CreateRestaurantsCommandValidator();
            var command = new CreateRestaurantCommand { Category = category };
            // act
            var result = validator.TestValidate(command);
            // assert
            result.ShouldNotHaveValidationErrorFor(c => c.Category);
        }
    }

}