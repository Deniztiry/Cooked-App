using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

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
                // Neues Rezept erstellen
                var newRecipe = new Recipe
                {
                    Title = recipeName,
                    ImageUrl = _imagePath, // Bildpfad
                    Difficulty = new bool[] { false, false, false }, // Beispiel für Schwierigkeitsgrad
                    Ingredients = ingredients.Split('\n').ToList(), // Zutaten aus Editor
                    Instructions = instructions.Split('\n').ToList() // Zubereitung aus Editor
                };

                try
                {
                    // Pfad zur Datei
                    var filePath = Path.Combine(FileSystem.AppDataDirectory, "recipes.json");

                    // Rezepte aus der Datei laden
                    var recipes = LoadRecipes(filePath);

                    // Neues Rezept hinzufügen
                    recipes.Add(newRecipe);

                    // Rezepte wieder in die Datei speichern
                    var json = JsonSerializer.Serialize(recipes);
                    File.WriteAllText(filePath, json);

                    await DisplayAlert("Erfolgreich", "Rezept wurde hinzugefügt!", "OK");

                    // Zur MainPage zurückkehren und Rezeptliste aktualisieren
                    await Shell.Current.GoToAsync("//MainPage");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Fehler", $"Rezept konnte nicht gespeichert werden: {ex.Message}", "OK");
                }
            }
            else
            {
                await DisplayAlert("Fehler", "Bitte einen Rezeptnamen eingeben!", "OK");
            }
        }

        private List<Recipe> LoadRecipes(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    var recipes = JsonSerializer.Deserialize<List<Recipe>>(json);
                    return recipes ?? new List<Recipe>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden der Rezepte: {ex.Message}");
            }

            return new List<Recipe>();
        }
    }
}
