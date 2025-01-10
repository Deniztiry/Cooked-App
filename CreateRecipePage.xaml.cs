using Microsoft.Maui.Controls;

namespace Cooked_App
{
    public partial class CreateRecipePage : ContentPage
    {
        public CreateRecipePage()
        {
            InitializeComponent();
        }

        private async void OnSaveRecipeClicked(object sender, EventArgs e)
        {
            // Beispiel für das Speichern eines neuen Rezepts
            var recipeName = RecipeNameEntry.Text;
            var ingredients = IngredientsEditor.Text;
            var instructions = InstructionsEditor.Text;

            if (!string.IsNullOrWhiteSpace(recipeName))
            {
                // Logik für das Hinzufügen des Rezepts zur MainPage
                await DisplayAlert("Erfolgreich", "Rezept wurde hinzugefügt!", "OK");
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                await DisplayAlert("Fehler", "Bitte einen Rezeptnamen eingeben!", "OK");
            }
        }
    }
}
