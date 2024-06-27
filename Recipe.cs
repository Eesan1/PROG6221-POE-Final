using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace RecipeApp
{
    public class Recipe
    {
        public string Name { get; set; }
        private List<Ingredient> ingredients;
        public List<Ingredient> Ingredients
        {
            get { return ingredients; }
        }
        private List<string> steps;
        public List<string> Steps
        {
            get { return steps; }
            set { steps = value; }
        }

        public delegate void HighCalorieNotification(string message); // delegate type for notifications 
        public HighCalorieNotification highCalorieNotification { get; }

        private List<double> originalQuantities;

        public double TotalCalories => CalculateTotalCalories();

        public Recipe(string name)
        {
            Name = name;
            ingredients = new List<Ingredient>();
            steps = new List<string>();
            originalQuantities = new List<double>();
            highCalorieNotification = ShowHighCalorieMessage;
        }

        public void AddIngredient(string name, double quantity, string unit, double calories, FoodGroup foodGroup) // adds ingredients to the recipe
        {
            ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup));
            originalQuantities.Add(quantity);
        }

        public void AddStep(string step) // adds step to the recipe
        {
            steps.Add(step);
        }

        public void Scale(double factor) // scales the quantities of the ingredients
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                ingredients[i].Quantity *= factor;
            }
        }

        public void Clear() // clear all ingredients
        {
            ingredients.Clear();
            steps.Clear();
            originalQuantities.Clear();
        }

        public void ResetQuantities() // resets the quantities to the original values
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                ingredients[i].Quantity = originalQuantities[i];
            }
        }

        public double CalculateTotalCalories() // calculates total calories
        {
            double total = 0;
            foreach (var ingredient in ingredients)
                total += ingredient.Calories;
            return total;
        }

        public override string ToString() // returns a string representation of the recipe
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Name}");
            sb.AppendLine("Ingredients:");
            foreach (var ingredient in ingredients)
            {
                sb.AppendLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.Calories} calories, {ingredient.FoodGroup})");
            }
            sb.AppendLine("\nSteps:");
            for (int i = 0; i < steps.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {steps[i]}");
            }
            sb.AppendLine($"\nTotal Calories: {TotalCalories}");
            return sb.ToString();
        }

        public void CheckTotalCalories()
        {
            if (TotalCalories > 300) // checks if the calories don't exceed 300
                highCalorieNotification?.Invoke($"The recipe {Name} has high calories (more than 300).");
        }

        public static void ShowHighCalorieMessage(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    public class Ingredient
    {
        public string Name { get; }
        public double Quantity { get; set; }
        public string Unit { get; }
        public double Calories { get; }
        public FoodGroup FoodGroup { get; }

        public Ingredient(string name, double quantity, string unit, double calories, FoodGroup foodGroup)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }

    public enum FoodGroup // different food groups 
    {
        Dairy,
        Protein,
        Fruit,
        Vegetable,
        Grain,
        Fat,
        Sugar
    }
}
