using System.Text.Json;

namespace Cooked_App
{
    public partial class MainPage : ContentPage
    {
        private const string FileName = "recipes.json";
        private List<Recipe> _recipes;

        public MainPage()
        {
            InitializeComponent();
            LoadAndDisplayRecipes();
        }

        private void LoadAndDisplayRecipes()
        {
            _recipes = LoadRecipes();

            RecipesContainer.Children.Clear();
            foreach (var recipe in _recipes)
            {
                RecipesContainer.Children.Add(new Components.RecipeElement { Recipe = recipe });
            }
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
                Console.WriteLine($"Fehler beim Laden der Rezepte: {ex.Message}");
            }

            return new List<Recipe>();
        }
    }

}

// Rezeptklasse, die die Eigenschaften des Rezepts beschreibt
public class Recipe
{
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public bool[] Difficulty { get; set; }  // Bool-Array f�r die Schwierigkeitsstufen
}

