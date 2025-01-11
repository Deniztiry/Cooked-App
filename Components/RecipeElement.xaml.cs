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

        public RecipeElement()
        {
            InitializeComponent();
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
