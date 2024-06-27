using System;
using System.Windows;
using System.Windows.Controls; // Add this using directive for ComboBoxItem
using RecipeApp;

namespace RecipeAppWPF
{
    public partial class ScaleRecipeWindow : Window
    {
        private Recipe? scaledRecipe;

        public ScaleRecipeWindow(Recipe? recipeToBeScaled)
        {
            InitializeComponent();
            scaledRecipe = recipeToBeScaled ?? new Recipe("Name"); // Adjust according to Recipe constructor
            DataContext = scaledRecipe; // Set DataContext for data binding
        }

        public void BtnDoneScale_Click(object sender, RoutedEventArgs e)
        {
            double result;
            if (cbScale.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)cbScale.SelectedItem;
                string? content = selectedItem.Content.ToString();
                MessageBox.Show($"Selected item: {content}");
                
                if (double.TryParse(content, out result))
                {
                    scaledRecipe?.Scale(result);
                    MessageBox.Show($"Recipe scaled by {result}.");
                }
                else
                {
                    // Parsing failed, handle the error
                    MessageBox.Show("An error occurred.");
                }
            }
            else
            {
                // No item is selected
                MessageBox.Show("No item was selected.");
            }
        }
    }
}
