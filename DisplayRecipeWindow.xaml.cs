using System.Windows;
using RecipeApp;

namespace RecipeAppWPF
{
    public partial class DisplayRecipeWindow : Window
    {
        private List<Recipe> recipes;
        private Recipe? selectedRecipe;

        public DisplayRecipeWindow(List<Recipe> recipes)
        {
            InitializeComponent();
            this.recipes = recipes;

            foreach (var recipe in recipes.OrderBy(r => r.Name))
            {
                cbRecipes.Items.Add(recipe.Name);
            }
        }

        private void BtnDisplayRecipe_Click(object sender, RoutedEventArgs e)
        {
            string? selectedRecipeName = cbRecipes.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(selectedRecipeName))
            {
                MessageBox.Show("Please select a recipe.");
                return;
            }

            selectedRecipe = recipes.FirstOrDefault(r => r.Name == selectedRecipeName);
            if (selectedRecipe != null)
            {
                txtRecipeDetails.Text = selectedRecipe.ToString();

            }
        }

        public void BtnScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            ScaleRecipeWindow s = new ScaleRecipeWindow(selectedRecipe);
            s.Show();
        }

        public void BtnResetRecipe_Click(object sender, RoutedEventArgs e)
        {
            selectedRecipe?.ResetQuantities();
        }

        public void BtnClearRecipe_Click(object sender, RoutedEventArgs e)
        {
            selectedRecipe?.Clear();
        }
    }
}
