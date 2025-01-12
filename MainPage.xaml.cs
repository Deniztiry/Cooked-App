using System.ComponentModel;
using System.Text.Json;
using System.Linq; // Für die LINQ-Abfrage

namespace Cooked_App
{
    public partial class MainPage : ContentPage
    {
        private const string FileName = "recipes.json";
        private List<Recipe> _recipes;
        private List<Recipe> _filteredRecipes; // Neue Liste für gefilterte Rezepte

        public MainPage()
        {
            InitializeComponent();
            LoadAndDisplayRecipes();
        }

        private void LoadAndDisplayRecipes()
        {
            _recipes = LoadRecipes();
            _filteredRecipes = _recipes; // Zu Beginn sind alle Rezepte sichtbar
            RecipesContainer.ItemsSource = _filteredRecipes; // Weist die gefilterten Rezepte zu
        }

        private List<Recipe> LoadRecipes()
        {
            try
            {
                var filePath = Path.Combine(FileSystem.AppDataDirectory, "recipes.json");

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


        // Event-Handler für den Suchbutton
        private void OnSearchButtonClicked(object sender, EventArgs e)
        {
            string searchText = SearchEntry.Text?.ToLower() ?? string.Empty;

            if (string.IsNullOrEmpty(searchText))
            {
                // Wenn kein Suchtext eingegeben wurde, zeige alle Rezepte
                _filteredRecipes = _recipes;
            }
            else
            {
                // Filtere Rezepte basierend auf dem Titel
                _filteredRecipes = _recipes.Where(recipe => recipe.Title.ToLower().Contains(searchText)).ToList();
            }

            // Aktualisiere die ItemsSource der CollectionView
            RecipesContainer.ItemsSource = _filteredRecipes;
        }

        private async void OnRecipeTapped(object sender, EventArgs e)
        {
            var tappedRecipe = (sender as Grid)?.BindingContext as Recipe;
            if (tappedRecipe != null)
            {
                await Navigation.PushAsync(new Detailpage(tappedRecipe));
            }
        }
    }

        public class Recipe : INotifyPropertyChanged
    {
        private bool _isFavorite;

        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public bool[] Difficulty { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Instructions { get; set; }
        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                if (_isFavorite != value)
                {
                    _isFavorite = value;
                    OnPropertyChanged(nameof(IsFavorite));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
