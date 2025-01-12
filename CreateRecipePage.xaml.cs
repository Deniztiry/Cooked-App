using Microsoft.Maui.Controls;
using System;
using System.IO;

namespace Cooked_App
{
    public partial class CreateRecipePage : ContentPage
    {
        private string _imagePath;

        public CreateRecipePage()
        {
            InitializeComponent();
        }

        private async void OnSelectImageClicked(object sender, EventArgs e)
        {
            try
            {
                // Datei auswählen
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Wähle ein Bild aus",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    _imagePath = result.FullPath;
                    RecipeImage.Source = ImageSource.FromFile(_imagePath);
                    RecipeImage.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fehler", $"Bild konnte nicht geladen werden: {ex.Message}", "OK");
            }
        }

        private async void OnSaveRecipeClicked(object sender, EventArgs e)
        {
            var recipeName = RecipeNameEntry.Text;
            var ingredients = IngredientsEditor.Text;
            var instructions = InstructionsEditor.Text;

            if (!string.IsNullOrWhiteSpace(recipeName))
            {
                // Beispiel für das Speichern der Rezeptdaten mit Bild
                var newRecipe = new Recipe
                {
                    Title = recipeName,
                    ImageUrl = _imagePath, // Speichert den Pfad oder die URL des Bildes
                    Difficulty = new bool[] { false, false, false } // Beispielwert
                };

                // Logik zur Speicherung (z. B. JSON-Update)
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
