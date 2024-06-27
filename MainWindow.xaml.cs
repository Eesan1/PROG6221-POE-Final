using System.Windows;
using System.Windows.Controls;
using RecipeApp;

namespace RecipeAppWPF
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes;

        public MainWindow()
        {
            InitializeComponent();
            recipes = new List<Recipe>();
        }

        private void BtnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow(recipes);
            addRecipeWindow.Show();
        }

        private void BtnDisplayRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (recipes.Count == 0)
            {
                MessageBox.Show("No recipes available.");
                return;
            }

            DisplayRecipeWindow displayRecipeWindow = new DisplayRecipeWindow(recipes);
            displayRecipeWindow.Show();
        }

        private void BtnListRecipes_Click(object sender, RoutedEventArgs e)
        {
            //lstRecipes.Items.Clear();
            foreach (var recipe in recipes.OrderBy(r => r.Name))
            {
                lstRecipes.Items.Add(recipe.Name);
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnFilterRecipes_Click(object sender, RoutedEventArgs e)
        {
            string ingredientFilter = txtIngredientFilter.Text;
            string foodGroupFilter = (cbFoodGroupFilter.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "";
            bool isMaxCaloriesValid = double.TryParse(txtMaxCaloriesFilter.Text, out double maxCalories);

            var filteredRecipes = recipes.AsEnumerable();

            if (!string.IsNullOrEmpty(ingredientFilter))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.Name.Contains(ingredientFilter, StringComparison.OrdinalIgnoreCase)));
            }

            if (!string.IsNullOrEmpty(foodGroupFilter))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.FoodGroup.ToString() == foodGroupFilter));
            }

            if (isMaxCaloriesValid)
            {
                filteredRecipes = filteredRecipes.Where(r => r.TotalCalories <= maxCalories);
            }

            lstRecipes.Items.Clear();
            foreach (var recipe in filteredRecipes.OrderBy(r => r.Name))
            {
                lstRecipes.Items.Add(recipe.Name);
            }
        }
    }
}
