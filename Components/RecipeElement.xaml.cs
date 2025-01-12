using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Cooked_App.Components
{
    public partial class RecipeElement : ContentView
    {
        public static readonly BindableProperty RecipeProperty =
            BindableProperty.Create(nameof(Recipe), typeof(Recipe), typeof(RecipeElement), propertyChanged: OnRecipeChanged);

        public Recipe Recipe
        {
            get => (Recipe)GetValue(RecipeProperty);
            set => SetValue(RecipeProperty, value);
        }

        public ICommand ToggleFavoriteCommand { get; }
        public ICommand NavigateToRecipeDetailCommand { get; }

        public RecipeElement()
        {
            InitializeComponent();

            // Command zum Umschalten des Favoritenstatus
            ToggleFavoriteCommand = new Command(() =>
            {
                if (Recipe != null)
                {
                    Recipe.IsFavorite = !Recipe.IsFavorite; // Favoritenstatus umschalten
                    OnPropertyChanged(nameof(Recipe));
                }
            });

            // Command zum Navigieren zur Detailseite
            NavigateToRecipeDetailCommand = new Command(async () =>
            {
                if (Recipe != null)
                {
                    // Verwende den Titel oder andere Parameter für die Navigation
                    await Shell.Current.GoToAsync($"RecipeDetailPage?title={Recipe.Title}");
                }
            });
        }

        private static void OnRecipeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is RecipeElement control && newValue is Recipe recipe)
            {
                control.BindingContext = recipe;
            }
        }
    }
}