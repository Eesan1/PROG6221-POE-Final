using Xunit;
using RecipeApp;

namespace RecipeApp.Tests
{
    public class RecipeTests
    {
        [Fact]
        public void CalculateTotalCalories_ShouldReturnCorrectTotal()
        {
            // Arrange
            var recipe = new Recipe("Test Recipe");
            recipe.AddIngredient("Sugar", 1.0, "cup", 500, FoodGroup.Sugar);
            recipe.AddIngredient("Milk", 2.0, "cup", 150, FoodGroup.Dairy);

            // Act
            double totalCalories = recipe.TotalCalories;

            // Assert
            Assert.Equal(800, totalCalories);
        }

        [Fact]
        public void CalculateTotalCalories_ShouldReturnZeroWhenNoIngredients()
        {
            // Arrange
            var recipe = new Recipe("Empty Recipe");

            // Act
            double totalCalories = recipe.TotalCalories;

            // Assert
            Assert.Equal(0, totalCalories);
        }
    }
}
