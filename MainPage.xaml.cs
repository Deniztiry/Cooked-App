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
            RecipesContainer.ItemsSource = null; // Setze auf null, um sicherzustellen, dass es neu geladen wird
            RecipesContainer.ItemsSource = _recipes; // Weise die neue Liste zu

            // Optional: Setze BindingContext explizit
            RecipesContainer.BindingContext = _recipes;
        }


        private List<Recipe> LoadRecipes()
        {
            try
            {
                var filePath = Path.Combine(FileSystem.AppDataDirectory, FileName);
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    Console.WriteLine($"Geladene JSON-Daten: {json}"); // Gib die geladenen JSON-Daten aus

                    var recipes = JsonSerializer.Deserialize<List<Recipe>>(json);

                    // Überprüfe die Anzahl der deserialisierten Rezepte
                    Console.WriteLine($"Anzahl der Rezepte: {recipes?.Count}");

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

    public class Recipe
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public bool[] Difficulty { get; set; } // Bool-Array für die Schwierigkeitsstufen
    }
}