using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Maui.Controls;

namespace Cooked_App
{

    public class DifficultyToCircleCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool[] difficulty && parameter is string param)
            {
                int paramValue = int.Parse(param);
                // Gebe zurück, ob die Schwierigkeitsstufe "true" ist
                return difficulty.Length >= paramValue && difficulty[paramValue - 1];
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public partial class MainPage : ContentPage
    {
        private const string FileName = "recipes.json";  // Der Dateiname für die Rezepte
        private List<Recipe> _recipes;

        public MainPage()
        {
            InitializeComponent();
            _recipes = LoadRecipes();  // Rezepte laden
            RecipesCollectionView.ItemsSource = _recipes;  // CollectionView mit den geladenen Rezepten füllen
        }

        private List<Recipe> LoadRecipes()
        {
            try
            {
                var filePath = Path.Combine(FileSystem.AppDataDirectory, FileName);
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<List<Recipe>>(json) ?? new List<Recipe>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fehler beim Laden der Rezepte: {ex.Message}");
            }

            return new List<Recipe>();  // Wenn keine Datei gefunden wurde oder ein Fehler auftritt, eine leere Liste zurückgeben
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            var newRecipe = new Recipe
            {
                Title = "Neues Rezept",  // Beispiel für ein neues Rezept
                ImageUrl = "https://example.com/image.jpg",  // Beispielbild
                Difficulty = new bool[] { true, false, true }  // Beispiel für Schwierigkeitsgrad
            };

            _recipes.Add(newRecipe);  // Neues Rezept hinzufügen
            SaveRecipes();  // Rezepte speichern
            RecipesCollectionView.ItemsSource = null;  // CollectionView zurücksetzen
            RecipesCollectionView.ItemsSource = _recipes;  // CollectionView neu binden
        }

        private void SaveRecipes()
        {
            try
            {
                var filePath = Path.Combine(FileSystem.AppDataDirectory, FileName);
                var json = JsonSerializer.Serialize(_recipes);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern der Rezepte: {ex.Message}");
            }
        }
        
    }

    // Rezeptklasse, die die Eigenschaften des Rezepts beschreibt
    public class Recipe
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public bool[] Difficulty { get; set; }  // Bool-Array für die Schwierigkeitsstufen
    }
}
