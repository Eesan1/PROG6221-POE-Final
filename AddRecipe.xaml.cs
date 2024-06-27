using System.Windows;
using System.Windows.Controls;
using RecipeApp;

namespace RecipeAppWPF
{
    public partial class AddRecipeWindow : Window
    {
        private List<Recipe> _recipes;
        public List<Recipe> Recipes
        {
            get { return _recipes; }
            set { _recipes = value; }
        }

        private Recipe _currentRecipe;
        public Recipe CurrentRecipe
        {  
            get { return _currentRecipe; }
            set { _currentRecipe = value; }
        }

        public AddRecipeWindow(List<Recipe> recipes)
        {
            InitializeComponent();
            Recipes = recipes ?? [];
            CurrentRecipe = new Recipe("");
        }

        private void BtnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtIngredientQuantity.Text, out double quantity) && double.TryParse(txtIngredientCalories.Text, out double calories) && cbFoodGroup.SelectedItem != null)
            {
                string name = txtIngredientName.Text;
                string unit = txtIngredientUnit.Text;
                FoodGroup foodGroup = (FoodGroup)Enum.Parse(typeof(FoodGroup), (cbFoodGroup.SelectedItem as ComboBoxItem).Content.ToString());

                CurrentRecipe.AddIngredient(name, quantity, unit, calories, foodGroup);
                MessageBox.Show("Ingredient added.");
            }
            else
            {
                MessageBox.Show("Please enter valid ingredient details.");
                return;
            }

            txtIngredientName.Clear();
            txtIngredientQuantity.Clear();
            txtIngredientUnit.Clear();
            txtIngredientCalories.Clear();
        }

        private void BtnEnterSteps_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtSteps.Text))
            {
                MessageBox.Show("Please enter valid step details.");
                return;
            }

            string step = txtSteps.Text;
            CurrentRecipe.Steps.Add(step);
            MessageBox.Show(step);
            txtSteps.Clear();
        }

        private void BtnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            CurrentRecipe.Name = txtRecipeName.Text;
            Recipes.Add(CurrentRecipe);
            MessageBox.Show("Recipe saved.");
            CurrentRecipe.CheckTotalCalories();
            this.Close();
        }
    }
}
